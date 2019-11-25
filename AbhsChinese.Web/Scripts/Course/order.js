$(document).ready(function () {

    //订单支付倒计时
    var payTimeDeadLine = Number($("#orderInvalidTime").val())
    $.extend({
        payTimeCountdown: function () {
            var countdown = payTimeDeadLine - (new Date()).getTime();
            if (countdown < 0) {
                clearInterval(orderTimeIntervalHandler);
                $(".orderPayTimeCountdownTip_1").text("超时未支付，订单已关闭");
                $(".orderPayTimeCountdownTip_2").text("超时未支付，订单已关闭");
                $('.order-shadow').hide();
                $("#btnPayment").text("重新购买").unbind().click(function () {
                    location.href = $("#reBuyUrl").val();
                });
            } else {
                $(".orderPayTimeCountdownTip_1").html("请在 <span>" + moment(countdown).format("mm分ss秒") + "</span> 内完成支付");
                $(".orderPayTimeCountdownTip_2").html("请在 <span>" + moment(countdown).format("mm分ss秒") + "</span> 内完成支付");
            }
        }
    });
    var orderTimeIntervalHandler = setInterval("$.payTimeCountdown()", 1000);

    // 点击立即支付
    $("#btnPayment").click(throttle(function () {
        var status = $("#orderStatus").val();
        if ("1" == status || "2" == status) {
            var orderId = $("#orderId").val();
            var voucherIds = [];
            $.each($("#usedVoucherList div.conpouitem[data-voucherid]"), function (index, item) { voucherIds.push($(item).attr("data-voucherid")); });
            var paytype = $(".paytypeitem.active").attr("data-paytype");//微信
            $.ajax({
                url: "/OwnOrder/GetWechatPayQR",
                type: "POST",
                dataType: "json",
                data: { orderId: orderId, voucherIds: voucherIds, paytype: paytype },
                cache: false,
                success: function (result) {
                    if (result.State) {
                        if (result.Data.IsFreeOrder) {
                            //跳转到订单详情页
                            redirectToDetail();
                        } else {
                            var content = result.Data.QR;
                            if (content != undefined && content != null && content != "") {
                                $("#wechatPayQRImg").attr("src", "data:image/gif;base64," + content);
                                $('.paycodepop').show();
                            }
                        }
                    } else {
                        if (result.ErrorMsg != null && result.ErrorMsg != "") {
                            $("#paymentErrorTip").text(result.ErrorMsg);
                            $("#payErrorPanel").show();
                        }
                    }
                }
            });
        }
    }, 2000));

    //支付完成关闭按钮
    $('.paycodepop .closepaydepopimg').click(function () {
        orderquery(function (ispay) {
            if (ispay) {
                //跳转到订单详情页
                redirectToDetail();
            }
            $('.paycodepop').hide();
        });
    });
    //点击支付完成
    $("#btnOrderQuery").click(function () {
        orderquery(function (ispay) {
            if (ispay) {
                //支付成功
                //跳转到订单详情页
                redirectToDetail();
            } else {
                //未支付成功
                $('.paycodepop').hide();
                $('.payfailpage').show();
            }
        });
    });
    //支付失败关闭按钮
    $('.payfailpage .closepaydepopimg').click(function () {
        $('.payfailpage').hide();
    });
    $("#btnRePay").click(function () {
        $('.payfailpage').hide();
        $('.paycodepop').show();
    });

    function orderquery(callback) {
        var orderId = $("#orderId").val();
        var paytype = $(".paytypeitem.active").attr("data-paytype");//微信
        $.ajax({
            url: "/OwnOrder/OrderQuery",
            type: "POST",
            dataType: "json",
            data: { orderId: orderId, paytype: paytype },
            cache: false,
            success: function (result) {
                if ($.isFunction(callback)) {
                    callback(result.Data);
                }
            }
        });
    }

    function redirectToDetail() {
        var orderId = $("#orderId").val();
        location.href = "/OwnOrder/OrderDetail/" + orderId;
    }

    $("#btnCloseErrorTip").click(function () {
        $("#paymentErrorTip").text("");
        $("#payErrorPanel").hide();
    });
});