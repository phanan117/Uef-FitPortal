var user = {
    init: function () {
        user.registerEvent();
    },
    registerEvent: function () {
        $('.btn-active').off('click').on('click', function(e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/Post/changeDisplay",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    console.log(response);
                    if (reponse.status == true) {
                        btn.text('Show');
                    }
                    else {
                        btn.text('Hide');
                    }
                }
            });
        });
    }
}
user.init();