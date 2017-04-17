(function () {
    'use strict';

    angular
        .module('app')
        .controller('UsersController', users);

    users.$inject = ['$http', '$scope', '$location', 'usersService'];

    function users($http, $scope, $location, usersService) {
        var vm = this;
        vm.title = 'user';

        activate();

        function activate() {
            usersService.getUsers().then(function (response) {
                vm.users = response;
                vm.filteredUsers = vm.users;
            });
        }

        vm.filterUsers = function () {
            var filter = vm.usersFilter.toLowerCase();
            vm.filteredUsers = [];
            for (var i = 0; i < this.users.length; i++) {
                var user = this.users[i];
                if (user.firstName.toLowerCase().indexOf(filter) >= 0 ||
                    user.lastName.toLowerCase().indexOf(filter) >= 0) {
                    vm.filteredUsers.push(user);
                }
            }
        };

    }
})();
