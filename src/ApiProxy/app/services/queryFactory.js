define(['angularAMD'], function (angularAMD) {

    angularAMD.factory('queryFactory', ['$http', function ($http) {

        var queryUrl = '/query/';
        var queryFactory = {};

        queryFactory.getSessionData = function (sessionId) {
            return $http.get(queryUrl + 'sessions/' + sessionId);
        };

        return queryFactory;
    }]);

});
