/// <reference path="../plugins/angular/angular.js" />

var myApp = angular.module("myModule", []);

myApp.controller("SchoolController", SchoolController);
myApp.controller("StudentController", StudentController);
myApp.controller("TeacherController", TeacherController);

//myApp.$inject = ['$scope']; //hoạt động bình thường khi bỏ ở file ko phải dạng nén, khi nén file ở dạng .min sẽ bị lỗi
//declare
//function StudentController($rootScope,$scope) {
//    $rootScope.message = "This is my message from Student";
//};

function SchoolController($Scope) {
    $Scope.message = "Announcement from school.";
};

function StudentController($scope) {
    $Scope.message = "This is my message from Student";
};

function TeacherController($scope) {
   $scope.message = "This is my message from Teacher";
}