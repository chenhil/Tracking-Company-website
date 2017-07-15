(function () {
    "use strict";

    angular.module("waitControls", [])
      .directive("waitCursor", waitCursor);

    function waitCursor() {
        return {
            scope: {
                show: "=displayWhen"
            },
            restrict: "E",
            templateUrl: "/views/waitCursor.html"
        };
    }
})();