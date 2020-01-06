<%--<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid, Version=1.1.20061.60, Culture=neutral, PublicKeyToken=589f1fc067ff4031" %>--%>
<%--<%@ Register TagPrefix="c1webgrid1" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid" %>--%>
<%@ Page language="c#" Codebehind="Logout.aspx.cs" AutoEventWireup="True" Inherits="NewOrderingSystem.Logout" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>New Ordering System - Logout</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="C#" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta http-equiv="X-UA-Compatible" content="IE=Edge" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		<link href="CSS/Style.css" type="text/css" rel="stylesheet"/>
		<script language="javascript" type="text/javascript">						 
		function Done() 
		{			
			//This to close the browser with out asking for conformation, if you dont put window.opener = window; browser will ask for conformation: Ref http://www.thescripts.com/forum/thread94729.html
			window.opener = window;
			window.close();
		}
		</script>
	</head>
	<body style="margin-left:5;margin-top:0;" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table id="Table1" style="height:200" cellspacing="0" cellpadding="0" width="100%" border="0">
				<tr style=height:20%">
					<td align="center" valign="middle">
									&nbsp;</td>
				</tr>
				<tr style="height:10%">
					<td align="center"></td>
				</tr>
				<tr style="height:70%">
					<td valign="middle" align="center">
						<table id="Table2" cellspacing="1" cellpadding="1" width="328" border="0">
							<tr>
								<td align="center" colspan="2">
									<asp:Label id="lblError0" runat="server" CssClass="labelerror" Font-Size="14pt">Logout Successful!</asp:Label></td>
							</tr>
							<tr>
								<td>
									<asp:Label id="lblError" runat="server" CssClass="labelerror"></asp:Label></td>
								<td>
									&nbsp;</td>
							</tr>
							<tr>
								<td>
									<input type="submit" value="Back to Login" class="button" 
                                        ID="btnLogin" NAME="btnLogin"  onclick="~/Login.aspx"
                                         style="color: #3399FF; background-color: #FFFFFF;"></td>
								<td>
									<input type="button" value="Close" class="button" onclick="javascript:Done();" 
                                        ID="btnClose" NAME="btnClose" style="color: #3399FF; background-color: #FFFFFF"></td>
							</tr>
							<tr>
								<td align="center">
									&nbsp;</td>
								<td align="center">
									&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
