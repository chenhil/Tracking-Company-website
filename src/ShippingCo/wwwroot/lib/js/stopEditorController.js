// stopEditorController.js
(function () {
    "use strict";

    angular.module("app-ManagePackage").controller("StopEditorController", StopEditorController);

    function StopEditorController($routeParams, $http) {
        var vm = this;

        vm.trackingNumber = $routeParams.trackingNumber;
        vm.stops = [];
        vm.errorMessage = "";
        vm.isBusy = true;
        vm.newStop = {};

        var url = "/api/package/" + vm.trackingNumber + "/stops";

        $http.get(url)
          .then(function (response) {
              // success
              angular.copy(response.data, vm.stops);
          }, function (err) {
              // failure
              vm.errorMessage = "Failed to load package infos.";
          })
          .finally(function () {
              vm.isBusy = false;
          });

        vm.addStop = function () {

            vm.isBusy = true;

            $http.post(url, vm.newStop)
              .then(function (response) {
                  // success
                  vm.stops.push(response.data);
                  vm.newStop = {};
              }, function (err) {
                  // failure
                  vm.errorMessage = "Failed to add new stop";
              })
              .finally(function () {
                  vm.isBusy = false;
              });
        };
    }
})();