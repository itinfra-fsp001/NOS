<%@ Page language="c#" Codebehind="ComputationStatus.aspx.cs" AutoEventWireup="false" Inherits="NewOrderingSystem.ComputationStatus" %>
<%@ Register TagPrefix="c1webgrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid, Version=1.1.20061.60, Culture=neutral, PublicKeyToken=589f1fc067ff4031" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>New Ordering System - Computation Status</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="CSS/Style.css" type="text/css" rel="stylesheet">
		<script language="javascript">
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
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" MS_POSITIONING="GridLayout" onload="fnHide()">
		<form id="Form1" method="post" runat="server" autocomplete="off">
			<TABLE id="Table1" height="113" cellSpacing="0" cellPadding="0" width="100%" border="0"
				style="HEIGHT: 113px" background="Images/top-bannerNOS.jpg">
				<TR height="80%">
					<TD height="100"></TD>
				</TR>
				<TR height="10%">
					<TD><asp:label id="lblUserName" runat="server" CssClass="usernamelabel"></asp:label></TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" height="562" cellSpacing="0" cellPadding="0" width="984" border="0"
				style="WIDTH: 984px; HEIGHT: 562px" background="Images/background2New.JPG">
				<TR>
					<TD vAlign="top" align="center">
						<TABLE id="Table5" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD colSpan="2"><asp:label id="lblError" runat="server" CssClass="labelerror"></asp:label></TD>
							</TR>
							<TR align="left">
								<TD colSpan="2">
									<P><c1webgrid:c1webgrid id="wgComputeStatus" runat="server" AllowPaging="True" BorderWidth="1px" BorderStyle="None"
											DefaultRowHeight="22px" CellPadding="4" DefaultColumnWidth="120px" BorderColor="#33CC66" BackColor="White"
											GroupIndent="15px" AutoGenerateColumns="False" PageSize="20" ImageSortAscending="v1_13/c1grid_ImgColDn.gif"
											ImageSortDescending="v1_13/c1grid_ImgColUp.gif" Width="100%" ClientDir="v1_13/" VScrollBarStyle="Automatic"
											HScrollBarStyle="Automatic">
											<ItemStyle Font-Size="Smaller" ForeColor="#003399" BackColor="White"></ItemStyle>
											<PagerStyle HorizontalAlign="Left" ForeColor="#003399" BackColor="#99CCCC"></PagerStyle>
											<Columns>
												<c1webgrid:C1BoundColumn SortExpression="WBSElement" HeaderText="Contract No" DataField="ContractNo"></c1webgrid:C1BoundColumn>
												<c1webgrid:C1BoundColumn SortExpression="SubmitDate" HeaderText="SubmitDate" DataField="SubmitDate"></c1webgrid:C1BoundColumn>
												<c1webgrid:C1BoundColumn SortExpression="SubmitBy" HeaderText="SubmitBy" DataField="SubmitBy"></c1webgrid:C1BoundColumn>
												<c1webgrid:C1BoundColumn SortExpression="StartDate" HeaderText="StartDate" DataField="StartDate"></c1webgrid:C1BoundColumn>
												<c1webgrid:C1BoundColumn SortExpression="Status" HeaderText="Status" DataField="Status"></c1webgrid:C1BoundColumn>
											</Columns>
											<HeaderStyle Font-Size="Small" Font-Bold="True" ForeColor="#003399" BackColor="#99CCCC"></HeaderStyle>
											<SelectedItemStyle Font-Bold="True" ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
											<GroupingStyle BackColor="LightSkyBlue"></GroupingStyle>
											<FooterStyle ForeColor="#003399" BackColor="#99CCCC"></FooterStyle>
										</c1webgrid:c1webgrid></P>
								</TD>
							</TR>
						</TABLE>
						<asp:Button id="btnRefresh" runat="server" CssClass="button" Text="Refresh"></asp:Button>
						<INPUT class="button" id="btnClose" type="button" value="Close" name="btnClose" onclick="javascript:window.close();">
					</TD>
				</TR>
			</TABLE>
			<C1WebGrid1:C1WebGrid style="Z-INDEX: 101; POSITION: absolute; TOP: 768px; LEFT: 112px" id="C1WebGrid1"
				runat="server"></C1WebGrid1:C1WebGrid>
		</form>
	</body>
</HTML>
