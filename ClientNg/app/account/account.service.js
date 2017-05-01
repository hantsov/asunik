(function () {
    'use strict';

    angular
        .module('app')
        .service('accountService', accountService);

    function accountService($http, $q, localStorageService, usersService) {
        var shared = this;

        var serviceAddress = "http://localhost:57953/api/account";
        var tokenAddress = "http://localhost:57953/token";

        shared.authentication = {
            isAuth: false,
            username: "",
            userId: "",
            roles: ""
        };

        shared.createAccount = function (registration) {
            return $http.post(serviceAddress + "/register", registration).then(function (response) {
                return response;
            });

        };

        var fillAuthData = function () {
            var authData = localStorageService.get('authorizationData');
            if (authData) {
                shared.authentication.isAuth = true;
                shared.authentication.username = authData.username;
                shared.authentication.userId = authData.userId;
                shared.authentication.token = authData.token;
                shared.authentication.roles = authData.roles;
            }
        };

        // from https://github.com/tjoudeh/AngularJSAuthentication
        shared.signIn = function (signinData) {

            var data = "grant_type=password&username=" + signinData.username + "&password=" + signinData.password;

            var deferred = $q.defer();

            $http.post(tokenAddress, data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).then(function (response) {
                localStorageService.set('authorizationData', { token: response.data.access_token, username: response.data.username, userId: response.data.userId });
                fillAuthData();
                // fill roles
                usersService.getUserRoles(response.data.userId).then(function (response) {
                    var authData = localStorageService.get('authorizationData');
                    authData.roles = response.roles;
                    localStorageService.set('authorizationData', authData);
                    fillAuthData();
                });

                deferred.resolve(response);

            }, function (err, status) {
                shared.signOut();
                deferred.reject(err);
            });

            return deferred.promise;

        };

        shared.signOut = function () {
            localStorageService.remove('authorizationData');
            shared.authentication.isAuth = false;
            shared.authentication.username = "";

        };

        shared.isUserWithRequiredRoleForFeature = function (feature) {
            if (feature === "Users") {
                return isUserWithRequiredRole("Admin");
            }
            else if (feature === "CourseEdit") {
                return isUserWithRequiredRole("Admin");
            }
            return true;
        };

        function isUserWithRequiredRole(requiredRole) {
            if (shared.authentication.roles) {
                for (var i = 0; i < shared.authentication.roles.length; i++) {
                    if (shared.authentication.roles[i].indexOf(requiredRole) >= 0) {
                        return true;
                    }
                }
            }
            return false;
        }
        // init values
        fillAuthData();
    }
})();