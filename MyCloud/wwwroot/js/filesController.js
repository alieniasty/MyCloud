(function () {
    'use strict';

    angular
        .module('appPanel')
        .controller('filesController',
        [
            '$scope', 'FileUploader', function($scope, FileUploader) {
                var uploader = $scope.uploader = new FileUploader();
            }
        ]);

    function filesController() {
        $scope.uploader = new FileUploader();
    }
})();
