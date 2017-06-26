function saveSettings() {
    var Rows = $("#rows").val();
    var Cols = $("#cols").val();
    var Algo = $("#algo").val();

    localStorage.setItem("rows", Rows);
    localStorage.setItem("cols", Cols);
    localStorage.setItem("algo", Algo);
    alert("Settings were saved");   
}