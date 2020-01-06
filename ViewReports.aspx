<%@ Page Language="c#" CodeBehind="ViewReports.aspx.cs" AutoEventWireup="true" Inherits="NewOrderingSystem.ViewReports" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>ViewReports</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1" />
    <meta name="CODE_LANGUAGE" content="C#" />
    <meta name="vs_defaultClientScript" content="JavaScript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />

    <script language="javascript" type="text/javascript">
        function fnHide() {
            window.setTimeout("fnHide2()", 1740000);
        }
        function fnHide2() {
            window.focus();
            if (confirm('Your session going to expire in 60 sec. Do you want to continue?')) {
                window.document.forms(0).submit();
            }
            else {
                location.href = 'Login.aspx';
            }
        }
        function ExitReportForNoCostReport() {
            alert('No Records found for Cost Report, please verify zero cost exist or not  using ZeroCostReport !');
            window.close();
        }
        function ExitReportForZeroCostReport() {
            alert('No Records found for Zero Cost Report !');
            window.close();
        }

        function NoRecordForComponentReport() {
            alert('No Records found for Component Report !');
            //window.close();
        }


        function checkComponentNo() {
            if (document.getElementById("<%=txtComponentNo.ClientID %>").value == "" || document.getElementById("<%=txtComponentNo.ClientID %>").value.trim() == "") {
                alert("Please enter component no.");
                return false;
            }
            return true;
        }
    </script>

</head>
<body ms_positioning="GridLayout" leftmargin="0" topmargin="0" onload="fnHide()">
    <form id="Form1" method="post" runat="server" style="background-color: #eff3fb">
    <table style="width: 100%;">
        <tr>
            <td style="width: 100%;">
                <asp:Panel ID="pnlVendor" runat="server">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Preferred Vendor"></asp:Label>
                                <asp:DropDownList ID="ddlVendor" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Panel ID="pnlMatCategory" runat="server">
                                    <asp:Label ID="lblMatCategory" runat="server" Text="Material Category"></asp:Label>
                                    <asp:DropDownList ID="ddlMatCategory" runat="server">
                                    </asp:DropDownList>
                                </asp:Panel>
                            </td>
                            <td>
                                <asp:Button ID="btnShow" runat="server" Style="margin-left: 15px" OnClick="btnShow_Click"
                                    Text="Show" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnlComponent" runat="server">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Component No"></asp:Label>
                                <asp:TextBox ID="txtComponentNo" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnShowComponent" runat="server" Style="margin-left: 15px" OnClientClick="return checkComponentNo();" OnClick="btnShowComponent_Click"
                                    Text="Show" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td style="width: 100%;">
                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
                    ReuseParameterValuesOnRefresh="True" DisplayGroupTree="False" PrintMode="ActiveX" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
