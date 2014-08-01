define('logger',['toastr'], function (toastr) {

    function log(message, data, source, showToast) {logIt(message, data, source, showToast, 'info');}
    function logWarning(message, data, source, showToast) {logIt(message, data, source, showToast, 'warning');}
    function logSuccess(message, data, source, showToast) {logIt(message, data, source, showToast, 'success');}
    function logError(message, data, source, showToast) {logIt(message, data, source, showToast, 'error');}

    function logIt(message, data, source, showToast, toastType) {
        source = source ? '[' + source + '] ' : '';
        if (data) {
            console.log(source, message, data);
        } else {
            console.log(source, message);
        }

        if (showToast) {
            switch (toastType) {
                case 'error':
                    toastr.error(message);
                    break;
                case 'success':
                    toastr.success(message);
                    break;
                case 'warning':
                    toastr.warning(message);
                    break;
                default:
                    toastr.info(message);
            }
        }
    }

    return {
        log: log,
        logWarning: logWarning,
        logSuccess: logSuccess,
        logError: logError
    };
});