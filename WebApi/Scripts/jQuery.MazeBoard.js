(function ($) {
    $.fn.drawMazey = function (data) {
        var obj = JSON.parse(data);
        var maze = obj.Maze;
        //var myCanvas = document.getElementById("mazeCanvas");
        var myCanvas = $(this);
        var context = myCanvas.getContext("2d");
        context.clearRect(0, 0, myCanvas.width, myCanvas.height);
        var rows = obj.Rows;
        var cols = obj.Cols;
        var cellWidth = myCanvas.width / cols;
        var cellHeight = myCanvas.height / rows;
        var count = 0;
        for (var i = 0; i < rows; i++) {
            for (var j = 0; j < cols; j++) {
                if (maze[count] === '1') {
                    context.fillRect(cellWidth * j, cellHeight * i, cellWidth, cellHeight);
                }
                count++;
            }
        }
        return this;
    };
}) (jQuery);
