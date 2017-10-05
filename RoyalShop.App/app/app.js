/// <reference path="\Common/Admin/libs/angular/angular.js" />
(function(){
    angular.module("royalshop",
        ["royalshop.products",
          "royalshop.product_categories",
          "royalshop.common"])
          .config(config)
           .config(configAuthentication);

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
                controller:"LoginController"
            })
            .state("home", {
                url: "/admin",
                parent:"base",
                templateUrl: "/app/components/home/HomeView.html",
                controller:"HomeController"
        });
        $urlRouterProvider.otherwise("/login");// neu khong phai truong hop nao thi tra ve login
    }

    function configAuthentication($httpProvider) {
        //interceptors quản trị việc tương tác giữa client và server
        $httpProvider.interceptors.push(function ($q, $location) { 
            return {
                request: function (config) {

                    return config;
                },
                requestError: function (rejection) {

                    return $q.reject(rejection);
                },
                response: function (response) {
                    if (response.status == "401") {
                        $location.path('/login');
                    }
                    //the same response/modified/or a new one need to be returned.
                    return response;
                },
                responseError: function (rejection) {

                    if (rejection.status == "401") {
                        $location.path('/login');
                    }
                    return $q.reject(rejection);
                }
            };
        });
    }
})();