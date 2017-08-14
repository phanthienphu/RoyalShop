/// <reference path="/Common/Admin/libs/angular/angular.js" />
(function () {
    angular.module("royalshop.product_categories", ["royalshop.common"]).config(config);

    config.$inject = ["$stateProvider", "$urlRouterProvider"];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state("product_categories", {
            url: "/product_categories",
            templateUrl: "/app/components/product_categories/ProductCategoryListView.html",
            controller: "ProductCategoryListController"
        });
    }
})();