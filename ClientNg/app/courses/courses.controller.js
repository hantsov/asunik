(function () {
	'use strict';

	angular
        .module('app')
        .controller('CoursesController', courses);

	courses.$inject = ['$http', '$timeout', 'coursesService'];

	function courses($http, $timeout, coursesService) {
	    var vm = this;

		activate();

		function activate() {
		    coursesService.getCourses().then(function (response) {
				vm.courses = response;
		    });
		    vm.modalMessage = '';
		    vm.selectedCourseId = '';
		}

		var startTimer = function () {
		    var timer = $timeout(function () {
		        $timeout.cancel(timer);
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
