/// <reference path="D:\THIENPHU_ID\hoctap\Online\NET_MVC5_Entity_Angular1_API\Git\RoyalShop.App\Common/Admin/libs/angular/angular.js" />

(function (app) {
    app.controller("RevenueStatisticController", RevenueStatisticController);

    RevenueStatisticController.$inject = ["$scope", "apiService", "notificationService","$filter"];

    function RevenueStatisticController($scope, apiService, notificationService, $filter) {
        $scope.resData=[];
        $scope.labels = [];
        $scope.series = ['Doanh số', 'Lợi nhuận'];

        $scope.chartdata = [];
        function GetStatistic(){
            var config = {
                param:{
                    fromDate:"01/01/2016",
                    toDate:"01/01/2018"
                }
            }
            apiService.get("/api/statistic/getrevenue",config,function(response)
            {
                $scope.resData = response.data;
                var labels = [];
                var charData = [];
                var revenue = [];
                var benefits = [];
                $.each(response.data,function(i,item)
                {
                    labels.push($filter("date")(item.Date,"dd/MM/yyyy"));
                    revenue.push(item.Revenue);
                    benefits.push(item.Benefit);
                });
                charData.push(revenue);
                charData.push(benefits);

                $scope.chartdata =charData; 
                $scope.labels = labels;
            },function(response)
            {
                notificationService.displayError("Không thể tải dữ liệu!");
            });
        }

        GetStatistic();
    }
})(angular.module("royalshop.products"));