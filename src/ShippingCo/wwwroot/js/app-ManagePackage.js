//app-AddPackage.js

(function () {

    //Create the Module
    angular.module("app-ManagePackage", ["waitControls", "ngRoute","datatables"])
        .config(function ($routeProvider) {

            $routeProvider.when("/", {
                controller: "PackageEditorController",
                controllerAs: "vm",
                templateUrl: "/views/packageEditor.html"
            });

            $routeProvider.when("/editor/:trackingNumber", {
                controller: "StopEditorController",
                controllerAs: "vm",
                templateUrl: "/views/stopEditor.html"
            });

            $routeProvider.when("/addpackage", {
                controller: "addPackage",
                controllerAs: "vm",
                templateUrl: "/views/addPackage.html"
            });

            $routeProvider.otherwise({ redirectTo: "/" });
        });
})();