var user = {
    init: function () {
        user.registerEvent();
    },
    registerEvent: function () {
        $('.js-switch').off('change').on('change', function (e) {
            e.preventDefault();
            var cbx = $(this);
            var id = cbx.data('id');
            $.ajax({
                url: "/Admin/Post/changeDisplay",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (recData) {
                    var notify = $.notify(recData.rpMessage, {globalPosition: 'top-center',className: 'success'});
                },
                error: function () { alert('An error occured'); }
            });
        });
    }
}
user.init();