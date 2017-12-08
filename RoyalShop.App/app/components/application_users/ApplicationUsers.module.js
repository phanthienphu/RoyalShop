/// <reference path="/Common/Admin/libs/angular/angular.js" />

(function () {
    angular.module('royalshop.application_users', ['royalshop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {

        $stateProvider.state('application_users', {
            url: "/application_users",
            templateUrl: "/app/components/application_users/ApplicationUserListView.html",
            parent: 'base',
            controller: "applicationUserListController"
        })
            .state('add_application_user', {
                url: "/add_application_user",
                parent: 'base',
                templateUrl: "/app/components/application_users/ApplicationUserAddView.html",
                controller: "applicationUserAddController"
            })
            .state('edit_application_user', {
                url: "/edit_application_user/:id",
                templateUrl: "/app/components/application_users/ApplicationUserEditView.html",
                controller: "applicationUserEditController",
                parent: 'base',
            });
    }
})();