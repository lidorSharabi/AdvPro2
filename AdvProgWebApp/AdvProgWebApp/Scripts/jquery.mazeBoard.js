
(function ($) {
    $.fn.mazeBoard = function (mazeData,
        startRow, startCol,
        exitRow, exitCol,
        playerImage,
        exitImage) {
            var myCanvas = this[0];
            var context = myCanvas.getContext("2d");
            var rows = mazeData.length;
            var cols = mazeData[0].length;
            var cellWidth = myCanvas.width / cols;
            var cellHeight = myCanvas.height / rows;
            var currentStateCol = startCol;
            var currentStateRow = startRow;
            for (var i = 0; i < rows; i++) {
                for (var j = 0; j < cols; j++) {
                    if (mazeData[i][j] == 1) {
                        context.fillRect(cellWidth * j, cellHeight * i,
                            cellWidth, cellHeight);
                    }
                }
            }
            playerImage.onload = function () {
                context.drawImage(playerImage, cellHeight * startCol, cellWidth * startRow, cellWidth, cellHeight);
            };
            exitImage.onload = function () {
                context.drawImage(exitImage, cellHeight * exitCol, cellWidth * exitRow, cellWidth, cellHeight);
            };

            addKeyboardListener();
            function moveSelection(e) {
                switch (e.keyCode) {
                    case 37:
                        leftArrowPressed();
                        checkIfWinner();
                        break;
                    case 39:
                        rightArrowPressed();
                        checkIfWinner();
                        break;
                    case 38:
                        upArrowPressed();
                        checkIfWinner();
                        break;
                    case 40:
                        downArrowPressed();
                        checkIfWinner();
                        break;
                }
            };

            function clearPlayer() {
                context.clearRect(currntStateCol * cellHeight, currntStateRow * cellWidth, cellWidth, cellHeight);
            };

            function drawPlayer() {
                context.drawImage(player_Image, currntStateCol * cellHeight, currntStateRow * cellWidth, cellWidth, cellHeight);
            };

            function checkIfWinner() {
                if (currntStateCol == exitCol && currntStateRow == exitRow) {
                    alert("winner");
                    removeKeyboardListener();
                }
            };

            function removeKeyboardListener() {
                myCanvas.onkeydown = null;
            };

            function addKeyboardListener() {
                myCanvas.onkeydown = moveSelection.bind(this);
            };

            function leftArrowPressed() {
                if (currntStateCol - 1 >= 0 && mazeData[currntStateRow][(currntStateCol - 1)] == 0) {
                    clearPlayer();
                    currntStateCol -= 1;
                    drawPlayer();
                }
            };

            function rightArrowPressed() {
                if (currntStateCol + 1 <= cols - 1 && mazeData[currntStateRow][(currntStateCol + 1)] == 0) {
                    clearPlayer();
                    currntStateCol += 1;
                    drawPlayer();
                }
            };

            function upArrowPressed() {
                if (currntStateRow - 1 >= 0 && mazeData[currntStateRow-1][(currntStateCol)] == 0) {
                    clearPlayer();
                    currntStateRow -= 1;
                    drawPlayer();
                }
            };

            function downArrowPressed() {
                if (currntStateRow + 1 <= rows - 1 && mazeData[currntStateRow + 1][(currntStateCol)] == 0) {
                    clearPlayer();
                    currntStateRow += 1;
                    drawPlayer();
                }
            };

        return this;
    };
})(jQuery);

var mazeData = [[0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0],
[0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0],
[0, 0, 0, 0, 1, 0, 0, 1, 0, 1, 0],
[0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0],
[0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0],
[0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0],
[0, 0, 0, 1, 0, 0, 1, 1, 0, 1, 0],
[0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0]];
var startRow = 0;
var startCol = 1;
var exitRow = 7;
var exitCol = 10;
var playerImage = new Image();
playerImage.src = "Images/dog.jpg"
var exitImage = new Image;
exitImage.src="Images/exit.png"
$("#mazeCanvas").mazeBoard(mazeData,
    startRow, startCol,
    exitRow, exitCol,
    playerImage,
    exitImage);