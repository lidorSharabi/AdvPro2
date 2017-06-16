
(function ($) {
    $.fn.mazeList = function (games) {
        var x = document.getElementById("games");
        var xList = x.options;
        var i = 0;
        if (xList.length) {
            for (i = 0; i < xList.length; i++) {
                xList[i]=null;
            }
        }
        
        for (i = 0; i < games.length; i++) {
            var option = document.createElement("option");
            option.text = games[i];
            x.add(option);
        }
        return this;
    };

    $.fn.mazeBoard = function (name, rows, cols, mazeData,
        startRow, startCol,
        exitRow, exitCol,
        playerImage,
        exitImage) {
        document.title = name;
        var myCanvas = this[0];
        var context = myCanvas.getContext("2d");
        var cellWidth = myCanvas.width / cols;
        var cellHeight = myCanvas.height / rows;
        var colPlayerPos = startCol;
        var rowPlayerPos = startRow;
        var counter = 0;
        var indexInMaze = 0;

        clearCanvas();
        drawMaze();
        addKeyboardListener();

        function clearCanvas() {
            context.clearRect(0, 0, myCanvas.width, myCanvas.height);
        };

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
                context.clearRect(0, 0, myCanvas.width, myCanvas.height);
                var endAnimation_image = new Image();
                endAnimation_image.src = "Images/Image.png";
                endAnimation_image.onload = function () {
                    context.drawImage(endAnimation_image, 100, 150, myCanvas.width - 200, myCanvas.height - 200);
                }
                context.font = "32px Arial"
                context.fillText("You Won!", 75, 100);
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

    $.fn.opponentMazeBoard = function (name, rows, cols, mazeData,
        startRow, startCol,
        exitRow, exitCol,
        oplayerImage,
        oexitImage) {
        var myCanvas = this[0];
        var context = myCanvas.getContext("2d");
        var cellWidth = myCanvas.width / cols;
        var cellHeight = myCanvas.height / rows;
        var colPlayerPos = startCol;
        var rowPlayerPos = startRow;
        var counter = 0;

        clearCanvas();
        drawMaze();

        function clearCanvas() {
            context.clearRect(0, 0, myCanvas.width, myCanvas.height);
        };

        function drawMaze() {
            for (var i = 0; i < rows; i++) {
                for (var j = 0; j < cols; j++) {
                    if (mazeData[counter] == '1') {
                        context.fillRect(cellWidth * j, cellHeight * i, cellWidth, cellHeight);
                    }
                    if (i == startRow && j == startCol) {

                        indexInMaze = counter;
                    }
                    if (i == exitRow && j == exitCol) {

                    }
                    counter++;
                }
            }
        };
        oplayerImage.onload = function () {
            context.drawImage(oplayerImage, cellWidth * startCol, cellHeight * startRow, cellWidth, cellHeight);
        };
        oexitImage.onload = function () {
            context.drawImage(oexitImage, cellWidth * exitCol, cellHeight * exitRow, cellWidth, cellHeight);
        };
        return this;
    };

})(jQuery);