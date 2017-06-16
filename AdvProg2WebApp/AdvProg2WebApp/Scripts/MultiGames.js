src = "Scripts/jquery.multiMaze.js";

$("#MazeRows").val(localStorage.getItem("rows"));
$("#MazeCols").val(localStorage.getItem("cols"));
var gamesList = $("#games");
var mazeCanvas = $("#mazeCanvas");
var opponentMazeCanvas = $("#OpponentMazeCanvas");

var multiplayer = $.connection.multiPlayerHandler;
multiplayer.client.broadcastMessage = function (msg) {
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
    mazeCanvas.mazeBoard(name, rows, cols, mazeData,
        startRow, startCol,
        exitRow, exitCol,
        playerImage,
        exitImage);
    var oPlayerImage = new Image();
    oPlayerImage.src = "Images/dog - Copy.jpg"
    var oExitImage = new Image;
    oExitImage.src = "Images/exit - Copy.png"
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
    });

    $("#JoinGameBtn").click(function () {
        var name = $("#games").val();
        if (name) {
            multiplayer.server.joinGame(name);
        }
    });

});

(function ($) {
    $("#games").slideDown(function () {
        var apiUrl = "api/Multi/GetList";
        $.get(apiUrl)
            .done(function (msg) {
                gamesList.mazeList(msg);
            })
            .fail(function (jqXHR, textStatus, err) {
                if (jqXHR.status == 500) {
                    alert("error in connection to server");
                    return;
                }
                mazeCanvas.text("Error: " + err);
            });
    });

})(jQuery);