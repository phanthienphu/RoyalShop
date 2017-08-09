/// <reference path="../plugins/angular/angular.js" />

var myApp = angular.module("myModule", []);

myApp.controller("SchoolController", SchoolController);
myApp.service("Validator", Validator);

SchoolController.$inject = ['$scope', 'Validator'];

function SchoolController($scope, Validator) {
    
    $scope.checkNumber = function ()
    {
        $scope.message = Validator.checkNumber($scope.number);
    }
    $scope.number = 1
};

function Validator($window) {
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

