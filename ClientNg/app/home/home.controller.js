(function () {
	'use strict';

	angular
        .module('app')
        .controller('HomeController', home);

	home.$inject = ['$http', '$location', 'eventsService'];

	function home($http, $location, eventsService) {
		var vm = this;
		activate();

		function activate() {
			//usersService.getUsers().then(function (response) {
			//	vm.users = response;
			//	vm.filteredUsers = vm.users;
		    //});

		    eventsService.getEvents().then(function(response) {
		        vm.events = response;
		    });
		}

	}
})();
