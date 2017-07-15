// add-package.js
(function () {
    "use strict";

    angular.module("app-ManagePackage").controller("addPackage", addPackage);


    function addPackage($http) {
        var vm = this;
        vm.newPackage = {};
        vm.errorMessage = "";
        vm.isBusy = false;

        var url = "/api/package";

        vm.createNewPackage = function () {

            vm.isBusy = true;

            $http.post(url, vm.newPackage)
                .then(function (response) {
                    vm.errorMessage = "Successfully Created";
                    vm.newPackage = {};
                }, function (error) {
                    vm.errorMessage = error.data;
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        };
    }
})();