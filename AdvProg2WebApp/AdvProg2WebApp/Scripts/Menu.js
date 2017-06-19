/*
    @description load menu.html
*/
$(function () {
    $("#menu").load("../Menu.html");
});

//*******************************new user section*******************************

/*
    @description create new account - add him to DB, and log in new user
 */
function createAccount() {
    var usersUrl = "api/Users/";
    var userName = $("#signup-username").val();
    var email = $("#signup-email").val();
    var password = $("#signup-password").val();
    var confrimPassword = $("#signup-confrim-password").val();

    //create default user
    var user = {
        UserNameId: userName,
        Losses: 0,
        Victories: 0,
        MailAddress: email,
        Password: password
    };

    /*
    *check that all the necessary fields aren't empty
    *show validation for each empty field
    */
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
    /*
    *add user to DB
    */
    $.post(usersUrl, user).done(function (data) {
        document.getElementById("cd-user-modal").classList.remove('is-visible');
        sessionStorage.setItem('userName', userName);
        document.getElementById("cd-loggedin-span").innerText = sessionStorage.getItem('userName');
        document.getElementById("cd-signup-nav").style.visibility = "hidden";
        document.getElementById("cd-signin-nav").style.visibility = "hidden";
        document.getElementById("cd-loggedin-a").style.visibility = "visible";
        document.getElementById("cd-loggedin-span").style.visibility = "visible";
        window.location.href = "index.html";
    }).fail(function (response) {
        //if the request failed check the reason and show error according the reason
        if (response.statusText == "Conflict") {
            document.getElementById("signup-userName-required").textContent = "this User Name already exist";
        }
        document.getElementById("signup-username").value = "";
    });
}

/*
    *@description: remove validation if the field value has change
 */
function registerUsernameChange() {
    document.getElementById("signup-userName-required").classList.remove('is-visible');
}

/*
    *@description: remove validation if the field value has change
 */
function emailChange() {
    document.getElementById("signup-email-required").classList.remove('is-visible');
}

/*
    *@description: remove validation if the field value has change
 */
function registerPassWordChange() {
    document.getElementById("signup-password-required").classList.remove('is-visible');
}

/*
    *@description: remove validation if the field value has change
 */
function confrimPassWordChange() {
    document.getElementById("signup-confrim-password-required").classList.remove('is-visible');
}


//*******************************login section*******************************

/*
    *@description: confrim user name and password by calling server to check in the DB
    *if the user exist - log in, otherwise shoe validation error
 */
function login() {
    var usersUrl = "api/Users/";
    var userName = $("#login-username").val();
    var password = $("#login-password").val();

    /*
    *check that all the necessary fields aren't empty
    *show validation for each empty field
    */
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
        document.getElementById("cd-loggedin-span").innerText = sessionStorage.getItem('userName');
        document.getElementById("cd-signup-nav").style.visibility = "hidden";
        document.getElementById("cd-signin-nav").style.visibility = "hidden";
        document.getElementById("cd-loggedin-a").style.visibility = "visible";
        document.getElementById("cd-loggedin-span").style.visibility = "visible";
        window.location.href = "index.html";
    }).fail(function (response) {
        //if the request failed check the reason and show error according the reason
        document.getElementById("login-error-required").textContent = "username or password is incorrect";
        document.getElementById("login-error-required").classList.add('is-visible');
    });
}

/*
    *@description: remove validation if the field value has change
 */
function loginUsernameChange() {
    document.getElementById("login-userName-required").classList.remove('is-visible');
    loginErrorChange();
}

/*
    *@description: remove validation if the field value has change
 */
function loginPassWordChange() {
    document.getElementById("login-password-required").classList.remove('is-visible');
    loginErrorChange();
}

/*
    *@description: remove validation if the field value has change
 */
function loginErrorChange() {
    document.getElementById("login-error-required").classList.remove('is-visible');
}


//*******************************general section*******************************

/*
    *@description: check if the email is in mail address format
    *@param email string
 */
function validateEmail(email) {
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
}

/*
    *@description: MultiPlayerOnClick check if ther's any user log in
    *if the user ism't log in show error alert
*/
function MultiPlayerOnClick() {
    if (sessionStorage.getItem('userName')) {
        window.location.href = "MultiGame.html";
    }
    else {
        alert("Please Sign in or Register")
    }
}

/*
    *@description: log out the user by deleting the user from the session Storage
 */
function logOutOnClick() {
    sessionStorage.removeItem('userName');
}
