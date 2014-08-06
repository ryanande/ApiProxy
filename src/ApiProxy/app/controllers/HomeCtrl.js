define(['app'], function (app) {

    app.register.controller('HomeCtrl', function ($scope, $log, queryFactory) {

        $scope.pageTitle = "Validation API Proxy";
        $scope.pageSubTitle = "Session Validator";
        $scope.searchText = "";
        $scope.sessions = [];

        $scope.searchClick = getSessions;

        $scope.isSearch = isInSearch;


        function getSessions() {
            queryFactory.getSessionData($scope.searchText).success(function (data) {
                $scope.sessions = data;
            }).error(function (err) {
                //TODO: toastr?
                $log.error(err);
            });
        }

        function isInSearch() {
            return $scope.searchText.length > 0;
        }
    });
});