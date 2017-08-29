/// <reference path="D:\THIENPHU_ID\hoctap\Online\NET_MVC5_Entity_Angular1_API\Git\RoyalShop.App\Common/Admin/libs/angular/angular.js" />

(function (app) {
    app.controller("rootController", rootController);

    rootController.$inject = ["$scope", "$state"];
    
    function rootController($scope, $state) {
        $scope.logout = function () {
            $scope.go("login")
        }
    }
})(angular.module("royalshop"));