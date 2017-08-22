/// <reference path="/Common/Admin/libs/angular/angular.js" />

(function (app) {
    app.controller("ProductCategoryListController", ProductCategoryListController);

    ProductCategoryListController.$inject = ["$scope", "apiService", "notificationService","$ngBootbox"];
    function ProductCategoryListController($scope, apiService, notificationService, $ngBootbox) {
        $scope.productCategories = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getProductCategories = getProductCategories;
        $scope.keyword = "";

        $scope.Search = Search;

        $scope.deleteProductCategory = deleteProductCategory;

        function deleteProductCategory(id) {
            $ngBootbox.confirm("Bạn có muốn xoá?").then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del("/api/productCategory/delete", config, function () {
                    notificationService.displaySuccess("Xoá thành công!");
                    Search();
                }, function () {
                    notificationService.displayError("Xảy ra lỗi! Vui lòng thử lại!");
                })
            });
        }

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
                if (resuilt.data.TotalCount == 0)
                {
                    notificationService.displayWarning("Không tìm thấy bản ghi nào!");
                }
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