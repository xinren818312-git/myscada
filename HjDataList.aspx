<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="HjDataList.aspx.cs" Inherits="_Default" %>

<%@ Register Src="ctrShowJSTH.ascx" TagName="ctrShowJSTH" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>扬州天启新材料股份有限公司</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
            <div style="text-align: left">
                <strong><em><span style="font-size: 24pt">&nbsp; &nbsp; &nbsp; &nbsp; 扬州天启新材料股份有限公司混胶查询</span></em></strong>
                <br />
                <hr />
                <table border="0" style="width: 1156px; height: 1px">
                    <tr>
                        <td style="width: 84px; height: 3px">
                            <strong>胶水桶号</strong></td>
                        <td style="width: 10px; height: 3px">
                            <asp:TextBox ID="txtJSTH1" runat="server" Width="130px"></asp:TextBox></td>
                        <td style="width: 14px; height: 3px">
                            <strong>到</strong></td>
                        <td style="width: 65px; height: 3px">
                            <asp:TextBox ID="txtJSTH2" runat="server" Width="130px"></asp:TextBox></td>
                        <td style="width: 264px; height: 3px">
                            <asp:Button ID="btnSearch" runat="server" CausesValidation="False" Font-Bold="True"
                                Font-Size="Larger" Height="35px" OnClick="ButRun_Click" Text="查询" Width="174px" /></td>
                    </tr>
                    <tr>
                        <td style="width: 84px; height: 6px">
                            <strong>创建日期</strong></td>
                        <td style="width: 10px; height: 6px">
                            <asp:TextBox ID="txtCJRQ1" runat="server" Width="130px"></asp:TextBox></td>
                        <td style="width: 14px; height: 6px">
                            <strong>到</strong></td>
                        <td style="width: 65px; height: 6px">
                            <asp:TextBox ID="txtCJRQ2" runat="server" Width="130px"></asp:TextBox></td>
                        <td colspan="1" style="height: 6px; width: 264px;">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <hr style="color: blue" />
            </div>
        </div>
        <br />
        <br />
        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
        <table style="width: 100%">
            <tr style="width: 100%">
                <td style="width: 100%; height: 40px">
                    <strong><em><span style="color: #0000ff">CopyRight: 生益科技 &nbsp; 信息管理部 &nbsp;&nbsp;
                        &nbsp;<br />
                    </span><span style="color: #0000ff">&nbsp; &nbsp; &nbsp; &nbsp;&nbsp; 版本:&nbsp; 2019-09-0001</span></em></strong>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
