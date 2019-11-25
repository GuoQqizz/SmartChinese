$(document).ready(function () {

    var studentId = $("#studentId").val();
    var orderId = $("#orderId").val();
    var courseInfo = JSON.parse($("#courseInfo").val());

    function intiVoucherList() {
        if (courseInfo && courseInfo.CourseId > 0) {
            var requestData = { OrderAmountLimit: courseInfo.CoursePrice, Grade: courseInfo.Grade, CourseType: courseInfo.CourseType, CourseId: courseInfo.CourseId };

            var bindVouchers_Deferred = $.Deferred();

            getCashVoucherByOrderId({ OrderId: orderId }, function (bindVouchers) {
                var availableBindVouchers = [];//尚在有效期内的优惠券
                $.each(bindVouchers, function (index, item) {
                    switch (item.Ycv_ExpireType) {
                        case 1:
                        case 2:
                            if (!moment().isAfter(item.Ysv_ExpireDate, "day")) {
                                availableBindVouchers.push(item);
                            }
                            break;
                        case 3:
                            availableBindVouchers.push(item);
                            break;
                    }
                });
                bindVouchers_Deferred.resolve(availableBindVouchers);
            });

            $.when(bindVouchers_Deferred).done(function (bindVouchers) {
                //console.log("all done");
                getAllAvailableCashVoucher(requestData, function (availableList, ownedList, allVoucherList) {
                    if (availableList.length > 0) {
                        $("#AvailableCashVoucher_List").empty().append(renderVoucherList(availableList, false, true, false));
                        $("#AvailableCashVoucherPanel").show();
                        bindTakeCashVoucherEvent();
                    }
                    if (ownedList.length > 0 || bindVouchers.length > 0) {
                        var newOwnedList = ownedList;
                        $.each(bindVouchers, function (index, item) { newOwnedList.push(item) });
                        $("#HaveCashVoucher_List").empty().append(renderVoucherList(newOwnedList, true, true, true));
                        $("#HaveCashVoucherPanel").show();
                        bindVoucherSelectEvent();
                        //调用计算最优使用方案方法
                        bestUsePlan(ownedList, bindVouchers);
                    }
                    if (allVoucherList.length == 0 && bindVouchers.length == 0) {
                        $("#usedreceivedList").hide();
                        $("#noneVouchers").show();
                        $("#openconpoulist").hide();
                    } else {
                        $("#noneVouchers").hide();
                        if (ownedList.length == 0 && bindVouchers.length == 0) {
                            //如果已领取和已绑定都为空，但是尚有可用未领取的优惠券存在
                            $("#usedreceivedList").show();
                            var maxvoucher;
                            $.each(availableList, function (index, item) {
                                if (maxvoucher == undefined || item.Ycv_Amount > maxvoucher.Ycv_Amount) {
                                    maxvoucher = item;
                                }
                            });
                            $("#openconpoulist").text("领取" + maxvoucher.Ycv_Amount + (maxvoucher.Ycv_VoucherType == 8 ? "校区券" : "总部券") + " >").show();
                        } else {
                            $("#openconpoulist").text("更多可用现金券 >").show();
                        }
                    }
                });
            });
        }
    }

    intiVoucherList();

    //优惠券的最优使用方案
    function bestUsePlan(receivedList, highestPriorityReceivedList) {
        //console.log("计算最优使用方案方法，已领取优惠券%d个与订单绑定优惠券%d", receivedList.length, highestPriorityReceivedList.length);
        var baseVoucher, schoolVoucher;
        var hasBindBaseVoucher = false, hasBindSchoolVoucher = false;
        if (highestPriorityReceivedList != undefined && highestPriorityReceivedList.length > 0) {
            $.each(highestPriorityReceivedList, function (index, item) {
                if (item.Ycv_VoucherType != 8) {
                    if (baseVoucher == undefined || item.Ycv_Amount > baseVoucher.Ycv_Amount) {
                        baseVoucher = item;
                    }
                } else {
                    if (schoolVoucher == undefined || item.Ycv_Amount > schoolVoucher.Ycv_Amount) {
                        schoolVoucher = item;
                    }
                }
            });
        }
        hasBindBaseVoucher = baseVoucher != undefined;
        hasBindSchoolVoucher = schoolVoucher != undefined;
        //如果与订单关联的优惠券不存在总部券或校区券，需要从已领取中选取对应的最优券
        $.each(receivedList, function (index, item) {
            if (item.Ycv_VoucherType != 8) {
                if (!hasBindBaseVoucher && (baseVoucher == undefined || item.Ycv_Amount > baseVoucher.Ycv_Amount)) {
                    baseVoucher = item;
                }
            } else {
                if (!hasBindSchoolVoucher && (schoolVoucher == undefined || item.Ycv_Amount > schoolVoucher.Ycv_Amount)) {
                    schoolVoucher = item;
                }
            }
        });

        if (baseVoucher != undefined && baseVoucher.Ycv_Amount > 0) {
            $("#HaveCashVoucher_List .conpouitem[data-voucherid='" + baseVoucher.Ycv_Id + "']").triggerHandler("click");
        }
        if (schoolVoucher != undefined && schoolVoucher.Ycv_Amount > 0) {
            $("#HaveCashVoucher_List .conpouitem[data-voucherid='" + schoolVoucher.Ycv_Id + "']").triggerHandler("click");
        }
    }

    //系统推荐的最优使用方案是否被人为修改，如果被修改，再领取优惠券后将不再推荐最优方案
    var isBestUsePlanChangeByPerson = false;
    //绑定领取优惠券事件
    //领取成功后触发绑定已领优惠券选中取消事件
    function bindTakeCashVoucherEvent() {
        $("#AvailableCashVoucher_List span.couponbtn").unbind().click(throttle(function () {
            var voucherId = $(this).attr("data-voucherid");
            var requestData = { StudentId: studentId, CashVoucherId: voucherId, GotType: 2, GotReferId: 0 };
            takeStudentCashVoucher(requestData, function (result) {
                //领取成功
                if (result) {
                    $("#AvailableCashVoucher_List div.conpouitem[data-voucherid='" + voucherId + "']").fadeOut("slow", function () {
                        var voucher = JSON.parse(unescape($("#AvailableCashVoucher_List div.conpouitem[data-voucherid='" + voucherId + "']").attr("data-jsondata")));
                        //从可领取位置移除
                        $("#AvailableCashVoucher_List div.conpouitem[data-voucherid='" + voucherId + "']").remove();
                        if ($("#AvailableCashVoucher_List div.conpouitem").length == 0) {
                            $("#AvailableCashVoucherPanel").hide();
                        }
                        $("#HaveCashVoucherPanel").show();
                        $(renderVoucherList(new Array(voucher), true, true, true)).appendTo("#HaveCashVoucher_List").fadeIn("slow", function () {
                            //绑定点击选中事件
                            bindVoucherSelectEvent();
                            //是否需要重新计算最优优惠券？？？触发过手动选择就不再计算，否则计算最优方案
                            if (!isBestUsePlanChangeByPerson) {
                                var currVoucherList = [];
                                $.each($("#HaveCashVoucher_List div.conpouitem[data-jsondata]"), function (index, item) {
                                    currVoucherList.push(JSON.parse(unescape($(this).attr("data-jsondata"))));
                                });
                                bestUsePlan(currVoucherList, []);
                            }
                        });
                        $("#openconpoulist").text("更多可用现金券 >").show();
                    });
                }
            });
        }, 1500));
    }

    //绑定已领优惠券选择取消事件
    //根据用户所选优惠券重新计算订单价格
    function bindVoucherSelectEvent() {
        //本次可用选中状态
        $("#HaveCashVoucher_List div.conpouitem").unbind().click(function () {
            //人工修改了最优方案
            isBestUsePlanChangeByPerson = true;

            var voucher = JSON.parse(unescape($(this).attr("data-jsondata")));
            var isSelected = $(this).hasClass('active');
            if (isSelected) {
                //取消选中，同时取消页面展示
                $(this).removeClass('active').find(".couponbtn").text("立即使用").find("div.selstatus").remove();
                $("#usedVoucherList div.conpouitem[data-voucherid='" + voucher.Ycv_Id + "']").parent().remove();
            } else {
                //选中，先判断是否有同类型优惠券已被选中（总部、校区），如有则先取消同类型，然后选中此次用户所选
                $.each($("#usedVoucherList div.conpouitem"), function (index, item) {
                    var vid = Number($(item).attr("data-voucherid"));
                    var vt = Number($(item).attr("data-vouchertype"));
                    if (!isNaN(vt) && ((vt == 8 && vt == voucher.Ycv_VoucherType) || ((vt != 8 && voucher.Ycv_VoucherType != 8)))) {
                        //先移除
                        $("#HaveCashVoucher_List div.conpouitem[data-voucherid='" + vid + "']").triggerHandler("click");
                        return false;
                    }
                });

                $("#HaveCashVoucher_List div.conpouitem[data-voucherid='" + voucher.Ycv_Id + "']")
                    .addClass('active')
                    .append("<div class=\"selstatus\"><img src=\"/Images/Course/selstatus.png\"></div>")
                    .find(".couponbtn").text("暂不使用");
                $(renderVoucherList(new Array(voucher), true, false, false))
                    .append("<div class=\"selstatus\"><img src=\"/Images/Course/selstatus.png\"></div>")
                    .insertBefore("#usedVoucherList .getconputbox")
                    .wrap("<div class='orconpouitem'></div>");

                bindVoucherSelectEvent();
            }
            reCalcPrice();
        });
        $("#usedVoucherList div.conpouitem[data-voucherid]").unbind().click(function () {
            var vid = $(this).attr("data-voucherid");
            $("#HaveCashVoucher_List div.conpouitem[data-voucherid='" + vid + "']").triggerHandler("click");
        });
    }
    //重新计算当前订单价格
    function reCalcPrice() {
        var postCouponPrice = courseInfo.CoursePrice;
        $("#selectedSchoolVoucherAmount").text("-￥0.00");
        $("#selectedBaseVoucherAmount").text("-￥0.00");
        $("#needPayAmount").text("￥" + postCouponPrice.toFixed(2));
        $.each($("#usedVoucherList div.conpouitem"), function (index, item) {
            var voucherType = Number($(item).attr("data-vouchertype"));
            var voucherAmount = Number($(item).attr("data-voucheramount"));

            if (voucherType == 8) {
                $("#selectedSchoolVoucherAmount").text("-￥" + voucherAmount.toFixed(2));
            } else {
                $("#selectedBaseVoucherAmount").text("-￥" + voucherAmount.toFixed(2));
            }
            postCouponPrice -= voucherAmount;
        });
        postCouponPrice = postCouponPrice > 0 ? postCouponPrice : 0;
        $("#needPayAmount").text("￥" + postCouponPrice.toFixed(2));
        $("#needPayAmount_modelTip").text("￥" + postCouponPrice.toFixed(2));
    }


    //闭关右侧优惠券
    $('#closecoupon').click(function () {
        $('#conpoupage').stop(true, false).animate({
            'right': '-393px'
        })
    })
    //打开右侧优惠券
    $('#openconpoulist').click(function () {
        $('#conpoupage').stop(true, false).animate({
            'right': '0'
        })
    })
});
