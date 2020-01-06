<%@ Page language="c#" Codebehind="ChangePassword.aspx.cs" AutoEventWireup="false" Inherits="NewOrderingSystem.ChangePasswrod" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>New Ordering System - Change Passwrod</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="CSS/Style.css" type="text/css" rel="stylesheet">
		<meta http-equiv="X-UA-Compatible" content="IE=Edge" />
		<script language="javascript">
		/*function CloseWindow()
		{
			//window.open("ChangePassword.aspx","ChangePassword","width=470,height=250,left=250,top=200,resizable=no,scrollbars=no,model=yes");
			self.close();
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
		 }*/
		 function IsRePasswordCorrect()
		 {							 
			//alert('values is' + Form1.txtOldPasswrod.value.length)
			if(Form1.txtOldPasswrod.value.length==0)
			{
				alert('OldPassword Cannot be blank');
				Form1.txtOldPasswrod.select();
				Form1.txtOldPasswrod.focus();
				return false;
			}
			else if(Form1.txtNewPassword.value.length==0)
			{
				alert('NewPassword Cannot be blank');
				Form1.txtNewPassword.select();
				Form1.txtNewPassword.focus();
				return false;
			}
			else if(Form1.txtReEnterPasswrod.value.length==0)
			{
				alert('Re-EnterPasswrod Cannot be blank');
				Form1.txtReEnterPasswrod.select();
				Form1.txtReEnterPasswrod.focus();
				return false;
			}
			else if (Form1.txtOldPasswrod.value==Form1.txtNewPassword.value)
			{
				alert('Old password and New Password can not be same');
				Form1.txtNewPassword.select();
				Form1.txtNewPassword.focus();
				return false;
			}
			else if(Form1.txtNewPassword.value!=Form1.txtReEnterPasswrod.value)
				{
					alert('Re-Enter Passwrod is Wrong');
					Form1.txtReEnterPasswrod.select();
					Form1.txtReEnterPasswrod.focus();
					return false;
				}
			else
				{				
					return true;
				}
				
		 }
		 function Done() {
	/*var ParmA = tbParamA.value;
	var ParmB = tbParamB.value;
	var ParmC = tbParamC.value;
	var MyArgs = new Array(ParmA, ParmB, ParmC);
	window.returnValue = MyArgs;
	window.close();*/
	self.close();
}
		</script>
	</HEAD>
	<body leftMargin="5" topMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" height="200" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR height="20%">
					<TD></TD>
				</TR>
				<TR height="10%">
					<TD align="center" rowSpan="1"><!--<asp:panel id="pnlMenu" runat="server" BackImageUrl="Images\toolgrad.gif" Wrap="False" HorizontalAlign="Center"
							BorderStyle="Groove">--><asp:label id="lblMsg" runat="server" CssClass="labelerror">For first time login you must change your password</asp:label>
						<!--</asp:panel></TD>--></TD>
				</TR>
				<TR height="70%">
					<TD vAlign="middle" align="center">
						<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="328" border="0">
							<TR>
								<TD colSpan="2"><asp:label id="lblError" runat="server" CssClass="labelerror" DESIGNTIMEDRAGDROP="21"></asp:label></TD>
							</TR>
							<TR>
								<TD>Old Password</TD>
								<TD><asp:textbox id="txtOldPasswrod" runat="server" TextMode="Password" MaxLength="10"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>New Password</TD>
								<TD><asp:textbox id="txtNewPassword" runat="server" DESIGNTIMEDRAGDROP="425" TextMode="Password"
										MaxLength="10"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>Re-Enter New Password</TD>
								<TD><asp:textbox id="txtReEnterPasswrod" runat="server" TextMode="Password" MaxLength="10"></asp:textbox></TD>
							</TR>
							<TR>
								<TD align="center"><asp:button id="btnChange" runat="server" CssClass="button" DESIGNTIMEDRAGDROP="92" Text="Change"></asp:button></TD>
								<TD align="center"><input class="button" id="btnCancel" onclick="javascript:self.close();" type="button" value="Cancel"
										name="btnCancel">
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>			
		</form>
	</body>
</HTML>
