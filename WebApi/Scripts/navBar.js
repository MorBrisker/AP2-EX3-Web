$(document).ready(function () {
    var s = "Hello " + sessionStorage.getItem("user");
    var register = document.getElementById("register");
    var login = document.getElementById("login");

    if (sessionStorage.getItem("user")) {
        register.textContent = s;
        register.href = "#";
        login.textContent = "Log off";
        login.onclick = logOff;
        login.href = "MainPage.html";
    }

});
function logOff() {
    sessionStorage.removeItem("user");
    window.location.replace("MainPage.html");

}