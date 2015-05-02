$(function () {

    $("#meldingsdatum").datepicker({
        inline: true,
        dateFormat: 'yy-mm-dd'
    });

    $("#alarmdatum").datepicker({
        inline: true,
        dateFormat: 'yy-mm-dd'
    });

    $("#dialog-link, #icons li").hover(
        function () {
            $(this).addClass("ui-state-hover");
        },
        function () {
            $(this).removeClass("ui-state-hover");
        }
    );
});
