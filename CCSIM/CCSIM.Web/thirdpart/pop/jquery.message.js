(function () {
    var ua = navigator.userAgent.toLowerCase();

    var is = (ua.match(/\b(chrome|opera|safari|msie|firefox)\b/) || ['', 'mozilla'])[1];

    var r = '(?:' + is + '|version)[\\/: ]([\\d.]+)';

    var v = (ua.match(new RegExp(r)) || [])[1];

    if (jQuery.browser) return;
    jQuery.browser = {};
    jQuery.browser.is = is;
    jQuery.browser.ver = v;
    jQuery.browser[is] = true;
})();

(function (jQuery) {

    /*
    
     * jQuery Plugin - Messager
    
     * Author: corrie	Mail: corrie@sina.com	Homepage: www.corrie.net.cn
    
     * Copyright (c) 2008 corrie.net.cn
    
     * @license http://www.gnu.org/licenses/gpl.html [GNU General Public License]
    
     *
    
     * $Date: 2008-12-26 
    
     * $Vesion: 1.4
    
     @ how to use and example: Please Open demo.html
    
     */

    this.version = '@1.3';

    this.layer = { 'width': 200, 'height': 100 };

    this.title = '信息提示';

    this.time = 4000;

    this.anims = { 'type': 'slide', 'speed': 600 };
    this.timer1 = null;



    this.inits = function (title, text, remarks, sender, objectId) {

        if ($("#message" + objectId).is("div")) { return; }

        //循环所有message
        var allDiv = $('div');
        var zIndex = 1;
        $.each(allDiv, function (index, data) {
            if (data.id.indexOf('message') == 0) {
                if (zIndex <= parseInt($("#" + data.id).css('z-index'))) {

                    zIndex = parseInt($("#" + data.id).css('z-index'));
                }
            }
        });
        zIndex = zIndex + 1;

        var topHeight = document.documentElement.scrollTop + document.documentElement.clientHeight - this.layer.height - 2;
        //$(document.body).prepend('<div id="message" style="background:#fff;width:260px;border:1px solid #e0e0e0;font-size:12px;position: fixed;right:10px;bottom:10px;"><div style="border:1px solid #fff;border-bottom:none;width:100%;height:25px;font-size:12px;overflow:hidden;color:#1f336b;"><span id="message_close" style="float:right;padding:5px 0 5px 0;width:16px;line-height:auto;color:red;font-size:12px;font-weight:bold;text-align:center;cursor:pointer;overflow:hidden;">×</span><div style="line-height:32px;background:#f6f0f3;border-bottom:1px solid #e0e0e0;position:relative;font-size:12px;padding:0 0 0 10px;">'+title+'</div><div style="clear:both;"></div></div> <div style="padding-bottom:5px;border:1px solid #fff;border-top:none;width:100%;height:auto;font-size:12px;"><div id="message_content" style="">'+text+'</div></div></div>');
        $(document.body).prepend('<div id="message' + objectId + '" style="background:#fff;width:300px;border:1px solid #e0e0e0;font-size:12px;position: fixed;right:10px;bottom:10px;z-index:' + zIndex + ';"><div class="popHead" id="popHead">' +
                                                    '<a class="popClose" id="popClose' + objectId + '" title="关闭">关闭</a>' +
            '<h2 style="margin-bottom:0px">' + sender + '</h2>' +
            '</div>' +
            '<div class="popContent" id="popContent">' +
            '<dl>' +
                '<dt class="popTitle" id="popTitle">' + title + '</dt>' +
                '<dd class="popIntro" id="popIntro">' + text + '</dd>' +
                '<dd class="popIntro" id="popIntro">' + remarks + '</dd>' +
            '</dl>' +
            '<p class="popMore" id="popMore"><a id="reply' + objectId + '" href="#">回复 »  </a><a id="look' + objectId + '" href="#">查看 »</a></p>' +
            '</div></div>');


        $("#popClose" + objectId).click(function () {
            setTimeout(function () {
                $("#message" + objectId).remove();
            }, 1);

        });
        $("#message" + objectId).hover(function () {
            clearTimeout(timer1);
            timer1 = null;
        }, function () {
            //timer1 = setTimeout('this.close()', time);
            timer1 = setTimeout(function () {
                $("#message" + objectId).remove()
            }, time);
            //alert(timer1);
        });

    };

    this.show = function (title, text, remarks, sender, time, objectId) {

        //if($("#message").is("div")){ return; }

        //if(title==0 || !title)title = this.title;

        this.inits(title, text, remarks, sender, objectId);

        if (time >= 0) this.time = time;

        switch (this.anims.type) {

            case 'slide': $("#message" + objectId).slideDown(this.anims.speed); break;

            case 'fade': $("#message" + objectId).fadeIn(this.anims.speed); break;

            case 'show': $("#message" + objectId).show(this.anims.speed); break;

            default: $("#message" + objectId).slideDown(this.anims.speed); break;

        }

        if ($.browser.is == 'chrome') {

            //setTimeout(function () {

            //    $("#message" + objectId).remove();

            //    this.inits(title, text,remarks,sender, objectId);

            //    $("#message" + objectId).css("display", "block");

            //}, this.anims.speed - (this.anims.speed / 5));

        }

        //$("#message").slideDown('slow');

        this.rmmessage(this.time);

    };

    this.lays = function (width, height) {

        if ($("#message").is("div")) { return; }

        if (width != 0 && width) this.layer.width = width;

        if (height != 0 && height) this.layer.height = height;

    }

    this.anim = function (type, speed) {

        if ($("#message").is("div")) { return; }

        if (type != 0 && type) this.anims.type = type;

        if (speed != 0 && speed) {

            switch (speed) {

                case 'slow':; break;

                case 'fast': this.anims.speed = 200; break;

                case 'normal': this.anims.speed = 400; break;

                default:

                    this.anims.speed = speed;

            }

        }

    }

    this.rmmessage = function (time) {

        if (time > 0) {

            timer1 = setTimeout('this.close()', time);

            //setTimeout('$("#message").remove()', time+1000);

        }

    };
    this.close = function () {
        switch (this.anims.type) {
            case 'slide': $("#message").slideUp(this.anims.speed); break;
            case 'fade': $("#message").fadeOut(this.anims.speed); break;
            case 'show': $("#message").hide(this.anims.speed); break;
            default: $("#message").slideUp(this.anims.speed); break;
        };
        setTimeout('$("#message").remove();', this.anims.speed);
        this.original();
    }

    this.original = function () {

        this.layer = { 'width': 200, 'height': 100 };

        this.title = '信息提示';

        this.time = 4000;

        this.anims = { 'type': 'slide', 'speed': 600 };

    };

    jQuery.messager = this;

    return jQuery;

})(jQuery);

$(window).scroll(function () {
    var topHeight = document.documentElement.scrollTop + document.documentElement.clientHeight - this.layer.height - 2;
    $("#message").css("top", topHeight + "px");
});