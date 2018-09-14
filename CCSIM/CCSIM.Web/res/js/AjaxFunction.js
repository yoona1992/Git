function ajaxFunction(url, data, type, async, callback) {
    $.ajax({
        url: url,
        data: data,
        type: type,
        async: async,
        beforeSend: function () {
            //超时跳转
            $.ajax({
                url: "/Home/IsTimeOut",
                type: "post",
                async: false,
                success: function (data) {
                    if (data == true) {
                        parent.location.href = "../Home/Index";
                        return false;
                    }
                }
            });
        },
        success: function (data) {
            callback(data);
        }
    });
}