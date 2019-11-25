var CurriulumSet = (function () {
    //章节
    function Chapter() {
        var index = 0, id = 0, name = "", setTime = "", startTime = 0, status = "", pages = [], nowIndex = 0, stop = false, isApprove = false, lastTimeIsOver = true, startAddGoldsPageNum = 0, startPageNum = 0, totalPageNum = 0, courseId = 0, lessonId = 0, lessonProgressId = 0, lessonIndex = 0, progressKey = "";

        var isSubminting = false;//是否正在提交
        //设定章节对象参数
        Object.defineProperties(this, {
            //序号
            index: {
                get: function () { return index; },
                set: function (val) {
                    if (typeof (val) == "number") {
                        index = val;
                    }
                }
            },
            //章节id
            id: {
                enumerable: true,//允许枚举
                get: function () {
                    return id;
                },
                set: function (val) {
                    id = val;
                }
            },
            //章节名称
            name: {
                enumerable: true,//允许枚举
                get: function () {
                    return name;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        name = val;
                    }
                    else {
                        console.error("章节名称必须为字符串");
                    }
                }

            },
            //最后修改时间
            setTime: {
                enumerable: true,//允许枚举
                get: function () {
                    return setTime;
                },
                set: function (val) {
                    if (typeof (val) == "string" && /^ *\d{3,4}(-|\/)\d{1,2}(-|\/)\d{1,2}( *\d{1,2}:(\d{1,2}(:\d{1,2})?)?)? *$/.test(val)) {
                        setTime = val.trim();
                    }
                    else if (val instanceof Date) {
                        setTime = val.formatter("yyyy-MM-dd HH:mm:ss");
                    }
                    else {
                        console.error("章节最后修改时间应该为 时间类型字符串 或 Date类型");
                    }
                }
            },
            //当前页面的下标
            nowIndex: {
                writable: true,
                value: nowIndex
            },
            //当前页面开始时间
            startTime: {
                writable: true,
                value: startTime
            },
            //状态
            status: {
                enumerable: true,
                get: function () {
                    return status;
                },
                set: function (val) {
                    switch (val) {
                        case "未编辑": status = "未编辑"; break;
                        case "未完成": status = "未完成"; break;
                        case "已完成": status = "已完成"; break;
                        case "合格": status = "合格"; break;
                        case "不合格": status = "不合格"; break;
                        default: console.error("章节状态应为 未编辑/未完成/已完成/合格/不合格 几种状态."); break;
                    }
                }
            },
            //章节列表副本
            pages: {
                enumerable: true,
                get: function () {
                    return pages;
                }
            },
            //添加讲义
            addPage: {
                value: function addPage(page) {
                    if (page instanceof Page) {
                        //                      page.pageNum = pages.length;
                        Object.defineProperty(page, "parent", {
                            value: this
                        })
                        pages.push(page);
                    }
                    else {
                        console.error("添加的讲义页类型错误,应为 Page 对象");
                    }
                }
            },
            //移动讲义
            movePage: {
                value: function movePage(i, t) {
                    if (!(typeof (i) == "number" && 0 <= i && i < pages.length)) {
                        console.error("章节下标选择错误");
                    }
                    if (!(typeof (t) == "number" && 0 <= t && t < pages.length)) {
                        console.error("移动至下表选择错误");
                    }
                    var p = pages.splice(i, 1)[0];//获取自己并从数组中移出
                    pages.splice(t, 0, p);//在插入到对应位置中去
                    for (var i in pages)//遍历后从新设置页码
                    {
                        pages[i].pageNum = parseInt(i);
                    }
                }
            },
            //删除讲义
            deletePage: {
                value: function deletePage(i) {
                    if (!(typeof (i) == "number" && 0 <= i && i < pages.length)) {
                        console.error("章节下标选择错误");
                    }
                    var self = pages.splice(i, 1)[0];//获取自己并从数组中移出并返回
                    for (var i in pages) {//重新设置页码
                        pages[i].pageNum = parseInt(i);
                    }
                    return self;
                }
            },
            //获取讲义
            getPage: {
                value: function getChapter(index) {
                    if (typeof (index) == "number" && index < pages.length) {
                        return pages[index];
                    }
                    else {
                        console.error("获取章节讲义失败,请查看下标是否正确");
                    }
                }
            },
            isStop: {
                get: function () {
                    return stop;
                }
            },
            //暂停
            Stop: {
                value: function Stop() {
                    stop = true;
                }
            },
            //继续
            Continue: {
                value: function Continue() {
                    stop = false;
                    startTime = (new Date()).getTime();
                }
            },
            //是否为审批状态
            isApprove: {
                enumerable: true,//允许枚举
                get: function () {
                    return isApprove;
                },
                set: function (val) {
                    isApprove = val;
                }
            },
            //上次学习是否结束
            lastTimeIsOver: {
                enumerable: true,//允许枚举
                get: function () {
                    return lastTimeIsOver;
                },
                set: function (val) {
                    lastTimeIsOver = val;
                }
            },
            //金币开始累加的页码
            startAddGoldsPageNum: {
                enumerable: true,//允许枚举
                get: function () {
                    return startAddGoldsPageNum;
                },
                set: function (val) {
                    startAddGoldsPageNum = val;
                }
            },
            //开始执行的页码
            startPageNum: {
                enumerable: true,//允许枚举
                get: function () {
                    return startPageNum;
                },
                set: function (val) {
                    startPageNum = val;
                }
            },
            //开始执行的页码
            totalPageNum: {
                enumerable: true,//允许枚举
                get: function () {
                    return totalPageNum;
                },
                set: function (val) {
                    totalPageNum = val;
                }
            },
            //课程ID
            courseId: {
                enumerable: true,//允许枚举
                get: function () {
                    return courseId;
                },
                set: function (val) {
                    courseId = val;
                }
            },
            //课时ID
            lessonId: {
                enumerable: true,//允许枚举
                get: function () {
                    return lessonId;
                },
                set: function (val) {
                    lessonId = val;
                }
            },
            //课时进度ID
            lessonProgressId: {
                enumerable: true,//允许枚举
                get: function () {
                    return lessonProgressId;
                },
                set: function (val) {
                    lessonProgressId = val;
                }
            },
            //课时下标
            lessonIndex: {
                enumerable: true,//允许枚举
                get: function () {
                    return lessonIndex;
                },
                set: function (val) {
                    lessonIndex = val;
                }
            },
            //进度秘钥
            progressKey: {
                enumerable: true,//允许枚举
                get: function () {
                    return progressKey;
                },
                set: function (val) {
                    progressKey = val;
                }
            },
            //提交数据的数组
            submitDataArr: {
                value: []
            },
            //提交数据的方法
            submit: {
                value: function submit(data) {
                    var _this = this;
                    if (!_this.isApprove) {//如果不是审批状态,提交数据
                        if (data) {//如果给了数据,追加到提交的数组中
                            _this.submitDataArr.push(data);
                        }
                        if (!isSubminting && _this.submitDataArr.length > 0) {//如果不是提交状态,且仍有待提交数据
                            isSubminting = true;//变更为提交状态
                            var d = _this.submitDataArr.shift();//移除并获取第一个数据;
                            function callback() {
                                isSubminting = false;//变更为提交状态
                                if (_this.submitDataArr.length > 0) {
                                    _this.submit();
                                }
                                else if (d.pagenum == _this.totalPageNum) {
                                    _this.dataCallBack();
                                }
                            }
                            console.log("第", d.pagenum, "页数据准备提交", d);
                            $.ajax({
                                url: "/LearningCenter/SubmintProcressData?r=" + Math.floor(Math.random() * 10000),
                                type: "post",
                                dataType: "json",
                                data: {
                                    dataStr: d.data
                                },
                                success: function (data) {
                                    console.log("第", d.pagenum, "页数据提交完成", data);
                                    callback();
                                },
                                error: function (e) {
                                    console.warn("第", d.pagenum, "页数据提交失败", e);
                                    callback();
                                }
                            });//提交数据
                        }
                    }

                }
            },
            //加载页码
            loadPage: {
                writable: true,
                value: function loadPage(index, size, callback) {
                    var _this = this;
                    $.ajax({
                        url: "/LearningCenter/GetLessonPage",
                        type: "post",
                        dataType: "json",
                        data: {
                            CourseID: _this.courseId,
                            LessonID: _this.lessonId,
                            isApprove: _this.isApprove,
                            Pagination: {
                                PageIndex: index,
                                PageSize: size
                            }
                        },
                        success: function (data) {
                            if (data.ErrorCode == 0) {
                                var list = data.Data;
                                console.log("加载页数据", list);
                                for (var i = 0; i < list.length; i++) {

                                    var newPage = new Page(list[i]);
                                    _this.addPage(newPage);
                                    newPage.callBack = function () {
                                        var _chapter = this.parent;
                                        $('.CurriculumPage').remove();
                                        this.submitData.courseId = _chapter.courseId;//课程ID
                                        this.submitData.lessonId = _chapter.lessonId;//课时ID
                                        this.submitData.lessonNum = _chapter.lessonIndex;//课时序号
                                        this.submitData.unitId = this.pageId;//讲义页ID
                                        this.submitData.unitNum = this.pageNum;//讲义页序号
                                        this.submitData.totalUnitNum = _chapter.totalPageNum;//讲义总页码数
                                        this.submitData.useTime += ((new Date()).getTime() - _chapter.startTime);//页面结束时记录学习用时
                                        this.submitData.useTime = Math.ceil(this.submitData.useTime / 1000);//学习用时向上取整转成秒
                                        _chapter.startTime = (new Date()).getTime();

                                        this.submitData.progressId = _chapter.lessonProgressId;//学习进度id
                                        this.submitData.progressKey = _chapter.progressKey;//学习进度秘钥
                                        this.submitData.coinsKey = this.coinsKey;//金币秘钥
                                        if (!_chapter.isApprove) {//如果不是审批状态,提交数据
                                            _chapter.submit({ pagenum: this.pageNum, data: JSON.stringify(this.submitData) });
                                        }
                                        _chapter.nowIndex++;
                                        if (_chapter.nowIndex < _chapter.pages.length) {
                                            _chapter.pages[_chapter.nowIndex].doWork();
                                        } else {
                                            _chapter.callBack();
                                        }
                                    }
                                }
                                if (typeof (callback) == "function") {
                                    callback(list);
                                }
                            }
                            else {

                            }
                        },
                        error: function (e) {

                        }
                    });
                }
            },
            //开始工作
            doWork: {
                value: function doWrok() {
                    var _this = this;
                    if (this.isApprove) {//如果是审批界面,需要加密处理

                        tryAutoPlay(loadpage, function () {
                            $(".inquireMask").show();
                            $(".inquireTitle").html("开始审批");
                            $(".inquireRightBtn").html("确定").show();
                            $(".inquireRightBtn").click(function () {
                                loadpage();
                            })
                        });
                        function loadpage() {
                            $.ajax({
                                url: "/LearningCenter/GetUnitPage",
                                type: "post",
                                dataType: "json",
                                data: {
                                    unitid: _this.startPageNum,
                                    isshow: _this.lastTimeIsOver,
                                    jy: _this.progressKey
                                },
                                async: false,
                                success: function (data) {
                                    if (data.ErrorCode == 0) {
                                        var page = data.Data;
                                        console.log("page", page);
                                        function showPage() {
                                            $(".inquireMask").hide();
                                            $(".approveover").hide();
                                            _this.pages = [];
                                            var newPage = new Page(page);
                                            _this.addPage(newPage);
                                            newPage.callBack = function () {
                                                var _chapter = this.parent;
                                                $('.CurriculumPage').remove();
                                                $(".approveover").show();
                                            }
                                            newPage.doWork();
                                        }
                                        showPage();
                                        $(".approveover button").click(showPage);
                                    }
                                    else {

                                    }
                                },
                                error: function (e) {

                                }
                            });
                        }

                    }
                    else if (!this.lastTimeIsOver) {
                        $(".inquireLeftBtn").show();
                        $(".inquireRightBtn").show();
                        $(".inquireTitle").html("是否继续上次进度学习？");
                        $(".inquireRightBtn").click(function () {
                            startLearn();
                        })
                        $(".inquireLeftBtn").click(function () {
                            $.ajax({
                                url: "/LearningCenter/NewProgress",
                                type: "post",
                                data: {
                                    courseId: _this.courseId,
                                    lessonId: _this.lessonId
                                },
                                success: function (data) {
                                    if (data.ErrorCode == 0) {
                                        var data = data.Data;
                                        _this.isApprove = data.isApprove;
                                        _this.lastTimeIsOver = data.lastTimeIsOver;
                                        _this.startAddGoldsPageNum = data.startAddGoldsPageNum;
                                        _this.startPageNum = data.startPageNum;
                                        _this.lessonProgressId = data.lessonProgressId;
                                        _this.progressKey = data.progressKey;
                                        startLearn();
                                    }
                                    else {
                                        console.error("获取数据错误")
                                    }
                                }
                            });
                        })
                    }
                    else {
                        $(".inquireTitle").html("开始学习");
                        $(".inquireRightBtn").html("确定").show();
                        $(".inquireRightBtn").click(function () {
                            startLearn();
                        })
                    }
                    function startLearn() {
                        $(".inquireMask").hide();
                        _this.loadPage(_this.startPageNum, 5, function () {
                            if (_this.pages.length > 0) {
                                _this.pages[0].doWork();
                            }
                        });
                    }
                }
            },
            //回调函数
            callBack: {
                writable: true,
                value: function callBack() {
                    console.log("课时结束");
                }
            },
            //数据提交完成回调函数
            dataCallBack: {
                writable: true,
                value: function dataCallBack() {
                    console.log("所有课时数据提交结束");
                }
            },
            //错误弹框
            errorMsg: {
                writable: true,
                value: function errorMsg(errmsg) {
                    $(".errorMask .errorTitle").html(errmsg);
                    $(".errorMask").show();
                    $(".errorBtn").off("click");
                    $(".errorBtn").on("click", function () {
                        $(".errorMask .errorTitle").html("");
                        $(".errorMask").hide();
                    })
                }
            },
        });
        if (arguments.length > 0 && arguments[0]) {
            var data = arguments[0];
            if (typeof (data.index) != "undefined") {
                this.index = data.index;
            }
            if (typeof (data.id) != "undefined") {
                this.id = data.id;
            }
            if (typeof (data.name) != "undefined") {
                this.name = data.name;
            }
            if (typeof (data.setTime) != "undefined") {
                this.setTime = data.setTime;
            }
            if (typeof (data.status) != "undefined") {
                this.status = data.status;
            }
            if (typeof (data.isApprove) != "undefined") {
                this.isApprove = data.isApprove;
            }
            if (typeof (data.lastTimeIsOver) != "undefined") {
                this.lastTimeIsOver = data.lastTimeIsOver;
            }
            if (typeof (data.startAddGoldsPageNum) != "undefined") {
                this.startAddGoldsPageNum = data.startAddGoldsPageNum;
            }
            if (typeof (data.startPageNum) != "undefined") {
                this.startPageNum = data.startPageNum;
            }
            if (typeof (data.totalPageNum) != "undefined") {
                this.totalPageNum = data.totalPageNum;
            }
            if (typeof (data.courseId) != "undefined") {
                this.courseId = data.courseId;
            }
            if (typeof (data.lessonId) != "undefined") {
                this.lessonId = data.lessonId;
            }
            if (typeof (data.lessonProgressId) != "undefined") {
                this.lessonProgressId = data.lessonProgressId;
            }
            if (typeof (data.lessonIndex) != "undefined") {
                this.lessonIndex = data.lessonIndex;
            }
            if (typeof (data.progressKey) != "undefined") {
                this.progressKey = data.progressKey;
            }
            if (typeof (data.pages) != "undefined") {
                for (var p in data.pages) {
                    var page = new Page(data.pages[p]);
                    this.addPage(page);
                }
            }
        }
    }
    //讲义页类
    function Page() {
        var page = 0, pageId = 0, pageName = "", maxZindex = 0, coinsKey = "", title = new TextBox(), xiaoAiImg = new ImgBox(), background = "", steps = [], submitData = {
            "coins": [], "answers": [], "useTime": 0
        };
        var titleseted = false;//标题是否设置过
        var xiaoAiSeted = false;
        var searchUrl = "/Subject/GetSubjectOfLearning";
        var doc = document.createElement("div");
        doc.classList.add("CurriculumPage");
        var box = document.createElement("div");
        box.classList.add("CurriculumPageBox");
        box.style.height = document.body.offsetHeight + "px";
        box.style.width = document.body.offsetHeight * 800 / 600 + "px";
        doc.appendChild(box);
        //设置属性
        Object.defineProperties(this, {
            //页码
            pageNum: {
                enumerable: true,
                get: function () {
                    return page;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        page = val;
                    }
                    else {
                        console.error("页码必须为数字类型");
                    }
                }
            },
            //页码Id
            pageId: {
                enumerable: true,
                get: function () {
                    return pageId;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        pageId = val;
                    }
                    else {
                        console.error("页码必须为数字类型");
                    }
                }
            },
            //每页完成提交服务端数据
            submitData: {
                writable: true,
                value: submitData
            },
            //金币秘钥
            coinsKey: {
                writable: true,
                value: coinsKey
            },
            //讲义名称
            pageName: {
                writable: true,
                value: pageName
            },
            //标题
            title: {
                value: title
            },
            //小艾图片
            xiaoAiImg: {
                value: xiaoAiImg
            },
            //背景图
            background: {
                get: function () {
                    return background;
                },
                set: function (v) {
                    background = v;
                }
            },
            //步骤列表副本
            steps: {
                enumerable: true,
                get: function () {
                    return steps.concat([]);
                }
            },
            //添加步骤
            addStep: {
                value: function addStep(step) {
                    if (step instanceof Step) {
                        step.stepNum = steps.length;
                        Object.defineProperty(step, "parent", {
                            value: this
                        })
                        steps.push(step);
                    }
                    else {
                        console.error("添加的步骤类型错误,应为 Step 对象");
                    }
                }
            },
            //移动步骤
            moveStep: {
                value: function moveStep(i, t) {
                    if (!(typeof (i) == "number" && 0 <= i && i < steps.length)) {
                        console.error("章节下标选择错误");
                    }
                    if (!(typeof (t) == "number" && 0 <= t && t < steps.length)) {
                        console.error("移动至下表选择错误");
                    }
                    var s = steps.splice(i, 1)[0];//获取自己并从数组中移出
                    steps.splice(t, 0, s);//在插入到对应位置中去
                    for (var i in steps)//遍历后从新设置页码
                    {
                        steps[i].stepNum = parseInt(i);
                    }
                }
            },
            //删除步骤
            deleteStep: {
                value: function deleteStep(i) {
                    if (!(typeof (i) == "number" && 0 <= i && i < steps.length)) {
                        console.error("步骤下标选择错误");
                    }
                    var self = steps.splice(i, 1)[0];//获取自己并从数组中移出并返回
                    for (var i in steps) {//重新设置顺序
                        steps[i].stepNum = parseInt(i);
                    }
                    return self;
                }
            },
            //获取步骤
            getStep: {
                value: function getStep(index) {
                    if (typeof (index) == "number" && index < steps.length) {
                        return steps[index];
                    }
                    else {
                        console.error("获取步骤失败,请查看下标是否正确");
                    }
                }
            },
            //判断是否可以加金币
            canNotAddGolds: {
                get: function () {
                    if (this.parent && this.parent instanceof Chapter) {
                        return this.pageNum < this.parent.startAddGoldsPageNum;
                    }
                    return false;
                }
            },
            //设置标题
            setTitle: {
                value: function setTitle(_this, id, text, size, color, align, x, y, intype) {
                    $(".backToParent").show();
                    if (!titleseted) {
                        var _page = this;
                        _page.title.text = text;
                        _page.title.size = size;
                        _page.title.color = color;
                        _page.title.position.align = align;
                        _page.title.position.x = x;
                        _page.title.position.y = y;
                        _page.title.intype = intype;
                        _page.title.id = id;
                        var doc = _page.title.doc;
                        doc.style.zIndex = (maxZindex++);
                        box.appendChild(doc);
                        if (intype == "none") {
                            _this.callBack();
                        }
                        else {
                            $(doc).on("animationend", function (e) {
                                if (e.currentTarget == doc && " bounceInDown bounceInLeft bounceInRight bounceInUp ".indexOf(e.originalEvent.animationName) > 0 && !_this.isover) {
                                    _this.isover = true;
                                    $(doc).removeClass('bounceInDown bounceInLeft bounceInRight bounceInUp');
                                    _this.callBack();
                                }
                            });
                        }
                        titleseted = true;
                    }
                    else {
                        console.warn("标题只允许设置一次");
                    }
                }
            },
            //设置背景
            setBackground: {
                value: function setBackground(bgtype, bg) {
                    //                  this.background = bg;
                    $(".backToParent").show();
                    if (bgtype == "image") {
                        doc.style.backgroundImage = "url(" + bg + ")";
                        doc.style.backgroundColor = "";
                    } else {
                        doc.style.backgroundColor = bg;
                        doc.style.backgroundImage = "";
                    }
                }
            },
            //插入图片
            insertImg: {
                value: function insertImg(_this, img) {
                    $(".backToParent").show();
                    if (img) {
                        img.style.zIndex = (maxZindex++);
                        box.appendChild(img);
                        if (_this.intype == "none") {
                            _this.callBack();
                        }
                        else {
                            $(img).on("animationend", function (e) {
                                if (e.currentTarget == img && " bounceInDown bounceInLeft bounceInRight bounceInUp ".indexOf(e.originalEvent.animationName) > 0 && !_this.isover) {
                                    _this.isover = true;
                                    $(img).removeClass('bounceInDown bounceInLeft bounceInRight bounceInUp');
                                    _this.callBack();
                                }
                            });
                        }
                    }
                    else {
                        console.error("插入图片必须是图片标签");
                    }
                }
            },
            //插入文字
            insertText: {
                value: function insertText(_this, text) {
                    $(".backToParent").show();
                    if (text) {
                        text.style.zInde = (maxZindex++);
                        box.appendChild(text);
                        if (_this.intype == "none") {
                            _this.callBack();
                        } else {
                            $(text).on("animationend", function (e) {
                                if (e.currentTarget == text && " bounceInDown bounceInLeft bounceInRight bounceInUp ".indexOf(e.originalEvent.animationName) > 0 && !_this.isover) {
                                    _this.isover = true;
                                    $(text).removeClass('bounceInDown bounceInLeft bounceInRight bounceInUp');
                                    _this.callBack();
                                }
                            });
                        }
                    }
                    else {
                        console.error("插入文字必须是P标签");
                    }
                }
            },
            //小艾说
            xiaoAiSay: {
                value: function scaleDom(_this, src) {
                    $(".backToParent").show();
                    var newAudio = document.createElement("audio");
                    newAudio.src = src;
                    newAudio.oncanplay = function () {
                        newAudio.play();
                        var f = function () {
                            _this.callBack();
                            newAudio.removeEventListener('ended', f);
                        }
                        newAudio.addEventListener('ended', f, false);
                    }
                }
            },
            //小艾变
            xiaoAiChange: {
                value: function xiaoAiChange(imgId, src, width, height, align, valign, x, y) {
                    $(".backToParent").show();

                    if (!xiaoAiSeted) {
                        this.xiaoAiImg.id = 'xiaoAiChangeImg';
                        this.xiaoAiImg.imgId = imgId;
                        this.xiaoAiImg.src = src;
                        this.xiaoAiImg.width = width;
                        this.xiaoAiImg.height = height;
                        this.xiaoAiImg.position.align = align;
                        this.xiaoAiImg.position.valign = valign;
                        this.xiaoAiImg.position.x = x;
                        this.xiaoAiImg.position.y = y;
                        this.xiaoAiImg.intype = "none";
                        this.xiaoAiImg.doc.style.zIndex = (maxZindex++);
                        box.appendChild(this.xiaoAiImg.doc);
                        xiaoAiSeted = true;
                    } else {
                        //$('#xiaoAiChangeImg img').attr("src", src);
                        var oldimg = $("#xiaoAiChangeImg img");
                        var newimg = document.createElement("img");
                        newimg.src = src;
                        newimg.onload = function () {
                            $("#xiaoAiChangeImg").append(newimg);
                            $(oldimg).remove();
                            $('#xiaoAiChangeImg').css({
                                "width": width * zoom,
                                "height": height * zoom,
                                "left": x * zoom,
                                'top': y * zoom
                            });
                        }
                    }
                }
            },
            //缩放元素
            scaleDom: {
                value: function scaleDom(_this, objectId, duration, ratio, num) {
                    $(".backToParent").show();
                    if (!_this.isover) {
                        var obj = $('#' + objectId);
                        var str = "scaleDom" + objectId;
                        var runkeyframes = "." + str + "{z-index:999;animation:" + str + " " + (duration / 1000) + "s linear 0s " + num + " normal;-webkit-animation:" + str + " " + (duration / 1000) + "s linear 0s " + num + " normal;} @keyframes " + str + " {0% { -webkit-transform: scale(1);transform: scale(1);} 50% { -webkit-transform: scale(" + ratio + ");transform: scale(" + ratio + ");} 100% { -webkit-transform: scale(1);transform: scale(1);}}"
                        // 创建style标签
                        var styles = document.createElement('style');
                        // 设置style属性
                        styles.type = 'text/css';
                        // 将 keyframes样式写入style内
                        styles.innerHTML = runkeyframes;
                        // 将style样式存放到head标签
                        $('head').append(styles);
                        obj.removeClass("animated").addClass(str);

                        obj.on("animationend", function (e) {
                            if (e.currentTarget == obj[0] && e.originalEvent.animationName == str && !_this.isover) {

                                _this.isover = true;
                                removeDom(styles);
                                obj.removeClass(str);
                                $('#' + objectId).css("z-index", (maxZindex++));
                                _this.callBack();
                            }
                        });
                    }
                }
            },
            //移动元素
            moveDom: {
                value: function moveDom(_this, objectId, duration, x, y) {
                    $(".backToParent").show();

                    $('#' + objectId).css("z-index", 999);
                    $('#' + objectId).removeClass('bounceInDown bounceInLeft bounceInRight bounceInUp');
                    $('#' + objectId).animate({
                        left: x * zoom, top: y * zoom
                    }, duration, function () {
                        $('#' + objectId).css("z-index", (maxZindex++));
                        _this.callBack();
                    })
                }
            },
            //闪烁元素
            twinkleDom: {
                value: function twinkleDom(_this, objectId, duration, num) {
                    $(".backToParent").show();


                    var obj = $('#' + objectId);
                    var n = 0;
                    var t = duration / 2;
                    var f = function () {

                        if (n++ < num) {
                            obj.animate({
                                "opacity": "0"
                            },
							t,
							function () {
							    obj.animate({
							        "opacity": "1"
							    },
								t,
								f
								)
							})
                        }
                        else {

                            _this.callBack();
                        }

                    };
                    f();

                }
            },
            //隐藏元素
            hideDom: {
                value: function hideDom(_this, objectId, outtype) {
                    $(".backToParent").show();
                    if (!_this.isover) {
                        var outClass = "";
                        switch (outtype) {
                            case "none":
                                $('#' + objectId).hide();
                                _this.isover = true;
                                _this.callBack();
                                return;
                            case "top":
                                outClass = "bounceOutUp";
                                break;
                            case "bottom":
                                outClass = "bounceOutDown";
                                break;
                            case "left":
                                outClass = "bounceOutLeft";
                                break;
                            case "right":
                                outClass = "bounceOutRight";
                                break;
                            default: console.error("飞出状态应为 none/top/bottom/left/right 五种效果"); return;
                        }
                        var dom = $('#' + objectId);
                        dom.addClass(outClass);
                        dom.on("animationend", function (e) {
                            if (e.currentTarget == dom[0] && outClass == e.originalEvent.animationName && !_this.isover) {
                                _this.isover = true;
                                dom.removeClass(outClass);
                                dom.hide();
                                _this.callBack();
                            }
                        })
                    }
                }
            },
            //学习音频
            studyAudio: {
                value: function studyAudio(_this, kcb, audios) {
                    $(".backToParent").hide();
                    var _page = this;
                    if (_page.canNotAddGolds) {//如果此页不加金币,则动作的金币数改为0
                        _this.goldCoins = 0;
                    }
                    var datas = {
                        _this: _this,
                        kcb: kcb
                    };
                    var html = template('studyAudioScriptDiv', datas);
                    $(".maskContent").html(html);
                    $(".mask").show();
                    var W1 = $(".learnCenterBgImg").height() * 1431 / 728 + "px";
                    $(".learnCenterBox").css("width", W1);
                    $(".maskContent").append(audios.audio)
                    if (_page.parent.isApprove) {
                        audios.audio.setAttribute("controls", true);
                        audios.audio.classList.add("audio2");
                    }
                    if (_this.kcbSrc !== "") {//如果有开场白,则执行开场白
                        var f = function () {
                            audios.audio.play();
                            kcb.audio.removeEventListener('ended', f);
                        }
                        kcb.audio.addEventListener('ended', f, false);
                        kcb.audio.play();

                    } else {
                        audios.audio.play();
                    }
                    if (_this.audioTextSrc !== undefined) {
                        var tops = $(".readContent").css("top").split("px")[0];
                    }
                    audios.audio.ontimeupdate = function (e) {
                        if (this.ended) {
                            $(".mask").hide();
                            $(".maskContent").html("");
                            var thisData = {
                            };
                            thisData.acid = _this.actionId;
                            thisData.type = _this.type;
                            thisData.acin = _this.goldCoins;
                            thisData.gcin = _this.goldCoins;
                            _page.submitData.coins.push(thisData);
                            _this.callBack();
                        }

                        if (_this.audioTextSrc !== undefined) {
                            for (var t = 0; t < _this.audioTextSrc.length; t++) {
                                if (this.currentTime >= _this.audioTextSrc[t][0] - 1) {
                                    $('p[name=lyric]').css('color', '#8e5f19');
                                    $('#p' + t).css('color', '#76bdb7');
                                    $('.readContent').css('top', tops - 40 * t);
                                }
                            }
                        }
                        if (_this.audioImgSrc !== "") {
                            $(".audioImgBox").addClass("rotates")
                        }
                    }
                }
            },
            //学习视频
            studyVideo: {
                //_action:动作对象
                //kcb:动作开场白,为null则没有开场白
                value: function studyVideo(_this, kcb) {
                    $(".backToParent").hide();
                    var _page = this;
                    if (_page.canNotAddGolds) {//如果此页不加金币,则动作的金币数改为0
                        _this.goldCoins = 0;
                    }
                    var datas = {
                        _this: _this,
                        kcb: kcb
                    };
                    var html = template('studyVideoScriptDiv', datas);
                    $(".maskContent").html(html);
                    $(".mask").show();
                    if (_page.parent.isApprove) {
                        $(".videos")[0].setAttribute("controls", true);
                    }
                    $(".videos")[0].setAttribute("webkit-playsinline", true);
                    $(".videos")[0].setAttribute("playsinline", true);
                    var H = $(".videoBox").width() * 9 / 16 + "px";
                    $(".videoBox").height(H);
                    var isOver = false;
                    if (_this.kcbSrc !== "") {//如果有开场白,则执行开场白
                        var f = function () {
                            isOver = true;
                            $(".videos")[0].play();
                            $(".playVideo").addClass("active");
                            kcb.audio.removeEventListener('ended', f);
                        }
                        kcb.audio.addEventListener('ended', f, false);
                        kcb.audio.play();

                    } else {
                        isOver = true;
                        $(".videos")[0].play();
                    }
                    $(".playVideo").click(function () {
                        if (isOver) {
                            if ($(this).hasClass("active")) {
                                $(".videos")[0].pause();
                                _page.submitData.useTime += ((new Date()).getTime() - _page.parent.startTime);
                                $(".playVideo").removeClass("active");
                            } else {
                                $(".videos")[0].play();
                                _page.parent.startTime = (new Date()).getTime();
                                $(".playVideo").addClass("active");
                            }
                        }
                    })
                    $(".videos")[0].addEventListener('ended', function () {

                        $(".mask").hide();
                        $(".maskContent").html("");
                        var thisData = {
                        };
                        thisData.acid = _this.actionId;
                        thisData.type = _this.type;
                        thisData.acin = _this.goldCoins;
                        thisData.gcin = _this.goldCoins;
                        _page.submitData.coins.push(thisData);
                        _this.callBack();
                    });
                }

            },
            //学习图文
            studyArticle: {
                value: function studyArticle(_this, kcb) {
                    $(".backToParent").hide();
                    var _page = this;
                    if (_page.canNotAddGolds) {//如果此页不加金币,则动作的金币数改为0
                        _this.goldCoins = 0;
                    }
                    var datas = {
                        _this: _this,
                        kcb: kcb
                    };
                    var html = template('studyArticleScriptDiv', datas);
                    $(".maskContent").html(html);
                    $(".mask").show();
                    var W1 = $(".learnCenterBgImg").height() * 1431 / 728 + "px";
                    $(".learnCenterBox").css("width", W1);
                    var W2 = $(".articleImgBox").height() + "px";
                    $(".articleImgBox").css("width", W2);

                    if (_this.kcbSrc !== "") {//如果有开场白,则执行开场白
                        kcb.audio.play();
                    }
                    var articleStudyTime = setTimeout(function () {
                        $(".mask").hide();
                        $(".maskContent").html("");
                        var thisData = {
                        };
                        thisData.acid = _this.actionId;
                        thisData.type = _this.type;
                        thisData.acin = _this.goldCoins;
                        thisData.gcin = _this.goldCoins;
                        _page.submitData.coins.push(thisData);
                        _this.callBack();
                    }, _this.studyTime)
                    $(".subMit").click(function () {
                        clearTimeout(articleStudyTime);
                        $(".mask").hide();
                        $(".maskContent").html("");
                        var thisData = {
                        };
                        thisData.acid = _this.actionId;
                        thisData.type = _this.type;
                        thisData.acin = _this.goldCoins;
                        thisData.gcin = _this.goldCoins;
                        _page.submitData.coins.push(thisData);
                        _this.callBack();
                    })
                }
            },
            //学习朗读
            studyRecitation: {
                //_action:动作对象
                //kcb:动作开场白,为null则没有开场白
                value: function studyRecitation(_this, kcb) {
                    $(".backToParent").hide();
                    var _action = _this;
                    var _page = this;
                    var _chapter = _page.parent;
                    if (_page.canNotAddGolds) {//如果此页不加金币,则动作的金币数改为0
                        _this.goldCoins = 0;
                    }
                    var datas = {
                        _this: _this,
                        kcb: kcb
                    };
                    var html = template('studyRecitationScriptDiv', datas);
                    $(".maskContent").html(html);
                    $(".mask").show();
                    var W1 = $(".learnCenterBgImg").height() * 1431 / 728 + "px";
                    $(".learnCenterBox").css("width", W1);

                    if (_this.kcbSrc !== "") {//如果有开场白,则执行开场白
                        kcb.audio.play();
                    }

                    var cando = true;
                    var fen = 0, audioTimeOut = null;
                    var recorder = null;
                    if (!_chapter.isApprove) {//如果不是审批界面,加载录音项
                        if (_chapter.Html5Recorder && _chapter.Html5Recorder instanceof Html5Recorder) {
                            recorder = _chapter.Html5Recorder;
                        }
                        else {
                            recorder = _chapter.Html5Recorder = new Html5Recorder({
                                server: 'wss://cloud.chivox.com',
                                appKey: '155891795800000f', //填写驰声授权评测的appkey
                                sigurl: '/StudyTask/GetSig',
                                onInit: function (mess) {
                                    console.log("onInit success");
                                },
                                onError: function (err) {
                                    console.log("onError:", err);
                                    _chapter.errorMsg("请检查话筒是否正常接入后刷新页面重试.");
                                }
                            });
                        }
                    }
                    var refText = $("#hideText").text()//.replace(/\s/g, "");//获取驰声需要朗读文本内容
                    //开始录制
                    $(".startLY").click(function () {
                        kcb.audio.pause();
                        if (!$(this).hasClass("active")) {
                            if (_chapter.isApprove) {
                                $(".startLY").addClass("active");
                                $(".doneLY").addClass("active");
                            }
                            else {
                                if (recorder && cando) {
                                    recorder.record({
                                        duration: 60 * 1000,
                                        audioType: "mp3",
                                        serverParams: {
                                            coreType: "cn.pred.raw",
                                            precision: 1,
                                            //res: "chn.snt.G4",
                                            refText: refText,
                                            rank: 100,
                                            userId: "tester",
                                            attachAudioUrl: 1
                                        },
                                        onRecordIdGenerated: function (tokenId) {
                                            console.log("=============onRecordIdGenerated start=============");
                                            console.log(JSON.stringify(tokenId));
                                            console.log("=============onRecordIdGenerated end=============");
                                        },
                                        onStart: function () {
                                            console.log("开始录音");
                                            $(".startLY").addClass("active");
                                            $(".doneLY").addClass("active");

                                        },
                                        onStop: function () {
                                            console.log("结束录音", new Date().getTime(), arguments);
                                            cando = false;
                                            $(".startLY").removeClass("active").addClass("contRecord");
                                            $(".doneLY").removeClass("active");
                                            $(".doneLY").addClass("uploading");

                                        },
                                        onScore: function (score) {
                                            console.log("录音结果", new Date().getTime(), score);
                                            if (typeof (score.result.overall) != "number") { score.error = "总分返回错误"; }
                                            if (!score.error) {
                                                $.ajax({
                                                    url: "/LearningCenter/SaveAndCalculateRankPercent",
                                                    type: "post",
                                                    dataType: "json",
                                                    data: {
                                                        courseId: _chapter.courseId, lessonId: _chapter.lessonId, unitId: _page.pageId, actionId: _action.actionId, score: score.result.overall, url: score.audioUrl
                                                    },
                                                    async: false,
                                                    success: function (result) {
                                                        let scores = {
                                                            phn: score.result.phn,//声韵得分
                                                            tone: score.result.tone, //声调得分
                                                            fluency: score.result.fluency.overall, //流利度
                                                            total: score.result.overall,//总分
                                                            chao: result.Data
                                                        };
                                                        fen = scores.total;
                                                        upload(scores);
                                                        cando = true;
                                                    },
                                                    error: function (e) {
                                                        upload();
                                                        cando = true;
                                                    }
                                                });
                                            }
                                            else {
                                                upload();
                                                cando = true;
                                            }
                                        },
                                        onScoreError: function (err) {
                                            console.log("录音失败", new Date().getTime(), err);
                                            upload();
                                            cando = true;
                                        }
                                    })
                                }
                            }
                        }
                    })
                    //完成录制
                    $(".doneLY").click(function () {
                        if ($(this).hasClass("active")) {
                            if (_chapter.isApprove) {
                                $(".startLY").removeClass("active");
                                $(".doneLY").removeClass("active");
                                $(".doneLY").addClass("uploading");
                                let scores = {
                                    phn: 100,//声韵得分
                                    tone: 100, //声调得分
                                    fluency: 100, //流利度
                                    total: 100,//总分
                                    chao: 100
                                };
                                fen = scores.total;
                                upload(scores);
                            }
                            else {
                                if (recorder) {
                                    recorder.stopRecord();
                                }
                            }
                        }
                    })
                    //重新录制
                    $(".TodosRevoke").click(function () {
                        $(".startLY").removeClass("active").removeClass("contRecord");
                        $(".doneLY").removeClass("active");
                        $(".learnCenterBox").show();
                        $(".learnCenter_Recitation").hide();
                        if (!_chapter.isApprove) {
                            if (recorder) {
                                recorder.reset();
                            }
                        }
                    })


                    function Submint() {
                        $(".mask").hide();
                        $(".maskContent").html("");
                        var thisData = {
                        };
                        thisData.acid = _this.actionId;
                        thisData.type = _this.type;
                        thisData.acin = _this.goldCoins;
                        thisData.gcin = _this.goldCoins > 0 ? Math.ceil(_this.goldCoins * fen / 100) : 0;
                        _page.submitData.coins.push(thisData);
                        _this.callBack();
                    }
                    //朗读环节结束
                    $(".TodosSubmit").click(function () {
                        Submint();
                    })

                    function upload(scores) {
                        console.log("分数", scores);

                        if (!scores) {
                            scores = {
                                phn: 0,//声韵得分
                                tone: 0, //声调得分
                                fluency: 0, //流利度
                                total: 0,//总分
                                chao: 0
                            };
                            fen = scores.total;
                        }
                        $("#chaoyue").html(scores.chao + "%");
                        $("#scoreNum").html(scores.total);

                        //上传成功后改变自己状态
                        $(".doneLY").removeClass("uploading");
                        $(".learnCenterBox").hide();
                        $(".learnCenter_Recitation").show();
                        $(".learnCenter_Recitation").css("width", W1);

                        var scoreDistribution = echarts.init(document.getElementById('scoreDistribution'));
                        var xAxisData = ['完整度', '声调', '流畅度'];
                        var yAxisData = [scores.phn, scores.tone, scores.fluency];
                        var option = {
                            grid: {
                                left: '20',
                                right: '20',
                                bottom: '30',
                                top: '30',
                                containLabel: true
                            },
                            xAxis: [{
                                type: 'category',
                                data: xAxisData,
                                axisLabel: {
                                    color: "#8a8a8a",
                                    fontSize: 16,
                                    margin: 10,
                                    interval: 0
                                },
                                axisLine: {
                                    lineStyle: {
                                        color: '#d4d4d4'
                                    }
                                },
                                splitLine: {
                                    show: false
                                }
                            }],
                            yAxis: {
                                type: 'value',
                                max: 100,
                                axisLabel: {
                                    textStyle: {
                                        fontSize: 15,
                                        color: '#8a8a8a'
                                    }
                                },
                                axisLine: {
                                    lineStyle: {
                                        color: '#d4d4d4'
                                    }
                                },
                                splitLine: {
                                    show: false
                                }
                            },
                            series: [{
                                name: '得分',
                                type: 'bar',
                                data: yAxisData,
                                barWidth: 30,
                                itemStyle: {
                                    normal: {
                                        color: function (params) {
                                            var colorListr = ["#99cace", "#d5bb8a", "#97d39f", "#cf9b8e"];
                                            return colorListr[params.dataIndex]
                                        },
                                        label: {
                                            show: true,
                                            position: 'top',
                                            formatter: '{c}分'
                                        }
                                    }
                                }
                            }]
                        };
                        scoreDistribution.setOption(option);


                    }
                }

            },
            //快速阅读-简单模式
            studyFastReadEasy: {
                value: function studyFastReadEasy(_this, kcb) {
                    $(".backToParent").hide();
                    var _page = this;
                    if (_page.canNotAddGolds) {//如果此页不加金币,则动作的金币数改为0
                        _this.goldCoins = 0;
                    }
                    var datas = {
                        _this: _this,
                        kcb: kcb
                    };
                    var html = template('studyFastReadEasyScriptDiv', datas);
                    $(".maskContent").html(html);
                    $(".mask").show();
                    var W1 = $(".learnCenterBgImg").height() * 1431 / 728 + "px";
                    $(".learnCenterBox").css("width", W1);
                    if (_this.kcbSrc !== "") {//如果有开场白,则执行开场白
                        var f = function () {
                            playFastReadEasy()
                            kcb.audio.removeEventListener('ended', f);
                        }
                        kcb.audio.addEventListener('ended', f, false);
                        kcb.audio.play();
                    } else {
                        playFastReadEasy()
                    }
                    function playFastReadEasy() {
                        var k = 0;
                        var num = $('.fastReadEasyReadList').length;
                        var time1 = setInterval(function () {
                            $('.fastReadEasyReadList').removeClass("active").find(".fastReadEasyReadListContent").html("")
                            $('.fastReadEasyReadList').eq(k).addClass("active").find(".fastReadEasyReadListContent").html(_this.fastReadText.slice(k * _this.showNum, (k + 1) * _this.showNum));
                            k++;
                            if (k == (num + 1)) {
                                clearInterval(time1);
                                $(".mask").hide();
                                $(".maskContent").html("");
                                var thisData = {
                                };
                                thisData.acid = _this.actionId;
                                thisData.type = _this.type;
                                thisData.acin = _this.goldCoins;
                                thisData.gcin = _this.goldCoins;
                                _page.submitData.coins.push(thisData);
                                _this.callBack();
                            }
                        }, _this.speed)
                    }
                }
            },
            //快速阅读
            studyFastRead: {
                value: function studyFastRead(_this, kcb) {
                    $(".backToParent").hide();
                    var _page = this;
                    if (_page.canNotAddGolds) {//如果此页不加金币,则动作的金币数改为0
                        _this.goldCoins = 0;
                    }
                    var datas = {
                        _this: _this,
                        kcb: kcb
                    };
                    var html = template('studyFastReadScriptDiv', datas);
                    $(".maskContent").html(html);
                    $(".mask").show();
                    var W1 = $(".learnCenterBgImg").height() * 1431 / 728 + "px";
                    $(".learnCenterBox").css("width", W1);
                    if (_this.kcbSrc !== "") {//如果有开场白,则执行开场白
                        var f = function () {
                            playFastRead()
                            kcb.audio.removeEventListener('ended', f);
                        }
                        kcb.audio.addEventListener('ended', f, false);
                        kcb.audio.play();
                    } else {
                        playFastRead()
                    }
                    function playFastRead() {
                        var k = 0;
                        var num = $(".fastReadPlayBox .fastReadList").length;
                        var H = $(".fastReadPlayBox .fastReadList").height() + "px";
                        var time1 = setInterval(function () {
                            if (_this.showModel == "2") {//整行展示
                                if (k % 4 == 0) {
                                    $(".fastReadPlayBox .fastReadList").eq(k).css({
                                        "top": "0",
                                        "lineHeight": H
                                    }).show().siblings().hide();

                                } else if (k % 4 == 1) {
                                    $(".fastReadPlayBox .fastReadList").eq(k).css({
                                        "top": "25%",
                                        "lineHeight": H
                                    }).show().siblings().hide();
                                } else if (k % 4 == 2) {
                                    $(".fastReadPlayBox .fastReadList").eq(k).css({
                                        "top": "50%",
                                        "lineHeight": H
                                    }).show().siblings().hide();
                                } else if (k % 4 == 3) {
                                    $(".fastReadPlayBox .fastReadList").eq(k).css({
                                        "top": "75%",
                                        "lineHeight": H
                                    }).show().siblings().hide();
                                }
                            } else if (_this.showModel == "1") {//半行展示
                                if (k % 8 == 0) {
                                    $(".fastReadPlayBox .fastReadList").eq(k).css({
                                        "top": "0",
                                        "left": "0",
                                        "lineHeight": H
                                    }).show().siblings().hide();

                                } else if (k % 8 == 1) {
                                    $(".fastReadPlayBox .fastReadList").eq(k).css({
                                        "top": "0",
                                        "left": "50%",
                                        "lineHeight": H
                                    }).show().siblings().hide();
                                } else if (k % 8 == 2) {
                                    $(".fastReadPlayBox .fastReadList").eq(k).css({
                                        "top": "25%",
                                        "left": "0",
                                        "lineHeight": H
                                    }).show().siblings().hide();
                                } else if (k % 8 == 3) {
                                    $(".fastReadPlayBox .fastReadList").eq(k).css({
                                        "top": "25%",
                                        "left": "50%",
                                        "lineHeight": H
                                    }).show().siblings().hide();
                                } else if (k % 8 == 4) {
                                    $(".fastReadPlayBox .fastReadList").eq(k).css({
                                        "top": "50%",
                                        "left": "0",
                                        "lineHeight": H
                                    }).show().siblings().hide();
                                } else if (k % 8 == 5) {
                                    $(".fastReadPlayBox .fastReadList").eq(k).css({
                                        "top": "50%",
                                        "left": "50%",
                                        "lineHeight": H
                                    }).show().siblings().hide();
                                } else if (k % 8 == 6) {
                                    $(".fastReadPlayBox .fastReadList").eq(k).css({
                                        "top": "75%",
                                        "left": "0",
                                        "lineHeight": H
                                    }).show().siblings().hide();
                                } else if (k % 8 == 7) {
                                    $(".fastReadPlayBox .fastReadList").eq(k).css({
                                        "top": "75%",
                                        "left": "50%",
                                        "lineHeight": H
                                    }).show().siblings().hide();
                                }
                            }
                            k++;
                            if (k == (num + 1)) {
                                clearInterval(time1);
                                $(".mask").hide();
                                $(".maskContent").html("");
                                var thisData = {
                                };
                                thisData.acid = _this.actionId;
                                thisData.type = _this.type;
                                thisData.acin = _this.goldCoins;
                                thisData.gcin = _this.goldCoins;
                                _page.submitData.coins.push(thisData);
                                _this.callBack();
                            }
                        }, _this.speed)
                    }
                }
            },
            //判断题
            studyJudgment: {
                //_this:动作对象
                //kcb:动作开场白,为null则没有开场白
                value: function studyJudgment(_this, kcb) {
                    $(".backToParent").hide();
                    var _page = this;
                    if (_page.canNotAddGolds) {//如果此页不加金币,则动作的金币数改为0
                        _this.goldCoins = 0;
                    }
                    //动作执行内容
                    $.ajax({
                        type: "get",
                        url: searchUrl + "?r=" + Math.floor(Math.random() * 10000),
                        data: {
                            subjectId: _this.questionId
                        },
                        success: function (data) {
                            if (data.State) {
                                var data = data.Data;
                                var datas = {
                                    _this: _this,
                                    kcb: kcb,
                                    list: data
                                };
                                var html = template('studyJudgmentScriptDiv', datas);
                                $(".maskContent").html(html);
                                $(".mask").show();
                                var W1 = $(".learnCenterBgImg").height() * 1431 / 728 + "px";
                                $(".learnCenterBox").css("width", W1);
                                if (_this.kcbSrc !== "") {//如果有开场白,则执行开场白
                                    kcb.audio.play();
                                }
                                var isChoose = false;
                                $(".learnCenter_doneJudgmentOption .learnCenter_judgmentOptions").click(function () {
                                    isChoose = true;
                                    $(this).addClass('active').siblings().removeClass('active');
                                })
                                $(".subMit").click(function () {
                                    if (isChoose) {
                                        clearTimeout(setTimeoutCheckAnswer);
                                        checkJudgmentAnswer();
                                    }
                                })
                                var setTimeoutCheckAnswer = setTimeout(function () {
                                    checkJudgmentAnswer();
                                }, _this.studyTime)
                                function checkJudgmentAnswer() {
                                    var myanswer = $('.learnCenter_judgmentOptions.active').find(".signCont").html();
                                    var submitAnswer = $('.learnCenter_judgmentOptions.active').index();
                                    var answer = data.Answer;
                                    var allCount = 0;
                                    if (submitAnswer > -1) {
                                        if (submitAnswer == data.Answer) {
                                            allCount = 5;
                                            $(".learnCenter_AnswerjudgmentOption .learnCenter_judgmentOptions").eq(submitAnswer).addClass("right");
                                        } else {
                                            allCount = 1;
                                            $(".learnCenter_AnswerjudgmentOption .learnCenter_judgmentOptions").addClass("right");
                                            $(".learnCenter_AnswerjudgmentOption .learnCenter_judgmentOptions").eq(submitAnswer).addClass("wrong");
                                        }
                                    } else {
                                        //$(".learnCenter_AnswerjudgmentOption .learnCenter_judgmentOptions").eq(answer).addClass("right");
                                        allCount = 0;
                                    }
                                    $(".honorStatus").addClass("honorStatus" + allCount);
                                    if (allCount > 0) {
                                        for (var i = 0; i < allCount; i++) {
                                            $(".allStars").append("<span class='_starActive _starActive" + i + "'></span>");
                                        }
                                        var _starNum = 0;
                                        setInterval(function () {
                                            if (_starNum < allCount) {
                                                $(".allStars ._starActive").eq(_starNum).addClass("animation2")
                                                _starNum++;
                                            }

                                        }, 300)
                                    }

                                    if (answer == 0) {
                                        answer = "错误";
                                    } else if (answer == 1) {
                                        answer = "正确";
                                    }
                                    var getGoldNum = Math.round(_this.goldCoins * allCount / 5);
                                    $(".trueAnswerContent").html(answer);
                                    $('.AnalysisContent').html(data.Analysis);
                                    $(".learnCenterBox").hide();
                                    $(".checkAnswerBox").show();
                                    var H1 = $(".checkAnswerPlayBox")[0].scrollHeight, H2 = $(".checkAnswerPlayBox")[0].clientHeight;
                                    if (H1 > H2) {
                                        $(".ScrollDown").show();
                                    }
                                    $(".checkAnswerPlayBox").scroll(function () {
                                        SrollMonitor($(this));
                                    })
                                    $(".sureBtn").click(function () {
                                        submitJudgmentAnswers();
                                    })
                                    function submitJudgmentAnswers() {
                                        $(".mask").hide();
                                        $(".maskContent").html("");
                                        //生成金币结果
                                        var thisData = {
                                        };
                                        thisData.acid = _this.actionId;
                                        thisData.type = _this.type;
                                        thisData.acin = _this.goldCoins;
                                        thisData.gcin = getGoldNum;
                                        _page.submitData.coins.push(thisData);
                                        //生成答题结果
                                        var thisSubjectAnswerData = {
                                        };
                                        thisSubjectAnswerData.type = data.SubjectType;//题目类型 
                                        thisSubjectAnswerData.sbId = _this.questionId;//题目id
                                        thisSubjectAnswerData.rtar = allCount;//获得星数
                                        thisSubjectAnswerData.scon = _this.goldCoins;//题目金币数(非第一次学习,则为0)
                                        thisSubjectAnswerData.rcon = getGoldNum;//得到金币数
                                        thisSubjectAnswerData.kdge = data.KnowledgeId;//知识点ID
                                        thisSubjectAnswerData.sanw = submitAnswer;//答案
                                        _page.submitData.answers.push(thisSubjectAnswerData);
                                        _this.callBack();
                                    }
                                }
                            }

                        }
                    })

                }
            },
            //选择题
            studyOption: {
                //_action:动作对象
                //kcb:动作开场白,为null则没有开场白
                value: function studyOption(_this, kcb) {
                    $(".backToParent").hide();
                    var _page = this;
                    if (_page.canNotAddGolds) {//如果此页不加金币,则动作的金币数改为0
                        _this.goldCoins = 0;
                    }
                    //动作执行内容
                    $.ajax({
                        type: "get",
                        url: searchUrl + "?r=" + Math.floor(Math.random() * 10000),
                        data: {
                            subjectId: _this.questionId
                        },
                        success: function (data) {
                            if (data.State) {
                                var data = data.Data;
                                var sops = [];
                                for (var i = 0; i < data.Options.length; i++) {
                                    sops.push(data.Options[i].Key);
                                }
                                var datas = {
                                    _this: _this,
                                    kcb: kcb,
                                    list: data
                                };
                                var html = template('studyOptionScriptDiv', datas);
                                $(".maskContent").html(html);
                                $(".mask").show();
                                var W1 = $(".learnCenterBgImg").height() * 1431 / 728 + "px";
                                $(".learnCenterBox").css("width", W1);
                                if (_this.kcbSrc !== "") {//如果有开场白,则执行开场白
                                    kcb.audio.play();
                                }
                                var myanswer = [];
                                $(".learnCenter_OptionOption .learnCenter_OptionOptionList").click(function () {
                                    if (data.Answer.length > 1) {
                                        if ($(this).hasClass('active')) {
                                            $(this).removeClass('active');
                                            var index = myanswer.indexOf(Number($(this).attr("optionId")));
                                            if (index > -1) {
                                                myanswer.splice(index, 1);
                                            }
                                        } else {
                                            $(this).addClass('active');
                                            myanswer.push(Number($(this).attr("optionId")));
                                        }
                                    }
                                    else {
                                        if (!$(this).hasClass('active')) {
                                            myanswer = [];
                                            $(".learnCenter_OptionOption .learnCenter_OptionOptionList").removeClass("active");
                                            $(this).addClass('active');
                                            myanswer.push(Number($(this).attr("optionId")));
                                        }
                                    }

                                })

                                $(".subMit").click(function () {
                                    if (myanswer.length > 0) {
                                        clearTimeout(setTimeoutCheckAnswer);
                                        checkOptionAnswer();
                                    }
                                })

                                var setTimeoutCheckAnswer = setTimeout(function () {
                                    checkOptionAnswer();
                                }, _this.studyTime)
                                function checkOptionAnswer() {
                                    var answer = data.Answer.sort(sortNumber);
                                    myanswer.sort(sortNumber);
                                    var allCount = 0;

                                    if (myanswer.length > 0) {
                                        $('.learnCenter_OptionOptionList').each(function () {
                                            var optionId = Number($(this).attr("optionId"));
                                            if (myanswer.indexOf(optionId) > -1) {
                                                $(this).addClass('active');
                                            }
                                            if (answer.indexOf(optionId) > -1) {
                                                $(this).addClass('right');
                                            }
                                        });
                                        for (var a = 0; a < myanswer.length; a++) {
                                            if (answer.indexOf(myanswer[a]) > -1) {
                                                $('.learnCenter_OptionOptionList').each(function () {
                                                    var optionId = Number($(this).attr("optionId"));
                                                    if (optionId == myanswer[a]) {
                                                        $(this).addClass('right');
                                                    }
                                                });
                                            } else {
                                                $('.learnCenter_OptionOptionList').each(function () {
                                                    var optionId = Number($(this).attr("optionId"));
                                                    if (optionId == myanswer[a]) {
                                                        $(this).addClass('wrong');
                                                    }
                                                });
                                            }
                                        }
                                        if (myanswer.toString() == answer.toString()) {
                                            allCount = 5;
                                        } else {
                                            allCount = 1;
                                        }
                                    }

                                    $(".honorStatus").addClass("honorStatus" + allCount);
                                    var getGoldNum = Math.round(_this.goldCoins * allCount / 5);
                                    if (allCount > 0) {
                                        for (var i = 0; i < allCount; i++) {
                                            $(".allStars").append("<span class='_starActive _starActive" + i + "'></span>");
                                        }
                                        var _starNum = 0;
                                        setInterval(function () {
                                            if (_starNum < allCount) {
                                                $(".allStars ._starActive").eq(_starNum).addClass("animation2")
                                                _starNum++;
                                            }

                                        }, 300)
                                    }
                                    var trueAnswer = [];
                                    for (var s = 0; s < answer.length; s++) {
                                        for (var h = 0; h < data.Options.length; h++) {
                                            if (answer[s] == data.Options[h].Key) {
                                                if (h == 0) {
                                                    trueAnswer.push("A");
                                                } else if (h == 1) {
                                                    trueAnswer.push("B");
                                                } else if (h == 2) {
                                                    trueAnswer.push("C");
                                                } else if (h == 3) {
                                                    trueAnswer.push("D");
                                                }
                                            }
                                        }
                                    }
                                    trueAnswer.sort();
                                    $(".trueAnswerContent").html(trueAnswer.toString())
                                    $('.AnalysisContent').html(data.Analysis);
                                    $(".learnCenterBox").hide();
                                    $(".checkAnswerBox").show();
                                    var H1 = $(".checkAnswerPlayBox")[0].scrollHeight, H2 = $(".checkAnswerPlayBox")[0].clientHeight;
                                    if (H1 > H2) {
                                        $(".ScrollDown").show();
                                    }
                                    $(".checkAnswerPlayBox").scroll(function () {
                                        SrollMonitor($(this));
                                    })

                                    if ($(".learnCenter_OptionContent")) {
                                        var H = $(".learnCenter_AnswerOptionOption").height();
                                        $(".learnCenter_OptionContent .learnCenter_OptionStem").height(H + "px");
                                    }
                                    $(".sureBtn").click(function () {
                                        submitOptionAnswers();
                                    })
                                    function submitOptionAnswers() {
                                        $(".mask").hide();
                                        $(".maskContent").html("");
                                        //生成金币结果
                                        var thisData = {
                                        };
                                        thisData.acid = _this.actionId;
                                        thisData.type = _this.type;
                                        thisData.acin = _this.goldCoins;
                                        thisData.gcin = getGoldNum;
                                        _page.submitData.coins.push(thisData);
                                        //生成答题结果
                                        var thisSubjectAnswerData = {
                                        };
                                        thisSubjectAnswerData.type = data.SubjectType;//题目类型 
                                        thisSubjectAnswerData.sbId = _this.questionId;//题目id
                                        thisSubjectAnswerData.rtar = allCount;//获得星数
                                        thisSubjectAnswerData.scon = _this.goldCoins;//题目金币数(非第一次学习,则为0)
                                        thisSubjectAnswerData.rcon = getGoldNum;//得到金币数
                                        thisSubjectAnswerData.kdge = data.KnowledgeId;//知识点id
                                        thisSubjectAnswerData.sanw = myanswer;//答案
                                        thisSubjectAnswerData.sops = sops;//选项顺序
                                        _page.submitData.answers.push(thisSubjectAnswerData);
                                        _this.callBack();
                                    }
                                }

                            }

                        }
                    })
                }
            },
            //填空题
            studyFill: {
                //_action:动作对象
                //kcb:动作开场白,为null则没有开场白
                value: function studyFill(_this, kcb) {
                    $(".backToParent").hide();
                    var _page = this;
                    if (_page.canNotAddGolds) {//如果此页不加金币,则动作的金币数改为0
                        _this.goldCoins = 0;
                    }
                    //动作执行内容
                    $.ajax({
                        type: "get",
                        url: searchUrl + "?r=" + Math.floor(Math.random() * 10000),
                        data: {
                            subjectId: _this.questionId
                        },
                        success: function (data) {
                            if (data.State) {
                                var data = data.Data;
                                var arr = data.Stem.split("{:}");
                                data.StemArr = arr;
                                var lenArr = [];
                                for (var i = 0; i < arr.length; i++) {
                                    if (i == arr.length - 1) {
                                        lenArr.push(0);
                                    } else {
                                        var len = data.Answer.Perfect[i].split("|")[0].length;
                                        if (len <= 5) {
                                            len = 5;
                                        } else {
                                            len = Math.ceil(len / 5) * 5;
                                        }
                                        lenArr.push(24 * len);
                                    }
                                }
                                var datas = {
                                    _this: _this,
                                    kcb: kcb,
                                    list: data,
                                    lenArr: lenArr
                                };
                                var html = template('studyFillScriptDiv', datas);
                                $(".maskContent").html(html)
                                $(".mask").show();
                                var W1 = $(".learnCenterBgImg").height() * 1431 / 728 + "px";
                                $(".learnCenterBox").css("width", W1);

                                if (_this.kcbSrc !== "") {//如果有开场白,则执行开场白
                                    kcb.audio.play();
                                }

                                $(".subMit").click(function () {
                                    clearTimeout(setTimeoutCheckAnswer);
                                    checkFillAnswer();
                                })
                                var setTimeoutCheckAnswer = setTimeout(function () {
                                    checkFillAnswer();
                                }, _this.studyTime)
                                function checkFillAnswer() {
                                    var myanswer = [];
                                    var allCount = 0;
                                    var count = 0;
                                    var trueAnswer = data.Answer;
                                    $('.learnCenter_fillStemText input').each(function (i, val) {
                                        if ($(this).val().trim() !== "") {
                                            var myVal = $(this).val().trim();
                                            $(".learnCenter_fillAnswerStemText input").eq(i).val(myVal);
                                            var jsonDom = {
                                            };
                                            jsonDom.Indx = i;
                                            jsonDom.Text = myVal;
                                            if (trueAnswer.Perfect[i].trim().indexOf(myVal) != -1) {
                                                count += 1;
                                                jsonDom.Scor = 1;
                                                $(".learnCenter_fillAnswerStemText input").eq(i).css("color", "#479e95");
                                            } else if (trueAnswer.Correct[i] && trueAnswer.Correct[i].trim().indexOf(myVal) != -1) {
                                                count += 0.8;
                                                jsonDom.Scor = 0.8;
                                                $(".learnCenter_fillAnswerStemText input").eq(i).css("color", "#f57363");
                                            } else if (trueAnswer.Other[i] && trueAnswer.Other[i].trim().indexOf(myVal) != -1) {
                                                count += 0.6;
                                                jsonDom.Scor = 0.6;
                                                $(".learnCenter_fillAnswerStemText input").eq(i).css("color", "#f57363");
                                            } else {
                                                count += 0;
                                                jsonDom.Scor = 0;
                                                $(".learnCenter_fillAnswerStemText input").eq(i).css("color", "#f57363");
                                            }
                                            myanswer.push(jsonDom)
                                        }

                                    });
                                    allCount = Math.round(5 * count / trueAnswer.Perfect.length);
                                    if (allCount == 0) {
                                        allCount = 1;
                                    }
                                    if (myanswer.length == 0) {
                                        allCount = 0;
                                    }
                                    $(".honorStatus").addClass("honorStatus" + allCount);
                                    var getGoldNum = Math.round(_this.goldCoins * allCount / 5);
                                    if (allCount > 0) {
                                        for (var i = 0; i < allCount; i++) {
                                            $(".allStars").append("<span class='_starActive _starActive" + i + "'></span>");
                                        }
                                        var _starNum = 0;
                                        setInterval(function () {
                                            if (_starNum < allCount) {
                                                $(".allStars ._starActive").eq(_starNum).addClass("animation2")
                                                _starNum++;
                                            }

                                        }, 300)
                                    }
                                    var trueAnswerStr = "";
                                    for (var k in trueAnswer.Perfect) {
                                        trueAnswerStr += trueAnswer.Perfect[k].replace(/\|/g, "&nbsp;或&nbsp;") + "&nbsp;&nbsp";
                                    }
                                    $(".trueAnswerContent").html(trueAnswerStr)
                                    $('.AnalysisContent').html(data.Analysis);
                                    $(".learnCenterBox").hide();
                                    $(".checkAnswerBox").show();
                                    var H1 = $(".checkAnswerPlayBox")[0].scrollHeight, H2 = $(".checkAnswerPlayBox")[0].clientHeight;
                                    if (H1 > H2) {
                                        $(".ScrollDown").show();
                                    }
                                    $(".checkAnswerPlayBox").scroll(function () {
                                        SrollMonitor($(this));
                                    })
                                    $(".sureBtn").click(function () {
                                        submitFillAnswers();
                                    })
                                    function submitFillAnswers() {
                                        $(".mask").hide();
                                        $(".maskContent").html("");
                                        //生成金币结果
                                        var thisData = {
                                        };
                                        thisData.acid = _this.actionId;
                                        thisData.type = _this.type;
                                        thisData.acin = _this.goldCoins;
                                        thisData.gcin = getGoldNum;
                                        _page.submitData.coins.push(thisData);
                                        //生成答题结果
                                        var thisSubjectAnswerData = {
                                        };
                                        thisSubjectAnswerData.type = data.SubjectType;//题目类型 
                                        thisSubjectAnswerData.sbId = _this.questionId;//题目id
                                        thisSubjectAnswerData.rtar = allCount;//获得星数
                                        thisSubjectAnswerData.scon = _this.goldCoins;//题目金币数(非第一次学习,则为0)
                                        thisSubjectAnswerData.rcon = getGoldNum;//得到金币数
                                        thisSubjectAnswerData.kdge = data.KnowledgeId;//知识点id
                                        thisSubjectAnswerData.sanw = myanswer;//答案
                                        _page.submitData.answers.push(thisSubjectAnswerData);
                                        _this.callBack();
                                    }
                                }
                            }
                        }
                    })
                }
            },
            //连线题
            studyLinking: {
                //_action:动作对象
                //kcb:动作开场白,为null则没有开场白
                value: function studyLinking(_this, kcb) {
                    $(".backToParent").hide();
                    var _page = this;
                    if (_page.canNotAddGolds) {//如果此页不加金币,则动作的金币数改为0
                        _this.goldCoins = 0;
                    }
                    //动作执行内容
                    $.ajax({
                        type: "get",
                        url: searchUrl + "?r=" + Math.floor(Math.random() * 10000),
                        data: {
                            subjectId: _this.questionId
                        },
                        success: function (data) {
                            if (data.State) {
                                var data = data.Data;
                                var datas = {
                                    _this: _this,
                                    kcb: kcb,
                                    list: data
                                };
                                var html = template('studyLinkingScriptDiv', datas);
                                $(".maskContent").html(html);
                                $(".mask").show();
                                var W1 = $(".learnCenterBgImg").height() * 1431 / 728 + "px";
                                $(".learnCenterBox").css("width", W1);
                                if (_this.kcbSrc !== "") {//如果有开场白,则执行开场白
                                    kcb.audio.play();
                                }
                                $(".learnCenter_linkingPart").onLine({
                                    allData: data,
                                    _this: _this
                                });

                            }

                        }
                    })
                }
            },
            //选择填空
            studyOptionFill: {
                //_action:动作对象
                //kcb:动作开场白,为null则没有开场白
                value: function studyOptionFill(_this, kcb) {
                    $(".backToParent").hide();
                    var _page = this;
                    if (_page.canNotAddGolds) {//如果此页不加金币,则动作的金币数改为0
                        _this.goldCoins = 0;
                    }
                    //动作执行内容
                    $.ajax({
                        type: "get",
                        url: searchUrl + "?r=" + Math.floor(Math.random() * 10000),
                        data: {
                            subjectId: _this.questionId
                        },
                        success: function (data) {
                            if (data.State) {
                                var data = data.Data;
                                var arr = data.Stem.split("{:}");
                                data.StemArr = arr;
                                var datas = {
                                    _this: _this,
                                    kcb: kcb,
                                    list: data
                                };
                                var html = template('studyOptionFillScriptDiv', datas);
                                $(".maskContent").html(html);
                                var sops = [];
                                for (var i = 0; i < data.Options.length; i++) {
                                    sops.push(data.Options[i].Key);
                                }

                                $(".mask").show();
                                var W1 = $(".learnCenterBgImg").height() * 1431 / 728 + "px";
                                $(".learnCenterBox").css("width", W1);
                                if (_this.kcbSrc !== "") {//如果有开场白,则执行开场白
                                    kcb.audio.play();
                                }

                                var _copy = null;
                                $(".learnCenter_FillOptionList").on("mousedown touchstart", function (e) {
                                    e.preventDefault();
                                    if ($(this).css("opacity") == 0) {
                                        return
                                    }
                                    var touch;
                                    if (event.touches) {
                                        touch = event.touches[0];
                                    } else {
                                        touch = event;
                                    }
                                    var x = touch.clientX;
                                    var y = touch.clientY;
                                    //复制自己并加入定位,去除外边距
                                    _copy = $(this).clone().css({ "position": "absolute", "zIndex": "999", "margin": "0px" });
                                    //添加到 body中
                                    $("body").append(_copy);
                                    //计算x,y值
                                    var _left = x - _copy.width() / 2;
                                    var _top = y - _copy.height() / 2;
                                    //定位copy;
                                    _copy.css({ "top": _top + "px", "left": _left + "px" });
                                    //自己的元素变成透明
                                    $(this).css({ "opacity": 0 });
                                })
                                $(window).on("mousemove touchmove", function (e) {
                                    e.preventDefault();
                                    if (_copy) {
                                        var touch;
                                        if (event.touches) {
                                            touch = event.touches[0];
                                        } else {
                                            touch = event;
                                        }
                                        var x = touch.clientX;
                                        var y = touch.clientY;
                                        //计算x,y值
                                        var _left = x - _copy.width() / 2;
                                        var _top = y - _copy.height() / 2;
                                        //定位copy;
                                        _copy.css({ "top": _top + "px", "left": _left + "px" });
                                    }
                                })

                                $(window).on("mouseup touchend", function (e) {
                                    if (_copy) {
                                        e.preventDefault();
                                        var touch;
                                        if (event.touches) {
                                            touch = event.changedTouches[0];
                                        } else {
                                            touch = event;
                                        }
                                        var x = touch.clientX;
                                        var y = touch.clientY;
                                        var inDom = null;
                                        $('.learnCenter_OptionFillStemList').each(function (j, dom) {
                                            var $this = $(this)[0];
                                            //每个题干选项的左上角位置
                                            var x1 = getElementPageLeft($this);
                                            var y1 = getElementPageTop($this);
                                            //每个题干选项的右下角位置
                                            var x2 = x1 + $(this).width();
                                            var y2 = y1 + $(this).height();
                                            //判断拖拽元素中心坐标是否在题干选项范围之内
                                            if ((x1 <= x && x <= x2) && (y1 <= y && y <= y2)) {
                                                inDom = $(this);
                                            }
                                        });
                                        if (inDom) {
                                            _that = _copy;
                                            //如果题干选项内容为空，即没有拖拽的选项插入进去
                                            if (inDom.children().length > 0) {
                                                var oldOptionId = inDom.find(".learnCenter_newfillBox").attr("optionId");
                                                $(".learnCenter_FillOptionList[optionId='" + oldOptionId + "']").css({ "opacity": "1" });
                                            }
                                            //获取拖拽选项的基本信息
                                            var optionId = _that.attr("optionId");
                                            var texts = _that.html();
                                            //重新生成一个元素其信息与拖拽元素的信息相同插入题干选项内
                                            var creatStr = "<div optionId=" + optionId + " class='learnCenter_newfillBox'>" + texts + "</div>";
                                            inDom.html(creatStr);
                                            inDom.css("top", 0);
                                        } else {
                                            var optionId = _copy.attr("optionId");
                                            $(".learnCenter_FillOptionList[optionId='" + optionId + "']").css({ "opacity": "1" });
                                        }
                                        _copy.remove();
                                        _copy = null;
                                    }
                                });

                                $(".subMit").click(function () {
                                    clearTimeout(setTimeoutCheckAnswer);
                                    checkOptionFillAnswer();
                                })

                                var setTimeoutCheckAnswer = setTimeout(function () {
                                    checkOptionFillAnswer();
                                }, _this.studyTime)
                                function checkOptionFillAnswer() {
                                    var myanswer = [];
                                    for (var m = 0; m < $('.learnCenter_OptionFillStemList').length; m++) {
                                        var arr = [];
                                        var a = $('.learnCenter_OptionFillStemList').eq(m).find('.learnCenter_newfillBox');
                                        if (a.length > 0) {
                                            arr.push(m);
                                            arr.push(Number(a.attr("optionId")));
                                            myanswer.push(arr);
                                        }
                                    }
                                    var num = 0;
                                    for (var j = 0; j < data.Answer.length; j++) {
                                        if ($('.learnCenter_newfillBox').eq(j).attr("optionId") == data.Answer[j][1]) {
                                            num++
                                        }
                                    }
                                    var allCount = Math.round(5 * (num / data.Answer.length));
                                    if (allCount == 0) {
                                        allCount = 1;
                                    }
                                    if ($('.learnCenter_newfillBox').length == 0) {
                                        allCount = 0;
                                    }
                                    $(".honorStatus").addClass("honorStatus" + allCount);
                                    var getGoldNum = Math.round(_this.goldCoins * allCount / 5);
                                    if (allCount > 0) {
                                        for (var i = 0; i < allCount; i++) {
                                            $(".allStars").append("<span class='_starActive _starActive" + i + "'></span>");
                                        }
                                        var _starNum = 0;
                                        setInterval(function () {
                                            if (_starNum < allCount) {
                                                $(".allStars ._starActive").eq(_starNum).addClass("animation2")
                                                _starNum++;
                                            }

                                        }, 300)
                                    }
                                    creatOptionFillHtml($(".fillAnswerBox")[0], myanswer, data);
                                    creatOptionFillHtml($(".trueAnswerContent")[0], data.Answer, data);
                                    function creatOptionFillHtml(dom, answer, data) {
                                        var allStr = data.Stem.replace(/\{:\}/g, "<span class='answerFillBox'><span class='fillBoxs verticalcenter' style='border: 0px;'>&nbsp;</span></span>");
                                        $(dom).html(allStr);
                                        var answerFillBox = $(dom).find(".answerFillBox");
                                        for (var i = 0; i < answer.length; i++) {
                                            for (var k = 0; k < data.Options.length; k++) {
                                                if (data.Options[k].Key == answer[i][1]) {
                                                    var texts = "";
                                                    if (data.OptionType == 0) {
                                                        texts = "<img src=" + data.Options[k].Text + " />"
                                                    } else {
                                                        texts = data.Options[k].Text;
                                                    }

                                                    for (var j = 0; j < data.Answer.length; j++) {
                                                        if (data.Answer[j][0] == answer[i][0]) {
                                                            var domstr = "";
                                                            if (data.Answer[j][1] == answer[i][1]) {
                                                                domstr = "<span class='fillBoxs right verticalcenter'>" + texts + "</span>";
                                                            } else {
                                                                domstr = "<span class='fillBoxs wrong verticalcenter'>" + texts + "</span>";
                                                            }
                                                            answerFillBox.eq(answer[i][0]).html(domstr);
                                                            break;
                                                        }
                                                    }

                                                    break;
                                                }
                                            }

                                        }
                                    }

                                    $('.AnalysisContent').html(data.Analysis);
                                    $(".learnCenterBox").hide();
                                    $(".checkAnswerBox").show();
                                    var H1 = $(".checkAnswerPlayBox")[0].scrollHeight, H2 = $(".checkAnswerPlayBox")[0].clientHeight;
                                    if (H1 > H2) {
                                        $(".ScrollDown").show();
                                    }
                                    $(".checkAnswerPlayBox").scroll(function () {
                                        SrollMonitor($(this));
                                    })
                                    $(".sureBtn").click(function () {
                                        submitOptionFillAnswers();
                                    })

                                    function submitOptionFillAnswers() {
                                        $(".mask").hide();
                                        $(".maskContent").html("");
                                        //生成金币结果
                                        var thisData = {
                                        };
                                        thisData.acid = _this.actionId;
                                        thisData.type = _this.type;
                                        thisData.acin = _this.goldCoins;
                                        thisData.gcin = getGoldNum;
                                        _page.submitData.coins.push(thisData);
                                        //生成答题结果
                                        var thisSubjectAnswerData = {
                                        };
                                        thisSubjectAnswerData.type = data.SubjectType;//题目类型 
                                        thisSubjectAnswerData.sbId = _this.questionId;//题目id
                                        thisSubjectAnswerData.rtar = allCount;//获得星数
                                        thisSubjectAnswerData.scon = _this.goldCoins;//题目金币数(非第一次学习,则为0)
                                        thisSubjectAnswerData.rcon = getGoldNum;//得到金币数
                                        thisSubjectAnswerData.kdge = data.KnowledgeId;//知识点id
                                        thisSubjectAnswerData.sanw = myanswer;//答案
                                        thisSubjectAnswerData.sops = sops;//选项顺序
                                        _page.submitData.answers.push(thisSubjectAnswerData);
                                        _this.callBack();
                                    }
                                }
                            }
                        }
                    })
                }
            },
            //主观题
            studySubjective: {
                //_action:动作对象
                //kcb:动作开场白,为null则没有开场白
                value: function studySubjective(_this, kcb) {
                    $(".backToParent").hide();
                    var _page = this;
                    if (_page.canNotAddGolds) {//如果此页不加金币,则动作的金币数改为0
                        _this.goldCoins = 0;
                    }
                    //动作执行内容
                    $.ajax({
                        type: "get",
                        url: searchUrl + "?r=" + Math.floor(Math.random() * 10000),
                        data: {
                            subjectId: _this.questionId
                        },
                        success: function (data) {
                            if (data.State) {
                                var data = data.Data;
                                var datas = {
                                    _this: _this,
                                    kcb: kcb,
                                    list: data
                                };
                                var html = template('studySubjectiveScriptDiv', datas);
                                $(".maskContent").html(html);
                                $(".mask").show();
                                var W1 = $(".learnCenterBgImg").height() * 1431 / 728 + "px";
                                $(".learnCenterBox").css("width", W1);

                                if (_this.kcbSrc !== "") {//如果有开场白,则执行开场白
                                    kcb.audio.play();
                                }
                                var myText = "";
                                $(".subMit").click(function () {
                                    myText = $(".textAreaBox .learnCenter_subjectiveTextarea").val();
                                    if (myText !== "") {
                                        clearTimeout(setTimeoutCheckAnswer);
                                        checkSubjectiveAnswer();
                                    }
                                });

                                var setTimeoutCheckAnswer = setTimeout(function () {
                                    myText = $(".textAreaBox .learnCenter_subjectiveTextarea").val();
                                    checkSubjectiveAnswer();
                                }, _this.studyTime)
                                function checkSubjectiveAnswer() {
                                    $(".subjective_contrast .mySubAnswerContent").html(myText);
                                    var allCount = 0;
                                    $('.starsScoreBox').raty({
                                        width: false,
                                        click: function (score, evt) {
                                            allCount = score;
                                        }
                                    });
                                    var Num = 40;
                                    $(".subjectiveSure").html("确定 （" + Num + "s）");
                                    var djs = setInterval(function () {
                                        Num--;
                                        $(".subjectiveSure").html("确定 （" + Num + "s）");
                                        if (Num == 0) {
                                            clearInterval(djs);
                                            $(".subjectiveSure").html("确定");
                                            $(".subjectiveSure").addClass("active")
                                        }
                                    }, 1000);
                                    $(".learnCenterBox").hide();
                                    $(".StandardMask").show();

                                    var W = $(".subjectiveBgImg").height() * 1431 / 728 + "px";
                                    $(".showStandard").css("width", W);

                                    $(".subjectiveSure").click(function () {
                                        if ($(this).hasClass("active")) {
                                            clearTimeout(showScoreTimeOut);
                                            showScore();
                                        }
                                    })
                                    var showScoreTimeOut = setTimeout(function () {
                                        showScore();
                                    }, 60000);
                                    function showScore() {
                                        $(".honorStatus").addClass("honorStatus" + allCount);
                                        if (allCount > 0) {
                                            for (var i = 0; i < allCount; i++) {
                                                $(".allStars").append("<span class='_starActive _starActive" + i + "'></span>");
                                            }
                                            var _starNum = 0;
                                            setInterval(function () {
                                                if (_starNum < allCount) {
                                                    $(".allStars ._starActive").eq(_starNum).addClass("animation2")
                                                    _starNum++;
                                                }

                                            }, 300)
                                        }
                                        var getGoldNum = Math.round(_this.goldCoins * allCount / 5);
                                        $(".contrast .mySubAnswerContent").html(myText);
                                        $('.AnalysisContent').html(data.Analysis);
                                        $(".StandardMask").hide();
                                        $(".checkAnswerBox").show();
                                        var H1 = $(".checkAnswerPlayBox")[0].scrollHeight, H2 = $(".checkAnswerPlayBox")[0].clientHeight;
                                        if (H1 > H2) {
                                            $(".ScrollDown").show();
                                        }
                                        $(".checkAnswerPlayBox").scroll(function () {
                                            SrollMonitor($(this));
                                        })

                                        $(".sureBtn").click(function () {
                                            submitSubjectiveAnswers();
                                        })
                                        function submitSubjectiveAnswers() {
                                            $(".mask").hide();
                                            $(".maskContent").html("");
                                            //生成金币结果
                                            var thisData = {
                                            };
                                            thisData.acid = _this.actionId;
                                            thisData.type = _this.type;
                                            thisData.acin = _this.goldCoins;
                                            thisData.gcin = getGoldNum;
                                            _page.submitData.coins.push(thisData);
                                            //生成答题结果
                                            var thisSubjectAnswerData = {
                                            };
                                            thisSubjectAnswerData.type = data.SubjectType;//题目类型
                                            thisSubjectAnswerData.sbId = _this.questionId;//题目id
                                            thisSubjectAnswerData.rtar = allCount;//获得星数
                                            thisSubjectAnswerData.scon = _this.goldCoins;//题目金币数(非第一次学习,则为0)
                                            thisSubjectAnswerData.rcon = getGoldNum;//得到金币数
                                            thisSubjectAnswerData.kdge = data.knowledgeId;//知识点id
                                            thisSubjectAnswerData.sanw = myText;//答案
                                            _page.submitData.answers.push(thisSubjectAnswerData);
                                            _this.callBack();
                                        }

                                    }
                                }
                            }
                        }
                    })
                }
            },
            //圈点批注-标色
            studyAnnotation: {
                //_action:动作对象
                //kcb:动作开场白,为null则没有开场白
                value: function studyAnnotation(_this, kcb) {
                    $(".backToParent").hide();
                    var _page = this;
                    if (_page.canNotAddGolds) {//如果此页不加金币,则动作的金币数改为0
                        _this.goldCoins = 0;
                    }
                    //动作执行内容
                    $.ajax({
                        type: "get",
                        url: searchUrl + "?r=" + Math.floor(Math.random() * 10000),
                        data: {
                            subjectId: _this.questionId
                        },
                        success: function (data) {
                            if (data.State) {
                                //将数组进行正序排列,并返回一个二维数组,二维数组中的每个数组中的值都是连续的
                                function splitArray(arr) {
                                    arr = arr.sort((a, b) => a - b);
                                    var arrs = [];
                                    if (arr.length > 0) {
                                        var _start = arr[0];
                                        var _arr = [];
                                        for (var i = 0; i < arr.length; i++) {
                                            if (arr[i] == _start++) {
                                                _arr.push(arr[i]);
                                            } else {
                                                arrs.push(_arr);
                                                _arr = [arr[i]];
                                                _start = arr[i] + 1;
                                            }
                                        }
                                        arrs.push(_arr);
                                    }
                                    return arrs;
                                }
                                //上个方法的辅助方法,获取数组中包含次数字的数组
                                function getInArrFirst(arrs, index) {
                                    for (var i in arrs) {
                                        var arr = arrs[i];
                                        for (var a in arr) {
                                            if (arr[a] == index) {
                                                return arr;
                                            }
                                        }
                                    }
                                }

                                var data = data.Data;
                                var datas = {
                                    _this: _this,
                                    kcb: kcb,
                                    list: data
                                };
                                var html = template('studyAnnotationScriptDiv', datas);
                                $(".maskContent").html(html);
                                $(".mask").show();
                                var W1 = $(".learnCenterBgImg").height() * 1431 / 728 + "px";
                                $(".learnCenterBox").css("width", W1);
                                if (_this.kcbSrc !== "") {//如果有开场白,则执行开场白
                                    kcb.audio.play();
                                }
                                var mousedown = false; //是否点击过程中
                                var _start = 0; //开始位置下标
                                var _end = 0; //结束位置下标
                                var _left = 0; //此次操作最左侧位置下标
                                var _right = 0; //此次操作最右侧位置下标
                                var steplist = [];//用于记录此次操作的内容
                                var steps = [];//用于记录所有步骤
                                $(".learnCenter_AnnotationContents").on('mousedown', '.learnCenter_normalText', function (e) {
                                    mousedown = true;
                                    var _index = $(this).index();
                                    if ($(this).hasClass('learnCenter_specialText')) {
                                        _index = getInArrFirst(splitArray(steplist), _index)[0];
                                    }
                                    _start = _end = _left = _right = _index;
                                    $(this).addClass('learnCenter_specialText');

                                });

                                $(".learnCenter_AnnotationContents").on("mousemove", ".learnCenter_normalText", function (e) {
                                    e.preventDefault();
                                    var _that = this;
                                    mouseMove(_that);
                                });
                                $(".learnCenter_AnnotationContents").on("touchmove", ".learnCenter_normalText", function (e) {
                                    e.preventDefault();
                                    var _that;
                                    var touch = e.originalEvent.targetTouches[0];
                                    var ele = document.elementFromPoint(touch.clientX, touch.clientY);
                                    if ($(ele).hasClass("learnCenter_normalText")) {
                                        _that = ele;
                                        mouseMove(_that);
                                    }

                                });

                                //鼠标移动的方法
                                function mouseMove(_this) {
                                    var _index = $(_this).index();
                                    if (mousedown && _end != _index) {
                                        if (_index > _right) { //如果位置大于最右侧位置,填充最右侧位置
                                            _right = _index;
                                        } else if (_index < _left) { //如果位置小于最左侧位置,填充最左侧位置
                                            _left = _index;
                                        }
                                        _end = _index;

                                        var start = _start;
                                        var end = _end;
                                        if (start > end) {
                                            var i = end;
                                            end = start;
                                            start = i;
                                        }
                                        var indexdoms = $(".learnCenter_AnnotationContents").children();
                                        indexdoms.removeClass('learnCenter_specialText');
                                        //遍历去除所有的曾经选中过的元素(再次选中的元素)
                                        $(".reselected").each(function () {
                                            var _index = $(this).index(); //获取自己的下标
                                            if (_left <= _index && _index <= _right) { //如果在最左侧和最右侧之间
                                                for (var o = 0; o < steplist.length; o++) { //遍历选中的元素
                                                    if (steplist[o] == _index) { //如果选中的元素与数组中的一致
                                                        steplist.splice(o, 1); //删除元素
                                                        break; //退出循环
                                                    }
                                                }
                                            }
                                        });
                                        $(".reselected").removeClass('reselected');

                                        for (var i in steplist) {
                                            indexdoms.eq(steplist[i]).addClass('learnCenter_specialText');
                                        } //遍历其他步骤中选择到的数据
                                        while (start <= end) {
                                            var dom = indexdoms.eq(start++);
                                            if (dom.hasClass('learnCenter_specialText')) {
                                                dom.addClass('reselected');
                                            } else {
                                                dom.addClass('learnCenter_specialText');
                                            }
                                        }; //遍历所有开始位置到结束位置的元素
                                    }
                                }

                                $(window).on("mouseup touchend ", function () {
                                    if (mousedown) {
                                        mousedown = false;
                                        steplist = [];
                                        $(".learnCenter_specialText").each(function () {
                                            steplist.push($(this).index());
                                        });
                                        if (steps.length > 0) { //如果不是第一次操作
                                            var last = steps[steps.length - 1]; //获取上一次的数据
                                            var same = true; //假设与上次操作相同
                                            if (same = last.length == steplist.length) { //如果长度相同
                                                for (var i in last) {
                                                    if (last[i] != steplist[i]) {
                                                        same = false;
                                                        break;
                                                    }
                                                }
                                            }
                                            if (same) {
                                                return;
                                            }
                                        }
                                        steps.push(steplist.concat([]));
                                    }

                                });

                                $(".TodosRevoke").click(function () {
                                    if (steps.length > 0) {
                                        var del = steps.pop();
                                        var indexdoms = $(".learnCenter_AnnotationContents").children();
                                        indexdoms.removeClass("learnCenter_specialText reselected");
                                        if (steps.length > 0) {
                                            steplist = steps[steps.length - 1];
                                            for (var i in steplist) {
                                                indexdoms.eq(steplist[i]).addClass("learnCenter_specialText");
                                            }
                                        } else {
                                            steplist = [];
                                        }
                                    }
                                })
                                var myAnswer = [];
                                $(".TodosSubmit").click(function () {
                                    clearTimeout(setTimeoutCheckAnswer);
                                    checkAnnotationAnswer();
                                })
                                var setTimeoutCheckAnswer = setTimeout(function () {
                                    checkAnnotationAnswer();
                                }, _this.studyTime)
                                function checkAnnotationAnswer() {

                                    var answer = [];
                                    $(".learnCenter_specialText").each(function () {
                                        answer.push($(this).index());
                                    });
                                    var answers = splitArray(answer);
                                    for (var i in answers) {
                                        var a = answers[i];
                                        answers[i] = [a[0], a.length];
                                    }
                                    myAnswer = answers;

                                    var allCount = 0;

                                    if (myAnswer.length > 0) {
                                        for (var i = 0; i < data.Answer.length; i++) {
                                            for (var j = 0; j < myAnswer.length; j++) {
                                                if (myAnswer[j][0] == data.Answer[i][0] && myAnswer[j][1] == data.Answer[i][1]) {
                                                    allCount++;
                                                }
                                            }
                                        }
                                        if (myAnswer.length > data.Answer.length) {
                                            allCount -= (0.5 * (myAnswer.length - data.Answer.length))
                                        }
                                        allCount = Math.round(5 * (allCount / data.Answer.length));
                                        if (allCount <= 0) {
                                            allCount = 1;
                                        }
                                    } else {
                                        allCount = 0;
                                    }
                                    $(".honorStatus").addClass("honorStatus" + allCount);
                                    var getGoldNum = Math.round(_this.goldCoins * allCount / 5);
                                    if (allCount > 0) {
                                        for (var i = 0; i < allCount; i++) {
                                            $(".allStars").append("<span class='_starActive _starActive" + i + "'></span>");
                                        }
                                        var _starNum = 0;
                                        setInterval(function () {
                                            if (_starNum < allCount) {
                                                $(".allStars ._starActive").eq(_starNum).addClass("animation2")
                                                _starNum++;
                                            }

                                        }, 300)
                                    }
                                    $(".StandardContent").html(creatAnnotationHtml(data.Answer, data));
                                    $(".mySubAnswerContent").html(creatAnnotationHtml(myAnswer, data));
                                    function creatAnnotationHtml(answer, data) {
                                        var dom = document.createElement("div");
                                        var contentStr = "";
                                        for (var i = 0; i < data.Content.length; i++) {
                                            var char = data.Content[i];
                                            if (/^\n$/.test(char)) {
                                                contentStr += "<br />";
                                            } else if (/\r/.test(char)) {
                                                contentStr += "<span></span>";
                                            } else if (/ /.test(char)) {
                                                contentStr += "<span>&nbsp;</span>";
                                            } else {
                                                contentStr += "<span class='answerNormalText'>" + char + "</span>";
                                            }
                                        }
                                        dom.innerHTML = contentStr;

                                        for (var i = 0; i < answer.length; i++) {
                                            var isIn = false;
                                            var newArr = answer[i];
                                            for (var k = 0; k < data.Answer.length; k++) {
                                                var newArr2 = data.Answer[k];
                                                if (newArr[0] == newArr2[0] && newArr[1] == newArr2[1]) {
                                                    isIn = true;
                                                    break;
                                                }
                                            }
                                            for (var j = 0; j < newArr[1]; j++) {
                                                $(dom).children().eq(newArr[0] + j).addClass(isIn ? 'trueSpecialText' : 'mySpecialText');
                                            }
                                        }

                                        return $(dom).html();
                                    }

                                    $('.AnalysisContent').html(data.Analysis);
                                    $(".learnCenterBox").hide();
                                    $(".checkAnswerBox").show();
                                    var H1 = $(".checkAnswerPlayBox")[0].scrollHeight, H2 = $(".checkAnswerPlayBox")[0].clientHeight;
                                    if (H1 > H2) {
                                        $(".ScrollDown").show();
                                    }
                                    $(".checkAnswerPlayBox").scroll(function () {
                                        SrollMonitor($(this));
                                    })
                                    $(".sureBtn").click(function () {
                                        submitAnnotationAnswers();
                                    })

                                    function submitAnnotationAnswers() {
                                        $(".mask").hide();
                                        $(".maskContent").html("");
                                        //生成金币结果
                                        var thisData = {
                                        };
                                        thisData.acid = _this.actionId;
                                        thisData.type = _this.type;
                                        thisData.acin = _this.goldCoins;
                                        thisData.gcin = getGoldNum;
                                        _page.submitData.coins.push(thisData);
                                        //生成答题结果
                                        var thisSubjectAnswerData = {
                                        };
                                        thisSubjectAnswerData.type = data.SubjectType;//题目类型 
                                        thisSubjectAnswerData.sbId = _this.questionId;//题目id
                                        thisSubjectAnswerData.rtar = allCount;//获得星数
                                        thisSubjectAnswerData.scon = _this.goldCoins;//题目金币数(非第一次学习,则为0)
                                        thisSubjectAnswerData.rcon = getGoldNum;//得到金币数
                                        thisSubjectAnswerData.kdge = data.KnowledgeId;//知识点id
                                        thisSubjectAnswerData.sanw = myAnswer;//答案
                                        _page.submitData.answers.push(thisSubjectAnswerData);
                                        _this.callBack();
                                    }
                                }
                            }

                        }
                    })
                }
            },
            //圈点批注-断句
            studyAnnotation2: {
                //_action:动作对象
                //kcb:动作开场白,为null则没有开场白
                value: function studyAnnotation2(_this, kcb) {
                    $(".backToParent").hide();
                    var _page = this;
                    if (_page.canNotAddGolds) {//如果此页不加金币,则动作的金币数改为0
                        _this.goldCoins = 0;
                    }
                    //动作执行内容
                    $.ajax({
                        type: "get",
                        url: searchUrl + "?r=" + Math.floor(Math.random() * 10000),
                        data: {
                            subjectId: _this.questionId
                        },
                        success: function (data) {
                            if (data.State) {
                                var data = data.Data;
                                var datas = {
                                    _this: _this,
                                    kcb: kcb,
                                    list: data
                                };
                                var html = template('studyAnnotation2ScriptDiv', datas);
                                $(".maskContent").html(html);
                                $(".mask").show();
                                var W1 = $(".learnCenterBgImg").height() * 1431 / 728 + "px";
                                $(".learnCenterBox").css("width", W1);
                                if (_this.kcbSrc !== "") {//如果有开场白,则执行开场白
                                    kcb.audio.play();
                                }
                                var myAnswer = [];
                                $(".learnCenter_Annotation2NormalText").click(function () {
                                    if (!$(this).hasClass("truncation")) {
                                        $(this).addClass("truncation");
                                        var index = $(this).index();
                                        myAnswer.push(index);
                                    }
                                });
                                $(".TodosRevoke").click(function () {
                                    if (myAnswer.length > 0) {
                                        var num = myAnswer[myAnswer.length - 1];
                                        $(".learnCenter_Annotation2NormalText").eq(num).removeClass("truncation");
                                        myAnswer.pop(myAnswer.length - 1);
                                    }
                                })

                                $(".TodosSubmit").click(function () {
                                    clearTimeout(setTimeoutCheckAnswer);
                                    checkAnnotationAnswer();
                                })
                                var setTimeoutCheckAnswer = setTimeout(function () {
                                    checkAnnotationAnswer();
                                }, _this.studyTime)
                                function checkAnnotationAnswer() {
                                    myAnswer.sort(sortNumber)
                                    var allCount = 0;
                                    if (myAnswer.length > 0) {
                                        for (var i = 0; i < data.Answer.length; i++) {
                                            for (var j = 0; j < myAnswer.length; j++) {
                                                if (myAnswer[j] == data.Answer[i]) {
                                                    allCount++;
                                                }
                                            }
                                        }
                                        if (myAnswer.length > data.Answer.length) {
                                            allCount -= (0.5 * (myAnswer.length - data.Answer.length))
                                        }
                                        allCount = Math.round(5 * (allCount / data.Answer.length));

                                        if (allCount <= 0) {
                                            allCount = 1;
                                        }
                                    } else {
                                        allCount = 0;
                                    }
                                    $(".honorStatus").addClass("honorStatus" + allCount);
                                    var getGoldNum = Math.round(_this.goldCoins * allCount / 5);
                                    if (allCount > 0) {
                                        for (var i = 0; i < allCount; i++) {
                                            $(".allStars").append("<span class='_starActive _starActive" + i + "'></span>");
                                        }
                                        var _starNum = 0;
                                        setInterval(function () {
                                            if (_starNum < allCount) {
                                                $(".allStars ._starActive").eq(_starNum).addClass("animation2")
                                                _starNum++;
                                            }

                                        }, 300)
                                    }
                                    $(".StandardContent").html(creatAnnotation2Html(data.Answer, data));
                                    $(".mySubAnswerContent").html(creatAnnotation2Html(myAnswer, data));
                                    function creatAnnotation2Html(answer, data) {
                                        var dom = document.createElement("div");
                                        var contentStr = "";
                                        for (var i = 0; i < data.Content.length; i++) {
                                            var char = data.Content[i];
                                            if (/^\n$/.test(char)) {
                                                contentStr += "<span class='gap gaps fl'  style='display:none;'></span><br />";
                                            }
                                            else if (/\r/.test(char)) {
                                                contentStr += "<span class='normalText fl' style='display:none;'></span><span class='gap gaps fl'  style='display:none;'></span>";
                                            }
                                            else if (/ /.test(char)) {
                                                contentStr += "<span class='normalText fl'>&nbsp;</span><span class='gap gaps fl'></span>";
                                            }
                                            else {
                                                contentStr += "<span class='normalText fl'>" + char + "</span><span class='gap gaps fl'></span>";
                                            }
                                        }
                                        dom.innerHTML = contentStr;
                                        for (var i = 0; i < answer.length; i++) {
                                            var isIn = false;
                                            var newNum = answer[i];
                                            for (var k = 0; k < data.Answer.length; k++) {
                                                var newNum2 = data.Answer[k];
                                                if (newNum == newNum2) {
                                                    isIn = true;
                                                    break;
                                                }
                                            }
                                            $(dom).find(".gap").eq(newNum).addClass(isIn ? 'rightGap' : 'wrongGap').html("/");
                                        }

                                        return $(dom).html();
                                    }
                                    $('.AnalysisContent').html(data.Analysis);
                                    $(".learnCenterBox").hide();
                                    $(".checkAnswerBox").show();

                                    var H1 = $(".checkAnswerPlayBox")[0].scrollHeight, H2 = $(".checkAnswerPlayBox")[0].clientHeight;
                                    if (H1 > H2) {
                                        $(".ScrollDown").show();
                                    }
                                    $(".checkAnswerPlayBox").scroll(function () {
                                        SrollMonitor($(this));
                                    })
                                    $(".sureBtn").click(function () {
                                        submitAnnotation2Answers();
                                    })
                                    function submitAnnotation2Answers() {
                                        $(".mask").hide();
                                        $(".maskContent").html("");
                                        //生成金币结果
                                        var thisData = {
                                        };
                                        thisData.acid = _this.actionId;
                                        thisData.type = _this.type;
                                        thisData.acin = _this.goldCoins;
                                        thisData.gcin = getGoldNum;
                                        _page.submitData.coins.push(thisData);
                                        //生成答题结果
                                        var thisSubjectAnswerData = {
                                        };
                                        thisSubjectAnswerData.type = data.SubjectType;//题目类型 
                                        thisSubjectAnswerData.sbId = _this.questionId;//题目id
                                        thisSubjectAnswerData.rtar = allCount;//获得星数
                                        thisSubjectAnswerData.scon = _this.goldCoins;//题目金币数(非第一次学习,则为0)
                                        thisSubjectAnswerData.rcon = getGoldNum;//得到金币数
                                        thisSubjectAnswerData.kdge = data.KnowledgeId;//知识点id
                                        thisSubjectAnswerData.sanw = myAnswer;//答案
                                        _page.submitData.answers.push(thisSubjectAnswerData);
                                        _this.callBack();
                                    }
                                }
                            }

                        }
                    })
                }
            },
            //自身的文档对象
            doc: {
                value: doc
            },
            //开始执行方法
            doWork: {
                value: function doWork() {

                    var _chapter = this.parent;
                    if (!_chapter.isApprove) {
                        if ((_chapter.nowIndex + 1) % 5 == 1) {
                            var pages = _chapter.pages;
                            _chapter.loadPage(pages[pages.length - 1].pageNum + 1, 5)
                        }
                    }
                    document.body.appendChild(doc);
                    var ThisPage = this;
                    //循环每一个步骤
                    for (var s = 0; s < steps.length; s++) {
                        steps[s].callBack = function () {
                            if (this.stepNum != steps.length - 1)//如果此步骤不是最后一个
                            {
                                steps[this.stepNum + 1].doWork();//执行下一个步骤
                            }
                            else {
                                ThisPage.callBack();
                            }
                        };//设置每一个步骤的回调
                    }
                    //本页开始时记录开始时间
                    this.parent.startTime = (new Date()).getTime();
                    if (steps.length > 0) {//如果步骤数量不为0
                        steps[0].doWork();//执行第一个步骤
                    }
                    else {
                        ThisPage.callBack();
                    }
                }
            },
            //回调函数
            callBack: {
                writable: true,
                value: function callBack(data) {

                }
            }
        });
        if (arguments.length > 0 && arguments[0]) {
            var data = arguments[0];
            if (typeof (data.pageId) != "undefined") {
                this.pageId = data.pageId;
            }
            if (typeof (data.pageNum) != "undefined") {
                this.pageNum = data.pageNum;
            }
            if (typeof (data.pageName) != "undefined") {
                this.pageName = data.pageName;
            }
            if (typeof (data.coinsKey) != "undefined") {
                this.coinsKey = data.coinsKey;
            }
            if (typeof (data.steps) != "undefined") {
                for (var s in data.steps) {
                    var step = new Step(data.steps[s]);
                    this.addStep(step);
                }
            }
        }
    }
    //步骤类
    function Step() {
        var index, actions = [], overAction = 0;
        //设置属性
        Object.defineProperties(this, {
            //页码
            stepNum: {
                enumerable: true,
                get: function () {
                    return index;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        index = val;
                    }
                    else {
                        console.error("页码必须为数字类型");
                    }
                }
            },
            //步骤列表副本
            actions: {
                enumerable: true,
                get: function () {
                    return actions.concat([]);
                }
            },
            //添加步骤
            addAction: {
                value: function addAction(action) {
                    if (action instanceof Action) {
                        action.actionNum = actions.length;
                        Object.defineProperty(action, "parent", {
                            value: this
                        })
                        actions.push(action);
                    }
                    else {
                        console.error("添加的步骤类型错误,应为 Action 对象");
                    }
                }
            },
            //移动步骤
            moveAction: {
                value: function moveAction(i, t) {
                    if (!(typeof (i) == "number" && 0 <= i && i < actions.length)) {
                        console.error("章节下标选择错误");
                    }
                    if (!(typeof (t) == "number" && 0 <= t && t < actions.length)) {
                        console.error("移动至下表选择错误");
                    }
                    var a = actions.splice(i, 1)[0];//获取自己并从数组中移出
                    actions.splice(t, 0, a);//在插入到对应位置中去
                    for (var i in actions)//遍历后从新设置页码
                    {
                        actions[i].actionNum = parseInt(i);
                    }
                }
            },
            //删除步骤
            deleteAction: {
                value: function deleteAction(i) {
                    if (!(typeof (i) == "number" && 0 <= i && i < actions.length)) {
                        console.error("步骤下标选择错误");
                    }
                    var self = actions.splice(i, 1)[0];//获取自己并从数组中移出并返回
                    for (var i in actions) {//重新设置顺序
                        actions[i].actionNum = parseInt(i);
                    }
                    return self;
                }
            },
            //获取步骤
            getAction: {
                value: function getChapter(index) {
                    if (typeof (index) == "number" && index < actions.length) {
                        return actions[index];
                    }
                    else {
                        console.error("获取步骤失败,请查看下标是否正确");
                    }
                }
            },

            //完成动作数
            overAction: {
                get: function () {
                    return overAction;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        overAction = val;
                    }
                    else {
                        console.error("页码必须为数字类型");
                    }
                }
            },
            //执行步骤
            doWork: {
                value: function doWork() {
                    var ThisStep = this;

                    function dothis() {

                        if (ThisStep.actions.length > 0) {//如果有动作
                            var t = 0, c = 0;
                            //t用于循环actions使用,因为setTimeOut后a得到的是最后一个.所以使用一个外部的t来在定时器内部进行循环
                            //c用于记录完成的动作数量
                            //ThisStep用于记录Step自身,防止this混淆
                            for (var a = 0 ; a < ThisStep.actions.length; a++) {//遍历所有的动作
                                //启用定时器进行模仿异步操作
                                setTimeout(function () {
                                    var action = ThisStep.actions[t++];//循环获取值
                                    action.callBack = function () {
                                        var _ThisStep = this.parent;
                                        _ThisStep.overAction++;//增加完成的数量
                                        if (_ThisStep.overAction >= _ThisStep.actions.length) {//如果全部执行完成
                                            _ThisStep.callBack();//执行Step的完成回调
                                        }
                                    }//设置自身执行完成时回调

                                    action.doWork();//执行自己的动作
                                }, 1);
                            }
                        }
                        else {
                            ThisStep.callBack();
                        }
                    }
                    if (this.parent.parent instanceof Chapter) {
                        var _chapter = this.parent.parent;
                        var stop = setInterval(function () {
                            if (_chapter.isStop) {
                                //
                            }
                            else {
                                clearInterval(stop);
                                dothis();
                            }
                        }, 1000);
                        if (_chapter.isStop) {
                            //
                            //在这里记录暂停时间
                            this.parent.submitData.useTime += ((new Date()).getTime() - _chapter.startTime);
                        }
                        else {
                            clearInterval(stop);
                            dothis();
                        }
                    }
                    else {
                        dothis();
                    }

                }
            },
            //步骤执行完成后回调
            callBack: {
                writable: true,
                value: function () {
                    //                	/
                }
            }
        });

        if (arguments.length > 0 && arguments[0]) {
            var data = arguments[0];

            if (typeof (data.stepNum) != "undefined") {
                this.stepNum = data.stepNum;
            }
            if (typeof (data.actions) != "undefined") {
                for (var a in data.actions) {
                    var action;
                    var d = data.actions[a];
                    switch (d.type) {
                        case "setTitle": action = new SetTitle(d); break;
                        case "setBackground": action = new SetBackground(d); break;
                        case "setWaitMillisecond": action = new SetWaitMillisecond(d); break;
                        case "insertImg": action = new InsertImg(d); break;
                        case "insertText": action = new InsertText(d); break;
                        case "xiaoAiChange": action = new XiaoAiChange(d); break;
                        case "scaleDom": action = new ScaleDom(d); break;
                        case "moveDom": action = new MoveDom(d); break;
                        case "twinkleDom": action = new TwinkleDom(d); break;
                        case "hideDom": action = new HideDom(d); break;
                        case "xiaoAiSay": action = new XiaoAiSay(d); break;
                        case "studyAudio": action = new StudyAudio(d); break;
                        case "studyVideo": action = new StudyVideo(d); break;
                        case "studyArticle": action = new StudyArticle(d); break;
                        case "studyRecitation": action = new StudyRecitation(d); break; StudyFastRead
                        case "studyFastReadEasy": action = new StudyFastReadEasy(d); break;
                        case "studyFastRead": action = new StudyFastRead(d); break;
                        case "studyJudgment": action = new StudyJudgment(d); break;
                        case "studyOption": action = new StudyOption(d); break;
                        case "studyFill": action = new StudyFill(d); break;
                        case "studyLinking": action = new StudyLinking(d); break;
                        case "studyOptionFill": action = new StudyOptionFill(d); break;
                        case "studySubjective": action = new StudySubjective(d); break;
                        case "studyAnnotation": action = new StudyAnnotation(d); break;
                        case "studyAnnotation2": action = new StudyAnnotation2(d); break;
                        default: action = new Action(d); break;
                    }
                    this.addAction(action);
                }
            }
        }
    }
    //动作类
    function Action() {
        var index = 0, actionId, type, callback = function () {
        };
        Object.defineProperties(this, {
            //动作序号
            actionNum: {
                enumerable: true,
                get: function () {
                    return index;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        index = val;
                    } else {
                        console.error("动作序号应为数字类型");
                    }
                }
            },
            actionId: {
                enumerable: true,
                get: function () {
                    return actionId;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        actionId = val;
                    } else {
                        console.error("动作Id应为字符串类型");
                    }
                }
            },
            type: {
                enumerable: true,
                get: function () {
                    return type;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        type = val;
                    }
                }

            },
            //执行完成回调
            callBack: {
                get: function () {
                    return callback;
                },
                set: function (val) {
                    if (typeof (val) == "function") {
                        callback = val;
                    }
                    else {
                        console.error("回调必须是function类型");
                    }
                }
            },
            //执行动作
            doWork: {
                writable: true,
                value: function () {
                    this.callBack();
                }
            }
        });
    }
    //设置标题
    function SetTitle() {
        var index = 0, text = "标题", actionId = "", size = "middle", color = "#000", align = "left", x = 0, y = 0, intype = "none", isover = false;
        Object.defineProperties(this, {
            //动作序号
            actionNum: {
                enumerable: true,
                get: function () {
                    return index;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        index = val;
                    } else {
                        console.error("动作序号应为数字类型");
                    }
                }
            },
            actionId: {
                enumerable: true,
                get: function () {
                    return actionId;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        actionId = val;
                    } else {
                        console.error("动作Id应为字符串类型");
                    }
                }
            },
            type: {
                enumerable: true,
                value: "setTitle"
            },
            //文本值
            text: {
                enumerable: true,
                get: function () {
                    return text;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        text = val;
                    }
                }
            },
            //尺寸
            size: {
                enumerable: true,
                get: function () {
                    return size
                },
                set: function (val) {
                    switch (val) {
                        case "big":
                        case "middle":
                        case "small":
                            size = val;
                            break;
                        default: console.error("文本尺寸只能有 big/middle/small 三种"); break;

                    }
                }
            },
            //颜色
            color: {
                enumerable: true,
                get: function () {
                    return color;
                },
                set: function (val) {
                    if (/^(#[0-9A-Fa-f]{3,8})|(rgb\(\d{1,3},\d{1,3},\d{1,3}\)|(rgba\(\d{1,3},\d{1,3},\d{1,3},\d{1,3}\)))$/.test(val)) {
                        color = val;
                    }
                    else {
                        console.error("颜色值填写错误");
                    }
                }
            },
            //对其方式
            align: {
                enumerable: true,
                get: function () {
                    return align;
                },
                set: function (value) {
                    switch (value) {
                        case "left":
                        case "center":
                        case "right":
                            align = value; break;
                        default: console.error("水平对其方式值应为 left/center/right"); break;
                    }
                }
            },
            //横坐标
            x: {
                enumerable: true,
                get: function () {
                    return x;
                },
                set: function (value) {
                    if (typeof (value) == "number") {
                        x = value;
                    }
                    else {
                        console.error("位置的坐标必须为数字");
                    }
                }
            },
            //纵坐标
            y: {
                enumerable: true,
                get: function () {
                    return y;
                },
                set: function (value) {
                    if (typeof (value) == "number") {
                        y = value;
                    }
                    else {
                        console.error("位置的坐标必须为数字");
                    }
                }
            },
            //动画是否结束
            isover: {
                get: function () {
                    return isover;
                },
                set: function (val) {
                    isover = val;
                }
            },
            intype: {
                enumerable: true,
                get: function () {
                    return intype;
                },
                set: function (value) {
                    if (typeof (value) == "string") {
                        intype = value;
                    }
                }
            }
        });
        this.doWork = function () {
            this.parent.parent.setTitle(this, this.actionId, this.text, this.size, this.color, this.align, this.x, this.y, this.intype);
        }

        if (arguments.length != 0) {
            var data = arguments[0];

            if (typeof (data.actionId) != "undefined") {
                this.actionId = data.actionId;
            }
            if (typeof (data.text) != "undefined") {
                this.text = data.text;
            }
            if (typeof (data.size) != "undefined") {
                this.size = data.size;
            }
            if (typeof (data.color) != "undefined") {
                this.color = data.color;
            }
            if (typeof (data.align) != "undefined") {
                this.align = data.align;
            }
            if (typeof (data.x) != "undefined") {
                this.x = data.x;
            }
            if (typeof (data.y) != "undefined") {
                this.y = data.y;
            }
            if (typeof (data.intype) != "undefined") {
                this.intype = data.intype;
            }
        }
    }
    SetTitle.prototype = new Action();
    //设置背景
    function SetBackground() {
        var index = 0, bgType = "color", bg = "#fff", actionId = "", img = new ImgBox();
        Object.defineProperties(this, {
            //动作序号
            actionNum: {
                enumerable: true,
                get: function () {
                    return index;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        index = val;
                    } else {
                        console.error("动作序号应为数字类型");
                    }
                }
            },
            actionId: {
                enumerable: true,
                get: function () {
                    return actionId;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        actionId = val;
                    } else {
                        console.error("动作Id应为字符串类型");
                    }
                }
            },
            type: {
                enumerable: true,
                value: "setBackground"
            },
            bgType: {
                enumerable: true,
                get: function () {
                    return bgType;
                },
                set: function (val) {
                    switch (val) {
                        case "image":
                        case "color":
                            bgType = val;
                            break;
                        default: console.error("背景类型必须是 image/color 两种类型"); break;
                    }
                }
            },
            bg: {
                enumerable: true,
                get: function () {
                    return bg;
                },
                set: function (val) {
                    bg = val;
                    if (bgType == "image") {
                        img.src = val;
                    }
                }
            },
        });
        this.doWork = function () {
            this.parent.parent.setBackground(this.bgType, this.bg);
            //
            this.callBack();
        }
        if (arguments.length != 0) {
            var data = arguments[0];


            if (typeof (data.actionId) != "undefined") {
                this.actionId = data.actionId;
            }
            if (typeof (data.bgType) != "undefined") {
                this.bgType = data.bgType;
            }
            if (typeof (data.bg) != "undefined") {
                this.bg = data.bg;
            }
        }
    }
    SetBackground.prototype = new Action();
    //插入图片
    function InsertImg() {
        var index = 0, actionId = "", img = new ImgBox(), isover = false;
        Object.defineProperties(this, {
            //动作序号
            actionNum: {
                enumerable: true,
                get: function () {
                    return index;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        index = val;
                    } else {
                        console.error("动作序号应为数字类型");
                    }
                }
            },
            actionId: {
                enumerable: true,
                get: function () {
                    return actionId;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        actionId = val;
                        img.id = val;
                    } else {
                        console.error("动作Id应为字符串类型");
                    }
                }
            },
            type: {
                enumerable: true,
                value: "insertImg"
            },
            //图片id
            imgId: {
                enumerable: true,
                get: function () {
                    return img.imgid;
                },
                set: function (val) {
                    img.imgid = val;
                }
            },
            //图片路径
            src: {
                enumerable: true,
                get: function () {
                    return img.src;
                },
                set: function (val) {
                    img.src = val;
                }
            },
            //图片宽度
            width: {
                enumerable: true,
                get: function () {
                    return img.width;
                },
                set: function (val) {
                    img.width = val;
                }
            },
            //图片高度
            height: {
                enumerable: true,
                get: function () {
                    return img.height;
                },
                set: function (val) {
                    img.height = val;
                }
            },
            //水平对齐方式
            align: {
                enumerable: true,
                get: function () {
                    return img.position.align;
                },
                set: function (val) {
                    img.position.align = val;
                }
            },
            //垂直对齐方式
            valign: {
                enumerable: true,
                get: function () {
                    return img.position.valign;
                },
                set: function (val) {
                    img.position.valign = val;
                }
            },
            //x坐标
            x: {
                enumerable: true,
                get: function () {
                    return img.position.x;
                },
                set: function (val) {
                    img.position.x = val;
                }
            },
            //y坐标
            y: {
                enumerable: true,
                get: function () {
                    return img.position.y;
                },
                set: function (val) {
                    img.position.y = val;
                }
            },
            //动画是否结束
            isover: {
                get: function () {
                    return isover;
                },
                set: function (val) {
                    isover = val;
                }
            },
            intype: {
                enumerable: true,
                get: function () {
                    return img.intype;
                },
                set: function (value) {
                    img.intype = value;
                }
            }
        });
        this.doWork = function () {
            this.parent.parent.insertImg(this, img.doc);
        }
        if (arguments.length != 0) {
            var data = arguments[0];

            if (typeof (data.actionId) != "undefined") {
                this.actionId = data.actionId;
            }
            if (typeof (data.imgId) != "undefined") {
                this.imgId = data.imgId;
            }
            if (typeof (data.src) != "undefined") {
                this.src = data.src;
            }
            if (typeof (data.width) != "undefined") {
                this.width = data.width;
            }
            if (typeof (data.height) != "undefined") {
                this.height = data.height;
            }
            if (typeof (data.align) != "undefined") {
                this.align = data.align;
            }
            if (typeof (data.valign) != "undefined") {
                this.valign = data.valign;
            }
            if (typeof (data.x) != "undefined") {
                this.x = data.x;
            }
            if (typeof (data.y) != "undefined") {
                this.y = data.y;
            }
            if (typeof (data.intype) != "undefined") {
                this.intype = data.intype;
            }
        }
    }
    InsertImg.prototype = new Action();
    //插入文字
    function InsertText() {
        var index = 0, actionId = "", text = new TextBox(), isover = false;
        Object.defineProperties(this, {
            //动作序号
            actionNum: {
                enumerable: true,
                get: function () {
                    return index;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        index = val;
                    } else {
                        console.error("动作序号应为数字类型");
                    }
                }
            },
            actionId: {
                enumerable: true,
                get: function () {
                    return actionId;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        actionId = val;
                        text.id = val;
                    } else {
                        console.error("动作Id应为字符串类型");
                    }
                }
            },
            type: {
                enumerable: true,
                value: "insertText"
            },
            //文本值
            text: {
                enumerable: true,
                get: function () {
                    return text.text;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        text.text = val;
                    }
                }
            },
            //尺寸
            size: {
                enumerable: true,
                get: function () {
                    return text.size
                },
                set: function (val) {
                    switch (val) {
                        case "big":
                        case "middle":
                        case "small":
                            text.size = val;
                            break;
                        default: console.error("文本尺寸只能有 big/middle/small 三种"); break;

                    }
                }
            },
            //颜色
            color: {
                enumerable: true,
                get: function () {
                    return text.color;
                },
                set: function (val) {
                    if (/^(#[0-9A-Fa-f]{3,8})|(rgb\(\d{1,3},\d{1,3},\d{1,3}\)|(rgba\(\d{1,3},\d{1,3},\d{1,3},\d{1,3}\)))$/.test(val)) {
                        text.color = val;
                    }
                    else {
                        console.error("颜色值填写错误");
                    }
                }
            },
            //对其方式
            align: {
                enumerable: true,
                get: function () {
                    return text.position.align;
                },
                set: function (value) {
                    switch (value) {
                        case "left":
                        case "center":
                        case "right":
                            text.position.align = value; break;
                        default: console.error("水平对其方式值应为 left/center/right"); break;
                    }
                }
            },
            //横坐标
            x: {
                enumerable: true,
                get: function () {
                    return text.position.x;
                },
                set: function (value) {
                    if (typeof (value) == "number") {
                        text.position.x = value;
                    }
                    else {
                        console.error("位置的坐标必须为数字");
                    }
                }
            },
            //纵坐标
            y: {
                enumerable: true,
                get: function () {
                    return text.position.y;
                },
                set: function (value) {
                    if (typeof (value) == "number") {
                        text.position.y = value;
                    }
                    else {
                        console.error("位置的坐标必须为数字");
                    }
                }
            },
            //动画是否结束
            isover: {
                get: function () {
                    return isover;
                },
                set: function (val) {
                    isover = val;
                }
            },
            intype: {
                enumerable: true,
                get: function () {
                    return text.intype;
                },
                set: function (value) {
                    if (typeof (value) == "string") {
                        text.intype = value;
                    }
                }
            }
        });
        this.doWork = function () {
            this.parent.parent.insertText(this, text.doc);
        }
        if (arguments.length != 0) {
            var data = arguments[0];

            if (typeof (data.actionId) != "undefined") {
                this.actionId = data.actionId;
            }
            if (typeof (data.text) != "undefined") {
                this.text = data.text;
            }
            if (typeof (data.size) != "undefined") {
                this.size = data.size;
            }
            if (typeof (data.color) != "undefined") {
                this.color = data.color;
            }
            if (typeof (data.align) != "undefined") {
                this.align = data.align;
            }
            if (typeof (data.x) != "undefined") {
                this.x = data.x;
            }
            if (typeof (data.y) != "undefined") {
                this.y = data.y;
            }
            if (typeof (data.intype) != "undefined") {
                this.intype = data.intype;
            }
        }
    }
    InsertText.prototype = new Action();
    //小艾说
    function XiaoAiSay() {
        var index = 0, actionId = "", src = "", audio = new AudioBox();
        Object.defineProperties(this, {
            //动作序号
            actionNum: {
                enumerable: true,
                get: function () {
                    return index;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        index = val;
                    } else {
                        console.error("动作序号应为数字类型");
                    }
                }
            },
            actionId: {
                enumerable: true,
                get: function () {
                    return actionId;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        actionId = val;
                    } else {
                        console.error("动作Id应为字符串类型");
                    }
                }
            },
            type: {
                enumerable: true,
                value: "xiaoAiSay"
            },
            //音频路径
            src: {
                enumerable: true,
                get: function () {
                    return src;
                },
                set: function (val) {
                    src = audio.src = val;
                }
            }
        });
        this.doWork = function () {
            this.parent.parent.xiaoAiSay(this, this.src);
        }
        if (arguments.length != 0) {
            var data = arguments[0];
            if (typeof (data.actionId) != "undefined") {
                this.actionId = data.actionId;
            }
            if (typeof (data.src) != "undefined") {
                this.src = data.src;
            }
        }
    }
    XiaoAiSay.prototype = new Action();
    //小艾变
    function XiaoAiChange() {
        var index = 0, id = "", actionId = "", src = "", width = 0, height = 0, align = "left", valign = "top", x = 0, y = 0, imgBox = new ImgBox();
        Object.defineProperties(this, {
            //动作序号
            actionNum: {
                enumerable: true,
                get: function () {
                    return index;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        index = val;
                    } else {
                        console.error("动作序号应为数字类型");
                    }
                }
            },
            actionId: {
                enumerable: true,
                get: function () {
                    return actionId;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        actionId = val;
                    } else {
                        console.error("动作Id应为字符串类型");
                    }
                }
            },
            type: {
                enumerable: true,
                value: "xiaoAiChange"
            },
            //图片id
            imgId: {
                enumerable: true,
                get: function () {
                    return imgid;
                },
                set: function (val) {
                    imgid = val;
                }
            },
            //图片路径
            src: {
                enumerable: true,
                get: function () {
                    return src;
                },
                set: function (val) {
                    src = val;
                    imgBox.src = src;
                }
            },
            //图片宽度
            width: {
                enumerable: true,
                get: function () {
                    return width;
                },
                set: function (val) {
                    width = val;
                }
            },
            //图片高度
            height: {
                enumerable: true,
                get: function () {
                    return height;
                },
                set: function (val) {
                    height = val;
                }
            },
            //水平对齐方式
            align: {
                enumerable: true,
                get: function () {
                    return align;
                },
                set: function (val) {
                    align = val;
                }
            },
            //垂直对齐方式
            valign: {
                enumerable: true,
                get: function () {
                    return valign;
                },
                set: function (val) {
                    valign = val;
                }
            },
            //x坐标
            x: {
                enumerable: true,
                get: function () {
                    return x;
                },
                set: function (val) {
                    x = val;
                }
            },
            //y坐标
            y: {
                enumerable: true,
                get: function () { return y; },
                set: function (val) {
                    y = val;
                }
            }
        });
        this.doWork = function () {
            this.parent.parent.xiaoAiChange(this.imgId, this.src, this.width, this.height, this.align, this.valign, this.x, this.y);
            this.callBack();
        }
        if (arguments.length != 0) {
            var data = arguments[0];

            if (typeof (data.actionId) != "undefined") {
                this.actionId = data.actionId;
            }
            if (typeof (data.objectId) != "undefined") {
                this.objectId = data.objectId;
            }
            if (typeof (data.imgId) != "undefined") {
                this.imgId = data.imgId;
            }
            if (typeof (data.src) != "undefined") {
                this.src = data.src;
            }
            if (typeof (data.width) != "undefined") {
                this.width = data.width;
            }
            if (typeof (data.height) != "undefined") {
                this.height = data.height;
            }
            if (typeof (data.align) != "undefined") {
                this.align = data.align;
            }
            if (typeof (data.valign) != "undefined") {
                this.valign = data.valign;
            }
            if (typeof (data.x) != "undefined") {
                this.x = data.x;
            }
            if (typeof (data.y) != "undefined") {
                this.y = data.y;
            }
            if (typeof (data.intype) != "undefined") {
                this.intype = data.intype;
            }
        }
    }
    XiaoAiChange.prototype = new Action();
    //暂停方法
    function SetWaitMillisecond() {
        var index = 0, ms = 0, actionId = "";
        Object.defineProperties(this, {
            //动作序号
            actionNum: {
                enumerable: true,
                get: function () { return index; },
                set: function (val) {
                    if (typeof (val) == "number") {
                        index = val;
                    } else {
                        console.error("动作序号应为数字类型");
                    }
                }
            },
            actionId: {
                enumerable: true,
                get: function () { return actionId; },
                set: function (val) {
                    if (typeof (val) == "string") {
                        actionId = val;
                    } else {
                        console.error("动作Id应为字符串类型");
                    }
                }
            },
            type: {
                enumerable: true,
                value: "setWaitMillisecond"
            },
            stop: {
                enumerable: true,
                get: function () {
                    return ms;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        ms = val;
                    }
                    else {
                        console.error("等待毫秒时间必须为数字");
                    }
                }
            }
        });
        this.doWork = function () {
            var _this = this;
            if (Worker && false) {//如果浏览器支持worker
                var worker = new Worker("js/TimerWorker.js");
                worker.postMessage(this.stop);
                worker.onmessage = function (e) {
                    //
                    _this.callBack();
                    worker.terminate();
                }
            }
            else {//如果浏览器不支持worker
                setTimeout(function () {
                    //
                    _this.callBack();
                }, this.stop);
            }
        }

        if (arguments.length != 0) {
            var data = arguments[0];

            if (typeof (data.actionId) != "undefined") {
                this.actionId = data.actionId;
            }
            if (typeof (data.stop) != "undefined") {
                this.stop = data.stop;
            }
        }

    }
    SetWaitMillisecond.prototype = new Action();
    //缩放元素
    function ScaleDom() {
        var index = 0, duration = 0, ratio = 1, actionId = "", num = 0, objectId = "", isover = false;
        Object.defineProperties(this, {
            //动作序号
            actionNum: {
                enumerable: true,
                get: function () { return index; },
                set: function (val) {
                    if (typeof (val) == "number") {
                        index = val;
                    } else {
                        console.error("动作序号应为数字类型");
                    }
                }
            },
            actionId: {
                enumerable: true,
                get: function () { return actionId; },
                set: function (val) {
                    if (typeof (val) == "string") {
                        actionId = val;
                    } else {
                        console.error("动作Id应为字符串类型");
                    }
                }
            },
            objectId: {
                enumerable: true,
                get: function () { return objectId; },
                set: function (val) {
                    if (typeof (val) == "string") {
                        objectId = val;
                    } else {
                        console.error("操作对象Id应为字符串类型");
                    }
                }
            },
            type: {
                enumerable: true,
                value: "scaleDom"
            },
            duration: {
                enumerable: true,
                get: function () {
                    return duration;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        duration = val;
                    } else {
                        console.error("间隔时间应为数字类型");
                    }
                }
            },
            ratio: {
                enumerable: true,
                get: function () {
                    return ratio;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        ratio = val;
                    } else {
                        console.error("缩放比例应为数字类型");
                    }
                }
            },
            num: {
                enumerable: true,
                get: function () {
                    return num;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        num = val;
                    } else {
                        console.error("缩放次数应为数字类型");
                    }
                }
            },
            isover: {
                get: function () {
                    return isover;
                },
                set: function (val) {
                    isover = val;
                }








            }
        });
        this.doWork = function () {
            this.parent.parent.scaleDom(this, this.objectId, this.duration, this.ratio, this.num);
            //
            //          this.callBack();
        }
        if (arguments.length != 0) {
            var data = arguments[0];
            if (typeof (data.actionId) != "undefined") {
                this.actionId = data.actionId;
            }
            if (typeof (data.objectId) != "undefined") {
                this.objectId = data.objectId;
            }
            if (typeof (data.duration) != "undefined") {
                this.duration = data.duration;
            }
            if (typeof (data.ratio) != "undefined") {
                this.ratio = data.ratio;
            }
            if (typeof (data.num) != "undefined") {
                this.num = data.num;
            }
        }
    }
    ScaleDom.prototype = new Action();
    //移动元素
    function MoveDom() {
        var index = 0, actionId = "", duration = 0, x = 0, y = 0, objectId = "";
        Object.defineProperties(this, {
            //动作序号
            actionNum: {
                enumerable: true,
                get: function () { return index; },
                set: function (val) {
                    if (typeof (val) == "number") {
                        index = val;
                    } else {
                        console.error("动作序号应为数字类型");
                    }
                }
            },
            actionId: {
                enumerable: true,
                get: function () { return actionId; },
                set: function (val) {
                    if (typeof (val) == "string") {
                        actionId = val;
                    } else {
                        console.error("动作Id应为字符串类型");
                    }
                }
            },
            objectId: {
                enumerable: true,
                get: function () { return objectId; },
                set: function (val) {
                    if (typeof (val) == "string") {
                        objectId = val;
                    } else {
                        console.error("操作对象Id应为字符串类型");
                    }
                }
            },
            type: {
                enumerable: true,
                value: "moveDom"
            },
            duration: {
                enumerable: true,
                get: function () {
                    return duration;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        duration = val;
                    } else {
                        console.error("间隔时间应为数字类型");
                    }
                }
            },
            x: {
                enumerable: true,
                get: function () {
                    return x;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        x = val;
                    } else {
                        console.error("x坐标应为数字类型");
                    }
                }
            },
            y: {
                enumerable: true,
                get: function () {
                    return y;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        y = val;
                    } else {
                        console.error("y坐标应为数字类型");
                    }
                }
            }
        });
        this.doWork = function () {
            this.parent.parent.moveDom(this, this.objectId, this.duration, this.x, this.y);
        }
        if (arguments.length != 0) {
            var data = arguments[0];
            if (typeof (data.actionId) != "undefined") {
                this.actionId = data.actionId;
            }
            if (typeof (data.objectId) != "undefined") {
                this.objectId = data.objectId;
            }
            if (typeof (data.duration) != "undefined") {
                this.duration = data.duration;
            }
            if (typeof (data.x) != "undefined") {
                this.x = data.x;
            }
            if (typeof (data.y) != "undefined") {
                this.y = data.y;
            }
        }
    }
    MoveDom.prototype = new Action();
    //闪烁元素
    function TwinkleDom() {
        var index = 0, actionId = "", duration = 0, num = 0, objectId = "", isover = false;
        Object.defineProperties(this, {
            //动作序号
            actionNum: {
                enumerable: true,
                get: function () { return index; },
                set: function (val) {
                    if (typeof (val) == "number") {
                        index = val;
                    } else {
                        console.error("动作序号应为数字类型");
                    }
                }
            },
            actionId: {
                enumerable: true,
                get: function () { return actionId; },
                set: function (val) {
                    if (typeof (val) == "string") {
                        actionId = val;
                    } else {
                        console.error("动作Id应为字符串类型");
                    }
                }
            },
            objectId: {
                enumerable: true,
                get: function () { return objectId; },
                set: function (val) {
                    if (typeof (val) == "string") {
                        objectId = val;
                    } else {
                        console.error("操作对象Id应为字符串类型");
                    }
                }
            },
            type: {
                enumerable: true,
                value: "twinkleDom"
            },
            duration: {
                enumerable: true,
                get: function () {
                    return duration;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        duration = val;
                    } else {
                        console.error("闪烁间隔时间应为数字类型");
                    }
                }
            },
            num: {
                enumerable: true,
                get: function () {
                    return num;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        num = val;
                    } else {
                        console.error("闪烁次数应为数字类型");
                    }
                }
            },
            isover: {
                get: function () {
                    return isover;
                },
                set: function (val) {
                    isover = val;
                }








            }
        });
        this.doWork = function () {
            this.parent.parent.twinkleDom(this, this.objectId, this.duration, this.num);
        }
        if (arguments.length != 0) {
            var data = arguments[0];
            if (typeof (data.actionId) != "undefined") {
                this.actionId = data.actionId;
            }
            if (typeof (data.objectId) != "undefined") {
                this.objectId = data.objectId;
            }
            if (typeof (data.duration) != "undefined") {
                this.duration = data.duration;
            }
            if (typeof (data.num) != "undefined") {
                this.num = data.num;
            }
        }
    }
    TwinkleDom.prototype = new Action();
    //隐藏元素
    function HideDom() {
        var index = 0, actionId = "", objectId = "", outtype = "none", isover = false;
        Object.defineProperties(this, {
            //动作序号
            actionNum: {
                enumerable: true,
                get: function () { return index; },
                set: function (val) {
                    if (typeof (val) == "number") {
                        index = val;
                    } else {
                        console.error("动作序号应为数字类型");
                    }
                }
            },
            actionId: {
                enumerable: true,
                get: function () { return actionId; },
                set: function (val) {
                    if (typeof (val) == "string") {
                        actionId = val;
                    } else {
                        console.error("动作Id应为字符串类型");
                    }
                }
            },
            objectId: {
                enumerable: true,
                get: function () { return objectId; },
                set: function (val) {
                    if (typeof (val) == "string") {
                        objectId = val;
                    } else {
                        console.error("操作对象Id应为字符串类型");
                    }
                }
            },
            type: {
                enumerable: true,
                value: "hideDom"
            },
            outtype: {
                enumerable: true,
                get: function () {
                    return outtype;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        outtype = val;
                    } else {
                        console.error("飞出类型应为字符串类型");
                    }
                }
            },
            isover: {
                get: function () {
                    return isover;
                },
                set: function (val) {
                    isover = val;
                }
            }
        });
        this.doWork = function () {
            this.parent.parent.hideDom(this, this.objectId, this.outtype);
        }
        if (arguments.length != 0) {
            var data = arguments[0];
            if (typeof (data.actionId) != "undefined") {
                this.actionId = data.actionId;
            }
            if (typeof (data.objectId) != "undefined") {
                this.objectId = data.objectId;
            }
            if (typeof (data.outtype) != "undefined") {
                this.outtype = data.outtype;
            }
        }
    }
    HideDom.prototype = new Action();
    //学习音频
    function StudyAudio() {
        var index = 0, actionId = "", wordPosition = "bottom", goldCoins = 0, audioText = [], kcb = new AudioBox(), audios = new AudioBox();
        Object.defineProperties(this, {
            //动作序号
            actionNum: {
                enumerable: true,
                get: function () { return index; },
                set: function (val) {
                    if (typeof (val) == "number") {
                        index = val;
                    } else {
                        console.error("动作序号应为数字类型");
                    }
                }
            },
            actionId: {
                enumerable: true,
                get: function () { return actionId; },
                set: function (val) {
                    if (typeof (val) == "string") {
                        actionId = val;
                    } else {
                        console.error("动作Id应为字符串类型");
                    }
                }
            },
            type: {
                enumerable: true,
                value: "studyAudio"
            },
            wordPosition: {
                enumerable: true,
                get: function () {
                    return wordPosition;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        wordPosition = val;
                    } else {
                        console.error("文字位置应为字符串类型");
                    }
                }
            },
            goldCoins: {
                enumerable: true,
                get: function () {
                    return goldCoins;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        goldCoins = val;
                    } else {
                        console.error("金币个数应为数字类型");
                    }
                }
            },
            kcbSrc: {
                enumerable: true,
                get: function () {
                    return kcb.src;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        kcb.src = val;
                    } else {
                        console.error("开场白链接应为字符串类型");
                    }
                }
            },
            kcbImgSrc: {
                enumerable: true,
                get: function () {
                    return kcb.imgSrc;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        kcb.imgSrc = val;
                    } else {
                        console.error("开场白图片应为字符串类型");
                    }
                }
            },
            kcbTextSrc: {
                enumerable: true,
                get: function () {
                    return kcb.textSrc;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        kcb.textSrc = val;
                    } else {
                        console.error("开场白文字应为字符串类型");
                    }
                }
            },
            audioSrc: {
                enumerable: true,
                get: function () {
                    return audios.src;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        audios.src = val;
                    } else {
                        console.error("音频资源应为字符串类型");
                    }
                }
            },
            audioImgSrc: {
                enumerable: true,
                get: function () {
                    return audios.imgSrc;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        audios.imgSrc = val;
                    } else {
                        console.error("音频图片应为字符串类型");
                    }
                }
            },
            audioTextSrc: {
                enumerable: true,
                get: function () {
                    return audioText;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        audioText = audios.textSrc = lrcToAudioTextArray(val);
                    }
                    else {
                        console.error("音频文字应为字符串类型");
                    }
                }
            }
        });
        this.doWork = function () {
            var _kcb = null;
            if (kcb.kcbSrc != "") {
                _kcb = kcb;
            }
            this.parent.parent.studyAudio(this, _kcb, audios);
        }
        if (arguments.length != 0) {
            var data = arguments[0];
            if (typeof (data.actionId) != "undefined") {
                this.actionId = data.actionId;
            }
            if (typeof (data.wordPosition) != "undefined") {
                this.wordPosition = data.wordPosition;
            }
            if (typeof (data.goldCoins) != "undefined") {
                this.goldCoins = data.goldCoins;
            }
            if (typeof (data.kcbSrc) != "undefined") {
                this.kcbSrc = data.kcbSrc;
            }
            if (typeof (data.kcbImgSrc) != "undefined") {
                this.kcbImgSrc = data.kcbImgSrc;
            }
            if (typeof (data.kcbTextSrc) != "undefined") {
                this.kcbTextSrc = data.kcbTextSrc;
            }
            if (typeof (data.audioSrc) != "undefined") {
                this.audioSrc = data.audioSrc;
            }
            if (typeof (data.audioImgSrc) != "undefined") {
                this.audioImgSrc = data.audioImgSrc;
            }
            if (typeof (data.audioTextSrc) != "undefined") {
                this.audioTextSrc = data.audioTextSrc;
            }
        }
    }
    StudyAudio.prototype = new Action();
    //学习视频
    function StudyVideo() {
        var index = 0, actionId = "", goldCoins = 0, kcb = new AudioBox(), videoSrc = "";
        Object.defineProperties(this, {
            //动作序号
            actionNum: {
                enumerable: true,
                get: function () { return index; },
                set: function (val) {
                    if (typeof (val) == "number") {
                        index = val;
                    } else {
                        console.error("动作序号应为数字类型");
                    }
                }
            },
            actionId: {
                enumerable: true,
                get: function () { return actionId; },
                set: function (val) {
                    if (typeof (val) == "string") {
                        actionId = val;
                    } else {
                        console.error("动作Id应为字符串类型");
                    }
                }
            },
            type: {
                enumerable: true,
                value: "studyVideo"
            },
            goldCoins: {
                enumerable: true,
                get: function () {
                    return goldCoins;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        goldCoins = val;
                    } else {
                        console.error("金币个数应为数字类型");
                    }
                }
            },
            kcbSrc: {
                enumerable: true,
                get: function () {
                    return kcb.src;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        kcb.src = val;
                    } else {
                        console.error("开场白链接应为字符串类型");
                    }
                }
            },
            kcbImgSrc: {
                enumerable: true,
                get: function () {
                    return kcb.imgSrc;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        kcb.imgSrc = val;
                    } else {
                        console.error("开场白图片应为字符串类型");
                    }
                }
            },
            kcbTextSrc: {
                enumerable: true,
                get: function () {
                    return kcb.textSrc;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        kcb.textSrc = val;
                    } else {
                        console.error("开场白文字应为字符串类型");
                    }
                }
            },
            videoSrc: {
                enumerable: true,
                get: function () {
                    return videoSrc;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        videoSrc = val;
                    } else {
                        console.error("视频资源应为字符串类型");
                    }
                }
            }
        });
        this.doWork = function () {
            var _kcb = null;
            if (kcb.audioSrc != "") {
                _kcb = kcb;
            }
            this.parent.parent.studyVideo(this, _kcb);
        }
        if (arguments.length != 0) {
            var data = arguments[0];
            if (typeof (data.actionId) != "undefined") {
                this.actionId = data.actionId;
            }
            if (typeof (data.wordPosition) != "undefined") {
                this.wordPosition = data.wordPosition;
            }
            if (typeof (data.goldCoins) != "undefined") {
                this.goldCoins = data.goldCoins;
            }
            if (typeof (data.kcbSrc) != "undefined") {
                this.kcbSrc = data.kcbSrc;
            }
            if (typeof (data.kcbImgSrc) != "undefined") {
                this.kcbImgSrc = data.kcbImgSrc;
            }
            if (typeof (data.kcbTextSrc) != "undefined") {
                this.kcbTextSrc = data.kcbTextSrc;
            }
            if (typeof (data.videoSrc) != "undefined") {
                this.videoSrc = data.videoSrc;
            }
        }
    }
    StudyVideo.prototype = new Action();
    //学习图文
    function StudyArticle() {
        var index = 0, actionId = "", wordPosition = "bottom"; goldCoins = 0, kcb = new AudioBox(), articleImgSrc = "", articleTextSrc = "", studyTime = 0;
        Object.defineProperties(this, {
            //动作序号
            actionNum: {
                enumerable: true,
                get: function () { return index; },
                set: function (val) {
                    if (typeof (val) == "number") {
                        index = val;
                    } else {
                        console.error("动作序号应为数字类型");
                    }
                }
            },
            actionId: {
                enumerable: true,
                get: function () { return actionId; },
                set: function (val) {
                    if (typeof (val) == "string") {
                        actionId = val;
                    } else {
                        console.error("动作Id应为字符串类型");
                    }
                }
            },
            type: {
                enumerable: true,
                value: "studyArticle"
            },
            goldCoins: {
                enumerable: true,
                get: function () {
                    return goldCoins;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        goldCoins = val;
                    } else {
                        console.error("金币个数应为数字类型");
                    }
                }
            },
            wordPosition: {
                enumerable: true,
                get: function () {
                    return wordPosition;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        wordPosition = val;
                    } else {
                        console.error("文字位置应为字符串类型");
                    }
                }
            },
            kcbSrc: {
                enumerable: true,
                get: function () {
                    return kcb.src;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        kcb.src = val;
                    } else {
                        console.error("开场白链接应为字符串类型");
                    }
                }
            },
            kcbImgSrc: {
                enumerable: true,
                get: function () {
                    return kcb.imgSrc;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        kcb.imgSrc = val;
                    } else {
                        console.error("开场白图片应为字符串类型");
                    }
                }
            },
            kcbTextSrc: {
                enumerable: true,
                get: function () {
                    return kcb.textSrc;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        kcb.textSrc = val;
                    } else {
                        console.error("开场白文字应为字符串类型");
                    }
                }
            },
            articleImgSrc: {
                enumerable: true,
                get: function () {
                    return articleImgSrc;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        articleImgSrc = val;
                    } else {
                        console.error("图片资源应为字符串类型");
                    }
                }
            },
            articleTextSrc: {
                enumerable: true,
                get: function () {
                    return articleTextSrc;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        articleTextSrc = val;
                    } else {
                        console.error("文字资源应为字符串类型");
                    }
                }
            },
            studyTime: {
                enumerable: true,
                get: function () {
                    return studyTime;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        studyTime = val;
                    } else {
                        console.error("学习时间应为字符串类型");
                    }
                }
            }

        });
        this.doWork = function () {
            var _kcb = null;
            if (kcb.audioSrc != "") {
                _kcb = kcb;
            }
            this.parent.parent.studyArticle(this, _kcb);
        }
        if (arguments.length != 0) {
            var data = arguments[0];
            if (typeof (data.actionId) != "undefined") {
                this.actionId = data.actionId;
            }
            if (typeof (data.wordPosition) != "undefined") {
                this.wordPosition = data.wordPosition;
            }
            if (typeof (data.goldCoins) != "undefined") {
                this.goldCoins = data.goldCoins;
            }
            if (typeof (data.kcbSrc) != "undefined") {
                this.kcbSrc = data.kcbSrc;
            }
            if (typeof (data.kcbImgSrc) != "undefined") {
                this.kcbImgSrc = data.kcbImgSrc;
            }
            if (typeof (data.kcbTextSrc) != "undefined") {
                this.kcbTextSrc = data.kcbTextSrc;
            }
            if (typeof (data.articleImgSrc) != "undefined") {
                this.articleImgSrc = data.articleImgSrc;
            }
            if (typeof (data.articleTextSrc) != "undefined") {
                this.articleTextSrc = data.articleTextSrc;
            }
            if (typeof (data.studyTime) != "undefined") {
                this.studyTime = data.studyTime;
            }
        }
    }
    StudyArticle.prototype = new Action();
    //学习朗读
    function StudyRecitation() {
        var index = 0, actionId = "", goldCoins = 0, kcb = new AudioBox(), recitationSrc = "", studyTime = 0;
        Object.defineProperties(this, {
            //动作序号
            actionNum: {
                enumerable: true,
                get: function () { return index; },
                set: function (val) {
                    if (typeof (val) == "number") {
                        index = val;
                    } else {
                        console.error("动作序号应为数字类型");
                    }
                }
            },
            actionId: {
                enumerable: true,
                get: function () { return actionId; },
                set: function (val) {
                    if (typeof (val) == "string") {
                        actionId = val;
                    } else {
                        console.error("动作Id应为字符串类型");
                    }
                }
            },
            type: {
                enumerable: true,
                value: "studyRecitation"
            },
            goldCoins: {
                enumerable: true,
                get: function () {
                    return goldCoins;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        goldCoins = val;
                    } else {
                        console.error("金币个数应为数字类型");
                    }
                }
            },
            kcbSrc: {
                enumerable: true,
                get: function () {
                    return kcb.src;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        kcb.src = val;
                    } else {
                        console.error("开场白链接应为字符串类型");
                    }
                }
            },
            kcbImgSrc: {
                enumerable: true,
                get: function () {
                    return kcb.imgSrc;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        kcb.imgSrc = val;
                    } else {
                        console.error("开场白图片应为字符串类型");
                    }
                }
            },
            kcbTextSrc: {
                enumerable: true,
                get: function () {
                    return kcb.textSrc;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        kcb.textSrc = val;
                    } else {
                        console.error("开场白文字应为字符串类型");
                    }
                }
            },
            recitationSrc: {
                enumerable: true,
                get: function () {
                    return recitationSrc;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        recitationSrc = val;
                    } else {
                        console.error("文字资源应为字符串类型");
                    }
                }
            },
            studyTime: {
                enumerable: true,
                get: function () {
                    return studyTime;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        studyTime = val;
                    } else {
                        console.error("学习时间应为字符串类型");
                    }
                }
            }
        });
        this.doWork = function () {
            var _kcb = null;
            if (kcb.audioSrc != "") {
                _kcb = kcb;
            }
            this.parent.parent.studyRecitation(this, _kcb);
        }
        if (arguments.length != 0) {
            var data = arguments[0];
            if (typeof (data.actionId) != "undefined") {
                this.actionId = data.actionId;
            }
            if (typeof (data.goldCoins) != "undefined") {
                this.goldCoins = data.goldCoins;
            }
            if (typeof (data.kcbSrc) != "undefined") {
                this.kcbSrc = data.kcbSrc;
            }
            if (typeof (data.kcbImgSrc) != "undefined") {
                this.kcbImgSrc = data.kcbImgSrc;
            }
            if (typeof (data.kcbTextSrc) != "undefined") {
                this.kcbTextSrc = data.kcbTextSrc;
            }
            if (typeof (data.recitationSrc) != "undefined") {
                this.recitationSrc = data.recitationSrc;
            }
            if (typeof (data.studyTime) != "undefined") {
                this.studyTime = data.studyTime;
            }
        }
    }
    StudyRecitation.prototype = new Action();
    //快速阅读-简单模式
    function StudyFastReadEasy() {
        var index = 0, actionId = "", goldCoins = 0, kcb = new AudioBox(), fastReadText = "", speed = 0, showNum = 0;
        Object.defineProperties(this, {
            //动作序号
            actionNum: {
                enumerable: true,
                get: function () { return index; },
                set: function (val) {
                    if (typeof (val) == "number") {
                        index = val;
                    } else {
                        console.error("动作序号应为数字类型");
                    }
                }
            },
            actionId: {
                enumerable: true,
                get: function () { return actionId; },
                set: function (val) {
                    if (typeof (val) == "string") {
                        actionId = val;
                    } else {
                        console.error("动作Id应为字符串类型");
                    }
                }
            },
            type: {
                enumerable: true,
                value: "studyFastReadEasy"
            },
            goldCoins: {
                enumerable: true,
                get: function () {
                    return goldCoins;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        goldCoins = val;
                    } else {
                        console.error("金币个数应为数字类型");
                    }
                }
            },
            kcbSrc: {
                enumerable: true,
                get: function () {
                    return kcb.src;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        kcb.src = val;
                    } else {
                        console.error("开场白链接应为字符串类型");
                    }
                }
            },
            kcbImgSrc: {
                enumerable: true,
                get: function () {
                    return kcb.imgSrc;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        kcb.imgSrc = val;
                    } else {
                        console.error("开场白图片应为字符串类型");
                    }
                }
            },
            kcbTextSrc: {
                enumerable: true,
                get: function () {
                    return kcb.textSrc;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        kcb.textSrc = val;
                    } else {
                        console.error("开场白文字应为字符串类型");
                    }
                }
            },
            fastReadText: {
                enumerable: true,
                get: function () {
                    return fastReadText;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        fastReadText = val;
                    } else {
                        console.error("文字资源应为字符串类型");
                    }
                }
            },
            speed: {
                enumerable: true,
                get: function () {
                    return speed;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        speed = val;
                    } else {
                        console.error("切换速度应为数字类型");
                    }
                }
            },
            showNum: {
                enumerable: true,
                get: function () {
                    return showNum;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        showNum = val;
                    } else {
                        console.error("显示字数应为数字类型");
                    }
                }
            }
        });
        this.doWork = function () {
            var _kcb = null;
            if (kcb.audioSrc != "") {
                _kcb = kcb;
            }
            this.parent.parent.studyFastReadEasy(this, _kcb);
        }
        if (arguments.length != 0) {
            var data = arguments[0];
            if (typeof (data.actionId) != "undefined") {
                this.actionId = data.actionId;
            }
            if (typeof (data.goldCoins) != "undefined") {
                this.goldCoins = data.goldCoins;
            }
            if (typeof (data.kcbSrc) != "undefined") {
                this.kcbSrc = data.kcbSrc;
            }
            if (typeof (data.kcbImgSrc) != "undefined") {
                this.kcbImgSrc = data.kcbImgSrc;
            }
            if (typeof (data.kcbTextSrc) != "undefined") {
                this.kcbTextSrc = data.kcbTextSrc;
            }
            if (typeof (data.fastReadText) != "undefined") {
                this.fastReadText = data.fastReadText;
            }
            if (typeof (data.speed) != "undefined") {
                this.speed = data.speed;
            }
            if (typeof (data.showNum) != "undefined") {
                this.showNum = data.showNum;
            }
        }
    }
    StudyFastReadEasy.prototype = new Action();
    //快速阅读
    function StudyFastRead() {
        var index = 0, actionId = "", goldCoins = 0, kcb = new AudioBox(), showModel = "whole", fastReadText = "", speed = 0, showNum = 0;
        Object.defineProperties(this, {
            //动作序号
            actionNum: {
                enumerable: true,
                get: function () { return index; },
                set: function (val) {
                    if (typeof (val) == "number") {
                        index = val;
                    } else {
                        console.error("动作序号应为数字类型");
                    }
                }
            },
            actionId: {
                enumerable: true,
                get: function () { return actionId; },
                set: function (val) {
                    if (typeof (val) == "string") {
                        actionId = val;
                    } else {
                        console.error("动作Id应为字符串类型");
                    }
                }
            },
            type: {
                enumerable: true,
                value: "studyFastRead"
            },
            goldCoins: {
                enumerable: true,
                get: function () {
                    return goldCoins;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        goldCoins = val;
                    } else {
                        console.error("金币个数应为数字类型");
                    }
                }
            },
            kcbSrc: {
                enumerable: true,
                get: function () {
                    return kcb.src;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        kcb.src = val;
                    } else {
                        console.error("开场白链接应为字符串类型");
                    }
                }
            },
            kcbImgSrc: {
                enumerable: true,
                get: function () {
                    return kcb.imgSrc;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        kcb.imgSrc = val;
                    } else {
                        console.error("开场白图片应为字符串类型");
                    }
                }
            },
            kcbTextSrc: {
                enumerable: true,
                get: function () {
                    return kcb.textSrc;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        kcb.textSrc = val;
                    } else {
                        console.error("开场白文字应为字符串类型");
                    }
                }
            },
            showModel: {
                enumerable: true,
                get: function () {
                    return showModel;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        showModel = val;
                    } else {
                        console.error("显示模式应为字符串类型");
                    }
                }
            },
            fastReadText: {
                enumerable: true,
                get: function () {
                    return fastReadText;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        fastReadText = val;
                    } else {
                        console.error("文字资源应为字符串类型");
                    }
                }
            },
            speed: {
                enumerable: true,
                get: function () {
                    return speed;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        speed = val;
                    } else {
                        console.error("切换速度应为数字类型");
                    }
                }
            },
            showNum: {
                enumerable: true,
                get: function () {
                    return showNum;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        showNum = val;
                    } else {
                        console.error("显示字数应为数字类型");
                    }
                }
            }
        });
        this.doWork = function () {
            var _kcb = null;
            if (kcb.audioSrc != "") {
                _kcb = kcb;
            }
            this.parent.parent.studyFastRead(this, _kcb);
        }
        if (arguments.length != 0) {
            var data = arguments[0];
            if (typeof (data.actionId) != "undefined") {
                this.actionId = data.actionId;
            }
            if (typeof (data.goldCoins) != "undefined") {
                this.goldCoins = data.goldCoins;
            }
            if (typeof (data.kcbSrc) != "undefined") {
                this.kcbSrc = data.kcbSrc;
            }
            if (typeof (data.kcbImgSrc) != "undefined") {
                this.kcbImgSrc = data.kcbImgSrc;
            }
            if (typeof (data.kcbTextSrc) != "undefined") {
                this.kcbTextSrc = data.kcbTextSrc;
            }
            if (typeof (data.showModel) != "undefined") {
                this.showModel = data.showModel;
            }
            if (typeof (data.fastReadText) != "undefined") {
                this.fastReadText = data.fastReadText;
            }
            if (typeof (data.speed) != "undefined") {
                this.speed = data.speed;
            }
            if (typeof (data.showNum) != "undefined") {
                this.showNum = data.showNum;
            }
        }
    }
    StudyFastRead.prototype = new Action();
    //判断题
    function StudyJudgment() {
        var index = 0, actionId = "", goldCoins = 0, studyTime = 0, kcb = new AudioBox(), questionId = "";
        Object.defineProperties(this, {
            //动作序号
            actionNum: {
                enumerable: true,
                get: function () { return index; },
                set: function (val) {
                    if (typeof (val) == "number") {
                        index = val;
                    } else {
                        console.error("动作序号应为数字类型");
                    }
                }
            },
            actionId: {
                enumerable: true,
                get: function () { return actionId; },
                set: function (val) {
                    if (typeof (val) == "string") {
                        actionId = val;
                    } else {
                        console.error("动作Id应为字符串类型");
                    }
                }
            },
            type: {
                enumerable: true,
                value: "studyJudgment"
            },
            goldCoins: {
                enumerable: true,
                get: function () {
                    return goldCoins;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        goldCoins = val;
                    } else {
                        console.error("金币个数应为数字类型");
                    }
                }
            },
            studyTime: {
                enumerable: true,
                get: function () {
                    return studyTime;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        studyTime = val;
                    } else {
                        console.error("学习时间应为数字类型");
                    }
                }
            },
            kcbSrc: {
                enumerable: true,
                get: function () {
                    return kcb.src;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        kcb.src = val;
                    } else {
                        console.error("开场白链接应为字符串类型");
                    }
                }
            },
            kcbImgSrc: {
                enumerable: true,
                get: function () {
                    return kcb.imgSrc;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        kcb.imgSrc = val;
                    } else {
                        console.error("开场白图片应为字符串类型");
                    }
                }
            },
            kcbTextSrc: {
                enumerable: true,
                get: function () {
                    return kcb.textSrc;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        kcb.textSrc = val;
                    } else {
                        console.error("开场白文字应为字符串类型");
                    }
                }
            },
            questionId: {
                enumerable: true,
                get: function () {
                    return questionId;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        questionId = val;
                    } else {
                        console.error("问题Id应为数字类型");
                    }
                }
            }
        });
        this.doWork = function () {
            var _kcb = null;
            if (kcb.audioSrc != "") {
                _kcb = kcb;
            }
            this.parent.parent.studyJudgment(this, _kcb);
        }
        if (arguments.length != 0) {
            var data = arguments[0];
            if (typeof (data.actionId) != "undefined") {
                this.actionId = data.actionId;
            }
            if (typeof (data.goldCoins) != "undefined") {
                this.goldCoins = data.goldCoins;
            }
            if (typeof (data.studyTime) != "undefined") {
                this.studyTime = data.studyTime;
            }
            if (typeof (data.kcbSrc) != "undefined") {
                this.kcbSrc = data.kcbSrc;
            }
            if (typeof (data.kcbImgSrc) != "undefined") {
                this.kcbImgSrc = data.kcbImgSrc;
            }
            if (typeof (data.kcbTextSrc) != "undefined") {
                this.kcbTextSrc = data.kcbTextSrc;
            }
            if (typeof (data.questionId) != "undefined") {
                this.questionId = data.questionId;
            }
        }
    }
    StudyJudgment.prototype = new Action();
    //选择题
    function StudyOption() {
        var index = 0, actionId = "", goldCoins = 0, studyTime = 0, kcb = new AudioBox(), questionId = "";
        Object.defineProperties(this, {
            //动作序号
            actionNum: {
                enumerable: true,
                get: function () { return index; },
                set: function (val) {
                    if (typeof (val) == "number") {
                        index = val;
                    } else {
                        console.error("动作序号应为数字类型");
                    }
                }
            },
            actionId: {
                enumerable: true,
                get: function () { return actionId; },
                set: function (val) {
                    if (typeof (val) == "string") {
                        actionId = val;
                    } else {
                        console.error("动作Id应为字符串类型");
                    }
                }
            },
            type: {
                enumerable: true,
                value: "StudyOption"
            },
            goldCoins: {
                enumerable: true,
                get: function () {
                    return goldCoins;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        goldCoins = val;
                    } else {
                        console.error("金币个数应为数字类型");
                    }
                }
            },
            studyTime: {
                enumerable: true,
                get: function () {
                    return studyTime;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        studyTime = val;
                    } else {
                        console.error("学习时间应为数字类型");
                    }
                }
            },
            kcbSrc: {
                enumerable: true,
                get: function () {
                    return kcb.src;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        kcb.src = val;
                    } else {
                        console.error("开场白链接应为字符串类型");
                    }
                }
            },
            kcbImgSrc: {
                enumerable: true,
                get: function () {
                    return kcb.imgSrc;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        kcb.imgSrc = val;
                    } else {
                        console.error("开场白图片应为字符串类型");
                    }
                }
            },
            kcbTextSrc: {
                enumerable: true,
                get: function () {
                    return kcb.textSrc;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        kcb.textSrc = val;
                    } else {
                        console.error("开场白文字应为字符串类型");
                    }
                }
            },
            questionId: {
                enumerable: true,
                get: function () {
                    return questionId;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        questionId = val;
                    } else {
                        console.error("问题Id应为数字类型");
                    }
                }
            }
        });
        this.doWork = function () {
            var _kcb = null;
            if (kcb.audioSrc != "") {
                _kcb = kcb;
            }
            this.parent.parent.studyOption(this, _kcb);
        }
        if (arguments.length != 0) {
            var data = arguments[0];
            if (typeof (data.actionId) != "undefined") {
                this.actionId = data.actionId;
            }
            if (typeof (data.goldCoins) != "undefined") {
                this.goldCoins = data.goldCoins;
            }
            if (typeof (data.studyTime) != "undefined") {
                this.studyTime = data.studyTime;
            }
            if (typeof (data.kcbSrc) != "undefined") {
                this.kcbSrc = data.kcbSrc;
            }
            if (typeof (data.kcbImgSrc) != "undefined") {
                this.kcbImgSrc = data.kcbImgSrc;
            }
            if (typeof (data.kcbTextSrc) != "undefined") {
                this.kcbTextSrc = data.kcbTextSrc;
            }
            if (typeof (data.questionId) != "undefined") {
                this.questionId = data.questionId;
            }
        }
    }
    StudyOption.prototype = new Action();
    //填空题
    function StudyFill() {
        var index = 0, actionId = "", goldCoins = 0, studyTime = 0, kcb = new AudioBox(), questionId = "";
        Object.defineProperties(this, {
            //动作序号
            actionNum: {
                enumerable: true,
                get: function () { return index; },
                set: function (val) {
                    if (typeof (val) == "number") {
                        index = val;
                    } else {
                        console.error("动作序号应为数字类型");
                    }
                }
            },
            actionId: {
                enumerable: true,
                get: function () { return actionId; },
                set: function (val) {
                    if (typeof (val) == "string") {
                        actionId = val;
                    } else {
                        console.error("动作Id应为字符串类型");
                    }
                }
            },
            type: {
                enumerable: true,
                value: "studyFill"
            },
            goldCoins: {
                enumerable: true,
                get: function () {
                    return goldCoins;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        goldCoins = val;
                    } else {
                        console.error("金币个数应为数字类型");
                    }
                }
            },
            studyTime: {
                enumerable: true,
                get: function () {
                    return studyTime;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        studyTime = val;
                    } else {
                        console.error("学习时间应为数字类型");
                    }
                }
            },
            kcbSrc: {
                enumerable: true,
                get: function () {
                    return kcb.src;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        kcb.src = val;
                    } else {
                        console.error("开场白链接应为字符串类型");
                    }
                }
            },
            kcbImgSrc: {
                enumerable: true,
                get: function () {
                    return kcb.imgSrc;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        kcb.imgSrc = val;
                    } else {
                        console.error("开场白图片应为字符串类型");
                    }
                }
            },
            kcbTextSrc: {
                enumerable: true,
                get: function () {
                    return kcb.textSrc;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        kcb.textSrc = val;
                    } else {
                        console.error("开场白文字应为字符串类型");
                    }
                }
            },
            questionId: {
                enumerable: true,
                get: function () {
                    return questionId;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        questionId = val;
                    } else {
                        console.error("问题Id应为数字类型");
                    }
                }
            }
        });
        this.doWork = function () {
            var _kcb = null;
            if (kcb.audioSrc != "") {
                _kcb = kcb;
            }
            this.parent.parent.studyFill(this, _kcb);
        }
        if (arguments.length != 0) {
            var data = arguments[0];
            if (typeof (data.actionId) != "undefined") {
                this.actionId = data.actionId;
            }
            if (typeof (data.goldCoins) != "undefined") {
                this.goldCoins = data.goldCoins;
            }
            if (typeof (data.studyTime) != "undefined") {
                this.studyTime = data.studyTime;
            }
            if (typeof (data.kcbSrc) != "undefined") {
                this.kcbSrc = data.kcbSrc;
            }
            if (typeof (data.kcbImgSrc) != "undefined") {
                this.kcbImgSrc = data.kcbImgSrc;
            }
            if (typeof (data.kcbTextSrc) != "undefined") {
                this.kcbTextSrc = data.kcbTextSrc;
            }
            if (typeof (data.questionId) != "undefined") {
                this.questionId = data.questionId;
            }
        }
    }
    StudyFill.prototype = new Action();
    //连线题
    function StudyLinking() {
        var index = 0, actionId = "", goldCoins = 0, studyTime = 0, kcb = new AudioBox(), questionId = "";
        Object.defineProperties(this, {
            //动作序号
            actionNum: {
                enumerable: true,
                get: function () { return index; },
                set: function (val) {
                    if (typeof (val) == "number") {
                        index = val;
                    } else {
                        console.error("动作序号应为数字类型");
                    }
                }
            },
            actionId: {
                enumerable: true,
                get: function () { return actionId; },
                set: function (val) {
                    if (typeof (val) == "string") {
                        actionId = val;
                    } else {
                        console.error("动作Id应为字符串类型");
                    }
                }
            },
            type: {
                enumerable: true,
                value: "studyLinking"
            },
            goldCoins: {
                enumerable: true,
                get: function () {
                    return goldCoins;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        goldCoins = val;
                    } else {
                        console.error("金币个数应为数字类型");
                    }
                }
            },
            studyTime: {
                enumerable: true,
                get: function () {
                    return studyTime;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        studyTime = val;
                    } else {
                        console.error("学习时间应为数字类型");
                    }
                }
            },
            kcbSrc: {
                enumerable: true,
                get: function () {
                    return kcb.src;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        kcb.src = val;
                    } else {
                        console.error("开场白链接应为字符串类型");
                    }
                }
            },
            kcbImgSrc: {
                enumerable: true,
                get: function () {
                    return kcb.imgSrc;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        kcb.imgSrc = val;
                    } else {
                        console.error("开场白图片应为字符串类型");
                    }
                }
            },
            kcbTextSrc: {
                enumerable: true,
                get: function () {
                    return kcb.textSrc;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        kcb.textSrc = val;
                    } else {
                        console.error("开场白文字应为字符串类型");
                    }
                }
            },
            questionId: {
                enumerable: true,
                get: function () {
                    return questionId;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        questionId = val;
                    } else {
                        console.error("问题Id应为数字类型");
                    }
                }
            }
        });
        this.doWork = function () {
            var _kcb = null;
            if (kcb.audioSrc != "") {
                _kcb = kcb;
            }
            this.parent.parent.studyLinking(this, _kcb);
        }
        if (arguments.length != 0) {
            var data = arguments[0];
            if (typeof (data.actionId) != "undefined") {
                this.actionId = data.actionId;
            }
            if (typeof (data.goldCoins) != "undefined") {
                this.goldCoins = data.goldCoins;
            }
            if (typeof (data.studyTime) != "undefined") {
                this.studyTime = data.studyTime;
            }
            if (typeof (data.kcbSrc) != "undefined") {
                this.kcbSrc = data.kcbSrc;
            }
            if (typeof (data.kcbImgSrc) != "undefined") {
                this.kcbImgSrc = data.kcbImgSrc;
            }
            if (typeof (data.kcbTextSrc) != "undefined") {
                this.kcbTextSrc = data.kcbTextSrc;
            }
            if (typeof (data.questionId) != "undefined") {
                this.questionId = data.questionId;
            }
        }
    }
    StudyLinking.prototype = new Action();
    //选择填空
    function StudyOptionFill() {
        var index = 0, actionId = "", goldCoins = 0, studyTime = 0, kcb = new AudioBox(), questionId = "";
        Object.defineProperties(this, {
            //动作序号
            actionNum: {
                enumerable: true,
                get: function () { return index; },
                set: function (val) {
                    if (typeof (val) == "number") {
                        index = val;
                    } else {
                        console.error("动作序号应为数字类型");
                    }
                }
            },
            actionId: {
                enumerable: true,
                get: function () { return actionId; },
                set: function (val) {
                    if (typeof (val) == "string") {
                        actionId = val;
                    } else {
                        console.error("动作Id应为字符串类型");
                    }
                }
            },
            type: {
                enumerable: true,
                value: "studyOptionFill"
            },
            goldCoins: {
                enumerable: true,
                get: function () {
                    return goldCoins;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        goldCoins = val;
                    } else {
                        console.error("金币个数应为数字类型");
                    }
                }
            },
            studyTime: {
                enumerable: true,
                get: function () {
                    return studyTime;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        studyTime = val;
                    } else {
                        console.error("学习时间应为数字类型");
                    }
                }
            },
            kcbSrc: {
                enumerable: true,
                get: function () {
                    return kcb.src;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        kcb.src = val;
                    } else {
                        console.error("开场白链接应为字符串类型");
                    }
                }
            },
            kcbImgSrc: {
                enumerable: true,
                get: function () {
                    return kcb.imgSrc;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        kcb.imgSrc = val;
                    } else {
                        console.error("开场白图片应为字符串类型");
                    }
                }
            },
            kcbTextSrc: {
                enumerable: true,
                get: function () {
                    return kcb.textSrc;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        kcb.textSrc = val;
                    } else {
                        console.error("开场白文字应为字符串类型");
                    }
                }
            },
            questionId: {
                enumerable: true,
                get: function () {
                    return questionId;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        questionId = val;
                    } else {
                        console.error("问题Id应为数字类型");
                    }
                }
            }
        });
        this.doWork = function () {
            var _kcb = null;
            if (kcb.audioSrc != "") {
                _kcb = kcb;
            }
            this.parent.parent.studyOptionFill(this, _kcb);
        }
        if (arguments.length != 0) {
            var data = arguments[0];
            if (typeof (data.actionId) != "undefined") {
                this.actionId = data.actionId;
            }
            if (typeof (data.goldCoins) != "undefined") {
                this.goldCoins = data.goldCoins;
            }
            if (typeof (data.studyTime) != "undefined") {
                this.studyTime = data.studyTime;
            }
            if (typeof (data.kcbSrc) != "undefined") {
                this.kcbSrc = data.kcbSrc;
            }
            if (typeof (data.kcbImgSrc) != "undefined") {
                this.kcbImgSrc = data.kcbImgSrc;
            }
            if (typeof (data.kcbTextSrc) != "undefined") {
                this.kcbTextSrc = data.kcbTextSrc;
            }
            if (typeof (data.questionId) != "undefined") {
                this.questionId = data.questionId;
            }
        }
    }
    StudyOptionFill.prototype = new Action();
    //主观题
    function StudySubjective() {
        var index = 0, actionId = "", goldCoins = 0, studyTime = 0, kcb = new AudioBox(), questionId = "";
        Object.defineProperties(this, {
            //动作序号
            actionNum: {
                enumerable: true,
                get: function () { return index; },
                set: function (val) {
                    if (typeof (val) == "number") {
                        index = val;
                    } else {
                        console.error("动作序号应为数字类型");
                    }
                }
            },
            actionId: {
                enumerable: true,
                get: function () { return actionId; },
                set: function (val) {
                    if (typeof (val) == "string") {
                        actionId = val;
                    } else {
                        console.error("动作Id应为字符串类型");
                    }
                }
            },
            type: {
                enumerable: true,
                value: "studySubjective"
            },
            goldCoins: {
                enumerable: true,
                get: function () {
                    return goldCoins;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        goldCoins = val;
                    } else {
                        console.error("金币个数应为数字类型");
                    }
                }
            },
            studyTime: {
                enumerable: true,
                get: function () {
                    return studyTime;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        studyTime = val;
                    } else {
                        console.error("学习时间应为数字类型");
                    }
                }
            },
            kcbSrc: {
                enumerable: true,
                get: function () {
                    return kcb.src;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        kcb.src = val;
                    } else {
                        console.error("开场白链接应为字符串类型");
                    }
                }
            },
            kcbImgSrc: {
                enumerable: true,
                get: function () {
                    return kcb.imgSrc;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        kcb.imgSrc = val;
                    } else {
                        console.error("开场白图片应为字符串类型");
                    }
                }
            },
            kcbTextSrc: {
                enumerable: true,
                get: function () {
                    return kcb.textSrc;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        kcb.textSrc = val;
                    } else {
                        console.error("开场白文字应为字符串类型");
                    }
                }
            },
            questionId: {
                enumerable: true,
                get: function () {
                    return questionId;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        questionId = val;
                    } else {
                        console.error("问题Id应为数字类型");
                    }
                }
            }
        });
        this.doWork = function () {
            var _kcb = null;
            if (kcb.audioSrc != "") {
                _kcb = kcb;
            }
            this.parent.parent.studySubjective(this, _kcb);
        }
        if (arguments.length != 0) {
            var data = arguments[0];
            if (typeof (data.actionId) != "undefined") {
                this.actionId = data.actionId;
            }
            if (typeof (data.goldCoins) != "undefined") {
                this.goldCoins = data.goldCoins;
            }
            if (typeof (data.studyTime) != "undefined") {
                this.studyTime = data.studyTime;
            }
            if (typeof (data.kcbSrc) != "undefined") {
                this.kcbSrc = data.kcbSrc;
            }
            if (typeof (data.kcbImgSrc) != "undefined") {
                this.kcbImgSrc = data.kcbImgSrc;
            }
            if (typeof (data.kcbTextSrc) != "undefined") {
                this.kcbTextSrc = data.kcbTextSrc;
            }
            if (typeof (data.questionId) != "undefined") {
                this.questionId = data.questionId;
            }
        }
    }
    StudySubjective.prototype = new Action();
    //圈点批注-标色
    function StudyAnnotation() {
        var index = 0, actionId = "", goldCoins = 0, studyTime = 0, kcb = new AudioBox(), questionId = "";
        Object.defineProperties(this, {
            //动作序号
            actionNum: {
                enumerable: true,
                get: function () { return index; },
                set: function (val) {
                    if (typeof (val) == "number") {
                        index = val;
                    } else {
                        console.error("动作序号应为数字类型");
                    }
                }
            },
            actionId: {
                enumerable: true,
                get: function () { return actionId; },
                set: function (val) {
                    if (typeof (val) == "string") {
                        actionId = val;
                    } else {
                        console.error("动作Id应为字符串类型");
                    }
                }
            },
            type: {
                enumerable: true,
                value: "studyAnnotation"
            },
            goldCoins: {
                enumerable: true,
                get: function () {
                    return goldCoins;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        goldCoins = val;
                    } else {
                        console.error("金币个数应为数字类型");
                    }
                }
            },
            studyTime: {
                enumerable: true,
                get: function () {
                    return studyTime;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        studyTime = val;
                    } else {
                        console.error("学习时间应为数字类型");
                    }
                }
            },
            kcbSrc: {
                enumerable: true,
                get: function () {
                    return kcb.src;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        kcb.src = val;
                    } else {
                        console.error("开场白链接应为字符串类型");
                    }
                }
            },
            kcbImgSrc: {
                enumerable: true,
                get: function () {
                    return kcb.imgSrc;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        kcb.imgSrc = val;
                    } else {
                        console.error("开场白图片应为字符串类型");
                    }
                }
            },
            kcbTextSrc: {
                enumerable: true,
                get: function () {
                    return kcb.textSrc;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        kcb.textSrc = val;
                    } else {
                        console.error("开场白文字应为字符串类型");
                    }
                }
            },
            questionId: {
                enumerable: true,
                get: function () {
                    return questionId;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        questionId = val;
                    } else {
                        console.error("问题Id应为数字类型");
                    }
                }
            }
        });
        this.doWork = function () {
            var _kcb = null;
            if (kcb.audioSrc != "") {
                _kcb = kcb;
            }
            this.parent.parent.studyAnnotation(this, _kcb);
        }
        if (arguments.length != 0) {
            var data = arguments[0];
            if (typeof (data.actionId) != "undefined") {
                this.actionId = data.actionId;
            }
            if (typeof (data.goldCoins) != "undefined") {
                this.goldCoins = data.goldCoins;
            }
            if (typeof (data.studyTime) != "undefined") {
                this.studyTime = data.studyTime;
            }
            if (typeof (data.kcbSrc) != "undefined") {
                this.kcbSrc = data.kcbSrc;
            }
            if (typeof (data.kcbImgSrc) != "undefined") {
                this.kcbImgSrc = data.kcbImgSrc;
            }
            if (typeof (data.kcbTextSrc) != "undefined") {
                this.kcbTextSrc = data.kcbTextSrc;
            }
            if (typeof (data.questionId) != "undefined") {
                this.questionId = data.questionId;
            }
        }
    }
    StudyAnnotation.prototype = new Action();
    //圈点批注-断句
    function StudyAnnotation2() {
        var index = 0, actionId = "", goldCoins = 0, studyTime = 0, kcb = new AudioBox(), questionId = "";
        Object.defineProperties(this, {
            //动作序号
            actionNum: {
                enumerable: true,
                get: function () { return index; },
                set: function (val) {
                    if (typeof (val) == "number") {
                        index = val;
                    } else {
                        console.error("动作序号应为数字类型");
                    }
                }
            },
            actionId: {
                enumerable: true,
                get: function () { return actionId; },
                set: function (val) {
                    if (typeof (val) == "string") {
                        actionId = val;
                    } else {
                        console.error("动作Id应为字符串类型");
                    }
                }
            },
            type: {
                enumerable: true,
                value: "studyAnnotation2"
            },
            goldCoins: {
                enumerable: true,
                get: function () {
                    return goldCoins;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        goldCoins = val;
                    } else {
                        console.error("金币个数应为数字类型");
                    }
                }
            },
            studyTime: {
                enumerable: true,
                get: function () {
                    return studyTime;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        studyTime = val;
                    } else {
                        console.error("学习时间应为数字类型");
                    }
                }
            },
            kcbSrc: {
                enumerable: true,
                get: function () {
                    return kcb.src;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        kcb.src = val;
                    } else {
                        console.error("开场白链接应为字符串类型");
                    }
                }
            },
            kcbImgSrc: {
                enumerable: true,
                get: function () {
                    return kcb.imgSrc;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        kcb.imgSrc = val;
                    } else {
                        console.error("开场白图片应为字符串类型");
                    }
                }
            },
            kcbTextSrc: {
                enumerable: true,
                get: function () {
                    return kcb.textSrc;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        kcb.textSrc = val;
                    } else {
                        console.error("开场白文字应为字符串类型");
                    }
                }
            },
            questionId: {
                enumerable: true,
                get: function () {
                    return questionId;
                },
                set: function (val) {
                    if (typeof (val) == "number") {
                        questionId = val;
                    } else {
                        console.error("问题Id应为数字类型");
                    }
                }
            }
        });
        this.doWork = function () {
            var _kcb = null;
            if (kcb.audioSrc != "") {
                _kcb = kcb;
            }
            this.parent.parent.studyAnnotation2(this, _kcb);
        }
        if (arguments.length != 0) {
            var data = arguments[0];
            if (typeof (data.actionId) != "undefined") {
                this.actionId = data.actionId;
            }
            if (typeof (data.goldCoins) != "undefined") {
                this.goldCoins = data.goldCoins;
            }
            if (typeof (data.studyTime) != "undefined") {
                this.studyTime = data.studyTime;
            }
            if (typeof (data.kcbSrc) != "undefined") {
                this.kcbSrc = data.kcbSrc;
            }
            if (typeof (data.kcbImgSrc) != "undefined") {
                this.kcbImgSrc = data.kcbImgSrc;
            }
            if (typeof (data.kcbTextSrc) != "undefined") {
                this.kcbTextSrc = data.kcbTextSrc;
            }
            if (typeof (data.questionId) != "undefined") {
                this.questionId = data.questionId;
            }
        }
    }
    StudyAnnotation2.prototype = new Action();
    //定位类
    function Position(doc) {
        var x = 0, y = 0, align = "left", valign = "top";
        Object.defineProperties(this, {
            x: {
                enumerable: true,
                get: function () {
                    return x;
                },
                set: function (value) {
                    if (typeof (value) == "number") {
                        x = value;
                        switch (align) {
                            case "left":
                                doc.style.left = x * zoom + "px";
                                doc.style.right = "";
                                break;
                            case "center":
                                //doc.style.left = x * zoom + "px";
                                //doc.style.marginLeft = (0 - doc.offsetWidth / 2) + "px";
                                break;
                            case "right":
                                doc.style.right = x * zoom + "px";
                                doc.style.left = "";
                                break;
                        }
                    }
                    else {
                        console.error("位置的坐标必须为数字");
                    }
                }
            },
            y: {
                enumerable: true,
                get: function () {
                    return y;
                },
                set: function (value) {
                    if (typeof (value) == "number") {
                        y = value;
                        switch (valign) {
                            case "top":
                                doc.style.top = y * zoom + "px";
                                doc.style.bottom = "";
                                break;
                            case "middle":
                                //doc.style.top = y * zoom + "px";
                                //doc.style.marginTop = (0 - doc.offsetHeight / 2) + "px";
                                break;
                            case "bottom":
                                doc.style.bottom = y * zoom + "px";
                                doc.style.top = "";
                                break;
                        }
                    }
                    else {
                        console.error("位置的坐标必须为数字");
                    }
                }
            },
            align: {
                enumerable: true,
                get: function () {
                    return align;
                },
                set: function (value) {
                    switch (value) {
                        case "left":
                        case "center":
                        case "right":
                            align = value;
                            this.x = x;
                            break;
                        default: console.error("水平对其方式值应为 left/center/right"); break;
                    }
                }
            },
            valign: {
                enumerable: true,
                get: function () {
                    return valign;
                },
                set: function (value) {
                    switch (value) {
                        case "top":
                        case "middle":
                        case "bottom":
                            valign = value;
                            this.y = y;
                            break;
                        default: console.error("垂直对其方式值应为 top/middle/bottom"); break;
                    }
                }
            }
        });
    }
    //文本框类
    function TextBox() {
        var text = "", id = "", size = "middle", color = "", intype = "none";
        var p = document.createElement("p");
        var span = document.createElement("span");
        p.classList.add("TextBox");
        p.classList.add("animated");
        p.appendChild(span)
        Object.defineProperties(this, {
            id: {
                enumerable: true,
                get: function () {
                    return id;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        id = val;
                        p.id = id;
                    }
                }
            },
            text: {
                enumerable: true,
                get: function () { return text; },
                set: function (val) {
                    if (typeof (val) == "string") {
                        text = val;
                        span.innerHTML = val
                    }
                    else {
                        console.error("文本值必须是字符串");
                    }
                }
            },
            size: {
                enumerable: true,
                get: function () { return size },
                set: function (val) {
                    switch (val) {
                        case "big":
                            size = val;
                            this.doc.style.fontSize = Math.floor(24 * zoom) + "px";
                            break;
                        case "middle":
                            size = val;
                            this.doc.style.fontSize = Math.floor(18 * zoom) + "px";
                            break;
                        case "small":
                            size = val;
                            this.doc.style.fontSize = Math.floor(14 * zoom) + "px";
                            break;
                        default: console.error("文本尺寸只能有 big/middle/small 三种"); break;

                    }
                }
            },
            color: {
                enumerable: true,
                get: function () { return color; },
                set: function (val) {
                    if (/^(#[0-9A-Fa-f]{3,8})|(rgb\(\d{1,3},\d{1,3},\d{1,3}\)|(rgba\(\d{1,3},\d{1,3},\d{1,3},\d{1,3}\)))$/.test(val)) {
                        color = val;
                        this.doc.style.color = color;
                    }
                    else {
                        console.error("颜色值填写错误");
                    }
                }
            },
            intype: {
                enumerable: true,
                get: function () { return intype; },
                set: function (val) {
                    switch (val) {
                        case "none":
                            intype = val;
                            break;
                        case "top":
                            intype = val;
                            p.classList.add("bounceInDown");
                            break;
                        case "bottom":
                            intype = val;
                            p.classList.add("bounceInUp");
                            break;
                        case "left":
                            intype = val;
                            p.classList.add("bounceInLeft");
                            break;
                        case "right":
                            intype = val;
                            p.classList.add("bounceInRight");
                            break;
                        default: console.error("进入状态应为 none/top/bottom/left/right 五种效果"); break;
                    }

                }
            },
            position: {
                enumerable: true,
                value: new Position(p)
            },

            doc: {
                value: p
            }
        });
        if (arguments.length > 0) {
            this.text = arguments[0];
        }
        if (arguments.length > 1) {
            this.size = arguments[1];
        }
        if (arguments.length > 2) {
            this.color = arguments[2];
        }
        if (arguments.length > 3) {
            this.intype = arguments[3];
        }
    }
    //图片框类
    function ImgBox() {
        var imgid = "", id = "", src = "", width, height, intype = "none";
        var div = document.createElement("div");
        div.classList.add("ImgBox");
        var img = document.createElement("img");
        div.appendChild(img);
        div.classList.add("animated")
        Object.defineProperties(this, {
            id: {
                enumerable: true,
                get: function () {
                    return id;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        id = val;
                        div.id = id;
                    }
                }
            },
            //图片id
            imgid: {
                enumerable: true,
                get: function () { return imgid; },
                set: function (val) {
                    if (typeof (val) == "string") {
                        imgid = val;
                    }
                    else {
                        console.error("文本值必须是字符串");
                    }
                }
            },
            //路径
            src: {
                enumerable: true,
                get: function () { return src; },
                set: function (val) {
                    if (typeof (val) == "string") {
                        src = val;
                        img.src = val;
                    }
                    else {
                        console.error("路径值必须是字符串");
                    }
                }
            },
            //图片宽度
            width: {
                enumerable: true,
                get: function () {
                    return width;
                },
                set: function (value) {
                    if (typeof (value) == "number") {
                        width = value;
                        this.doc.style.width = value * zoom + "px";
                    }
                    else {
                        console.error("宽度必须为数字");
                    }
                }
            },
            //图片高度
            height: {
                enumerable: true,
                get: function () {
                    return height;
                },
                set: function (value) {
                    if (typeof (value) == "number") {
                        height = value;
                        this.doc.style.height = value * zoom + "px";
                    }
                    else {
                        console.error("高度必须为数字");
                    }
                }
            },
            intype: {
                enumerable: true,
                get: function () { return intype; },
                set: function (val) {
                    switch (val) {
                        case "none":
                            intype = val;
                            break;
                        case "top":
                            intype = val;
                            div.classList.add("bounceInDown");
                            break;
                        case "bottom":
                            intype = val;
                            div.classList.add("bounceInUp");
                            break;
                        case "left":
                            intype = val;
                            div.classList.add("bounceInLeft");
                            break;
                        case "right":
                            intype = val;
                            div.classList.add("bounceInRight");
                            break;
                        default: console.error("进入状态应为 none/top/bottom/left/right 五种效果"); break;
                    }

                }
            },
            position: {
                enumerable: true,
                value: new Position(div)
            },
            doc: {
                value: div
            }
        });

        if (arguments.length > 0) {
            this.imgid = arguments[0];
        }
        if (arguments.length > 1) {
            this.src = arguments[1];
        }
        if (arguments.length > 2) {
            this.width = arguments[2];
        }
        if (arguments.length > 3) {
            this.height = arguments[3];
        }
        if (arguments.length > 4) {
            this.intype = arguments[4];
        }
    }
    //音频类
    function AudioBox() {
        var src = "", imgSrc = "", textSrc = "", audio = new Audio();
        Object.defineProperties(this, {
            src: {
                enumerable: true,
                get: function () {
                    return src;
                },
                set: function (val) {
                    if (typeof (val) == "string") {
                        audio.src = src = val;
                    }
                }
            },
            imgSrc: {
                enumerable: true,
                get: function () { return imgSrc; },
                set: function (val) {
                    if (typeof (val) == "string") {
                        imgSrc = val;
                    }
                }
            },
            textSrc: {
                enumerable: true,
                get: function () { return textSrc; },
                set: function (val) {
                    textSrc = val;
                }
            },
            //audio对象
            audio: {
                enumerable: true,
                get: function () {
                    return audio;
                }

            },
        });
        if (arguments.length > 0) {
            this.src = arguments[0];
        }
        if (arguments.length > 1) {
            this.imgSrc = arguments[1];
        }
        if (arguments.length > 2) {
            this.textSrc = arguments[2];
        }
    }

    function sortNumber(a, b) {
        return a - b
    }
    //数组去重
    function unique(arr) {
        if (!Array.isArray(arr)) {

            return;
        }
        arr = arr.sort()
        var arrry = [arr[0]];
        for (var i = 1; i < arr.length; i++) {
            if (arr[i] !== arr[i - 1]) {
                arrry.push(arr[i]);
            }
        }
        return arrry;
    }
    /* 判断是否是IE删除节点*/
    function removeDom(dom) {
        if (!!window.ActiveXobject || "ActiveXObject" in window) {
            dom.parentNode.removeChild(dom)
        } else {
            dom.remove();
        }
    }
    //获取元素距离页面左侧位置
    function getElementPageLeft(element) {
        var actualLeft = element.offsetLeft;
        var parent = element.offsetParent;
        while (parent != null) {
            actualLeft += parent.offsetLeft + (parent.offsetWidth - parent.clientWidth) / 2;
            parent = parent.offsetParent;
        }
        return actualLeft;
    }
    //获取元素距离页面顶部位置
    function getElementPageTop(element) {
        var actualTop = element.offsetTop;
        var parent = element.offsetParent;
        while (parent != null) {
            actualTop += parent.offsetTop + (parent.offsetHeight - parent.clientHeight) / 2;
            parent = parent.offsetParent;
        }
        return actualTop;
    }
    //格式化歌词
    function lrcToAudioTextArray(str) {
        var strlist = str.split('\n');
        var array = [];
        var timeRegexp = /\[\d{1,}:\d{1,2}\.\d{1,2}\]/i;
        for (var i = 0; i < strlist.length; i++) {
            var time = strlist[i].match(timeRegexp);
            if (time) {
                var text = strlist[i].replace(timeRegexp, "").trim();
                var timestr = time[0].replace(/\[|\]/g, "").split(":");
                var time = parseInt(timestr[0]) * 60 + parseFloat(timestr[1]);
                array.push([time, text]);
            }
        }
        return array;
    }
    //元素监听自身是否滚动到底
    function SrollMonitor(dom) {
        var scrollTop = dom.scrollTop();
        var ks_area = dom.innerHeight();
        var nScrollHight = 0; //滚动距离总长(注意不是滚动条的长度)  
        nScrollHight = dom[0].scrollHeight;
        if (scrollTop + Math.ceil(ks_area) >= nScrollHight - 10) {
            $(".ScrollDown").hide()
        }
    }
    var zoom = 1;
    return {
        //创建章节类方法
        createChapter: function createChapter(data) {
            return new Chapter(data);
        },
        //创建讲义类方法
        createPage: function createPage(data) {
            return new Page(data);
        },
        //创建步骤类方法
        createStep: function createStep(data) {
            return new Step(data);
        },
        //创建章节类方法
        createAction: function createAction(data) {
            switch (data.type) {
                case "setTitle": return new SetTitle(data);
                case "setBackground": return new SetBackground(data);
                case "setWaitMillisecond": return new SetWaitMillisecond(data);
                case "insertImg": return new InsertImg(data);
                case "insertText": return new InsertText(data);
                case "xiaoAiChange": return new XiaoAiChange(data);
                case "scaleDom": return new ScaleDom(data);
                case "moveDom": return new MoveDom(data);
                case "twinkleDom": return new TwinkleDom(data);
                case "hideDom": return new HideDom(data);
                case "xiaoAiSay": return new XiaoAiSay(data);
                case "studyAudio": return new StudyAudio(data);
                case "studyVideo": return new StudyVideo(data);
                case "studyArticle": return new StudyArticle(data);
                case "studyRecitation": return new StudyRecitation(data);
                case "studyFastReadEasy": return new StudyFastReadEasy(data);
                case "studyFastRead": return new StudyFastRead(data);
                case "studyJudgment": return new StudyJudgment(data);
                case "studyOption": return new StudyOption(data);
                case "studyLinking": return new StudyLinking(data);
                case "studyOptionFill": return new StudyOptionFill(data);
                case "studySubjective": return new StudySubjective(data);
                case "studyAnnotation": return new StudyAnnotation(data);
                case "studyAnnotation2": return new StudyAnnotation2(data);
                default: return new Action(data);
            }

        },
        //设置缩放比例
        setZoom: function (val) {
            if (typeof (val) == "number") {
                zoom = val;
            }
            else {
                console.error("缩放比例必须为数字");
            }
        },
        //获取缩放比例
        getZoom: function () {
            return zoom;
        }


    };
})();