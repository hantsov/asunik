(function () {
    'use strict';

    angular
        .module('app')
        .controller('EditCourseController', editcourse);

    editcourse.$inject = ['$http', '$scope', '$location', '$routeParams', 'usersService'];

    function editcourse($http, $scope, $location, $routeParams, usersService) {
        var vm = this;
        var nonModdedUser = {};
        activate();

        function activate() {
            vm.message = '';

            usersService.getUser($routeParams.id).then(function (response) {
                vm.user = response;
                nonModdedUser = angular.copy(response);
            });

            usersService.getUserCourses($routeParams.id).then(function (response) {
                vm.userCourses = response;
            });
        }

        vm.updateUser = function () {
            // javascript "truthy" values
            if (vm.user.id) {
                usersService.updateUser(vm.user).then(function (response) {
                    vm.savedSuccessfully = true;
                    vm.message = "User updated";
                },
                function (response) {
                    setError("Failed to update user");
                });
            } else {
                setError("No user id given for update");
            }
        };

        vm.back = function() {
            window.history.back();
        }

        function setError(errorMsg) {
            vm.user = nonModdedUser;
            vm.savedSuccessfully = false;
            vm.message = errorMsg;
        }
    }
})();
