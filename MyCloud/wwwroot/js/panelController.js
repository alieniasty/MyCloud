(function () {
    'use strict';

    angular
        .module('appPanel')
        .controller('panelController', panelController);

    function panelController() {
        var vm = this;
        vm.title = 'panelController';
    }
})();
