define(['knockout', 'moment'], function (ko, moment) {

    var viewModel = function () {

        var self = this;

        this.isLoading = ko.observable(false);
        this.sessionId = ko.observable("");
        this.transactions = ko.observableArray([]);
        this.requestCount = ko.computed(function() {
            return self.transactions().length;
        });

        this.errorCount = ko.computed(function() {
            return self.transactions().filter(function(val) {
                return !val.ApiResponse.IsSuccessStatusCode;
            }).length;
        });
    };


    viewModel.prototype.formatDate = function(date) {
        return moment().format('MM/DD/YYYY h:mma')
    }
    viewModel.prototype.labelStyle = function(isSuccess) {
        return isSuccess ? "label label-success" : "label label-danger";
    };

    viewModel.prototype.panelStyle = function(isSuccess) {
        return isSuccess ? "panel panel-success" : "panel panel-danger";
    };

    viewModel.prototype.parseUrl = function(val) {
        var url = new URL(val);
        return url.pathname;
    };

    viewModel.prototype.loadData = function() {

        var self = this;
        self.isLoading(true);

        $.ajax({
            type: 'GET',
            url: '/query/sessions/' + self.sessionId(),
            dataType: 'json',
            success: function(result) {
           
                
                var items = ko.mapping.toJS(result);
                items.forEach(function (item) {
                    item.ApiRequest.Headers = mapDictionaryToArray(item.ApiRequest.Headers);
                    item.ApiResponse.Headers = mapDictionaryToArray(item.ApiResponse.Headers);
                })

                console.log(items);
                self.transactions(items);
            }
        }).done(function() {
            self.isLoading(false);
        });
    };

    return viewModel;


    function mapDictionaryToArray(dictionary) {
        var result = [];
        for (var key in dictionary) {
            if (dictionary.hasOwnProperty(key)) {
                result.push({ key: key, value: dictionary[key] });
            }
        }

        return result;
    }
});
