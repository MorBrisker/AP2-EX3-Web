function submit() {
    var apiUrl = "../api/Users";
    var pass = $("#pass").val();
    var shaObj = new jsSHA("SHA-1", "TEXT");
    shaObj.update(pass);
    var hash = shaObj.getHash("HEX");
    var user = {
        Name:  $("#name").val(),
        Password: hash,
        Email: $("#email").val(),
        Wins: 0,
        Losses: 0
    };
    //var request = apiUrl + "/" + user.Name + "/" + user.Password + "/" + user.Email;

    $.post(apiUrl, user)
        .done(function (result) {
            if (result == "exists") {
                alert("User name already exists");
            }
            else {
                sessionStorage.setItem("user", user.Name);
                window.location.replace("MainPage.html");
            }
        })
        .fail(function (jqXHR, textStatus, err) {
            alert(err);
        });
}