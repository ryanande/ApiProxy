define(['app'], function (app) {

    app.register.controller('ValidateCtrl', function ($scope, $log, $routeParams, commandFactory) {

        $scope.pageTitle = 'Validation Details';
        $scope.sessionId = $routeParams.sessionId;
        

        function validateSession() {
            commandFactory.validateSession($scope.sessionId).success(function (data) {
                $log.log('data: ' + data);
            }).error(function (err) {
                $log.error(err);
            });
        }

        validateSession();
    });

});