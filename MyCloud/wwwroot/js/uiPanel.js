$(document).ready(function() {
    $("#js-navbar-toggle").on("click",
        function () {
            var elm = $("#js-navbar-toggle");

            if (elm.hasClass('collapsed')) {

                elm.removeClass('fa fa-toggle-off');
                elm.addClass('fa fa-toggle-on');

            } else {

                elm.removeClass('fa fa-toggle-on');
                elm.addClass('fa fa-toggle-off');

            }
        });
})