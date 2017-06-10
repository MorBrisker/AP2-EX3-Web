(function ($) {
    $.fn.drawMaze = function () {
        this.each(function () {
            maze = [[0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0],
            [0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 0],
            [0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0]];
            //var myCanvas = document.getElementById("mazeCanvas");
            var myCanvas = $(this)[0];
            var context = myCanvas.getContext("2d");
            var rows = maze.length;
            var cols = maze[0].length;
            var cellWidth = myCanvas.width / cols;
            var cellHeight = myCanvas.height / rows;
            for (var i = 0; i < rows; i++) {
                for (var j = 0; j < cols; j++) {
                    if (maze[i][j] == 1) {
                        context.fillRect(cellWidth * j, cellHeight * i, cellWidth, cellHeight);
                    }
                }
            }
        });
        return this;
    };
})(jQuery);
