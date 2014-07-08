$(function() {
    $('.mainbanner').each(function() {
        var $_root = $(this);
        var $window_b = $_root.find('.mainbanner_window');
        var $list = $_root.find('.mainbanner_list');
        var $items = $list.children();
        var $window_ul = $window_b.find('#slideContainer');
        var count = $items.length;
        var item_size = 2000;
        var dur_ms = 1000;
        var autoplay_interval = 8000;
        var cur_idx = 0;
        var fix_idx = function(_idx) {
            if (_idx < 0)
                return
            (count - 1);
            if (_idx >= count)
                return 0;
            return _idx;
        }

        var goto = function(_idx) {
            var idx = fix_idx(_idx);
            $items.eq(idx).addClass('active').siblings().removeClass('active');
            if (cur_idx != idx) {
                var offset_x = - idx * item_size;
                $window_ul.stop().animate({ 'left': offset_x }, dur_ms);
                cur_idx = idx;
            }
        }

        $items.each(function(index, element) {
            var $cur_item = $(this);
            var $cur_a = $cur_item.find('a');
            $cur_a.data('index', index);
            $cur_a.click(function() {
                var index = $(this).data('index');
                goto(index);
                return false;
            });
        });

        var autoplay_flag = true;

        window.setInterval(function() {
            if (autoplay_flag) {
                goto(cur_idx + 1);
            }
        }, autoplay_interval);

        $_root.hover(function() {
            autoplay_flag = false;
        }, function() {
            autoplay_flag = true;
        });

        goto(0);
    });

    $("#btn_login").click(function () {
        var userName = $("input[name=userName]").val().trim();
        var password = $("input[name=password]").val().trim();
        var remember = false;
        if (userName != "" && password != "") {
            $.ajax({
                url: "/Handler/Login",
                type: "post",
                datatype: "json",
                data: { userName: userName, password: password, remember: remember },
                success: function (response) {
                    if (response.Code == 0) {
                        window.location = "/Manager/Index";
                    } else {
                        alert("用户名或者密码错误");
                    }
                },
                error: function () {
                    alert("用户名或者密码错误");
                }
            });
        }
    });
});