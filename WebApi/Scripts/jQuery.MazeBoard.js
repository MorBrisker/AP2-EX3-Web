
var cellWidth, cellHeight;
var rows, cols;

(function ($) {
    $.fn.drawMazey = function (maze, player, goal) {

        mazeString = maze.Maze;
        //var player = start;
        //var goal = end;
        var myCanvas = $(this)[0];
        var context = myCanvas.getContext("2d");
        context.clearRect(0, 0, myCanvas.width, myCanvas.height);
        rows = maze.Rows;
        cols = maze.Cols;
        cellWidth = myCanvas.width / cols;
        cellHeight = myCanvas.height / rows;
        initPos = maze.Start;
        currPos = maze.Start;
        goalPos = maze.End;
        var count = 0;
        for (var i = 0; i < rows; i++) {
            for (var j = 0; j < cols; j++) {
                if (mazeString[count] === '1') {
                    context.fillStyle = "black";
                    context.fillRect(cellWidth * j, cellHeight * i, cellWidth, cellHeight);
                } else {
                    context.fillStyle = "white";
                    context.fillRect(cellWidth * j, cellHeight * i, cellWidth, cellHeight);
                }
                count++;
            }
        }
        context.drawImage(player, initPos.Col * cellWidth, initPos.Row * cellHeight, cellWidth, cellHeight);
        context.drawImage(goal, goalPos.Col * cellWidth, goalPos.Row * cellHeight, cellWidth, cellHeight);
        return this;
    };
})(jQuery);

(function ($) {
    $.fn.solveMazey = function (sol) {
        var player = document.getElementById("unicorn");
        var myCanvas = $(this)[0];
        var context = myCanvas.getContext("2d");
        context.fillStyle = "white";
        context.fillRect(currPos.Col * cellWidth, currPos.Row * cellHeight, cellWidth, cellHeight);
        currPos.Row = initPos.Row;
        currPos.Col = initPos.Col;

        context.drawImage(player, currPos.Col * cellWidth, currPos.Row * cellHeight, cellWidth, cellHeight);



        var i = 0;
        var id = setInterval(frame, 200);
        function frame() {
            switch (sol.charAt(i)) {
                case '0':
                    moveLeft(player, myCanvas, currPos);
                    break;
                case '1':
                    moveRight(player, myCanvas, currPos);
                    break;
                case '2':
                    moveUp(player, myCanvas, currPos);
                    break;
                case '3':
                    moveDown(player, myCanvas, currPos);
                    break;
            }
            i++;
        }
    
    };
})(jQuery);

function moveOneStep(e) {
    var myCanvas = document.getElementById("mazeCanvas");
    var context = myCanvas.getContext("2d");
    var player = document.getElementById("unicorn");
    switch (e.which) {
        case 37:
            moveLeft(player, myCanvas, currPos);
            break;
        case 38:
            moveUp(player, myCanvas, currPos);
            break;
        case 39:
            moveRight(player, myCanvas, currPos);
            break;
        case 40:
            moveDown(player, myCanvas, currPos);
            break;
        default:
            break;
    }

    /*if ((currPos.Row == goalPos.Row) && (currPos.Col == goalPos.Col)) {
        alert("you are the best");
    }*/

}

function moveLeft(player, canvas, currentPos) {
    var context = canvas.getContext("2d");
    if (mazeString[currentPos.Row * cols + (currentPos.Col - 1)] === '0') {
        context.fillStyle = "white";
        context.fillRect(currentPos.Col * cellWidth, currentPos.Row * cellHeight, cellWidth, cellHeight);
        context.drawImage(player, (currentPos.Col - 1) * cellWidth, currentPos.Row * cellHeight, cellWidth, cellHeight);
        currentPos.Col = currentPos.Col - 1;
    }
}

function moveUp(player, canvas, currentPos) {
    var context = canvas.getContext("2d");
    if (mazeString[(currentPos.Row - 1) * cols + currentPos.Col] === '0') {
        context.fillStyle = "white";
        context.fillRect(currentPos.Col * cellWidth, currentPos.Row * cellHeight, cellWidth, cellHeight);
        context.drawImage(player, currentPos.Col * cellWidth, (currentPos.Row - 1) * cellHeight, cellWidth, cellHeight);
        currentPos.Row = currentPos.Row - 1;
    }
}

function moveRight(player, canvas, currentPos) {
    var context = canvas.getContext("2d");
    if (mazeString[currentPos.Row * cols + (currentPos.Col + 1)] === '0') {
        context.fillStyle = "white";
        context.fillRect(currentPos.Col * cellWidth, currentPos.Row * cellHeight, cellWidth, cellHeight);
        context.drawImage(player, (currentPos.Col + 1) * cellWidth, currentPos.Row * cellHeight, cellWidth, cellHeight);
        currentPos.Col = currentPos.Col + 1;
    }
}

function moveDown(player, canvas, currentPos) {
    var context = canvas.getContext("2d");
    if (mazeString[(currentPos.Row + 1) * cols + currentPos.Col] === '0') {
        context.fillStyle = "white";
        context.fillRect(currentPos.Col * cellWidth, currentPos.Row * cellHeight, cellWidth, cellHeight);
        context.drawImage(player, currentPos.Col * cellWidth, (currentPos.Row + 1) * cellHeight, cellWidth, cellHeight);
        currentPos.Row = currentPos.Row + 1;
    }
}

