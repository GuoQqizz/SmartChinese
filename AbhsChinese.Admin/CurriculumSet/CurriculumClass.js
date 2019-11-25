//课时类
function ChapterSet() {
    var courseId, id, name, setTime, status, pages = [];
    Object.defineProperties(this, {
        courseId: {
            enumerable: true,
            get: function () {
                return courseId;
            },
            set: function (val) {
                courseId = val;
            }
        },
        id: {
            enumerable: true,
            get: function () {
                return id;
            },
            set: function (val) {
                if (typeof (val) == "string" || typeof (val) == "number") {
                    id = val;
                }
            }
        },
        name: {
            enumerable: true,
            get: function () {
                return name;
            },
            set: function (val) {
                if (typeof (name) == "string") {
                    name = val;
                }
            }
        },
        setTime: {
            enumerable: true,
            get: function () {
                return setTime;
            },
            set: function (val) {
                if (typeof (setTime) == "string") {
                    setTime = val;
                }
            }
        },
        status: {
            enumerable: true,
            get: function () {
                return status;
            },
            set: function (val) {
                if (typeof (status) == "string") {
                    status = val;
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
                    page.pageNum = pages.length;
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
    });
    if (arguments.length > 0 && arguments[0]) {
        var data = arguments[0];
        if (typeof (data.index) != "undefined") {
            this.index = data.index;
        }
        if (typeof (data.courseId != "undefined")) {
            this.courseId = data.courseId;
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
    var id, page, name, maxzindex = 0, thumbnail = "", approveType = 0, approveAddress = "", select = "", steps = [], xline = [], yline = [], isSave = true;
    var titleseted = false;//标题是否设置过
    //设置属性
    Object.defineProperties(this, {
        id: {
            enumerable: true,
            get: function () { return id; },
            set: function (val) {
                if (typeof (val) == "number") {
                    id = val;
                }
                else {
                    console.error("id必须为数字类型");
                }
            }
        },
        //页码
        pageNum: {
            enumerable: true,
            get: function () { return page; },
            set: function (val) {
                if (typeof (val) == "number") {
                    page = val;
                }
                else {
                    console.error("页码必须为数字类型");
                }
            }
        },
        //讲义名称
        name: {
            enumerable: true,
            get: function () { return name; },
            set: function (val) {
                if (typeof (val) == "string") {
                    name = val;
                }
                else {
                    console.error("讲义名称应为字符类型");
                }
            }
        },
        //缩略图
        thumbnail: {
            enumerable: true,
            get: function () { return thumbnail; },
            set: function (val) {
                if (typeof (val) == "string") {
                    thumbnail = val;
                }
                else {
                    console.error("缩略图路径应为字符类型");
                }
            }
        },
        //审批状态(1:正常,2:错误,3:疑问)
        approveType: {
            get: function () { return approveType; },
            set: function (val) { approveType = val; }
        },
        //审批编码(用于学生端请求)
        approveAddress: {
            get: function () { return approveAddress; },
            set: function (val) { approveAddress = val; }
        },
        isSave: {
            get: function () { return isSave; },
            set: function (val) { isSave = val; }
        },
        //步骤列表副本
        steps: {
            enumerable: true,
            get: function () {
                return steps;
            }
        },
        //添加步骤
        addStep: {
            value: function addStep(step) {
                var page = this;
                if (step instanceof Step) {
                    isSave = false;
                    step.stepNum = steps.length;
                    Object.defineProperty(step, "parent", {
                        value: page
                    });
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
                    return;
                }
                if (!(typeof (t) == "number" && 0 <= t && t < steps.length)) {
                    console.error("移动至下表选择错误");
                    return;
                }
                isSave = false;
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
                    return;
                }
                isSave = false;
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
        //绘制
        draw: {
            value: function draw(stepNum) {
                if ($(select).length > 0) {
                    $(select).html("");
                    $(select).parent().css({ "background-color": "", "background-image": "" });
                    var length = (typeof (stepNum) != "undefined" ? (stepNum + 1) : steps.length);
                    for (var s = 0; s < length; s++) {
                        if (typeof (steps[s].draw) == "function") {
                            steps[s].draw();
                        }
                    }
                }
            }
        },
        show: {
            value: function show(index, callback) {
                if (index < steps.length) {
                    var _this = this;
                    _this.draw(index - 1);
                    steps[index].show(function () {
                        if (typeof (callback) == "function") {
                            callback(index);
                        }
                    })
                }
            }
        },
        //绘制区域
        selecter: {
            get: function () {
                return select;
            },
            set: function (val) {
                select = val;
            }
        },
        xline: {
            value: xline
        },
        yline: {
            value: yline
        },
        //获取最大的zindex
        maxZIndex: {
            value: function maxZIndex() {
                return ++maxzindex;
            }
        }
    });
    if (arguments.length > 0 && arguments[0]) {
        var data = arguments[0];
        if (typeof (data.id) != "undefined") {
            this.id = data.id;
        }
        if (typeof (data.pageNum) != "undefined") {
            this.pageNum = data.pageNum;
        }
        if (typeof (data.thumbnail) != "undefined" && data.thumbnail) {
            this.thumbnail = data.thumbnail;
        }
        if (typeof (data.name) != "undefined") {
            this.name = data.name;
        }
        if (typeof (data.selecter) != "undefined") {
            this.selecter = data.selecter;
        }
        if (typeof (data.steps) != "undefined") {
            for (var s in data.steps) {
                var step = new Step(data.steps[s]);
                this.addStep(step);
            }
            this.isSave = true;
        }
        if (typeof (data.approveType) != "undefined") {
            this.approveType = data.approveType;
        }
        if (typeof (data.approveAddress) != "undefined") {
            this.approveAddress = data.approveAddress;
        }
    }
}

//步骤类
function Step() {
    var index, actions = [];
    //设置属性
    Object.defineProperties(this, {
        //页码
        stepNum: {
            enumerable: true,
            get: function () { return index; },
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
                return actions;
            }
        },
        //添加步骤
        addAction: {
            value: function addAction(action) {
                var step = this;
                if (action instanceof Action) {
                    if (step.parent) {
                        step.parent.isSave = false;
                    }
                    action.actionNum = actions.length;
                    Object.defineProperty(action, "parent", {
                        value: step
                    });
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
                    return;
                }
                if (!(typeof (t) == "number" && 0 <= t && t < actions.length)) {
                    console.error("移动至下表选择错误");
                    return;
                }

                if (this.parent) {
                    this.parent.isSave = false;
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
                if (this.parent) {
                    this.parent.isSave = false;
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
        //绘制
        draw: {
            value: function draw() {
                for (var a in actions) {
                    if (typeof (actions[a].draw) == "function") {
                        actions[a].draw();
                    }
                }
            }
        },
        //预览方法
        show: {
            value: function show(callback) {
                if (actions.length > 0) {//如果有动作
                    var t = 0, c = 0;
                    //t用于循环actions使用,因为setTimeOut后a得到的是最后一个.所以使用一个外部的t来在定时器内部进行循环
                    //c用于记录完成的动作数量
                    //ThisStep用于记录Step自身,防止this混淆
                    for (var a in actions) {//遍历所有的动作
                        //启用定时器进行模仿异步操作
                        setTimeout(function () {
                            var action = actions[t++];//循环获取值
                            console.log("Step" + action.parent.stepNum, "action" + action.actionNum, action.type, "is start");//记录自身动作信息
                            action.show(function () {
                                c++;//增加完成的数量
                                if (c == actions.length) {//如果全部执行完成
                                    callback();//执行Step的完成回调
                                }
                            });//设置自身执行完成时回调);//执行自己的动作
                        }, 1);
                    }
                }
                else {
                    callback();
                }
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
                    case "xiaoAiSay": action = new XiaoAiSay(d); break;
                    case "xiaoAiChange": action = new XiaoAiChange(d); break;
                    case "insertImg": action = new InsertImg(d); break;
                    case "insertText": action = new InsertText(d); break;
                    case "setWaitMillisecond": action = new SetWaitMillisecond(d); break;
                    case "moveDom": action = new MoveDom(d); break;
                    case "scaleDom": action = new ScaleDom(d); break;
                    case "twinkleDom": action = new TwinkleDom(d); break;
                    case "hideDom": action = new HideDom(d); break;
                    case "studyAudio": action = new StudyAudio(d); break;
                    case "studyVideo": action = new StudyVideo(d); break;
                    case "studyArticle": action = new StudyArticle(d); break;
                    case "studyRecitation": action = new StudyRecitation(d); break;
                    case "studyFastReadEasy": action = new StudyFastReadEasy(d); break;
                    case "studyFastRead": action = new StudyFastRead(d); break;
                    case "studyAnnotation": action = new StudyAnnotation(d); break;
                    case "studyAnnotation2": action = new StudyAnnotation2(d); break;
                    case "studyJudgment": action = new StudyJudgment(d); break;
                    case "studyLinking": action = new StudyLinking(d); break;
                    case "studyOption": action = new StudyOption(d); break;
                    case "studyOptionFill": action = new StudyOptionFill(d); break;
                    case "studyFill": action = new StudyFill(d); break;
                    case "studySubjective": action = new StudySubjective(d); break;
                    case "studyCalligraphy": action = new StudyCalligraphy(d); break;
                    default: action = new Action(d); break;
                }
                this.addAction(action);
            }
        }
    }
}

//动作类
function Action() {
    var index = 0, actionId, type;
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
        //动作id
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
        //动作类型
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
        //绘制方法
        draw: {
            writable: true,
            value: function draw() {

            }
        },
        //预览方法
        show: {
            writable: true,
            value: function show(callback) {

            }
        }
    });
}

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
                            doc.style.left = x + "px";
                            doc.style.right = "";
                            break;
                        case "center":
                            //doc.style.left = x + "px";
                            //doc.style.marginLeft = (0 - doc.offsetWidth / 2) + "px";
                            break;
                        case "right":
                            doc.style.right = x + "px";
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
                            doc.style.top = y + "px";
                            doc.style.bottom = "";
                            break;
                        case "middle":
                            //doc.style.top = y * zoom + "px";
                            //doc.style.marginTop = (0 - doc.offsetHeight / 2) + "px";
                            break;
                        case "bottom":
                            doc.style.bottom = y + "px";
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
    p.classList.add("TextBox");
    p.classList.add("animated");
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
                    this.doc.innerHTML = val;
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
                        this.doc.style.fontSize = Math.floor(24) + "px";
                        break;
                    case "middle":
                        size = val;
                        this.doc.style.fontSize = Math.floor(18) + "px";
                        break;
                    case "small":
                        size = val;
                        this.doc.style.fontSize = Math.floor(14) + "px";
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
                    case "top":
                    case "bottom":
                    case "left":
                    case "right":
                        intype = val;
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
    var imgid = "", id, name = "", src = "", width, height, intype = "none";
    var div = document.createElement("div");
    div.classList.add("ImgBox");
    var img = document.createElement("img");
    img.draggable = false;
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
        imgname: {
            enumerable: true,
            get: function () { return name; },
            set: function (val) {
                val = val || "";
                if (typeof (val) == "string") {
                    name = val;
                    img.title = val;
                }
                else {
                    console.error("名称值必须是字符串");
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
                    this.doc.style.width = value + "px";
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
                    this.doc.style.height = value + "px";
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
                    case "top":
                    case "bottom":
                    case "left":
                    case "right":
                        intype = val;
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

//添加新动作模板
//function ChildDom() {
//    var index = 0, actionId;
//    Object.defineProperties(this, {
//        //动作序号
//        actionNum: {
//            enumerable: true,
//            get: function () { return index; },
//            set: function (val) {
//                if (typeof (val) == "number") {
//                    index = val;
//                } else {
//                    console.error("动作序号应为数字类型");
//                }
//            }
//        },
//        actionId: {
//            enumerable: true,
//            get: function () { return actionId; },
//            set: function (val) {
//                if (typeof (val) == "string") {
//                    actionId = val;
//                } else {
//                    console.error("动作Id应为字符串类型");
//                }
//            }
//        },
//        type: {
//            enumerable: true,
//            value: "childDom"/*需要改为方法名,首字母小写*/
//        },
//        /*在这里追加属性*/
//        draw: {
//            value: function draw() {
//                if (typeof (this.parent.parent) != "undefined" && this.parent.parent instanceof Page) {
//                }
//            }
//        }
//    });
//    if (arguments.length != 0) {
//        var data = arguments[0];
//        if (typeof (data.actionId) != "undefined") {
//            this.actionId = data.actionId;
//        }
//        if (typeof (data.actionNum) != "undefined") {
//            this.actionNum = data.actionNum;
//        }
//        /*这里追加属性的初始化*/
//    }
//}
//ChildDom.prototype = new Action();

//绑定动画完成事件回调
function bindAnimationend(obj, classname, callback) {
    $(obj).on("animationend", function (e) {
        if (e.originalEvent.animationName == classname) {
            $(obj).removeClass(classname);
            callback();
        }
    });
}

//设置标题
function SetTitle() {
    var index = 0, actionId, text = new TextBox();
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
        //动作id
        actionId: {
            enumerable: true,
            get: function () { return actionId; },
            set: function (val) {
                if (typeof (val) == "string") {
                    actionId = val;
                    text.id = val;
                } else {
                    console.error("动作Id应为字符串类型");
                }
            }
        },
        //文本内容
        type: {
            enumerable: true,
            value: "setTitle"
        },
        //文本值
        text: {
            enumerable: true,
            get: function () { return text.text; },
            set: function (val) {
                if (typeof (val) == "string") {
                    text.text = val;
                }
            }
        },
        //尺寸
        size: {
            enumerable: true,
            get: function () { return text.size },
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
            get: function () { return text.color; },
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
        //进入类型
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
        },
        //绘制
        draw: {
            value: function draw() {
                if (typeof (this.parent.parent) != "undefined" && this.parent.parent instanceof Page) {
                    text.doc.style.zIndex = this.parent.parent.maxZIndex();
                    $(this.parent.parent.selecter).append(text.doc);
                }
            }
        },
        //预览方法
        show: {
            value: function show(callback) {
                text.doc.style.zIndex = this.parent.parent.maxZIndex();
                $(this.parent.parent.selecter).append(text.doc);
                switch (this.intype) {
                    case "top":
                        text.doc.classList.add("bounceInDown");
                        bindAnimationend(text.doc, "bounceInDown", callback);
                        break;
                    case "bottom":
                        text.doc.classList.add("bounceInUp");
                        bindAnimationend(text.doc, "bounceInUp", callback);
                        break;
                    case "left":
                        text.doc.classList.add("bounceInLeft");
                        bindAnimationend(text.doc, "bounceInLeft", callback);
                        break;
                    case "right":
                        text.doc.classList.add("bounceInRight");
                        bindAnimationend(text.doc, "bounceInRight", callback);
                        break;
                    default: callback(); break;
                }

            }
        }
    });
    if (arguments.length > 0) {
        data = arguments[0];
        if (typeof (data.actionId) != "undefined") {
            this.actionId = data.actionId;
        }
        if (typeof (data.actionNum) != "undefined") {
            this.actionNum = data.actionNum;
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
    var index = 0, type, bg, bgUrl, bgName, actionId;
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
            value: "setBackground"
        },
        bgType: {
            enumerable: true,
            get: function () {
                return type;
            },
            set: function (val) {
                switch (val) {
                    case "image":
                    case "color":
                        type = val;
                        break;
                    default: console.error("背景类型必须是 image/color 两种类型"); break;
                }
            }
        },
        //颜色值或者图片id
        bg: {
            enumerable: true,
            get: function () {
                return bg;
            },
            set: function (val) {
                bg = val;
            }
        },
        //背景图片链接
        bgUrl: {
            enumerable: true,
            get: function () {
                return bgUrl;
            },
            set: function (val) {
                bgUrl = val;
            }
        },
        bgName: {
            enumerable: true,
            get: function () {
                return bgName;
            },
            set: function (val) {
                bgName = val;
            }
        },
        draw: {
            value: function draw() {
                if (typeof (this.parent.parent) != "undefined" && this.parent.parent instanceof Page) {
                    if (this.bgType == "color") {
                        $(this.parent.parent.selecter).parent().css("background-color", bg);
                        $(this.parent.parent.selecter).parent().css("background-image", "");
                    }
                    else {
                        $(this.parent.parent.selecter).parent().css("background-color", "");
                        $(this.parent.parent.selecter).parent().css("background-image", "url(" + bgUrl + ")");
                    }
                }
            }
        },
        //预览方法
        show: {
            value: function show(callback) {
                if (typeof (this.parent.parent) != "undefined" && this.parent.parent instanceof Page) {
                    if (this.bgType == "color") {
                        $(this.parent.parent.selecter).parent().css("background-color", bg);
                        $(this.parent.parent.selecter).parent().css("background-image", "");
                    }
                    else {
                        $(this.parent.parent.selecter).parent().css("background-color", "");
                        $(this.parent.parent.selecter).parent().css("background-image", "url(" + bg + ")");
                    }
                }
                callback();
            }
        }
    });

    if (arguments.length != 0) {
        var data = arguments[0];
        if (typeof (data.actionId) != "undefined") {
            this.actionId = data.actionId;
        }
        if (typeof (data.actionNum) != "undefined") {
            this.actionNum = data.actionNum;
        }
        if (typeof (data.bgType) != "undefined") {
            this.bgType = data.bgType;
        }
        if (typeof (data.bg) != "undefined") {
            this.bg = data.bg;
        }
        if (typeof (data.bgUrl) != "undefined") {
            this.bgUrl = data.bgUrl;
        }
        if (typeof (data.bgName) != "undefined") {
            this.bgName = data.bgName;
        }
    }
}
SetBackground.prototype = new Action();

//小艾说
function XiaoAiSay() {
    var index = 0, actionId, mediaid, medianame, src;
    var audio = new Audio();

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
        mediaid: {
            enumerable: true,
            get: function () {
                return mediaid;
            },
            set: function (val) {
                if (typeof (val) == "string") {
                    mediaid = val;
                } else {
                    console.error("小艾说媒体id应为字符串类型");
                }
            }
        },
        medianame: {
            enumerable: true,
            get: function () {
                return medianame;
            },
            set: function (val) {
                if (typeof (val) == "string") {
                    medianame = val;
                } else {
                    console.error("小艾说名称应为字符串类型");
                }
            }
        },
        src: {
            enumerable: true,
            get: function () {
                return src;
            },
            set: function (val) {
                if (typeof (val) == "string") {
                    src = val;
                    audio.src = val;
                } else {
                    console.error("小艾说路径应为字符串类型");
                }
            }
        },
        draw: {
            value: function draw() {
                if (typeof (this.parent.parent) != "undefined" && this.parent.parent instanceof Page) {

                }
            }
        },
        //预览方法
        show: {
            value: function show(callback) {
                audio.play();
                audio.onended = function () {
                    callback();
                }
            }
        }
    });
    if (arguments.length != 0) {
        var data = arguments[0];

        if (typeof (data.actionId) != "undefined") {
            this.actionId = data.actionId;
        }
        if (typeof (data.actionNum) != "undefined") {
            this.actionNum = data.actionNum;
        }
        if (typeof (data.mediaid) != "undefined") {
            this.mediaid = data.mediaid;
        }
        if (typeof (data.medianame) != "undefined") {
            this.medianame = data.medianame;
        }
        if (typeof (data.src) != "undefined") {
            this.src = data.src;
        }
    }
}
XiaoAiSay.prototype = new Action();

//小艾变
function XiaoAiChange() {
    var index = 0, actionId, img = new ImgBox();
    img.id = "xiaoAiChangeImg";
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
            value: "xiaoAiChange"
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
        //图片名称
        imgName: {
            enumerable: true,
            get: function () {
                return img.imgname;
            },
            set: function (val) {
                img.imgname = val;
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
            get: function () { return img.height; },
            set: function (val) {
                img.height = val;
            }
        },
        //水平对齐方式
        align: {
            enumerable: true,
            get: function () { return img.position.align; },
            set: function (val) {
                img.position.align = val;
            }
        },
        //垂直对齐方式
        valign: {
            enumerable: true,
            get: function () { return img.position.valign; },
            set: function (val) {
                img.position.valign = val;
            }
        },
        //x坐标
        x: {
            enumerable: true,
            get: function () { return img.position.x; },
            set: function (val) {
                img.position.x = val;
            }
        },
        //y坐标
        y: {
            enumerable: true,
            get: function () { return img.position.y; },
            set: function (val) {
                img.position.y = val;
            }
        },
        //绘制
        draw: {
            value: function draw() {
                if (typeof (this.parent.parent) != "undefined" && this.parent.parent instanceof Page) {
                    if ($(this.parent.parent.selecter).find("#xiaoAiChangeImg").length == 0) {
                        img.doc.style.zIndex = this.parent.parent.maxZIndex();
                        $(this.parent.parent.selecter).append(img.doc);
                    }
                    $(this.parent.parent.selecter).find("#xiaoAiChangeImg").width(this.width)
                     .height(this.height).css({ "top": this.y + "px", "left": this.x + "px" })
                     .children("img").attr("src", this.src);
                }
            }
        },
        //预览方法
        show: {
            value: function show(callback) {
                if (typeof (this.parent.parent) != "undefined" && this.parent.parent instanceof Page) {
                    if ($(this.parent.parent.selecter).find("#xiaoAiChangeImg").length == 0) {
                        img.doc.style.zIndex = this.parent.parent.maxZIndex();
                        $(this.parent.parent.selecter).append(img.doc);
                    }
                    $(this.parent.parent.selecter).find("#xiaoAiChangeImg").width(this.width)
                     .height(this.height).css({ "top": this.y + "px", "left": this.x + "px" })
                     .children("img").attr("src", this.src);
                }
                callback();
            }
        }
    });
    if (arguments.length != 0) {
        var data = arguments[0];

        if (typeof (data.actionId) != "undefined") {
            this.actionId = data.actionId;
        }
        if (typeof (data.actionNum) != "undefined") {
            this.actionNum = data.actionNum;
        }
        if (typeof (data.imgId) != "undefined") {
            this.imgId = data.imgId;
        }
        if (typeof (data.imgName) != "undefined") {
            this.imgName = data.imgName;
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
    }
}
XiaoAiChange.prototype = new Action();

//插入图片
function InsertImg() {
    var index = 0, actionId, img = new ImgBox();
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
        //图片名称
        imgName: {
            enumerable: true,
            get: function () {
                return img.imgname;
            },
            set: function (val) {
                img.imgname = val;
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
            get: function () { return img.height; },
            set: function (val) {
                img.height = val;
            }
        },
        //水平对齐方式
        align: {
            enumerable: true,
            get: function () { return img.position.align; },
            set: function (val) {
                img.position.align = val;
            }
        },
        //垂直对齐方式
        valign: {
            enumerable: true,
            get: function () { return img.position.valign; },
            set: function (val) {
                img.position.valign = val;
            }
        },
        //x坐标
        x: {
            enumerable: true,
            get: function () { return img.position.x; },
            set: function (val) {
                img.position.x = val;
            }
        },
        //y坐标
        y: {
            enumerable: true,
            get: function () { return img.position.y; },
            set: function (val) {
                img.position.y = val;
            }
        },
        //进入动画
        intype: {
            enumerable: true,
            get: function () {
                return img.intype;
            },
            set: function (value) {
                img.intype = value;
            }
        },
        //绘制
        draw: {
            value: function draw() {
                if (typeof (this.parent.parent) != "undefined" && this.parent.parent instanceof Page) {
                    img.doc.style.zIndex = this.parent.parent.maxZIndex();
                    $(this.parent.parent.selecter).append(img.doc);
                }
            }
        },
        //预览方法
        show: {
            value: function show(callback) {
                if (typeof (this.parent.parent) != "undefined" && this.parent.parent instanceof Page) {
                    img.doc.style.zIndex = this.parent.parent.maxZIndex();
                    $(this.parent.parent.selecter).append(img.doc);
                    switch (this.intype) {
                        case "top":
                            img.doc.classList.add("bounceInDown");
                            bindAnimationend(img.doc, "bounceInDown", callback);
                            break;
                        case "bottom":
                            img.doc.classList.add("bounceInUp");
                            bindAnimationend(img.doc, "bounceInUp", callback);
                            break;
                        case "left":
                            img.doc.classList.add("bounceInLeft");
                            bindAnimationend(img.doc, "bounceInLeft", callback);
                            break;
                        case "right":
                            img.doc.classList.add("bounceInRight");
                            bindAnimationend(img.doc, "bounceInRight", callback);
                            break;
                        default: callback(); break;
                    }
                }

            }
        }
    });
    if (arguments.length != 0) {
        var data = arguments[0];

        if (typeof (data.actionId) != "undefined") {
            this.actionId = data.actionId;
        }
        if (typeof (data.actionNum) != "undefined") {
            this.actionNum = data.actionNum;
        }
        if (typeof (data.imgId) != "undefined") {
            this.imgId = data.imgId;
        }
        if (typeof (data.imgName) != "undefined") {
            this.imgName = data.imgName;
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

//插入文本
function InsertText() {
    var index = 0, actionId, text = new TextBox();
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
            get: function () { return text.text; },
            set: function (val) {
                if (typeof (val) == "string") {
                    text.text = val;
                }
            }
        },
        //尺寸
        size: {
            enumerable: true,
            get: function () { return text.size },
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
            get: function () { return text.color; },
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
        //进入动画
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
        },
        //绘制
        draw: {
            value: function draw() {
                if (typeof (this.parent.parent) != "undefined" && this.parent.parent instanceof Page) {
                    text.doc.style.zIndex = this.parent.parent.maxZIndex();
                    $(this.parent.parent.selecter).append(text.doc);
                }
            }
        },
        //预览方法
        show: {
            value: function show(callback) {
                if (typeof (this.parent.parent) != "undefined" && this.parent.parent instanceof Page) {
                    text.doc.style.zIndex = this.parent.parent.maxZIndex();
                    $(this.parent.parent.selecter).append(text.doc);
                    switch (this.intype) {
                        case "top":
                            text.doc.classList.add("bounceInDown");
                            bindAnimationend(text.doc, "bounceInDown", callback);
                            break;
                        case "bottom":
                            text.doc.classList.add("bounceInUp");
                            bindAnimationend(text.doc, "bounceInUp", callback);
                            break;
                        case "left":
                            text.doc.classList.add("bounceInLeft");
                            bindAnimationend(text.doc, "bounceInLeft", callback);
                            break;
                        case "right":
                            text.doc.classList.add("bounceInRight");
                            bindAnimationend(text.doc, "bounceInRight", callback);
                            break;
                        default: callback(); break;
                    }
                }

            }
        }
    });
    if (arguments.length != 0) {
        var data = arguments[0];

        if (typeof (data.actionId) != "undefined") {
            this.actionId = data.actionId;
        }
        if (typeof (data.actionNum) != "undefined") {
            this.actionNum = data.actionNum;
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

//等待
function SetWaitMillisecond() {
    var index = 0, ms = 0, actionId;
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
        },
        //预览方法
        show: {
            value: function show(callback) {
                setTimeout(callback, this.stop);
            }
        }
    });
    if (arguments.length != 0) {
        var data = arguments[0];

        if (typeof (data.actionId) != "undefined") {
            this.actionId = data.actionId;
        }
        if (typeof (data.actionNum) != "undefined") {
            this.actionNum = data.actionNum;
        }
        if (typeof (data.stop) != "undefined") {
            this.stop = data.stop;
        }
    }

}
SetWaitMillisecond.prototype = new Action();

//移动
function MoveDom() {
    var objectId = "", ox = 0, oy = 0, x = 0, y = 0, duration = 1000;
    var index = 0, actionId;
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
            value: "moveDom"
        },
        x: {
            enumerable: true,
            get: function () {
                return x;
            },
            set: function (val) {
                if (typeof (val) == "number") {
                    x = val;
                }
                else {
                    console.error("移动位置x坐标必须为数字")
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
                }
                else {
                    console.error("移动位置y坐标必须为数字")
                }
            }
        },
        ox: {
            enumerable: true,
            get: function () {
                return ox;
            },
            set: function (val) {
                if (typeof (val) == "number") {
                    ox = val;
                } else {
                    console.error("原始x坐标必须为数字");
                }
            }
        },
        oy: {
            enumerable: true,
            get: function () {
                return oy;
            },
            set: function (val) {
                if (typeof (val) == "number") {
                    oy = val;
                } else {
                    console.error("原始y坐标必须为数字");
                }
            }
        },
        duration: {
            enumerable: true,
            get: function () {
                return duration;
            },
            set: function (val) {
                if (typeof (val) == "number") {
                    duration = val;
                }
                else {
                    console.error("移动位置耗时(duration)必须为数字")
                }
            }
        },
        objectId: {
            enumerable: true,
            get: function () {
                return objectId;
            },
            set: function (val) {
                if (typeof (val) == "string") {
                    objectId = val;
                }
                else {
                    console.error("移动元素id必须为字符串");
                }
            }
        },
        draw: {
            value: function draw() {
                if (typeof (this.parent.parent) != "undefined" && this.parent.parent instanceof Page) {
                    var selecter = $(this.parent.parent.selecter);
                    selecter.find("#" + this.objectId).css({ "top": y + "px", "left": x + "px", "z-index": this.parent.parent.maxZIndex() });
                }
            }
        },
        //预览方法
        show: {
            value: function show(callback) {
                if (typeof (this.parent.parent) != "undefined" && this.parent.parent instanceof Page) {
                    var selecter = $(this.parent.parent.selecter);
                    selecter.find("#" + this.objectId).animate({ "top": y + "px", "left": x + "px", "z-index": this.parent.parent.maxZIndex() }, this.duration, callback);
                }
            }
        }
    });
    if (arguments.length != 0) {
        var data = arguments[0];

        if (typeof (data.actionId) != "undefined") {
            this.actionId = data.actionId;
        }
        if (typeof (data.actionNum) != "undefined") {
            this.actionNum = data.actionNum;
        }
        if (typeof (data.x) != "undefined") {
            this.x = data.x;
        }
        if (typeof (data.y) != "undefined") {
            this.y = data.y;
        }
        if (typeof (data.ox) != "undefined") {
            this.ox = data.ox;
        }
        if (typeof (data.oy) != "undefined") {
            this.oy = data.oy;
        }
        if (typeof (data.duration) != "undefined") {
            this.duration = data.duration;
        }
        if (typeof (data.objectId) != "undefined") {
            this.objectId = data.objectId;
        }
    }

}
MoveDom.prototype = new Action();

//缩放
function ScaleDom() {
    var index = 0, actionId;
    var objectId = "", ratio = 1.1, num = 1, duration = 1000;
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
                }
                else {
                    console.error("缩放耗时(duration)必须为数字")
                }
            }
        },
        objectId: {
            enumerable: true,
            get: function () {
                return objectId;
            },
            set: function (val) {
                if (typeof (val) == "string") {
                    objectId = val;
                }
                else {
                    console.error("缩放元素id必须为字符串");
                }
            }
        },
        ratio: {
            enumerable: true,
            get: function () { return ratio; },
            set: function (val) { if (typeof (val) == "number") { ratio = val; } else { console.error("缩放大小必须为数字") } }
        },
        num: {
            enumerable: true,
            get: function () { return num; },
            set: function (val) { if (typeof (val) == "number") { num = val; } else { console.error("缩放次数必须为数字") } }
        },
        draw: {
            value: function draw() {
                if (typeof (this.parent.parent) != "undefined" && this.parent.parent instanceof Page) {
                    var selecter = $(this.parent.parent.selecter);
                    selecter.find("#" + this.objectId).css({ "z-index": this.parent.parent.maxZIndex() });
                }
            }
        },
        //预览方法
        show: {
            value: function show(callback) {
                if (typeof (this.parent.parent) != "undefined" && this.parent.parent instanceof Page) {
                    var str = "myAnimate" + Math.floor(Math.random() * 10000);
                    var runkeyframes = "." + str + "{z-index:999;animation:" + str + " " + duration / 1000 + "s linear 0s " + num + " normal;-webkit-animation:" + str + " " + duration / 1000 + "s linear 0s " + num + " normal;} @keyframes " + str + " {0% { -webkit-transform: scale(1);transform: scale(1);} 50% { -webkit-transform: scale(" + ratio + ");transform: scale(" + ratio + ");} 100% { -webkit-transform: scale(1);transform: scale(1);}}"
                    // 创建style标签
                    var styles = document.createElement('style');
                    // 设置style属性
                    styles.type = 'text/css';
                    // 将 keyframes样式写入style内
                    styles.innerHTML = runkeyframes;
                    // 将style样式存放到head标签
                    $('head').append(styles);

                    var selecter = $(this.parent.parent.selecter);
                    var obj = selecter.find("#" + this.objectId);
                    var _this = this;
                    bindAnimationend(obj, str, function () {
                        $(styles).remove();
                        obj.css("z-index", _this.parent.parent.maxZIndex());
                        callback();
                    });
                    obj.addClass(str);
                }
            }
        }
    });
    if (arguments.length != 0) {
        var data = arguments[0];

        if (typeof (data.actionId) != "undefined") {
            this.actionId = data.actionId;
        }
        if (typeof (data.actionNum) != "undefined") {
            this.actionNum = data.actionNum;
        }
        if (typeof (data.duration) != "undefined") {
            this.duration = data.duration;
        }
        if (typeof (data.objectId) != "undefined") {
            this.objectId = data.objectId;
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

//闪烁
function TwinkleDom() {
    var index = 0, actionId;
    var objectId = "", num = 1, duration = 1000;
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
                }
                else {
                    console.error("闪烁耗时(duration)必须为数字")
                }
            }
        },
        objectId: {
            enumerable: true,
            get: function () {
                return objectId;
            },
            set: function (val) {
                if (typeof (val) == "string") {
                    objectId = val;
                }
                else {
                    console.error("闪烁元素id必须为字符串");
                }
            }
        },
        num: {
            enumerable: true,
            get: function () { return num; },
            set: function (val) { if (typeof (val) == "number") { num = val; } else { console.error("闪烁次数必须为数字") } }
        },
        draw: {
            value: function draw() {
                if (typeof (this.parent.parent) != "undefined" && this.parent.parent instanceof Page) {
                    var selecter = $(this.parent.parent.selecter);
                    selecter.find("#" + this.objectId).css({ "z-index": this.parent.parent.maxZIndex() });
                }
            }
        },
        //预览方法
        show: {
            value: function show(callback) {
                if (typeof (this.parent.parent) != "undefined" && this.parent.parent instanceof Page) {
                    var str = "twinkleDom" + Math.floor(Math.random() * 10000);
                    var runkeyframes = "." + str + "{z-index:999;animation:" + str + " " + duration / 1000 + "s linear 0s " + num + " normal;-webkit-animation:" + str + " " + duration / 1000 + "s linear 0s " + num + " normal;} @keyframes " + str + " {0% { opacity: 0;} 50% { opacity: 1;} 100% { opacity: 0;}}"
                    // 创建style标签
                    var styles = document.createElement('style');
                    // 设置style属性
                    styles.type = 'text/css';
                    // 将 keyframes样式写入style内
                    styles.innerHTML = runkeyframes;
                    // 将style样式存放到head标签
                    $('head').append(styles);

                    var selecter = $(this.parent.parent.selecter);
                    var obj = selecter.find("#" + this.objectId);
                    var _this = this;
                    bindAnimationend(obj, str, function () {
                        $(styles).remove();
                        obj.css("z-index", _this.parent.parent.maxZIndex());
                        callback();
                    });
                    obj.addClass(str);
                }
            }
        }
    });
    if (arguments.length != 0) {
        var data = arguments[0];

        if (typeof (data.actionId) != "undefined") {
            this.actionId = data.actionId;
        }
        if (typeof (data.actionNum) != "undefined") {
            this.actionNum = data.actionNum;
        }
        if (typeof (data.duration) != "undefined") {
            this.duration = data.duration;
        }
        if (typeof (data.objectId) != "undefined") {
            this.objectId = data.objectId;
        }
        if (typeof (data.num) != "undefined") {
            this.num = data.num;
        }
    }
}
TwinkleDom.prototype = new Action();

