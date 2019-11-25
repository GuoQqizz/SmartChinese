
; window.PageBase = function () {

};
; $(function () {
    PageBase.loadSearchParam();
})
window.onbeforeunload = function (e) {
}

//加载页面参数
PageBase.loadSearchParam = function () {
    if (this.NeedLoadParam()) {
        var form = $('#form_search');
        var formObj = this.GetPageParam();
        if (form && formObj != '') {
            formObj = JSON.parse(formObj);
            for (var k in formObj) {
                var exp = `$("[name='${k}']")`;
                var item = eval(exp);
                var v = formObj[k];
                if (item && v && v != -1) {
                    item.val(v);
                    item.data('val', v);
                    //快速选择按钮
                    var forItem = item.data('for');
                    if (forItem) {
                        forItem = $('.' + forItem);
                        if (forItem) {
                            forItem.data('val', v);
                        }
                    }
                }
            }
        }
    }
}

//formObj操作
PageBase.SavePageParam = function (data) {
    window.localStorage.setItem('form_search', data);
}
PageBase.GetPageParam = function () {
    var data = window.localStorage.getItem('form_search');
    return data || '';
}
//获取当前页码
PageBase.GetPageIndex = function () {
    let page = 1;
    if (this.NeedLoadParam()) {
        var formObj = this.GetPageParam();
        if (formObj != '') {
            formObj = JSON.parse(formObj);
            page = formObj['pageIndex'] || 1;
        }
    }
    return page;
}
//是否需要加载参数
PageBase.NeedLoadParam = function () {
    var result = false;
    var toPage = this.GetToPage().toLowerCase();
    var currentPage = window.location.pathname.toLowerCase();
    result = toPage == currentPage || toPage == currentPage + '/index';
    return result || this.IsReload();  //刷新保存form信息 
    //return result;
}
//路由to操作
PageBase.SetToPage = function (page) {
    window.localStorage.setItem('toPage', page);
}
PageBase.GetToPage = function () {
    var toPage = window.localStorage.getItem('toPage');
    return toPage || '';
}

//手动刷新操作
PageBase.IsReload = function () {
    var isReload = window.localStorage.getItem('reload');
    return isReload && isReload == 1;
}

//保存form状态
PageBase.SetReload = function () {
    this.SetToPage('');
    window.localStorage.setItem('reload', 1);
}
PageBase.ClearReload = function () {
    window.localStorage.setItem('reload', 0);
}



//基本流程  ->进入list页,判断topath==pathname,成功则渲染form信息
//          ->list页开始查询,查询后 保存当前form信息,清空topath信息
//          ->跳转到详情页(此时form信息已经保存)
//          ->详情页离开时候,记录 topath=list
//          ->list页面 判断 topath==pathname     -↑
//          ->其他页面进入list页面 topath为空或!=pathname
//          ->topath在详情页离开时候赋值,在list页查询后、刷新清空


//快速选择按钮初始化
//->隐藏域 data-for指定到快算选择元素                                (PageBase.loadSearchParam)
//->隐藏域赋值时候,找到对应的 data-for 元素,给该元素设置 data-val值  (PageBase.loadSearchParam)
//->渲染快速选择时候，判断data-val值，然后给对于的按钮设置选中       (jquery.htmlhelper.initBtns)
//->页面初始化时候 data-for 元素调用 initBtns方法                    (jquery.htmlhelper.initBtns)
//->快速选择按钮点击时候 给对应隐藏域元素赋值


