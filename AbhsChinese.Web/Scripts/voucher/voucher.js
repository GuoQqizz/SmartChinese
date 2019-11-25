
//领取优惠券
//data：{ StudentId: 当前用户Id, CashVoucherId: 优惠券Id, GotType: 领取类型, GotReferId: 0 }
//callback：领取成功回调，回调参数为领取是否成功（true、false）
function takeStudentCashVoucher(data, callback) {
    var voucherId = $("#voucherPanel").attr("data-voucherid");
    $.ajax({
        url: "/OwnCashVoucher/TakeStudentCashVoucher",
        type: "POST",
        dataType: "json",
        data: data,
        cache: false,
        success: function (result) {
            if (result.State && $.isFunction(callback)) {
                callback(result.Data);
            }
        }
    });
}

//获取可用优惠券列表
//data：{ OrderAmountLimit: courseInfo.CoursePrice, Grade: courseInfo.Grade, CourseType: courseInfo.CourseType, CourseId: courseInfo.CourseId }
//callback：成功后的回调，回调参数arg1:未领取的优惠券列表；arg2:已领取的优惠券列表；arg3:所有优惠券
function getAllAvailableCashVoucher(data, callback) {
    $.ajax({
        url: "/OwnCashVoucher/GetAllCashVoucher",
        type: "POST",
        dataType: "json",
        data: data,
        cache: false,
        success: function (result) {
            if (result.State && $.isFunction(callback)) {
                var vouchers = result.Data;
                var availableList = [], ownedList = [];
                $.each(vouchers, function (index, item) {
                    formatVoucherDate(item)
                    if (item.IsAvailable == 1) {
                        ownedList.push(item);
                    } else {
                        availableList.push(item);
                    }
                });
                callback(availableList, ownedList, vouchers);
            }
        }
    });
}

//获取与订单绑定的优惠券列表
//data：{ OrderId: 订单Id }
//callback：成功后的回调，回调参数与订单绑定的优惠券列表
function getCashVoucherByOrderId(data, callback) {
    $.ajax({
        url: "/OwnCashVoucher/GetCashVoucherByOrderId",
        type: "POST",
        dataType: "json",
        data: data,
        cache: false,
        success: function (result) {
            if (result.State && $.isFunction(callback)) {
                var vouchers = result.Data;
                $.each(vouchers, function (index, item) {
                    item.IsAvailable = 1;
                    formatVoucherDate(item);
                });
                callback(vouchers);
            }
        }
    });
}

function formatVoucherDate(item) {
    item.Ycv_CreateTime = moment(item.Ycv_CreateTime).format("YYYY-MM-DD");
    if (item.IsAvailable == 1) {
        item.Ysv_TakenTime = moment(item.Ysv_TakenTime).format("YYYY-MM-DD");
        switch (item.Ycv_ExpireType) {
            case 1:
            case 2:
                item.Ysv_ExpireDate = moment(item.Ysv_ExpireDate).format("YYYY-MM-DD");
                item.Ycv_ExpireDate = item.Ysv_ExpireDate;
                break;
            case 3:
                item.Ycv_ExpireDate = "";
                item.Ysv_ExpireDate = "";
                break;
        }
    } else {
        item.Ysv_TakenTime = moment().format("YYYY-MM-DD");
        switch (item.Ycv_ExpireType) {
            case 1:
                item.Ycv_ExpireDate = moment(item.Ycv_ExpireDate).format("YYYY-MM-DD");
                item.Ysv_ExpireDate = item.Ycv_ExpireDate;
                break;
            case 2:
                item.Ycv_ExpireDate = moment(item.Ysv_TakenTime).add(item.Ycv_ExpireDayCount, 'days').format("YYYY-MM-DD");
                item.Ysv_ExpireDate = item.Ycv_ExpireDate;
                break;
            case 3:
                item.Ycv_ExpireDate = "";
                item.Ysv_ExpireDate = "";
                break;
        }
    }
}