//隐藏
function HideDom() {
    var index = 0, actionId, outtype = "none", objectId = "";
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
            value: "hideDom"
        },
        objectId: {
            enumerable: true,
            get: function () {
                return objectId;
            },
            set: function (val) {
                if (typeof (val) == "string") {
                    objectId = val;
                }
                else {
                    console.error("隐藏元素id必须为字符串");
                }
            }
        },
        outtype: {
            enumerable: true,
            get: function () { return outtype; },
            set: function (val) {
                switch (val) {
                    case "none":
                        outtype = val;
                        break;
                    case "top":
                        outtype = val;
                        break;
                    case "bottom":
                        outtype = val;
                        break;
                    case "left":
                        outtype = val;
                        break;
                    case "right":
                        outtype = val;
                        break;
                    default: console.error("隐藏状态应为 none/top/bottom/left/right 五种效果"); break;
                }
            }
        },
        draw: {
            value: function draw() {
                if (typeof (this.parent.parent) != "undefined" && this.parent.parent instanceof Page) {
                    var selecter = $(this.parent.parent.selecter);
                    selecter.find("#" + this.objectId).hide();
                }
            }
        },
        //预览方法
        show: {
            value: function show(callback) {
                if (typeof (this.parent.parent) != "undefined" && this.parent.parent instanceof Page) {

                    var selecter = $(this.parent.parent.selecter);
                    var obj = selecter.find("#" + this.objectId);
                    switch (this.outtype) {
                        case "top":
                            text.doc.classList.add("bounceOutUp");
                            bindAnimationend(obj, "bounceInDown", callback);
                            break;
                        case "bottom":
                            text.doc.classList.add("bounceOutDown");
                            bindAnimationend(obj, "bounceInUp", callback);
                            break;
                        case "left":
                            text.doc.classList.add("bounceOutLeft");
                            bindAnimationend(obj, "bounceInLeft", callback);
                            break;
                        case "right":
                            text.doc.classList.add("bounceOutRight");
                            bindAnimationend(obj, "bounceInRight", callback);
                            break;
                        default: callback(); break;
                    }
                }
            }
        }
    });
    if (arguments.length != 0) {
        var data = arguments[0];

        if (typeof (data.actionId) != "undefined") {
            this.actionId = data.actionId;
        }
        if (typeof (data.actionNum) != "undefined") {
            this.actionNum = data.actionNum;
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

//音频
function StudyAudio() {
    var index = 0, actionId, mediaid = "0", medianame = "", src = "", imgsrc = "", text = "", kcbid = "0", kcbsrc = "", kcbtext = "", wordPosition = "top", goldCoins = 0;
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
        mediaid: {
            enumerable: true,
            get: function () { return mediaid; },
            set: function (val) {
                if (typeof (val) == "string") {
                    mediaid = val;
                } else {
                    console.error("媒体id应为字符串类型");
                }
            }
        },
        medianame: {
            enumerable: true,
            get: function () { return medianame; },
            set: function (val) {
                if (typeof (val) == "string") {
                    medianame = val;
                } else {
                    console.error("媒体名称应为字符串类型");
                }
            }
        },
        src: {
            enumerable: true,
            get: function () { return src; },
            set: function (val) {
                if (typeof (val) == "string") {
                    src = val;
                } else {
                    console.error("媒体路径应为字符串类型");
                }
            }
        },
        imgsrc: {
            enumerable: true,
            get: function () { return imgsrc; },
            set: function (val) {
                if (typeof (val) == "string") {
                    imgsrc = val;
                } else {
                    console.error("媒体路径应为字符串类型");
                }
            }
        },
        text: {
            enumerable: true,
            get: function () { return text; },
            set: function (val) {
                if (typeof (val) == "string") {
                    text = val;
                } else {
                    console.error("媒体文字应为字符串类型");
                }
            }
        },
        kcbid: {
            enumerable: true,
            get: function () { return kcbid; },
            set: function (val) {
                if (typeof (val) == "string") {
                    kcbid = val;
                } else {
                    console.error("开场白id应为字符串类型");
                }
            }
        },
        kcbsrc: {
            enumerable: true,
            get: function () { return kcbsrc; },
            set: function (val) {
                if (typeof (val) == "string") {
                    kcbsrc = val;
                } else {
                    console.error("媒体路径应为字符串类型");
                }
            }
        },
        kcbtext: {
            enumerable: true,
            get: function () { return kcbtext; },
            set: function (val) {
                if (typeof (val) == "string") {
                    kcbtext = val;
                } else {
                    console.error("媒体文本应为字符串类型");
                }
            }
        },
        wordPosition: {
            enumerable: true,
            get: function () { return wordPosition; },
            set: function (val) {
                if (typeof (val) == "string") {
                    wordPosition = val;
                } else {
                    console.error("媒体id应为字符串类型");
                }
            }
        },
        goldCoins: {
            enumerable: true,
            get: function () { return goldCoins; },
            set: function (val) {
                if (typeof (val) == "number") {
                    goldCoins = val;
                } else {
                    console.error("金币必须是数字");
                }
            }
        },
        draw: {
            value: function draw() {
                if (typeof (this.parent.parent) != "undefined" && this.parent.parent instanceof Page) {

                }
            }
        },
        //预览方法
        show: {
            value: function show(callback) {

            }
        }
    });
    if (arguments.length != 0) {
        var data = arguments[0];

        if (typeof (data.actionId) != "undefined") {
            this.actionId = data.actionId;
        }
        if (typeof (data.actionNum) != "undefined") {
            this.actionNum = data.actionNum;
        }
        if (typeof (data.mediaid) != "undefined") {
            this.mediaid = data.mediaid;
        }
        if (typeof (data.medianame) != "undefined") {
            this.medianame = data.medianame;
        }
        if (typeof (data.src) != "undefined") {
            this.src = data.src;
        }
        if (typeof (data.imgsrc) != "undefined") {
            this.imgsrc = data.imgsrc;
        }
        if (typeof (data.text) != "undefined" && data.text) {
            this.text = data.text;
        }
        if (typeof (data.kcbid) != "undefined") {
            this.kcbid = data.kcbid;
        }
        if (typeof (data.kcbsrc) != "undefined" && data.kcbsrc) {
            this.kcbsrc = data.kcbsrc;
        }
        if (typeof (data.kcbtext) != "undefined" && data.kcbtext) {
            this.kcbtext = data.kcbtext;
        }
        if (typeof (data.wordPosition) != "undefined") {
            this.wordPosition = data.wordPosition;
        }
        if (typeof (data.goldCoins) != "undefined") {
            this.goldCoins = data.goldCoins;
        }


    }
}
StudyAudio.prototype = new Action();

