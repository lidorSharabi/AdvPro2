src = "Scripts/jquery.mazeBoard.js";

(function ($) {
    $("#StartGameBtn").click(function () {
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
        /*
        var mazeData = "0001000100000001000000000010010100101010100001000000010000101010000001001101000000100000";
        var startRow = 0;
        var startCol = 1;
        var exitRow = 7;
        var exitCol = 10;
        */
        var playerImage = new Image();
        playerImage.src = "Images/dog.jpg"
        var exitImage = new Image;
        exitImage.src = "Images/exit.png"
        $("#mazeCanvas").mazeBoard(mazeData,
            startRow, startCol,
            exitRow, exitCol,
            playerImage,
            exitImage);
        $("#div2").show();
        document.getElementById("mazeCanvas").focus();
            });
    });
})(jQuery);