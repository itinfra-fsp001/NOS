<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZeroCost.aspx.cs" Inherits="NewOrderingSystem.ZeroCost" %>
<%@ Register Assembly="Utilities" Namespace="Utilities" TagPrefix="cc1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN"  "http://www.w3.org/TR/html4/loose.dtd">

<html>
<head>
		<title>New Ordering System - ZeroCost</title>
		    
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="C#" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta http-equiv="X-UA-Compatible" content="IE=Edge" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		<link href="CSS/Style.css" type="text/css" rel="stylesheet"  />
		<link href="./CSS/MenuButtonStyle.css" type="text/css" rel="stylesheet"/>
		<link rel='stylesheet' type='text/css' href='Styles/StaticHeader.css' />
		<script type='text/javascript' src='Styles/x.js'></script>
        <script type='text/javascript' src='Styles/xtableheaderfixed.js'></script>
        <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
        <script type='text/javascript'>
            xAddEventListener(window, 'load', function() { new xTableHeaderFixed('grEx', 'divContainer', 0); }, false);
        </script>
		<script type='text/javascript' language="javascript">

		    $(function() {
		        try {
		            $(document).keydown(function(e) {
		                document.Form1.hidShiftKey.value = e.shiftKey;
		                document.Form1.hidCtrlKey.value = e.ctrlKey;
		                document.Form1.hidAltKey.value = e.altKey;
		            });

		            $(document).keyup(function(e) {
		                document.Form1.hidShiftKey.value = false;
		                document.Form1.hidCtrlKey.value = false;
		                document.Form1.hidAltKey.value = false;
		            });
		        } catch (e) { }
		    }
            );
            
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
		    var alRowIndex = new Array();
		    function fnHide() {
		        window.setTimeout("fnHide2()", 1740000);
		        document.Form1.hidRowIndex.value = "";
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

		    function GetContract(RefNo, Mode, Status, Model, ContractNo) {
		        document.Form1.HidRefNo.value = RefNo;
		        document.Form1.HidMode.value = Mode;
		        document.Form1.HidStatus.value = Status;
		        document.Form1.HidModel.value = Model;
		        document.Form1.HidContractNo.value = ContractNo;
		    }

		    function IsItemSelected() {
		        if (document.Form1.HidRefNo.value == "") {
		            alert("Please Select an item for this action");
		            return false;
		        }
		    }
		    function GoHome() {
		        window.location = "Home.aspx";
		    }
		    /*function display(imgName,imgUrl) 
		    {	
		    if (document.images && typeof imgUrl != 'undefined')
		    document.Form1[imgName].src = imgUrl.src;			
		    }
		 
		function OpenWindowForCopy(url)
		    {	
		    if (document.Form1.HidRefNo.value=="")
		    {				
		    alert ("Please Select an item for this action");							
		    }
		    else
		    {
		    newwindow=window.open(url+"?RefNo="+document.Form1.HidRefNo.value+"&ContractNo="+document.Form1.HidContractNo.value,"CopyContract","width=470,height=250,left=250,top=200,resizable=no,scrollbars=no");
		    if (window.focus) {newwindow.focus();}						
		    }
		    return false;
		    }	
		    function OpenNewWindow(OpenFor)
		    {			
		    if(OpenFor=='Compute')
		    window.open('ComputationStatus.aspx','Compute','width=800,height=600');				
		    }
		 
		 
		    function ViewReport(RptFor)
		    {
		    if (document.Form1.HidRefNo.value == "") {
		    alert("Please Select an item for this action");
		    }
		    else if (document.Form1.HidStatus.value == "Processing" || document.Form1.HidStatus.value == "JobQueue") {
		    alert("Selected Item is in " + document.Form1.HidStatus.value + ", No Exception available... ");
		    }
		    else if (RptFor == 'Exception') {
		    window.open("ViewReports.aspx?ReportName=" + RptFor + "&StrSql=EXEC SP_GetTempException ''&RefNo=" + document.Form1.HidRefNo.value, "RptPage", "resizable=yes,scrollbars=yes,left=100,top=50,width=800,height=600");
		    }
		    else if (RptFor == 'Output') {
		    window.open("ViewReports.aspx?ReportName=" + RptFor + "&StrSql=EXEC SP_RptSummaryDetails 0,'All'" + "&RefNo=" + document.Form1.HidRefNo.value + "&IsPDF=false", "RptPage", "resizable=yes,status=yes,scrollbars=yes,left=100,top=50");
		    }
		    else if (RptFor == 'PDFOutput') {
		    window.open("ViewReports.aspx?ReportName=" + RptFor + "&StrSql=EXEC SP_RptSummaryDetails 0,'All'" + "&RefNo=" + document.Form1.HidRefNo.value + "&IsPDF=true", "RptPage", "resizable=yes,status=yes,scrollbars=yes,left=100,top=50");
		    }
		    else if (RptFor == 'OutputList') {
		    //window.open("KSetReport.aspx?ReportName=" + RptFor + "&StrSql=EXEC SP_RptKSetList " + document.Form1.HidRefNo.value + ",'All'&RefNo=" + document.Form1.HidRefNo.value + "&IsPDF=false", "RptPage", "resizable=yes,status=yes,scrollbars=yes,left=100,top=50");
		    window.open("ViewReports.aspx?ReportName=" + RptFor + "&StrSql=EXEC SP_RptKSetList " + document.Form1.HidRefNo.value + ",'All'&RefNo=" + document.Form1.HidRefNo.value + "&IsPDF=false", "RptPage", "resizable=yes,status=yes,scrollbars=yes,left=100,top=50");
		    }
		    else if (RptFor == 'PDFOutputList') {
		    window.open("ViewReports.aspx?ReportName=" + RptFor + "&StrSql=EXEC SP_RptKSetList " + document.Form1.HidRefNo.value + ",'All'&RefNo=" + document.Form1.HidRefNo.value + "&IsPDF=true", "RptPage", "resizable=yes,status=yes,scrollbars=yes,left=100,top=50");
		    }
		    else if (RptFor == 'Production') {
		    window.open("ViewReports.aspx?ReportName=Production&SpName=EXEC SP_RptGetScreenTemplateForModel&Model=" + document.Form1.HidModel.value + "&ContractNo=" + document.Form1.HidContractNo.value, "RptPage", "resizable=yes,status=yes,scrollbars=yes,left=100,top=50,width=800,height=600");
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
		    if (document.Form1.HidStatus.value != 'END') {
		    alert('Status should be "END" to release');
		    return false;
		    }		        
		    }
		    }*/
		    function display(imgName, imgUrl) {
		        if (document.images && typeof imgUrl != 'undefined')
		            document.Form1[imgName].src = imgUrl.src;
		    }
		    function ConfirmLogout() {
		        if (confirm('Sure do you want to logout?'))
		            location.href = "Logout.aspx";
		    }
		    function SelectAllVendor() {
		        try {
		            var tab = document.getElementById('cbVL');
		            var cbAll = document.getElementById('cbAV');
		            var checkboxes = tab.getElementsByTagName("input");
		            for (i = 0; i < checkboxes.length; i++)
		                checkboxes[i].checked = cbAll.checked;
		        } catch (e) {
		           
		        }
		    }
		    //Added on 15/Sep/2014 - for MaterialCategory
		    function ValidateVendorOnSelect() {
		        try {
		            var tab = document.getElementById('cbVL');
		            var cbAll = document.getElementById('cbAV');
		            var checkboxes = tab.getElementsByTagName("input");
		            var bAllChecked = true;
		            for (i = 0; i < checkboxes.length; i++) {
		                if (!checkboxes[i].checked) {
		                    bAllChecked = false;
		                    break;
		                }
		            }
		            cbAll.checked = bAllChecked;
		        } catch (e) {  }
		    }
		    function SelectAllMatCat(sCheckBoxListID, sSelectAllID) {
		        try {
		            
		            var cbAll = document.getElementById(sSelectAllID);
		            var checkboxes = document.getElementById(sCheckBoxListID).getElementsByTagName("input");
		            for (i = 0; i < checkboxes.length; i++)
		                checkboxes[i].checked = cbAll.checked;
		        } catch (e) { }
		    }
		    function SelectAllPrjGrpItem(sCheckBoxListID, sSelectAllID) {
		        try {
		            
		            var cbAll = document.getElementById(sSelectAllID);
		            var checkboxes = document.getElementById(sCheckBoxListID).getElementsByTagName("input");
		            for (i = 0; i < checkboxes.length; i++)
		                checkboxes[i].checked = cbAll.checked;
		        } catch (e) { }
		    }
		    function ValidateCheckBoxListOnSelect(sCheckBoxListID, sSelectAllID) {
		        try {
		            var cbAll = document.getElementById(sSelectAllID);
		            var checkboxes = document.getElementById(sCheckBoxListID).getElementsByTagName("input");
		            var bAllChecked = true;
		            for (i = 0; i < checkboxes.length; i++)
		                if (!checkboxes[i].checked) {
		                bAllChecked = false;
		                break;
		            }
		            cbAll.checked = bAllChecked;
		        } catch (e) { }

		    }
		    function ValidateProjectNames() {
		        var bRetVal = false;
		        var sCheckBoxListID = "cbPrjGrpItem";
		        var sSelectAllID = "cbPrjGrpItemAll";
		        var cbAll = document.getElementById(sSelectAllID);
		        var checkboxes = document.getElementById(sCheckBoxListID).getElementsByTagName("input");
		        if (cbAll.checked) {
		            bRetVal = true;
		        }
		        else {
		            for (i = 0; i < checkboxes.length; i++)
		                if (checkboxes[i].checked) {
		                    bRetVal = true;
		                    break;
		            }
		        }
		        if (!bRetVal)
		            alert("Please select at least one project name to release!");
		        return bRetVal;
		    }
		    function AddRowIndex(nRowIdx) {
		        try {
		            
		            if ($.inArray(nRowIdx, alRowIndex) < 0)
		                alRowIndex.push(nRowIdx);
		            document.Form1.hidRowIndex.value = alRowIndex.join("|");   
		            //alert(alRowIndex.join("|"));
		        } catch (e) {
		            alert(e.message);
		        }
		    }
		    //Restrict the user to key-in only decimal numbers
		    function IsDecimal(evt,nRowIdx) {
		        try {
		            var charCode = (evt.which) ? evt.which : event.keyCode

		            if (charCode == 46) {
		                var inputValue = evt.value;
		                if (inputValue.indexOf('.') < 1) {
		                    {
		                        AddRowIndex(nRowIdx);
		                        return true;
		                    }
		                }
		                return false;
		            }
		            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
		                return false;
		            }
		            AddRowIndex(nRowIdx);
		            
		            return true;
		        } catch (e) {}
		    }
		    function IsNumeric(evt, nRowIdx) {
		        try {
		            var charCode = (evt.which) ? evt.which : event.keyCode
		            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
		                return false;
		            }
		            AddRowIndex(nRowIdx);
		            return true;
		        } catch (e) { }
		    }
		    function IsDecimalcPaste(evt, nRowIdx) {
		        try {
		            var ObjContent = { obj: evt, value: evt.value };
		            setTimeout(function() {
		                try {
		                    if (isNaN(ObjContent.obj.value)) {
		                        ObjContent.obj.value = ObjContent.value;
		                    }
		                    else
		                        AddRowIndex(nRowIdx);
		                } catch (e) { }
		            }, 10);

		        } catch (e) { }
		        return true;

		    }
		    
		    function IsNumericPaste(evt, nRowIdx) {
		        try {
		            var ObjContent = { obj: evt, value: evt.value };
		            setTimeout(function() {
		                try {
		                    if (isNaN(ObjContent.obj.value)) {
		                        ObjContent.obj.value = ObjContent.value;		                        
		                    }
		                    else {
		                        if (ObjContent.obj.value.indexOf('.') >= 1) {
		                            ObjContent.obj.value = ObjContent.value;
		                        }
		                        else
		                            AddRowIndex(nRowIdx);

		                    }
		                } catch (e) { }
		            }, 10);

		        } catch (e) { }
		        return true;

		    }
		     function IsValueChange(evt, nRowIdx) {
		        try {
		          AddRowIndex(nRowIdx);
		            return true;
		        } catch (e) { }
		    }
		      function IsValueChangePaste(evt, nRowIdx) {
		        try {
		            var ObjContent = { obj: evt, value: evt.value };
		            setTimeout(function() {
		                try {
		                    AddRowIndex(nRowIdx);

		                    
		                } catch (e) { }
		            }, 10);

		        } catch (e) { }
		        return true;

		    }
        </script>
	    <style type="text/css">
            .style1
            {
                height: 100%;
            }
        </style>
	    </head>
	<body onload="fnHide()">
        <form id="Form1" name="Form1" method="post" runat="server">
        
        <input id="HidContractNo" type="hidden" name="HidContractNo" runat="server" />
        <input id="HidRefNo" type="hidden" name="HidRefNo" runat="server" designtimedragdrop="710" />
        <input id="HidStatus" type="hidden" name="HidStatus" runat="server" />
        <input id="HidModel" type="hidden" name="HidModel" runat="server" />
        <input id="HidMode" type="hidden" name="HidMode" runat="server" />
        <input id="hidRowIndex" type="hidden" name="hidRowIndex" runat="server" />
        <input id="hidShiftKey" type="hidden" name="hidShiftKey" runat="server" />
        <input id="hidCtrlKey" type="hidden" name="hidCtrlKey" runat="server" />
        <input id="hidAltKey" type="hidden" name="hidAltKey" runat="server" />
        <table id="Table3" cellspacing="1" cellpadding="1" border="0" style="border: thin solid #3399FF;
            width: 100%; height: 100%;">
            <tr style="height: 100px; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #0000FF;">
                <td >
                    <table id="Table2" cellspacing="0" cellpadding="0" width="100%;" class="style1" style="border-style: none">
                        <tr valign="bottom">
                            <td style="background-position: center center; width: 30%; background-repeat: no-repeat;
                                border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #3399FF;"
                                align="left" valign="middle">
                                <asp:Image ID="Image2" runat="server" Height="56px" ImageUrl="~/Images/logo.gif"
                                    Width="201px" />
                            </td>
                            <td align="left" valign="middle" style="border-bottom-style: solid; border-bottom-width: thin;
                                border-bottom-color: #3399FF; width: 70%;">
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Header.png" Width="613px" />
                                <asp:Label ID="lblVersion" runat="server" CssClass="labelVersion"  ></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="border-style: none; height: 20px;background-color: #3399FF;">
                <td>
                    <asp:Label ID="lblUserName" runat="server" CssClass="usernamelabel" ForeColor="#FFFFCC"></asp:Label>
                </td>
            </tr>
            <tr style="border-style: none;height:100%;">
                <td style="width:100%;height: 100%;">
                    <table id="Table4" cellspacing="1" cellpadding="1" border="0" style="border: thin solid #3399FF;
                        width: 100%; height: 100%;">
                        <tr valign="top" style="width:100%;height: 100%;">
                            <td style="background-color: #3399FF; width: 15%;height: 100%;" valign="top" align="center">
                                <table width="100%" style="width:100%;height: 100%;">
                                    <tr valign="top">
                                        <td style="width: 15%; background-color: #3399FF; height: 100%" align="center">
                                            <br />
                                            <a onmouseover="display('Home_img', Home_over);" onmouseout="display('Home_img', Home_out);"
                                                href="Home.aspx" onfocus="display('Home_img', Home_over);" onblur="display('Home_img', Home_out);" tabindex="1">
                                                <img alt="" src="Images/homeNew.png" border="0" name="Home_img" /></a><br />
                                            <br />
                                            <a onmouseover="display('New_img', New_over);" onmouseout="display('New_img', New_out);"
                                                href="MainNewScreen.aspx?Mode=New" onfocus="display('New_img', New_over);" onblur="display('New_img', New_out);" tabindex="2">
                                                <img alt="" src="Images/newNew.png" border="0" name="New_img" /></a><br />
                                            <br />
                                            <a onmouseover="display('Viewoutput_img', Viewoutput_over);" onmouseout="display('Viewoutput_img', Viewoutput_out);"
                                                href="ViewOutput.aspx" onfocus="display('Viewoutput_img', Viewoutput_over);" onblur="display('Viewoutput_img', Viewoutput_out);" tabindex="3">
                                                <img alt="" src="Images/outputNew.png" border="0" name="Viewoutput_img" /></a><br />
                                            <br />
                                            <a onmouseover="display('IssuePO_img', IssuePO_over);" onmouseout="display('IssuePO_img',IssuePO_out );"
                                                href="IssuePO.aspx" onfocus="display('IssuePO_img', IssuePO_over);" onblur="display('IssuePO_img',IssuePO_out );" tabindex="4">
                                                <img alt="" src="Images/IssuePO.png" border="0" name="IssuePO_img" /></a><br />
                                            <br />
                                            <a onmouseover="display('Logout_img', Logout_over);" onclick="ConfirmLogout();" onmouseout="display('Logout_img', Logout_out);"
                                                onfocus="display('Logout_img', Logout_over);" onblur="display('Logout_img', Logout_out);" tabindex="5" >
                                                <img alt="" src="Images/LogoutNew.png" border="0" name="Logout_img" /></a>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top" style="width: 85%;height:100%">
                                <table style="width:100%;height:100%">
                                    <tr valign="top">
                                        <td  >
                                            <asp:Label ID="lblError" runat="server" CssClass="labelerror"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr valign="top" style="height:100%">
                                        <td >
                                            <table bgcolor="#EBEBD8">
                                                <tr>
                                                    <td colspan="2" style="font-weight: bold;">
                                                        <span >Contract No : &nbsp;</span><asp:Label ID="lblCN" runat="server" ForeColor="Green" Text="Label"></asp:Label>
                                                    </td>                                                    
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="font-weight: bold;">
                                                        <div id="dvVersionNo" runat="server">
                                                            <span lang="en-us">Version No :&nbsp;</span><asp:Label ID="lblVNo" runat="server" ForeColor="Green" Text="Label"></asp:Label>
                                                        </div>
                                                        <div id="dvProjectNo" runat="server">
                                                            <span lang="en-us">Project No :&nbsp;</span><asp:Label ID="lblPrjNo" runat="server" ForeColor="Green" Text=""></asp:Label>
                                                        </div>
                                                    </td>                                                    
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblVendorList" runat="server" style="font-weight: bold;" Text="Vendor List"></asp:Label>
                                                        <br />
                                                        <input id="cbAV" type="checkbox" runat="server"   onclick="SelectAllVendor();" />
                                                        Select All Vendor
                                                        <br />
                                                        <div  style="height: 120px;font-size: 11px; width: 250px; padding: 5px; overflow: auto; border: 1px solid #ccc;">
                                                            <asp:CheckBoxList ID="cbVL" runat="server"  style="font-size: 11px;" OnDataBound="cbVL_DataBound">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblMaterialCategory" style="font-weight: bold;" runat="server" Text="Material Category List"></asp:Label>
                                                        <br />
                                                        <input id="cbMatCatAll" type="checkbox"   runat="server" onclick="SelectAllMatCat('cbMatCat','cbMatCatAll');" />
                                                        Select All Material Category
                                                        <br />
                                                        <div id="dvMaterialCategory"  style="height: 120px;font-size: 11px; width: 300px; padding: 5px; overflow: auto; border: 1px solid #ccc;">
                                                            <asp:CheckBoxList ID="cbMatCat"    runat="server" style="font-size: 11px;" OnDataBound="cbMatCat_DataBound">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                        
                                                    </td>                                                    
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <br />
                                                            <asp:LinkButton ID="btnMntExp" runat="server" CssClass="LinkButton" Text="Maintain Zero Cost Exception"
                                                                OnClick="btnMntExp_Click" />
                                                                
                                                                 <asp:LinkButton ID="btnChangeComp" runat="server" 
                                                            CssClass="LinkButton" Text="Change Componet" onclick="btnChangeComp_Click"
                                                                 />
                                                       
                                                    </td>
                                                </tr>
                                                
                                                
                                                <tr>
                                                    <td colspan="2">&nbsp;<br /> </td>
                                                </tr> 
                                                 <tr>
                                                    <td colspan="2">
                                                    <asp:Label ID="lblHeading" runat="server" Visible="False" BackColor="BlanchedAlmond" ForeColor=Black Font-Bold="true" Font-Size="Large" ></asp:Label>
                                                     </td>
                                                </tr>
                                            </table>
                                        </td>                                        
                                    </tr>
                                    <tr>
                                        <td >
                                            <asp:Panel ID="pnlEx" runat="server" Style="height: 340px;" ScrollBars="Auto" Visible="False"
                                                BorderStyle="None">
                                                <div id='divContainer' class="table-container12" style="border-style: none;width:99%;overflow:auto" >
                                                    <asp:GridView ID="grEx" AutoGenerateColumns="False" runat="server" 
                                                        CellPadding="2" BorderColor="#CCCCCC" BorderStyle="None"
                                                        BorderWidth="1px" PageSize="20" OnRowDataBound="grEx_RowDataBound" OnPreRender="grEx_PreRender"
                                                         DataKeyNames="RefNo,NodeID,FunctionID,CurrencyID,VendorType,CablesRopeQty" BackColor="White">
                                                        <RowStyle Font-Size="8pt" ForeColor="#000066" />
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Vendor Name" DataField="VendorName" />
                                                            <asp:BoundField HeaderText="Material Category" DataField="MaterialCategoryName" />                                                            
                                                            <asp:BoundField HeaderText="Set Name" DataField="SetName" />                                                            
                                                            <asp:BoundField HeaderText="Component Name" DataField="ComponentName" />
                                                            <%--<asp:BoundField HeaderText="Component No." DataField="ComponentNo" />--%>
                                                             <asp:TemplateField HeaderText="Component No." HeaderStyle-Width="300px" >
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtComponentNo" Width="300px" MaxLength="100" Font-Size="Small" ForeColor="Black" runat="server"></asp:TextBox>                                                                    
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Drawing No." HeaderStyle-Width="150px" >
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtDrawingNo" Width="150px" MaxLength="50" Font-Size="Small" ForeColor="Black" runat="server"></asp:TextBox>                                                                    
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Rev No." HeaderStyle-Width="50px" >
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtRevNo" Width="50px" MaxLength="10" Font-Size="Small" ForeColor="Black" runat="server"></asp:TextBox>                                                                    
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="DirectMtl" HeaderStyle-Width="70px"  >
                                                                <ItemTemplate >
                                                                    <asp:TextBox ID="txtDirectMtl"  Width="70px" runat="server"  ></asp:TextBox>
                                                                      <asp:Label ID="lblDirectMtl"  Width="70px" runat="server"  ></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Labor" HeaderStyle-Width="70px" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtLabor" Width="70px"  runat="server"></asp:TextBox>                                                                    
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Subcon" HeaderStyle-Width="70px" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtSubcon" Width="70px"  runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Subcon Batam" HeaderStyle-Width="70px" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtSubconBatam" Width="70px"  runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="DMFJ" HeaderStyle-Width="70px" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtDMFJ" Width="70px"  runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>  
                                                            <asp:TemplateField HeaderText="Qty." HeaderStyle-Width="30px" >
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtQty" Width="30px"  runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>  
                                                             <asp:BoundField DataField="NO" HeaderText="NO." HeaderStyle-Width="50px"  Visible="True" />                                                            
                                                            <asp:BoundField HeaderText="Currency" DataField="CurrencyID" />                                                                                                                       
                                                            <asp:BoundField  DataField="VendorType" HeaderText="VendorType"  Visible="False" />                                                                                                                      
                                                            <asp:BoundField DataField="RefNo" HeaderText="RefNo" Visible="False" />
                                                            <asp:BoundField DataField="NodeID" HeaderText="NodeID" Visible="False" />
                                                            <asp:BoundField DataField="FunctionID" HeaderText="FunctionID" Visible="False" />
                                                             <asp:BoundField DataField="CablesRopeQty" HeaderText="CablesRopeQty" HeaderStyle-Width="50px"  Visible="False" />  
                                                           
                                                        </Columns>
                                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                        <AlternatingRowStyle BackColor="#EBEBD8" />
                                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" 
                                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px"/>
                                                    </asp:GridView>
                                                </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>                                    
                                    <tr>
                                        <td><br />
                                            <ul id="navPnlBtn" class="NavMain" visible="False" runat="server">
                                                <li>
                                                    <asp:LinkButton ID="btnCan" runat="server" CssClass="LinkButton" Text="Cancel" 
                                                        OnClick="btnCan_Click" />
                                                </li>
                                                <li>
                                                    <asp:LinkButton ID="btnSav" runat="server" CssClass="LinkButton" Text="Save" OnClick="btnSav_Click"
                                                         />
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
