/// <reference path="\Common/Admin/libs/angular/angular.js" />
(function(){
    angular.module("royalshop",
        ["royalshop.products",
          "royalshop.product_categories",
          "royalshop.common"])
          .config(config);

    config.$inject = ["$stateProvider", "$urlRouterProvider"];

    function config($stateProvider,$urlRouterProvider) {
        $stateProvider.state("home", {
            url: "/admin",
            templateUrl: "/app/components/home/HomeView.html",
            controller:"HomeController"
        });
        $urlRouterProvider.otherwise("/admin");// neu khong phai truong hop nao thi tra ve admin 
    }
})();