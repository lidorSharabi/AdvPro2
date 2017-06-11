
(function ($) {
    $.fn.mazeBoard = function (mazeData,
        startRow, startCol,
        exitRow, exitCol,
        playerImage,
        exitImage) {
            var myCanvas = this[0];
            var context = myCanvas.getContext("2d");
            var rows = 8;
            var cols = 11;
            var cellWidth = myCanvas.width / cols;
            var cellHeight = myCanvas.height / rows;
            var colPlayerPos = startCol;
            var rowPlayerPos = startRow;
            var counter = 0;
            var initialIndexInMaze = 0;
            for (var i = 0; i < rows; i++) {
                for (var j = 0; j < cols; j++) {
                    if (mazeData[counter] == '1') {
                        context.fillRect(cellWidth * j, cellHeight * i, cellWidth, cellHeight);
                    }
                    if (i == startRow && j == startCol) {
                        playerImage.onload = function () {
                            context.drawImage(playerImage, cellWidth * startCol, cellHeight * startRow, cellWidth, cellHeight);
                        };
                        indexInMaze = counter;
                    }
                    if (i == exitRow && j == exitCol) {
                        exitImage.onload = function () {
                            context.drawImage(exitImage, cellWidth * exitCol, cellHeight * exitRow, cellWidth, cellHeight);
                        };
                    }
                    counter++;
                }
            }



            addKeyboardListener();
            function moveSelection(e) {
                switch (e.keyCode) {
                    case 37:
                        if ((colPlayerPos - 1) >= 0 && mazeData[indexInMaze - 1] == '0') {
                            clearPlayer();
                            colPlayerPos -= 1;
                            indexInMaze -= 1;
                            drawPlayer();
                        }
                        checkIfWinner();
                        break;
                    case 39:
                        if ((colPlayerPos + 1) < cols && mazeData[indexInMaze + 1] == '0') {
                            clearPlayer();
                            colPlayerPos += 1;
                            indexInMaze += 1;
                            drawPlayer();
                        }
                        checkIfWinner();
                        break;
                    case 38:
                        if ((rowPlayerPos - 1) >= 0 && mazeData[indexInMaze - cols] == '0') {
                            clearPlayer();
                            rowPlayerPos -= 1;
                            indexInMaze -= cols;
                            drawPlayer();
                        }
                        checkIfWinner();
                        break;
                    case 40:
                        if ((rowPlayerPos + 1) < rows && mazeData[indexInMaze + cols] == '0') {
                            clearPlayer();
                            rowPlayerPos += 1;
                            indexInMaze += cols;
                            drawPlayer();
                        }
                        checkIfWinner();
                        break;
                }
            };

            function clearPlayer() {
                context.clearRect(colPlayerPos * cellWidth, rowPlayerPos * cellHeight, cellWidth, cellHeight);
            };

            function drawPlayer() {
                context.drawImage(playerImage, colPlayerPos * cellWidth, rowPlayerPos * cellHeight, cellWidth, cellHeight);
            };

            function checkIfWinner() {
                if (colPlayerPos == exitCol && rowPlayerPos == exitRow) {
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


        return this;
    };
})(jQuery);

document.getElementById("mazeCanvas").focus();
var mazeData = "0001000100000001000000000010010100101010100001000000010000101010000001001101000000100000"
var startRow = 0;
var startCol = 1;
var exitRow = 7;
var exitCol = 10;
var playerImage = new Image();
playerImage.src = "Images/dog.jpg"
var exitImage = new Image;
exitImage.src = "Images/exit.png"
$("#mazeCanvas").mazeBoard(mazeData,
    startRow, startCol,
    exitRow, exitCol,
    playerImage,
    exitImage);