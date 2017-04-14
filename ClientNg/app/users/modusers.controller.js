(function () {
    'use strict';

    angular
        .module('app')
        .controller('ModUsersController', modusers);

    modusers.$inject = ['$http', '$scope', '$location', '$routeParams', 'UsersService'];

    function modusers($http, $scope, $location, $routeParams, usersService) {
        var vm = this;

        activate();

        function activate() {
            usersService.getUser($routeParams.id).then(function (response) {
                vm.user = response;
            });
        }

        vm.updateUser = function (user) {
            // javascript "truthy" values
            if (vm.user.id) {
                usersService.updateUser(user).then(function (response) {
                    console.log("user service update");
                    console.log(response);
                });
            } else {
                return "error";
            }
        };
    }
})();
