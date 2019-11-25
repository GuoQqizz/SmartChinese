function showMessage(ajaxResponse) {
    let numOfIcon = 1;
    if (!ajaxResponse.State) {
        numOfIcon = 5;
    }
    layer.msg(ajaxResponse.ErrorMsg, { icon: numOfIcon });
}

function complete(response) {
    if (response.status === 500) {
        layer.msg('服务器异常,请稍后重试!', { icon: 5 });
        return false;
    }
    let res = response.responseJSON;
    $('#subjectId').val(res.Data);
    showMessage(res);
}

function redirect(response) {
    if (response.status === 500) {
        layer.msg('服务器异常,请稍后重试!', { icon: 5 });
        return false;
    }
    let res = response.responseJSON;
    window.location.href = res.Data;
}