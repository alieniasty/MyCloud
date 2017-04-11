(function () {
    var opacity = 0.3;

    $("canvas")
        .css('opacity', 0.3)
        .off('click')
        .on('click',
            function() {
                $(this).toggleClass('selected-canvas-opacity');
            });
})();