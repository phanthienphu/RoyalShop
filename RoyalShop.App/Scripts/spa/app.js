/// <reference path="../plugins/angular/angular.js" />

var myApp = angular.module("myModule", []);

myApp.controller("SchoolController", SchoolController);

myApp.directive("royalShopDirective", royalShopDirective);

myApp.service("ValidatorService", ValidatorService);

SchoolController.$inject = ['$scope', 'ValidatorService'];

function SchoolController($scope, ValidatorService) {
    
    $scope.checkNumber = function ()
    {
        $scope.message = ValidatorService.checkNumber($scope.number);
    }
    $scope.number = 1
};

function ValidatorService($window) {
    return{
        checkNumber: checkNumber
    }
    function checkNumber(input) {
        if (input % 2 == 0)
            return "This is even";
        else
            return "This is odd";
    }
};

function royalShopDirective() {
    return {
        restrict:"E";
        template: "<h1>What is this?</h1>"
    }
}

