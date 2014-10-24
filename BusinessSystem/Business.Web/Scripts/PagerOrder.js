$.fn.extend({
    //pageContainer:分页控件的父容器Id
    //pageSize:每页展现的数据量
    //pageIndex:当前页
    //ajaxParas:异部请求的url以及参数，如：{ action: { url: "/api/CustPromote/GetCustomPromList", data: dataParas } }
    //tbBodyList:展示数据的body的Id
    //tbodyTemplate:展示body数据的js模板Id
    //check_page_all:所有查询结果全选checkBox的Id
    //check_all:当页查询结果全选checkBox的Id
    //divMsg:查无数据的容器Id
    //callback:回掉方法 一个接收参数返回查询结果集
    JPager: function (pageContainer, pageSize, pageIndex, ajaxParas, tbBodyList, tbodyTemplate, check_page_all, check_all, divMsg, callback) {
        if (callback != undefined) {
            $("#" + pageContainer).data("callb", callback);
        }
        callback = $("#" + pageContainer).data("callb");
        if (ajaxParas && pageIndex > 0 && pageSize > 0) {
            //第一页
            var firstPage = function () {
                $("#" + pageContainer).JPager(pageContainer, pageSize, 1, ajaxParas, tbBodyList, tbodyTemplate);
            };
            //最后一页
            var lastPage = function () {
                var totalPage = parseInt($("#totalPage").text());
                $("#" + pageContainer).JPager(pageContainer, pageSize, totalPage, ajaxParas, tbBodyList, tbodyTemplate, check_page_all, check_all, divMsg);
            };
            //下一页
            var nextPage = function () {
                $("#" + pageContainer).JPager(pageContainer, pageSize, pageIndex + 1, ajaxParas, tbBodyList, tbodyTemplate, check_page_all, check_all, divMsg);

            };
            //上一页
            var prePage = function () {
                $("#" + pageContainer).JPager(pageContainer, pageSize, pageIndex - 1, ajaxParas, tbBodyList, tbodyTemplate, check_page_all, check_all, divMsg);
            };

            //跳转目标页面
            var pageGo = function () {
                var targetPage = parseInt($("#targetPage").val());
                var totalPage = parseInt($("#totalPage").text());

                if (targetPage) {
                    if (targetPage > totalPage) {
                        $("#" + pageContainer).JPager(pageContainer, pageSize, totalPage, ajaxParas, tbBodyList, tbodyTemplate, check_page_all, check_all, divMsg);
                    }
                    else if (targetPage < 1) {
                        $("#" + pageContainer).JPager(pageContainer, pageSize, 1, ajaxParas, tbBodyList, tbodyTemplate, check_page_all, check_all, divMsg);
                    }
                    else {
                        $("#" + pageContainer).JPager(pageContainer, pageSize, targetPage, ajaxParas, tbBodyList, tbodyTemplate, check_page_all, check_all, divMsg);
                    }
                }

                checkAll(check_all);
            };

            //检查跳转目标页面是否是数字
            var checkGoNumber = function () {
                if (!Number(this.value)) {
                    this.value = "";
                }
                else {
                    this.value = Number(this.value);
                }
            };

            //分页时，根据页面，实现分页控件上按钮的状态（点击或者不能点击）
            var changeState = function (totalPage) {
                if (pageIndex == 1) {
                    $("#prePage").removeAttr("href");
                    $("#firstPage").removeAttr("href");
                }
                else {
                    $("#firstPage").bind("click", firstPage).attr("class", "number");
                    $("#prePage").bind("click", prePage).attr("class", "number");
                }

                if (pageIndex == totalPage) {
                    $("#lastPage").removeAttr("href");
                    $("#nextPage").removeAttr("href");
                }
                else {
                    $("#lastPage").bind("click", lastPage).attr("class", "number");
                    $("#nextPage").bind("click", nextPage).attr("class", "number");
                }
            };

            //无数据时显示 查无数据 提示
            var noDataShow = function (recordCount) {
                var noDataHtml = "<div class=\"system_ts\">"
                                               + "<div class=\"system_ts_center\">"
                                                   + "<div class=\"system_search\">查无数据</div>"
                                                       + "</div>"
                                               + "<div class=\"system_ts_bottom\"></div>"
                                           + "</div>";

                if (divMsg != null && divMsg != "") {
                    if (recordCount > 0) {
                        $("#" + divMsg).empty();
                    }
                    else {
                        $("#" + divMsg).html(noDataHtml);
                    }
                }
            }

            //当页全选和所有结果全选互斥，实现页面有数据或者无数据时checkBox的显示或者隐藏
            var checkStatus = function (recordCount) {
                if (check_all != null && check_all != "") {
                    $("#" + check_all).click(function () {
                        $("#" + check_page_all).attr("checked", false);
                        checkAll(check_all);
                    });
                }

                if (check_page_all != null && check_page_all != "") {
                    $("#" + check_page_all).click(function () {
                        $("#" + check_all).attr("checked", false);
                        checkAll(check_page_all);
                    });
                }

                if (recordCount > 0) {
                    if (check_all != null && check_all != "") {
                        $("#" + check_all).attr("display", "");
                    }
                    if (check_page_all != null && check_page_all != "") {
                        $("#" + check_page_all).attr("display", "");
                    }

                } else {
                    if (check_all != null && check_all != "") {
                        $("#" + check_all).attr("display", "none");
                    }
                    if (check_page_all != null && check_page_all != "") {
                        $("#" + check_page_all).attr("display", "none");
                    }
                }
            };

            //实现表格中的checkBox全选功能
            var checkAll = function (checkboxId) {
                if (checkboxId != null && checkboxId != "") {
                    if ($("#" + checkboxId).attr("checked") == true) {
                        $("#" + tbBodyList).find("input[type='checkbox']").each(function () { $(this).attr("checked", true); });
                    }
                    else {
                        $("#" + tbBodyList).find("input[type='checkbox']").each(function () { $(this).attr("checked", false); });
                    }
                }
            }

            //初始化加载后，页面元素的加载
            var initPager = function (data) {
                $("#page").empty();

                if ($.isArray(data.ReturnData) && data.RecordCount > 0) {
                    if (data.RecordCount > pageSize) {
                        $("#page").append("<a style='font-weight: bold;' href='#' id='firstPage'>第一页</a>"
                            + " <a id='prePage' href='#'>«上一页</a>"
                            + " <a id='nextPage' href='#'>下一页»</a>"
                            + " <a style='font-weight: bold;' href='#' id='lastPage'>最后页</a>"
                            + " 共<span id='totalCount'></span>条信息"
                            + " <span id='currentPage'></span>/<span id='totalPage'></span>页"
                            + " <input type='text' style='Width:40px' id='targetPage'></input>  "
                            + " <input type='button' value='跳转' id='pageGo'></input>");

                        var totalPage = Math.ceil(data.RecordCount / pageSize);

                        $("#totalPage").html(totalPage);
                        $("#totalCount").html(data.RecordCount);
                        $("#currentPage").html(pageIndex);
                        $("#targetPage").attr("value", pageIndex);
                        $("#targetPage").bind("blur", checkGoNumber);
                        $("#pageGo").bind("click", pageGo);

                        changeState(totalPage);
                    }

                    //实现表格中的checkBox全选功能
                    checkAll(check_all);
                }

                checkStatus(data.RecordCount);
                //noDataShow(data.RecordCount);
            }

            //异步加载数据
            if (ajaxParas.action) {
                if (ajaxParas.action.url && ajaxParas.action.data) {

                    var para = ajaxParas.action.data.substr(0, ajaxParas.action.data.lastIndexOf("}")) + ',"PageIndex":' + pageIndex + ',"PageSize":' + pageSize + "}";

                    var colspannum = $("#" + tbBodyList).parent().find("th").length;

                    $("#" + tbBodyList).html("<tr><td colspan='" + colspannum + "' height='20px' ><div style='text-align:center;width:100%;' > <img src=\"../../images/loading01.gif\" align=\"absmiddle\" /> 数据加载中...</div></td></tr>");

                    $("#" + pageContainer).empty();

                    $("#" + divMsg).empty();

                    $("#toolbar").hide();

                    $.post(ajaxParas.action.url, eval('(' + para + ')'), function (data) {

                        if (data != null && data.Status.Code == 200) {

                            data.pageIndex = pageIndex;

                            var promList = eval(data.ReturnData);

                            $("#" + tbBodyList).empty();

                            $("#" + tbodyTemplate).tmpl(promList).appendTo("#" + tbBodyList);

                            initPager(data);

                            if (data.ReturnData.length > 0) {
                                $("#toolbar").show();
                            } else {
                                $("#" + tbBodyList).html("<tr><td colspan='" + colspannum + "' height='20px' ><div style='text-align:center;width:100%;' >查无数据</td></tr>");
                            }
                        }
                        else {
                            alert(data.Status.Description)
                            $("#" + tbBodyList).html("<tr><td colspan='" + colspannum + "' height='20px' ><div style='text-align:center;width:100%;' >" + data.Status.Description + "</td></tr>");
                        }

                        if (callback != undefined) {
                            callback(data);
                        }

                    }, "json");
                }
            }
        }
    }
});

