(function () {
    'use strict';

    angular
        .module('app')
        .service('coursesService', coursesService);


    function coursesService($http) {
        var shared = this;

        var serviceAddress = "http://localhost:57953/api/courses";

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

        shared.registerToCourse = function (courseMember) {
            return $http.get(serviceAddress).then(function (response) {
                return response.data;
            });
            //return $http.put(getMemberServiceAddress(courseMember.id), courseMember).then(function (response) {
            //    return response.data;
            //});
        }

        function getMemberServiceAddress(courseId) {
            return serviceAddress + "/" + courseId + "/members";
        }
    }
})();