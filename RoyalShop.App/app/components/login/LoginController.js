(function (app) {
    app.controller('LoginController', ['$scope', 'loginService', '$injector', 'notificationService',
        function ($scope, loginService, $injector, notificationService) {

            $scope.loginData = {
                userName: "",
                password: ""
            };

            $scope.loginSubmit = function () {
                loginService.login($scope.loginData.userName, $scope.loginData.password).then(function (response) {
                    if (response != null && response.data.error != undefined) {
                        notificationService.displayError(response.data.error_description);
                    }
                    else {
                        var stateService = $injector.get('$state'); //inject tự động tránh lỗi liên kết vòng (http:\\abc/http:\\)
                        stateService.go('home');
                    }
                });
            }
        }]);
})(angular.module('royalshop'));