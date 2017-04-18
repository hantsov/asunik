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
		}

		var startTimer = function () {
		    var timer = $timeout(function () {
		        $timeout.cancel(timer);
		        $location.path('/signin');
		    }, 2000);
		}

		vm.register = function () {

		    coursesService.registerToCourse("123").then(function (response) {

		        vm.registeredSuccessfully = true;
		        vm.modalMessage = "Registered successfully, you will be redicted to MyCourses page in 2 seconds.";
		        startTimer();

		    },
             function (response) {
                 vm.registeredSuccessfully = false;
                 var errors = [];
                 for (var key in response.data.modelState) {
                     for (var i = 0; i < response.data.modelState[key].length; i++) {
                         errors.push(response.data.modelState[key][i]);
                     }
                 }
                 vm.modalMessage = "Failed to register due to:" + errors.join(' ');
             });
		};
	}
})();
