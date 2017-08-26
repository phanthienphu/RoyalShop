/// <reference path="D:\THIENPHU_ID\hoctap\Online\NET_MVC5_Entity_Angular1_API\Git\RoyalShop.App\Common/Admin/libs/angular/angular.js" />

(function (app) {
    app.controller("ProductAddController", ProductAddController);

    ProductAddController.$inject = ["$scope", "apiService", "notificationService", "$state"];

    function ProductAddController($scope, apiService, notificationService, $state) {
        $scope.product = {
            CreatedDate: new Date(),
            Status: true
        }

        $scope.ckeditorOptions = {
            language: "vi",
            height: "200px"
        }

        $scope.AddProduct = AddProduct;

        function AddProduct() {
            apiService.post("/api/product/create", $scope.product, function (resuilt) {
                notificationService.displaySuccess(resuilt.data.Name + " đã được thêm mới!");
                $state.go("product_categories");
            }, function (error) {
                notificationService.displayError("Thêm mới thất bại!");
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