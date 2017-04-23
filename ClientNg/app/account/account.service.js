﻿(function () {
    'use strict';

    angular
        .module('app')
        .service('accountService', accountService);

    function accountService($http, $q, localStorageService) {
        var shared = this;

        var serviceAddress = "http://localhost:57953/api/account";
        var tokenAddress = "http://localhost:57953/token";

        shared.authentication = {
            isAuth: false,
            username: "",
            userId: ""
        };

        shared.createAccount = function (registration) {
            return $http.post(serviceAddress + "/register", registration).then(function (response) {
                return response;
            });

        };


        // from https://github.com/tjoudeh/AngularJSAuthentication
        shared.signIn = function (signinData) {

            var data = "grant_type=password&username=" + signinData.username + "&password=" + signinData.password;

            var deferred = $q.defer();

            $http.post(tokenAddress, data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).then(function (response) {

                localStorageService.set('authorizationData', { token: response.data.access_token, username: response.data.username, userId: response.data.userId });

                shared.authentication.isAuth = true;
                shared.authentication.username = signinData.username;
                shared.authentication.userId = signinData.id;

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

        var fillAuthData = function () {

            var authData = localStorageService.get('authorizationData');
            if (authData) {
                shared.authentication.isAuth = true;
                shared.authentication.username = authData.username;
                shared.authentication.userId = authData.userId;
                shared.authentication.token = authData.token;   
            }

        };

        // init values
        fillAuthData();

    }
})();