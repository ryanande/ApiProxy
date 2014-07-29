

var viewModel = function () {

    var self = this;

    this.isLoading = ko.observable(false);

    this.sessionId = ko.observable(getSessionId());
    this.requestCount = ko.observable(0);
    this.errorCount = ko.observable(0);
    this.validUseCases = ko.observableArray([]);
    self.loadData();
};

viewModel.prototype.loadData = function () {
    var self = this;
    self.isLoading(true);

    $.ajax({
        type: 'GET',
        url: '/validateRun/' + self.sessionId(),
        dataType: 'json',
        success: function (result) {
            self.requestCount(result.TotalRequests);
            self.errorCount(result.RequestErrors);
            toastr.success("Loaded validation use case results", "Success!");
        },
        error: function (jqXHR, textStatus, errorThrow) {
            self.isLoading(false);
            toastr.error(errorThrow, "Err");
        }
    }).done(function () {
        self.isLoading(false);
    });
}

viewModel.prototype.runValidation = function () {

    //var self = this;
    //self.isLoading(true);

    //$.ajax({
    //    type: 'GET',
    //    url: '/validateRun/' + self.sessionId(),
    //    dataType: 'json',
    //    success: function (result) {
    //        self.validUseCases(ko.mapping.fromJS(result));
    //        toastr.success("Loaded validation use case results", "Success!");
    //    },
    //    error: function (jqXHR, textStatus, errorThrow) {
    //        self.isLoading(false);
    //        toastr.error(errorThrow, "Err");
    //    }
    //}).done(function () {
    //    self.isLoading(false);
    //});
};

ko.bindingHandlers.fadeVisible = {
    init: function (element, valueAccessor) {
        // Initially set the element to be instantly visible/hidden depending on the value
        var value = valueAccessor();
        $(element).toggle(ko.unwrap(value)); // Use "unwrapObservable" so we can handle values that may or may not be observable
    },
    update: function (element, valueAccessor) {
        // Whenever the value subsequently changes, slowly fade the element in or out
        var value = valueAccessor();
        ko.unwrap(value) ? $(element).fadeIn() : $(element).fadeOut();
    }
};


ko.applyBindings(new viewModel());

function getSessionId() {
    var pathArray = window.location.pathname.split('/');
    return pathArray[pathArray.length - 1];
}



