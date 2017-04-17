'use strict';
angular
    .module('app')
    .factory('authInterceptorService', ['$q', '$injector', '$location', 'localStorageService', function ($q, $injector, $location, localStorageService) {

        var authInterceptorService = {

            request: function (config) {

                config.headers = config.headers || {};

                var authData = localStorageService.get('authorizationData');
                if (authData) {
                    config.headers.Authorization = 'Bearer ' + authData.token;
                }

                return config;
            },

            responseError: function (rejection) {
                if (rejection.status === 401) {
                    var authService = $injector.get('accountService');

                    authService.logOut();
                    $location.path('/signin');
                }
                return $q.reject(rejection);
            }

        };

        return authInterceptorService;
    }]);