var BillGatesApp = angular.module('BillGatesApp', ['ngRoute', 'ngSanitize', 'ui.bootstrap', 'ui.tree']);

BillGatesApp.config(function ($routeProvider) {
    $routeProvider
        .when('/test-general', {
            templateUrl: 'Master/GeneralRefMstr.html',
            controller: 'GeneralRefController'
        })
        

});