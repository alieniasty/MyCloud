(function () {
    'use strict';

    angular
        .module('appPanel')
        .controller('panelController', panelController);

    function panelController($http) {

        var disk = this;

        disk.usedSpace = 0;
        disk.totalSpace = 0;

        $http({

            url: '/api/DiskSpace/UsedSpace',
            method: "GET"

        }).then(function (response) {

            disk.usedSpace = response.data;
        });

        $http({

            url: '/api/DiskSpace/TotalSpace',
            method: "GET"

        }).then(function (response) {

            disk.totalSpace = response.data;
        });
    }

})();