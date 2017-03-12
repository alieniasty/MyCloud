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
        });

})();