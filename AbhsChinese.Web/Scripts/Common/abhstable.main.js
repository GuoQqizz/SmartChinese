
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
            let loadingFlag;
            let showLoading = function () {
                if (window.layer) {
                    //注意，layer.msg默认3秒自动关闭，如果数据加载耗时比较长，需要设置time
                    loadingFlag = layer.msg('正在读取数据，请稍候……', { icon: 16, shade: 0.01, shadeClose: false, time: 10000 });
                }
            };
            let closeLoading = function () {
                if (window.layer && loadingFlag) {
                    layer.close(loadingFlag);
                }
            };
            var outParas = {
                reload: function () {
                    $.ajax({
                        url: option.ajax.url,
                        type: option.ajax.type || 'GET',
                        data: dataStr,
                        dataType: 'json',
                        beforeSend: function () {
                            showLoading();
                        },
                        success: function (res) {
                            closeLoading();
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
                            closeLoading();
                        }
                    });
                }
            };
            $seachButton.on('click', function () {
                $table.parent('.table_container').children('.pagination').MyPaging({
                    size: 10,
                    total: 0,
                    current: 1,
                    prevHtml: '上一页',
                    nextHtml: '下一页',
                    layout: 'total, totalPage, prev, pager, next, jumper',
                    jump: function () {
                        var _this = this;

                        dataStr = '';
                        let formData = $form.serialize();
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
                                showLoading();
                            },
                            success: function (res) {
                                closeLoading();
                                $table.abhsTable({
                                    theadData: option.columns,//列头
                                    tbodyData: res.Data,//table显示的数据
                                    tableBordered: true, //是否边框
                                    tableStriped: true, //是否隔行变色
                                    tableHover: true, //是否划过变色,
                                    loaded:option.loaded||{}//table 渲染完成后的回调
                                });
                                _this.setTotal(res.TotalRecord);
                            },
                            error: function (error) {
                                closeLoading();
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

