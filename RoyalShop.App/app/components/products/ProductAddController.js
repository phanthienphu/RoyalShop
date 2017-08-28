/// <reference path="D:\THIENPHU_ID\hoctap\Online\NET_MVC5_Entity_Angular1_API\Git\RoyalShop.App\Common/Admin/libs/angular/angular.js" />

(function (app) {
    app.controller("ProductAddController", ProductAddController);

    ProductAddController.$inject = ["$scope", "apiService", "notificationService", "$state", "commonService"];

    function ProductAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.product = {
            CreatedDate: new Date(),
            Status: true
        }

        $scope.ckeditorOptions = {
            language: "vi",
            height: "200px"
        }

        $scope.AddProduct = AddProduct;

        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }

        function AddProduct() {
            $scope.product.MoreImages = JSON.stringify($scope.moreImages);
            apiService.post("/api/product/create", $scope.product, function (resuilt) {
                notificationService.displaySuccess(resuilt.data.Name + " đã được thêm mới!");
                $state.go("products");
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
                $scope.$apply(function () { //$apply thực thi ngay lập tức mà ko chờ hàm
                    $scope.product.Image = fileUrl;
                })
               
            }
            finder.popup();
        }

        $scope.moreImages = [];
        $scope.ChooseMoreImage = function ()
        {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl)
            {
                $scope.$apply(function () {
                    $scope.moreImages.push(fileUrl);
                })
                
            }
            finder.popup();
        }

        loadProductCategory();
    }
})(angular.module("royalshop.products"));