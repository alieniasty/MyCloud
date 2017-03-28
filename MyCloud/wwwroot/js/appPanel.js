(function () {
    'use strict';

    angular.module('appPanel', ['ui.router', 'angularFileUpload'])
        .config(function ($stateProvider, $urlRouterProvider) {

            $urlRouterProvider.when("", "/");

            $stateProvider.state("/",
            {
                url: "/",
                templateUrl: "/views/panelView.html"
            });

            $stateProvider.state("files",
            {
                url: "files",
                controller: "filesController",
                templateUrl: "/views/filesView.html"
            });
        });

})();