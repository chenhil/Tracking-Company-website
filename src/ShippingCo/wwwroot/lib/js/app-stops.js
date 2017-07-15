//app-stops.js

(function () {

    //Create the Module
    //angular.module("app-stops", ["trackingControls"]);
    angular.module("app-stops", ["waitControls", "ngRoute"])
        .config(function ($routeProvider) {

            $routeProvider.when("/", {
                controller: "StopsController",
                controllerAs: "vm",
                templateUrl: "/lib/views/trackingControl.html"
            });
            $routeProvider.otherwise({ redirectTo: "/" });
        });
})();