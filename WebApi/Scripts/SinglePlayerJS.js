
function generate() {
    var apiUrl = "../api/SingleMaze";
    var mazey = {
        Name: $("#mazeName").val(),
        Rows: $("#mazeRows").val(),
        Cols: $("#mazeCols").val()
    };
    var request = apiUrl + "/" + mazey.Name + "/" + mazey.Rows + "/" + mazey.Cols;

    $.getJSON(request, function (data, status) {
        $("#mazeCanvas").drawMazey(data);
    });
}