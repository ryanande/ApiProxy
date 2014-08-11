require.config({
    baseUrl: '../app',
    paths: {
        'angular': '../Scripts/angular.min',
        'angularAMD': '../Scripts/angularAMD.min',
        'angular-route': '../Scripts/angular-route.min',
        'angular-animate': '../Scripts/angular-animate.min',
        'angular-resource': '../Scripts/angular-resource.min',
        'angular-loading-bar': '../Scripts/angular-loading-bar.min',
        'jquery': '../Scripts/jquery-2.1.1.min',
        'bootstrap': '../Scripts/bootstrap.min',
        'linq': '../Scripts/linq.min',
        'moment': '../Scripts/moment.min',

        'HomeCtrl': 'controllers/HomeCtrl',
        'ErrorCtrl': 'controllers/ErrorCtrl',
        'ValidateCtrl': 'controllers/ValidateCtrl',
        'UseCasesCtrl': 'controllers/UseCasesCtrl',
        'UseCaseCtrl': 'controllers/UseCaseCtrl'
    },
    shim: {
        'angular': {
            exports: 'angular'
        },
        'jquery': {
            exports: '$'
        },
        'bootstrap': {
            deps: ['jquery']
        },
        'angularAMD': {
            deps: ['angular']
        },
        'angular-route': {
            deps: ['angular']
        },
        'angular-animate': {
            deps: ['angular']
        },
        'angular-resource': {
            deps: ['angular']
        },
        'angular-loading-bar': {
            deps: ['angular']
        }
    },
    deps: ['app']
});

define(["moment"], function (moment) {
    moment().format();
});