(function () {
    'use strict';

    angular.module('appPanel', ['ngRoute', 'angularFileUpload'])
        .config(function($routeProvider) {

            $routeProvider.when("/",
            {
                controllerAs: "vm",
                templateUrl: "/views/panelView.html"
            });

            $routeProvider.when("/files",
            {
                controllerAs: "vm",
                templateUrl: "/views/filesView.html"
            });
        });

})();