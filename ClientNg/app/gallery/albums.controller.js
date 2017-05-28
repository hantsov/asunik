(function () {
    'use strict';

    angular
        .module('app')
        .controller('AlbumsController', albums);

    albums.$inject = ['$location', '$routeParams', 'albumsService'];

    function albums($location, $routeParams, albumsService) {
        var vm = this;
        activate();

        function activate() {
            albumsService.getAlbum($routeParams.id).then(function (response) {
                vm.album = response;
            });
        }

        vm.back = function () {
            $location.path('/gallery');
        }
    }
})();
