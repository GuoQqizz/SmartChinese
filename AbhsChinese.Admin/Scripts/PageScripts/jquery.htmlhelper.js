
; (function ($) {
    $.fn.extend({
        initICheck: function (options) {
            let $this = this;
            options = $.extend(true, {
                'source': [],
                'selected': [],
                'name': 'name',
                'className': '',
            }, options);
            let innerHtml = '';
            options.source.map(function (s) {
                let checked = options.selected.includes(s.key) ? 'checked' : '';
                innerHtml += `<div class="${options.className}"><label ><input type="checkbox" class="i-checks" name="${options.name}" value="${s.key}"   ${checked}>${s.value}</label></div>`;
            })
            $($this).html(innerHtml);
            if ($.fn.iCheck) {
                $('.i-checks').iCheck({
                    checkboxClass: 'icheckbox_square-green',
                    radioClass: 'iradio_square-green',
                });
            }

        },
        initSelectOption: function (options) {
            let $this = this;
            options = $.extend(true, {
                'source': [],
                'selected': -1,
                'className': '',
                'default': { key: '-1', value: '请选择' },
                'addDefault': true,
                'useSelect2': false,
            }, options);
            let innerHtml = '';
            if (options.addDefault && options.default) {
                options.source.unshift(options.default);
            }
            var selectedVal = $(this).data('val')
            if (selectedVal && options.selected == -1) {
                options.selected = selectedVal;
            }
            options.source.map(function (s) {
                let selected = options.selected == s.key ? "selected ='selected'" : '';
                innerHtml += `<option value="${s.key}"   class="${options.className}" ${selected}>${s.value}</option>`;
            })
            $($this).html(innerHtml);
            if (options.useSelect2 && $.fn.select2) {
                $($this).select2({ minimumResultsForSearch: Infinity, });
            }
        },
        serializeObject: function () {
            var o = {};
            var a = this.serializeArray();
            $.each(a, function () {
                if (o[this.name] !== undefined) {
                    if (!o[this.name].push) {
                        o[this.name] = [o[this.name]];
                    }
                    o[this.name].push(this.value || '');
                } else {
                    o[this.name] = this.value || ''
                }
            })
            return o;
        },
        initBtns: function (options) {
            let $this = this;
            options = $.extend(true, {
                'source': [],
                'selected': -1,
                'name': '',
                'className': '',
                'default': { key: '-1', value: '全部' },
                'addDefault': true,
            }, options);
            let innerHtml = '';
            if (options.addDefault && options.default) {
                options.source.unshift(options.default);
            }
            var selectedVal = $(this).data('val')
            if (selectedVal && options.selected == -1) {
                options.selected = selectedVal;
            }
            options.source.map(function (s) {
                let className = options.selected == (s.key || s.id) ? "btn-primary" : 'btn-default';
                innerHtml += ` <button type="button"  data-for='${options.name}' data-val="${s.key || s.id}" class="btn   ${options.className}  ${className}">${s.value || s.text}</button>`;
                //innerHtml += `<option value="${s.key || s.id}"   class="${options.className}" ${selected}>${s.value || s.text}</option>`;
            })
            $($this).html(innerHtml);
        }
    });
})(jQuery);