/// <reference path="/Common/Admin/libs/angular/angular.js" />

(function () {
    angular.module('royalshop.application_groups', ['royalshop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {

        $stateProvider.state('application_groups', {
            url: "/application_groups",
            templateUrl: "/app/components/application_groups/ApplicationGroupListView.html",
            parent: 'base',
            controller: "ApplicationGroupListController"
        })
            .state('add_application_group', {
                url: "/add_application_group",
                parent: 'base',
                templateUrl: "/app/components/application_groups/ApplicationGroupAddView.html",
                controller: "ApplicationGroupAddController"
            })
            .state('edit_application_group', {
                url: "/edit_application_group/:id",
                templateUrl: "/app/components/application_groups/ApplicationGroupEditView.html",
                controller: "ApplicationGroupEditController",
                parent: 'base',
            });
    }
})();