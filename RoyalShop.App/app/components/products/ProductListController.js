/// <reference path="/Common/Admin/libs/angular/angular.js" />

(function (app) {
    app.controller("ProductListController", ProductListController);

    ProductListController.$inject = ["$scope", "apiService", "notificationService", "$ngBootbox", "$filter"];
    function ProductListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.products = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getProducts = getProducts;
        
        $scope.keyword = "";

        $scope.Search = Search;

        $scope.deleteProduct = deleteProduct;

        $scope.SelectAll = SelectAll;

        $scope.DeleteMultiple = DeleteMultiple;
        $scope.ExportExcel = ExportExcel;
        $scope.ExportPdf = ExportPdf;

        function ExportExcel()
        {
            var config = {
                params: {
                    filter: $scope.keyword
                }
            }
            apiService.get('/api/product/ExportXls', config, function (response) {
                if (response.status = 200) {
                    window.location.href = response.data.Message;
                }
            }, function (error) {
                notificationService.displayError(error);

            });
        }

        function ExportPdf(productId) {
            var config = {
                params: {
                    id: productId
                }
            }
            apiService.get('/api/product/ExportPdf', config, function (response) {
                if (response.status = 200) {
                    window.location.href = response.data.Message;
                }
            }, function (error) {
                notificationService.displayError(error);

            });
        }

        function DeleteMultiple() {
            var listID = [];
            $.each($scope.selected, function (i, item) {
                listID.push(item.ID);
            });
            var config = {
                params: {
                    checkedProducts: JSON.stringify(listID)
                }
            };
            apiService.del("/api/product/deletemulti", config, function (resuilt) {
                notificationService.displaySuccess("Xoá thành công " + resuilt.data + " bản ghi!");
                Search();
            }, function (error) {
                notificationService.displayError("Xảy ra lỗi! Vui lòng thử lại!");
            });
        }

        $scope.isAll = false;
        function SelectAll() {
            if ($scope.isAll == false) {
                angular.forEach($scope.products, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            }
            else {
                angular.forEach($scope.products, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("products", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $("#btnDelete").removeAttr("disabled");
            }
            else {
                $("#btnDelete").attr("disabled", "disabled")
            }
        }, true);

        function deleteProduct(id) {
            $ngBootbox.confirm("Bạn có muốn xoá?").then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del("/api/product/delete", config, function () {
                    notificationService.displaySuccess("Xoá thành công!");
                    Search();
                }, function () {
                    notificationService.displayError("Xảy ra lỗi! Vui lòng thử lại!");
                })
            });
        }

        function Search() {
            getProducts();
        }
        function getProducts(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 20
                }
            }
            //url web API
            apiService.get("/api/product/getall", config, function (resuilt) {
                if (resuilt.data.TotalCount == 0) {
                    notificationService.displayWarning("Không tìm thấy bản ghi nào!");
                }
                $scope.products = resuilt.data.Items;
                $scope.page = resuilt.data.Page;
                $scope.pagesCount = resuilt.data.TotalPages;
                $scope.totalCount = resuilt.data.TotalCount;
            }, function () {
                console.log("Load product failed!");
            });
        }

        $scope.getProducts();
    }
})(angular.module("royalshop.products"));