//视频
function StudyVideo() {
    var index = 0, actionId, mediaid = "0", medianame = "", src = "", kcbid = "0", kcbsrc = "", kcbtext = "", goldCoins = 0;
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
        mediaid: {
            enumerable: true,
            get: function () { return mediaid; },
            set: function (val) {
                if (typeof (val) == "string") {
                    mediaid = val;
                } else {
                    console.error("媒体id应为字符串类型");
                }
            }
        },
        medianame: {
            enumerable: true,
            get: function () { return medianame; },
            set: function (val) {
                if (typeof (val) == "string") {
                    medianame = val;
                } else {
                    console.error("媒体名称应为字符串类型");
                }
            }
        },
        src: {
            enumerable: true,
            get: function () { return src; },
            set: function (val) {
                if (typeof (val) == "string") {
                    src = val;
                } else {
                    console.error("媒体路径应为字符串类型");
                }
            }
        },
        kcbid: {
            enumerable: true,
            get: function () { return kcbid; },
            set: function (val) {
                if (typeof (val) == "string") {
                    kcbid = val;
                } else {
                    console.error("开场白id应为字符串类型");
                }
            }
        },
        kcbsrc: {
            enumerable: true,
            get: function () { return kcbsrc; },
            set: function (val) {
                if (typeof (val) == "string") {
                    kcbsrc = val;
                } else {
                    console.error("媒体路径应为字符串类型");
                }
            }
        },
        kcbtext: {
            enumerable: true,
            get: function () { return kcbtext; },
            set: function (val) {
                if (typeof (val) == "string") {
                    kcbtext = val;
                } else {
                    console.error("媒体文本应为字符串类型");
                }
            }
        },
        goldCoins: {
            enumerable: true,
            get: function () { return goldCoins; },
            set: function (val) {
                if (typeof (val) == "number") {
                    goldCoins = val;
                } else {
                    console.error("金币必须是数字");
                }
            }
        },
        draw: {
            value: function draw() {
                if (typeof (this.parent.parent) != "undefined" && this.parent.parent instanceof Page) {

                }
            }
        },
        //预览方法
        show: {
            value: function show(callback) {

            }
        }
    });
    if (arguments.length != 0) {
        var data = arguments[0];

        if (typeof (data.actionId) != "undefined") {
            this.actionId = data.actionId;
        }
        if (typeof (data.actionNum) != "undefined") {
            this.actionNum = data.actionNum;
        }
        if (typeof (data.mediaid) != "undefined") {
            this.mediaid = data.mediaid;
        }
        if (typeof (data.medianame) != "undefined") {
            this.medianame = data.medianame;
        }
        if (typeof (data.src) != "undefined") {
            this.src = data.src;
        }
        if (typeof (data.kcbid) != "undefined") {
            this.kcbid = data.kcbid;
        }
        if (typeof (data.kcbsrc) != "undefined" && data.kcbsrc) {
            this.kcbsrc = data.kcbsrc;
        }
        if (typeof (data.kcbtext) != "undefined" && data.kcbtext) {
            this.kcbtext = data.kcbtext;
        }
        if (typeof (data.goldCoins) != "undefined") {
            this.goldCoins = data.goldCoins;
        }


    }
}
StudyVideo.prototype = new Action();

