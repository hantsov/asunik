(function () {
    'use strict';

    angular
        .module('app')
        .controller('SigninController', signin);

    signin.$inject = ['$location', 'AccountService']; 

    function signin($location, accountService) {
        var vm = this;

        activate();

        function activate() {
            vm.message = '';
        }

        vm.signIn = function () {

            accountService.signIn(vm.signinData).then(function (response) {
                $location.path('/home');

            },
             function (error) {
                 vm.message = error.data.error_description;
             });
        };
    }
})();
