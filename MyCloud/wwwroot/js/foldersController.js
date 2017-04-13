(function() {
    'use strict';

    angular
        .module('appPanel')
        .controller('foldersController', foldersController)
        .directive('newFolder', function() {
            return {
                restrict: 'E',
                templateUrl: "/views/newFolder.html"
            }
        })
        .directive('enterClick', function () {
            return {
                restrict: 'A',
                link: function(scope, element, attrs) {
                    element.bind("keypress", function (event) {
                        if (event.which === 13) {
                            alert("Nowy Folder!");
                        }
                    });
                }
        }
        });

    function foldersController($http, $scope) {

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