//图文
function StudyArticle() {
    var index = 0, actionId, textid = "", textstr = "", imgid = "", imgsrc = "", kcbid = "", kcbsrc = "", kcbtext = "", wordPosition = "top", goldCoins = 0, usetime = 0;
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
        textid: {
            enumerable: true,
            get: function () { return textid; },
            set: function (val) {
                if (typeof (val) == "string") {
                    textid = val;
                } else {
                    console.error("文本id应为字符串类型");
                }
            }
        },
        textstr: {
            enumerable: true,
            get: function () { return textstr; },
            set: function (val) {
                if (typeof (val) == "string") {
                    textstr = val;
                } else {
                    console.error("文本应为字符串类型");
                }
            }
        },
        imgid: {
            enumerable: true,
            get: function () { return imgid; },
            set: function (val) {
                if (typeof (val) == "string") {
                    imgid = val;
                } else {
                    console.error("图片Id应为字符串类型");
                }
            }
        },
        imgsrc: {
            enumerable: true,
            get: function () { return imgsrc; },
            set: function (val) {
                if (typeof (val) == "string") {
                    imgsrc = val;
                } else {
                    console.error("图片路径应为字符串类型");
                }
            }
        },
        kcbid: {
            enumerable: true,
            get: function () { return kcbid; },
            set: function (val) {
                if (typeof (val) == "string") {
                    kcbid = val;
                } else {
                    console.error("开场白Id应为字符串类型");
                }
            }
        },
        kcbsrc: {
            enumerable: true,
            get: function () { return kcbsrc; },
            set: function (val) {
                kcbsrc = val;
            }
        },
        kcbtext: {
            enumerable: true,
            get: function () { return kcbtext; },
            set: function (val) {
                if (typeof (val) == "string") {
                    kcbtext = val;
                } else {
                    console.error("开场白文本应为字符串类型");
                }
            }
        },
        wordPosition: {
            enumerable: true,
            get: function () { return wordPosition; },
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
            get: function () { return goldCoins; },
            set: function (val) {
                if (typeof (val) == "number") {
                    goldCoins = val;
                } else {
                    console.error("奖励金币应为数字");
                }
            }
        },
        usetime: {
            enumerable: true,
            get: function () { return usetime; },
            set: function (val) {
                if (typeof (val) == "number") {
                    usetime = val;
                } else {
                    console.error("学习时间应为数字");
                }
            }
        },
        draw: {
            value: function draw() {
                if (typeof (this.parent.parent) != "undefined" && this.parent.parent instanceof Page) {

                }
            }
        },
        //预览方法
        show: {
            value: function show(callback) {

            }
        }
    });
    if (arguments.length != 0) {
        var data = arguments[0];

        if (typeof (data.actionId) != "undefined") {
            this.actionId = data.actionId;
        }
        if (typeof (data.actionNum) != "undefined") {
            this.actionNum = data.actionNum;
        }

        if (typeof (data.textid) != "undefined" && data.textid != "") {
            this.textid = data.textid;
        }
        if (typeof (data.textstr) != "undefined" && data.textstr != "") {
            this.textstr = data.textstr;
        }
        if (typeof (data.imgid) != "undefined" && data.imgid != "") {
            this.imgid = data.imgid;
        }
        if (typeof (data.imgsrc) != "undefined" && data.imgsrc != "") {
            this.imgsrc = data.imgsrc;
        }
        if (typeof (data.kcbid) != "undefined" && data.kcbid != "") {
            this.kcbid = data.kcbid;
        }
        if (typeof (data.kcbsrc) != "undefined" && data.kcbsrc != "") {
            this.kcbsrc = data.kcbsrc;
        }
        if (typeof (data.kcbtext) != "undefined" && data.kcbtext != "") {
            this.kcbtext = data.kcbtext;
        }
        if (typeof (data.wordPosition) != "undefined" && data.wordPosition != "") {
            this.wordPosition = data.wordPosition;
        }
        if (typeof (data.goldCoins) != "undefined" && data.goldCoins != "") {
            this.goldCoins = data.goldCoins;
        }
        if (typeof (data.usetime) != "undefined" && data.usetime != "") {
            this.usetime = data.usetime;
        }
    }
}
StudyArticle.prototype = new Action();

