define(['angularAMD'], function (angularAMD) {

    angularAMD.factory('commandFactory', ['$http', function ($http) {

        var commandUrl = '/command/';
        var commandFactory = {};

        commandFactory.validateSession = function (sessionId) {
            return $http.get(commandUrl + 'validate/' + sessionId);
        };

        return commandFactory;
    }]);

});