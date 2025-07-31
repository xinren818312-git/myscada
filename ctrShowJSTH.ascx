<%@ Control Language="C#" AutoEventWireup="false" CodeFile="ctrShowJSTH.ascx.cs" Inherits="ctrShowJSTH" %>


<%@ Register Src="ctrShowJSLX.ascx" TagName="ctrShowJSLX" TagPrefix="uc1" %>

<table style="width: 1064px; height: 25px">
    <tr>
        <td style="width: 53px; height: 1px">
            <strong><span style="font-family: Verdana">胶水桶号：</span></strong></td>
        <td style="width: 95px; height: 1px">
            <asp:Label ID="LabJSTH" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Verdana"
                Text="Label" ForeColor="Red"></asp:Label><strong><span style="font-family: Verdana"> </span></strong>
        </td>
        <td style="width: 49px; height: 1px">
            反应釜：</td>
        <td style="width: 95px; height: 1px">
            <asp:Label ID="LabFYF" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Verdana"
                ForeColor="Red" Text="Label"></asp:Label></td>
        <td style="font-weight: bold; width: 91px; font-family: Verdana; height: 1px">
            <span>创建时间:</span></td>
        <td style="width: 89px; height: 1px">
            <asp:Label ID="LabCJSJ" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Verdana"
                Text="Label" ForeColor="Red"></asp:Label></td>
    </tr>
</table>
<asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Verdana"
    ForeColor="OliveDrab" Text="拣配数据："></asp:Label><br />
<asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
<br />
<asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Verdana"
    ForeColor="OliveDrab" Text="操作流程数据："></asp:Label><br />
<asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
<br />
<br />
<br />
<hr style="color: blue" />
&nbsp;

