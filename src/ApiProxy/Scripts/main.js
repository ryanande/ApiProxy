require.config({
    baseUrl: '../app',
    paths: {
        'angular': '../Scripts/angular.min',
        'angularAMD': '../Scripts/angularAMD.min',
        'angular-route': '../Scripts/angular-route.min',
        'angular-animate': '../Scripts/angular-animate.min',
        'angular-resource': '../Scripts/angular-resource.min',
        'angular-loading-bar': '../Scripts/angular-loading-bar.min',

        'HomeCtrl': 'controllers/HomeCtrl',
        'ErrorCtrl': 'controllers/ErrorCtrl',
        'SessionsCtrl': 'controllers/SessionsCtrl',
        'SessionCtrl': 'controllers/SessionCtrl',
        'UseCasesCtrl': 'controllers/UseCasesCtrl',
        'UseCaseCtrl': 'controllers/UseCaseCtrl'
    },
    shim: {
        'angular': {
            exports: 'angular'
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
        'angular-loading-bar' : {
            deps : ['angular']
        }
    },
    deps: ['app']
});