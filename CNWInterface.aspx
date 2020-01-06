<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CNWInterface.aspx.cs" Inherits="NewOrderingSystem.CNWInterface" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link rel="stylesheet" type="text/css" href="CSS/Style.css"/>
    <title>Newordering System - Interface from Carnetweight system</title>   
    <style type="text/css">
        .style2
        {
            height: 255px;
        }
        .style3
        {
            height: 183px;
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="style2" >
    <table id="Table1" style="border-style: none; border-color: inherit; border-width: 0;" 
        cellspacing="0" cellpadding="0" class="style3" >
        <tr bgcolor="seashell" style="height:30%">
            <td>
            </td>
        </tr>
        <tr style="height:20%">
            <td align="center" bgcolor="inactivecaptiontext" rowspan="1">                
                <asp:Label ID="lblMsg" runat="server" CssClass="labelerror">Interface from Carnetweight System</asp:Label>
            </td>
        </tr>
        <tr style="height:50%">
            <td valign="middle" align="center">
                <table id="Table2" cellspacing="1" cellpadding="1" width="400" border="0">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblError" runat="server" CssClass="labelerror" DESIGNTIMEDRAGDROP="379"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server">Job Location :</asp:Label>
                        </td>
                        <td align="left">
                            <asp:Label ID="lblJL" runat="server">Label</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server">Contract No :</asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtConNo" runat="server" Width="221px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnConfirm" runat="server" Text="Confirm" CssClass="button"></asp:Button>
                        </td>
                        <td>
                            <input class="button" id="btnClose" style="width: 72px; height: 24px" type="button"
                                value="Close" name="btnClose" onclick="self.close()"/>
                        </td>
                    </tr>
                </table>
                <input id="hidVersionNo" type="hidden" name="hidVersionNo" runat="server"/>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
