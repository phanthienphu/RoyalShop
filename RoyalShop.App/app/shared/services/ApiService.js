/// <reference path="/Common/Admin/libs/angular/angular.js" />

(function (app) {
    app.factory("apiService", apiService);

    apiService.$inject = ["$http"];
    function apiService($http) {
        return {
            get: get
        }

        function get(url, params, success, failure) {
            $http.get(url, params).then(function (resuilt) {
                success(resuilt);
            }, function (error) {
                failure(error);
            });
        }
    }
})(angular.module("royalshop.common"));