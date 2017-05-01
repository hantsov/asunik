(function () {
    'use strict';

    angular
        .module('app')
        .controller('ProfileController', profile);

    profile.$inject = ['$location', 'usersService', 'accountService'];

    function profile($location, usersService, accountService) {
        var vm = this;

        vm.user = {};
        vm.courses = {};

        activate();

        function activate() {
            usersService.getUser(accountService.authentication.userId).then(function (response) {
                vm.user = response;
            });
            usersService.getUserCourses(accountService.authentication.userId).then(function (response) {
                vm.courses = response;
            });
        }

    }
})();
