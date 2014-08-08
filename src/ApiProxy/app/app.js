define(['angularAMD', 'angular-route', 'angular-loading-bar', 'angular-animate', 'jquery', 'bootstrap', 'linq', 'services/queryFactory', 'services/commandFactory'], function (angularAMD) {

    var app = angular.module('app', ['ngRoute', 'angular-loading-bar', 'ngAnimate'])
        .config(['$routeProvider', function ($routeProvider) {

            $routeProvider.when('/',
                angularAMD.route({
                    templateUrl: 'app/views/home.html',
                    controller: 'HomeCtrl'
                }));

            $routeProvider.when('/error/:errorId',
                angularAMD.route({
                    templateUrl: 'app/views/error.html',
                    controller: 'ErrorCtrl'
                }));
            
            $routeProvider.when('/validate/:sessionId',
                angularAMD.route({
                    templateUrl: 'app/views/validate.html',
                    controller: 'ValidateCtrl'
                }));

            $routeProvider.when('/usecases',
                angularAMD.route({
                    templateUrl: 'app/views/useCases.html',
                    controller: 'UseCasesCtrl'
                }));

            $routeProvider.when('/usecase/:useCaseId',
                angularAMD.route({
                    templateUrl: 'app/views/useCase.html',
                    controller: 'UseCaseCtrl'
                }));

            $routeProvider.otherwise({
                redirectTo: '/error/404'
            });
        }]);

    angularAMD.bootstrap(app);

    return app;
});

