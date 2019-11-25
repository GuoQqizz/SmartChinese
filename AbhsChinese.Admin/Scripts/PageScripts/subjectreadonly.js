function submitForm(button) {
    let $form = $(button).parents('form');
    console.log(button.value);
    $form.find(':hidden[name="nextStatus"]').val(button.value);
    let url = $form.attr('action');
    let data = $form.serialize();
    $.ajax({
        type: "POST",
        url: url,
        data: data,
        success: function (reponse) {
            layer.msg(reponse.ErrorMsg, { icon: reponse.State ? 1 : 5 });
            if (reponse.State) {
                setTimeout(function () {
                    window.location.href = "/Question/ListQuestions";
                }, 2000);
            }
        }
    });
}

function reEdit(redirectUrl) {
    let routeData = redirectUrl.split('/');
    $.post('/Question/ReEdit', { subjectId: routeData[3] },
        function (data) {
            if (data && data.State === true) {
                window.location.href = redirectUrl;
            }
        });
}