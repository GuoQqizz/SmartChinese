$(function () {
    $(".header .navlist a").removeClass('active');

    if (document.body.clientWidth < 1280) {
        document.body.style.zoom = 0.8;
        $('.contentright3 .contrbox').addClass('noposi');
    }

    $('.contentleft').height($('.contentright').height());
    $('.loginpage').height(document.documentElement.clientHeight);
    $(window).resize(myfunction);

    function myfunction() {
        $('.contentleft').height($('.contentright').height());
        if (document.body.clientWidth < 1280) {
            document.body.style.zoom = 0.8;
            $('.contentright3 .contrbox').addClass('noposi');
        }
    }
});