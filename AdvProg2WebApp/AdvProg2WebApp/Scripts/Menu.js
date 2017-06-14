$(function () {
    $("#menu").load("../Menu.html");
});

function addUser() {
        var usersUrl = "api/Users/";
        var user = {
            UserNameId: $("#signup-username").val(),
            Losses: 0,
            Victories: 0
        };
        $.post(usersUrl, user).done(function (data) {
            //self.books.push(data);
        });
}