(function () {
    'use strict';

    angular
        .module('app')
        .service('usersService', usersService);


    function usersService($http, localStorageService) {
        var shared = this;

        var serviceAddress = "http://localhost:57953/api/users";

        shared.getUsers = function() {
            var token = localStorageService.get('authorizationData').token;
            return $http.get(serviceAddress).then(function (response) {
                //console.log(response.data);
                return response.data;
            });
        };

        shared.getUser = function (userId) {
            
            return $http.get(serviceAddress + "/" + userId).then(function (response) {
                console.log(response.data);
                return response.data;
            });
        };

        shared.createUser = function (user) {
            return $http.post(serviceAddress, user).then(function (response) {
                console.log(response.data);
                return response.data;
            });
        }

        shared.updateUser = function (user) {
            return $http.put(serviceAddress + "/" + user.id, user).then(function (response) {
                console.log(response.data);
                return response.data;
            });
        }

        shared.getUserCourses = function (userId) {
            return $http.get(serviceAddress + "/" + userId + "/getcourses").then(function (response) {
                console.log(response.data);
                //then get course name etc without getting other users info
                //return listOfCourses;
                return response.data;
            });
        }
    }
})();