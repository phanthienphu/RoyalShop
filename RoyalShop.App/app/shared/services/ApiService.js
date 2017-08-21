/// <reference path="/Common/Admin/libs/angular/angular.js" />

(function (app) {
    app.factory("apiService", apiService);

    apiService.$inject = ["$http", "notificationService"];
    function apiService($http, notificationService) {
        return {
            get: get,
            post: post
        }

        function post(url,data,success,failure) {
            $http.post(url, data).then(function (resuilt) {
                success(resuilt);
            }, function (error) {
                if (error.status === 401)
                {
                    notificationService.displayError("Authenticate is require!");
                }
                else if(failure != null)
                {
                    failure(error);
                }
               
            });
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