//页面表格排序方法
//imgIdStr：排序所有需要排序栏位图标的id,使用“,”隔开，如：change_Date,change_bal
//query:页面查询函数
//hf_orderValue：页面存放排序字段（必须与数据库字段名称一致）的隐藏控件的ID
//hf_orderDesc:页面存放排序方式（升序，降序）的隐藏控件的ID
function initOrder(imgIdStr, query, hf_orderValue, hf_orderDesc) {
    var src_down = "../../images/arrow_up2.gif";
    var src_up = "../../images/arrow_up.gif";

    var imgIdArray = new Array();

    imgIdArray = imgIdStr.split(",");

    $.each(imgIdArray, function (index, imgId) {

        $("#" + imgId).css("cursor", "pointer");

        var imgClick = function () {
            var imgSrc = $("#" + imgId).attr("src");
            if (imgSrc.indexOf("arrow_up2") > 0) {
                $("#" + imgId).attr('src', src_up);
                $("#" + hf_orderDesc).attr("value", "asc");
                $("#" + hf_orderValue).attr("value", imgId);
            }
            else {
                $("#" + imgId).attr('src', src_down);
                $("#" + hf_orderDesc).attr("value", "desc");
                $("#" + hf_orderValue).attr("value", imgId);
            }

            query();
        };

        $("#" + imgId).bind("click", imgClick);
    });
}