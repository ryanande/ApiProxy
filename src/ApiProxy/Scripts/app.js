



var reqResPair = function() {
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

    this.searchText = ko.observable("");
    this.logItems = ko.observableArray([]);
    this.showResults = ko.computed(function() {
        return self.logItems.length > 0;
    });
    this.onSearchClicked = function () {
        $.get("/Sessions/" + self.searchText(), function (data) {
            var it = [];
            data.forEach(function (i) {
                it.push(new reqResPair().InjectFrom(i));
            });

            self.logItems(it);
        });
    };
};




ko.applyBindings(viewModel);