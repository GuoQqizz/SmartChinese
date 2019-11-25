$(function () {
    //提示信息
    var lang = {
        "sProcessing": "<img src='/Images/ajax-loading-icon-19.jpg' style='width:50px;height:50px' />加载中...",
        "sLengthMenu": "每页显示 _MENU_ 项",
        "sZeroRecords": "没有匹配结果",
        "sInfo": "显示第 _START_ 至 _END_ 项，共 _TOTAL_ 项",
        "sInfoEmpty": "显示第 0 至 0 项，共 0 项",
        "sInfoFiltered": "(由 _MAX_ 项过滤)",
        "sInfoPostFix": "",
        "sSearch": "搜索:",
        "sUrl": "",
        "sEmptyTable": "表中数据为空",
        "sLoadingRecords": "正在拼命加载...",
        "sInfoThousands": ",",
        "oPaginate": {
            "sFirst": "首页",
            "sPrevious": "上页",
            "sNext": "下页",
            "sLast": "末页"
        },
        "oAria": {
            "sSortAscending": ": 以升序排列此列",
            "sSortDescending": ": 以降序排列此列"
        },
        "deferRender": true
    }
    //初始化表格
    var table = $("#table1").DataTable({
        language: lang,  //提示信息
        autoWidth: false,  //禁用自动调整列宽
        stripeClasses: ["odd", "even"],  //为奇偶行加上样式，兼容不支持CSS伪类的场合
        processing: true,  //隐藏加载提示,自行处理
        serverSide: true,  //启用服务器端分页
        searching: false,  //禁用原生搜索
        orderMulti: true,  //启用多列排序
        pageLength: 3, //首次加载的数据条数
        ordering: false,//禁用排序
        order: [],  //取消默认排序查询,否则复选框一列会出现小箭头
        renderer: "bootstrap",  //渲染样式：Bootstrap和jquery-ui
        pagingType: "full_numbers",  //分页样式：simple,simple_numbers,full,full_numbers
        columnDefs: [{
            "targets": [0, 1, 2, 3],  //列的样式名
            "orderable": false    //包含上样式名‘nosort’的禁止排序
        }, {
            "targets": [2],
            createdCell: function (cell, cellData, rowData, rowIndex, colIndex) {
                $(cell).click(function () {
                    $(this).html('<input type="text" size="16" style="width: 100%" />');
                    var aInput = $(this).find(":input");
                    aInput.focus().val(cellData);
                });
                $(cell).on("blur", ":input", function () {
                    var text = $(this).val();
                    $(cell).html(text);
                    updatecell(text, rowData.Id);
                    //editTableObj.cell(cell).data(text)
                })
            }
        }, {
            // 定义操作列,######以下是重点########
            "targets": [5],//操作按钮目标列
            "data": null,
            "render": function (data, type, row) {
                var id = '"' + row.Id + '"';
                var html = '<button onclick="detail()" class="delete btn btn-default btn-xs"><i class="fa fa-times"></i>查看</button>'
                html += '<button data-user-id="' + row.Id + '" data-target="#modal_edit" data-toggle="modal" class="btn btn-default btn-xs">编辑</button>'
                html += '<button onclick="remove(' + id + ')" class="btn btn-default btn-xs"><i class="fa fa-arrow-down"></i>删除</button>'
                html += ' <a href="javascript:void(0);"  class="up btn btn-default btn-xs"><i class="fa fa-arrow-up"></i> 上升</a>'
                html += ' <a href="javascript:void(0);"  class="down btn btn-default btn-xs"><i class="fa fa-arrow-down"></i> 下降</a>'
                return html;
            },
        }],
        "aLengthMenu": [3, 6, 8, 10],
        ajax: function (data, callback, settings) {
            //封装请求参数
            var param = {};
            param.pageSize = data.length;//页面显示记录条数，在页面显示每页显示多少项的时候
            param.start = data.start;//开始的记录序号
            param.pageIndex = (data.start / data.length) + 1;//当前页码
            param.search_key = $("#search_key").val();//查询条件
            //param.order = data.order[0];//排序列及方式

            //console.log(param);
            //ajax请求数据
            $.ajax({
                type: "GET",
                url: "/User/GetUsers",
                cache: false,  //禁用缓存
                data: param,  //传入组装的参数
                dataType: "json",
                success: function (result) {
                    //setTimeout仅为测试延迟效果
                    setTimeout(function () {
                        //封装返回数据
                        var returnData = {};
                        returnData.draw = result.draw;//这里直接自行返回了draw计数器,应该由后台返回
                        returnData.recordsTotal = result.total;//返回数据全部记录
                        returnData.recordsFiltered = result.total;//后台不实现过滤功能，每次查询均视作全部结果
                        returnData.data = result.data;//返回的数据列表
                        //console.log(returnData);
                        //调用DataTables提供的callback方法，代表数据已封装完成并传回DataTables进行渲染
                        //此时的数据需确保正确无误，异常判断应在执行此回调前自行处理完毕
                        callback(returnData);
                    }, 200);
                }
            });
        },
        //列表表头字段
        columns: [
            { "data": "Id", "width": "50px" },
            { "data": "Name", "width": "100px" },
            { "data": "Age", "width": "50px" },
            { "data": "Phone", "width": "150px" },
            { "data": "Email", "width": "200px" }
        ]
    });

    //搜索
    $("#btnSearch").click(function () {
        $("#table1").dataTable().fnDraw(false);
    })

    // 初始化上升按钮
    $('#table1 tbody').on('click', 'a.up', function (e) {
        var index = $(this).parent().parent().index() //行号
        if ((index - 1) >= 0) {
            //$("#table1 tbody tr:nth-child(" + index + ")").insertAfter($("#table1 tbody tr:nth-child(" + (index + 1) + ")"))
            var current = $(this).parent().parent();  //获取当前<tr>
            var prev = current.prev();   //获取当前<tr>前一个元素
            current.insertBefore(prev);  //插入到当前<tr>前一个元素前
        } else {
            layer.msg('亲，已经到顶了', { icon: 5 });
        }
    });

    // 初始化下降按钮
    $('#table1 tbody').on('click', 'a.down', function (e) {
        var max = table.rows().data().length;//总条数
        var index = $(this).parent().parent().index()//行号
        if ((index + 1) < max) {
            //$("#table1 tbody tr:nth-child(" + index + ")").insertAfter($("#table1 tbody tr:nth-child(" + (index - 1) + ")"))
            var current = $(this).parent().parent();  //获取当前<tr>
            var next = current.next();  //获取当前<tr>后面一个元素
            current.insertAfter(next);   //插入到当前<tr>后面一个元素后面
        } else {
            layer.msg('亲，已经到底了', { icon: 5 });
        }
    });

    registEventForModal();
});

