(function () {
    'use strict';

    angular
        .module('app')
        .controller('EditCourseController', editcourse);

    editcourse.$inject = ['$http', '$location', '$routeParams', 'coursesService', 'accountService'];

    function editcourse($http, $location, $routeParams, coursesService, accountService) {
        var vm = this;
        var nonModdedCourse = {};
        activate();

        function activate() {
            vm.message = '';
            refreshCourseData();
            vm.isUserWithRequiredRoleForFeature = accountService.isUserWithRequiredRoleForFeature;
        }

        vm.updateCourse = function () {
            if (vm.course.id) {
                coursesService.updateCourse(vm.course).then(function (response) {
                    vm.savedSuccessfully = true;
                    vm.message = "Course updated";
                },
                function (response) {
                    setError("Failed to update course");
                });
            } else {
                setError("No course id given for update");
            }
        };

        vm.removeMember = function (userId) {
            if (vm.course.id && userId) {
                coursesService.removeMember(vm.course.id, userId).then(function (response) {
                    vm.savedSuccessfully = true;
                    vm.message = "Member removed";
                    refreshCourseData();
                },
                function (response) {
                    setError("Failed to remove member");
                });
            } else {
                setError("No user id given for update");
            }
        };

        function refreshCourseData() {
            coursesService.getCourse($routeParams.id).then(function (response) {
                vm.course = response;
                nonModdedCourse = angular.copy(response);
            });
        }

        function setError(errorMsg) {
            vm.course = nonModdedCourse;
            vm.savedSuccessfully = false;
            vm.message = errorMsg;
        }

        vm.back = function () {
            window.history.back();
        }
    }
})();
