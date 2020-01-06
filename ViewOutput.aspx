<%@ Page Language="c#" CodeBehind="ViewOutput.aspx.cs" AutoEventWireup="True" Inherits="NewOrderingSystem.ViewOutput" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AJAXCtrlToolKit" %>
<%@ Register Assembly="Utilities" Namespace="Utilities" TagPrefix="cc1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN"  "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
    <title>New Ordering System - View output</title>
    <%-- <script type='text/javascript' src='Styles/x.js'></script>
            <script type='text/javascript' src='Styles/xtableheaderfixed.js'></script>
            <script type='text/javascript'>
                xAddEventListener(window, 'load',function() { new xTableHeaderFixed('grViewOutput', 'table-container', 0); }, false);
            </script>--%>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="C#" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <link href="CSS/Style.css" type="text/css" rel="stylesheet" />
    <link href="./CSS/MenuButtonStyle.css" type="text/css" rel="stylesheet" />
    <link rel='stylesheet' type='text/css' href='Styles/StaticHeader.css' />

    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="Scripts/Common.js" type="text/javascript"></script>

    <style type="text/css">
        #tblInner, #tblGridHolder, #UpdatePanel1, #grViewOutput
        {
            height: 100%;
        }
    </style>

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

        function fnHide() {
            window.setTimeout("fnHide2()", 1740000);

        }

        function fnHide2() {
            window.focus();
            if (confirm('Your session going to expire in 60 sec. Do you want to continue?')) {
                window.document.forms(0).submit();
            }
            else {
                location.href = 'Login.aspx';
            }

        }

        var previousRow;
        var previousRowBC;
        function GetContract(RefNo, Mode, Status, Model, ContractNo, VersionNo, row) {


            document.Form1.HidRefNo.value = RefNo;
            document.Form1.HidMode.value = Mode;
            document.Form1.HidStatus.value = Status;
            document.Form1.HidModel.value = Model;
            document.Form1.HidContractNo.value = ContractNo;
            document.Form1.HidVersionNo.value = VersionNo;

            //alert("document.Form1.HidRefNo.value=" + document.Form1.HidRefNo.value);
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

        function IsItemSelected() {
            if (document.Form1.HidRefNo.value == "") {
                alert("Please Select an item for this action");
                return false;
            }
        }
        /*function display(imgName,imgUrl) 
        {	
        if (document.images && typeof imgUrl != 'undefined')
        document.Form1[imgName].src = imgUrl.src;			
        }*/

        function OpenWindowForCopy(url) {
            if (document.Form1.HidRefNo.value == "") {
                alert("Please Select an item for this action");
            }
            else {
                newwindow = window.open(url + "?RefNo=" + document.Form1.HidRefNo.value + "&ContractNo=" + document.Form1.HidContractNo.value, "CopyContract", "width=470,height=250,left=250,top=200,resizable=no,scrollbars=no");
                if (window.focus) { newwindow.focus(); }
            }
            return false;
        }
        function OpenNewWindow(OpenFor) {
            if (OpenFor == 'Compute')
                window.open('ComputationStatus.aspx', 'Compute', 'width=800,height=600');
        }
        function GoHome() {
            window.location = "Home.aspx";
        }

        function ViewReport(RptFor) {
            var sStatus = document.Form1.HidStatus.value;
            sStatus = sStatus.toUpperCase();


            if (document.Form1.HidRefNo.value == "") {
                alert("Please Select an item for this action");
            }
            else if (document.Form1.HidStatus.value == "Processing" || document.Form1.HidStatus.value == "JobQueue") {
                alert("Selected Item is in " + document.Form1.HidStatus.value + ", No Exception available... ");
            }
            else if (RptFor == 'Exception') {
                if (sStatus == 'EXCEPTION') {
                    window.open("ViewReports.aspx?ReportName=" + RptFor + "&StrSql=EXEC SP_GetTempException ''&RefNo="
		                                + document.Form1.HidRefNo.value, "RptPage",
		                                 "resizable=yes,scrollbars=yes,left=100,top=50,width=800,height=600");
                }
                else {
                    alert('Status should be "EXCEPTION" to view Exception Report');
                    return false;
                }
            }
            else if (RptFor == 'Output') {
                window.open("ViewReports.aspx?ReportName=" + RptFor + "&StrSql=EXEC SP_RptSummaryDetails 0,'All'" + "&RefNo=" + document.Form1.HidRefNo.value + "&IsPDF=false", "RptPage", "resizable=yes,status=yes,scrollbars=yes,left=100,top=50");
            }
            else if (RptFor == 'PDFOutput') {
                window.open("ViewReports.aspx?ReportName=" + RptFor + "&StrSql=EXEC SP_RptSummaryDetails 0,'All'" + "&RefNo=" + document.Form1.HidRefNo.value + "&IsPDF=true", "RptPage", "resizable=yes,status=yes,scrollbars=yes,left=100,top=50");
            }
            else if (RptFor == 'OutputList') {
                //window.open("KSetReport.aspx?ReportName=" + RptFor + "&StrSql=EXEC SP_RptKSetList " + document.Form1.HidRefNo.value + ",'All'&RefNo=" + document.Form1.HidRefNo.value + "&IsPDF=false", "RptPage", "resizable=yes,status=yes,scrollbars=yes,left=100,top=50");
                window.open("ViewReports.aspx?ReportName=" + RptFor + "&StrSql=EXEC SP_RptKSetList " + document.Form1.HidRefNo.value + ",'All','All'&RefNo=" + document.Form1.HidRefNo.value + "&IsPDF=false", "RptPage", "resizable=yes,status=yes,scrollbars=yes,left=100,top=50");
            }
            else if (RptFor == 'PDFOutputList') {
                window.open("ViewReports.aspx?ReportName=" + RptFor + "&StrSql=EXEC SP_RptKSetList " + document.Form1.HidRefNo.value + ",'All','All'&RefNo=" + document.Form1.HidRefNo.value + "&IsPDF=true", "RptPage", "resizable=yes,status=yes,scrollbars=yes,left=100,top=50");
            }
            else if (RptFor == 'Production') {
                window.open("ViewReports.aspx?ReportName=Production&SpName=EXEC SP_RptGetScreenTemplateForModel&Model=" + document.Form1.HidModel.value + "&ContractNo=" + document.Form1.HidContractNo.value, "RptPage", "resizable=yes,status=yes,scrollbars=yes,left=100,top=50,width=800,height=600");
            }
            else if (RptFor == 'CostReport') {

                if (sStatus == 'END' || sStatus == 'EXCEPTION' || sStatus == 'ACTIVATED') {
                    window.open("ViewReports.aspx?ReportName=" + RptFor + "&StrSql=EXEC SP_RptCostReport " + document.Form1.HidRefNo.value
		                                                    + "&RefNo=" + document.Form1.HidRefNo.value + "&IsPDF=false", "RptPage",
		                                                     "resizable=yes,status=yes,scrollbars=yes,left=100,top=50");
                }
                else {
                    alert('Status should be "END,EXCEPTION or ACTIVATED" to view Cost Report');
                    return false;
                }
            }
            else if (RptFor == 'ZeroCostReport') {

                if (sStatus == 'END' || sStatus == 'EXCEPTION' || sStatus == 'ACTIVATED') {
                    window.open("ViewReports.aspx?ReportName=" + RptFor + "&StrSql=EXEC SP_RptZeroCostReport " + document.Form1.HidRefNo.value
		                                                    + "&RefNo=" + document.Form1.HidRefNo.value + "&IsPDF=false", "RptPage",
		                                                     "resizable=yes,status=yes,scrollbars=yes,left=100,top=50");
                }
                else {
                    alert('Status should be "END,EXCEPTION or ACTIVATED" to view Zero Cost Report');
                    return false;
                }
            }
            else if (RptFor == 'CWTReport') {

                if (sStatus == 'END' || sStatus == 'EXCEPTION' || sStatus == 'ACTIVATED') {
                    window.open("ViewReports.aspx?ReportName=" + RptFor + "&StrSql=EXEC Rpt_CWTWeight " + document.Form1.HidRefNo.value
		                                                    + "&RefNo=" + document.Form1.HidRefNo.value + "&IsPDF=false", "RptPage",
		                                                     "resizable=yes,status=yes,scrollbars=yes,left=100,top=50");
                }
                else {
                    alert('Status should be "END,EXCEPTION or ACTIVATED" to view Cost Report');
                    return false;
                }
            }


            else if (RptFor == 'ExportReport') {

                if (sStatus == 'END' || sStatus == 'EXCEPTION' || sStatus == 'ACTIVATED') {
                    window.open("ReportExport.aspx?RefNo=" + document.Form1.HidRefNo.value, "ReportExport", "resizable=yes,status=yes,scrollbars=yes,left=100,top=50");
                }
                else {
                    alert('Status should be "END,EXCEPTION or ACTIVATED" to view Cost Report');
                    return false;
                }
            }


            if (RptFor == 'UpdateCost') {
                if (document.Form1.HidRefNo.value == "")
                    return false;
                else
                    return true;
            }

        }
        function OpenSignature() {
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
        }

        function CanProcess(ProcessFor) {
            if (document.Form1.HidRefNo.value == "") {
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
                //		         if (document.Form1.HidStatus.value == 'Saved') {
                //		             alert('"Saved" item can not compute from this screen, Compute from Main screen');
                //		             return false;
                //		         }
                //		         else 
                if (document.Form1.HidStatus.value == 'Processing' || document.Form1.HidStatus.value == 'JobQueue') {
                    alert('Contract is in ' + document.Form1.HidStatus.value + ' for Selected item, You cannot compute at this time');
                    return false;
                }
            }
            else if (ProcessFor == 'DELETE') {
                if (document.Form1.HidStatus.value == 'Processing') {
                    alert('Contract is in ' + document.Form1.HidStatus.value + ' for Selected item, You cannot Delete at this time');
                    return false;
                }
                else if (parseInt(document.Form1.HidVersionNo.value) >= 0) {

                    alert('Contract is Activated for Selected item, You cannot Delete at this time');
                    return false;

                }
                else {
                    return confirm('Do you want to delete this Contract?');
                }
            }
            else if (ProcessFor == 'RELEASE') {

                if (document.Form1.HidStatus.value.toUpperCase() != 'END') {
                    alert('Status should be "END" to Release');
                    return false;
                }
                return true;
            }
            else if (ProcessFor == 'ACTIVATE') {
                if (document.Form1.HidStatus.value != 'End') {
                    alert('Status should be "END" to Activate');
                    return false;
                }
                var retVal = confirm('New version will be generated for Order Release, Are you sure to activate?');
                //var retVal = confirm('Do you want to maintain the revision for K - Set Details ?');
                if (retVal == true) {
                    //window.location = "ActivationHistoryMaintain.aspx?RefNo=" + document.Form1.HidRefNo.value;
                    previousRow = '';
                    //return false;
                    return true;
                }
                else
                //return true;
                    return false;

            }
        }
        function display(imgName, imgUrl) {
            if (document.images && typeof imgUrl != 'undefined')
                document.Form1[imgName].src = imgUrl.src;
        }
        function ConfirmLogout() {
            if (confirm('Sure do you want to logout?'))
                location.href = "Logout.aspx";
        }
        function IsItemSelected() {
            try {
                if (document.Form1.HidRefNo.value == "") {
                    alert("Please Select an item for this action");
                    return false;
                }
                return true;
            } catch (e) {
            }
        }
        function ValidateActivate() {
            try {
                var sApprovedBy = document.Form1.txtApprovedBy.value;
                var sCheckedBy = document.Form1.txtCheckedBy.value;
                sApprovedBy = sApprovedBy.trim();
                sCheckedBy = sCheckedBy.trim();

                if (sApprovedBy == '' && sCheckedBy == '') {
                    alert("Approved By and Checked By Can't be empty, please enter valid data!");
                    return false;
                }
                else if (sApprovedBy == '') {
                    alert("Approved By Can't be empty, please enter valid data!");
                    return false;
                }
                else if (sCheckedBy == '') {
                    alert("Checked By Can't be empty, please enter valid data!");
                    return false;
                }
                else {
                    return confirm('Are you sure you want to activate?');
                }


            } catch (e) {
            }
        }

        function OnActivateComplete(sRefNo, sVersion) {
            window.location = "ActivationHistoryMaintain.aspx?RefNo=" + sRefNo + "&VersionNo=" + sVersion;
        }
        
    </script>

</head>
<body onload="fnHide()" style="margin-left: 0; margin-top: 0">
    <form id="Form1" name="Form1" method="post" runat="server">
    <div style="display: none;">
        <asp:LinkButton runat="server" ID="lbPrivacy" Text="PRIVACY" />
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <AJAXCtrlToolKit:ModalPopupExtender ID="mpeCompute" runat="server" PopupControlID="pnlComputeHolder"
        TargetControlID="lbPrivacy" CancelControlID="lnkBtnClose" BackgroundCssClass="Background">
    </AJAXCtrlToolKit:ModalPopupExtender>
    <asp:Panel ID="pnlComputeHolder" runat="server" CssClass="PopupActivate" align="center"
        Style="display: none; width: 700px">
        <table id="tblComputeHolder" style="width: 100%; height: 100%;">
            <tr>
                <td align="left" valign="top">
                    <div style="margin: 10px;">
                        <asp:Label ID="lblMaterialCategory" Style="font-weight: bold;" runat="server" Text="Material Category List"></asp:Label>
                        <br />
                        <input id="cbMatCatAll" type="checkbox" runat="server" onclick="SetAllCheckBoxListChecked('cbMatCat','cbMatCatAll');SetExcludeSetList('cbMatCat','cbMatCatAll','FULLSET');" />
                        Select All Material Category
                        <br />
                        <div id="dvMaterialCategory" style="height: 120px; font-size: 11px; width: 300px;
                            padding: 5px; overflow: auto; border: 1px solid #ccc;">
                            <asp:CheckBoxList ID="cbMatCat" runat="server" Style="font-size: 11px;" OnDataBound="cbMatCat_DataBound">
                            </asp:CheckBoxList>
                        </div>
                    </div>
                </td>
                <td align="left" valign="top">
                    <div style="margin: 10px;" id="dvExcludedSetList" disabled="true">
                        <asp:Label ID="lblExcludedSets" Style="font-weight: bold;" runat="server" Text="Excluded Set List"></asp:Label>
                        <br />
                        <input id="cbExcludedSetAll" type="checkbox" runat="server" onclick="SetAllCheckBoxListChecked('cbExcludedSet','cbExcludedSetAll');" />
                        Select All Set
                        <br />
                        <div id="dvExcludedSet" style="height: 120px; font-size: 11px; width: 300px; padding: 5px;
                            overflow: auto; border: 1px solid #ccc;">
                            <asp:CheckBoxList ID="cbExcludedSet" runat="server" Style="font-size: 11px; visibility: hidden"
                                OnDataBound="cbExcludedSet_DataBound">
                            </asp:CheckBoxList>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
        <asp:LinkButton ID="lnkBtnClose" CssClass="LinkButton" runat="server" Text="Close" />
        <asp:LinkButton ID="lnkBtnCompute" OnClientClick="return IsCheckBoxListItemSelected('cbMatCat','cbMatCatAll','Please select at least one Material Category to compute!');"
            CssClass="LinkButton" runat="server" Text="Compute" OnClick="lnkBtnCompute_Click" />
    </asp:Panel>
    
    
    <AJAXCtrlToolKit:ModalPopupExtender ID="mpeUpdateCost" runat="server" PopupControlID="pnlUCChooseLift"
        TargetControlID="lbPrivacy" CancelControlID="btnUCClose" BackgroundCssClass="Background">
    </AJAXCtrlToolKit:ModalPopupExtender>
    <asp:Panel ID="pnlUCChooseLift" runat="server" CssClass="PopupActivate" align="center"
        Style="display: none; width: 400px">
        <table id="tblUCChooseLift" style="width: 100%; height: 100%;">
            <tr>
                <td align="left" valign="top">
                    <div style="margin: 10px;">
                        <asp:Label ID="lblUCProjectInfo" Style="margin-bottom:10px;" runat="server" Text=""></asp:Label>
                        <br />
                        <input id="cbUCLiftsAll" type="checkbox" runat="server" onclick="SetAllCheckBoxListChecked('cbUCLifts','cbUCLiftsAll');SetUpdateCostLiftList('cbUCLifts','cbUCLiftsAll','ALL LIFTS');" />
                        Select All Lifts
                        <br />
                        <div id="Div1" style="height: 200px; font-size: 11px; width: 300px;
                            padding: 5px; overflow: auto; border: 1px solid #ccc;">
                            <asp:CheckBoxList ID="cbUCLifts" runat="server" Style="font-size: 11px;" OnDataBound="cbUCLifts_DataBound">
                            </asp:CheckBoxList>
                        </div>
                    </div>
                </td>                
            </tr>
        </table>
        <asp:LinkButton ID="btnUCClose" CssClass="LinkButton" runat="server" Text="Close" />
        <asp:LinkButton ID="lnkBtnUpdateCost" OnClientClick="return IsCheckBoxListItemSelected('cbUCLifts','cbUCLiftsAll','Please select at least one Material Category to update cost!');"
            CssClass="LinkButton" runat="server" Text="Update Cost" />
    </asp:Panel>
    
    
    <input id="HidContractNo" type="hidden" name="HidContractNo" runat="server" />
    <input id="HidRefNo" type="hidden" name="HidRefNo" runat="server" designtimedragdrop="710" />
    <input id="HidStatus" type="hidden" name="HidStatus" runat="server" />
    <input id="HidModel" type="hidden" name="HidModel" runat="server" />
    <input id="HidMode" type="hidden" name="HidMode" runat="server" />
    <input id="HidVersionNo" type="hidden" name="HidVersionNo" runat="server" />
    <input id="HidSearchText" type="hidden" name="HidSearchText" runat="server" />
    <table id="Table3" width="100%" style="border: thin solid #3399FF; width: 100%; height: 100%;">
        <tr style="height: 100px; border-bottom-width: 1px; border-bottom-color: #0000FF;">
            <td>
                <table id="Table2" style="height: 100%;" cellspacing="0" cellpadding="0" width="100%"
                    border="0">
                    <tr valign="bottom">
                        <td style="background-position: center center; width: 30%; background-repeat: no-repeat;
                            border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #3399FF;"
                            align="left" valign="middle">
                            <asp:Image ID="Image2" runat="server" Height="56px" ImageUrl="~/Images/logo.gif"
                                Width="201px" />
                        </td>
                        <td align="left" valign="middle" style="border-bottom-style: solid; border-bottom-width: thin;
                            border-bottom-color: #3399FF;">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Header.png" Width="613px" />
                            <asp:Label ID="lblVersion" runat="server" CssClass="labelVersion"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 20px; background-color: #3399FF;">
            <td>
                <asp:Label ID="lblUserName" runat="server" CssClass="usernamelabel" ForeColor="#FFFFCC"></asp:Label>
            </td>
        </tr>
        <tr valign="top" style="height: 100%;">
            <td style="width: 100%; height: 100%;">
                <table id="tblInner" style="width: 100%; height: 100%;">
                    <tr valign="top">
                        <td style="width: 15%; background-color: #3399FF;" align="center">
                            <br />
                            <a onmouseover="display('Home_img', Home_over);" onmouseout="display('Home_img', Home_out);"
                                href="Home.aspx" onfocus="display('Home_img', Home_over);" onblur="display('Home_img', Home_out);"
                                tabindex="1">
                                <img src="Images/homeNew.png" border="0" name="Home_img" /></a><br />
                            <br />
                            <a onmouseover="display('New_img', New_over);" onmouseout="display('New_img', New_out);"
                                href="MainNewScreen.aspx?Mode=New" onfocus="display('New_img', New_over);" onblur="display('New_img', New_out);"
                                tabindex="2">
                                <img alt="" src="Images/newNew.png" border="0" name="New_img" /></a>
                            <br />
                            <br />
                            <a onmouseover="display('Viewoutput_img', Viewoutput_out);" onmouseout="display('Viewoutput_img',Viewoutput_over );"
                                href="ViewOutput.aspx" onfocus="display('Viewoutput_img', Viewoutput_out);" onblur="display('Viewoutput_img',Viewoutput_over );"
                                tabindex="3">
                                <img alt="" src="Images/outputNew-h.png" border="0" name="Viewoutput_img" /></a>
                            <br />
                            <br />
                            <a onmouseover="display('IssuePO_img', IssuePO_over);" onmouseout="display('IssuePO_img', IssuePO_out);"
                                href="IssuePO.aspx" onfocus="display('IssuePO_img', IssuePO_over);" onblur="display('IssuePO_img', IssuePO_out);"
                                tabindex="4">
                                <img alt="" src="Images/IssuePO.png" border="0" name="IssuePO_img" /></a>
                            <br />
                            <br />
                            <a onmouseover="display('Logout_img', Logout_over);" onclick="ConfirmLogout();" onmouseout="display('Logout_img', Logout_out);"
                                onfocus="display('Logout_img', Logout_over);" onblur="display('Logout_img', Logout_out);"
                                tabindex="5">
                                <img alt="" src="Images/LogoutNew.png" border="0" name="Logout_img" /></a>
                        </td>
                        <td valign="top" style="width: 85%">
                            <table id="tblGridHolder" style="width: 100%; height: 100%">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:Label ID="lblError" runat="server" CssClass="labelerror"></asp:Label>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="9pt">Contract No</asp:Label>
                                        <asp:TextBox ID="txtSe" runat="server"></asp:TextBox>
                                        <asp:LinkButton ID="btnSearch" runat="server" CssClass="LinkButton" Text="Search"
                                            OnClick="btnSearch_Click">
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr style="height: 100%">
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:Panel ID="pnlWBS" runat="server" Style="height: 100%;">
                                                    <asp:GridView ID="grViewOutput" AutoGenerateColumns="False" runat="server" OnRowDataBound="grViewOutput_RowDataBound"
                                                        CellPadding="4" ForeColor="#333333" GridLines="None" BorderColor="#507CD1" BorderStyle="Solid"
                                                        BorderWidth="1px" PageSize="18" OnPageIndexChanging="grViewOutput_PageIndexChanging"
                                                        CssClass="grViewOutput" AllowPaging="True" AllowSorting="True">
                                                        <RowStyle BackColor="#EFF3FB" Font-Size="8pt" Height="15px" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Select">
                                                                <ItemTemplate>
                                                                    <cc1:GridViewRowSelector ID="RowSel1" runat="server" />
                                                                </ItemTemplate>
                                                                <ControlStyle Font-Size="8pt" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField Visible="False" HeaderText="RefNo" DataField="RefNo" />
                                                            <asp:BoundField HeaderText="Contract No" DataField="ContractNo" />
                                                            <asp:BoundField DataField="ModelType" HeaderText="Model" />
                                                            <asp:BoundField HeaderText="Last Modified By" DataField="LastModifiedBy" />
                                                            <asp:BoundField HeaderText="Last Modified Date" DataField="LastModifiedDate" />
                                                            <asp:BoundField HeaderText="Status" DataField="Status" />
                                                            <asp:BoundField Visible="False" HeaderText="Mode" DataField="Mode" />
                                                            <asp:BoundField Visible="False" HeaderText="Model Type" DataField="ModelType" />
                                                            <asp:BoundField DataField="LastComputeDate" HeaderText="Last Compute Date" />
                                                            <asp:BoundField Visible="False" DataField="VersionNo" HeaderText="Version No" />
                                                            <asp:BoundField Visible="False" DataField="VendorNameList" HeaderText="Released Vendor Names" />
                                                            <asp:BoundField Visible="False" DataField="ReleaseStatus" HeaderText="Released Status" />
                                                            <asp:BoundField HeaderText="K-Set PDF Generation" DataField="KSetDetails" />
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
                                                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="btnRefresh" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <ul id="navPnlOutput" class="NavMain">
                                            <li>
                                                <asp:LinkButton ID="btnCompute" runat="server" OnClick="btnCompute_Click">Compute</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="btnChangeInputData" runat="server" OnClick="btnChangeInputData_Click">Change Input Data</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="btnViewInputDataH" runat="server" OnClientClick="ViewReport('Production');return false;">View Input Data</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click">Delete</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="btnCopy" runat="server" OnClick="btnCopy_Click">Copy</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="btnAct" Visible="false" runat="server" OnClick="btnAct_Click">Activate</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="btnRelease" runat="server" OnClick="btnRelease_Click">Release</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="btnMaintainZeroCost" runat="server" OnClick="btnMaintainZeroCost_Click">Maintain Zero Cost\Change Component</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click">Refresh</asp:LinkButton>
                                            </li>
                                        </ul>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <ul id="navPnlOutputReport" class="NavMain">
                                            <li>
                                                <asp:LinkButton ID="btnOutput" runat="server" OnClientClick="ViewReport('Output');return false;">K-Set Details</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="btnOutputPDF" runat="server" OnClientClick="ViewReport('PDFOutput');return false;">PDF K-Set Details</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="btnOutputList" runat="server" OnClientClick="ViewReport('OutputList');return false;">K-SetListing</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="btnOutputListPDF" runat="server" OnClientClick="ViewReport('PDFOutputList');return false;">PDF K-Set Listing</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="btnCostReport" runat="server" OnClientClick="ViewReport('CostReport');return false;">Cost Report</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="btnException" runat="server" OnClientClick="ViewReport('Exception');return false;">View Exception</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="btnZeroCostReport" runat="server" OnClientClick="ViewReport('ZeroCostReport');return false;">Zero Cost Report</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="btnCWTReport" runat="server" OnClientClick="ViewReport('CWTReport');return false;">CWT Weight Report</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="btnExportReport" runat="server" OnClientClick="ViewReport('ExportReport');return false;">Export Report</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="btnUpdateCost" runat="server" OnClientClick="return ViewReport('UpdateCost');">Update Cost</asp:LinkButton>
                                            </li>
                                        </ul>
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
