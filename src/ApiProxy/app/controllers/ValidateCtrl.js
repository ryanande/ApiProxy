define(['app'], function (app) {

    app.register.controller('ValidateCtrl', function ($scope, $log, $routeParams, commandFactory) {

        $scope.pageTitle = 'Validation Details';
        $scope.sessionId = $routeParams.sessionId;
        $scope.validationCases = [];

        $scope.errorMessage = '';


        $scope.getLabel = getLabel;
        $scope.getIcon = getIcon;
        

        function validateSession() {
            commandFactory.validateSession($scope.sessionId).success(function (data) {
                $log.log('data: ', data);
                $scope.validationCases = data;
            }).error(function (err) {
                $log.error(err);
                $scope.errorMessage = err;
            });
        }




        function getLabel(isSuccess) {
            return isSuccess ? 'label label-success' : 'label label-danger';
        }

        function getIcon(isSuccess) {
            return isSuccess ? "glyphicon glyphicon-ok" : "glyphicon glyphicon-remove";
        }



        validateSession();
    });

});