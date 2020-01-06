<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportExport.aspx.cs" Inherits="NewOrderingSystem.ReportExport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Report Export</title>
</head>
<body>
    <form id="form1" runat="server">   
    <asp:GridView ID="grdReportExcel" runat="server" AutoGenerateColumns="False" CellPadding="3"
        BorderStyle="None" BorderWidth="1px" Width="1027px" >        
         <AlternatingRowStyle Font-Size="8pt" BackColor="#D7EBFF" />
        <Columns>
            <asp:BoundField DataField="DocumentNo" HeaderText="Reference Number" />
            <asp:BoundField DataField="VendorName" HeaderText="Preferred Vendor" />
            <asp:BoundField DataField="VersionNo" HeaderText="Version No" />
            <asp:BoundField  HeaderText="Doc# Date" >
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField  HeaderText="PO No" >
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="MaterialCategoryID" HeaderText="Material Category ID" />
            <asp:BoundField DataField="SetGroup" HeaderText="Group" />
            <asp:BoundField DataField="DisplaySeq" HeaderText="Item" />            
            <asp:BoundField DataField="Meterial" HeaderText="Material" />
            <asp:BoundField DataField="SetName" HeaderText="Short Text" />
            <asp:BoundField DataField="NoOfEquipment" HeaderText="Quantity" />
            <asp:BoundField  HeaderText="Deliv# Date" >
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField> 
            <asp:BoundField DataField="PartNo" HeaderText="Component No" />
            <asp:BoundField DataField="PartName" HeaderText="Component Text" />
            <asp:BoundField DataField="QtyPerLift" HeaderText="ChildPer" />
            <asp:BoundField DataField="ParmUOM" HeaderText="UOM" />
            <asp:BoundField DataField="CablesRopeQty" HeaderText="NO" />
            <asp:BoundField DataField="DwgNo" HeaderText="Drawing No" />
            <asp:BoundField DataField="RevNo" HeaderText="Rev No" /> 
            <asp:BoundField DataField="UnitPrice" HeaderText="Unit Price" /> 
            <asp:BoundField DataField="CurrencyID" HeaderText="Currency ID" />
            <asp:BoundField DataField="TotalCost" HeaderText="Total Cost" /> 
            <asp:BoundField DataField="TotalCostLocal" HeaderText="Total Cost Local" /> 
            <asp:BoundField DataField="Notes" HeaderText="Notes" /> 
                                                                              
        </Columns>
        <FooterStyle BackColor="White" ForeColor="#000066" />
        <HeaderStyle BackColor="#3399FF" Font-Bold="True" Font-Size="11pt" ForeColor="White" />
        <RowStyle Font-Size="8pt" ForeColor="#000066" />
    </asp:GridView>   
    </form>
</body>
</html>
