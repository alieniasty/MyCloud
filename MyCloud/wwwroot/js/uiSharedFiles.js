$(document).ready(function () {
    var close = $('.close');

    close.on('click', function () {
        $("#js-modal").css('display', 'none');
    });

    $('.img-snippet').on("click", function () {

        $("#js-modal").css('display', 'block');
        $("#js-img-modal")[0].src = $(this)[0].src;
    });
});