//下面2行代码是为了设置，菜单的默认选中效果
$(function () {
    let $secondMenu = $('a[href="/Question/ListApprovedQuestions"]');
    let $li = $secondMenu.parent().addClass('active');
    let $a = $li.parent().prev();
    if (!$a.parent().hasClass('active')) {
        $a.click();
    }
});