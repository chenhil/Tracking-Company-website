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

        vm.deleteStop = function (index) {
            var stopInfo = vm.stops[index];
            if (confirm("Are you sure you want to delete this item?")) {
                vm.isBusy = true;
                    $http({
                        url: "/api/package/" + vm.trackingNumber + "/stops",
                        method: 'DELETE',
                        data: {
                            location: stopInfo.location,
                            activity: stopInfo.activity
                        },
                        headers: {
                            "Content-Type": "application/json;charset=utf-8"
                        }
                    }).then(function (response) {
                        //success
                        vm.errorMessage = response.data;
                        vm.stops.splice(index, 1);
                    }, function (error) {
                        vm.errorMessage = error.data;
                    }).finally(function () {
                        vm.isBusy = false;
                    });
             
            } else {

            }

        };


    }
})();