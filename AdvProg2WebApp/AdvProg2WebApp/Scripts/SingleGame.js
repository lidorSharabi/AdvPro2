src = "Scripts/jquery.mazeBoard.js";

$("#MazeRows").val(localStorage.getItem("rows"));
$("#MazeCols").val(localStorage.getItem("cols"));
$("#algo").val(localStorage.getItem("algo"));
var mazeCanvas = $("#mazeCanvas");

(function ($) {
    $("#StartGameBtn").click(function () {
        document.getElementById("MazeName").className = "";
        document.getElementById("MazeCols").className = "";
        document.getElementById("MazeRows").className = "";
        var check = true;
            if (!$("#MazeName").val()) {
                document.getElementById("MazeName").className = "error";
                check= false;
            }

            if (!$("#MazeRows").val() || $("#MazeRows").val() < 1 || $("#MazeRows").val()>100) {
                document.getElementById("MazeRows").className = "error";
                check= false;
            }

            if (!$("#MazeCols").val() || $("#MazeCols").val() < 1 || $("#MazeCols").val() > 100) {
                document.getElementById("MazeCols").className = "error";
                check= false;
            }
            if (!check)
            {
                return check;
            }
        $("#loader").show();
        var apiUrl = "api/Single/GetMaze";
        var name = $("#MazeName").val();
        var rows = $("#MazeRows").val();
        var cols = $("#MazeCols").val();
        var mazeData = 0;
        var startRow = 0;
        var startCol = 0;
        var exitRow = 0;
        var exitCol = 0;
        $.get(apiUrl, { name: name, cols: cols, rows: rows })
            .done(function (msg) {
                mazeData = msg.MazeString;
                startRow = msg.StartRow;
                startCol = msg.StartCol;
                exitRow = msg.ExitRow;
                exitCol = msg.ExitCol;
                var playerImage = new Image();
                playerImage.src = "Images/dog.jpg"
                var exitImage = new Image;
                exitImage.src = "Images/exit.png"
                mazeCanvas.mazeBoard(mazeData,
                    startRow, startCol,
                    exitRow, exitCol,
                    playerImage,
                    exitImage);
                $("#loader").hide();
                $("#div2").show();
                $("#solve").show();
                $("#algorithm").show();
                document.getElementById("mazeCanvas").focus();
            })
            .fail(function (jqXHR, textStatus, err) {
                mazeCanvas.text("Error: " + err);
            });
        
    });


    $("#SolveGameBtn").click(function () {
        var apiUrl = "api/Single/SolveMaze";
        var name = $("#MazeName").val();
        var algo = $("#algo").val();
        $.get(apiUrl, { name: name, algo: algo })
            .done(function (msg) {
                mazeCanvas.solveMaze(msg);
            })
            .fail(function (jqXHR, textStatus, err) {
                mazeCanvas.text("Error: " + err);
            });
    });
})(jQuery);