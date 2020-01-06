<%@ Page language="c#" Codebehind="PCESInteface.aspx.cs" AutoEventWireup="True" Inherits="NewOrderingSystem.PCESInteface" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>PCESInteface</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta http-equiv="X-UA-Compatible" content="IE=Edge" />
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
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
		</script>		
	</HEAD>
	<body MS_POSITIONING="GridLayout" onload="fnHide()">
		<form id="Form1" method="post" runat="server" style="background-color: #eff3fb">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 16px; POSITION: absolute; TOP: 24px" cellSpacing="1"
				cellPadding="1" width="300" border="0">
				<TR>
					<TD colSpan="2">
						<asp:label id="lblError" runat="server" CssClass="labelerror" DESIGNTIMEDRAGDROP="379"></asp:label></TD>
				</TR>
				<TR>
					<TD>
						<asp:Label id="Label1" runat="server">Job Location :</asp:Label></TD>
					<TD>
						<asp:Label id="lblCountryCode" runat="server">Label</asp:Label></TD>
				</TR>
				<TR>
					<TD>
						<asp:Label id="Label2" runat="server">WBSElement :</asp:Label></TD>
					<TD>
						<asp:DropDownList id="ddlWBSElement" runat="server" AutoPostBack="True" onselectedindexchanged="ddlWBSElement_SelectedIndexChanged"></asp:DropDownList></TD>
				</TR>
				<TR>
					<TD>
						<asp:Button id="btnConfirm" runat="server" Text="Confirm" CssClass="button" onclick="btnConfirm_Click"></asp:Button></TD>
					<TD><INPUT class="button" id="btnClose" style="WIDTH: 72px; HEIGHT: 24px" type="button" value="Close"
							name="btnClose" onclick="self.close()"></TD>
				</TR>
			</TABLE>
			<INPUT id="hidVersionNo" type="hidden" name="hidVersionNo" runat="server" style="Z-INDEX: 102; LEFT: 32px; POSITION: absolute; TOP: 120px">
		</form>
	</body>
</HTML>
