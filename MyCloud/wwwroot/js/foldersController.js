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
        .directive('enterClick', [
            '$state', '$http', function ($state, $http) {
            return {
                restrict: 'A',
                link: function(scope, element, attrs) {
                    element.bind("keypress", function (event) {
                        if (event.which === 13) {

                            var params = scope.$eval(attrs.enterClick);

                            $http({
                                url: '/api/folders/createNewFolder',
                                method: "POST",
                                data: {
                                    "name": params.folderName
                                }
                            }).then(function() {
                                $state.go($state.current, {}, { reload: true });
                            });
                        }
                    });
                }
        }
        }]);

    function foldersController($http) {

        var userFolders = this;

        userFolders.names = [];
        userFolders.isGettingPreviews = true;

        $http({

            url: '/api/folders/getUserFolders',
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