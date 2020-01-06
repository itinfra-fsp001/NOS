<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IssuePO.aspx.cs" Inherits="NewOrderingSystem.IssuePO" %>
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
		        try { document.getElementById("UpdatePanel1").style.height = "100%"; } catch (e) { }
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

        var previousRow;
        var previousRowBC;
        function GetWBSDetails(DocumentNo, WBSRefNo, WBSNo, ModelType, MaterialCategoryID, VersionNo, Status, ReleaseBy, ReleaseDate, ProjRefNo, row) {

            document.Form1.HidDocumentNo.value = DocumentNo;
            document.Form1.HidWBSRefNo.value = WBSRefNo;
            document.Form1.HidWBSNo.value = WBSNo;
            document.Form1.HidModelType.value = ModelType;
            document.Form1.HidMaterialCategoryID.value = MaterialCategoryID;
            document.Form1.HidVersionNo.value = VersionNo;
            document.Form1.HidStatus.value = Status;
            document.Form1.HidReleaseBy.value = ReleaseBy;
            document.Form1.HidReleaseDate.value = ReleaseDate;
            document.Form1.HidProjRefNo.value = ProjRefNo;

            //If last clicked row and the current clicked row are same
            if (previousRow == row)
                return; //do nothing
            //If there is row clicked earlier
            else if (document.getElementById(previousRow) != null)
            //change the color of the previous row back to its own color                           
                document.getElementById(previousRow).style.backgroundColor = previousRowBC;

            //change the color of the current row to light yellow
            previousRowBC = document.getElementById(row).style.backgroundColor;
            document.getElementById(row).style.backgroundColor = "#BCD0F1";
            //assign the current row id to the previous row id 
            //for next row to be clicked
            previousRow = row;
        }
		
		function IsItemSelected()
		{
		    if (document.Form1.HidWBSRefNo.value == "")
			{				
				alert ("Please Select an item for this action");
				return false;
			}
			return true;			
		}
		/*function display(imgName,imgUrl) 
		 {	
			if (document.images && typeof imgUrl != 'undefined')
			document.Form1[imgName].src = imgUrl.src;			
		 }*/
		 
		function OpenWindowForCopy(url)
		{
		    if (document.Form1.HidWBSRefNo.value == "")
			{				
				alert ("Please Select an item for this action");							
			}
			else
			{
			    newwindow = window.open(url + "?RefNo=" + document.Form1.HidWBSRefNo.value + "&ContractNo=" + document.Form1.HidContractNo.value, "CopyContract", "width=470,height=250,left=250,top=200,resizable=no,scrollbars=no");
				if (window.focus) {newwindow.focus();}						
			}
			return false;
		}	
		function OpenNewWindow(OpenFor)
		{			
			if(OpenFor=='Compute')
				window.open('ComputationStatus.aspx','Compute','width=800,height=600');				
		}
		 function GoHome()
		 {		 		 			
			window.location="Home.aspx";		
		 }
		 
		 function ViewReport(RptFor)
		 {
		     if (document.Form1.HidWBSRefNo.value == "") {
		         alert("Please Select an item for this action");
		     }
/*	     else if (document.Form1.HidStatus.value == "Processing" || document.Form1.HidStatus.value == "JobQueue") {
		         alert("Selected Item is in " + document.Form1.HidStatus.value + ", No Exception available... ");
		     }
		     else if (RptFor == 'Exception') {
		         window.open("ViewReports.aspx?ReportName=" + RptFor + "&StrSql=EXEC SP_GetTempException ''&RefNo=" + document.Form1.HidRefNo.value, "RptPage", "resizable=yes,scrollbars=yes,left=100,top=50,width=800,height=600");
		     }*/
		     else if (RptFor == 'OutputPO') {
		     window.open("ViewReports.aspx?ReportName=" + RptFor + "&StrSql=EXEC SP_RptWBSSetDetails 0,'All',''" + "&RefNo=" + document.Form1.HidWBSRefNo.value + "&IsPDF=false", "RptPage", "resizable=yes,status=yes,scrollbars=yes,left=100,top=50");
		     }
		     else if (RptFor == 'PDFOutputPO') {
		     window.open("ViewReports.aspx?ReportName=" + RptFor + "&StrSql=EXEC SP_RptWBSSetDetails 0,'All',''" + "&RefNo=" + document.Form1.HidWBSRefNo.value + "&IsPDF=true", "RptPage", "resizable=yes,status=yes,scrollbars=yes,left=100,top=50");
		     }
		     else if (RptFor == 'OutputListPO') {
		         //window.open("KSetReport.aspx?ReportName=" + RptFor + "&StrSql=EXEC SP_RptKSetList " + document.Form1.HidRefNo.value + ",'All'&RefNo=" + document.Form1.HidRefNo.value + "&IsPDF=false", "RptPage", "resizable=yes,status=yes,scrollbars=yes,left=100,top=50");
		     window.open("ViewReports.aspx?ReportName=" + RptFor + "&StrSql=EXEC SP_RptKSetWBSList " + document.Form1.HidWBSRefNo.value + ",'All'&RefNo=" + document.Form1.HidWBSRefNo.value + "&IsPDF=false", "RptPage", "resizable=yes,status=yes,scrollbars=yes,left=100,top=50");
		     }
		     else if (RptFor == 'PDFOutputListPO') {
		     window.open("ViewReports.aspx?ReportName=" + RptFor + "&StrSql=EXEC SP_RptKSetWBSList " + document.Form1.HidWBSRefNo.value + ",'All'&RefNo=" + document.Form1.HidWBSRefNo.value + "&IsPDF=true", "RptPage", "resizable=yes,status=yes,scrollbars=yes,left=100,top=50");
		     }
		     else if (RptFor == 'ExportReport') {
		     window.open("ReportExport.aspx?RefNo=" + document.Form1.HidWBSRefNo.value + "&PrjDocNo=" + document.Form1.HidDocumentNo.value, "ReportExport", "resizable=yes,status=yes,scrollbars=yes,left=100,top=50");
		     }
		     
		     
            /* else if (RptFor == 'Production') {
		         window.open("ViewReports.aspx?ReportName=Production&SpName=EXEC SP_RptGetScreenTemplateForModel&Model=" + document.Form1.HidModel.value + "&ContractNo=" + document.Form1.HidContractNo.value, "RptPage", "resizable=yes,status=yes,scrollbars=yes,left=100,top=50,width=800,height=600");
		     }*/
		 }
		 /*function OpenSignature() {
		     //JOBQUEUE  PROCESSING		 
		     if ((document.Form1.HidStatus.value.toUpperCase() == 'JOBQUEUE') || (document.Form1.HidStatus.value.toUpperCase() == 'PROCESSING')) {
		         alert("Contract is in " + document.Form1.HidStatus.value + " for Selected item, You cannot Export at this time, Save and try again");
		         return false;
		     }
		     if (document.Form1.HidRefNo.value == "") {
		         alert("Please Select an item for this action");
		         return false;
		     }
		     else {
		         window.open("Signature.aspx?Model=" + document.Form1.HidModel.value + "&RefNo=" + document.Form1.HidRefNo.value + "&OutputType=1", "Signature", "resizable=yes,menubar=1,width=600,height=300");
		         return false;
		     }
		 }*/
		 
		 function CanProcess(ProcessFor) {
		     if (document.Form1.HidWBSRefNo.value == "") {
		         alert("Please Select an item for this action");
		         return false;
		     }
		     if (ProcessFor == 'SAVE') {
		         if (document.Form1.HidStatus.value == 'Processing' || document.Form1.HidStatus.value == 'JobQueue') {
		             alert('Contract is in ' + document.Form1.HidStatus.value + ' for Selected item, You cannot Save at this time');
		             return false;
		         }
		     }
		     else if (ProcessFor == 'CHANGEINPUTDATA') {
		         if (document.Form1.HidStatus.value == 'Processing' || document.Form1.HidStatus.value == 'JobQueue') {
		             alert('Contract is in ' + document.Form1.HidStatus.value + ' for Selected item, You cannot Change at this time');
		             return false;
		         }
		     }
		     else if (ProcessFor == 'COPY') {
		         if (document.Form1.HidStatus.value == 'Processing' || document.Form1.HidStatus.value == 'JobQueue') {
		             alert('Contract is in ' + document.Form1.HidStatus.value + ' for Selected item, You cannot Copy at this time');
		             return false;
		         }
		     }

		     else if (ProcessFor == 'COMPUTE') {
		         if (document.Form1.HidStatus.value == 'Saved') {
		             alert('"Saved" item can not compute from this screen, Compute from Main screen');
		             return false;
		         }
		         else if (document.Form1.HidStatus.value == 'Processing' || document.Form1.HidStatus.value == 'JobQueue') {
		             alert('Contract is in ' + document.Form1.HidStatus.value + ' for Selected item, You cannot compute at this time');
		             return false;
		         }
		     }
		     else if (ProcessFor == 'DELETE') {
		         if (document.Form1.HidStatus.value == 'Processing') {
		             alert('Contract is in ' + document.Form1.HidStatus.value + ' for Selected item, You cannot Delete at this time');
		             return false;
		         }
		         else {
		             return confirm('Do you want to delete this Contract?');
		         }
		     }
		     else if (ProcessFor == 'RELEASE') {		    
		     if (document.Form1.HidVersionNo.value == '') {
		         alert('Version is not generated for release');
		             return false;
		         }
		     }
		     else if (ProcessFor == 'ACTIVATE') {
		         if (document.Form1.HidStatus.value != 'End') {
		             alert('Status should be "END" to Activate');
		             return false;
		         }
		         return confirm('New version will be generated for Order Release, Are you sure to activate?');
		     }
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
		</script>
	    </head>
	<body onload="fnHide()">
        <form id="Form1" name="Form1" method="post" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <input id="HidDocumentNo" type="hidden" name="HidDocumentNo" runat="server" />
        <input id="HidWBSRefNo" type="hidden" name="HidWBSRefNo" runat="server" designtimedragdrop="710" />
        <input id="HidWBSNo" type="hidden" name="HidWBSNo" runat="server" />
        <input id="HidModelType" type="hidden" name="HidModelType" runat="server" />
        <input id="HidVendorName" type="hidden" name="HidVendorName" runat="server" /> 
        <input id="HidMaterialCategoryID" type="hidden" name="HidMaterialCategoryID" runat="server" /> 
        <input id="HidVersionNo" type="hidden" name="HidVersionNo" runat="server" />        
        <input id="HidStatus" type="hidden" name="HidStatus" runat="server" />
        <input id="HidReleaseBy" type="hidden" name="HidReleaseBy" runat="server" />
        <input id="HidReleaseDate" type="hidden" name="HidReleaseDate" runat="server" /> 
        <input id="HidProjRefNo" type="hidden" name="HidProjRefNo" runat="server" />
        
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
                                    <tr>
                                        <td>                                            
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:Label ID="lblError" runat="server" CssClass="labelerror"></asp:Label>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnGet" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="9pt">Contract No : </asp:Label>
                                                        <asp:TextBox ID="txtSearchContract" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="9pt">Model Type : </asp:Label>
                                                        <asp:DropDownList ID="ddlModelType" CssClass="ddl" runat="server" >
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="9pt">Material Category ID : </asp:Label>
                                                        <asp:DropDownList ID="ddlMatCatID" CssClass="ddl" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:LinkButton ID="btnGet" runat="server" CssClass="LinkButton" OnClick="btnGet_Click"
                                                            Text="Get" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr style="height:100%">
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                            <asp:Panel ID="pnlWBS" runat="server" style="height:100%;">
                                                <asp:GridView ID="grWBS" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                    BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px" 
                                                    CellPadding="4" CssClass="grViewOutput"
                                                    ForeColor="#333333" GridLines="None" OnRowDataBound="grWBS_RowDataBound" 
                                                    PageSize="20" onpageindexchanging="grWBS_PageIndexChanging">
                                                    <RowStyle BackColor="#EFF3FB" Font-Size="8pt" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Select">
                                                            <ItemTemplate>                                                             
                                                            </ItemTemplate>
                                                            <ControlStyle Font-Size="8pt" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="DocumentNo" HeaderText="Document No" />
                                                        <asp:BoundField DataField="WBSRefNo" HeaderText="WBSRefNo" Visible="False" />
                                                        <asp:BoundField DataField="WBSNo" HeaderText="Contract Number" />
                                                        <asp:BoundField DataField="ModelType" HeaderText="Model Type" />
                                                        <asp:BoundField DataField="MaterialCategoryID" HeaderText="MaterialCategoryID" />
                                                        <asp:BoundField DataField="VersionNo" HeaderText="Version No" />
                                                        <asp:BoundField DataField="Status" HeaderText="Status" />
                                                        <asp:BoundField DataField="ReleaseBy" HeaderText="Release By" />
                                                        <asp:BoundField DataField="ReleaseDate" HeaderText="Release Date" />
                                                        <asp:BoundField DataField="ProjRefNo" HeaderText="ProjRefNo" Visible="False" />
                                                        <asp:BoundField DataField="ComputeDate" HeaderText="Compute Date" />
                                                    </Columns>
                                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Justify" />
                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                    <EditRowStyle BackColor="#2461BF" />
                                                    <AlternatingRowStyle BackColor="White" />
                                                </asp:GridView>
                                            </asp:Panel>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnGet" EventName="Click" />
                                            </Triggers>
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
                                                        <asp:LinkButton ID="btnOutput" runat="server" OnClientClick="ViewReport('OutputPO');return false;">K-Set Details</asp:LinkButton>  
                                                    </li>
                                                    <li >
                                                        <asp:LinkButton ID="btnOutput0" runat="server" OnClientClick="ViewReport('PDFOutputPO');return false;">PDF K-Set Details</asp:LinkButton>  
                                                    </li>
	                                                <li >
                                                        <asp:LinkButton ID="btnOutputList" runat="server" OnClientClick="ViewReport('OutputListPO');return false;">K-SetListing</asp:LinkButton>  
                                                    </li>
	                                                <li >
                                                        <asp:LinkButton ID="btnOutputList0" runat="server" OnClientClick="ViewReport('PDFOutputListPO');return false;">PDF K-SetListing</asp:LinkButton>  
                                                    </li>
	                                                <li >
                                                        <asp:LinkButton ID="btnExportReport" runat="server" OnClientClick="ViewReport('ExportReport');return false;">Export Report</asp:LinkButton>  
                                                    </li>
                                                    <li >
                                                        <asp:LinkButton ID="btnERPInterface" runat="server" OnClientClick="return IsItemSelected();"                                                              
                                                            onclick="btnERPInterface_Click">ERP Interface</asp:LinkButton>  
                                                    </li>	
                                                    <li >
                                                        <asp:LinkButton ID="btnERPInterfaceStatus" runat="server" 
                                                            OnClientClick="return IsItemSelected();" onclick="btnERPInterfaceStatus_Click"                                                              
                                                            >Interface Status</asp:LinkButton>  
                                                    </li>	
                                                    
                                                    <li >
                                                        <asp:LinkButton ID="btnShowComponentReport" runat="server" 
                                                            onclick="btnShowComponentReport_Click"                                                              
                                                            >Component Report</asp:LinkButton>  
                                                    </li>			
                                                		                                                               
                                                </ul>
                                              </asp:Panel>
                                            </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnGet" EventName="Click" />
                                                </Triggers>
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
