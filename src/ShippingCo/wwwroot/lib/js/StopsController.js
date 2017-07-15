//StopsController.js

(function () {
    "use strict";

    //Getting the existing module
    angular.module("app-stops").controller("StopsController", StopsController);


    function StopsController($http)
    {
        var vm = this;

        vm.stops = [];

        vm.order = {};

        vm.errorMessage = "";

        vm.trackingNumber = "";

        vm.isBusy = false;

        var parameter = getParameterByName("tracking");
        if (parameter != null) {
            vm.trackingNumber = parameter;
            getTrackingInfo(parameter, $http);
        }

        vm.getTracking = function () {
            vm.isBusy = true;

            var foundTracking = vm.trackingNumber = vm.order.number;
            getTrackingInfo(vm.order.number, $http);
            vm.order = {};
        };


        function getTrackingInfo(trackingNumber, http) {
            http.get("/api/package/" + trackingNumber + "/stops")
                .then(function (response) {
                    angular.copy(response.data, vm.stops);
                    showDiv("trackingContainer");
                    showDiv("trackingTable");
                    hideDiv("notFound");
                }, function (error) {                    
                    vm.errorMessage = "Unable to find Package";
                    showDiv("trackingContainer");
                    showDiv("notFound");
                    hideDiv("trackingTable");
                }).finally(function () {
                    vm.isBusy = false;

                });
        }
    }

    function getParameterByName(name, url) {
        if (!url) {
            url = window.location.href;
        }
        name = name.replace(/[\[\]]/g, "\\$&");
        var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
            results = regex.exec(url);
        if (!results) return null;
        if (!results[2]) return '';
        return decodeURIComponent(results[2].replace(/\+/g, " "));
    }

    function showDiv(show) {
        var s = document.getElementById(show);
        if (!s) {
            return true;
        }
        s.style.display = "block";
        return true;
    }

    function hideDiv(hide) {
        var h = document.getElementById(hide);
        if (!h) {
            return true;
        }
        h.style.display = "none";
        return true;
    }

})();