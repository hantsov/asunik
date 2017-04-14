(function () {
    'use strict';

    angular
        .module('app')
        .controller('UsersController', users);

    users.$inject = ['$http', '$scope', '$location', 'UsersService'];

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

        vm.filterUsers = function (filter) {
            filter = filter.toLowerCase();
            vm.filteredUsers = [];
            for (var i = 0; i < this.users.length; i++) {
                var user = this.users[i];
                if (user.firstname.toLowerCase().indexOf(filter) >= 0 ||
                    user.lastname.toLowerCase().indexOf(filter) >= 0) {
                    vm.filteredUsers.push(user);
                }
            }
        };

    }
})();
