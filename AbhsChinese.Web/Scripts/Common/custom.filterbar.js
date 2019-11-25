; (function ($) {
    $.fn.extend({
        filterbar: function (option, callback) {
            let $self = this;

            let defaultOption = {
                showButtonAll: true,
                propertyNameToSubmit: 'name',
                buttons: []
            };
            if (Object.prototype.toString.call(option) === '[object Object]') {
                $.extend(defaultOption, option);
            }

            let template = '';
            //创建全部按钮的html结构
            if (defaultOption.showButtonAll) {
                let buttonAllValue = -1;
                if (defaultOption.buttons.length > 0) {
                    let values = $.map(defaultOption.buttons, function (o, i) {
                        return o.value;
                    });
                    buttonAllValue = values.join(',');
                }
                template = createButton(
                    '全部',
                    buttonAllValue,
                    defaultOption.propertyNameToSubmit);
            }

            //创建按钮的html结构(除了全部按钮),添加到div中
            $.each(defaultOption.buttons, function (i, o) {
                template += createButton(
                    o.text,
                    o.value,
                    defaultOption.propertyNameToSubmit);
            });

            $self.html(template);
            
            //为radio注册change事件，触发事件时，调用回调函数，并把value传过去
            $self.off('change', ':radio').on('change', ':radio', function () {
                callback(this.value);
            });
        }
    });
})(jQuery);

//创建<label><input type="radio" /></label>结构，该段html会显示成按钮
function createButton(text, value, name) {
    let localText = text ? text : '';
    let localValue = value ? value : '';
    let localName = name ? name : '';
    return `<label class="btn btn-default">${localText}<input type="radio" name="${localName}" value="${localValue}" /></label>`;
}