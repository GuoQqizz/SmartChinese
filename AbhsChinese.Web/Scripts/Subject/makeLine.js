(function ($) {
    $.fn.makeLine = function (options) {
        var box = this;
        var allData = options.allData;
        var slos = [];
        var sros = [];
        for (var s in allData.LeftOptions) {
            slos.push(allData.LeftOptions[s].Key)
        }
        for (var m in allData.RightOptions) {
            sros.push(allData.RightOptions[m].Key)
        }

        var part1, part2;
        part1 = box.find(".showleft");
        part2 = box.find(".showright");
        //初始化赋值 列表内容
        box.find(".showleft").children("span").each(function (index, element) {
            $(this).attr({ group: "gpl", left: $(this).position().left + $(this).outerWidth(), top: $(this).position().top + $(this).outerHeight() / 2, sel: "0", check: "0" });
        });
        box.find(".showright").children("span").each(function (index, element) {
            $(this).attr({ group: "gpr", left: $(this).position().left, top: $(this).position().top + $(this).outerHeight() / 2, sel: "0", check: "0" });
        });
        part1.attr('first', 0);//初始赋值 列表内容容器
        part2.attr('first', 0);
        //canvas 赋值
        var canvas = box.find(".canvas")[0];  //获取canvas  实际连线标签
        canvas.width = box.find(".show").width();//canvas宽度等于div容器宽度
        canvas.height = box.find(".show").height();
        var backcanvas = box.find(".backcanvas")[0];  //获取canvas 模拟连线标签  
        backcanvas.width = box.find(".show").width();
        backcanvas.height = box.find(".show").height();
        //连线数据
        var groupstate = false;//按下事件状态，标记按下后的移动，抬起参考
        var mx = [];//连线坐标
        var my = [];
        var ms = [];
        var pairl = [];
        var pairr = [];
        var pair = 0;//配对属性
        var temp;//存贮按下的对象
        var mxid = [];
        var myid = [];
        var objL, objR;
        //提示线数据
        var mid_startx, mid_starty, mid_endx, mid_endy;//存储虚拟连线起始坐标
        //事件处理---按下
        var pairSet, countL, countR, getPairL, getPairR, orderCheck;
        var orderCheck = new Array();
        var orderPair = new Array();
        var subSize = box.find(".showitem").length;
        //addstyle showitem
        var doms = box.children(".show").children().children("span");
        doms.on("mousedown", function (event) {
            countL = 0;
            countR = 0;
            for (var i = 0; i < subSize; i++) {
                if (box.find(".showitem").eq(i).hasClass("addstyle") == true) {
                    orderCheck[i] = true;
                    orderPair[i] = box.find(".showitem").eq(i).attr("pair");
                }
                else {
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
            $(document).mousemove(function (event) {		//移动				
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
            $(document).mouseup(function (event) {  //抬起
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
                                    }
                                    else {
                                        box.find(".showitem").attr("sel", "0");
                                        for (var i = 0; i < subSize; i++) {
                                            if (orderCheck[i] == true) {
                                                box.find(".showitem").eq(i).addClass("addstyle").attr("check", "1");
                                                box.find(".showitem").eq(i).attr("pair", orderPair[i]);
                                            }
                                            else {
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

                };
            });

        });
        doms.on("touchstart", function (event) {
            countL = 0;
            countR = 0;
            for (var i = 0; i < subSize; i++) {
                if (box.find(".showitem").eq(i).hasClass("addstyle") == true) {
                    orderCheck[i] = true;
                    orderPair[i] = box.find(".showitem").eq(i).attr("pair");
                }
                else {
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
            doms.on("touchmove", function (e) {		//移动
                e.preventDefault();
                var $target = $(e.target);
                if (groupstate) {
                    mid_endx = e.originalEvent.touches[0].clientX - box.find(".show").offset().left;
                    mid_endy = e.originalEvent.touches[0].clientY - box.find(".show").offset().top;
                    var targettrue = null;
                    $('.showitem').each(function (i, val) {
                        var x1 = $(this).offset().left;
                        var y1 = $(this).offset().top;
                        var x2 = $(this).offset().left + $(this).outerWidth();
                        var y2 = $(this).offset().top + $(this).outerHeight();
                        if (e.originalEvent.touches[0].clientX >= x1 && e.originalEvent.touches[0].clientX <= x2 && e.originalEvent.touches[0].clientY >= y1 && e.originalEvent.touches[0].clientY <= y2) {
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
            doms.on("touchend", function (e) {  //抬起
                e.preventDefault();
                var $target = $(e.target);
                if (groupstate) {
                    var targettrue = null;
                    $('.showitem').each(function (i, val) {
                        var x1 = $(this).offset().left;
                        var y1 = $(this).offset().top;
                        var x2 = $(this).offset().left + $(this).outerWidth();
                        var y2 = $(this).offset().top + $(this).outerHeight();
                        if (e.changedTouches[0].clientX >= x1 && e.changedTouches[0].clientX <= x2 && e.changedTouches[0].clientY >= y1 && e.changedTouches[0].clientY <= y2) {
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
                                    }
                                    else {
                                        box.find(".showitem").attr("sel", "0");
                                        for (var i = 0; i < subSize; i++) {
                                            if (orderCheck[i] == true) {
                                                box.find(".showitem").eq(i).addClass("addstyle").attr("check", "1");
                                                box.find(".showitem").eq(i).attr("pair", orderPair[i]);
                                            }
                                            else {
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

                };
            });
        });
        var linewidth = 2, linestyle = "#8C611C";//连线绘制--线宽，线色
        //canvas 追加2d画布
        var context = canvas.getContext('2d');  //canvas追加2d画图
        var lastX, lastY;//存放遍历坐标
        function strockline() {//绘制方法
            context.clearRect(0, 0, box.find(".show").width(), box.find(".show").height());//整个画布清除
            context.save();
            context.beginPath();
            context.lineWidth = linewidth;
            for (var i = 0; i < ms.length; i++) {  //遍历绘制 
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
        function strockline2(pairSet) {//绘制方法
            context.clearRect(0, 0, box.find(".show").width(), box.find(".show").height());//整个画布清除
            context.save();
            context.beginPath();
            context.lineWidth = linewidth;
            var clearLine = pairSet;
            for (var i = 0; i < ms.length; i++) {  //遍历绘制 
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
        function clearline() {//清除
            context.clearRect(0, 0, box.find(".show").width(), box.find(".show").height());
            mx = [];//数据清除
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
        function backstrockline() {//绘制
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
        function clearbackline() {//清除
            backcxt.clearRect(0, 0, box.find(".show").width(), box.find(".show").height());
            mid_startx = null; mid_starty = null; mid_endx = null; mid_endy = null;
        };

        //获取连线题对应的key-value
        function getListPair() {
            var index;
            var leftVal, rightVal, nowVal;
            var sum = pair * 2;
            var pairA = new Array(sum);
            var rightPart1 = box.find(".showleft");
            var rightPart2 = box.find(".showright");
            var size = rightPart1.find(".showitem").length;
            for (var i = 0; i < size; i++) {
                if (rightPart1.find(".showitem").eq(i).hasClass("addstyle") == true) {
                    nowVal = rightPart1.find(".showitem").eq(i).attr("pair");
                    leftVal = parseInt(nowVal) * 2;
                    pairA[leftVal] = i;
                }
            }
            for (var i = 0; i < size; i++) {
                if (rightPart2.find(".showitem").eq(i).hasClass("addstyle") == true) {
                    nowVal = rightPart2.find(".showitem").eq(i).attr("pair");
                    rightVal = parseInt(nowVal) * 2 + 1;
                    pairA[rightVal] = i;
                }
            }
            var idStr = [];
            var idAttr = [];
            for (var i = 0; i < sum; i++) {
                if (typeof pairA[i] != "undefined") {
                    if (i % 2 == 0) {
                        idStr.push(Number(rightPart1.find(".showitem").eq(pairA[i]).attr("thisId")));
                    }
                    else {
                        idStr = idStr.concat([Number((rightPart2.find(".showitem").eq(pairA[i]).attr("thisId")))]);
                        idAttr.push(idStr);
                        idStr = [];
                    }
                }
            }
            return idAttr;
        }

        function saveListPair() {
            var size = box.find(".showitem").length;
            var listPair = new Array(size);
            for (var i = 0; i < size; i++) {
                if (box.find(".showitem").eq(i).hasClass("addstyle") == true) {
                    listPair[i] = box.find(".showitem").eq(i).attr("pair");
                }
                else {
                    listPair[i] = null;
                }
            }
        }

        //生成连线题默认的答案
        function newCanvas(dom) {
            var leftPair = new Array();
            var rightPair = new Array();
            var part1, part2;
            part1 = dom.find(".showleft");
            part2 = dom.find(".showright");
            //初始化赋值 列表内容
            dom.find(".showleft").children("span").each(function (index, element) {
                $(this).attr({ group: "gpl", left: $(this).position().left + $(this).outerWidth(), top: $(this).position().top + $(this).outerHeight() / 2, sel: "0", check: "0" });
            });
            dom.find(".showright").children("span").each(function (index, element) {
                $(this).attr({ group: "gpr", left: $(this).position().left, top: $(this).position().top + $(this).outerHeight() / 2, sel: "0", check: "0" });
            });
            part1.attr('first', 0);//初始赋值 列表内容容器
            part2.attr('first', 0);

            for (var k = 0; k < allData.LeftOptions.length; k++) {
                for (var n = 0; n < allData.Answer.length; n++) {
                    if (allData.LeftOptions[k].Key == allData.Answer[n][0]) {
                        var num = allData.Answer[n][1];
                        for (var s = 0; s < allData.RightOptions.length; s++) {
                            if (allData.RightOptions[s].Key == num) {
                                leftPair[k] = s;
                            }
                        }

                    }
                }
            }
            for (var k = 0; k < allData.RightOptions.length; k++) {
                rightPair[k] = k;
            }
            var pairA = new Array(sum);
            var size = part1.find(".showitem").length;
            var lastPair = parseInt(dom.attr("data_pair"));
            var index;
            var leftVal, rightVal, nowVal;
            var sum = lastPair * 2;
            for (var i = 0; i < size; i++) {
                if (typeof leftPair[i] == "number") {
                    nowVal = leftPair[i];
                    leftVal = parseInt(nowVal) * 2;
                    pairA[leftVal] = i;
                    part1.find(".showitem").eq(i).addClass("addstyle").attr("check", "1").attr("pair", leftPair[i]);
                }
                else {
                    part1.find(".showitem").eq(i).removeClass("addstyle").attr("check", "0").removeAttr("pair");
                }
                if (typeof rightPair[i] == "number") {
                    nowVal = rightPair[i];
                    rightVal = parseInt(nowVal) * 2 + 1;
                    pairA[rightVal] = i;
                    part2.find(".showitem").eq(i).addClass("addstyle").attr("check", "1").attr("pair", rightPair[i]);
                }
                else {
                    part2.find(".showitem").eq(i).removeClass("addstyle").attr("check", "0").removeAttr("pair");
                }
            }

            var leftPoint;
            var topPoint;
            var newMs = [];
            for (var i = 0; i <= (sum + 2) ; i++) {
                if (typeof pairA[i] !== "undefined") {
                    if (i % 2 == 0) {
                        leftPoint = parseInt(part1.find(".showitem").eq(pairA[i]).attr("left"));
                        topPoint = parseInt(part1.find(".showitem").eq(pairA[i]).attr("top"));
                        mx[i] = leftPoint;
                        my[i] = topPoint;
                        newMs[i] = 0;
                    }
                    else {
                        leftPoint = parseInt(part2.find(".showitem").eq(pairA[i]).attr("left"));
                        topPoint = parseInt(part2.find(".showitem").eq(pairA[i]).attr("top"));
                        mx[i] = leftPoint;
                        my[i] = topPoint;
                        newMs[i] = 1;
                    }
                }
                else {
                    mx[i] = null;
                    my[i] = null;
                    newMs[i] = 0;
                }
            }
            var newcanvas = dom.find(".canvas")[0];  //获取canvas  实际连线标签
            newcanvas.width = dom.find(".show").width();//canvas宽度等于div容器宽度
            newcanvas.height = dom.find(".show").height();
            var newbackcanvas = dom.find(".backcanvas")[0];  //获取canvas 模拟连线标签  
            newbackcanvas.width = dom.find(".show").width();
            newbackcanvas.height = dom.find(".show").height();


            var context1 = newcanvas.getContext('2d');
            context1.clearRect(0, 0, dom.find(".show").width(), box.find(".show").height());//整个画布清除
            context1.save();
            context1.beginPath();
            context1.lineWidth = linewidth;
            for (var i = 0; i < newMs.length; i++) {  //遍历绘制 
                lastX = mx[i];
                lastY = my[i];
                if (newMs[i] == 0) {
                    context1.moveTo(lastX, lastY);
                } else {
                    context1.lineTo(lastX, lastY);
                }
            }
            context1.strokeStyle = linestyle;
            context1.stroke();
            context1.restore();
        };

        //获取连线数据 
        $(".TodosSubmit").click(function () {
            judgeLinkingAnswer();
        })

        //重置
        $(".TodosRevoke").click(function () {
            clearline();
        })

        function judgeLinkingAnswer() {
            var myAnwser = getListPair();
            var allCount = 0;
            var num = 0;
            if (myAnwser.length == 0) {
                allCount = 0;
            } else {
                for (var i = 0; i < myAnwser.length; i++) {
                    var str1 = JSON.stringify(myAnwser[i]);
                    var str2 = JSON.stringify(allData.Answer);
                    if (str2.indexOf(str1) > -1) {
                        num++
                    }
                }
                allCount = Math.round(5 * num / allData.Answer.length);
                if (allCount == 0) {
                    allCount = 1;
                }
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
            $('.AnalysisContent').html(allData.Analysis);
            $(".wrongQuestionBox").hide();
            $(".checkAnswerBox").show();
           
            match($(".StandardContent"), allData.Answer, allData, true);
            match($(".mySubAnswerContent"), myAnwser, allData, false);
            function match(dom, answer, data, isRight) {
                let html = "";
                let array = [];
                let left = data.LeftOptions;
                let right = data.RightOptions;
                for (var i = 0; i < left.length; i++) {
                    array.push([left[i].Text, left[i].Key]);
                    array.push([right[i].Text, right[i].Key]);
                }
                var L = dom.width() - 110;
                //添加连线题选项
                for (var i = 0; i < array.length; i++) {
                    var d = array[i];
                    var top = Math.floor(i / 2) * 60;
                    if (i % 2 == 0) {
                        if (isRight) {
                            html += '<div style="top:' + top + 'px" class="questitem questitem2 leftoption' + d[1] + '">';
                        } else {
                            html += '<div style="top:' + top + 'px" class="questitem leftoption' + d[1] + '">';
                        }

                        if (data.LeftOptionType == 1) {
                            html += d[0];
                        } else { //如果左边是图片
                            html += "<img src ='" + d[0] + "' />";
                        }
                        html += '</div>';
                    } else {
                        if (isRight) {
                            html += '<div style="top:' + top + 'px;left:' + L + 'px;" class="questitem questitem2 rightoption' + d[1] + '">';
                        } else {
                            html += '<div style="top:' + top + 'px;left:' + L + 'px;" class="questitem rightoption' + d[1] + '">';
                        }

                        if (data.RightOptionType == 1) { //如果左边是图片
                            html += d[0];
                        } else {
                            html += "<img src ='" + d[0] + "' />";

                        }
                        html += '</div>';
                    }

                }
                //结束连线题选项
                dom.html(html);
                var hgt = (Math.floor(array.length / 2)) * 60;
                dom.css({
                    'height': hgt + 'px'
                });
                //遍历学生答案
                for (var i = 0; i < answer.length; i++) {
                    var sa = answer[i]; //学生划线
                    var isTrue = false; //假设答案错误
                    for (var o = 0; o < data.Answer.length; o++) //循环判断是否正确
                    {
                        var a = data.Answer[o]; //正确答案连线
                        if (sa[0] == a[0]) //如果左边是同一个选项
                        {
                            isTrue = sa[1] == a[1]; //右边选项是否正确
                            break;
                        }
                    }
                    var leftDom = $(dom).find(".leftoption" + sa[0]);
                    var rightDom = $(dom).find(".rightoption" + sa[1]);
                    drawLine(leftDom, rightDom, dom, isTrue);
                }
            }
            var H1 = $(".checkAnswerPlayBox")[0].scrollHeight, H2 = $(".checkAnswerPlayBox")[0].clientHeight;
            if (H1 > H2) {
                $(".ScrollDown").show();
            }
            $(".checkAnswerPlayBox").scroll(function () {
                SrollMonitor($(this));
            })
            // 添加连线
            function drawLine(startObj, endObj, objdemo, anws) {
                //console.log(startObj.html(), endObj.html());
                var html = "";
                var y_start = Number(startObj.css("top").replace("px", "")) + startObj.height() / 2;
                var x_start = Number(startObj.css("left").replace("px", "")) + startObj.width();
                var y_end = Number(endObj.css("top").replace("px", "")) + endObj.height() / 2;
                var x_end = Number(endObj.css("left").replace("px", ""));
                var deg = 0;
                if (y_start == y_end) // 画横线
                {
                    if (x_start > x_end) {
                        var t = x_start;
                        x_start = x_end;
                        x_end = t
                        deg = 180;
                    }
                    length = Math.abs(x_end - x_start);
                } else if (x_start == x_end) // 画竖线
                {
                    deg = 90;
                    if (y_start > y_end) {
                        var t = y_start;
                        y_start = y_end;
                        y_end = t
                        deg = 270;
                    }
                    length = Math.abs(y_end - y_start);
                } else { // 画线旋转角度
                    var lx = x_end - x_start;
                    var ly = y_end - y_start;
                    var length = Math.sqrt(lx * lx + ly * ly); //计算连线长度
                    var c = 360 * Math.atan2(ly, lx) / (2 * Math.PI); //弧度值转换为角度值
                    c = c <= -90 ? (360 + c) : c; //负角转换为正角
                    deg = c;
                }
                if (anws) {
                    html = "<div class='rotate' style='background:#499D90;top:" + y_start + "px;left:" + (x_start + 1) +
						"px;width:" + length + "px;transform:rotate(" + deg + "deg)'></div>";
                } else {
                    startObj.addClass('erroritem');
                    endObj.addClass('erroritem');
                    html = "<div class='rotate' style='background:#e85743;top:" + y_start + "px;left:" + (x_start + 1) +
						"px;width:" + length + "px;transform:rotate(" + deg + "deg)'></div>";
                }
                $(objdemo).append(html)
            }

            $(".sureBtn").click(function () {
                submitLinkingAnswers();
            })
            function submitLinkingAnswers() {
                var thisSubjectAnswerData = {};
                thisSubjectAnswerData.type = allData.SubjectType; //题目类型 
                thisSubjectAnswerData.sbId = allData.SubjectId; //题目id
                thisSubjectAnswerData.rtar = allCount; //获得星数
                thisSubjectAnswerData.kdge = allData.KnowledgeId; //知识点ID
                thisSubjectAnswerData.sanw = myAnwser; //答案
                thisSubjectAnswerData.slos = slos;//左边顺序
                thisSubjectAnswerData.sros = sros;//左边顺序
                submitQuestionAnswer(thisSubjectAnswerData);
            }
        }

        function goBack() {
            var linenlastIndex = ms.join("").substr(0, ms.length - 1).lastIndexOf("0");
            if (linenlastIndex == 0) {
                clearline();
            } else {
                mx = mx.slice(0, linenlastIndex);  //记录值
                my = my.slice(0, linenlastIndex);  //坐标
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

        /* 判断是否是IE删除节点*/
        function removeDom(dom) {
            if (!!window.ActiveXobject || "ActiveXObject" in window) {
                dom.parentNode.removeChild(dom)
            } else {
                dom.remove();
            }
        }
        //元素监听自身是否滚动到底
        function SrollMonitor(dom) {
            var scrollTop = dom.scrollTop();
            var ks_area = Math.ceil(dom.innerHeight());
            var nScrollHight = 0; //滚动距离总长(注意不是滚动条的长度)  
            nScrollHight = dom[0].scrollHeight;
            if (scrollTop + ks_area >= nScrollHight - 10) {
                $(".ScrollDown").hide()
            }
        }
    }
})(jQuery);