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
                    var returnInfo = data;
                    if (returnInfo == true) {
                        parent.location.href = "../Home/Index";
                        return false;
                    }
                },
                complete: function (XHR, TS) {
                    XHR = null
                } //回收资源
            });
        },
        success: function (data) {
            var returnInfo = data;
            callback(returnInfo);
        },
        complete: function (XHR, TS) {
            XHR = null
        } //回收资源
    });
}