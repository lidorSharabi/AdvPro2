src = "Scripts/jquery.multiMaze.js";

//get the default settings from the local storage
$("#MazeRows").val(localStorage.getItem("rows"));
$("#MazeCols").val(localStorage.getItem("cols"));
var gamesList = $("#games");
var mazeCanvas = $("#mazeCanvas");
var opponentMazeCanvas = $("#OpponentMazeCanvas");

//try to get connection with 
var multiplayer = $.connection.multiPlayerHandler;
multiplayer.client.broadcastMessage = function (msg) {
    if (msg == "Game Ended") {
        var canvas = document.getElementById("mazeCanvas");
        var context = canvas.getContext("2d");
        var endOfGame_image = new Image();
        context.clearRect(0, 0, canvas.width, canvas.height);
        endOfGame_image.src = "Images/sadDog.jpg";
        endOfGame_image.onload = function () {
            context.drawImage(endOfGame_image, 100, 150, canvas.width - 200, canvas.height - 200);
        }
        context.font = "32px Arial"
        context.fillText("You Lost!", 75, 100);
        //call http request to add current user one loss
        var usersUrl = "api/Users/";
        var userName = sessionStorage.getItem('userName');
        var user = {
            UserNameId: 0,
            Losses: 0,
            Victories: 0,
            MailAddress: 0,
            Password: 0
        };

        //get the user detials and add to him one loss
        $.get(usersUrl, { id: userName }).done(function (data) {
            user.UserNameId = data.UserNameId;
            user.Losses = data.Losses + 1;
            user.Victories = data.Victories;
            user.MailAddress = data.MailAddress;
            user.Password = data.Password;
            //save the updated details of the user in DB
            $.post(usersUrl + "Update", user).done(function (data) {
                document.getElementById("cd-user-modal").classList.remove('is-visible');
            }).fail(function (response) {
                //if the request failed show error alert
                alert('Something went worng with server...');
            });

        }).fail(function (response) {
            //if the request failed show error alert
            alert('Something went worng with server...');
        });
        canvas.onkeydown = null;
        return;
    }

    //move the opponent according the msg value
    if (msg == "left" || msg == "right" || msg == "up" || msg == "down") {
        switch (msg) {
            case 'left':
                opponentMazeCanvas.moveLeft();
                break;
            case 'right':
                opponentMazeCanvas.moveRight();
                break;
            case 'up':
                opponentMazeCanvas.moveUp();
                break;
            case 'down':
                opponentMazeCanvas.moveDown();
                break;
        }
        return;
    }

    //end the game in case this msg
    if (msg == "Game Closed") {
        alert("Game was closed");
        window.open("index.html", '_self');
        return;
    }

    //draw user and opponent maze board
    var name = msg.Name;
    var rows = msg.Rows;
    var cols = msg.Cols;
    mazeData = msg.MazeString;
    startRow = msg.StartRow;
    startCol = msg.StartCol;
    exitRow = msg.ExitRow;
    exitCol = msg.ExitCol;
    var playerImage = new Image();
    playerImage.src = "Images/dog.jpg"
    var exitImage = new Image;
    exitImage.src = "Images/exit.png"

    //draw user maze board
    mazeCanvas.mazeBoard(multiplayer, name, rows, cols, mazeData,
        startRow, startCol,
        exitRow, exitCol,
        playerImage,
        exitImage);
    var oPlayerImage = new Image();
    oPlayerImage.src = "Images/Opponent_Cat.jpg"
    var oExitImage = new Image;
    oExitImage.src = "Images/exit - Copy.png"
    //draw opponent maze board
    opponentMazeCanvas.opponentMazeBoard(name, rows, cols, mazeData,
        startRow, startCol,
        exitRow, exitCol,
        oPlayerImage,
        oExitImage);
    $("#loader").hide();
    $("#mazeCanvas").show();
    $("#label").show();
    $("#right").show();
    document.getElementById("mazeCanvas").focus();

};

/*
    @description click on StartGameBtn ask for server to start a game with another user
*/
$.connection.hub.start().done(function () {
    $("#StartGameBtn").click(function () {
        document.getElementById("MazeName").className = "";
        document.getElementById("MazeCols").className = "";
        document.getElementById("MazeRows").className = "";

        /*
        *check that all the necessary fields aren't empty
        *show validation for each empty field
        */
        var check = true;
        if (!$("#MazeName").val()) {
            document.getElementById("MazeName").className = "error";
            check = false;
        }

        if (!$("#MazeRows").val() || $("#MazeRows").val() < 1 || $("#MazeRows").val() > 100) {
            document.getElementById("MazeRows").className = "error";
            check = false;
        }

        if (!$("#MazeCols").val() || $("#MazeCols").val() < 1 || $("#MazeCols").val() > 100) {
            document.getElementById("MazeCols").className = "error";
            check = false;
        }
        if (!check) {
            return check;
        }
        $("#loader").show();
        var name = $("#MazeName").val();
        var rows = $("#MazeRows").val();
        var cols = $("#MazeCols").val();
        multiplayer.server.startGame(name, rows, cols);
        $("#left").hide();
    });

/*
    @description click on StartGameBtn ask for server to join game with another user
*/
    $("#JoinGameBtn").click(function () {
        var name = $("#games").val();
        if (name) {
            multiplayer.server.joinGame(name);
            $("#left").hide();
        }
    });

});

/*
    @description ask for all the available games
    and present them in list
*/
(function ($) {
    $("#games").focus(function () {
        var apiUrl = "api/Multi/GetList";
        $.get(apiUrl)
            .done(function (msg) {
                gamesList.mazeList(msg);
            })
            .fail(function (jqXHR, textStatus, err) {
                //if the request failed show error alert
                if (jqXHR.status == 500) {
                    alert("error in connection to server");
                    return;
                }
                mazeCanvas.text("Error: " + err);
            });
    });

})(jQuery);

/*
    @description ask for all the available games when user click on the list DOM
    and present them in list
*/
function focusFunction() {
    var apiUrl = "api/Multi/GetList";
    $.get(apiUrl)
        .done(function (msg) {
            gamesList.mazeList(msg);
        })
        .fail(function (jqXHR, textStatus, err) {
            if (jqXHR.status == 500) {
                //if the request failed show error alert
                alert("error in connection to server");
                return;
            }
            mazeCanvas.text("Error: " + err);
        });
};