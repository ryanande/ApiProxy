define(['app'], function (app) {

    app.register.controller('SessionCtrl', function ($scope, $routeParams) {

        $scope.Title = "Session Details";
        $scope.sessionId = $routeParams.sessionId;

    });

});