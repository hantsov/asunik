(function () {
    'use strict';

    angular
        .module('app')
        .service('eventsService', eventsService);


    function eventsService($http, apiSettings) {
        var shared = this;

        var serviceAddress = apiSettings.apiServiceBaseUri + "api/events";

        shared.getEvents = function (eventType) {
            var requestUrl = serviceAddress + (eventType ? "?type=" + eventType : "");
            return $http.get(requestUrl).then(function (response) {
                return response.data;
            });
        };

        shared.getEvent = function (eventId) {
            return $http.get(serviceAddress + "/" + eventId).then(function (response) {
                return response.data;
            });
        };

        shared.createEvent = function (event) {
            return $http.post(serviceAddress, event).then(function (response) {
                return response.data;
            });
        }

        shared.updateEvent = function (event) {
            return $http.put(serviceAddress + "/" + event.id, event).then(function (response) {
                return response.data;
            });
        }
    }
})();