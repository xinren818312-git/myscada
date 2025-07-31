<%@ Control Language="C#" AutoEventWireup="false" CodeFile="ctrShowJSLX.ascx.cs" Inherits="ctrShowJSLX" %>
<asp:GridView ID="dgv" runat="server" AutoGenerateColumns="False" BorderColor="#404040"
    BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="12pt" Height="60px"
    Width="1600px">
    <Columns>        
        <asp:BoundField DataField="tg_posnr" HeaderText="序号" >
            <HeaderStyle Width="50px" />
        </asp:BoundField>
        <asp:BoundField DataField="tg_mara_matnr" HeaderText="物料">
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <HeaderStyle Width="150px" />
        </asp:BoundField>        
        <asp:BoundField DataField="tg_menge" HeaderText="理论拣配数据" >
            <HeaderStyle Width="100px" />
            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
        </asp:BoundField>
        <asp:BoundField DataField="tg_tolerance" HeaderText="公差">
            <HeaderStyle Width="50px" />
            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
        </asp:BoundField>        
        <asp:BoundField DataField="tg_actual_menge" HeaderText="实际拣配数量">
            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
            <HeaderStyle Width="100px" />
            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
        </asp:BoundField>
        <asp:BoundField DataField="tg_qrcode_content" HeaderText="混胶条码" >
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <HeaderStyle Width="100px" />
        </asp:BoundField>    
        <asp:BoundField DataField="tg_repertory_material_info" HeaderText="库存条码">
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <HeaderStyle Width="100px" />
        </asp:BoundField>
        <asp:BoundField DataField="tg_date" HeaderText="拣配时间">
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <HeaderStyle Width="150px" />
        </asp:BoundField>
        <asp:BoundField DataField="tg_user_id" HeaderText="拣配用户">
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <HeaderStyle Width="100px" />
        </asp:BoundField>
    </Columns>
    <HeaderStyle BackColor="Teal" ForeColor="White" />
    <AlternatingRowStyle BackColor="#E0E0E0" />
</asp:GridView>
<br />
