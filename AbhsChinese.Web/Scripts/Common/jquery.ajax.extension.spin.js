$.ajaxSetup({
    //timeout: 10000, //超时时间设置，单位毫秒
    complete: function () {
        try {
            closeShadow();
        } catch (e) {

        }
    },
    beforeSend: function () {
        try {
            openShadow(6000);//6s后还没返回自动关闭
        } catch (e) {
        }
    },
});