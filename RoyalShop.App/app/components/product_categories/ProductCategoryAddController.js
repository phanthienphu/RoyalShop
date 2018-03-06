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

        $scope.flatFolders = [];
        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
        }

        $scope.AddProductCategory = AddProductCategory;

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
                $scope.parentCategories = commonService.getTree(resuilt.data, "ID", "ParentID");
                $scope.parentCategories.forEach(function (item) {
                    recur(item, 0, $scope.flatFolders);
                });
            }, function () {
                console.log("cannot get list parent!");
            });
        }

        function times(n, str) {
            var result = '';
            for (var i = 0; i < n; i++) {
                result += str;
            }
            return result;
        };
        function recur(item, level, arr) {
            arr.push({
                Name: times(level, '–') + ' ' + item.Name,
                ID: item.ID,
                Level: level,
                Indent: times(level, '–')
            });
            if (item.children) {
                item.children.forEach(function (item) {
                    recur(item, level + 1, arr);
                });
            }
        };

        loadParentCategory();
    }
})(angular.module("royalshop.product_categories"));