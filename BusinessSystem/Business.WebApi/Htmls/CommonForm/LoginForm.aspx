<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginForm.aspx.cs" Inherits="Business.WebApi.Htmls.LoginForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户登录页</title>
    <link href="../../Styles/skin2.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function LoginClick() {
            var username = $("#username").val().trim();
            var password = $("#password").val().trim();
            var valiad = $("#txtValid").val().trim();
            if (username == "") {
                alert("账号不能为空");
                $("#username").focus();
                return false;
            } else if (password == "") {
                alert("密码不能为空");
                $("#password").focus();
                return false;
            } else if (valiad == "") {
                alert("验证码不能为空");
                return false;
            }
            $.ajax({
                type: "post",
                contentType: "application/json",
                url: "/api/Login/CheckLogin",
                data: "{ username: '" + username + "', userpwd: '" + password + "', valiad:'" + valiad + "' }",
                dataType: "json",
                success: function (result) {
                    window.location.href = 'WebSiteAnalysis.aspx';
                    if (data != null && data.Status.Code == 200) {
                        alert("success!");
                        
                    } else {
                        alert("账号不存在或者密码错误或验证码错误");
                        return false;
                    }
                },
                error: function () {
                    alert("异常");
                }
            });
        }

        //***user operate***
        $(function () {
            $("body").keydown(function (e) {
                var theEvent = window.event || e;
                var code = theEvent.keyCode || theEvent.which;
                if (code == "13") {//keyCode=13是回车键
                    LoginClick();
                }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width:100%; height:166; border-width:0px; margin-left:-12px; margin-right:-14px; margin-top:-15px; margin-bottom:-13px;" cellpadding="0" cellspacing="0">
            <tr>
                <td height="42" valign="top">
                    <table width="100%" height="42" border="0" cellpadding="0" cellspacing="0" class="login_top_bg">
                        <tr>
                            <td width="1%" height="21">
                                &nbsp;
                            </td>
                            <td height="42">
                                &nbsp;
                            </td>
                            <td width="17%">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <table width="100%" height="532" border="0" cellpadding="0" cellspacing="0" class="login_bg">
                        <tr>
                            <td width="49%" align="right">
                                <table width="91%" height="332" border="0" cellpadding="0" cellspacing="0" class="login_bg2">
                                    <tr>
                                        <td height="138" valign="top">
                                            <table width="89%" height="427" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td height="149">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="80" align="right" valign="top">
                                                        <img src="../../Images/logo.png" width="279" height="68">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="198" align="right" valign="top">
                                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td width="35%">
                                                                    &nbsp;
                                                                </td>
                                                                <td height="25" colspan="2" class="left_txt">
                                                                    <p>
                                                                        1- 区商家信息网门户站建立的首选方案...</p>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td height="25" colspan="2" class="left_txt">
                                                                    <p>
                                                                        2- 一站通式的整合方式，方便用户使用...</p>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td height="25" colspan="2" class="left_txt">
                                                                    <p>
                                                                        3- 强大的后台系统，管理内容易如反掌...</p>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td width="30%" height="40">
                                                                    <img src="../../Images/icon-demo.gif" width="16" height="16"><a href="#"
                                                                        target="_blank" class="left_txt3"> 使用说明</a>
                                                                </td>
                                                                <td width="35%">
                                                                    <img src="../../Images/icon-login-seaver.gif" width="16" height="16"><a href="#"
                                                                        target="_blank" class="left_txt3">在线客服</a>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="1%">
                                &nbsp;
                            </td>
                            <td width="50%" valign="bottom">
                                <table width="100%" height="59" border="0" align="center" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td width="4%">
                                            &nbsp;
                                        </td>
                                        <td width="96%" height="38">
                                           <h3><span class="login_txt_bt">登陆信息网后台管理</span></h3>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td height="21">
                                            <table cellspacing="0" cellpadding="0" width="100%" border="0" id="table211" height="328">
                                                <tr>
                                                    <td height="164" colspan="2" align="middle">
                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0" height="143" id="table212">
                                                            <tr>
                                                                <td width="13%" height="38" class="top_hui_text">
                                                                    <span class="login_txt">用户名：&nbsp;&nbsp; </span>
                                                                </td>
                                                                <td height="38" colspan="2" class="top_hui_text" style="text-align: left">
                                                                    <input type="text" id="username" value="admin" class="username" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="13%" height="35" class="top_hui_text">
                                                                    <span class="login_txt">密 码： &nbsp;&nbsp; </span>
                                                                </td>
                                                                <td height="35" colspan="2" class="top_hui_text" style="text-align: left">
                                                                    <input type="password" id="password" value="12" class="password" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="13%" height="35">
                                                                    <span class="login_txt">验证码：</span>
                                                                </td>
                                                                <td height="35" colspan="2" class="top_hui_text" style="text-align: left;">
                                                                    <input type="text" id="txtValid" value="aaa" style="width: 80px;" />
                                                                    <%--<img id="Img1" src="~/Admin/Image.aspx" width="56" runat="server" height="20" onclick= "javascript:this.src= 'Image.aspx?id= ' + Math.random(); " alt= "看不清楚？点击刷新验证码 " />--%> 
                                                                    <img src="YZM.ashx" style="width:56px; height:20px;" onclick="this.src='YZM.ashx?aaa='+new Date()" alt="看不清楚？点击刷新验证码 "" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td height="35">
                                                                    &nbsp;
                                                                </td>
                                                                <td width="20%" height="35">
                                                                    <input type="button" id="submit" value="登 录" style="background-image:url(../../Images/btn_submit.png); background-repeat:repeat-x;" onclick="LoginClick()" />&nbsp;
                                                                    <input type="button" id="btnCancel" value="取 消" style="background-image:url(../../Images/btn_submit.png); background-repeat:repeat-x;" onclick="javascript:window.close()" />
                                                                </td>
                                                                <td width="67%" style="text-align:left;">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <br>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="433" height="64" align="right" valign="bottom">
                                                        <img src="../../Images/login-wel.gif" width="242" height="64">
                                                    </td>
                                                    <td width="57" align="right" valign="bottom">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td height="20">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="login-buttom-bg">
                        <tr>
                            <td align="center">
                                <span class="login-buttom-txt">Copyright &copy; 2014- ***公司 版权所有!</span>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
