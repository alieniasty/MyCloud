$(document).ready(function() {
    var sidebarAndWrapper = $("#sidebar, #wrapper");

    $("#sidebar-btn").click(function() {
        sidebarAndWrapper.toggleClass('hide-sidebar');
    });

    var close = $('.close');

    close.on('click', function () {
        $("#js-modal").css('display', 'none');
    });
});