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
                controller: "foldersController",
                templateUrl: "/views/foldersView.html"
            })
            .state("favorities",
            {
                url: "favorities",
                controller: "favoritiesController",
                templateUrl: "/views/favoritiesView.html"
            })
            .state("files",
            {
                url: "/files/{folder:string}",
                controller: "filesController",
                templateUrl: "/views/filesView.html"
            });
        });

})();