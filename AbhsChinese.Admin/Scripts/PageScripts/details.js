; (function ($) {
    $.fn.extend({
        displayDetails: function (data) {
            let $container = this;
            $.each(data, function (propertyName, propertyValue) {
                let name = propertyName.replace(propertyName[0],propertyName[0].toLowerCase());
                let $element = $container.find('[name="' + name + '"]');
                $element.text(propertyValue);
            });
        }
    });
})(jQuery);