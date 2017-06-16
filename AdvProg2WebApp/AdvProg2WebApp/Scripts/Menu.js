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

    $.get(usersUrl, { id: userName, password: password}).done(function (data) {
        document.getElementById("cd-user-modal").classList.remove('is-visible');
        sessionStorage.setItem('userName', userName);
    }).fail(function (response) {
        document.getElementById("login-error-required").classList.add('is-visible');
        document.getElementById("login-error-required").textContent = "username or password is incorrect";
        });
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