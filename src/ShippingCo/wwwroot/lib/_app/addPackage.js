!function(){"use strict";function a(a){var e=this;e.newPackage={},e.errorMessage="",e.isBusy=!1;var n="/api/package";e.createNewPackage=function(){e.isBusy=!0,a.post(n,e.newPackage).then(function(a){e.errorMessage="Successfully Created",e.newPackage={}},function(a){e.errorMessage=a.data}).finally(function(){e.isBusy=!1})}}a.$inject=["$http"],angular.module("app-ManagePackage").controller("addPackage",a)}();