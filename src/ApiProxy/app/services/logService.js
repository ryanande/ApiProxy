define(['app'], function(app){


    app.register.service('logService', function(){

        logService.log = function(msg, showToast){

            console.log(msg);
            var show = showToast || false;

            if(show){
                //TODO: Toast it up
            }
        };

        return logService;
    });

});