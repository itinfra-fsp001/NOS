<%@ Page language="c#" Codebehind="Output.aspx.cs" AutoEventWireup="false" Inherits="NewOrderingSystem.Output" %>
<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>New Ordering System - Output Screen</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta http-equiv="X-UA-Compatible" content="IE=Edge" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="CSS/Style.css" type="text/css" rel="stylesheet">
		<script language="javascript">		
		function fnHide(){
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
			Load_out = new Image();
			Load_out.src = "Images/Load-button.gif";
			Load_over = new Image();
			Load_over.src = "Images/Load-button-h.jpg";
			
			New_out = new Image();
			New_out.src = "Images/New-button.gif";
			New_over = new Image();
			New_over.src = "Images/New-button-h.jpg";
			
			Logout_out = new Image();
			Logout_out.src = "Images/Logout-button.jpg";
			Logout_over = new Image();
			Logout_over.src = "Images/Logout-button-h.jpg";
			
			Home_out = new Image();
			Home_out.src = "Images/home.gif";
			Home_over = new Image();
			Home_over.src = "Images/home-h.gif";		
		function OpenWindow(url)
		{	
			newwindow=window.open(url,"InterfaceToPCES","width=300,height=150,left=250,top=225,resizable=no,scrollbars=no");
			if (window.focus) {newwindow.focus();}		
			return false;	
		}
		/*function GoPrevious()
		{
			history.back();			
			window.history.back(1);
		}				
		function isNumeric(control)
		{
			if(control.value!="")
			{
				if(isNaN(parseInt(control.value)))
				{
					alert("Please enter a valid number to continue");
					control.focus();
				}
			}
		 }
		 function CloseMe()
		 {	
			window.returnValue = "";
			window.close();
			//window.returnValue = false;		
			//self.close();
		 }*/
		 function display(imgName, imgUrl) 
		 {	
			if (document.images && typeof imgUrl != 'undefined')
			document.Form1[imgName].src = imgUrl.src;
		}		
		/*function ViewReport(RptFor)
		 {		 
			if (RptFor=='Production')
			{		
				//window.open("ViewReports.aspx?ReportName=" + RptFor+ "&StrSql=EXEC SP_GetScreenTemplateForModel '','"+document.Form1.HidContractNo.value+"'&ModelType="+document.Form1.HidModel.value+"&ContractNo="+document.Form1.HidContractNo.value,"RptPage","resizable=yes,scrollbars=yes,left=100,top=50,width=800,height=600");
				window.open("ViewReports.aspx?ReportName=Production&SpName=EXEC SP_GetScreenTemplateForModel&Model=''&ContractNo="+document.Form1.hidContractNo.value);
				return false;
			}		 
		 }*/
		 function confirmInput()
			{
				return confirm('Do you really want to change data?');
			}
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" onload="fnHide()" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server" style="background-color: #eff3fb">
			<!--	<TABLE id="Table1" height="100" cellSpacing="0" cellPadding="0" width="100%" background="Images/top-banner.jpg"
				border="0">
				<TR borderColorDark="#ff0000" height="80%">
					<TD vAlign="bottom"></TD>
				</TR>
				<TR>
					<TD></TD>
				</TR>
				<TR height="20%">
					<TD></TD>
				</TR>
				<tr>
					<td><asp:label id="lblUserName" runat="server" CssClass="usernamelabel"></asp:label></td>
				</tr>
			</TABLE> -->
			<TABLE id="Table2" height="600" cellSpacing="1" cellPadding="1" width="800" border="0">
				<TR>
					<!-- <TD vAlign="top" align="center" width="150" bgColor="#ccccff">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<A onmouseover="display('Home_img', Home_over);" onmouseout="display('Home_img', Home_out);"
							href="Home.aspx"><IMG src="Images/home.gif" border="0" name="Home_img"></A> <A onmouseover="display('New_img', New_over);" onmouseout="display('New_img', New_out);"
							href="MainScreen.aspx?Mode=New"><IMG src="Images/New-button.gif" border="0" name="New_img"></A>
						<A onmouseover="display('Load_img', Load_over);" onmouseout="display('Load_img', Load_out);"
							href="MainScreen.aspx?Mode=Load"><IMG src="Images/Load-button.gif" border="0" name="Load_img"></A>
						<A onmouseover="display('Logout_img', Logout_over);" onmouseout="display('Logout_img', Logout_out);"
							href="Logout.aspx"><IMG src="Images/Logout-button.jpg" border="0" name="Logout_img"></A>
					</TD> -->
					<TD vAlign="top" width="874" colSpan="1">
						<TABLE id="Table3" cellSpacing="1" cellPadding="1"
							width="457" border="0">
							<TR>
								<TD><asp:label id="lblError" runat="server" DESIGNTIMEDRAGDROP="379" CssClass="labelerror"></asp:label></TD>
							</TR>
							<TR>
								<TD>
									
                                    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
                                        AutoDataBind="true" />
                               
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidContractNo" type="hidden" name="hidContractNo" runat="server">
		</form>
	</body>
</HTML>
