/// <reference path="\Common/Admin/libs/angular/angular.js" />
(function(){
    angular.module("royalshop",
        ["royalshop.products",
          "royalshop.product_categories",
          "royalshop.common"])
          .config(config);

    config.$inject = ["$stateProvider", "$urlRouterProvider"];

    function config($stateProvider,$urlRouterProvider) {
        $stateProvider
            .state("base",{
                url:"",
                templateUrl: "/app/shared/views/BaseView.html",
                abstract: true
            })
            .state("login", {
                url: "/login",
                templateUrl: "/app/components/login/LoginView.html",
                controller:"HomeController"
            })
            .state("home", {
                url: "/admin",
                parent:"base",
                templateUrl: "/app/components/home/HomeView.html",
                controller:"HomeController"
        });
        $urlRouterProvider.otherwise("/login");// neu khong phai truong hop nao thi tra ve login
    }
})();