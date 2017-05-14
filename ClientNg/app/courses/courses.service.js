(function () {
    'use strict';

    angular
        .module('app')
        .service('coursesService', coursesService);


    function coursesService($http, accountService, apiSettings) {
        var shared = this;

        var serviceAddress = apiSettings.apiServiceBaseUri + "api/courses";

        shared.getCourses = function () {
            return $http.get(serviceAddress).then(function (response) {
                return response.data;
            });
        };

        shared.getCourse = function (courseId) {
            return $http.get(serviceAddress + "/" + courseId).then(function (response) {
                return response.data;
            });
        };

        shared.createCourse = function (course) {
            return $http.post(serviceAddress, course).then(function (response) {
                return response.data;
            });
        }

        shared.updateCourse = function (course) {
            return $http.put(serviceAddress + "/" + course.id, course).then(function (response) {
                return response.data;
            });
        }

        shared.removeMember = function (courseId, userId) {
            return $http.delete(serviceAddress + "/" + courseId + "/members/" + userId).then(function (response) {
                return response.data;
            });
        }

        shared.removeCourse = function (courseId) {
            return $http.delete(serviceAddress + "/" + courseId).then(function (response) {
                return response.data;
            });
        }

        shared.registerToCourse = function (courseId) {
            var userData = {
                userId: accountService.authentication.userId,
                memberRole: "STUDENT"
            };
            return $http.post(getMemberServiceAddress(courseId), userData).then(function (response) {
                return response.data;
            });
        }

        function getMemberServiceAddress(courseId) {
            return serviceAddress + "/" + courseId + "/members";
        }
    }
})();