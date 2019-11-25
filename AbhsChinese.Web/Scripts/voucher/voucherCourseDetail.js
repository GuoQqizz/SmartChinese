$(document).ready(function () {

    var studentId = $("#studentId").val();
    var courseInfo = JSON.parse($("#courseInfo").val());

    function intiVoucherList() {
        if (courseInfo && courseInfo.CourseId > 0) {
            var requestData = { OrderAmountLimit: courseInfo.CoursePrice, Grade: courseInfo.Grade, CourseType: courseInfo.CourseType, CourseId: courseInfo.CourseId };
            getAllAvailableCashVoucher(requestData, function (availableList, ownedList, allVoucherList) {
                $("#AvailableCashVoucher_List").empty();
                $("#HaveCashVoucher_List").empty();
                if (availableList.length > 0) {
                    $("#AvailableCashVoucher_List").append(renderVoucherList(availableList, false, true, false));
                    bindTakeCashVoucherEvent();
                    $("#AvailableCashVoucherPanel").show();
                }
                if (ownedList.length > 0) {
                    $("#HaveCashVoucher_List").append(renderVoucherList(ownedList, true, false, true));
                    $("#HaveCashVoucherPanel").show();
                }

                //券后价逻辑
                showVoucherPrice(allVoucherList);
            });
        }
    }

    intiVoucherList();

    //课程介绍部分的优惠券和券后价逻辑
    function showVoucherPrice(VoucherList) {
        if (VoucherList.length == 0) {
            $("#voucherPart").hide();
            $("#courseWaitPayMoney").html("&nbsp;&nbsp;&nbsp;￥0<img src=\"/Images/Course/zh.png\" />").hide();
        } else {
            var baseVoucher, schoolVoucher;
            $.each(VoucherList, function (index, item) {
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
            var postCouponPrice = courseInfo.CoursePrice;
            if (baseVoucher != undefined && baseVoucher.Ycv_Amount > 0) {
                postCouponPrice -= baseVoucher.Ycv_Amount;
                $("#baseVoucherPart").html("<label><em>总部券￥" + baseVoucher.Ycv_Amount.toFixed(0) + "</em></label>").show();
            }
            if (schoolVoucher != undefined && schoolVoucher.Ycv_Amount > 0) {
                postCouponPrice -= schoolVoucher.Ycv_Amount;
                $("#schoolVoucherPart").html("<label><em>校区券￥" + schoolVoucher.Ycv_Amount.toFixed(0) + "</em></label>").show();
            }
            if (baseVoucher != undefined || schoolVoucher != undefined) {
                $("#voucherPart").show();
                postCouponPrice = postCouponPrice > 0 ? postCouponPrice : 0;
                $("#courseWaitPayMoney").html("&nbsp;&nbsp;&nbsp;￥" + postCouponPrice.toFixed(0) + "<img src=\"/Images/Course/zh.png\" />").show();
            }
        }
    }

    //绑定领取优惠券事件
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
                        $(renderVoucherList(new Array(voucher), true, false, true)).appendTo("#HaveCashVoucher_List").fadeIn("slow", function () {
                        });
                    });
                }
            });
        }, 1500));
    }


    //关闭右侧优惠券
    $('#closecoupon').click(function () {
        $('#conpoupage').stop(true, false).animate({
            'right': '-393px'
        })
    })
    //打开右侧优惠券
    $('.conpouboxconfig span').click(function () {
        $('#conpoupage').stop(true, false).animate({
            'right': '0'
        })
    })
});
