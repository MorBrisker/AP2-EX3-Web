
var currPos;
var mazeString;
var mName;
var goalPos;
var initPos;

function generate() {
    var apiUrl = "../api/SingleMaze";
    var mazey = {
        Name: $("#mazeName").val(),
        Rows: $("#mazeRows").val(),
        Cols: $("#mazeCols").val()
    };

    mName = $("#mazeName").val();
    
    $("body").addClass("loading");
    var request = apiUrl + "/" + mazey.Name + "/" + mazey.Rows + "/" + mazey.Cols;

    $.getJSON(request, function (data, status) {
        $("body").removeClass("loading");
        $("#mazeCanvas").show();
        document.title = mName;
        var player = document.getElementById("unicorn");
        var goal = document.getElementById("cloud");
        var maze = $("#mazeCanvas").drawMazey(data, player, goal);
    });
}

function solve() {
    var apiUrl = "../api/SingleMaze";
    var mazey = {
        Name: mName,
        Alg: $("#alg").val()
    };
    var request = apiUrl + "/" + mazey.Name + "/" + mazey.Alg;

    $.getJSON(request, function (data, status) {
        var sol = data.Solution; 
        var canvas = $("#mazeCanvas").solveMazey(sol);
    });
}

function endOfGame() {
    if ((currPos.Row == goalPos.Row) && (currPos.Col == goalPos.Col)) {
        alert("you are the best");
    }
}



