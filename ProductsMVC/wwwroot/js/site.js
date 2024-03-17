$(function () {
    var PlaceHolderElement = $('#PlaceHolderHere');

    $('button[data-toggle="ajax-modal"], button[data-toggle="ajax-modal-edit"]').click(function (event) {
        var url = $(this).data('url');
        var decodedUrl = decodeURIComponent(url);
        $.get(decodedUrl).done(function (data) {
            PlaceHolderElement.html(data);
            PlaceHolderElement.find('.modal').modal('show');
        });
    });

    PlaceHolderElement.on('click', 'button[data-save="modal"]', function (event) {
        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var url = /Products/ + actionUrl;
        var sendData = form.serialize();

        $(this).prop('disabled', true);

        $.post(url, sendData).done(function (data) {
            PlaceHolderElement.find('.modal').modal('hide');
        });
    });

    PlaceHolderElement.on('click', '#saveEditButton', function (event) {
        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var sendData = form.serialize();

        $(this).prop('disabled', true);

        $.post(actionUrl, sendData).done(function (data) {
            PlaceHolderElement.find('.modal').modal('hide');
        });
    });

    PlaceHolderElement.on('click', '[data-dismiss="modal"]', function (event) {
        PlaceHolderElement.find('.modal').modal('hide');
    });
});