//朗读
function StudyRecitation() {
    var index = 0, actionId, textid = "", textstr = "", kcbid = "", kcbsrc = "", kcbtext = "", goldCoins = 0, usetime = 0;
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

        textid: {
            enumerable: true,
            get: function () { return textid; },
            set: function (val) {
                if (typeof (val) == "string") {
                    textid = val;
                } else {
                    console.error("文本id应为字符串类型");
                }
            }
        },
        textstr: {
            enumerable: true,
            get: function () { return textstr; },
            set: function (val) {
                if (typeof (val) == "string") {
                    textstr = val;
                } else {
                    console.error("文本应为字符串类型");
                }
            }
        },
        kcbid: {
            enumerable: true,
            get: function () { return kcbid; },
            set: function (val) {
                if (typeof (val) == "string") {
                    kcbid = val;
                } else {
                    console.error("开场白Id应为字符串类型");
                }
            }
        },
        kcbsrc: {
            enumerable: true,
            get: function () { return kcbsrc; },
            set: function (val) {
                kcbsrc = val;
            }
        },
        kcbtext: {
            enumerable: true,
            get: function () { return kcbtext; },
            set: function (val) {
                if (typeof (val) == "string") {
                    kcbtext = val;
                } else {
                    console.error("开场白文本应为字符串类型");
                }
            }
        },
        goldCoins: {
            enumerable: true,
            get: function () { return goldCoins; },
            set: function (val) {
                if (typeof (val) == "number") {
                    goldCoins = val;
                } else {
                    console.error("奖励金币应为数字");
                }
            }
        },
        usetime: {
            enumerable: true,
            get: function () { return usetime; },
            set: function (val) {
                if (typeof (val) == "number") {
                    usetime = val;
                } else {
                    console.error("学习时间应为数字");
                }
            }
        },
        draw: {
            value: function draw() {
                if (typeof (this.parent.parent) != "undefined" && this.parent.parent instanceof Page) {

                }
            }
        },
        //预览方法
        show: {
            value: function show(callback) {

            }
        }
    });
    if (arguments.length != 0) {
        var data = arguments[0];

        if (typeof (data.actionId) != "undefined") {
            this.actionId = data.actionId;
        }
        if (typeof (data.actionNum) != "undefined") {
            this.actionNum = data.actionNum;
        }

        if (typeof (data.textid) != "undefined" && data.textid != "") {
            this.textid = data.textid;
        }
        if (typeof (data.textstr) != "undefined" && data.textstr != "") {
            this.textstr = data.textstr;
        }
        if (typeof (data.imgid) != "undefined" && data.imgid != "") {
            this.imgid = data.imgid;
        }
        if (typeof (data.imgsrc) != "undefined" && data.imgsrc != "") {
            this.imgsrc = data.imgsrc;
        }
        if (typeof (data.kcbid) != "undefined" && data.kcbid != "") {
            this.kcbid = data.kcbid;
        }
        if (typeof (data.kcbsrc) != "undefined" && data.kcbsrc != "") {
            this.kcbsrc = data.kcbsrc;
        }
        if (typeof (data.kcbtext) != "undefined" && data.kcbtext != "") {
            this.kcbtext = data.kcbtext;
        }
        if (typeof (data.wordPosition) != "undefined" && data.wordPosition != "") {
            this.wordPosition = data.wordPosition;
        }
        if (typeof (data.goldCoins) != "undefined" && data.goldCoins != "") {
            this.goldCoins = data.goldCoins;
        }
        if (typeof (data.usetime) != "undefined" && data.usetime != "") {
            this.usetime = data.usetime;
        }
    }
}
StudyRecitation.prototype = new Action();

