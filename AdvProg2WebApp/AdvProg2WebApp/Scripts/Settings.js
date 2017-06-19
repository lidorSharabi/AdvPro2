//save the default settings in the local storage
$("#MazeRows").val(localStorage.getItem("rows"));
$("#MazeCols").val(localStorage.getItem("cols"));
$("#algo").val(localStorage.getItem("algo"));

(function ($) {
    /*
    @description click on SaveSettingsBtn save all the default settings parameters
    (rows, cols, solve algorithm) in the local storage
    and navigate to the start page - index.html
    */
    $("#SaveSettingsBtn").click(function (e) {
        var rows = $("#MazeRows").val();
        var cols = $("#MazeCols").val();
        var algo = $("#algo").val();
        localStorage.setItem("rows", rows);
        localStorage.setItem("cols", cols);
        localStorage.setItem("algo", algo);
        alert("Settings saved");
        window.open("index.html", '_self');
    });
})(jQuery);