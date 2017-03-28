(function () {
    'use strict';

    angular
        .module('appPanel')
        .controller('filesController', filesController)
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
        ]);

    function filesController($http, $scope) {

        var base64UserFiles = this;

        base64UserFiles.codes = [];
        base64UserFiles.isGettingPreviews = true;

        $http({

            url: '/api/files/getJsonFiles',
            method: "GET",
            params: { folder: 'folder3' }

        }).then(function (response) {

            if (!response.data) {
                base64UserFiles.isGettingPreviews = false;
                return;
            }

            angular.forEach(response.data,
                function (key, value) {
                    base64UserFiles.codes.push(key);
                    base64UserFiles.isGettingPreviews = false;
                });
        });

        $scope.selected = { value: 0 };

        $scope.nextPicture = function (index) {
            if (index >= 0 && index < base64UserFiles.codes.length) {
                $("#js-img-modal")[0].src = "data:image/png;base64,".concat(base64UserFiles.codes[index]);
            } else {
                $scope.selected = { value: 0 };
            }

            
        } 
    }
})();
