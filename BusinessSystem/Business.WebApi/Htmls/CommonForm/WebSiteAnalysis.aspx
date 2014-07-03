﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebSiteAnalysis.aspx.cs"
    Inherits="Business.WebApi.Htmls.CommonForm.WebSiteAnalysis" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/WebSiteStyle.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.7.1.js" type="text/javascript"></script>
    <script src="../../Scripts/PagerOrder.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.tmpl.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.tmplPlus.js" type="text/javascript"></script>
    <script type="text/javascript">
        var originalColor = "#FFFFFF";
        function Mouseover(obj) {
            var tds = obj.getElementsByTagName("td");
            for (var i = 0; i < tds.length; i++) {
                originalColor = tds[i].style.backgroundColor;
                tds[i].style.backgroundColor = "#F2F2F2";
            }
        }
        function Mouseout(obj) {
            var tds = obj.getElementsByTagName("td");
            for (var i = 0; i < tds.length; i++) {
                tds[i].style.backgroundColor = originalColor;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <!--查询数据-->
    <script type="text/javascript">
        function queryDetail() {
            var v_ip = $("#txt_ip").val().trim();
            var orderByValue = $("#orderValue").val().trim();
            var orderByDesc = $("#orderDesc").val().trim();
            var dataParas = '{ "VIp": "' + v_ip + '", "OrderByValue": "' + orderByValue + '", "OrderByDesc": "' + orderByDesc + '"}';
//            $.post("/api/WebSiteAnalysis/GetInfoListByIp", eval('(' + dataParas + ')'), function (data) {
//                if (data != null && data.Status.Code == 200) {
//                    var list = data.ReturnData;
//                    alert(list);
//                    $('#mytmple').tmpl(list[0]).appendTo('#visitor_list_tbody');
//                    $('#visitor_list_tmpl').tmpl(list).appendTo('#visitor_list_tbody');
//                } else {
//                    alert("获取发送短信数据失败!");
//                }

//            }, "json");
            //分页
            $("#pager").JPager("page", 10, 1, { action: { url: "/api/WebSiteAnalysis/GetInfoListByIp", data: dataParas} }, "visitor_list_tbody", "visitor_list_tmpl", "", "", "divMsg");
        }


        $(function () {
            //初始化查询
            queryDetail();

            //排序
            //initOrder("VTime", queryDetail, "orderValue", "orderDesc");

        });

    </script>
    <!--数据列表绑定模版-->
    <script id="mytmple" type="text/x-jquery-tmpl">
        <td>${VIp}</td>
        <td>${ProductName}</td>
        <td>${VCountry}</td>
        <td>${VTime}</td>
    </script>
    <script id="visitor_list_tmpl" type="text/x-jquery-tmpl">
    <tr onmouseover="Mouseover(this)" onmouseout="Mouseout(this)" style="white-space: nowrap;">
        <td style="text-align: center">${VIp}</td>
        <td style="text-align: center">${ProductName}</td>
        <td style="text-align: center">${VCountry}</td>
        <td style="text-align: center">${VTime}</td>
    </tr>
    </script>
    <div style="width: 100%">
        <input type="hidden" id="orderDesc" value="desc" />
        <input type="hidden" id="orderValue" value="queryDetail" />
        <div class="margin_top">
        </div>
        <div>
            <div style="text-align:center;">
                <h1>
                    您现在进入的是<span style="color: Red;">英语</span>客户群体市场分析页面</h1>
            </div>
            <table id="tb_search" style="text-align: center; width: 100%;" cellpadding="0" cellspacing="1"
                class="blove2">
                <tr>
                    <td style="width: 15%" class="blove">国际市场选择
                    </td>
                    <td style="width: 35%; text-align: left" class="whiteBG" colspan="3">
                        <select id="sl_type">
                        </select>
                    </td>
                    <td style="width: 15%" class="blove">
                        查询采购商来看产品回数
                    </td>
                    <td style="width: 35%; text-align: left" class="whiteBG" colspan="3">
                        <input type="text" id="txt_ip" /><span style="color: Red">(*拷贝下面左边IP放入查询栏)</span>
                    </td>
                </tr>
            </table>
            <div class="btndiv">
                <input type="button" value="查询" onclick="queryDetail();" id="prom_query"
                    class="button" />
            </div>
            <div class="margin_top">
            </div>
            <table class="gvOne" cellspacing="1" cellpadding="0" id="tb_nation_debt_detail" style="width: 100%;">
                <tr>
                    <th scope="col">
                        <span>采购商所在位置（IP）</span>
                    </th>
                    <th scope="col">
                        <span>采购商所看产品</span>
                    </th>
                    <th scope="col">
                        <span>采购商所在位置地图显示</span>
                    </th>
                    <th scope="col">
                        <span>浏览时间（北京时间）</span><img id="VTime" src="../../images/arrow_up2.gif" />
                    </th>
                </tr>
                <tbody id="visitor_list_tbody">
                </tbody>
            </table>
            <div class="margin_top">
            </div>
        </div>
        <div class="margin_top">
        </div>
        <div id="page" style="text-align: right">
        </div>
        <div id="divMsg">
        </div>
    </div>
    </form>
</body>
</html>
