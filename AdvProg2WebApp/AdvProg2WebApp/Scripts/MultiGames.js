src = "Scripts/jquery.multiMaze.js";

$("#MazeRows").val(localStorage.getItem("rows"));
$("#MazeCols").val(localStorage.getItem("cols"));
var gamesList = $("#games");
var mazeCanvas = $("#mazeCanvas");
var opponentMazeCanvas = $("#OpponentMazeCanvas");

var multiplayer = $.connection.multiPlayerHandler;
multiplayer.client.broadcastMessage = function (msg) {
    if (msg === "Game Ended") {
        var canvas = document.getElementById("mazeCanvas");
        var context = canvas.getContext("2d");
        var endOfGame_image = new Image();
        context.clearRect(0, 0, canvas.width, canvas.height);
        endOfGame_image.src = "Images/sadDog.jpg";
        endOfGame_image.onload = function () {
            context.drawImage(endOfGame_image, 100, 150, canvas.width - 200, canvas.height - 200);
        };
        context.font = "32px Arial";
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

        $.get(usersUrl, { id: userName }).done(function (data) {
            user.UserNameId = data.UserNameId;
            user.Losses = data.Losses + 1;
            user.Victories = data.Victories;
            user.MailAddress = data.MailAddress;
            user.Password = data.Password;
            $.post(usersUrl + "Update", user).done(function (data) {
                document.getElementById("cd-user-modal").classList.remove('is-visible');
            }).fail(function (response) {
                alert('Something went worng with server...');
            });

        }).fail(function (response) {
            alert('Something went worng with server...');
        });
        canvas.onkeydown = null;
        return;
    }

    if (msg === "left" || msg === "right" || msg === "up" || msg === "down") {
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

    if (msg === "Game Closed") {
        alert("Game was closed");
        window.open("index.html", '_self');
        return;
    }

    var name = msg.Name;
    var rows = msg.Rows;
    var cols = msg.Cols;
    mazeData = msg.MazeString;
    startRow = msg.StartRow;
    startCol = msg.StartCol;
    exitRow = msg.ExitRow;
    exitCol = msg.ExitCol;
    var playerImage = new Image();
    playerImage.src = "Images/dog.jpg";
    var exitImage = new Image;
    exitImage.src = "Images/exit.png";
    mazeCanvas.mazeBoard(multiplayer, name, rows, cols, mazeData,
        startRow, startCol,
        exitRow, exitCol,
        playerImage,
        exitImage);
    var oPlayerImage = new Image();
    oPlayerImage.src = "Images/Opponent_Cat.jpg";
    var oExitImage = new Image;
    oExitImage.src = "Images/exit - Copy.png";
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
$.connection.hub.start().done(function () {
    $("#StartGameBtn").click(function () {
        document.getElementById("MazeName").className = "";
        document.getElementById("MazeCols").className = "";
        document.getElementById("MazeRows").className = "";
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

    $("#JoinGameBtn").click(function () {
        var name = $("#games").val();
        if (name) {
            multiplayer.server.joinGame(name);
            $("#left").hide();
        }
    });

});

(function ($) {
    $("#games").focus(function () {
        var apiUrl = "api/Multi/GetList";
        $.get(apiUrl)
            .done(function (msg) {
                gamesList.mazeList(msg);
            })
            .fail(function (jqXHR, textStatus, err) {
                if (jqXHR.status === 500) {
                    alert("error in connection to server");
                    return;
                }
                mazeCanvas.text("Error: " + err);
            });
    });

})(jQuery);

function focusFunction() {
    var apiUrl = "api/Multi/GetList";
    $.get(apiUrl)
        .done(function (msg) {
            gamesList.mazeList(msg);
        })
        .fail(function (jqXHR, textStatus, err) {
            if (jqXHR.status === 500) {
                alert("error in connection to server");
                return;
            }
            mazeCanvas.text("Error: " + err);
        });
}