//快速阅读-简单
function StudyFastReadEasy() {
    var index = 0, actionId, textid = "", textstr = "", kcbid = "", kcbsrc = "", kcbtext = "", speed = 1, showNum = 1, goldCoins = 0;
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
        textid: {
            enumerable: true,
            get: function () { return textid; },
            set: function (val) {
                if (typeof (val) == "string") {
                    textid = val;
                } else {
                    console.error("textid应为字符串类型");
                }
            }
        },
        textstr: {
            enumerable: true,
            get: function () { return textstr; },
            set: function (val) {
                if (typeof (val) == "string") {
                    textstr = val;
                } else {
                    console.error("textstr应为字符串类型");
                }
            }
        },
        kcbid: {
            enumerable: true,
            get: function () { return kcbid; },
            set: function (val) {
                if (typeof (val) == "string") {
                    kcbid = val;
                } else {
                    console.error("kcbid应为字符串类型");
                }
            }
        },
        kcbsrc: {
            enumerable: true,
            get: function () { return kcbsrc; },
            set: function (val) {
                if (typeof (val) == "string") {
                    kcbsrc = val;
                } else {
                    console.error("kcbsrc应为字符串类型");
                }
            }
        },
        kcbtext: {
            enumerable: true,
            get: function () { return kcbtext; },
            set: function (val) {
                if (typeof (val) == "string") {
                    kcbtext = val;
                } else {
                    console.error("kcbtext应为字符串类型");
                }
            }
        },
        speed: {
            enumerable: true,
            get: function () { return speed; },
            set: function (val) {
                if (typeof (val) == "number") {
                    speed = val;
                } else {
                    console.error("speed应为数字类型");
                }
            }
        },
        showNum: {
            enumerable: true,
            get: function () { return showNum; },
            set: function (val) {
                if (typeof (val) == "number") {
                    showNum = val;
                } else {
                    console.error("showNum应为数字类型");
                }
            }
        },
        goldCoins: {
            enumerable: true,
            get: function () { return goldCoins; },
            set: function (val) {
                if (typeof (val) == "number") {
                    goldCoins = val;
                } else {
                    console.error("goldCoins应为数字类型");
                }
            }
        },
        draw: {
            value: function draw() {
                if (typeof (this.parent.parent) != "undefined" && this.parent.parent instanceof Page) {

                }
            }
        },
        //预览方法
        show: {
            value: function show(callback) {

            }
        }
    });
    if (arguments.length != 0) {
        var data = arguments[0];

        if (typeof (data.actionId) != "undefined") {
            this.actionId = data.actionId;
        }
        if (typeof (data.actionNum) != "undefined") {
            this.actionNum = data.actionNum;
        }

        if (typeof (data.textid) != "undefined" && data.textid != "") {
            this.textid = data.textid;
        }

        if (typeof (data.textstr) != "undefined" && data.textstr != "") {
            this.textstr = data.textstr;
        }

        if (typeof (data.kcbid) != "undefined" && data.kcbid != "") {
            this.kcbid = data.kcbid;
        }

        if (typeof (data.kcbsrc) != "undefined" && data.kcbsrc != "") {
            this.kcbsrc = data.kcbsrc;
        }

        if (typeof (data.kcbtext) != "undefined" && data.kcbtext != "") {
            this.kcbtext = data.kcbtext;
        }

        if (typeof (data.speed) != "undefined") {
            this.speed = data.speed;
        }

        if (typeof (data.showNum) != "undefined") {
            this.showNum = data.showNum;
        }
        if (typeof (data.goldCoins) != "undefined") {
            this.goldCoins = data.goldCoins;
        }
    }
}
StudyFastReadEasy.prototype = new Action();

//快速阅读
function StudyFastRead() {
    var index = 0, actionId, textid = "", textstr = "", kcbid = "", kcbsrc = "", kcbtext = "", speed = 1, showModel = "", showNum = 1, goldCoins = 0;
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
        textid: {
            enumerable: true,
            get: function () { return textid; },
            set: function (val) {
                if (typeof (val) == "string") {
                    textid = val;
                } else {
                    console.error("textid应为字符串类型");
                }
            }
        },
        textstr: {
            enumerable: true,
            get: function () { return textstr; },
            set: function (val) {
                if (typeof (val) == "string") {
                    textstr = val;
                } else {
                    console.error("textstr应为字符串类型");
                }
            }
        },
        kcbid: {
            enumerable: true,
            get: function () { return kcbid; },
            set: function (val) {
                if (typeof (val) == "string") {
                    kcbid = val;
                } else {
                    console.error("kcbid应为字符串类型");
                }
            }
        },
        kcbsrc: {
            enumerable: true,
            get: function () { return kcbsrc; },
            set: function (val) {
                if (typeof (val) == "string") {
                    kcbsrc = val;
                } else {
                    console.error("kcbsrc应为字符串类型");
                }
            }
        },
        kcbtext: {
            enumerable: true,
            get: function () { return kcbtext; },
            set: function (val) {
                if (typeof (val) == "string") {
                    kcbtext = val;
                } else {
                    console.error("kcbtext应为字符串类型");
                }
            }
        },
        speed: {
            enumerable: true,
            get: function () { return speed; },
            set: function (val) {
                if (typeof (val) == "number") {
                    speed = val;
                } else {
                    console.error("speed应为数字类型");
                }
            }
        },
        showModel: {
            enumerable: true,
            get: function () { return showModel; },
            set: function (val) {
                if (typeof (val) == "string") {
                    showModel = val;
                } else {
                    console.error("showModel应为字符类型");
                }
            }
        },
        showNum: {
            enumerable: true,
            get: function () { return showNum; },
            set: function (val) {
                if (typeof (val) == "number") {
                    showNum = val;
                } else {
                    console.error("showNum应为数字类型");
                }
            }
        },
        goldCoins: {
            enumerable: true,
            get: function () { return goldCoins; },
            set: function (val) {
                if (typeof (val) == "number") {
                    goldCoins = val;
                } else {
                    console.error("goldCoins应为数字类型");
                }
            }
        },
        draw: {
            value: function draw() {
                if (typeof (this.parent.parent) != "undefined" && this.parent.parent instanceof Page) {

                }
            }
        },
        //预览方法
        show: {
            value: function show(callback) {

            }
        }
    });
    if (arguments.length != 0) {
        var data = arguments[0];

        if (typeof (data.actionId) != "undefined") {
            this.actionId = data.actionId;
        }
        if (typeof (data.actionNum) != "undefined") {
            this.actionNum = data.actionNum;
        }
        if (typeof (data.textid) != "undefined" && data.textid != "") {
            this.textid = data.textid;
        }

        if (typeof (data.textstr) != "undefined" && data.textstr != "") {
            this.textstr = data.textstr;
        }

        if (typeof (data.kcbid) != "undefined" && data.kcbid != "") {
            this.kcbid = data.kcbid;
        }

        if (typeof (data.kcbsrc) != "undefined" && data.kcbsrc != "") {
            this.kcbsrc = data.kcbsrc;
        }

        if (typeof (data.kcbtext) != "undefined" && data.kcbtext != "") {
            this.kcbtext = data.kcbtext;
        }

        if (typeof (data.speed) != "undefined") {
            this.speed = data.speed;
        }

        if (typeof (data.showNum) != "undefined") {
            this.showNum = data.showNum;
        }
        if (typeof (data.showModel) != "undefined") {
            this.showModel = data.showModel;
        }
        if (typeof (data.goldCoins) != "undefined") {
            this.goldCoins = data.goldCoins;
        }
    }
}
StudyFastRead.prototype = new Action();

//圈点批注-标色
function StudyAnnotation() {
    var index = 0, actionId, questionid = "", questionname = "", kcbid = "", kcbsrc = "", kcbtext = "", goldCoins = 0, usetime = 0;
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
        questionid: {
            enumerable: true,
            get: function () { return questionid; },
            set: function (val) {
                questionid = val;
            }
        },
        questionname: {
            enumerable: true,
            get: function () { return questionname; },
            set: function (val) {
                questionname = val;
            }
        },
        kcbid: {
            enumerable: true,
            get: function () { return kcbid; },
            set: function (val) {
                if (typeof (val) == "string") {
                    kcbid = val;
                } else {
                    console.error("开场白Id应为字符串类型");
                }
            }
        },
        kcbsrc: {
            enumerable: true,
            get: function () { return kcbsrc; },
            set: function (val) {
                kcbsrc = val;
            }
        },
        kcbtext: {
            enumerable: true,
            get: function () { return kcbtext; },
            set: function (val) {
                if (typeof (val) == "string") {
                    kcbtext = val;
                } else {
                    console.error("开场白文本应为字符串类型");
                }
            }
        },
        goldCoins: {
            enumerable: true,
            get: function () { return goldCoins; },
            set: function (val) {
                if (typeof (val) == "number") {
                    goldCoins = val;
                } else {
                    console.error("奖励金币应为数字");
                }
            }
        },
        usetime: {
            enumerable: true,
            get: function () { return usetime; },
            set: function (val) {
                if (typeof (val) == "number") {
                    usetime = val;
                } else {
                    console.error("学习时间应为数字");
                }
            }
        },
        draw: {
            value: function draw() {
                if (typeof (this.parent.parent) != "undefined" && this.parent.parent instanceof Page) {

                }
            }
        },
        //预览方法
        show: {
            value: function show(callback) {

            }
        }
    });
    if (arguments.length != 0) {
        var data = arguments[0];

        if (typeof (data.actionId) != "undefined") {
            this.actionId = data.actionId;
        }
        if (typeof (data.actionNum) != "undefined") {
            this.actionNum = data.actionNum;
        }
        if (typeof (data.questionid) != "undefined" && data.questionid != "") {
            this.questionid = data.questionid;
        }
        if (typeof (data.questionname) != "undefined" && data.questionname != "") {
            this.questionname = data.questionname;
        }
        if (typeof (data.kcbid) != "undefined" && data.kcbid != "") {
            this.kcbid = data.kcbid;
        }
        if (typeof (data.kcbsrc) != "undefined" && data.kcbsrc != "") {
            this.kcbsrc = data.kcbsrc;
        }
        if (typeof (data.kcbtext) != "undefined" && data.kcbtext != "") {
            this.kcbtext = data.kcbtext;
        }
        if (typeof (data.wordPosition) != "undefined" && data.wordPosition != "") {
            this.wordPosition = data.wordPosition;
        }
        if (typeof (data.goldCoins) != "undefined" && data.goldCoins != "") {
            this.goldCoins = data.goldCoins;
        }
        if (typeof (data.usetime) != "undefined" && data.usetime != "") {
            this.usetime = data.usetime;
        }
    }
}
StudyAnnotation.prototype = new Action();


//圈点批注-断句
function StudyAnnotation2() {
    var index = 0, actionId, questionid = "", questionname = "", kcbid = "", kcbsrc = "", kcbtext = "", goldCoins = 0, usetime = 0;
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
        questionid: {
            enumerable: true,
            get: function () { return questionid; },
            set: function (val) {
                questionid = val;
            }
        },
        questionname: {
            enumerable: true,
            get: function () { return questionname; },
            set: function (val) {
                questionname = val;
            }
        },
        kcbid: {
            enumerable: true,
            get: function () { return kcbid; },
            set: function (val) {
                if (typeof (val) == "string") {
                    kcbid = val;
                } else {
                    console.error("开场白Id应为字符串类型");
                }
            }
        },
        kcbsrc: {
            enumerable: true,
            get: function () { return kcbsrc; },
            set: function (val) {
                kcbsrc = val;
            }
        },
        kcbtext: {
            enumerable: true,
            get: function () { return kcbtext; },
            set: function (val) {
                if (typeof (val) == "string") {
                    kcbtext = val;
                } else {
                    console.error("开场白文本应为字符串类型");
                }
            }
        },
        goldCoins: {
            enumerable: true,
            get: function () { return goldCoins; },
            set: function (val) {
                if (typeof (val) == "number") {
                    goldCoins = val;
                } else {
                    console.error("奖励金币应为数字");
                }
            }
        },
        usetime: {
            enumerable: true,
            get: function () { return usetime; },
            set: function (val) {
                if (typeof (val) == "number") {
                    usetime = val;
                } else {
                    console.error("学习时间应为数字");
                }
            }
        },
        draw: {
            value: function draw() {
                if (typeof (this.parent.parent) != "undefined" && this.parent.parent instanceof Page) {

                }
            }
        },
        //预览方法
        show: {
            value: function show(callback) {

            }
        }
    });
    if (arguments.length != 0) {
        var data = arguments[0];

        if (typeof (data.actionId) != "undefined") {
            this.actionId = data.actionId;
        }
        if (typeof (data.actionNum) != "undefined") {
            this.actionNum = data.actionNum;
        }
        if (typeof (data.questionid) != "undefined" && data.questionid != "") {
            this.questionid = data.questionid;
        }
        if (typeof (data.questionname) != "undefined" && data.questionname != "") {
            this.questionname = data.questionname;
        }
        if (typeof (data.kcbid) != "undefined" && data.kcbid != "") {
            this.kcbid = data.kcbid;
        }
        if (typeof (data.kcbsrc) != "undefined" && data.kcbsrc != "") {
            this.kcbsrc = data.kcbsrc;
        }
        if (typeof (data.kcbtext) != "undefined" && data.kcbtext != "") {
            this.kcbtext = data.kcbtext;
        }
        if (typeof (data.wordPosition) != "undefined" && data.wordPosition != "") {
            this.wordPosition = data.wordPosition;
        }
        if (typeof (data.goldCoins) != "undefined" && data.goldCoins != "") {
            this.goldCoins = data.goldCoins;
        }
        if (typeof (data.usetime) != "undefined" && data.usetime != "") {
            this.usetime = data.usetime;
        }
    }
}
StudyAnnotation2.prototype = new Action();

