(function () {
    'use strict';

    angular.module('appPanel', ['ngRoute', 'angularFileUpload'])
        .config(function($routeProvider) {

            $routeProvider.when("/",
            {
                controller: "panelController",
                controllerAs: "vm",
                templateUrl: "/views/panelView.html"
            });

            $routeProvider.when("/files",
            {
                controller: "filesController",
                controllerAs: "vm",
                templateUrl: "/views/filesView.html"
            });
        })
        .directive('ngThumb',
        [
            '$window', function ($window) {
                return {
                    restrict: 'A',
                    template: '<canvas/>',
                    link: function (scope, element, attributes) {

                        var params = scope.$eval(attributes.ngThumb);

                        var canvas = element.find('canvas');
                        var reader = new FileReader();

                        reader.onload = onLoadFile;
                        reader.readAsDataURL(params.file);

                        function onLoadFile(event) {
                            var img = new Image();
                            img.onload = onLoadImage;
                            img.src = event.target.result;
                        }

                        function onLoadImage() {
                            var width = params.width || this.width / this.height * params.height;
                            var height = params.height || this.height / this.width * params.width;
                            canvas.attr({ width: width, height: height });
                            canvas[0].getContext('2d').drawImage(this, 0, 0, width, height);
                        }
                    }
                }
            }
        ]);
})();