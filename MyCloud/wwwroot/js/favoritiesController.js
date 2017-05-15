(function () {
    'use strict';

    angular
        .module('appPanel')
        .controller('favoritiesController', favoritiesController)
        .directive('ngDownloadPreview',
        [
            '$window', '$timeout', function ($window, $timeout) {
                return {
                    restrict: 'A',
                    template: '<canvas/>',
                    link: function (scope, element, attributes) {

                        var params = scope.$eval(attributes.ngDownloadPreview);

                        var canvas = element.find('canvas');

                        var img = new Image();
                        img.src = "data:image/png;base64,".concat(params.base64Code);

                        img.onload = function () {
                            canvas.attr({ width: 150, height: 100 });
                            var ctx = canvas[0].getContext('2d');
                            ctx.drawImage(this, 0, 0, 150, 100);
                        }

                        $timeout(function () {
                            canvas.on("click", function () {
                                $("#js-modal").css('display', 'block');
                                $("#js-img-modal")[0].src = img.src;
                            });
                        });
                    }
                }
            }
        ])
        .directive('selectPreview',
        [

            '$timeout', function ($timeout) {
            return {
                restrict: 'E',
                templateUrl: "/views/selectPreview.html",
                link: function (scope, element, attributes) {

                    $timeout(function () {

                        $(element).on('click',
                            function () {
                                $(this).children().toggleClass("circle-base-animation-checked");
                            });
                    });
                }
            }
        }]);

    function favoritiesController($http, $scope, $stateParams, $state) {
        
    }
})();