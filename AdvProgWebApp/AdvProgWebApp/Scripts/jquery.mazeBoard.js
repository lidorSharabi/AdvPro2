
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

            drawMaze();
            addKeyboardListener();
            function drawMaze() {
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
            };

            function moveSelection(e) {
                switch (e.keyCode) {
                    case 37:
                        if ((colPlayerPos - 1) >= 0 && mazeData[indexInMaze - 1] == '0') {
                            clearPlayer();
                            colPlayerPos -= 1;
                            indexInMaze -= 1;
                            drawPlayer();
                        }
                        checkIfWon();
                        break;
                    case 39:
                        if ((colPlayerPos + 1) < cols && mazeData[indexInMaze + 1] == '0') {
                            clearPlayer();
                            colPlayerPos += 1;
                            indexInMaze += 1;
                            drawPlayer();
                        }
                        checkIfWon();
                        break;
                    case 38:
                        if ((rowPlayerPos - 1) >= 0 && mazeData[indexInMaze - cols] == '0') {
                            clearPlayer();
                            rowPlayerPos -= 1;
                            indexInMaze -= cols;
                            drawPlayer();
                        }
                        checkIfWon();
                        break;
                    case 40:
                        if ((rowPlayerPos + 1) < rows && mazeData[indexInMaze + cols] == '0') {
                            clearPlayer();
                            rowPlayerPos += 1;
                            indexInMaze += cols;
                            drawPlayer();
                        }
                        checkIfWon();
                        break;
                }
            };

            function clearPlayer() {
                context.clearRect(colPlayerPos * cellWidth, rowPlayerPos * cellHeight, cellWidth, cellHeight);
            };

            function drawPlayer() {
                context.drawImage(playerImage, colPlayerPos * cellWidth, rowPlayerPos * cellHeight, cellWidth, cellHeight);
            };

            function checkIfWon() {
                if (colPlayerPos == exitCol && rowPlayerPos == exitRow) {
                    document.getElementById("div2").style.backgroundImage = "url('Images/Image.png')"
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
