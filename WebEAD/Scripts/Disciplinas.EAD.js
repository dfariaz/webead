$(function () {
    $('.left.demo.sidebar').first()
        .sidebar('attach events', '.attached.menu');
    $('.grid.resp').hide();
    $('#dialog').click(function () {
        $('.grid.resp').show('normal');
        scrollToAnchor('textareaM');
        $('#ptg_mensagem').focus();
    });
});

function scrollToAnchor(aid) {
    var aTag = $("a[name='" + aid + "']");
    $('html,body').animate({ scrollTop: aTag.offset().top }, 'slow');
}

