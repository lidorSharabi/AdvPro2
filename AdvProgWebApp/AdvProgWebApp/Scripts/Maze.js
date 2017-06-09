(function ($) {
    $.fn.mazeBoard = function () {
        function drawMaze(maze) {
            var rows = $(this).mazeData.length;
            var cols = $(this).mazeData[0].length;
            var cellWidth = $(this).width() / cols;
            var cellHeight = $(this).height() / rows;
            for (var i = 0; i < rows; i++) {
                for (var j = 0; j < cols; j++) {
                    if (maze[i][j] == 1) {
                        context.fillRect(cellWidth * j, cellHeight * i,
                            cellWidth, cellHeight);
                    }
                }
            }
        }
        drawMaze(maze);
        return this;
    };
})(jQuery);
