; (function ($) {
    $.fn.extend({
        select: function (option) {
            let $select = this;
            let html = '<option value="' + option.id + '">' + option.text + '</option>';
            $select.html(html).val( option.id).trigger('change');
        },
        selectMany: function (option) {
            let $select = this;
            let html = '';
            $.each(option.ids, function (index, id) {
                html += '<option value="' + id + '">' + option.texts[index] + '</option>';
            });
            $select.html(html).val(option.ids).trigger('change');
        }
    });
})(jQuery);