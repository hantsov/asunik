(function () {
	'use strict';

	angular
        .module('app')
        .controller('CoursesController', courses);

	courses.$inject = ['$http'];

	function courses($http) {
		var vm = this;
		vm.testdata = "testdata is balling out of control";
		activate();

		function activate() {
			//usersService.getUser($routeParams.id).then(function (response) {
			//	vm.user = response;
			//});
		}
	}
})();
