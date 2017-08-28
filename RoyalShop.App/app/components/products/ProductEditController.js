/// <reference path="D:\THIENPHU_ID\hoctap\Online\NET_MVC5_Entity_Angular1_API\Git\RoyalShop.App\Common/Admin/libs/angular/angular.js" />

(function (app) {
    app.controller("ProductEditController", ProductEditController);

    ProductEditController.$inject = ["$scope", "apiService", "notificationService", "$state","$stateParams","commonService"];

    function ProductEditController($scope, apiService, notificationService, $state,$stateParams, commonService) {
        $scope.product = {};

        $scope.ckeditorOptions = {
            language: "vi",
            height: "200px"
        }

        $scope.UpdateProduct = UpdateProduct;
        $scope.moreImages = [];
        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }

        function LoadProductDetail() {
            apiService.get("/api/product/getbyid/" + $stateParams.id, null, function (resuilt) {
                $scope.product = resuilt.data;
                $scope.moreImages = JSON.parse($scope.product.MoreImages);
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateProduct() {
            $scope.product.MoreImages = JSON.stringify($scope.moreImages);
            apiService.put("/api/product/update", $scope.product, function (resuilt) {
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
                $scope.$apply(function () {
                    $scope.product.Image = fileUrl;
                })
               
            }
            finder.popup();
        }

        $scope.ChooseMoreImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.moreImages.push(fileUrl);
                })

            }
            finder.popup();
        }

        loadProductCategory();
        LoadProductDetail();
    }
})(angular.module("royalshop.products"));