<%@ Page language="c#" Codebehind="Home.aspx.cs" AutoEventWireup="false" Inherits="NewOrderingSystem.Home" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AJAXCtrlToolKit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN"  "http://www.w3.org/TR/html4/loose.dtd">
<html>
	<head>
		<title>New Ordering System - Home</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="C#" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta http-equiv="X-UA-Compatible" content="IE=Edge" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		<link href="CSS/Style.css" type="text/css" rel="stylesheet"/>
		<link rel="stylesheet" type="text/css" href="./CSS/MenuButtonStyle.css" />	
		<script language="javascript" type="text/javascript">
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
			
		function OpenWindow(url)
		{	
			newwindow=window.open(url,"ChangePassword","width=470,height=250,left=250,top=200,resizable=no,scrollbars=no");
			if (window.focus) {newwindow.focus();}			
		}	
		
		function MultilineTextControl(ControlID,MaxLength)
        { 
			var ControlData = document.getElementById(ControlID).value; 
			if ( ControlData.length >= MaxLength) 
				event.keyCode=0; 
        }
	 
		 function display(imgName, imgUrl) 
		 {	
			if (document.images && typeof imgUrl != 'undefined')
			document.Form1[imgName].src = imgUrl.src;
		}		
		function ConfirmLogout()
		{
			if (confirm('Sure do you want to logout?'))
				location.href="Logout.aspx";
		}
		</script>
	</head>
<body onload="fnHide()" style="margin-left: 0; margin-top: 0">
    <form id="Form1" method="post" runat="server">
    <asp:ScriptManager ID="scHelpPopup" runat="server">
    </asp:ScriptManager>
    <AJAXCtrlToolKit:modalpopupextender id="mpeHelp" runat="server" popupcontrolid="pnlHelpHolder"
        targetcontrolid="lnkBtnVersion" cancelcontrolid="lnkBtnHelpClose" backgroundcssclass="Background">
            </AJAXCtrlToolKit:modalpopupextender>
    <asp:Panel ID="pnlHelpHolder" runat="server" CssClass="Popup" align="center" Style="display: none">
        <iframe style="width: 750px; height: 540px;" id="ifrHelp" src="Help.aspx" runat="server">
        </iframe>
        <br />
        <br />
        <asp:LinkButton ID="lnkBtnHelpClose" CssClass="LinkButton" runat="server" Text="Close" />
    </asp:Panel>
    <div id="Header">
        <table id="Table3" height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr valign="bottom">
                <td style="background-position: center center; width: 30%; background-repeat: no-repeat;
                    border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #3399FF;"
                    align="center" valign="middle">
                    <img id="Image3" src="Images/logo.gif" style="height: 56px; width: 201px; border-width: 0px;" />
                </td>
                <td align="left" valign="middle" style="border-bottom-style: solid; border-bottom-width: thin;
                    border-bottom-color: #3399FF">
                    <img id="Image4" src="Images/Header.png" style="width: 613px; border-width: 0px;" />
                    <asp:LinkButton ID="lnkBtnVersion"  CssClass="linkVersion" runat="server"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
    <div id="Content">
        <table width="100%" height="100%">
            <tr style="height: 20px;">
                <td align="left" valign="bottom" style="border-bottom-style: solid; border-bottom-width: thin;
                    border-bottom-color: #3399FF; background-color: #3399FF; border-top-style: solid;
                    border-top-width: thin; border-top-color: #3399FF;">
                    <asp:Label ID="lblUserName" runat="server" CssClass="usernamelabel" ForeColor="#FFFFCC"></asp:Label>
                </td>
                <td align="right" valign="bottom" style="border-bottom-color: #0000FF;">
                    <asp:LinkButton ID="lbkBtnChangePassword" runat="server" BackColor="Transparent"
                        BorderColor="Transparent" ForeColor="#996600">Change Password</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td valign="top" align="center" width="150" style="background-color: #3399FF;">
                    <br />
                    <a onmouseover="display('Home_img', Home_out);" onfocus="display('Home_img', Home_out);"  onmouseout="display('Home_img', Home_over);"
                        onblur="display('Home_img', Home_over);"  href="Home.aspx" tabindex="1">
                        <img src="Images/homeNew-h.png" border="0" name="Home_img" /></a><br />
                    <br />
                    <a onmouseover="display('New_img', New_over);" onmouseout="display('New_img', New_out);" onfocus="display('New_img', New_over);" onblur="display('New_img', New_out);"
                        href="MainNewScreen.aspx?Mode=New" tabindex="2">
                        <img src="Images/newNew.png" border="0" name="New_img"></a><br />
                    <br />
                    <a onmouseover="display('Viewoutput_img', Viewoutput_over);" onmouseout="display('Viewoutput_img', Viewoutput_out);"
                        onfocus="display('Viewoutput_img', Viewoutput_over);" onblur="display('Viewoutput_img', Viewoutput_out);" href="ViewOutput.aspx" tabindex="3" >
                    <img src="Images/outputNew.png" border="0" name="Viewoutput_img" /></a><br />
                    <br />
                    <a onmouseover="display('IssuePO_img', IssuePO_over);" onmouseout="display('IssuePO_img', IssuePO_out);"
                        onfocus="display('IssuePO_img', IssuePO_over);" onblur="display('IssuePO_img', IssuePO_out);" href="IssuePO.aspx" tabindex="4" >
                    <img alt="" src="Images/IssuePO.png" border="0" name="IssuePO_img" /></a><br />
                    <br />
                    <a onmouseover="display('Logout_img', Logout_over);" onclick="ConfirmLogout();" onmouseout="display('Logout_img', Logout_out);" 
                    tabindex="5" onfocus="display('Logout_img', Logout_over);" onblur="display('Logout_img', Logout_out);" >
                    <img src="Images/LogoutNew.png" border="0" name="Logout_img" /></a>
                </td>
                <td valign="top" width="874" colspan="1">
                    <p>
                        <asp:Label ID="lblError" runat="server" CssClass="labelerror" Font-Bold="True"></asp:Label></p>
                    <p>
                        <asp:TextBox ID="txtMultiline" runat="server" Visible="False" Width="232px" Height="64px"
                            TextMode="MultiLine"></asp:TextBox></p>
                </td>
            </tr>
        </table>
    </div>
    <div id="Footer">
    </div>
    </form>
</body>
</html>
