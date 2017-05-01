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
            vm.isUserWithRequiredRoleForFeature = accountService.isUserWithRequiredRoleForFeature;
        }

        vm.signOut = function () {
            accountService.signOut();
                $location.path('/home');
        };
    }
})();