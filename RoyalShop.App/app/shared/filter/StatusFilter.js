/// <reference path="/Common/Admin/libs/angular/angular.js" />

(function (app) {
    app.filter("StatusFilter", function () {
        return function (input) {
            if (input == true)
                return "Kích hoạt";
            else
                return "Khoá";
        }
    });
})(angular.module("royalshop.common"));