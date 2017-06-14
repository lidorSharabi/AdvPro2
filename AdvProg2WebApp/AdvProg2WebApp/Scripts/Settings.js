$("#MazeRows").val(localStorage.getItem("rows"));
$("#MazeCols").val(localStorage.getItem("cols"));
$("#algo").val(localStorage.getItem("algo"));

(function ($) {
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