/// <reference path="D:\THIENPHU_ID\hoctap\Online\NET_MVC5_Entity_Angular1_API\Git\RoyalShop.App\Common/Admin/libs/angular/angular.js" />

(function (app) {
    app.controller("ProductCategoryListController", ProductCategoryListController);

    ProductCategoryListController.$inject = ["$scope","apiService"];
    function ProductCategoryListController($scope, apiService) {
        $scope.productCategories = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getProductCategories = getProductCategories;
        $scope.keyword = "";

        $scope.Search = Search;

        function Search() {
            getProductCategories();
        }
        function getProductCategories(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 20
                }
            }
            //url web API
            apiService.get("/api/productcategory/getall", config, function (resuilt) {
                $scope.productCategories = resuilt.data.Items;
                $scope.page = resuilt.data.Page;
                $scope.pagesCount = resuilt.data.TotalPages;
                $scope.totalCount = resuilt.data.TotalCount;
            }, function () {
                console.log("Load productcategory failed!");
            });
        }

        $scope.getProductCategories();
    }
})(angular.module("royalshop.product_categories"));