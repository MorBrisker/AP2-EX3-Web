
function generate() {
    var apiUrl = "../api/SingleMaze";
    var mazey = {
        Name: $("#mazeName").val(),
        Rows: $("#mazeRows").val(),
        Cols: $("#mazeCols").val()
    };
    var request = apiUrl + "/" + mazey.Name + "/" + mazey.Rows + "/" + mazey.Cols;

    $.getJSON(request, function (data, status) {
        var maze = $("#mazeCanvas").drawMazey(data);
    });
}

function solve() {
    var apiUrl = "../api/SingleMaze";
    var mazey = {
        Name: $("#mazeName").val(),
        Alg: $("#alg").val()
    };
    var request = apiUrl + "/" + mazey.Name + "/" + mazey.Alg;

    $.getJSON(request, function (data, status) {
        //$("#mazeCanvas").drawMazey(data);
        var sol = data.Solution; 
        var s = $("#mazeCanvas").solveMazey(sol);
    });
}
