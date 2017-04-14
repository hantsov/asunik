(function () {
    'use strict';

    angular
        .module('app')
        .controller('MainController', main);

    main.$inject = ['$http', '$scope', '$location'];

    function main($http, $scope, $location) {

        var vm = this;

        vm.message = "lol123";
        var onError = function (reason) {
            vm.error = "Something went wrong...";
        }
        console.log("here");
    }

})();