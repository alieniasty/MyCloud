(function() {
    'use strict';

    angular
        .module('appPanel')
        .controller('foldersController', foldersController);

    function foldersController($http) {

        var userFolders = this;

        userFolders.names = [];
        userFolders.isGettingPreviews = true;

        $http({

            url: '/api/files/getUserFolders',
            method: "GET"

        }).then(function(response) {
            if (!response.data) {
                userFolders.isGettingPreviews = false;
                return;
            }

            angular.forEach(response.data,
                function (key, value) {
                    userFolders.names.push(key);
                    userFolders.isGettingPreviews = false;
                });
        });
    }

})();