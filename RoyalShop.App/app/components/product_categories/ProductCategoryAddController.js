/// <reference path="/Common/Admin/libs/angular/angular.js" />

(function (app) {
    app.controller("ProductCategoryAddController", ProductCategoryAddController);

    //$state=> đối tượng thuộc UI-Router dùng để điều hướng
    ProductCategoryAddController.$inject = ["$scope", "apiService", "notificationService", "$state", "commonService"];

    function ProductCategoryAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.productCategory = {
            CreatedDate: new Date(),
            Status: true
        }

        $scope.AddProductCategory = AddProductCategory;

        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
        }

        function AddProductCategory() {
            apiService.post("/api/productcategory/create", $scope.productCategory, function (resuilt) {
                notificationService.displaySuccess(resuilt.data.Name + " đã được thêm mới!");
                $state.go("product_categories");
            }, function (error) {
                notificationService.displayError("Thêm mới thất bại!");
            });
        }

        function loadParentCategory() {
            apiService.get("/api/productcategory/getallparents", null, function (resuilt) {
                $scope.parentCategories = resuilt.data;
            }, function () {
                console.log("cannot get list parent!");
            });
        }

        loadParentCategory();
    }
})(angular.module("royalshop.product_categories"));