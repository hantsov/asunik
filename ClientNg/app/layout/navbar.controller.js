(function () {
    'use strict';

    angular
        .module('app')
        .controller('NavbarController', navbar);

    navbar.$inject = ['$location', 'accountService'];

    function navbar($location, accountService) {
        var vm = this;

        activate();

        function activate() {
            vm.authentication = accountService.authentication;
        }

        vm.signOut = function () {
            accountService.signOut().then(function (response) {
                $location.path('/home');

            });
        };
    }
})();