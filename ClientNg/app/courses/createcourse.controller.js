(function () {
    'use strict';

    angular
        .module('app')
        .controller('CreateCourseController', createCourse);

    createCourse.$inject = ['$http', '$location', 'coursesService', 'accountService'];

    function createCourse($http, $location, coursesService, accountService) {
        var vm = this;

        activate();

        function activate() {
            vm.message = '';
            vm.isUserWithRequiredRoleForFeature = accountService.isUserWithRequiredRoleForFeature;
        }

        vm.createCourse = function () {
            coursesService.createCourse(vm.course).then(function (response) {
                vm.savedSuccessfully = true;
                vm.message = "Course updated";
                vm.back();
            },
                function (response) {
                    setError("Failed to create course");
                });
        }

        function setError(errorMsg) {
            vm.savedSuccessfully = false;
            vm.message = errorMsg;
        }

        vm.back = function () {
            window.history.back();
        }
    }
})();
