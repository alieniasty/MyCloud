$(document).ready(function() {
    var sidebarAndWrapper = $("#sidebar, #wrapper");

    $("#sidebar-btn").click(function() {
        sidebarAndWrapper.toggleClass('hide-sidebar');
    });

    var modal = $("#js-modal");
    var close = $('.close');

    close.on('click', function () {
        $("#js-modal").css('display', 'none');
    });
});