(function () {
    'use strict';

    angular
        .module('app')
        .controller('CreateEventController', createEvent);

    createEvent.$inject = ['$http', '$location', 'eventsService', 'accountService'];

    function createEvent($http, $location, eventsService, accountService) {
        var vm = this;

        activate();

        function activate() {
            vm.message = '';
            vm.isUserWithRequiredRoleForFeature = accountService.isUserWithRequiredRoleForFeature;
            vm.event = {};
            vm.event.authorId = accountService.authentication.userId;
        }

        vm.createEvent = function () {
            eventsService.createEvent(vm.event).then(function (response) {
                vm.savedSuccessfully = true;
                vm.message = "Event updated";
                vm.back();
            },
                function (response) {
                    setError("Failed to create Event");
                });
        }

        function setError(errorMsg) {
            vm.savedSuccessfully = false;
            vm.message = errorMsg;
        }

        vm.back = function () {
            window.history.back();
        }
    }
})();
