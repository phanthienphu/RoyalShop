/// <reference path="/Common/Admin/libs/angular/angular.js" />

(function (app) {
    app.controller("ProductCategoryListController", ProductCategoryListController);

    ProductCategoryListController.$inject = ["$scope", "apiService", "notificationService", "$ngBootbox", "$filter"];
    function ProductCategoryListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.productCategories = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getProductCategories = getProductCategories;
        $scope.keyword = "";

        $scope.Search = Search;

        $scope.deleteProductCategory = deleteProductCategory;

        $scope.SelectAll = SelectAll;

        $scope.DeleteMultiple = DeleteMultiple;

        function DeleteMultiple() {
            var listID = [];
            $.each($scope.selected, function (i, item) {
                listID.push(item.ID);
            });
            var config = {
                params: {
                    checkedProductCategories: JSON.stringify(listID)
                }
            };
            apiService.del("/api/productcategory/deletemulti", config, function (resuilt) {
                notificationService.displaySuccess("Xoá thành công " + resuilt.data + " bản ghi!");
                Search();
            }, function (error) {
                notificationService.displayError("Xảy ra lỗi! Vui lòng thử lại!");
            });
        }

        $scope.isAll = false;
        function SelectAll() {
            if($scope.isAll == false)
            {
                angular.forEach($scope.productCategories, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            }
            else
            {
                angular.forEach($scope.productCategories, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("productCategories", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if(checked.length)
            {
                $scope.selected = checked;
                $("#btnDelete").removeAttr("disabled");
            }
            else
            {
                $("#btnDelete").attr("disabled","disabled")
            }
        }, true);

        function deleteProductCategory(id) {
            $ngBootbox.confirm("Bạn có muốn xoá?").then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del("/api/productcategory/delete", config, function () {
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

            $scope.loading = true;
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
                $scope.loading = false;
            }, function () {
                console.log("Load productcategory failed!");
                $scope.loading = false;
            });
        }

        $scope.getProductCategories();
    }
})(angular.module("royalshop.product_categories"));