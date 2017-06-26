function login() {
    var apiUrl = "../api/Users";
    var name = $("#name").val();
    var pass = $("#pass").val();
    var shaObj = new jsSHA("SHA-1", "TEXT");
    shaObj.update(pass);
    var hash = shaObj.getHash("HEX");
    var request = apiUrl + "/" + name;

    $.getJSON(request)
        .done(function (data) {
            if (data.Password == hash) {
                sessionStorage.setItem("user", name);
                window.location.replace("MainPage.html");
            }
            else {
                alert("Wrong password");
            }
        })
        .fail(function (jqXHR, textStatus, err) {
            alert("User not found");
        });
}