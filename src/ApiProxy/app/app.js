define(['angularAMD', 'angular-route', 'angular-loading-bar', 'angular-animate', 'services/queryFactory'], function (angularAMD) {

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

            $routeProvider.when('/sessions',
                angularAMD.route({
                    templateUrl: 'app/views/sessions.html',
                    controller: 'SessionsCtrl'
                }));

            $routeProvider.when('/session/:sessionId',
                angularAMD.route({
                    templateUrl: 'app/views/session.html',
                    controller: 'SessionCtrl'
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

//    app.factory('queryFactory', ['$http', function ($http) {
//
//        var queryUrl = '/api/query/';
//        var queryFactory = {};
//
//        queryFactory.getSessionData = function (sessionId) {
//            return $http.get(queryUrl + 'sessions/' + sessionId);
//        };
//
//        return queryFactory;
//    }]);

    angularAMD.bootstrap(app);

    return app;
});

