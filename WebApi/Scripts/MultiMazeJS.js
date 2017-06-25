var currPos;
var currentPosOpp;
var gName;
// Declare a proxy to reference the hub
var game = $.connection.multiMazeHub;

$("document").ready(function () {
    var script = document.createElement('script');
    script.src = '../Scripts/jQuery.MazeBoard.js';
    document.getElementsByTagName('head')[0].appendChild(script);
});

game.client.drawMaze = function (maze) {
    $("body").removeClass("loading");
    $("#mazeCanvas").show();
    $("#mazeCanvasOpp").show();

    document.title = gName;

    var player1 = document.getElementById("unicorn");
    var goal1 = document.getElementById("cloud");
    var maze1 = $("#mazeCanvas").drawMazey(maze, player1, goal1);

    var player2 = document.getElementById("unicornOpp");
    var goal2 = document.getElementById("cloudOpp");
    var maze2 = $("#mazeCanvasOpp").drawMazey(maze, player2, goal2);

    currentPosOpp = jQuery.extend(true, {}, currPos)
}
game.client.getListOfGames = function (games) {
    $("#listOfGames option").remove();
    for (var i = 0; i < games.length; i++) {
        $('#listOfGames').append('<option value"' + games[i] + '">' + games[i] + '</option>');
    }
};

game.client.moveOpp = function (dir) {
    var oppCanvas = document.getElementById("mazeCanvasOpp");
    var player = document.getElementById("unicornOpp");

    switch (dir) {
        case 'left':
            moveLeft(player, oppCanvas, currentPosOpp);
            break;
        case 'up':
            moveUp(player, oppCanvas, currentPosOpp);
            break;
        case 'right':
            moveRight(player, oppCanvas, currentPosOpp);
            break;
        case 'down':
            moveDown(player, oppCanvas, currentPosOpp);
            break;
        default:
            break;
    }
}

$.connection.hub.start().done(function () {
    $('#start').click(function () {
        $("body").addClass("loading");

        var name = $('#player').val();
        gName = $('#mazeName').val();
        var rows = $('#mazeRows').val();
        var cols = $('#mazeCols').val();
        game.server.startGame(gName, rows, cols, name);
    });

    $('#join').click(function () {
        $("body").addClass("loading");

        var name = $('#player').val();
        gName = $("#listOfGames option:selected").text()
        game.server.joinGame(gName, name);
    });

    $('#listOfGames').click(function () {
        game.server.getList();
    });

    $('#bodyMaze').keydown(function (e) {
        var name = $('#player').val();
        var dir = null;
        switch (e.which) {
            case 37:
                dir = "left";
                break;
            case 38:
                dir = "up";
                break;
            case 39:
                dir = "right";
                break;
            case 40:
                dir = "down";
                break;
            default:
                return;
        }
        moveOneStep(e);
        
        game.server.playMove(dir, name);
    });

});