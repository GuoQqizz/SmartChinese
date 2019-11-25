; (function ($, window, document, undefined) {
    //默认参数
    var plugName = "abhsTable",
		defaults = {
		    theadData: null, //头部栏目
		    tbodyData: null, //tbody内容
		    tableBordered: true, //是否边框
		    tableStriped: true, //是否隔行变色
		    tableHover: true, //是否划过变色
		    rowHeight: 40,//行高,
		    scrollWidth: 6,//滚动条宽度
		    loaded: null,
		},
        headDefaults = {
            label: "",//列名
            name: null,//绑定列
            width: 0,//每列默认宽度
            align: "center",//对齐方式
            formatter: null,//自定义模板方法
        };

    //构造类
    function abhsTable($this, options) {
        $this.html("").addClass("abhs_table");
        this.doc = $this;//用于存放自己的节点
        this.boxWidth = $this.width();//用于获取控件的宽度
        this.boxHeight = $this.height();//用于获取控件的高度
        this.name = plugName;//用于设定表格名称
        this.headDefaults = headDefaults;//设定表头默认项
        this.options = $.extend({}, defaults, options);//合并默认设定
        this.init();
    }
    //制造表
    function makeTable(_this, options) {
        var headstr = "", columnWidth = 0;
        for (var i in options.theadData) {
            if (options.theadData.hasOwnProperty(i)) {
                var attr = $.extend({}, headDefaults, options.theadData[i]);
                var style = " style='";
                if (attr.width) {
                    style += " width:" + attr.width + "px;";
                    columnWidth += attr.width;
                }
                else {
                    columnWidth += 40;
                }
                style += "'";
                headstr += "<th" + style + ">" + attr.label + "</th>";
            }
        }
        var str = "<table class='table " + ((_this.options.tableBordered ? " tableBordered" : "") + (_this.options.tableStriped ? " tableStriped" : "") + (_this.options.tableHover ? " tableHover" : "")) + "'" + (columnWidth > _this.boxWidth ? " style='width:" + columnWidth + "px'" : "") + "><thead><tr>";
        str += headstr;
        str += "</tr></thead><tbody>";
        if (options.tbodyData) {
            for (var r = 0; r < options.tbodyData.length; r++) {
                str += "<tr>";
                for (var d in options.theadData) {
                    if (options.theadData.hasOwnProperty(d)) {
                        var attr = $.extend({}, headDefaults, options.theadData[d]);
                        str += "<td style='text-align:" + options.theadData[d].align + ";'>";
                        if (typeof (attr.formatter) == "function") {
                            str += attr.formatter(r, options.tbodyData[r][attr.name], options.tbodyData[r]);
                        }
                        else {
                            var val = options.tbodyData[r][attr.name];
                            str += (typeof (val) != "undefined" && val != null ? val : "");//如果是undefined或null定义为""
                        }
                        str += "</td>";
                    }
                }
                str += "</tr>";
            }
        }
        str += "</tbody></table>";
        return { str: str, sx: columnWidth > _this.boxWidth, cw: columnWidth };
    }
    /// <summary>获取滚动条大小</summary>
    /// <param name="box">内容框宽/高</param>
    /// <param name="scrollbox">滚动条框宽/高</param>
    /// <param name="inner">内容宽/高</param>
    function getScrollSize(box, scrollbox, inner) {
        //滚动框高度 - 向上取整( 最大距离[内容-内容框] ÷ 内容宽/高 × 滚动条高度 )
        return scrollbox - Math.ceil((inner - box) / inner * scrollbox);
    }
    /// <summary>获取滚动条滚动位置</summary>
    /// <param name="sp">内容位置</param>
    /// <param name="scrollbox">滚动条框宽/高</param>
    /// <param name="inner">内容宽/高</param>
    function getScrollPosition(innerp, scrollbox, inner) {
        return Math.ceil(innerp / inner * scrollbox);
    }
    /// <summary>获取内容滚动位置</summary>
    /// <param name="innerp">滚动条位置</param>
    /// <param name="scrollbox">滚动条框宽/高</param>
    /// <param name="inner">内容宽/高</param>
    function getInnerPosition(sp, scrollbox, inner) {
        return Math.ceil(sp / scrollbox * inner);
    }

    //计算滚动条
    function MakeScroll(_this) {
        var innerHeight = _this.bodyBox.scrollHeight, outerHeight = _this.bodyBox.offsetHeight;
        var innerWidth = _this.bodyBox.scrollWidth, outerWidth = _this.bodyBox.offsetWidth;
        var hasy = outerHeight < innerHeight;//判断是否有垂直滚动条
        var hasx = outerWidth < innerWidth;//判断是否有水平滚动条
        var headHeight = $(_this.headBox).height();


        var yDown = { down: false, top: 0, y: 0 }, xDown = { down: false, left: 0, x: 0 };
        if (hasy) {//如果有横向滚动条
            $(_this.headBox).width(_this.boxWidth - _this.options.scrollWidth);//减少宽度给滚动条留位置
            $(_this.bodyBox).width(_this.boxWidth - _this.options.scrollWidth);//减少宽度给滚动条留位置
            innerWidth = _this.bodyBox.scrollWidth, outerWidth = _this.bodyBox.offsetWidth;
            hasx = outerWidth < innerWidth;//再次判断是否有水平滚动轴


            var boxheight = (_this.boxHeight - (hasx ? _this.options.scrollWidth : 0));//计算表格去除滚动条后的高度
            $(_this.scrollY).height(boxheight).show();//给滚动条设置高度并显示
            $(_this.bodyBox).css("max-height", boxheight + "px");//设置表格的最大高度

            var scrollBoxHeight = boxheight - headHeight;//获取滚动框高度
            var scroll = $(_this.scrollY).find(".scroll");//获取滚动条
            scroll.height(getScrollSize(boxheight, scrollBoxHeight, innerHeight));//设置滚动条高度
            scroll.css("top", (headHeight + getScrollPosition(_this.bodyBox.scrollTop, scrollBoxHeight, innerHeight)) + "px");//设置滚动条位置
            scroll.width(_this.options.scrollWidth);
            scroll.mousedown(function (e) {
                yDown.down = true;
                yDown.y = e.pageY;
                yDown.top = parseInt($(this).css("top"));
                $(_this.doc).addClass("noselect");
            })
        }
        else {
            $(_this.scrollY).hide();
        }
        if (hasx) {//如果有水平滚动条
            $(_this.scrollX).width(_this.boxWidth).show();//设置滚动条的宽度并显示
            innerHeight = _this.bodyBox.scrollHeight, outerHeight = _this.bodyBox.offsetHeight;
            var scrollboxWidth = $(_this.scrollX).width() - _this.options.scrollWidth;//滚动框宽度
            var scroll = $(_this.scrollX).find(".scroll");//获取滚动条
            scroll.width(getScrollSize(scrollboxWidth, scrollboxWidth, innerWidth));//设置滚动条宽度
            scroll.css("left", getScrollPosition(_this.bodyBox.scrollLeft, scrollboxWidth, innerWidth) + "px");//设置距离左侧距离
            scroll.height(_this.options.scrollWidth);
            scroll.mousedown(function (e) {
                xDown.down = true;
                xDown.left = parseInt($(this).css("left"));
                xDown.x = e.pageX;
                $(_this.doc).addClass("noselect");
            })
        }
        else {
            $(_this.scrollX).hide();
        }
        $(window).mousemove(function (e) {
            if (xDown.down) {
                var scroll = $(_this.scrollX).find(".scroll");
                var move = e.pageX - xDown.x;
                var left = xDown.left + move;
                var maxl = (_this.boxWidth - scroll.width() - _this.options.scrollWidth);
                if (left < 0) {
                    scroll.css("left", "0px");
                    _this.headBox.scrollLeft = _this.bodyBox.scrollLeft = 0;
                }
                else if (left > maxl) {
                    scroll.css("left", maxl + "px");
                    _this.headBox.scrollLeft = _this.bodyBox.scrollLeft = innerWidth - outerWidth;
                }
                else {
                    scroll.css("left", left + "px");
                    var scrollboxWidth = $(_this.scrollX).width() - _this.options.scrollWidth;//滚动框宽度
                    _this.headBox.scrollLeft = _this.bodyBox.scrollLeft = getInnerPosition(left, scrollboxWidth, innerWidth);
                }

            }
            else if (yDown.down) {
                var scroll = $(_this.scrollY).find(".scroll");
                var move = e.pageY - yDown.y;
                var top = yDown.top + move;
                var maxl = _this.boxHeight - scroll.height() - (hasx ? _this.options.scrollWidth : 0);
                if (top < headHeight) {
                    scroll.css("top", headHeight + "px");
                    _this.bodyBox.scrollTop = 0;
                }
                else if (top > maxl) {
                    scroll.css("top", maxl + "px");
                    _this.bodyBox.scrollTop = innerHeight - outerHeight;
                }
                else {
                    scroll.css("top", top + "px");
                    var _top = top - headHeight;
                    var boxheight = (_this.boxHeight - (hasx ? _this.options.scrollWidth : 0));//计算表格去除滚动条后的高度
                    var scrollBoxHeight = boxheight - headHeight;//获取滚动框高度
                    _this.bodyBox.scrollTop = getInnerPosition(_top, scrollBoxHeight, innerHeight);
                }
            }
        });

        $(window).mouseup(function () {
            xDown.down = yDown.down = false;
            $(_this.doc).removeClass("noselect");
        });
        $(_this.doc).off("mousewheel DOMMouseScroll");
        //滚轮事件
        $(_this.doc).on("mousewheel DOMMouseScroll", function (e) {
            var delta = (e.originalEvent.wheelDelta && (e.originalEvent.wheelDelta > 0 ? 1 : -1)) ||
                          (e.originalEvent.detail && (e.originalEvent.detail > 0 ? -1 : 1));
            if (delta == 1) {
                _this.bodyBox.scrollTop -= 10;
            }
            else if (delta == -1) {
                _this.bodyBox.scrollTop += 10;
            }

            var scroll = $(_this.scrollY).find(".scroll");//获取垂直滚动条
            var boxheight = (_this.boxHeight - (hasx ? _this.options.scrollWidth : 0));//计算表格去除滚动条后的高度
            var scrollBoxHeight = boxheight - headHeight;//获取滚动框高度
            var top = getScrollPosition(_this.bodyBox.scrollTop, scrollBoxHeight, innerHeight) + headHeight;
            scroll.css("top", top + "px");
        });
    }

    function resize(_this) {
        var size = { width: $(_this.doc).width(), height: $(_this.doc).height() }
        $(_this.headBox).width(size.width);
        $(_this.bodyBox).width(size.width);
        $(_this.bodyBox).css("max-height", size.height + "px");

        _this.boxWidth = _this.doc.width();//用于获取控件的宽度
        _this.boxHeight = _this.doc.height();//用于获取控件的高度
        MakeScroll(_this);
    }
    abhsTable.prototype = {
        init: function () {
            var table = makeTable(this, this.options);
            var html = '<div class="abhs_table_thead" style="width:' + this.boxWidth + 'px; height:' + this.options.rowHeight + 'px;">';
            html += table.str;
            html += "</div>";
            html += '<div class="abhs_table_tbody" style="width:' + this.boxWidth + 'px;' + (this.boxHeight ? "max-height:" + this.boxHeight + "px" : "") + '">';
            html += table.str;
            html += "</div>";
            html += "<div class=\"abhs_table_overflowx\" style=\"height:" + this.options.scrollWidth + "px\"><div class=\"scroll\"></div><div class=\"scroll_xrightbox\" style=\"width:" + this.options.scrollWidth + "px;height:" + this.options.scrollWidth + "px;\"></div></div>";
            html += "<div class=\"abhs_table_overflowy\"style=\"width:" + this.options.scrollWidth + "px\"><div class=\"scroll_ytopbox\"  style=\"width:" + this.options.scrollWidth + "px;\"></div><div class=\"scroll\"></div></div>";
            this.doc.html(html);
            this.headBox = this.doc.find(".abhs_table_thead")[0];
            this.bodyBox = this.doc.find(".abhs_table_tbody")[0];
            this.scrollX = this.doc.find(".abhs_table_overflowx")[0];
            this.scrollY = this.doc.find(".abhs_table_overflowy")[0];
            $(this.headBox).css({ "height": $(this.headBox).find("thead").height() + "px" });
            $(this.scrollY).find(".scroll_ytopbox").css("height", $(this.headBox).height());
            MakeScroll(this);
            var _this = this;
            $(window).resize(function () {
                resize(_this);
            })
            if (typeof (this.options.loaded) == "function") {
                this.options.loaded();
            }

        },
        headBox: null,
        bodyBox: null,
        scrollX: null,//横向滚动条
        scrollY: null,//纵向滚动条
        resize: function () {
            resize(this);
        }
    }

    $.fn.extend({
        abhsTable: function (options) {
            return this.each(function () {
                new abhsTable($(this), options);
            })
        }
    })
})(jQuery, window, document);

