require(['knockout', 'logger', 'ajax', 'menu', 'mapping', 'jquery', 'toastr', 'bootstrap', 'knockout-amd-helpers', 'knockout-router'],
    function (ko, logger, ajax, menu, mapping) {

        ko.mapping = mapping;

        function notify() {
            console.log(arguments, new Date(), null, true);
        }

        function notFoundHandler(fragment, query) {
            logger.logError(fragment, query, null, true);
            // we actually want to load the 404 view
        }

        ajax.setUp();
        menu.setUp();

        ko.amdTemplateEngine.defaultPath = './app/views';
        ko.amdTemplateEngine.defaultSuffix = '.html';
        ko.bindingHandlers.module.baseDir = './app/viewmodels';

        ko.router.configure({ debug: true, notify: notify, pushState: false });
        /* define routes */
        ko.router.map([
            { route: '', module: 'home', title: 'Dashboard', href: '/', nav: true, icon: 'fa fa-dashboard fa-fw' },
            { route: 'sessions', module: 'sessions', title: 'Sessions', nav: true, icon: 'fa fa-bar-chart-o fa-f' },
            { route: 'validations', module: 'validations', title: 'Validations', nav: true, icon: 'fa fa-history fa-fw' },
            { route: 'useCases', module: 'useCases', title: 'Use Cases', nav: true, icon: 'fa fa-folder fa-fw' },
            { route: '404', module: '', title: '404', nav: false },
            { route: 'session/:id', module: 'session', title: 'session', nav: false },
            { route: 'validate/:id', module: 'validate', title: 'validate', nav: false }
        ]).mapNotFound({ callback: notFoundHandler }); // can specify a module/template/callback/title


        var vm = {
            router: ko.router.vm,
            moduleToShow: ko.observable()
        };

        ko.applyBindings(vm);
        ko.router.init(); //calls ko.history.start() behind the scenes

    });