define('ajax', ['jquery', 'logger'], function ($, logger) {

    //$.ajax({
    //    type: 'GET',
    //    url: '/query/sessions/' + sessionId,
    //    dataType: 'json',
    //    success: function (result) {
    //        return result;
    //    }
    //});


    var setUp = function() {
        // put this is a module !!!!!!
        $.ajaxSetup({
            error: function(jqXHR, textStatus, errorThrown) {

                switch (jqXHR.status) {
                case 401: // unauthorized
                    logger.logError("Unauthorized!", errorThrown, jqXHR.responseText, true);
                    break;
                case 403: //forbidden
                    window.location.hash = '#invalid';
                    break;
                case 404:
                    logger.logError("Endpoint not found!", errorThrown, "", true);
                    break;
                case 500: // server err
                    logger.logError("Server Error occured! ", errorThrown, "", true);
                    throw reason;
                default:
                    logger.logError(textStatus, errorThrown, jqXHR.responseText, true);
                }
            }
        });
    };

    return {
        setUp: setUp
    }; 
});