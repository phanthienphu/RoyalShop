/// <reference path="/Common/Admin/libs/angular/angular.js" />
(function () {
    angular.module("royalshop.products", ["royalshop.common"]).config(config);

    config.$inject = ["$stateProvider", "$urlRouterProvider"];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state("products", {
                url: "/products",
                parent: "base",
                templateUrl: "/app/components/products/ProductListView.html",
                controller: "ProductListController"
        }).state("product_add", {
                url: "/product_add",
                parent: "base",
                templateUrl: "/app/components/products/ProductAddView.html",
                controller: "ProductAddController"
        }).state("product_import", {
            url: "/product_import",
            parent: "base",
            templateUrl: "/app/components/products/ProductImportView.html",
            controller: "ProductImportController"
        }).state("product_edit", {
                url: "/product_edit/:id",
                parent: "base",
                templateUrl: "/app/components/products/ProductEditView.html",
                controller: "ProductEditController"
        });
    }
})();