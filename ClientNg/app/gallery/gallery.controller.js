(function () {
	'use strict';

	angular
        .module('app')
        .controller('GalleryController', gallery);

	gallery.$inject = ['$http', 'albumsService'];

	function gallery($http, albumsService) {
	    var vm = this;

		activate();

		function activate() {
		    albumsService.getAlbums().then(function (response) {
				vm.albums = response;
		    });
		    vm.selectedAlbumId = '';
		    vm.selectedAlbum = {};
		}

		vm.getAlbum = function () {
		    vm.selectedAlbum =  vm.albums.find(a => a.id === vm.selectedAlbumId);
        }

		vm.setSelectedAlbum = function (albumId) {
		    vm.selectedAlbumId = albumId;
	    };
	}
})();
