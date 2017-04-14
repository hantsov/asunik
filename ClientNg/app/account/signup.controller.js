(function () {
    'use strict';

    angular
        .module('app')
        .controller('SignupController', signup);

    signup.$inject = ['$location', '$timeout', 'AccountService'];

    function signup($location, $timeout, accountService) {
        var vm = this;

        activate();

        function activate() {
            vm.message = '';
        }

        var startTimer = function () {
            var timer = $timeout(function () {
                $timeout.cancel(timer);
                $location.path('/signin');
            }, 2000);
        }

        // from https://github.com/tjoudeh/AngularJSAuthentication
        vm.signUp = function () {

            accountService.createAccount(vm.registration).then(function (response) {

                vm.savedSuccessfully = true;
                vm.message = "User has been registered successfully, you will be redicted to login page in 2 seconds.";
                startTimer();

            },
             function (response) {
                 vm.savedSuccessfully = false;
                 var errors = [];
                 for (var key in response.data.modelState) {
                     for (var i = 0; i < response.data.modelState[key].length; i++) {
                         errors.push(response.data.modelState[key][i]);
                     }
                 }
                 vm.message = "Failed to register user due to:" + errors.join(' ');
             });
        };

    }
})();
