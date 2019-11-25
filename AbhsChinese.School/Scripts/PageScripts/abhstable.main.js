
//依赖于 common/base.js
var formatterStr = function (key) {
    return function (index, value, row) {
        let ret = '';
        if (!!row[key]) {
            ret = row[key];
        }
        return ret;
    }
};
var formatterImg = function (img, alt, className) {
    return function (index, value, row) {
        let ret = '';
        if (!!row[img]) {
            if (!!row[alt]) {
                ret = ' <img src="' + row[img] + '" alt="' + row[alt] + '"  class="' + className + '"/>'
            } else {
                ret = ' <img src="' + row[img] + ' class="img-info"/>'
            }

        }
        return ret;
    }
};
; (function ($) {
    $.fn.extend({
        table: function (option) {
            let $table = this;
            let $form = $(option.searchBox);
            let $seachButton = $form.find('.search');
            var dataStr = "";
            var outParas = {
                reload: function () {
                    $.ajax({
                        url: option.ajax.url,
                        type: option.ajax.type || 'GET',
                        data: dataStr,
                        dataType: 'json',
                        beforeSend: function () {
                            loading.showLoading('正在读取数据，请稍候……');// showLoading();
                        },
                        success: function (res) {
                            loading.closeLoading();
                            $table.abhsTable({
                                theadData: option.columns,//列头
                                tbodyData: res.Data,//table显示的数据
                                tableBordered: true, //是否边框
                                tableStriped: true, //是否隔行变色
                                tableHover: true, //是否划过变色,
                                loaded: option.loaded || {}//table 渲染完成后的回调
                            });
                        }, error: function (err) {
                            console.error(err);
                            loading.closeLoading();
                        }
                    });
                }
            };

            $seachButton.on('click', function () {
                $table.parent('.table_container').children('.pagination').MyPaging({
                    size: option.size || 10,
                    total: option.total || 0,
                    current: option.current || 1,
                    prevHtml: '上一页',
                    nextHtml: '下一页',
                    layout: 'total, totalPage, prev, pager, next, jumper',
                    jump: function () {
                        var _this = this;
                        dataStr = '';
                        let formData = $form.serialize();
                        if (window.PageBase && $form.serializeObject) {
                            let formObj = $form.serializeObject();
                            formObj['pageIndex'] = _this.current;
                            formObj['pageSize'] = _this.size;
                            PageBase.SavePageParam(JSON.stringify(formObj));
                            PageBase.SetToPage('');
                            PageBase.ClearReload();
                        }
                        if (formData) {
                            dataStr = formData + '&';
                        }

                        dataStr += 'pagination.pageIndex=' + _this.current;
                        dataStr += '&';
                        dataStr += 'pagination.pageSize=' + _this.size;

                        $.ajax({
                            url: option.ajax.url,
                            type: option.ajax.type || 'GET',
                            data: dataStr,
                            dataType: 'json',
                            beforeSend: function () {
                                loading.showLoading();
                            },
                            success: function (res) {
                                loading.closeLoading();
                                if (res.ErrorCode == 0) {
                                    $table.abhsTable({
                                        theadData: option.columns,//列头
                                        tbodyData: res.Data,//table显示的数据
                                        tableBordered: true, //是否边框
                                        tableStriped: true, //是否隔行变色
                                        tableHover: true, //是否划过变色,
                                        loaded: option.loaded || {}//table 渲染完成后的回调
                                    });
                                } else {
                                    let msg = '获取数据异常';
                                    if (res.ErrorCode <= 10000) {
                                        msg = res.ErrorMsg;
                                    }
                                    if (window.layer) {
                                        var index = window.layer.alert(msg,
                                           { icon: 0 },
                                           function () { window.layer.close(index); });
                                    } else {
                                        alert(msg);
                                    }

                                }
                                _this.setTotal(res.TotalRecord);
                            }, error: function (err) {
                                console.log(err);
                                loading.closeLoading();
                            }
                        });
                    }
                });
            });
            $seachButton.click();
            return outParas;
        }
    });
})(jQuery);

