
function showSpinner(id) {
    if (id === void 0) { id = null; }
    var elm;
    if (!id || id.length <= 0) {
        elm = $('.eGym-loader');
    }
    else {
        elm = $("#" + id);
    }
    elm.each(function () {
        $(this).css({ 'display': 'flex', 'visibility': 'visible' });
        $(this).on('click', function (e) {
            e.preventDefault();
            e.stopPropagation();
        });
    });
}

function hideSpinner(id) {
    if (id === void 0) { id = null; }
    var elm;
    if (!id || id.length <= 0) {
        elm = $('.eGym-loader');
    }
    else {
        elm = $("#" + id);
    }
    elm.each(function () {
        $(this).css({ 'display': 'none', 'visibility': 'hidden' });
        $(this).on('click', function (e) {
            e.preventDefault();
            e.stopPropagation();
        });
    });
}

