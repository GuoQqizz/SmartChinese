; (function ($) {
    $.fn.extend({
        iCheckCheckBox: function (values) {
            let $checkboxes = this;
            $.each(values, function (index, value) {
                $checkboxes.filter('[value="' + value + '"]').iCheck('check');
            });
        },
        iCheckRadio: function (value) {
            let $checkboxes = this;
            $checkboxes.filter('[value="' + value + '"]').iCheck('check');
        }
    });
})(jQuery);