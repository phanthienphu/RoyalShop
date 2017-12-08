/// <reference path="/Common/Admin/libs/angular/angular.js" />

(function () {
    angular.module('royalshop.application_roles', ['royalshop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {

        $stateProvider.state('application_roles', {
            url: "/application_roles",
            templateUrl: "/app/components/application_roles/ApplicationRoleListView.html",
            parent: 'base',
            controller: "applicationRoleListController"
        })
            .state('add_application_role', {
                url: "/add_application_role",
                parent: 'base',
                templateUrl: "/app/components/application_roles/ApplicationRoleAddView.html",
                controller: "applicationRoleAddController"
            })
            .state('edit_application_role', {
                url: "/edit_application_role/:id",
                templateUrl: "/app/components/application_roles/ApplicationRoleEditView.html",
                controller: "applicationRoleEditController",
                parent: 'base',
            });
    }
})();