<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActivationHistoryMaintain.aspx.cs" Inherits="NewOrderingSystem.ActivationHistoryMaintain" %>
<%@ Register Assembly="Utilities" Namespace="Utilities" TagPrefix="cc1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN"  "http://www.w3.org/TR/html4/loose.dtd">
<html> 
<head>
		<title>New Ordering System - Issue purchase order</title>
		    <%--<script type='text/javascript' src='Styles/x.js'></script>
            <script type='text/javascript' src='Styles/xtableheaderfixed.js'></script>
            <script type='text/javascript'>
                xAddEventListener(window, 'load',function() { new xTableHeaderFixed('grViewOutput', 'table-container', 0); }, false);
            </script>--%>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="C#" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta http-equiv="X-UA-Compatible" content="IE=Edge" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		<link href="CSS/Style.css" type="text/css" rel="stylesheet" />
		<link href="./CSS/MenuButtonStyle.css" type="text/css" rel="stylesheet" />
		<link rel='stylesheet' type='text/css' href='Styles/StaticHeader.css' />
		<script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
		<script type='text/javascript' language="javascript">
		
		    $(document).ready(function() {
		    try {
		        document.getElementById("UpdatePanel1").style.height = "100%";

		          } catch (e) { }
		    });
		    
		    Load_out = new Image();
		    Load_out.src = "Images/Load-button.gif";
		    Load_over = new Image();
		    Load_over.src = "Images/Load-button-h.gif";

		    New_out = new Image();
		    New_out.src = "Images/NewNew.png";
		    New_over = new Image();
		    New_over.src = "Images/NewNew-h.png";

		    Logout_out = new Image();
		    Logout_out.src = "Images/LogoutNew.png";
		    Logout_over = new Image();
		    Logout_over.src = "Images/LogoutNew-h.png";

		    Home_out = new Image();
		    Home_out.src = "Images/HomeNew.png";
		    Home_over = new Image();
		    Home_over.src = "Images/homeNew-h.png";

		    Viewoutput_out = new Image();
		    Viewoutput_out.src = "Images/outputNew.png";
		    Viewoutput_over = new Image();
		    Viewoutput_over.src = "Images/outputNew-h.png";

		    IssuePO_out = new Image();
		    IssuePO_out.src = "Images/IssuePO.png";
		    IssuePO_over = new Image();
		    IssuePO_over.src = "Images/IssuePO-h.png";

		function fnHide()
	    {
	    window.setTimeout("fnHide2()",1140000);
		
	    }
	    
		function fnHide2()
		{
			window.focus();		
			if (confirm('Your session going to expire in 60 sec. Do you want to continue?'))
			{
				window.document.forms(0).submit();
			}
			else
			{
				location.href ='Login.aspx';
			}

        }


        function IsItemSelected() {
            if (document.Form1.HidPartNo.value == "") {
                alert("Please Select an item for this action");
                return false;
            }
            else {
                var retVal = confirm('Version number will be incremented, Are you sure you want to save reason for revision ?');
                return retVal;
            }		
		    var gv = document.getElementById("<%=grPrjRev.ClientID%>");
		    var bRetVal = false;
		    var rbs = gv.getElementsByTagName("input");
		    
		    for (var i = 0; i < rbs.length; i++) {

		        if (rbs[i].type == "radio") {

		            if (rbs[i].checked) {

		                bRetVal = true;

		                break;

		            }

		        }

		    }
		    if (!bRetVal) {
		        alert("Please Select an item for this action");
		        return false;
		    }
		    else {
		        var retVal = confirm('Version number will be incremented, Are you sure you want to save reason for revision ?');
		        return retVal; 
		    }
						
		}
		
		 function GoHome()
		 {		 		 			
			window.location="Home.aspx";		
		 }		 
        function display(imgName, imgUrl) 
        {
            if (document.images && typeof imgUrl != 'undefined')
                document.Form1[imgName].src = imgUrl.src;
        }
        function ConfirmLogout() {
            if (confirm('Sure do you want to logout?'))
                location.href = "Logout.aspx";
        }
        function RadioCheck(rb,sPartNumber) {

            var gv = document.getElementById("<%=grPrjRev.ClientID%>");
            document.Form1.HidPartNo.value = sPartNumber;
            var rbs = gv.getElementsByTagName("input");
            var row = rb.parentNode.parentNode;

            for (var i = 0; i < rbs.length; i++) {

                if (rbs[i].type == "radio") {

                    if (rbs[i].checked && rbs[i] != rb) {

                        rbs[i].checked = false;

                        break;

                    }

                }

            }

        }
       
		</script>
	    </head>
	<body onload="fnHide()">
        <form id="Form1" name="Form1" method="post" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>        
        <input id="HidPartNo" type="hidden" name="HidPartNo" runat="server"  />
        
        <table id="Table3" width="100%" 
            style="border: thin solid #3399FF; width: 100%; height: 100%;">
            <tr style="height:100px; border-bottom-width: 1px; border-bottom-color: #0000FF;">
                <td >
                    <table id="Table2" style="height: 100%;" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr valign="bottom">
                            <td style="background-position: center center; width: 30%; background-repeat: no-repeat;border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #3399FF;" align="left" valign="middle">
                                <asp:Image ID="Image2" runat="server" Height="56px" ImageUrl="~/Images/logo.gif" Width="201px" />
                            </td>
                            <td align="left" valign="middle" style="border-bottom-style: solid; border-bottom-width: thin;border-bottom-color: #3399FF;">
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Header.png" Width="613px" />
                                 <asp:Label ID="lblVersion" runat="server" CssClass="labelVersion"  ></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="height: 20px; background-color: #3399FF;">
                <td >
                    <asp:Label ID="lblUserName" runat="server" CssClass="usernamelabel" ForeColor="#FFFFCC"></asp:Label>
                </td>                
            </tr>            
            <tr valign="top" style="height:100%;">
                <td style="width:100%;height: 100%;">
                    <table style="width:100%;height: 100%;">
                        <tr valign="top">
                            <td style="width: 15%;background-color: #3399FF;" align="center">
                                <br/>
                                <a onmouseover="display('Home_img', Home_over);" onmouseout="display('Home_img', Home_out);" href="Home.aspx"
                                    onfocus="display('Home_img', Home_over);" onblur="display('Home_img', Home_out);" tabindex="1">
                                    <img alt="" src="Images/homeNew.png" border="0" name="Home_img" /></a><br/>
                                <br />
                                <a onmouseover="display('New_img', New_over);" onmouseout="display('New_img', New_out);" href="MainNewScreen.aspx?Mode=New"
                                    onfocus="display('New_img', New_over);" onblur="display('New_img', New_out);" tabindex="2">
                                    <img alt="" src="Images/newNew.png" border="0" name="New_img" /></a><br/>
                                <br />
                                <a onmouseover="display('Viewoutput_img', Viewoutput_over);" onmouseout="display('Viewoutput_img', Viewoutput_out);" href="ViewOutput.aspx"
                                    onfocus="display('Viewoutput_img', Viewoutput_over);" onblur="display('Viewoutput_img', Viewoutput_out);" tabindex="3">
                                <img alt="" src="Images/outputNew.png" border="0" name="Viewoutput_img" /></a><br/>
                                <br />
                                <a onmouseover="display('IssuePO_img', IssuePO_out);" onmouseout="display('IssuePO_img',IssuePO_over );" href="IssuePO.aspx"
                                    onfocus="display('IssuePO_img', IssuePO_out);" onblur="display('IssuePO_img',IssuePO_over );" tabindex="4">
                                <img alt="" src="Images/IssuePO-h.png" border="0" name="IssuePO_img" /></a><br/>
                                <br />
                                <a onmouseover="display('Logout_img', Logout_over);" onclick="ConfirmLogout();" onmouseout="display('Logout_img', Logout_out);"
                                    onfocus="display('Logout_img', Logout_over);" onblur="display('Logout_img', Logout_out);" tabindex="5">
                                <img alt="" src="Images/LogoutNew.png" border="0" name="Logout_img" /></a>
                            </td> 
                            
                            <td valign="top" style="width:85%">    
                                <table style="width:100%;height:100%">                                                                     
                                    <tr style="height:100%">
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                            <asp:Label ID="lblError" runat="server" CssClass="labelerror"></asp:Label>
                                            <br />
                                            <br />                                            
                                            <asp:Panel ID="pnlPrjRev" runat="server" style="height:100%;">
                                                <asp:GridView ID="grPrjRev" AutoGenerateColumns="False" runat="server" OnRowDataBound="grPrjRev_RowDataBound"
                                                            CellPadding="3" ForeColor="#333333"  BorderColor="black" BorderStyle="Solid"
                                                            BorderWidth="1px" PageSize="18" OnPageIndexChanging="grPrjRev_PageIndexChanging"
                                                            CssClass="grViewOutput" AllowPaging="True" BackColor="White" GridLines="Vertical"  >
                                                            <RowStyle Font-Size="8pt" Height="15px" ForeColor="#000066"/>
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Select">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="rdbContract" AutoPostBack="true" GroupName="prjrevlist"  OnCheckedChanged="OnProjectRevSelect"  runat="server" />
                                                                    </ItemTemplate>
                                                                    <ControlStyle Font-Size="8pt" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>                                                                
                                                                <asp:BoundField HeaderText="Part Number" DataField="PartNo" />
                                                                <asp:BoundField DataField="Description" HeaderText="Set Name" />
                                                                <asp:BoundField HeaderText="Version No" DataField="VerNo" /> 
                                                                <asp:BoundField HeaderText="Last Update Date" DataField="RevUpdateDate" />                                                                
                                                            </Columns>
                                                            <FooterStyle BackColor="#507CD1" />
                                                            <PagerStyle BackColor="#006699" ForeColor="White" HorizontalAlign="left" />
                                                            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                                            <HeaderStyle BackColor="#006699" Font-Bold="True" 
                                                                ForeColor="White" />
                                                            <AlternatingRowStyle BackColor="#EBEBD8" />
                                                        </asp:GridView>
                                            </asp:Panel>
                                            </ContentTemplate>                                            
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:Label ID="lblRevCaption" runat="server" Text="Reason for revision" Font-Bold="True"></asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtRevReason" runat="server" Height="90px"   
                                                    Width="580px" MaxLength="300" TextMode="MultiLine" ></asp:TextBox>                                                   
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>                                                
                                            <asp:Panel ID="PnlBtn" runat="server" Visible="False">
                                                <ul id="navPnlBtn" class="NavMain">
	                                                <li >
                                                        <asp:LinkButton ID="btnSaveKSet" runat="server" OnClientClick="return IsItemSelected();" onclick="btnSaveKSet_Click" >&nbsp;&nbsp;Save&nbsp;&nbsp;</asp:LinkButton>  
                                                    </li>                                                              
                                                </ul>
                                              </asp:Panel>
                                            </ContentTemplate>                                                
                                            </asp:UpdatePanel>  
                                        </td>
                                    </tr>                                     
                                </table>
                            </td>
                        </tr>
                    </table>                    
                </td>
            </tr>
        </table>
        </form>        
    </body>
</html>
