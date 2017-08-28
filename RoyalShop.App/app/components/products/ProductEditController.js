/// <reference path="D:\THIENPHU_ID\hoctap\Online\NET_MVC5_Entity_Angular1_API\Git\RoyalShop.App\Common/Admin/libs/angular/angular.js" />

(function (app) {
    app.controller("ProductEditController", ProductEditController);

    ProductEditController.$inject = ["$scope", "apiService", "notificationService", "$state", "commonService"];

    function ProductEditController($scope, apiService, notificationService, $state, commonService) {
        $scope.product = {};

        $scope.ckeditorOptions = {
            language: "vi",
            height: "200px"
        }

        $scope.UpdateProduct = UpdateProduct;

        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }

        function UpdateProduct() {
            apiService.post("/api/product/update", $scope.product, function (resuilt) {
                notificationService.displaySuccess(resuilt.data.Name + " đã được cập nhật!");
                $state.go("products");
            }, function (error) {
                notificationService.displayError("Xảy ra lỗi! Vui lòng thử lại!");
            });
        }

        function loadProductCategory() {
            apiService.get("/api/productcategory/getallparents", null, function (resuilt) {
                $scope.productCategories = resuilt.data;
            }, function () {
                console.log("cannot get list parent!");
            });
        }

        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.product.Image = fileUrl;
            }
            finder.popup();
        }

        loadProductCategory();
    }
})(angular.module("royalshop.products"));