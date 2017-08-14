﻿/// <reference path="/Common/Admin/libs/angular/angular.js" />
(function () {
    angular.module("royalshop.products", ["royalshop.common"]).config(config);

    config.$inject = ["$stateProvider", "$urlRouterProvider"];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state("products", {
            url: "/products",
            templateUrl: "/app/components/products/ProductListView.html",
            controller: "ProductListController"
        });
        $stateProvider.state("product_add", {
            url: "/product_add",
            templateUrl: "/app/components/products/ProductAddView.html",
            controller: "ProductAddController"
        });
    }
})();