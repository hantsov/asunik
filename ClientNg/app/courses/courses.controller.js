(function () {
	'use strict';

	angular
        .module('app')
        .controller('CoursesController', courses);

	courses.$inject = ['$http', 'coursesService'];

	function courses($http, coursesService) {
		var vm = this;
		activate();

		function activate() {
		    coursesService.getCourses().then(function (response) {
				vm.courses = response;
			});
		}
	}
})();
