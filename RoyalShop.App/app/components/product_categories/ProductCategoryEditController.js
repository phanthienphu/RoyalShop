/// <reference path="/Common/Admin/libs/angular/angular.js" />

(function (app) {
    app.controller("ProductCategoryEditController", ProductCategoryEditController);

    //$state=> đối tượng thuộc UI-Router dùng để điều hướng
    ProductCategoryEditController.$inject = ["$scope", "apiService", "notificationService", "$state","$stateParams","commonService"];

    function ProductCategoryEditController($scope, apiService, notificationService, $state, $stateParams,commonService) {
        $scope.productCategory = {
            CreatedDate: new Date(),
            Status: true
        }

        $scope.UpdateProductCategory = UpdateProductCategory;
        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
        }

        function LoadProductCategoryDetail() {
            apiService.get("/api/productcategory/getbyid/" + $stateParams.id,null, function (resuilt) {
                $scope.productCategory = resuilt.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateProductCategory() {
            apiService.put("/api/productcategory/update", $scope.productCategory, function (resuilt) {
                notificationService.displaySuccess(resuilt.data.Name + " đã được cập nhật thành công!");
                $state.go("product_categories");
            }, function (error) {
                notificationService.displayError("Cập nhật thất bại!");
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
        LoadProductCategoryDetail();
    }
})(angular.module("royalshop.product_categories"));