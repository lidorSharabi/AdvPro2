$(function () {
    $("#menu").load("../Menu.html");
});

//*******************************new user section*******************************

function createAccount() {
    var usersUrl = "api/Users/";
    var userName = $("#signup-username").val();
    var email = $("#signup-email").val();
    var password = $("#signup-password").val();
    var confrimPassword = $("#signup-confrim-password").val();

    var user = {
        UserNameId: userName,
        Losses: 0,
        Victories: 0,
        MailAddress: email,
        Password: password
    };

    var validation = false;
    if (userName == "") {
        document.getElementById("signup-userName-required").classList.add('is-visible');
        validation = true;
    }
    if (!validateEmail(email)) {
        document.getElementById("signup-email-required").classList.add('is-visible');
        document.getElementById("signup-email-required").textContent = "Email format should be john@doe.com.";
        validation = true;
    }
    if (email == "") {
        document.getElementById("signup-email-required").classList.add('is-visible');
        document.getElementById("signup-email-required").textContent = "Required field";
        validation = true;
    }
    if (password == "") {
        document.getElementById("signup-password-required").classList.add('is-visible');
        validation = true;
    }
    if (confrimPassword == "") {
        document.getElementById("signup-confrim-password-required").classList.add('is-visible');
        validation = true;
    }
    if (password != confrimPassword) {
        document.getElementById("signup-confrim-password-required").textContent = "Password mismatch";
        validation = true;
    }
    if (validation) {
        return;
    }
    $.post(usersUrl, user).done(function (data) {
        document.getElementById("cd-user-modal").classList.remove('is-visible');
        sessionStorage.setItem('userName', userName);
        //$("#cd-signup-nav").css('visibility', 'hidden');
        document.getElementById("cd-loggedin-span").innerText = sessionStorage.getItem('userName');
        document.getElementById("cd-signup-nav").style.visibility = "hidden";
        document.getElementById("cd-signin-nav").style.visibility = "hidden";
        document.getElementById("cd-loggedin-a").style.visibility = "visible";
        document.getElementById("cd-loggedin-span").style.visibility = "visible";
        window.location.href = "index.html";
    }).fail(function (response) {
        if (response.statusText == "Conflict") {
            document.getElementById("signup-userName-required").textContent = "this User  Name already exist";
        }
        document.getElementById("signup-username").value = "";
    });
}

function registerUsernameChange() {
    document.getElementById("signup-userName-required").classList.remove('is-visible');
}

function emailChange() {
    document.getElementById("signup-email-required").classList.remove('is-visible');
}

function registerPassWordChange() {
    document.getElementById("signup-password-required").classList.remove('is-visible');
}

function confrimPassWordChange() {
    document.getElementById("signup-confrim-password-required").classList.remove('is-visible');
}


//*******************************login section*******************************
function login() {
    var usersUrl = "api/Users/";
    var userName = $("#login-username").val();
    var password = $("#login-password").val();

    var validation = false;
    if (userName == "") {
        document.getElementById("login-userName-required").classList.add('is-visible');
        validation = true;
    }
    if (password == "") {
        document.getElementById("login-password-required").classList.add('is-visible');
        validation = true;
    }
    if (validation) {
        return;
    }

    $.get(usersUrl, { id: userName, password: password }).done(function (data) {
        document.getElementById("cd-user-modal").classList.remove('is-visible');
        sessionStorage.setItem('userName', userName);
        //$("#cd-signup-nav").css('visibility', 'hidden');
        document.getElementById("cd-loggedin-span").innerText = sessionStorage.getItem('userName');
        document.getElementById("cd-signup-nav").style.visibility = "hidden";
        document.getElementById("cd-signin-nav").style.visibility = "hidden";
        document.getElementById("cd-loggedin-a").style.visibility = "visible";
        document.getElementById("cd-loggedin-span").style.visibility = "visible";
        window.location.href = "index.html";
    }).fail(function (response) {
        //TODO - lidor add the reasne for the response
        document.getElementById("login-error-required").textContent = "username or password is incorrect";
        document.getElementById("login-error-required").classList.add('is-visible');
    });




    ///////////////////////////
    //var user = {
    //    UserNameId: 0,
    //    Losses: 0,
    //    Victories: 0,
    //    MailAddress: 0,
    //    Password: 0
    //};
    //$.get(usersUrl, { id: userName}).done(function (data) {
    //    user.UserNameId = data.UserNameId;
    //    user.Losses = data.Losses;
    //    user.Victories = data.Victories;
    //    user.MailAddress = data.MailAddress;
    //    user.Password = data.Password;
    //    $.post(usersUrl + "Update", user ).done(function (data) {
    //        document.getElementById("cd-user-modal").classList.remove('is-visible');
    //    }).fail(function (response) {
    //        //TODO - lidor add the reasne for the response
    //        document.getElementById("login-error-required").classList.add('is-visible');
    //        document.getElementById("login-error-required").textContent = "username or password is incorrect";
    //    });

    //}).fail(function (response) {
    //    alert('Something went worng...');
    //});
    //////////////////////
}

function loginUsernameChange() {
    document.getElementById("login-userName-required").classList.remove('is-visible');
    loginErrorChange();
}

function loginPassWordChange() {
    document.getElementById("login-password-required").classList.remove('is-visible');
    loginErrorChange();
}

function loginErrorChange() {
    document.getElementById("login-error-required").classList.remove('is-visible');
}
//*******************************general section*******************************

function validateEmail(email) {
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
}

function MultiPlayerOnClick() {
    if (sessionStorage.getItem('userName')) {
        window.location.href = "MultiGame.html";
    }
    else {
        alert("Please Sign in or Register")
    }
}

function logOutOnClick() {
    sessionStorage.removeItem('userName');
}
//function rankingOnClick() {
//    var usersUrl = "api/Users/";
//    $.get(usersUrl, {}).done(function (data) {
//        length = data.length;
//        var users = [[]];
//        var str;
//        for (index = 0; index < length; ++index) {
//            //str = $('<tr/>');
//            //str.append("<td>" + (data[index].Victories - data[index].Losses) + "</td>");
//            //str.append("<td>" + data[index].UserNameId + "</td>");
//            //str.append("<td>" + data[index].Victories + "</td>");
//            //str.append("<td>" + data[index].Losses + "</td>");
//            $('table').append(str);
//            var name = data[index].UserNameId;
//            var wins = data[index].Victories;
//            var losses = data[index].Losses;
//            var rank = data[index].Victories - data[index].Losses;
//            users[index] = [rank, name, wins, losses];
//        }
//        localStorage.setItem('usersArray', users);
//        window.location.href = "Rankings.html";
//    }).fail(function (response) {
//        alert('Something went worng with server...');
//    });
//}
