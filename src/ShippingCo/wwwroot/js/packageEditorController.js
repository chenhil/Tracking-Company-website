//packageEditorController.js

(function () {
    "use strict";
    
    angular.module("app-ManagePackage").controller("PackageEditorController", PackageEditorController);

    function PackageEditorController($http, DTDefaultOptions)
    {

        var vm = this;

        var url = "/api/package";

        vm.packages = [];

        vm.errorMessage = "";

        vm.newPackage = {};

        vm.isBusy = true;

        $http.get(url)
            .then(function (response) {
                //success
                angular.copy(response.data, vm.packages);
            }, function (error) {
                vm.errorMessage = error.data;
            }).finally(function () {
                vm.isBusy = false;
            });

        vm.deleteTracking = function (index) {
            var trackingNumber = vm.packages[index].trackingNumber;

            if (confirm("Are you sure you want to delete this item?")) {
                vm.isBusy = true;

                $http.delete("/api/package/" + trackingNumber)
                            .then(function (response) {
                                //success
                                vm.errorMessage = response.data;
                                vm.packages.splice(index, 1);
                            }, function (error) {
                                vm.errorMessage = error.data;
                            }).finally(function () {
                                vm.isBusy = false;
                            });
            } else {

            }
        };

        DTDefaultOptions.setDisplayLength(25);
        DTDefaultOptions.setOption("bLengthChange", false);
    
    }
    
})();