(function () {
	'use strict';

	angular
        .module('app')
        .controller('CoursesController', courses);

	courses.$inject = ['$http', '$location', '$timeout', 'coursesService', 'accountService'];

	function courses($http, $location, $timeout, coursesService, accountService) {
	    var vm = this;

		activate();

		function activate() {
		    coursesService.getCourses().then(function (response) {
				vm.courses = response;
		    });
		    vm.authentication = accountService.authentication;
		    vm.modalMessage = '';
		    vm.selectedCourseId = '';
		    vm.isUserWithRequiredRoleForFeature = accountService.isUserWithRequiredRoleForFeature;
		}

		var startTimer = function () {
		    var timer = $timeout(function () {
		        $timeout.cancel(timer);
		        angular.element('.modal-backdrop').remove();
		        $location.path('/profile');
		    }, 2000);
		}

		vm.setSelectedCourse = function (courseId) {
		    vm.modalMessage = '';
	        vm.selectedCourseId = courseId;
	    };

		vm.register = function () {
		    coursesService.registerToCourse(vm.selectedCourseId).then(function (response) {
		        vm.registeredSuccessfully = true;
		        vm.modalMessage = "Registered successfully, you will be redicted to profile page in 2 seconds.";
		        startTimer();

		    },
             function (response) {
                 vm.registeredSuccessfully = false;
                 vm.modalMessage = "Failed to register due to:" + response.data.errors.join(' ');
             });
		};
	}
})();
