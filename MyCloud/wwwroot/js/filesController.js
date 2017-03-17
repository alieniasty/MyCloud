(function () {
    'use strict';

    angular
        .module('appPanel')
        .controller('filesController', filesController)
        .directive('ngDownloadPreview',
        [
            '$window', function ($window) {
                return {
                    restrict: 'A',
                    template: '<canvas/>',
                    link: function (scope, element, attributes) {

                        var params = scope.$eval(attributes.ngDownloadPreview);

                        var canvas = element.find('canvas');

                        var img = new Image();
                        img.src = "data:image/png;base64,".concat(params.base64Code);
                        img.onload = function () {
                            canvas.attr({ width: 100, height: 100 });
                            var ctx = canvas[0].getContext('2d');
                            ctx.drawImage(this, 0, 0, 100, 100);
                        }
                    }
                }
            }
        ]);

    function filesController($http) {

        var base64UserFiles = this;

        base64UserFiles.codes = [];

        $http({

            url: '/api/files/getJsonFiles',
            method: "GET",
            params: { folder: 'folder3' }

        }).then(function (response) {

            angular.forEach(response.data,
                function(key, value) {
                    base64UserFiles.codes.push(key);
                });
        });
    }
})();
