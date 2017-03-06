(function () {
    'use strict';

    angular.module('appPanel', ['ngRoute'])
        .config(function ($routeProvider) {

            $routeProvider.when("/",
            {
                controller: "panelController",
                controllerAs: "vm",
                templateUrl: "/views/panelView.html"
            });
        });
})();