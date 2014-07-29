
// on load








var reqResPair = function () {
    this.Id = "";
    this.LogDate = "";
    this.SessionId = "";
    this.ApiRequest = "";
    this.ApiResponse = "";
}

reqResPair.prototype.InjectFrom = function (obj) {
    for (var propertyName in obj) {
        if (this.hasOwnProperty(propertyName))
            this[propertyName] = obj[propertyName];
    }
    return this;
}



var viewModel = function () {

    var self = this;

    this.sessionId = ko.observable("");
    this.Items = ko.observableArray([]);

    this.validateData = function() {

    };

};






ko.applyBindings(viewModel);


function getData(session) {
    $.ajax({
        complete: function() {
            
        },
        type: 'GET',
        url: '/api/validate/' + session,

        success: function (result) {
            alert('yo:' + result)
        }
    })
}