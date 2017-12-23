/// <reference path="/Common/Admin/libs/angular/angular.js" />
(function () {
    angular.module("royalshop.statistics", ["royalshop.common"]).config(config);

    config.$inject = ["$stateProvider", "$urlRouterProvider"];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state("statistic_revenue", {
                url: "/statistic_revenue",
                parent: "base",
                templateUrl: "/app/components/statistic/RevenueStatisticView.html",
                controller: "RevenueStatisticController"
            });
    }
})();