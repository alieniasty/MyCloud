$(document).ready(function () {
    $("#js-newfolder").on('click',
        function () {
            if ($(this).hasClass('fa-plus-circle')) {
                $(this).removeClass('fa-plus-circle');
                $(this).addClass('fa-minus-circle');
            } else {
                $(this).removeClass('fa-minus-circle');
                $(this).addClass('fa-plus-circle');
            }
        });
})