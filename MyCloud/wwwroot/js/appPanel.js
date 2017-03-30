(function () {
    'use strict';

    angular.module('appPanel', ['ui.router', 'angularFileUpload'])
        .config(function ($stateProvider, $urlRouterProvider) {

            $urlRouterProvider.when("", "/");

            $stateProvider
            .state("panel",
            {
                url: "/",
                templateUrl: "/views/panelView.html"
            })
            .state("folders",
            {
                url: "/folders",
                controllers: "foldersController",
                templateUrl: "/views/foldersView.html"
            })
            .state("files",
            {
                url: "/files/{folder:string}",
                controller: "filesController",
                templateUrl: "/views/filesView.html"
            });
        });

})();