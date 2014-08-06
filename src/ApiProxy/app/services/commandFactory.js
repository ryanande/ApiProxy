define(['angularAMD'], function (angularAMD) {

    angularAMD.factory('commandFactory', ['$http', function ($http) {

        var commandUrl = '/command/';

        commandFactory.validateSession = function (sessionId) {
            return $http.get(commandUrl + 'validate/' + sessionId);
        };

        return commandFactory;
    }]);

});