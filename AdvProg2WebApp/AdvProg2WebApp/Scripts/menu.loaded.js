/*
    @description when menu loaded chck if there's user logged in
    if ther's present user name and log out btn ,also hides sign in and register
*/
if (sessionStorage.getItem('userName')) {
    document.getElementById("cd-loggedin-span").innerText = sessionStorage.getItem('userName');
    document.getElementById("cd-loggedin-span").style.visibility = "visible";
    document.getElementById("cd-signup-nav").style.visibility = "hidden";
    document.getElementById("cd-signin-nav").style.visibility = "hidden";
    document.getElementById("cd-loggedin-a").style.visibility = "visible";
} 