; (function ($, window) {
    /**
	  opt: {
		linkNum: 5,		// 中间按钮个数 		默认5
		current: 1,		// 页面初始当前页 	默认1
		size: 10,		// 每页显示的条数 	默认10
		layout: 'total, prev, pager, next, jumper',	// 设置显示的内容		// 默认'total, prev, pager, next, jumper'
		prevHtml: '&lt;',	// 上一页html	默认&lt;
		nextHtml: '&gt;',	// 下一页html	默认&gt;
		jump: fn 		// 跳转时执行方法 	必须
	  }
	  jump方法中获取当前页数this.current，获取显示条数this.current
	  jump中必须调用this.setTotal(100)方法设置总页数
	 */
    function MyPaging(oPagingParent, opt) {
        this.oPagingParent = oPagingParent;	// 初始化分页的盒子
        this.total = 0;					// 总条数
        this.totalPage = 0;				// 总页数

        this.linkNum = opt.linkNum || 5;	// 中间按钮个数
        this.current = opt.current || 1;	// 当前页
        this.size = opt.size || 10;			// 每页多少条
        this.prevHtml = opt.prevHtml || '&lt;';	// 上一页html
        this.nextHtml = opt.nextHtml || '&gt;';	// 下一页html


        this.layout = ['total', '  prev', 'pager', 'next', 'jumper'];
        if (opt.layout) {
            this.layout = opt.layout.split(',');
        }

        if (!opt.jump) {
            return;
        }
        this.jump = opt.jump;

        this._init();
    }
    MyPaging.prototype = {
        _init: function () {
            this.jump();
        },

        // 设置总页数方法 调用设置html方法
        setTotal: function (data) {
            if (data >= 0) {
                this.total = data;
                this.totalPage = Math.ceil(this.total / this.size);

                this._setPagingHtml();
            }
        },

        // 设置html
        _setPagingHtml: function () {
            var html = '<div class="_my-paging-box">';
            for (var iKey = 0; iKey < this.layout.length; iKey++) {
                var key = this.layout[iKey].replace(/\s/g, '');

                // 总条数
                if (key == 'total') {
                    html += '<div class="total pg-item">共<span>' + this.total + '</span>条</div>';
                }

                // 总页数
                if (key == 'totalPage') {
                    html += '<div class="sizes pg-item">共<span>' + this.totalPage + '</span>页</div>'
                }

                // 上一条
                if (key == 'prev') {
                    html += '<div class="floatR"><div class="link-btn prev pg-item' + (this.current == 1 ? ' disabled' : '') + '" data-current="prev">' + this.prevHtml + '</div>';
                }

                // 分页按钮
                if (key == 'pager') {
                    html += '<ul class="link-list pg-item">';


                    var start = end = 0;
                    var sPager = ''
                    // 总页数小于按钮个数
                    if (this.totalPage <= this.linkNum) {
                        start = 1;
                        end = this.totalPage;
                        for (var i = 1; i <= this.totalPage; i++) {
                            sPager += '<li class="link-btn' + (this.current == i ? ' active' : '') + '" data-current="' + i + '">' + i + '</li>';
                        }

                        // 当前页小于2分之最大按钮数
                    } else if (this.current < Math.ceil(this.linkNum / 2)) {
                        start = 1;
                        end = this.linkNum;
                        for (var i = 1; i <= this.linkNum; i++) {
                            sPager += '<li class="link-btn' + (this.current == i ? ' active' : '') + '" data-current="' + i + '">' + i + '</li>';
                        }

                        // 当前页大于总条数减2分之最大按钮数
                    } else if (this.current > this.totalPage - Math.ceil(this.linkNum / 2)) {
                        start = this.totalPage - this.linkNum + 1;
                        end = this.totalPage;
                        for (var i = this.totalPage - this.linkNum + 1; i <= this.totalPage; i++) {
                            sPager += '<li class="link-btn' + (this.current == i ? ' active' : '') + '" data-current="' + i + '">' + i + '</li>';
                        }

                        // 其它
                    } else {
                        start = this.current - Math.ceil(this.linkNum / 2) + 1;
                        end = this.current - Math.ceil(this.linkNum / 2) + this.linkNum;
                        for (var i = 1; i <= this.linkNum; i++) {
                            var idx = this.current - Math.ceil(this.linkNum / 2) + i;
                            sPager += '<li class="link-btn' + (this.current == idx ? ' active' : '') + '" data-current="' + idx + '">' + idx + '</li>';
                        }
                    }

                    // 当前页大于按钮页一般及总页数大于按钮数
                    if (this.current > Math.ceil(this.linkNum / 2) && this.totalPage > this.linkNum) {
                        html += '<li class="link-btn" data-current="1">1</li>';
                        if (start > 2) {
                            html += '<li>···</li>';
                        }
                    }

                    html += sPager;

                    // 当前页小于按钮数一般并且总页数大于按钮数
                    if (this.current <= this.totalPage - Math.ceil(this.linkNum / 2) && this.totalPage > this.linkNum) {
                        if (end < this.totalPage - 1) {
                            html += '<li>···</li>';
                        }
                        html += '<li class="link-btn" data-current="' + this.totalPage + '">' + this.totalPage + '</li>';
                    }

                    html += '</ul>';
                }

                // 下一条
                if (key == 'next') {
                    html += '<div class="link-btn next pg-item' + (this.current == this.totalPage ? ' disabled' : '') + '" data-current="next">' + this.nextHtml + '</div>';
                }

                // 跳输入框
                if (key == 'jumper') {
                    html += '<div class="jumper pg-item"><span>前往</span><input type="text"><span>页</span></div>';
                }
            }
            html += '</div></div>';

            this.oPagingParent.html(html);
            this._setPagingEvent();//设置事件
        },

        // 设置分页事件
        _setPagingEvent: function () {
            var _this = this;
            var oMyPaging = this.oPagingParent.find('._my-paging-box');
            var oLinkBtn = oMyPaging.find('.link-btn');
            var oIpt = oMyPaging.find('.jumper input');

            // 按钮点击事件
            oLinkBtn.on('click', function (e) {
                //e.preventDefault();
                var oTag = $(this);
                var current = oTag.data('current');
                var to = _this.current;
                //console.log(to)
                if (current == 'prev') {
                    to = to > 1 ? to - 1 : 1;
                } else if (current == 'next') {
                    to = to < _this.totalPage ? to + 1 : _this.totalPage;
                } else {
                    to = current;
                }

                if (to == _this.current) {
                    return;
                }

                _this.current = to;
                _this.jump();
            })

            // 输入框回车事件
            oIpt.on('keydown', function (event) {
                var code = event.keyCode;

                if (code == 13) {
                    var to = $(this).val();

                    if (!(to >= 1 && to <= _this.totalPage)) {
                        to = 1;
                    }

                    _this.current = to;
                    _this.jump();
                }
            })
        },
    }
    //	for (var i in prototype) {
    //		MyPaging.prototype[i] = prototype[i];
    //	}


    $.fn.MyPaging = function (opt) {
        new MyPaging(this, opt || {});
    }
})(jQuery, window);