//判断题
function StudyJudgment() {
    var index = 0, actionId, questionid = "", questionname = "", kcbid = "", kcbsrc = "", kcbtext = "", goldCoins = 0, usetime = 0;
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
        questionid: {
            enumerable: true,
            get: function () { return questionid; },
            set: function (val) {
                questionid = val;
            }
        },
        questionname: {
            enumerable: true,
            get: function () { return questionname; },
            set: function (val) {
                questionname = val;
            }
        },
        kcbid: {
            enumerable: true,
            get: function () { return kcbid; },
            set: function (val) {
                if (typeof (val) == "string") {
                    kcbid = val;
                } else {
                    console.error("开场白Id应为字符串类型");
                }
            }
        },
        kcbsrc: {
            enumerable: true,
            get: function () { return kcbsrc; },
            set: function (val) {
                kcbsrc = val;
            }
        },
        kcbtext: {
            enumerable: true,
            get: function () { return kcbtext; },
            set: function (val) {
                if (typeof (val) == "string") {
                    kcbtext = val;
                } else {
                    console.error("开场白文本应为字符串类型");
                }
            }
        },
        goldCoins: {
            enumerable: true,
            get: function () { return goldCoins; },
            set: function (val) {
                if (typeof (val) == "number") {
                    goldCoins = val;
                } else {
                    console.error("奖励金币应为数字");
                }
            }
        },
        usetime: {
            enumerable: true,
            get: function () { return usetime; },
            set: function (val) {
                if (typeof (val) == "number") {
                    usetime = val;
                } else {
                    console.error("学习时间应为数字");
                }
            }
        },
        draw: {
            value: function draw() {
                if (typeof (this.parent.parent) != "undefined" && this.parent.parent instanceof Page) {

                }
            }
        },
        //预览方法
        show: {
            value: function show(callback) {

            }
        }
    });
    if (arguments.length != 0) {
        var data = arguments[0];

        if (typeof (data.actionId) != "undefined") {
            this.actionId = data.actionId;
        }
        if (typeof (data.actionNum) != "undefined") {
            this.actionNum = data.actionNum;
        }
        if (typeof (data.questionid) != "undefined" && data.questionid != "") {
            this.questionid = data.questionid;
        }
        if (typeof (data.questionname) != "undefined" && data.questionname != "") {
            this.questionname = data.questionname;
        }
        if (typeof (data.kcbid) != "undefined" && data.kcbid != "") {
            this.kcbid = data.kcbid;
        }
        if (typeof (data.kcbsrc) != "undefined" && data.kcbsrc != "") {
            this.kcbsrc = data.kcbsrc;
        }
        if (typeof (data.kcbtext) != "undefined" && data.kcbtext != "") {
            this.kcbtext = data.kcbtext;
        }
        if (typeof (data.wordPosition) != "undefined" && data.wordPosition != "") {
            this.wordPosition = data.wordPosition;
        }
        if (typeof (data.goldCoins) != "undefined" && data.goldCoins != "") {
            this.goldCoins = data.goldCoins;
        }
        if (typeof (data.usetime) != "undefined" && data.usetime != "") {
            this.usetime = data.usetime;
        }
    }
}
StudyJudgment.prototype = new Action();

//连线题
function StudyLinking() {
    var index = 0, actionId, questionid = "", questionname = "", kcbid = "", kcbsrc = "", kcbtext = "", goldCoins = 0, usetime = 0;
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
            value: "studyLinking"
        },
        questionid: {
            enumerable: true,
            get: function () {
                return questionid;
            },
            set: function (val) {
                questionid = val;
            }
        },
        questionname: {
            enumerable: true,
            get: function () {
                return questionname;
            },
            set: function (val) {
                questionname = val;
            }
        },
        kcbid: {
            enumerable: true,
            get: function () {
                return kcbid;
            },
            set: function (val) {
                if (typeof (val) == "string") {
                    kcbid = val;
                } else {
                    console.error("开场白Id应为字符串类型");
                }
            }
        },
        kcbsrc: {
            enumerable: true,
            get: function () {
                return kcbsrc;
            },
            set: function (val) {
                kcbsrc = val;
            }
        },
        kcbtext: {
            enumerable: true,
            get: function () {
                return kcbtext;
            },
            set: function (val) {
                if (typeof (val) == "string") {
                    kcbtext = val;
                } else {
                    console.error("开场白文本应为字符串类型");
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
                    console.error("奖励金币应为数字");
                }
            }
        },
        usetime: {
            enumerable: true,
            get: function () { return usetime; },
            set: function (val) {
                if (typeof (val) == "number") {
                    usetime = val;
                } else {
                    console.error("学习时间应为数字");
                }
            }
        },
        draw: {
            value: function draw() {
                if (typeof (this.parent.parent) != "undefined" && this.parent.parent instanceof Page) {

                }
            }
        },
        //预览方法
        show: {
            value: function show(callback) {

            }
        }
    });
    if (arguments.length != 0) {
        var data = arguments[0];

        if (typeof (data.actionId) != "undefined") {
            this.actionId = data.actionId;
        }
        if (typeof (data.actionNum) != "undefined") {
            this.actionNum = data.actionNum;
        }
        if (typeof (data.questionid) != "undefined" && data.questionid != "") {
            this.questionid = data.questionid;
        }
        if (typeof (data.questionname) != "undefined" && data.questionname != "") {
            this.questionname = data.questionname;
        }
        if (typeof (data.kcbid) != "undefined" && data.kcbid != "") {
            this.kcbid = data.kcbid;
        }
        if (typeof (data.kcbsrc) != "undefined" && data.kcbsrc != "") {
            this.kcbsrc = data.kcbsrc;
        }
        if (typeof (data.kcbtext) != "undefined" && data.kcbtext != "") {
            this.kcbtext = data.kcbtext;
        }
        if (typeof (data.wordPosition) != "undefined" && data.wordPosition != "") {
            this.wordPosition = data.wordPosition;
        }
        if (typeof (data.goldCoins) != "undefined" && data.goldCoins != "") {
            this.goldCoins = data.goldCoins;
        }
        if (typeof (data.usetime) != "undefined" && data.usetime != "") {
            this.usetime = data.usetime;
        }
    }
}
StudyLinking.prototype = new Action();

//选择题
function StudyOption() {
    var index = 0, actionId, questionid = "", questionname = "", kcbid = "", kcbsrc = "", kcbtext = "", goldCoins = 0, usetime = 0;
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
            value: "studyOption"
        },
        questionid: {
            enumerable: true,
            get: function () {
                return questionid;
            },
            set: function (val) {
                questionid = val;
            }
        },
        questionname: {
            enumerable: true,
            get: function () {
                return questionname;
            },
            set: function (val) {
                questionname = val;
            }
        },
        kcbid: {
            enumerable: true,
            get: function () {
                return kcbid;
            },
            set: function (val) {
                if (typeof (val) == "string") {
                    kcbid = val;
                } else {
                    console.error("开场白Id应为字符串类型");
                }
            }
        },
        kcbsrc: {
            enumerable: true,
            get: function () {
                return kcbsrc;
            },
            set: function (val) {
                kcbsrc = val;
            }
        },
        kcbtext: {
            enumerable: true,
            get: function () {
                return kcbtext;
            },
            set: function (val) {
                if (typeof (val) == "string") {
                    kcbtext = val;
                } else {
                    console.error("开场白文本应为字符串类型");
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
                    console.error("奖励金币应为数字");
                }
            }
        },
        usetime: {
            enumerable: true,
            get: function () { return usetime; },
            set: function (val) {
                if (typeof (val) == "number") {
                    usetime = val;
                } else {
                    console.error("学习时间应为数字");
                }
            }
        },
        draw: {
            value: function draw() {
                if (typeof (this.parent.parent) != "undefined" && this.parent.parent instanceof Page) {

                }
            }
        },
        //预览方法
        show: {
            value: function show(callback) {

            }
        }
    });
    if (arguments.length != 0) {
        var data = arguments[0];

        if (typeof (data.actionId) != "undefined") {
            this.actionId = data.actionId;
        }
        if (typeof (data.actionNum) != "undefined") {
            this.actionNum = data.actionNum;
        }
        if (typeof (data.questionid) != "undefined" && data.questionid != "") {
            this.questionid = data.questionid;
        }
        if (typeof (data.questionname) != "undefined" && data.questionname != "") {
            this.questionname = data.questionname;
        }
        if (typeof (data.kcbid) != "undefined" && data.kcbid != "") {
            this.kcbid = data.kcbid;
        }
        if (typeof (data.kcbsrc) != "undefined" && data.kcbsrc != "") {
            this.kcbsrc = data.kcbsrc;
        }
        if (typeof (data.kcbtext) != "undefined" && data.kcbtext != "") {
            this.kcbtext = data.kcbtext;
        }
        if (typeof (data.wordPosition) != "undefined" && data.wordPosition != "") {
            this.wordPosition = data.wordPosition;
        }
        if (typeof (data.goldCoins) != "undefined" && data.goldCoins != "") {
            this.goldCoins = data.goldCoins;
        }
        if (typeof (data.usetime) != "undefined" && data.usetime != "") {
            this.usetime = data.usetime;
        }
    }
}
StudyOption.prototype = new Action();

