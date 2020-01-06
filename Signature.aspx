<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>
<%@ Page language="c#" Codebehind="Signature.aspx.cs" AutoEventWireup="false" Inherits="NewOrderingSystem.Signature" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>New Ordering System - Signature</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
		<meta name="author" content="Roderick Divilbiss">
		<meta name="copyright" content="? 2005, 2006 Roderick Divilbiss">
		<LINK href="CSS/Style.css" type="text/css" rel="stylesheet">
		<script language="javascript" type="text/javascript">
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
		function EnableDisablePassword()
		{		
			if (document.Form1.cbApprove.checked)
			{					
				document.Form1.txtPassword.disabled = false;
				document.Form1.txtPassword.focus();
			}
			else
			{
				document.Form1.txtPassword.value="";
				document.Form1.txtPassword.disabled = true;
			}
		}
		function ValidateForm()
		{
			if (document.Form1.txtRevisionNo.value=="")
			{
				alert('Please provide RevisionNO');
				document.Form1.txtRevisionNo.focus();
				return false;
			}
			if (document.Form1.txtIssueName.value=="")
			{
				alert('Please provide IssueName');
				document.Form1.txtIssueName.focus();
				return false;
			}			
			if (document.Form1.txtIssueDate.value=="")
			{
				alert('Please provide Issue Date');
				document.Form1.txtIssueDate.focus();
				return false;
			}
			else
			{
				//http://www.rodsdot.com/ee/dateValidate1.asp	
				//http://www.javascriptkit.com/javatutors/redev.shtml for regular expression				
				// Refere validateDate(fld) for more info
				var RegExPattern = /^((((0?[1-9]|[12]\d|3[01])[\.\-\/](0?[13578]|1[02])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|((0?[1-9]|[12]\d|30)[\.\-\/](0?[13456789]|1[012])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|((0?[1-9]|1\d|2[0-8])[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|(29[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00)))|(((0[1-9]|[12]\d|3[01])(0[13578]|1[02])((1[6-9]|[2-9]\d)?\d{2}))|((0[1-9]|[12]\d|30)(0[13456789]|1[012])((1[6-9]|[2-9]\d)?\d{2}))|((0[1-9]|1\d|2[0-8])02((1[6-9]|[2-9]\d)?\d{2}))|(2902((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00))))$/i;							
				var errorMessage = 'Issue Date Error \n Please enter valid date as month, day, and four digit year.\nYou may use a slash, hyphen or period to separate the values.\nThe date must be a real date. 30/2/2000 would not be accepted.\nFormay dd/mm/yyyy.';			
				if (!RegExPattern.test(document.Form1.txtIssueDate.value))
				{					
					alert(errorMessage);
					document.Form1.txtIssueDate.select();
					return false;
				} 				
			}
			if (document.Form1.txtCheckName.value=="")
			{
				alert('Please provide Check Name');
				document.Form1.txtCheckName.focus();
				return false;
			}
			if (document.Form1.txtCheckDate.value=="")
			{
				alert('Please provide Check Date');
				document.Form1.txtCheckDate.focus();
				return false;
			}
			else
			{
				//http://www.rodsdot.com/ee/dateValidate1.asp	
				//http://www.javascriptkit.com/javatutors/redev.shtml for regular expression	
				// Refere validateDate(fld) for more info			
				var RegExPattern = /^((((0?[1-9]|[12]\d|3[01])[\.\-\/](0?[13578]|1[02])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|((0?[1-9]|[12]\d|30)[\.\-\/](0?[13456789]|1[012])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|((0?[1-9]|1\d|2[0-8])[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|(29[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00)))|(((0[1-9]|[12]\d|3[01])(0[13578]|1[02])((1[6-9]|[2-9]\d)?\d{2}))|((0[1-9]|[12]\d|30)(0[13456789]|1[012])((1[6-9]|[2-9]\d)?\d{2}))|((0[1-9]|1\d|2[0-8])02((1[6-9]|[2-9]\d)?\d{2}))|(2902((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00))))$/i;			
				var errorMessage = 'Check Date Error \n Please enter valid date as month, day, and four digit year.\nYou may use a slash, hyphen or period to separate the values.\nThe date must be a real date. 30/2/2000 would not be accepted.\nFormay dd/mm/yyyy.';			
				if (!RegExPattern.test(document.Form1.txtCheckDate.value))
				{	
					alert(errorMessage);
					document.Form1.txtCheckDate.select();
					return false;
				} 				
			}			
		}
		function CloseMe()
		{	
			window.close();
		}
		
		/*function validateDate(fld) 
		{
		//http://www.rodsdot.com/ee/dateValidate1.asp								
			var RegExPattern = /^((((0?[1-9]|[12]\d|3[01])[\.\-\/](0?[13578]|1[02])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|((0?[1-9]|[12]\d|30)[\.\-\/](0?[13456789]|1[012])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|((0?[1-9]|1\d|2[0-8])[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|(29[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00)))|(((0[1-9]|[12]\d|3[01])(0[13578]|1[02])((1[6-9]|[2-9]\d)?\d{2}))|((0[1-9]|[12]\d|30)(0[13456789]|1[012])((1[6-9]|[2-9]\d)?\d{2}))|((0[1-9]|1\d|2[0-8])02((1[6-9]|[2-9]\d)?\d{2}))|(2902((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00))))$/i;			
			var errorMessage = 'Please enter valid date as month, day, and four digit year.\nYou may use a slash, hyphen or period to separate the values.\nThe date must be a real date. 30/2/2000 would not be accepted.\nFormay dd/mm/yyyy.';			
			if ((fld.value.match(RegExPattern)) && (fld.value!='')) 
			{
				alert('Date is OK'); 
				return false;
			} 
			else 
			{
				alert(errorMessage);
				fld.focus();
				return false;
			}
		}*/
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" MS_POSITIONING="GridLayout" onload="fnHide()">
		<form id="Form1" name="Form1" method="post" runat="server" style="background-color: #eff3fb">
			<TABLE id="Table2" height="500" cellSpacing="1" cellPadding="1" width="1024" border="0">
				<TR>
					<TD vAlign="top" width="874" colSpan="1">
						<TABLE id="Table1" cellSpacing="1" cellPadding="1" border="0">
							<TR>
								<TD><asp:label id="lblError" runat="server" CssClass="labelerror"></asp:label></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD><asp:label id="Label3" runat="server">Revision No</asp:label></TD>
								<TD><asp:textbox id="txtRevisionNo" runat="server" MaxLength="5"></asp:textbox></TD>
							</TR>
							<TR>
								<TD><asp:label id="Label1" runat="server">Issue Name</asp:label></TD>
								<TD><asp:textbox id="txtIssueName" runat="server" MaxLength="25"></asp:textbox></TD>
							</TR>
							<TR>
								<TD><asp:label id="Label2" runat="server">Issue Date</asp:label></TD>
								<TD><asp:textbox id="txtIssueDate" runat="server" MaxLength="10"></asp:textbox>
									<asp:label id="Label8" runat="server">dd/mm/yyyy</asp:label></TD>
							</TR>
							<TR>
								<TD><asp:label id="Label4" runat="server">Check Name</asp:label></TD>
								<TD><asp:textbox id="txtCheckName" runat="server" MaxLength="25"></asp:textbox></TD>
							</TR>
							<TR>
								<TD><asp:label id="Label5" runat="server" DESIGNTIMEDRAGDROP="206">Check Date</asp:label></TD>
								<TD><asp:textbox id="txtCheckDate" runat="server" MaxLength="10"></asp:textbox><asp:label id="Label9" runat="server"> dd/mm/yyyy</asp:label></TD>
							</TR>
							<TR>
								<TD><asp:label id="Label6" runat="server">Have you been Approved by Head?</asp:label></TD>
								<TD><asp:checkbox id="cbApprove" runat="server" Width="72px"></asp:checkbox><asp:label id="Label7" runat="server">Password  </asp:label><asp:textbox id="txtPassword" runat="server" TextMode="Password" Enabled="False"></asp:textbox></TD>
							</TR>
							<TR>
								<TD><asp:button id="btnOk" runat="server" CssClass="button" Text="OK"></asp:button></TD>
								<TD><asp:button id="btnCancel" runat="server" CssClass="button" Text="Cancel"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
