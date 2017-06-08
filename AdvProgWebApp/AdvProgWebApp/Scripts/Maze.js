var myMazeBoard = $("#mazeCanvas").mazeBoard(
    mazeData = [[0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0],
    [0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 0],
    [0, 0, 0, 0, 1, 0, 0, 1, 0, 1, 0],
    [0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 0],
    [0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0], // the matrix containing the maze cells
    startRow = mazeData.length, startCol = mazeData[0].length, // initial position of the player
    exitRow = 0, exitCol = 1, // the exit position
    playerImage = new Image(), // player's icon (of type Image)
    exitImage = new Image, // exit's icon (of type Image)
        true, // is the board enabled (i.e., player can move)
        playerImage.src("Images/dog.jpg")
    function (direction, playerRow, playerCol) {
        // a callback function which is invoked after each move
    }
}