//选择填空
function StudyOptionFill() {
    var index = 0, actionId, questionid = "", questionname = "", kcbid = "", kcbsrc = "", kcbtext = "", goldCoins = 0, usetime = 0;
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
            value: "studyOptionFill"
        },
        questionid: {
            enumerable: true,
            get: function () {
                return questionid;
            },
            set: function (val) {
                questionid = val;
            }
        },
        questionname: {
            enumerable: true,
            get: function () {
                return questionname;
            },
            set: function (val) {
                questionname = val;
            }
        },
        kcbid: {
            enumerable: true,
            get: function () {
                return kcbid;
            },
            set: function (val) {
                if (typeof (val) == "string") {
                    kcbid = val;
                } else {
                    console.error("开场白Id应为字符串类型");
                }
            }
        },
        kcbsrc: {
            enumerable: true,
            get: function () {
                return kcbsrc;
            },
            set: function (val) {
                kcbsrc = val;
            }
        },
        kcbtext: {
            enumerable: true,
            get: function () {
                return kcbtext;
            },
            set: function (val) {
                if (typeof (val) == "string") {
                    kcbtext = val;
                } else {
                    console.error("开场白文本应为字符串类型");
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
                    console.error("奖励金币应为数字");
                }
            }
        },
        usetime: {
            enumerable: true,
            get: function () { return usetime; },
            set: function (val) {
                if (typeof (val) == "number") {
                    usetime = val;
                } else {
                    console.error("学习时间应为数字");
                }
            }
        },
        draw: {
            value: function draw() {
                if (typeof (this.parent.parent) != "undefined" && this.parent.parent instanceof Page) {

                }
            }
        },
        //预览方法
        show: {
            value: function show(callback) {

            }
        }
    });
    if (arguments.length != 0) {
        var data = arguments[0];

        if (typeof (data.actionId) != "undefined") {
            this.actionId = data.actionId;
        }
        if (typeof (data.actionNum) != "undefined") {
            this.actionNum = data.actionNum;
        }
        if (typeof (data.questionid) != "undefined" && data.questionid != "") {
            this.questionid = data.questionid;
        }
        if (typeof (data.questionname) != "undefined" && data.questionname != "") {
            this.questionname = data.questionname;
        }
        if (typeof (data.kcbid) != "undefined" && data.kcbid != "") {
            this.kcbid = data.kcbid;
        }
        if (typeof (data.kcbsrc) != "undefined" && data.kcbsrc != "") {
            this.kcbsrc = data.kcbsrc;
        }
        if (typeof (data.kcbtext) != "undefined" && data.kcbtext != "") {
            this.kcbtext = data.kcbtext;
        }
        if (typeof (data.wordPosition) != "undefined" && data.wordPosition != "") {
            this.wordPosition = data.wordPosition;
        }
        if (typeof (data.goldCoins) != "undefined" && data.goldCoins != "") {
            this.goldCoins = data.goldCoins;
        }
        if (typeof (data.usetime) != "undefined" && data.usetime != "") {
            this.usetime = data.usetime;
        }
    }
}
StudyOptionFill.prototype = new Action();

//填空题
function StudyFill() {
    var index = 0, actionId, questionid = "", questionname = "", kcbid = "", kcbsrc = "", kcbtext = "", goldCoins = 0, usetime = 0;
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
            value: "studyFill"
        },
        questionid: {
            enumerable: true,
            get: function () {
                return questionid;
            },
            set: function (val) {
                questionid = val;
            }
        },
        questionname: {
            enumerable: true,
            get: function () {
                return questionname;
            },
            set: function (val) {
                questionname = val;
            }
        },
        kcbid: {
            enumerable: true,
            get: function () {
                return kcbid;
            },
            set: function (val) {
                if (typeof (val) == "string") {
                    kcbid = val;
                } else {
                    console.error("开场白Id应为字符串类型");
                }
            }
        },
        kcbsrc: {
            enumerable: true,
            get: function () {
                return kcbsrc;
            },
            set: function (val) {
                kcbsrc = val;
            }
        },
        kcbtext: {
            enumerable: true,
            get: function () {
                return kcbtext;
            },
            set: function (val) {
                if (typeof (val) == "string") {
                    kcbtext = val;
                } else {
                    console.error("开场白文本应为字符串类型");
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
                    console.error("奖励金币应为数字");
                }
            }
        },
        usetime: {
            enumerable: true,
            get: function () { return usetime; },
            set: function (val) {
                if (typeof (val) == "number") {
                    usetime = val;
                } else {
                    console.error("学习时间应为数字");
                }
            }
        },
        draw: {
            value: function draw() {
                if (typeof (this.parent.parent) != "undefined" && this.parent.parent instanceof Page) {

                }
            }
        },
        //预览方法
        show: {
            value: function show(callback) {

            }
        }
    });
    if (arguments.length != 0) {
        var data = arguments[0];

        if (typeof (data.actionId) != "undefined") {
            this.actionId = data.actionId;
        }
        if (typeof (data.actionNum) != "undefined") {
            this.actionNum = data.actionNum;
        }
        if (typeof (data.questionid) != "undefined" && data.questionid != "") {
            this.questionid = data.questionid;
        }
        if (typeof (data.questionname) != "undefined" && data.questionname != "") {
            this.questionname = data.questionname;
        }
        if (typeof (data.kcbid) != "undefined" && data.kcbid != "") {
            this.kcbid = data.kcbid;
        }
        if (typeof (data.kcbsrc) != "undefined" && data.kcbsrc != "") {
            this.kcbsrc = data.kcbsrc;
        }
        if (typeof (data.kcbtext) != "undefined" && data.kcbtext != "") {
            this.kcbtext = data.kcbtext;
        }
        if (typeof (data.wordPosition) != "undefined" && data.wordPosition != "") {
            this.wordPosition = data.wordPosition;
        }
        if (typeof (data.goldCoins) != "undefined" && data.goldCoins != "") {
            this.goldCoins = data.goldCoins;
        }
        if (typeof (data.usetime) != "undefined" && data.usetime != "") {
            this.usetime = data.usetime;
        }
    }
}
StudyFill.prototype = new Action();

//主观题
function StudySubjective() {
    var index = 0, actionId, questionid = "", questionname = "", kcbid = "", kcbsrc = "", kcbtext = "", goldCoins = 0, usetime = 0;
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
            value: "studySubjective"
        },
        questionid: {
            enumerable: true,
            get: function () {
                return questionid;
            },
            set: function (val) {
                questionid = val;
            }
        },
        questionname: {
            enumerable: true,
            get: function () {
                return questionname;
            },
            set: function (val) {
                questionname = val;
            }
        },
        kcbid: {
            enumerable: true,
            get: function () {
                return kcbid;
            },
            set: function (val) {
                if (typeof (val) == "string") {
                    kcbid = val;
                } else {
                    console.error("开场白Id应为字符串类型");
                }
            }
        },
        kcbsrc: {
            enumerable: true,
            get: function () {
                return kcbsrc;
            },
            set: function (val) {
                kcbsrc = val;
            }
        },
        kcbtext: {
            enumerable: true,
            get: function () {
                return kcbtext;
            },
            set: function (val) {
                if (typeof (val) == "string") {
                    kcbtext = val;
                } else {
                    console.error("开场白文本应为字符串类型");
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
                    console.error("奖励金币应为数字");
                }
            }
        },
        usetime: {
            enumerable: true,
            get: function () { return usetime; },
            set: function (val) {
                if (typeof (val) == "number") {
                    usetime = val;
                } else {
                    console.error("学习时间应为数字");
                }
            }
        },
        draw: {
            value: function draw() {
                if (typeof (this.parent.parent) != "undefined" && this.parent.parent instanceof Page) {

                }
            }
        },
        //预览方法
        show: {
            value: function show(callback) {

            }
        }
    });
    if (arguments.length != 0) {
        var data = arguments[0];

        if (typeof (data.actionId) != "undefined") {
            this.actionId = data.actionId;
        }
        if (typeof (data.actionNum) != "undefined") {
            this.actionNum = data.actionNum;
        }
        if (typeof (data.questionid) != "undefined" && data.questionid != "") {
            this.questionid = data.questionid;
        }
        if (typeof (data.questionname) != "undefined" && data.questionname != "") {
            this.questionname = data.questionname;
        }
        if (typeof (data.kcbid) != "undefined" && data.kcbid != "") {
            this.kcbid = data.kcbid;
        }
        if (typeof (data.kcbsrc) != "undefined" && data.kcbsrc != "") {
            this.kcbsrc = data.kcbsrc;
        }
        if (typeof (data.kcbtext) != "undefined" && data.kcbtext != "") {
            this.kcbtext = data.kcbtext;
        }
        if (typeof (data.wordPosition) != "undefined" && data.wordPosition != "") {
            this.wordPosition = data.wordPosition;
        }
        if (typeof (data.goldCoins) != "undefined" && data.goldCoins != "") {
            this.goldCoins = data.goldCoins;
        }
        if (typeof (data.usetime) != "undefined" && data.usetime != "") {
            this.usetime = data.usetime;
        }
    }
}
StudySubjective.prototype = new Action();

//田字格写字
function StudyCalligraphy() {
    var index = 0, actionId, questionid = "", questionname = "", kcbid = "", kcbsrc = "", kcbtext = "", goldCoins = 0, usetime = 0;
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
            value: "studyCalligraphy"
        },
        questionid: {
            enumerable: true,
            get: function () {
                return questionid;
            },
            set: function (val) {
                questionid = val;
            }
        },
        questionname: {
            enumerable: true,
            get: function () {
                return questionname;
            },
            set: function (val) {
                questionname = val;
            }
        },
        kcbid: {
            enumerable: true,
            get: function () {
                return kcbid;
            },
            set: function (val) {
                if (typeof (val) == "string") {
                    kcbid = val;
                } else {
                    console.error("开场白Id应为字符串类型");
                }
            }
        },
        kcbsrc: {
            enumerable: true,
            get: function () {
                return kcbsrc;
            },
            set: function (val) {
                kcbsrc = val;
            }
        },
        kcbtext: {
            enumerable: true,
            get: function () {
                return kcbtext;
            },
            set: function (val) {
                if (typeof (val) == "string") {
                    kcbtext = val;
                } else {
                    console.error("开场白文本应为字符串类型");
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
                    console.error("奖励金币应为数字");
                }
            }
        },
        usetime: {
            enumerable: true,
            get: function () { return usetime; },
            set: function (val) {
                if (typeof (val) == "number") {
                    usetime = val;
                } else {
                    console.error("学习时间应为数字");
                }
            }
        },
        draw: {
            value: function draw() {
                if (typeof (this.parent.parent) != "undefined" && this.parent.parent instanceof Page) {

                }
            }
        },
        //预览方法
        show: {
            value: function show(callback) {

            }
        }
    });
    if (arguments.length != 0) {
        var data = arguments[0];

        if (typeof (data.actionId) != "undefined") {
            this.actionId = data.actionId;
        }
        if (typeof (data.actionNum) != "undefined") {
            this.actionNum = data.actionNum;
        }
        if (typeof (data.questionid) != "undefined" && data.questionid != "") {
            this.questionid = data.questionid;
        }
        if (typeof (data.questionname) != "undefined" && data.questionname != "") {
            this.questionname = data.questionname;
        }
        if (typeof (data.kcbid) != "undefined" && data.kcbid != "") {
            this.kcbid = data.kcbid;
        }
        if (typeof (data.kcbsrc) != "undefined" && data.kcbsrc != "") {
            this.kcbsrc = data.kcbsrc;
        }
        if (typeof (data.kcbtext) != "undefined" && data.kcbtext != "") {
            this.kcbtext = data.kcbtext;
        }
        if (typeof (data.wordPosition) != "undefined" && data.wordPosition != "") {
            this.wordPosition = data.wordPosition;
        }
        if (typeof (data.goldCoins) != "undefined" && data.goldCoins != "") {
            this.goldCoins = data.goldCoins;
        }
        if (typeof (data.usetime) != "undefined" && data.usetime != "") {
            this.usetime = data.usetime;
        }
    }
}
StudyCalligraphy.prototype = new Action();


