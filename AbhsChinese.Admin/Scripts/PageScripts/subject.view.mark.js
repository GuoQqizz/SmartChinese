function registerEventForMark(status) {
    if (status === 2) {
        $('label.abhs-mark i').on('click', function (e) {
            $(this).toggleClass("abhs-warn");
        });
    } else {
        $('label.abhs-mark i').off('click');
    }
}