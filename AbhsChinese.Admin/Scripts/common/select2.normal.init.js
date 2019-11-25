;$(function myfunction() {
    $('.select2.normal').select2({
        width: '197px',
        allowClear: true,
        minimumResultsForSearch: Infinity
    }).on('change', function (e) {
        $(this).valid();
    });
});