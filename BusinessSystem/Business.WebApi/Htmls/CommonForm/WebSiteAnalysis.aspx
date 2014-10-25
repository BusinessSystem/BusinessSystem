<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebSiteAnalysis.aspx.cs"
    Inherits="Business.WebApi.Htmls.CommonForm.WebSiteAnalysis" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>站体分析</title>
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
        //获取传过来的参数
        function getLanguage() {
            var urlInfo = window.location.href; //获取当前页面的url  
            var intLen = urlInfo.length; //获取url的长度  
            var offset = urlInfo.indexOf("="); //设置参数字符串开始的位置(因为这里只有1个参数)
            var strKeyValue = urlInfo.substr(offset + 1, intLen); //取出参数字符串 这里会获得类似“俄语”这样的字符串

            return decodeURI(strKeyValue);
        }

        //点击查询按钮查询
        function queryDetail() {
            //先给标题赋值
            $("#sp_type").html($("#sl_type option:selected").val());
            var v_ip = $("#txt_ip").val().trim();
            var orderByValue = $("#orderValue").val().trim();
            var orderByDesc = $("#orderDesc").val().trim();
            var language = $("#sl_type").val();
            if (language.trim().length == 0) {
                alert("必须选择一种国际类型!");
                $("#sl_type").focus();
                return;
            }
            var dataParas = '{ "VIp": "' + v_ip + '", "OrderByValue": "' + orderByValue + '", "OrderByDesc": "' + orderByDesc + '", "Language":"' + language + '"}';

            //分页
            $("#pager").JPager("page", 13, 1, { action: { url: "/api/VisitorRecord/GetInfoListByIp", data: dataParas} }, "visitor_list_tbody", "visitor_list_tmpl", "", "", "divMsg", function (data) {
                if (data != null && data.Status.Code == 200) {
                    GetCompanyInfo();
                }
            });
        }

        function GetCompanyInfo() {
            var language = $("#sl_type").val();
            if (language == "") {
                alert("加载语言失败");
            }
            var dataParas = '{"Language":"' + language + '"}';
            $.post("/api/WebSiteAnalysis/GetCompanyInfo", eval('(' + dataParas + ')'), function (data) {
                if (data != null && data.Status.Code == 200) {
                    var companyModel = data.ReturnData;
                    $("#peopleNum").html(companyModel["PeopleNum"]);
                    $("#companyName").html(companyModel["CompanyName"]);
                    $("#productCount").html(companyModel["ProductCount"]);
                }
            }, "json");
        }

        //初始化下拉框
        function init() {
            var languageType = getLanguage();
            var dataParas = '{}';
      
            $.get("/api/WebSiteAnalysis/GetLanguageTypeList", eval('(' + dataParas + ')'), function (data) {
                if (data != null && data.Status.Code == 200) {
                    var list = data.ReturnData;
                    var len = list.length;
                    var options = "<option value=\"\"></option>";
                    for (var i = 0; i < len; i++) {
                        if (languageType == list[i].toString()) {
                            var tmp = "<option value=\"" + list[i].toString() + "\" selected=\"selected\">" + list[i].toString() + "</option>";
                        } else {
                            var tmp = "<option value=\"" + list[i].toString() + "\">" + list[i].toString() + "</option>";
                        }
                        options += tmp;
                    }
                    $(options).prependTo("#sl_type");

                    //给标题赋值
                    $("#sp_type").html(getLanguage());
                    
                    queryDetail();
                } else {
                    alert("获取发送短信数据失败!");
                }

            }, "json");
        }

        $(function () {
            //初始化查询
            init();

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
        <td style="text-align: center"><a href='${ProductName}'>${ProductName}</a></td>
        <td style="text-align: center">${VCountry}</td>
        <td style="text-align: center">${VTime}</td>
    </tr>
    </script>
    <div class="panel">
        <div style="width: 100%">
            <input type="hidden" id="orderDesc" value="desc" />
            <input type="hidden" id="orderValue" value="queryDetail" />
            <div class="margin_top">
            </div>
            <div>
                <div style="text-align: center;">
                    <h1>
                        您现在进入的是<span style="color: Red;" id="sp_type"></span><span style="color: Red;" id="Span1">客户群体市场</span>分析页面</h1>
                </div>
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" class="blove2">
                    <tr>
                        <td style="width: 100%" class="blove">
                            共有<span id="peopleNum" style="color:Red;"></span>位客户访问的了<span id="companyName" style="color:Red;"></span>公司共<span
                                id="productCount" style="color:Red;"></span>次产品
                        </td>
                    </tr>
                </table>
                <div class="margin_top">
                </div>
                <table id="tb_search" style="text-align: center; width: 100%;" cellpadding="0" cellspacing="1"
                    class="blove2">
                    <tr>
                        <td style="width: 15%" class="blove">
                            国际市场选择
                        </td>
                        <td style="width: 35%; text-align: left" class="whiteBG" colspan="3">
                            <select id="sl_type" style="width: 160px;">
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
                    <input type="button" value="查询" onclick="queryDetail();" id="prom_query" class="button" />
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
                            <span>浏览时间（北京时间）</span><img id="VTime" src="/Images/arrow_up2.gif" />
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
    </div>
    </form>
</body>
</html>
