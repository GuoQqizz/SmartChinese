(function ($) {
    $.fn.onLine = function (options) {
        var box = this;
        var parentDom = options.parentDom;
        var commonId = options.commonId;
        var part1, part2;
        part1 = box.find(".showleft");
        part2 = box.find(".showright");
        //初始化赋值 列表内容
        box.find(".showleft").children("span").each(function (index, element) {
            $(this).attr({
                group: "gpl",
                left: $(this).position().left + $(this).outerWidth(),
                top: $(this).position().top + $(this).outerHeight() / 2,
                sel: "0",
                check: "0"
            });
        });
        box.find(".showright").children("span").each(function (index, element) {
            $(this).attr({
                group: "gpr",
                left: $(this).position().left,
                top: $(this).position().top + $(this).outerHeight() / 2,
                sel: "0",
                check: "0"
            });
        });
        part1.attr('first', 0); //初始赋值 列表内容容器
        part2.attr('first', 0);
        //canvas 赋值
        var canvas = box.find(".canvas")[0]; //获取canvas  实际连线标签
        canvas.width = 500; //canvas宽度等于div容器宽度
        canvas.height = parentDom.find(box).find(".showleft .showitem").length * 60 + 10;
        var backcanvas = box.find(".backcanvas")[0]; //获取canvas 模拟连线标签  
        backcanvas.width = 500;
        backcanvas.height = parentDom.find(box).find(".showleft .showitem").length * 60 + 10;
        //连线数据
        var groupstate = false; //按下事件状态，标记按下后的移动，抬起参考
        var mx = []; //连线坐标
        var my = [];
        var ms = [];
        var pairl = [];
        var pairr = [];
        var pair = 0; //配对属性
        var temp; //存贮按下的对象
        var mxid = [];
        var myid = [];
        var objL, objR;
        //提示线数据
        var mid_startx, mid_starty, mid_endx, mid_endy; //存储虚拟连线起始坐标
        //事件处理---按下
        var pairSet, countL, countR, getPairL, getPairR, orderCheck;
        var orderCheck = new Array();
        var orderPair = new Array();
        var subSize = box.find(".showitem").length;
        //addstyle showitem
        var doms = parentDom.find(".show").children().children("span");
        doms.bind("mousedown", function (event) {
            countL = 0;
            countR = 0;
            for (var i = 0; i < subSize; i++) {
                if (box.find(".showitem").eq(i).hasClass("addstyle") == true) {
                    orderCheck[i] = true;
                    orderPair[i] = box.find(".showitem").eq(i).attr("pair");
                } else {
                    orderCheck[i] = false;
                    orderPair[i] = null;
                }
            }

            pairSet = parseInt($(this).attr("pair")) * 2;
            groupstate = true;

            var testP;
            if ($(this).attr("check") == 1) {
                $(this).attr("sel", "1").parent().attr("first", "1");
                temp = $(this);
            } else {
                $(this).attr("sel", "1").addClass("addstyle").parent().attr("first", "1");
                temp = $(this);

            };
            mid_startx = $(this).attr("left");
            mid_starty = $(this).attr("top");
            event.preventDefault();
            $(document).mousemove(function (event) { //移动				
                var $target = $(event.target);
                if (groupstate) {
                    mid_endx = event.pageX - box.find(".show").offset().left;
                    mid_endy = event.pageY - box.find(".show").offset().top;
                    var targettrue = null;
                    if ($target.is("span") && $target.closest(".show").parent().attr("class") == box.attr("class")) {
                        targettrue = $target;
                    } else if ($target.closest(".showitem").is("span") && $target.closest(".show").parent().attr("class") == box.attr("class")) {
                        targettrue = $target.closest(".showitem");
                    } else {
                        targettrue = null;
                    };
                    if (targettrue) {
                        if (targettrue.parent().attr("first") == 0) {
                            if (targettrue.attr("check") == 0) {
                                targettrue.addClass("addstyle").attr("sel", "1").siblings("span[check=0]").removeClass("addstyle").attr("sel", "0");
                            };
                        } else {
                            if (targettrue.attr("check") == 0) {
                                targettrue.addClass("addstyle").attr("sel", "1").siblings("span[check=0]").removeClass("addstyle").attr("sel", "0");
                                mid_startx = targettrue.attr("left");
                                mid_starty = targettrue.attr("top");

                            };

                        };
                    } else {
                        if (part1.attr("first") == 0) {
                            part1.children("span").each(function (index, element) {
                                if ($(this).attr('check') == 0) {
                                    $(this).attr("sel", "0").removeClass("addstyle");
                                };
                            });
                        };
                        if (part2.attr("first") == 0) {
                            part2.children("span").each(function (index, element) {
                                if ($(this).attr('check') == 0) {
                                    $(this).attr("sel", "0").removeClass("addstyle");
                                };
                            });
                        };

                    };
                    backstrockline();
                };
                event.preventDefault();
            });
            $(document).mouseup(function (event) { //抬起
                event.preventDefault();
                var $target = $(event.target);
                if (groupstate) {
                    var targettrue;
                    if ($target.is("span") && $target.closest(".show").parent().attr("class") == box.attr("class")) {
                        targettrue = $target;
                    } else if ($target.closest(".showitem").is("span") && $target.closest(".show").parent().attr("class") == box.attr("class")) {
                        targettrue = $target.closest(".showitem");
                    } else {
                        targettrue = null;
                    };
                    if (targettrue) {
                        if (targettrue.parent().attr("first") == 0) {
                            if (targettrue.attr("check") == 0) {
                                if (temp.attr('check') == 1) {
                                    part1.children("span").each(function (index, element) {
                                        if ($(this).attr('sel') == 1) {
                                            objL = $(this);
                                            objL.attr("check", "1");
                                            objL.attr("sel", "0");
                                            countL++;
                                        }
                                    });
                                    part2.children("span").each(function (index, element) {
                                        if ($(this).attr('sel') == 1) {
                                            objR = $(this);
                                            objR.attr("check", "1");
                                            objR.attr("sel", "0");
                                            countR++;
                                        }
                                    });

                                    if (countL == 1 && countR == 1) {
                                        mx.push(objL.attr('left'));
                                        my.push(objL.attr('top'));
                                        ms.push(0);
                                        objL.attr("pair", pair);
                                        pairl.push(pair);
                                        mx.push(objR.attr('left'));
                                        my.push(objR.attr('top'));
                                        ms.push(1);
                                        objR.attr("pair", pair);
                                        pairr.push(pair);
                                        pair += 1;
                                        part1.attr('first', 0);
                                        part2.attr('first', 0);
                                        var reLine = pairSet / 2;
                                        part1.find("span[pair='" + reLine + "']").removeClass("addstyle").attr("check", "0").removeAttr("pair");
                                        part2.find("span[pair='" + reLine + "']").removeClass("addstyle").attr("check", "0").removeAttr("pair");
                                        strockline2(pairSet);
                                    } else {
                                        box.find(".showitem").attr("sel", "0");
                                        for (var i = 0; i < subSize; i++) {
                                            if (orderCheck[i] == true) {
                                                box.find(".showitem").eq(i).addClass("addstyle").attr("check", "1");
                                                box.find(".showitem").eq(i).attr("pair", orderPair[i]);
                                            } else {
                                                box.find(".showitem").eq(i).removeClass("addstyle").attr("check", "0");
                                                box.find(".showitem").eq(i).removeAttr("pair");
                                            }
                                        }
                                        var sizeL = part1.find("addstyle").length;
                                        var sizeR = part2.find("addstyle").length;
                                        box.find("span[sel='1']").removeClass("addstyle");
                                        if (sizeL != sizeR) {
                                            alert("size");
                                        }

                                    }

                                } else {
                                    part1.children("span").each(function (index, element) {
                                        if ($(this).attr('sel') == 1) {
                                            mx.push($(this).attr('left'));
                                            my.push($(this).attr('top'));
                                            ms.push(0);
                                            $(this).attr("check", "1");
                                            $(this).attr("sel", "0");
                                            $(this).attr("pair", pair);
                                            pairl.push(pair);
                                            mxid.push($(this).attr('id'));
                                        }
                                    });
                                    part2.children("span").each(function (index, element) {
                                        if ($(this).attr('sel') == 1) {
                                            mx.push($(this).attr('left'));
                                            my.push($(this).attr('top'));
                                            ms.push(1);
                                            $(this).attr("check", "1");
                                            $(this).attr("sel", "0");
                                            $(this).attr("pair", pair);
                                            pairr.push(pair);
                                            myid.push($(this).attr('id'));
                                        }
                                    });
                                    pair += 1;
                                    part1.attr('first', 0);
                                    part2.attr('first', 0);
                                    strockline();
                                };
                            } else {

                                part1.children("span").each(function (index, element) {
                                    if ($(this).attr('sel') == 1) {
                                        if ($(this).attr('check') == 0) {
                                            $(this).attr("sel", "0").removeClass("addstyle");
                                        } else {
                                            $(this).attr("sel", "0").addClass("addstyle");
                                        };
                                    }
                                });
                                part1.attr('first', 0);
                                part2.children("span").each(function (index, element) {
                                    if ($(this).attr('sel') == 1) {
                                        if ($(this).attr('check') == 0) {
                                            $(this).attr("sel", "0").removeClass("addstyle");
                                        } else {
                                            $(this).attr("sel", "0").addClass("addstyle");
                                        };
                                    }
                                });
                                part2.attr('first', 0);
                            };
                        } else {

                            part1.children("span").each(function (index, element) {

                                if ($(this).attr('check') == 0) {
                                    if ($(this).attr('sel') == 1) {
                                        $(this).attr("sel", "0").removeClass("addstyle");
                                    };
                                } else {
                                    if ($(this).attr('sel') == 1) {
                                        $(this).attr("sel", "0").addClass("addstyle");
                                    };
                                };
                            });
                            part1.attr('first', 0);
                            part2.children("span").each(function (index, element) {
                                if ($(this).attr('check') == 0) {
                                    if ($(this).attr('sel') == 1) {
                                        $(this).attr("sel", "0").removeClass("addstyle");
                                    };
                                } else {
                                    if ($(this).attr('sel') == 1) {
                                        $(this).attr("sel", "0").addClass("addstyle");
                                    };
                                };
                            });
                            part2.attr('first', 0);
                        };
                    } else {
                        part1.children("span").each(function (index, element) {
                            if ($(this).attr('check') == 0) {
                                if ($(this).attr('sel') == 1) {
                                    $(this).attr("sel", "0").removeClass("addstyle");
                                };
                            };
                        });
                        part1.attr('first', 0);
                        part2.children("span").each(function (index, element) {
                            if ($(this).attr('check') == 0) {
                                if ($(this).attr('sel') == 1) {
                                    $(this).attr("sel", "0").removeClass("addstyle");
                                };
                            };
                        });
                        part2.attr('first', 0);
                    };
                    clearbackline();
                    groupstate = false;
                    box.attr("pair", pair);
                    if (pair > 0) {
                        $("#orderList_" + commonId).addClass("active");
                    }
                };
            });

        });
        parentDom.find(".resetBtn").click(function () {
            clearline();
            $("#orderList_" + commonId).removeClass("active");
        })
        doms.on("touchstart", function (event) {
            countL = 0;
            countR = 0;
            for (var i = 0; i < subSize; i++) {
                if (box.find(".showitem").eq(i).hasClass("addstyle") == true) {
                    orderCheck[i] = true;
                    orderPair[i] = box.find(".showitem").eq(i).attr("pair");
                } else {
                    orderCheck[i] = false;
                    orderPair[i] = null;
                }
            }

            pairSet = parseInt($(this).attr("pair")) * 2;
            groupstate = true;

            var testP;
            if ($(this).attr("check") == 1) {
                $(this).attr("sel", "1").parent().attr("first", "1");
                temp = $(this);
            } else {
                $(this).attr("sel", "1").addClass("addstyle").parent().attr("first", "1");
                temp = $(this);

            };
            mid_startx = $(this).attr("left");
            mid_starty = $(this).attr("top");
            event.preventDefault();
            doms.on("touchmove", function (e) { //移动
                e.preventDefault();
                var $target = $(e.target);
                if (groupstate) {
                    mid_endx = e.originalEvent.touches[0].pageX - box.find(".show").offset().left;
                    mid_endy = e.originalEvent.touches[0].pageY - box.find(".show").offset().top;
                    var targettrue = null;
                    $('.showitem').each(function (i, val) {
                        var x1 = $(this).offset().left;
                        var y1 = $(this).offset().top;
                        var x2 = $(this).offset().left + $(this).outerWidth();
                        var y2 = $(this).offset().top + $(this).outerHeight();
                        if (e.originalEvent.touches[0].pageX >= x1 && e.originalEvent.touches[0].pageX <= x2 && e.originalEvent.touches[0].pageY >= y1 && e.originalEvent.touches[0].pageY <= y2) {
                            targettrue = $(this);
                            return false;
                        } else {
                            targettrue = null;
                        }
                    })
                    if (targettrue) {
                        if (targettrue.parent().attr("first") == 0) {
                            if (targettrue.attr("check") == 0) {
                                targettrue.addClass("addstyle").attr("sel", "1").siblings("span[check=0]").removeClass("addstyle").attr("sel", "0");
                            };
                        } else {
                            if (targettrue.attr("check") == 0) {
                                targettrue.addClass("addstyle").attr("sel", "1").siblings("span[check=0]").removeClass("addstyle").attr("sel", "0");
                                mid_startx = targettrue.attr("left");
                                mid_starty = targettrue.attr("top");
                            };

                        };
                    } else {
                        if (part1.attr("first") == 0) {
                            part1.children("span").each(function (index, element) {
                                if ($(this).attr('check') == 0) {
                                    $(this).attr("sel", "0").removeClass("addstyle");
                                };
                            });
                        };
                        if (part2.attr("first") == 0) {
                            part2.children("span").each(function (index, element) {
                                if ($(this).attr('check') == 0) {
                                    $(this).attr("sel", "0").removeClass("addstyle");
                                };
                            });
                        };

                    };
                    backstrockline();
                };
            });
            doms.on("touchend", function (e) { //抬起
                e.preventDefault();
                var $target = $(e.target);
                if (groupstate) {
                    var targettrue = null;
                    $('.showitem').each(function (i, val) {
                        var x1 = $(this).offset().left;
                        var y1 = $(this).offset().top;
                        var x2 = $(this).offset().left + $(this).outerWidth();
                        var y2 = $(this).offset().top + $(this).outerHeight();
                        if (e.changedTouches[0].pageX >= x1 && e.changedTouches[0].pageX <= x2 && e.changedTouches[0].pageY >= y1 && e.changedTouches[0].pageY <= y2) {
                            targettrue = $(this);
                            return false;
                        } else {
                            targettrue = null;
                        }
                    })
                    if (targettrue) {
                        if (targettrue.parent().attr("first") == 0) {
                            if (targettrue.attr("check") == 0) {
                                if (temp.attr('check') == 1) {
                                    part1.children("span").each(function (index, element) {
                                        if ($(this).attr('sel') == 1) {
                                            objL = $(this);
                                            objL.attr("check", "1");
                                            objL.attr("sel", "0");
                                            countL++;
                                        }
                                    });
                                    part2.children("span").each(function (index, element) {
                                        if ($(this).attr('sel') == 1) {
                                            objR = $(this);
                                            objR.attr("check", "1");
                                            objR.attr("sel", "0");
                                            countR++;
                                        }
                                    });

                                    if (countL == 1 && countR == 1) {
                                        mx.push(objL.attr('left'));
                                        my.push(objL.attr('top'));
                                        ms.push(0);
                                        objL.attr("pair", pair);
                                        pairl.push(pair);
                                        mx.push(objR.attr('left'));
                                        my.push(objR.attr('top'));
                                        ms.push(1);
                                        objR.attr("pair", pair);
                                        pairr.push(pair);
                                        pair += 1;
                                        part1.attr('first', 0);
                                        part2.attr('first', 0);
                                        var reLine = pairSet / 2;
                                        part1.find("span[pair='" + reLine + "']").removeClass("addstyle").attr("check", "0").removeAttr("pair");
                                        part2.find("span[pair='" + reLine + "']").removeClass("addstyle").attr("check", "0").removeAttr("pair");
                                        strockline2(pairSet);
                                    } else {
                                        box.find(".showitem").attr("sel", "0");
                                        for (var i = 0; i < subSize; i++) {
                                            if (orderCheck[i] == true) {
                                                box.find(".showitem").eq(i).addClass("addstyle").attr("check", "1");
                                                box.find(".showitem").eq(i).attr("pair", orderPair[i]);
                                            } else {
                                                box.find(".showitem").eq(i).removeClass("addstyle").attr("check", "0");
                                                box.find(".showitem").eq(i).removeAttr("pair");
                                            }
                                        }
                                        var sizeL = part1.find("addstyle").length;
                                        var sizeR = part2.find("addstyle").length;
                                        box.find("span[sel='1']").removeClass("addstyle");
                                        if (sizeL != sizeR) {
                                            alert("size");
                                        }

                                    }

                                } else {
                                    part1.children("span").each(function (index, element) {
                                        if ($(this).attr('sel') == 1) {
                                            mx.push($(this).attr('left'));
                                            my.push($(this).attr('top'));
                                            ms.push(0);
                                            $(this).attr("check", "1");
                                            $(this).attr("sel", "0");
                                            $(this).attr("pair", pair);
                                            pairl.push(pair);
                                            mxid.push($(this).attr('id'));
                                        }
                                    });
                                    part2.children("span").each(function (index, element) {
                                        if ($(this).attr('sel') == 1) {
                                            mx.push($(this).attr('left'));
                                            my.push($(this).attr('top'));
                                            ms.push(1);
                                            $(this).attr("check", "1");
                                            $(this).attr("sel", "0");
                                            $(this).attr("pair", pair);
                                            pairr.push(pair);
                                            myid.push($(this).attr('id'));
                                        }
                                    });
                                    pair += 1;
                                    part1.attr('first', 0);
                                    part2.attr('first', 0);
                                    strockline();
                                };
                            } else {

                                part1.children("span").each(function (index, element) {
                                    if ($(this).attr('sel') == 1) {
                                        if ($(this).attr('check') == 0) {
                                            $(this).attr("sel", "0").removeClass("addstyle");
                                        } else {
                                            $(this).attr("sel", "0").addClass("addstyle");
                                        };
                                    }
                                });
                                part1.attr('first', 0);
                                part2.children("span").each(function (index, element) {
                                    if ($(this).attr('sel') == 1) {
                                        if ($(this).attr('check') == 0) {
                                            $(this).attr("sel", "0").removeClass("addstyle");
                                        } else {
                                            $(this).attr("sel", "0").addClass("addstyle");
                                        };
                                    }
                                });
                                part2.attr('first', 0);
                            };
                        } else {

                            part1.children("span").each(function (index, element) {

                                if ($(this).attr('check') == 0) {
                                    if ($(this).attr('sel') == 1) {
                                        $(this).attr("sel", "0").removeClass("addstyle");
                                    };
                                } else {
                                    if ($(this).attr('sel') == 1) {
                                        $(this).attr("sel", "0").addClass("addstyle");
                                    };
                                };
                            });
                            part1.attr('first', 0);
                            part2.children("span").each(function (index, element) {
                                if ($(this).attr('check') == 0) {
                                    if ($(this).attr('sel') == 1) {
                                        $(this).attr("sel", "0").removeClass("addstyle");
                                    };
                                } else {
                                    if ($(this).attr('sel') == 1) {
                                        $(this).attr("sel", "0").addClass("addstyle");
                                    };
                                };
                            });
                            part2.attr('first', 0);
                        };
                    } else {
                        part1.children("span").each(function (index, element) {
                            if ($(this).attr('check') == 0) {
                                if ($(this).attr('sel') == 1) {
                                    $(this).attr("sel", "0").removeClass("addstyle");
                                };
                            };
                        });
                        part1.attr('first', 0);
                        part2.children("span").each(function (index, element) {
                            if ($(this).attr('check') == 0) {
                                if ($(this).attr('sel') == 1) {
                                    $(this).attr("sel", "0").removeClass("addstyle");
                                };
                            };
                        });
                        part2.attr('first', 0);
                    };
                    clearbackline();
                    groupstate = false;
                    box.attr("pair", pair);
                    if (pair > 0) {
                        $("#orderList_" + commonId).addClass("active");
                    }
                };
            });
        });
        var linewidth = 2,
			linestyle = "#666666"; //连线绘制--线宽，线色
        //canvas 追加2d画布
        var context = canvas.getContext('2d'); //canvas追加2d画图
        var lastX, lastY; //存放遍历坐标
        function strockline() { //绘制方法
            context.clearRect(0, 0, box.find(".show").width(), box.find(".show").height()); //整个画布清除
            context.save();
            context.beginPath();
            context.lineWidth = linewidth;
            for (var i = 0; i < ms.length; i++) { //遍历绘制 
                lastX = mx[i];
                lastY = my[i];
                if (ms[i] == 0) {
                    context.moveTo(lastX, lastY);
                } else {
                    context.lineTo(lastX, lastY);
                }
            }
            context.strokeStyle = linestyle;
            context.stroke();
            context.restore();
        };

        function strockline2(pairSet) { //绘制方法
            context.clearRect(0, 0, box.find(".show").width(), box.find(".show").height()); //整个画布清除
            context.save();
            context.beginPath();
            context.lineWidth = linewidth;
            var clearLine = pairSet;
            for (var i = 0; i < ms.length; i++) { //遍历绘制 
                if (clearLine == i) {
                    mx[i] = null;
                    my[i] = null;
                }
                if ((clearLine + 1) == i) {
                    mx[i] = null;
                    my[i] = null;
                }
                lastX = mx[i];
                lastY = my[i];
                if (ms[i] == 0) {
                    context.moveTo(lastX, lastY);
                } else {
                    context.lineTo(lastX, lastY);
                }

            }
            context.strokeStyle = linestyle;
            context.stroke();
            // context.restore(); 
        };

        function clearline() { //清除
            context.clearRect(0, 0, box.find(".show").width(), box.find(".show").height());
            mx = []; //数据清除
            my = [];
            ms = [];
            pairl = [];
            pairr = [];
            pair = 0;
            part1.children("span").each(function (index, element) {
                $(this).removeClass("addstyle");
                $(this).attr("sel", "0");
                $(this).attr("check", "0");

            });
            part1.attr('first', 0);
            part2.children("span").each(function (index, element) {
                $(this).removeClass("addstyle");
                $(this).attr("sel", "0");
                $(this).attr("check", "0");
            });
            part2.attr('first', 0);
        };
        //init backcanvas 2d画布 虚拟绘制
        var backcxt = backcanvas.getContext('2d');

        function backstrockline() { //绘制
            backcxt.clearRect(0, 0, box.find(".show").width(), box.find(".show").height());
            backcxt.save();
            backcxt.beginPath();
            backcxt.lineWidth = linewidth;
            backcxt.strokeStyle = linestyle;
            backcxt.moveTo(parseFloat(mid_startx), parseFloat(mid_starty));
            backcxt.lineTo(parseFloat(mid_endx), parseFloat(mid_endy));

            backcxt.stroke();
            backcxt.closePath();
            backcxt.restore();
        };

        function clearbackline() { //清除
            backcxt.clearRect(0, 0, box.find(".show").width(), box.find(".show").height());
            mid_startx = null;
            mid_starty = null;
            mid_endx = null;
            mid_endy = null;
        };


        //重置
        box.find(".AnnotationRevoke").click(function () {
            clearline();
        })

        function goBack() {
            var linenlastIndex = ms.join("").substr(0, ms.length - 1).lastIndexOf("0");
            if (linenlastIndex == 0) {
                clearline();
            } else {
                mx = mx.slice(0, linenlastIndex); //记录值
                my = my.slice(0, linenlastIndex); //坐标
                ms = ms.slice(0, linenlastIndex);
                context.clearRect(0, 0, box.find(".show").width(), box.find(".show").height());
                context.save();
                context.beginPath();
                context.lineWidth = linewidth;
                for (var i = 0; i < ms.length; i++) {
                    lastX = mx[i];
                    lastY = my[i];
                    if (ms[i] == 0) {
                        context.moveTo(lastX, lastY);
                    } else {
                        context.lineTo(lastX, lastY);
                    }
                }
                context.strokeStyle = linestyle;
                context.stroke();
                context.restore();
                var pairindex = pairl.length - 1;
                part1.children("span").each(function (index, element) {
                    if ($(this).attr("pair") == pairl[pairindex]) {
                        $(this).removeClass("addstyle");
                        $(this).attr("sel", "0");
                        $(this).attr("check", "0");
                        $(this).removeAttr("pair");
                    };
                });
                part1.attr('first', 0);
                part2.children("span").each(function (index, element) {
                    if ($(this).attr("pair") == pairl[pairindex]) {
                        $(this).removeClass("addstyle");
                        $(this).attr("sel", "0");
                        $(this).attr("check", "0");
                        $(this).removeAttr("pair");
                    };
                });
                part2.attr('first', 0);
                pair -= 1;
                pairl = pairl.slice(0, pairindex);
                pairr = pairr.slice(0, pairindex);
            };
        };

    }
})(jQuery);