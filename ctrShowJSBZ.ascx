<%@ Control Language="C#" AutoEventWireup="false" CodeFile="ctrShowJSBZ.ascx.cs" Inherits="ctrShowJSBZ" %>
<asp:GridView ID="dgv" runat="server" AutoGenerateColumns="False" BorderColor="#404040"
    BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="12pt" Height="60px"
    Width="1600px">
    <Columns>        
        <asp:BoundField DataField="tg_title" HeaderText="步骤">
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <HeaderStyle Width="100px" />
        </asp:BoundField>
        <asp:BoundField DataField="tg_counts" HeaderText="拼接数据数目" >
            <HeaderStyle Width="100px" />
        </asp:BoundField>
        <asp:BoundField DataField="tg_done_flag" HeaderText="是否投料完成">
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <HeaderStyle Width="100px" />
        </asp:BoundField>        
        <asp:BoundField DataField="tg_maction" HeaderText="操作步骤描述" >
            <HeaderStyle Width="100px" />
            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
        </asp:BoundField>
        <asp:BoundField DataField="tg_qrcode" HeaderText="拼接数据内容">
            <HeaderStyle Width="100px" />
            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
        </asp:BoundField>        
        <asp:BoundField DataField="tg_serial_number" HeaderText="投料顺序">
            <HeaderStyle Width="100px" />
            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
        </asp:BoundField>
        <asp:BoundField DataField="tg_time" HeaderText="步骤执行时间" >
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <HeaderStyle Width="100px" />
        </asp:BoundField>    
    </Columns>
    <HeaderStyle BackColor="Teal" ForeColor="White" />
    <AlternatingRowStyle BackColor="#E0E0E0" />
</asp:GridView>
<br />