//优惠券呈现
//vouchers：优惠券列表
//isReceived：是否已领取
//isShowReceivedBtn：是否显示领取、使用按钮
//isShowReceivedIcon：是否显示已领取图标
function renderVoucherList(vouchers, isReceived, isShowReceivedBtn, isShowReceivedIcon) {
    var content = "";
    $.each(vouchers, function (index, voucher) {
        var priceLimit = "";
        if (voucher.Ycv_OrderAmountLimit > 0) {
            priceLimit = "满￥" + voucher.Ycv_OrderAmountLimit + "可用";
        } else {
            priceLimit = "无限制";
        }

        var daterange = "";
        switch (voucher.Ycv_ExpireType) {
            case 1:
                daterange = voucher.Ycv_CreateTime + "-" + voucher.Ycv_ExpireDate;
                break;
            case 2:
                if (isReceived) {
                    daterange = voucher.Ysv_TakenTime + "-" + voucher.Ysv_ExpireDate;
                } else {
                    daterange = "领取后" + voucher.Ycv_ExpireDayCount + "天内有效";
                }
                break;
            case 3:
                daterange = "长期有效";
                break;
        }

        var specialCourseName = "";
        if (voucher.Ycv_CourseId == 0) {
            specialCourseName = "全部课程可用";
        } else {
            specialCourseName = "适用" + voucher.Ycs_Name;
        }
        var specialSchool = "";
        if (voucher.Bsl_SchoolName != null && voucher.Bsl_SchoolName != "") {
            specialSchool = "适用" + voucher.Bsl_SchoolName;
        } else {
            specialSchool = "适用全国校区";
        }

        if (voucher.Ycv_VoucherType == 8) {
            content += "<div class=\"conpouitem\" data-voucherid=\"" + voucher.Ycv_Id + "\" data-vouchertype=\"" + voucher.Ycv_VoucherType + "\" data-voucheramount=\"" + voucher.Ycv_Amount + "\" data-jsondata=\"" + escape(JSON.stringify(voucher)) + "\">";
            content += "    <div class=\"itembox\">";
            content += "        <div class=\"iteminfo\">";
            content += "            <div class=\"iteminfoleft fl\">";
            content += "                <div class=\"price\"><span>￥</span>" + voucher.Ycv_Amount + "</div>";
            content += "                <div class=\"price1\"><span>" + priceLimit + "</span></div>";
            if (isShowReceivedBtn) {
                content += "                <div class=\"couponbtnbox\"><span class=\"couponbtn\" data-voucherid=\"" + voucher.Ycv_Id + "\">" + (isReceived ? "立即使用" : "立即领取") + "</span></div>";
            }
            content += "            </div>";
            content += "            <div class=\"iteminforight\">";
            content += "                <div class=\"copupintitle line1\">" + voucher.Ycv_Name + "</div>";
            content += "                <div class=\"copupindate line1 daterange\">" + daterange + "</div>";
            content += "                <div class=\"copupindate line1\" title=\"" + specialCourseName + "\">" + specialCourseName + "</div>";
            content += "                <div class=\"copupinarea line1\">" + specialSchool + "</div>";
            content += "            </div>";
            content += "            <div class=\"status\">校区券</div>";
            content += "        </div>";
            if (isReceived && isShowReceivedIcon) {
                content += "        <div class=\"selconpouimg\"><img src=\"/Images/Course/salconput.png\" /></div>";
            }
            content += "    </div>";
            content += "</div>";
        } else {
            content += "<div class=\"conpouitem\" data-voucherid=\"" + voucher.Ycv_Id + "\" data-vouchertype=\"" + voucher.Ycv_VoucherType + "\" data-voucheramount=\"" + voucher.Ycv_Amount + "\" data-jsondata=\"" + escape(JSON.stringify(voucher)) + "\">";
            content += "    <div class=\"itembox itembox1\">";
            content += "        <div class=\"iteminfo\">";
            content += "            <div class=\"iteminfoleft fl\">";
            content += "                <div class=\"price pirce2\"><span>￥</span>" + voucher.Ycv_Amount + "</div>";
            content += "                <div class=\"price1\"><span>" + priceLimit + "</span></div>";
            if (isShowReceivedBtn) {
                content += "                <div class=\"couponbtnbox\"><span class=\"couponbtn couponbtn1\" data-voucherid=\"" + voucher.Ycv_Id + "\">" + (isReceived ? "立即使用" : "立即领取") + "</span></div>";
            }
            content += "            </div>";
            content += "            <div class=\"iteminforight\">";
            content += "                <div class=\"copupintitle line1\">" + voucher.Ycv_Name + "</div>";
            content += "                <div class=\"copupindate line1 daterange\">" + daterange + "</div>";
            content += "                <div class=\"copupindate line1\" title=\"" + specialCourseName + "\">" + specialCourseName + "</div>";
            content += "                <div class=\"copupinarea line1\">" + specialSchool + "</div>";
            content += "            </div>";
            content += "            <div class=\"status status1\">总部券</div>";
            content += "        </div>";
            if (isReceived && isShowReceivedIcon) {
                content += "        <div class=\"selconpouimg\"><img src=\"/Images/Course/selconpou.png\" /></div>";
            }
            content += "    </div>";
            content += "</div>";
        }
    });
    return content;
}