define(['app'], function (app) {

    app.register.controller('HomeCtrl', function ($scope, $log, queryFactory) {

        $scope.pageTitle = "Validation API Proxy";
        $scope.pageSubTitle = "Session Validator";
        $scope.searchText = "";
        $scope.currentSession = "";
        $scope.sessions = [];
        $scope.totalSessions = sessionCount;
        $scope.totalErrors = sessionErrors;

        $scope.isSearch = isInSearch;
        $scope.urlPath = getUrlPath;
        $scope.getLabel = getLabel;
        $scope.getIcon = getIcon;
        $scope.formatDate = formatDate;

        $scope.searchClick = getSessions;


        function getSessions() {
            $scope.currentSession = $scope.searchText;

            queryFactory.getSessionData($scope.currentSession).success(function (data) {
                $scope.sessions = data;
            }).error(function (err) {
                $log.error(err);
            });
        }
        

        function sessionCount() {
            return $scope.sessions.length;
        }

        function sessionErrors() {
            return $scope.sessions.filter(function (item) { return !item.ApiResponse.IsSuccessStatusCode; }).length;
        }

        function isInSearch() {
            return $scope.searchText.length > 0;
        }


        function formatDate(d) {
            var s = moment(d).format('MM-DD-YYYY, h:mm:ss a');
            return s;
        }

        function getUrlPath(url) {
            var path = new URL(url);
            return path.pathname;
        }

        function getLabel(isSuccess) {
            return isSuccess ? 'label label-success' : 'label label-danger';
        }

        function getIcon(isSuccess) {
            return isSuccess ? "glyphicon glyphicon-ok" : "glyphicon glyphicon-remove";
        }


    });
});