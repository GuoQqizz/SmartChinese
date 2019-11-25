function initSelect2(selector, url) {
    $(selector).select2({
        width: '100%',
        placeholder: '请选择',
        minimumResultsForSearch: Infinity,
        allowClear: true,
        ajax: {
            url: url,
            dataType: 'json',
            type: 'GET',
            cache: true,
            processResults: function (data) {
                return {
                    results: data
                };
            }
        }
    });
}