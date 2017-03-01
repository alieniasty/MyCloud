$(document).ready(() => {
    $("#js-eye-switch").click(function() {

        var eyeElement = $("#js-eye");
        console.debug(eyeElement);

        if (eyeElement.hasClass('fa fa-eye')) {
            eyeElement
                .removeClass('fa fa-eye')
                .addClass('fa fa-eye-slash');

            $("#js-input-password").attr('type', 'text');

        } else {
            eyeElement
                .removeClass('fa fa-eye-slash')
                .addClass('fa fa-eye');

            $("#js-input-password").attr('type', 'password');
        }
    });
})