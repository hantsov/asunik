
// houses a single application-level module for your application
(function () {
    // triggers modern browsers to run the JavaScript in strict mode
    'use strict';

    var app = angular.module("app", ['ngRoute', 'LocalStorageModule']);

    app.config(function ($routeProvider) {
        $routeProvider
        .when("/", {
            templateUrl: "app/home/home.html",
            controller: "HomeController",
            controllerAs: "vm"
        })
        .when("/about", {
            templateUrl: "app/about/about.html"
        })
        .when("/users", {
            templateUrl: "app/users/users.html",
            controller: "UsersController",
            controllerAs: "vm"
        })
        .when("/users/edit/:id", {
                templateUrl: "app/users/user_edit.html",
                controller: "EditUserController",
                controllerAs: "vm"
        })
        .when("/signin", {
                templateUrl: "app/account/signin.html",
                controller: "SigninController",
                controllerAs: "vm"
        })
        .when("/signup", {
            templateUrl: "app/account/signup.html",
                controller: "SignupController",
                controllerAs: "vm"
        })
        .when("/courses", {
            templateUrl: "app/courses/courses.html",
            controller: "CoursesController",
            controllerAs: "vm"
        })
        .when("/courses/edit/:id", {
            templateUrl: "app/courses/course_edit.html",
            controller: "EditCourseController",
            controllerAs: "vm"
        })
        .when("/courses/create", {
            templateUrl: "app/courses/course_create.html",
            controller: "CreateCourseController",
            controllerAs: "vm"
        })
        .when("/profile", {
            templateUrl: "app/profile/profile.html",
            controller: "ProfileController",
            controllerAs: "vm"
        })
        .when("/events/create", {
            templateUrl: "app/events/event_create.html",
            controller: "CreateEventController",
            controllerAs: "vm"
        })
        .when("/gallery", {
            templateUrl: "app/gallery/gallery.html",
            controller: "GalleryController",
            controllerAs: "vm"
        })
        .when("/gallery/albums/:id", {
            templateUrl: "app/gallery/album.html",
            controller: "AlbumsController",
            controllerAs: "vm"
        })
        .otherwise({
            redirectTo: '/'
        });
    });

    app.config(function ($httpProvider) {
        $httpProvider.interceptors.push('authInterceptorService');
    });

    // var serviceBaseUri = 'http://apiasunik.azurewebsites.net/';
    var serviceBaseUri = 'http://localhost:57953/';
    app.constant('apiSettings', {
        apiServiceBaseUri: serviceBaseUri
    });

})();