function registEventForModal() {
    $('#modal_edit').on('show.bs.modal', function (event) {
        get(this, event);
        initCropper(this);
    }).on('hide.bs.modal', function (event) {
        disposeCropper(this);
    });

    $('#modal_add').on('show.bs.modal', function (event) {
        initCropper(this);
    }).on('hide.bs.modal', function (event) {
        disposeCropper(this);
    });
}

function save(self) {
    var $form = $(self).parents('.modal-content').find('form');
    $form.on('submit', function () {
        if ($(this).valid()) {
            $(this).ajaxSubmit(function (response) {
                if (response) {
                    $(self).prev().click();
                    reload();
                }
            });
        }
        return false;
    });
    $form.submit()
         .off('submit');
}

function get(modal, event) {
    let button = event.relatedTarget;
    let userId = button.dataset.userId;
    $.getJSON('/User/Get/' + userId)
       .done(function (userData) {
           let $form = $(modal).find('form');
           $.each(userData, function (propertyName, valueOfProperty) {
               let $input = $form.find('[name="' + propertyName + '"]');
               $input.val(valueOfProperty);
           });
       });
}


//提示弹框
function remove(id) {
    layer.confirm('确认删除吗?',
    {
        icon: 2,
        title: '提示',
        yes: function (index) {
            //确定后执行的代码
            $.get("/User/Delete/" + id).done(function (response) {
                if (response) {
                    layer.msg("删除成功", { icon: 1 })
                    layer.close(index);
                }
            });
        },
        cancel: function (index, layero) {
            layer.close(index);
            reload();// 可以在这里刷新窗口
        }
    });
}

function reload() {
    $("#table1").dataTable().fnDraw(false);
}

$('form').on('reset', function () {
    $('#myEditor').empty();
});