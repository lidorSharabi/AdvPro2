if (sessionStorage.getItem('userName')) {
    document.getElementById("cd-loggedin-span").innerText = sessionStorage.getItem('userName');
    document.getElementById("cd-loggedin-span").style.visibility = "visible";
    document.getElementById("cd-signup-nav").style.visibility = "hidden";
    document.getElementById("cd-signin-nav").style.visibility = "hidden";
    document.getElementById("cd-loggedin-a").style.visibility = "visible";
} 