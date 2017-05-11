(function () {
    'use strict';

    angular
        .module('app')
        .service('usersService', usersService);


    function usersService($http, localStorageService, apiSettings) {
        var shared = this;

        var serviceAddress = apiSettings.apiServiceBaseUri + "api/users";

        shared.getUsers = function() {
            return $http.get(serviceAddress).then(function (response) {
                return response.data;
            });
        };

        shared.getUser = function (userId) {
            
            return $http.get(serviceAddress + "/" + userId).then(function (response) {
                return response.data;
            });
        };

        shared.createUser = function (user) {
            return $http.post(serviceAddress, user).then(function (response) {
                return response.data;
            });
        }

        shared.updateUser = function (user) {
            return $http.put(serviceAddress + "/" + user.id, user).then(function (response) {
                return response.data;
            });
        }

        shared.getUserCourses = function (userId) {
            return $http.get(serviceAddress + "/" + userId + "/courses").then(function (response) {
                return response.data;
            });
        }

        shared.getUserRoles = function (userId) {
            return $http.get(serviceAddress + "/" + userId + "/roles").then(function (response) {
                return response.data;
            });
        }
    }
})();