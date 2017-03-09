$(document).ready(function() {
    var sidebarAndWrapper = $("#sidebar, #wrapper");

    $("#sidebar-btn").click(function() {
        sidebarAndWrapper.toggleClass('hide-sidebar');
    });
});