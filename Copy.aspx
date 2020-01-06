<%@ Page language="c#" Codebehind="Copy.aspx.cs" AutoEventWireup="false" Inherits="NewOrderingSystem.Copy" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Copy</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="CSS/Style.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		function CanCopy()
		{
			if (document.Form1.txtContractNo.value=="")
			{
				alert('ContractNo can not be blank');
				document.Form1.txtContractNo.focus();
				return false;
			}
			if (document.Form1.txtLiftNo.value=="")
			{
				alert('LiftNo can not be blank');
				document.Form1.txtLiftNo.focus();
				return false;
			}
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table5" style="Z-INDEX: 102; LEFT: 8px; POSITION: absolute; TOP: 8px" height="200"
				cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR bgColor="seashell" height="20%">
					<TD></TD>
				</TR>
				<TR height="10%">
					<TD align="center" bgColor="inactivecaptiontext" rowSpan="1"><!--<asp:panel id="pnlMenu" runat="server" BackImageUrl="Images\toolgrad.gif" Wrap="False" HorizontalAlign="Center"
							BorderStyle="Groove">-->
						<asp:label id="lblMsg" runat="server" CssClass="labelerror">-- Copy Contract --</asp:label> <!--</asp:panel></TD>--></TD>
				</TR>
				<TR height="70%">
					<TD vAlign="middle" align="center">
						<TABLE id="Table6" cellSpacing="0" cellPadding="0" border="0" style="BORDER-RIGHT: thin solid; BORDER-TOP: thin solid; BORDER-LEFT: thin solid; BORDER-BOTTOM: thin solid">
							<TR>
								<TD colspan="3">
									<asp:label id="lblError" runat="server" DESIGNTIMEDRAGDROP="21" CssClass="labelerror"></asp:label></TD>
							</TR>
							<TR>
								<TD style="BORDER-RIGHT: thin solid; BORDER-TOP: thin solid; BORDER-LEFT: thin solid; BORDER-BOTTOM: thin solid"></TD>
								<TD align="center" style="BORDER-LEFT: thin solid"><asp:label id="Label6" runat="server">Contract</asp:label></TD>
								<TD align="center" style="BORDER-LEFT: thin solid"><asp:label id="Label5" runat="server">Lift No</asp:label></TD>
							</TR>
							<TR>
								<TD style="BORDER-TOP: thin solid"><asp:label id="lblFrom" runat="server" Width="45px">From</asp:label></TD>
								<TD style="BORDER-TOP: thin solid; BORDER-LEFT: thin solid"><asp:label id="lblContractNo" runat="server"></asp:label></TD>
								<TD style="BORDER-TOP: thin solid; BORDER-LEFT: thin solid">
									<asp:label id="lblLiftNo" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD style="BORDER-RIGHT: thin solid; BORDER-TOP: thin solid; BORDER-LEFT: thin solid; BORDER-BOTTOM: thin solid"><asp:label id="lblTo" runat="server">To</asp:label></TD>
								<TD width="112" style="BORDER-LEFT: thin solid"><asp:textbox id="txtContractNo" runat="server" DESIGNTIMEDRAGDROP="425" MaxLength="50"></asp:textbox></TD>
								<TD style="BORDER-LEFT: thin solid"><asp:textbox id="txtLiftNo" runat="server" DESIGNTIMEDRAGDROP="425" MaxLength="20"></asp:textbox></TD>
							</TR>
							<TR>
								<TD style="BORDER-RIGHT: thin solid; BORDER-TOP: thin solid; BORDER-LEFT: thin solid; BORDER-BOTTOM: thin solid"></TD>
								<TD align="center"><asp:button id="btnCopy" runat="server" CssClass="button" 
                                        Text="Copy" onclick="btnCopy_Click"></asp:button></TD>
								<TD align="center"><INPUT class="button" id="btnCancel" onclick="javascript:self.close();" type="button" value="Cancel"
										name="btnCancel"></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
