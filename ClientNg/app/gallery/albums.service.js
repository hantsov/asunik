(function () {
    'use strict';

    angular
        .module('app')
        .service('albumsService', albumsService);


    function albumsService($http, apiSettings) {
        var shared = this;

        var serviceAddress = apiSettings.apiServiceBaseUri + "api/albums";

        shared.getAlbums = function () {
            return $http.get(serviceAddress).then(function (response) {
                return response.data;
            });
        };

        shared.getAlbum = function (albumId) {
            return $http.get(serviceAddress + "/" + albumId).then(function (response) {
                return response.data;
            });
        };
    }
})();