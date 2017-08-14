/// <reference path="D:\THIENPHU_ID\hoctap\Online\NET_MVC5_Entity_Angular1_API\Git\RoyalShop.App\Common/Admin/libs/angular/angular.js" />

(function (app) {
    app.controller("ProductCategoryListController", ProductCategoryListController);

    ProductCategoryListController.$inject = ["$scope","apiService"];
    function ProductCategoryListController($scope, apiService) {
        $scope.productCategories = [];

        $scope.getProductCategories = getProductCategories;

        function getProductCategories() {
            //url web API
            apiService.get("/api/productcategory/getall", null, function (resuilt) {
                $scope.productCategories = resuilt.data;
            }, function () {
                console.log("Load productcategory failed!");
            });
        }

        $scope.getProductCategories();
    }
})(angular.module("royalshop.product_categories"));