(function () {
	'use strict';

	angular
        .module('app')
        .controller('HomeController', home);

	home.$inject = ['$http', '$location', 'eventsService', 'accountService'];

	function home($http, $location, eventsService, accountService) {
		var vm = this;
		activate();

		function activate() {
		    eventsService.getEvents("NEWS").then(function(response) {
		        vm.events = response;
		    });
		    vm.isUserWithRequiredRoleForFeature = accountService.isUserWithRequiredRoleForFeature;
		}

	}
})();
