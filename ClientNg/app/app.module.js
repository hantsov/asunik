
// houses a single application-level module for your application
(function () {
    // triggers modern browsers to run the JavaScript in strict mode
    'use strict';

    var app = angular.module("app", ['ngRoute', 'LocalStorageModule']);

    app.config(function ($routeProvider) {
        $routeProvider
        .when("/", {
            templateUrl: "app/home/home.html",
            controller: "MainController",
            controllerAs: "vm"
        })
        .when("/users", {
            templateUrl: "app/users/users.html",
            controller: "UsersController",
            controllerAs: "vm"
        })
        .when("/users/edit/:id", {
                templateUrl: "app/users/user_edit.html",
                controller: "ModUsersController",
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
        .otherwise({
            redirectTo: '/'
        });
    });

})();