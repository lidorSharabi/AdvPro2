$(function () {
    $("#menu").load("../Menu.html");
});

var userName;

function addUser() {
    var usersUrl = "api/Users/";
    userName = $("#signup-username").val();
    var user = {
        UserNameId: $("#signup-username").val(),
        Losses: 0,
        Victories: 0
    };

    if (userName === "") {
        document.getElementById("signup-userName-required").classList.add('is-visible');
        return;
    }

    $.post(usersUrl, user).done(function (data) {
        document.getElementById("cd-user-modal").classList.remove('is-visible');

    }).fail(function (response) {
        if (response.statusText === "Conflict") {
            alert("ERROR: this user already exist \r\n please try again with different name");
        }
        document.getElementById("signup-username").value = "";
    });
}