<%@ Page language="c#" Codebehind="ErrorPage.aspx.cs" AutoEventWireup="false" Inherits="NewOrderingSystem.ErrorPage" %>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid, Version=1.1.20061.60, Culture=neutral, PublicKeyToken=589f1fc067ff4031" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>New Ordering System - Login Page</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="CSS/Style.css" rel="stylesheet" type="text/css">
	</HEAD>
	<body leftMargin="0" topMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server" autocomplete="off">
			<TABLE id="Table1" height="100" cellSpacing="1" cellPadding="1" width="100%" border="0"
				background="Images/top-bannerNOS.jpg">
				<TR height="75%">
					<TD></TD>
				</TR>
				<TR height="25%">
					<TD align="center"></TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" height="549" cellSpacing="1" cellPadding="1" width="100%" border="0"
				style="HEIGHT: 549px">
				<TR>
					<TD vAlign="top" align="left">
						<P>
							<asp:label id="lblError" runat="server" Width="96px" ForeColor="Red" Font-Bold="True" DESIGNTIMEDRAGDROP="16">Error Occured</asp:label></P>
						<P>
							<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="300" border="1">
								<TR>
									<TD>
										<asp:label id="Label1" runat="server" ForeColor="Red">May be the Following reason(s) <Br> <font size="2px">
					</asp:label></TD>
								</TR>
								<TR>
									<TD>1. You may idle for more the 20min</TD>
								</TR>
								<TR>
									<TD>
										<asp:Button id="btnLogin" runat="server" Text="Go To Login" CssClass="button"></asp:Button></TD>
								</TR>
							</TABLE>
						</P>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
