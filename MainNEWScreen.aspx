<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainNEWScreen.aspx.cs" Inherits="NewOrderingSystem.MainNEWScreen" culture="auto" uiculture="auto" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AJAXCtrlToolKit" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
   		<title>New Ordering System - MainScreen</title>
   		 <meta http-equiv="X-UA-Compatible" content="IE=edge" />		
   		 <script type='text/javascript' src='Styles/x.js'></script>
  <%--          <script type='text/javascript' src='Styles/xtableheaderfixed.js'></script>         
            <script type='text/javascript'>
                //xAddEventListener(window, 'load', function() { new xTableHeaderFixed('grScreenTemplate', 'divContainerMain', 0); }, false);
                xAddEventListener(window, 'load', function() { new xTableHeaderFixed('grModel', 'divContainerModel', 0); }, false);
                xAddEventListener(window, 'load', function() { new xTableHeaderFixed('grTemplate02', 'divContainer02', 0); }, false);
                xAddEventListener(window, 'load', function() { new xTableHeaderFixed('grTemplate03', 'divContainer03', 0); }, false);
                xAddEventListener(window, 'load', function() { new xTableHeaderFixed('grTemplate04', 'divContainer04', 0); }, false);
                xAddEventListener(window, 'load', function() { new xTableHeaderFixed('grTemplate05', 'divContainer05', 0); }, false);
                xAddEventListener(window, 'load', function() { new xTableHeaderFixed('grTemplate06', 'divContainer06', 0); }, false);
                xAddEventListener(window, 'load', function() { new xTableHeaderFixed('grTemplate07', 'divContainer07', 0); }, false);
           </script>   --%>
          <%--<script type="text/javascript">
              //http://go4answers.webhost4life.com/Example/maintain-scroll-position-postback-159338.aspx maintain scroll in panel
              var hid;
              var scrop;
              function Pageload() {
                  hid = document.getElementById("HiddenField1");

                  scrop = document.getElementById("pnlModel");                 
                  if (scrop == null)
                      scrop = document.getElementById("pnlTemplate02");
                  if (scrop == null)
                      scrop = document.getElementById("pnlTemplate03");
                  if (scrop == null)
                      scrop = document.getElementById("pnlTemplate04");
                  if (scrop == null)
                      scrop = document.getElementById("pnlTemplate05");
                  if (scrop == null)
                      scrop = document.getElementById("pnlTemplate06");
                  if (scrop == null)
                      scrop = document.getElementById("pnlTemplate07");
                  
                  hid.value = hid.value;
              }
              
              function SetPosition() {
                  if (scrop != null) {
                      hid.value = scrop.scrollTop;
                  }
              }
              
              function GetPosition() {
                  if (scrop != null) {
                      scrop.scrollTop = hid.value;
                  }
              }   
        </script>--%>

        <link rel='stylesheet' type='text/css' href='Styles/StaticHeader.css' />
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1"/>
		<meta name="CODE_LANGUAGE" content="C#"/>
		<meta name="vs_defaultClientScript" content="JavaScript"/>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"/>
		<link rel="stylesheet" type="text/css" href="CSS/Style.css"  />
		<link rel="stylesheet" type="text/css" href="./CSS/MenuButtonStyle.css"  />			
		<script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
		<script src="Scripts/Common.js" type="text/javascript"></script>
<script src="Scripts/ScrollableGridPlugin.js" type="text/javascript"></script>
<script type = "text/javascript">

    $(document).ready(function() {
    
        $('#<%=grModel.ClientID %>').Scrollable({
            ScrollHeight: 500
        });
        $('#<%=grTemplate02.ClientID %>').Scrollable({
            ScrollHeight: 500
        });
        $('#<%=grTemplate03.ClientID %>').Scrollable({
            ScrollHeight: 500
        });
        $('#<%=grTemplate04.ClientID %>').Scrollable({
            ScrollHeight: 500
        });
        $('#<%=grTemplate05.ClientID %>').Scrollable({
            ScrollHeight: 500
        });
        $('#<%=grTemplate06.ClientID %>').Scrollable({
            ScrollHeight: 500
        });
        $('#<%=grTemplate07.ClientID %>').Scrollable({
            ScrollHeight: 500
        });
    });
</script>
 <%--<script src="Scripts/jquery-1.7.1.min.js"></script>
    <script language="javascript" >
        $(document).ready(function() {
            var gridHeader = $('#<%=grModel.ClientID%>').clone(true); // Here Clone Copy of Gridview with style
            $(gridHeader).find("tr:gt(0)").remove(); // Here remove all rows except first row (header row)
            $('#<%=grModel.ClientID%> tr th').each(function(i) {
                // Here Set Width of each th from gridview to new table(clone table) th 
                $("th:nth-child(" + (i + 1) + ")", gridHeader).css('width', ($(this).width()).toString() + "px");
            });
            if ($('#<%=grModel.ClientID%>').offset()) {
                $("#GHead").append(gridHeader);
                $('#GHead').css('position', 'absolute');
                $('#GHead').css('top', $('#<%=grModel.ClientID%>').offset().top);
            }

        });
</script>--%>

		<script language="javascript" type="text/javascript">
		    Load_out = new Image();
		    Load_out.src = "Images/Load-button.gif";
		    Load_over = new Image();
		    Load_over.src = "Images/Load-button-h.png";

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
		    
		    /*function DoValidation()
		    {						
		    if (document.Form1.HidModel.value =='' || document.Form1.HidModel.value == 'Select One')
		    {
		    alert('Please Select Model');
		    return false;
		    }
		    else
		    return true;
		    }
		    function PopupForOutput()
		    {				
		    newwindow=window.open("Output.aspx?Mode=" + document.Form1.HidMode.value ,"Output","width=900,height=600,toolbar=0,menubar=0");				
		    if (window.focus)
		    newwindow.focus();	
		    window.showModalDialog("Output.aspx?Mode=" + document.Form1.HidMode.value +"&RefNo="+document.Form1.HidRefNo.value +"&Dept=" +document.Form1.HidDept.value +"&UserName=" +document.Form1.HidUserName.value + "&CountryCode=" + document.Form1.HidCountryCode.value, "Output","center=yes;dialogWidth:800px;dialogHeight:800px;toolbar=0;menubar=0");
		    return true;						
		    }*/		    
		    function isNumberKey(evt) {
		        var charCode = (evt.which) ? evt.which : event.keyCode		        
		        var e = event || window.event;
		        var src = e.srcElement || e.target;
		        if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46)
		            return false;
		        else {
		            var input = src.value;
		            var len = input.length;
		            var index = input.indexOf('.');
		            if (len == 0 && charCode == 46) return false;
		            if (index > 0 && charCode == 46) return false;

		            if (index > 0 || index == 0) {
		                var CharAfterdot = (len + 1) - index;
		                if (CharAfterdot > 3) return false;
		            }

		            if (charCode == 46 && input.split('.').length > 1) {
		                return false;
		            }

		        }
		        return true;
		    }	    
		    function fnHide() {
		        window.setTimeout("fnHide2()", 1740000);
		    }
		    function fnHide2() {
		        window.focus();
		        if (confirm('Your session going to expire in 60 sec. Do you want to continue?')) {
		            window.document.forms(0).submit();
		        }
		        else {
		            window.location.href = 'Login.aspx';
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

		    function GoHome(ExitFor) {
		        var Result;
		        if (ExitFor == 'Load') {
		            Result = confirm('Do you want to Cancel?');
		        }
		        else {
		            Result = confirm('All datas will loss, Do you want to Cancel?');
		        }

		        if (Result) {
		            window.location = 'Home.aspx';
		        }
		    }
		    function OpenViewPlatFormTable(URL) {
		        window.open(URL);
		    }
		    function ConfirmDelete() {
		        if (document.Form1.ddlContractNo.value == '0') {
		            alert('please select Contract');
		            return false;
		        }
		        else {
		            if (document.Form1.HidDeleteDept.value != document.Form1.HidUserDept.value) {
		                alert('You cannot delete other department contract...');
		                return false;
		            }
		            else {
		                return confirm('Do you want to delete?');
		            }
		        }
		    }
		    function DisabledExecutButton() {
		        document.Form1.btnExecuteModel.disabled = false;
		    }
//		    function btnHome_onclick() {

//		    }

//		    function btnHome_onclick() {

//		    }

		    function OpenWindow() {
		        //newwindow = window.open(url, "InterfaceFromCNW", "width=400,height=250,left=250,top=225,resizable=no,scrollbars=no");
		        //newwindow = window.open("CNWInterface.aspx?JobLoc=" + document.form1.HidJobLoc.value +"&JobCode=" + document.form1.HidJobCode.value, "InterfaceFromCNW", "width=500,height=250,left=250,top=225,resizable=no,scrollbars=no");
		        if (window.focus) { newwindow.focus(); }
		        return false;
		    }
function btnExitModel_onclick() {

}


        </script>
    <style type="text/css">


.ddlCSS
{
	font-size: 12px;
	border-left-color: #ffffff;
	border-bottom-color: #ffffff;
	border-top-color: #ffffff;
	border-right-color: #ffffff;
	color: black;	
	font-style: normal;
	font-family: Verdana,Arial, Helvetica, sans-serif;
	text-decoration: none;	
}

.textbox
{
	font-size: 12px;
	color: black;
	font-style: normal;
	font-family: Arial,Verdana, Helvetica, sans-serif;
	text-decoration: none;
}

    </style>
</head>
<body onload="fnHide();" style="margin-left:0;margin-top:0">
    <form id="Form1" runat="server">
     <div style="display:none;">
             <asp:LinkButton runat="server" ID="lbPrivacy" Text="PRIVACY"/>
        </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    
    <AJAXCtrlToolKit:modalpopupextender id="mpeCompute" runat="server" popupcontrolid="pnlComputeHolder"
            targetcontrolid="lbPrivacy" cancelcontrolid="lnkBtnClose" 
             backgroundcssclass="Background">
            </AJAXCtrlToolKit:modalpopupextender>
             <asp:Panel ID="pnlComputeHolder" runat="server" CssClass="PopupActivate" align="center" Style="display: none;width:700px">
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
                             <div style="margin: 10px;" id="dvExcludedSetList" disabled="true" >
                                 <asp:Label ID="lblExcludedSets" Style="font-weight: bold;" runat="server" Text="Excluded Set List"></asp:Label>
                                 <br />
                                 <input id="cbExcludedSetAll" type="checkbox" runat="server" onclick="SetAllCheckBoxListChecked('cbExcludedSet','cbExcludedSetAll');" />
                                 Select All Set
                                 <br />
                                 <div id="dvExcludedSet" style="height: 120px; font-size: 11px; width: 300px;
                                     padding: 5px; overflow: auto; border: 1px solid #ccc;">
                                     <asp:CheckBoxList ID="cbExcludedSet" runat="server" 
                                         Style="font-size: 11px;visibility:hidden" ondatabound="cbExcludedSet_DataBound"  >
                                     </asp:CheckBoxList>
                                 </div>
                             </div>                             
                         </td>
                     </tr>
                 </table>
                 <asp:LinkButton ID="lnkBtnClose" CssClass="LinkButton" runat="server" Text="Close" />
            <asp:LinkButton ID="lnkBtnCompute" OnClientClick="return IsCheckBoxListItemSelected('cbMatCat','cbMatCatAll','Please select at least one Material Category to compute!');" 
                                CssClass="LinkButton" runat="server" Text="Compute" onclick="lnkBtnCompute_Click" />
        </asp:Panel>
        
    <input id="HidUserDept" size="1" type="hidden" name="HidUserDept" runat="server" />
    <input id="HidDeleteDept" size="1" type="hidden" name="HidDeleteDept" runat="server" />
    <input id="HidModelType" type="hidden" name="HidModelType" runat="server" />
    <input id="HidContract" type="hidden" name="HidContract" runat="server" />
    <input id="HidJobLoc" type="hidden" name="HidJobLoc" runat="server" />
    <input id="HidJobCode" type="hidden" name="HidJobCode" runat="server" />    
    <input id="HiddenField1" type="hidden" name="HiddenField1" runat="server" />    
    
    <table id="Table3" cellspacing="1" cellpadding="1" border="0" style="border: thin solid #3399FF; width: 100%; height: 800px;" >
        <tr style="height: 100px;">
            <td>
                 <table id="Table2" style="height: 100%;" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr valign="bottom">
                            <td style="background-position: center center; width: 30%; background-repeat: no-repeat; border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #3399FF; " 
                                align="left" valign="middle">
                                <asp:Image ID="Image2" runat="server" Height="56px" 
                                    ImageUrl="~/Images/logo.gif" Width="201px" />   
                            </td>
                            <td align="left" valign="middle" 
                                style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #3399FF;">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Header.png" 
                                Width="613px" />
                                <asp:Label ID="lblVersion" runat="server" CssClass="labelVersion"  ></asp:Label>
                            </td>
                        </tr>
                    </table>
            </td>
        </tr>
        <tr>
            <td align="left" style="background-color: #3399FF">
                <asp:Label ID="lblUserName" runat="server" CssClass="usernamelabel" ForeColor="#FFFFCC"></asp:Label>               
            </td>            
        </tr>
        <tr style="height:100%">        
            <td valign="top" align="center" >
            <table style="width: 100%;height:700px">
                <tr>
                    <td valign="top" align="center" style="background-color: #3399FF;">     
                        <br />                   
                        <a onmouseover="display('Home_img', Home_over);" onmouseout="display('Home_img', Home_out);" href="Home.aspx"
                            onfocus="display('Home_img', Home_over);" onblur="display('Home_img', Home_out);" tabindex="1">
                            <img alt="" src="Images/homeNew.png" border="0" name="Home_img" /></a>
                            <br /><br />                        
                        <a onmouseover="display('New_img', New_out);" onmouseout="display('New_img',New_over );" href="MainNewScreen.aspx?Mode=New"
                            onfocus="display('New_img', New_out);" onblur="display('New_img',New_over );" tabindex="2">
                            <img alt="" src="Images/NewNew-h.png" border="0" name="New_img" /></a>
                            <br /><br />                        
                        <a onmouseover="display('Viewoutput_img', Viewoutput_over);" onmouseout="display('Viewoutput_img', Viewoutput_out);" href="ViewOutput.aspx"
                            onfocus="display('Viewoutput_img', Viewoutput_over);" onblur="display('Viewoutput_img', Viewoutput_out);" tabindex="3">
                            <img alt="" src="Images/outputNew.png" border="0" name="Viewoutput_img" /></a>
                            <br /><br />                        
                        <a onmouseover="display('IssuePO_img', IssuePO_over);" onmouseout="display('IssuePO_img', IssuePO_out);" href="IssuePO.aspx"
                            onfocus="display('IssuePO_img', IssuePO_over);" onblur="display('IssuePO_img', IssuePO_out);" tabindex="4">
                            <img alt="" src="Images/IssuePO.png" border="0" name="IssuePO_img" /></a>
                            <br /><br />                    
                        <a onmouseover="display('Logout_img', Logout_over);" onclick="ConfirmLogout();" onmouseout="display('Logout_img', Logout_out);"
                            onfocus="display('Logout_img', Logout_over);" onblur="display('Logout_img', Logout_out);" tabindex="5">
                            <img alt="" src="Images/LogoutNew.png" border="0" name="Logout_img" /></a>
                    </td>
                    <td>
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                            <ProgressTemplate>
                                <img alt="progress" src="Images/processing.gif" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>                
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table style="width: 100%;height:100%">
                                    <tr>
                                        <td align="right">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="lblError" runat="server" CssClass="labelerror"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblStatus" runat="server" ForeColor="Green" Font-Size="Small" Font-Bold="True"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lblHeading" runat="server" ForeColor="Green" Font-Size="Medium" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:RadioButton ID="rbE" runat="server" AutoPostBack="True" Checked="True" GroupName="rbm" OnCheckedChanged="rbEC_CheckedChanged" Text="Equipment" />
                                            <asp:RadioButton ID="rbC" runat="server" AutoPostBack="True" GroupName="rbm" Text="Common" OnCheckedChanged="rbEC_CheckedChanged" />
                                            </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlMain"  runat="server">
                                                <table id="Table4" border="0" cellspacing="1" cellpadding="1"   width="100%">
                                                    <tr >
                                                        <td align="left">
                                                         <div id='divContainerMain' class="table-container12" style="border-style: none">
                                                            <asp:GridView ID="grScreenTemplate" runat="server" AutoGenerateColumns="False" CellPadding="3"
                                                                DataKeyNames="ParmCode,ModelType,DisplaySequence,IsValueTable,IsAllowBlank"
                                                                OnRowDataBound="grScreenTemplate_RowDataBound" BorderColor="#CCCCCC"
                                                                BorderStyle="None" BorderWidth="1px" CssClass="grScreenTemplate" 
                                                                 onprerender="grScreenTemplate_PreRender" BackColor="White">
                                                                <AlternatingRowStyle Font-Size="8pt" BackColor="#D7EBFF" />
                                                                <Columns>
                                                                    <asp:BoundField HeaderText="Seq No" ReadOnly="true">                                                                    
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    
                                                                    <asp:BoundField DataField="ParmCode" HeaderText="Para Code" SortExpression="STGroupCode"
                                                                        Visible="False"></asp:BoundField>
                                                                    <asp:BoundField DataField="ParmDescription" HeaderText="Parameter" SortExpression="GroupName">
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="ModelType" HeaderText="Model Type" SortExpression="ParmCode"
                                                                        Visible="False"></asp:BoundField>
                                                                    <asp:BoundField DataField="DisplaySequence" HeaderText="Display Sequence" SortExpression="ParmDesc"
                                                                        Visible="False"></asp:BoundField>
                                                                    <asp:TemplateField HeaderText="Value">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="ddlValue" runat="server" Font-Names="Times New Roman" Font-Size="9pt"
                                                                                Height="22px" OnSelectedIndexChanged="OnProjectLocationIndexChange">
                                                                            </asp:DropDownList>
                                                                            <asp:TextBox ID="txtValue" runat="server" CssClass="textbox" Font-Names="Times New Roman"
                                                                                Font-Size="9pt" MaxLength="50" Width="235px"></asp:TextBox>
                                                                                <asp:HiddenField ID="hdnParmFormat" runat="server" Value='<%#Eval("ParmFormat") %>' />
                                                                            <!-- OnSelectedIndexChanged="ddlValueModel_SelectedIndexChanged"> -->
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="IsValueTable" HeaderText="IsValueTable" Visible="False">
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="ParmUOM" HeaderText="UOM"></asp:BoundField>
                                                                    <asp:BoundField DataField="ParmRemarks" HeaderText="Comments"></asp:BoundField>
                                                                    <asp:TemplateField HeaderText="Non Std">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="cbNS" runat="server" AutoPostBack="True" OnCheckedChanged="cbNS_CheckedChanged" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="IsAllowBlank" HeaderText="Is Allow Blank" Visible="False" >
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="Is Allow Blank" >
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="cbAllowBlank" runat="server" AutoPostBack="False" Enabled="False"  />
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    
                                                                </Columns>
                                                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                                                <HeaderStyle BackColor="#3399FF" Font-Bold="True" ForeColor="White" />
                                                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                                <RowStyle Font-Size="8pt" ForeColor="#000066" />
                                                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                            </asp:GridView>
                                                            </div> 
                                                        </td>
                                                    </tr>                                                                                                    
                                                    <tr >
                                                        <td align="left">
                                                            <ul id="navPnlMain" class="NavMain" >
                                                                <li >
                                                                    <asp:LinkButton ID="btnNextMain"  runat="server" OnClick="btnNextMain_Click">Next</asp:LinkButton>  
                                                                </li>
                                                                <li>
                                                                    <asp:LinkButton ID="btnExit" runat="server" OnClientClick="GoHome('Main');return false;">Cancel</asp:LinkButton>                 
                                                                </li>	                                                            
                                                            </ul>                                                         
                                                            
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Panel ID="pnlModelBtn" runat="server" CssClass="cssmenu"> 
                                                <div id='bg-bottomLine'></div>
	                                            <div id='bg-Menu'></div>                                               
                                                <ul id="navpnlModelBtn" >
                                                    <li >
                                                        <asp:LinkButton ID="btnMainSpec"   runat="server" OnClick="btnMainSpec_Click">Main Spec</asp:LinkButton>  
                                                    </li>
	                                                <li >
                                                        <asp:LinkButton ID="btnMacSpec" runat="server" OnClick="btnMacSpec_Click">Machine Spec</asp:LinkButton>  
                                                    </li>
	                                                <li >
                                                        <asp:LinkButton ID="btnCopOpFn" runat="server" OnClick="btnCopOpFn_Click">COP Operation &amp; Function Spec</asp:LinkButton>  
                                                    </li>
	                                                <li >
                                                        <asp:LinkButton ID="btnHallSpec" runat="server" OnClick="btnHallSpec_Click">Hall Spec</asp:LinkButton>  
                                                    </li>
	                                                <li >
                                                        <asp:LinkButton ID="btnCarSpec" runat="server" OnClick="btnCarSpec_Click">Car Spec</asp:LinkButton>  
                                                    </li>
	                                                <li >
                                                        <asp:LinkButton ID="btnHoistSpec" runat="server" OnClick="btnHoistSpec_Click">Hoistway Spec</asp:LinkButton>  
                                                    </li>
	                                                <li >
                                                        <asp:LinkButton ID="btnRemarks" runat="server" OnClick="btnRemarks_Click">Remarks</asp:LinkButton>  
                                                    </li>                                                                
                                               </ul>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                        <%-- <div style="width:1000px;">
                                            <div id="GHead"></div>--%>
                                            <%-- This GHead is added for Store Gridview Header  --%>
                                             <%--<div style="height:600px; overflow:auto"> --%>
                                              <asp:Panel ID="pnlModel" runat="server" Style="height: 600px; width: auto"
                                                 ScrollBars="Auto" BorderStyle="None"> 
                                               <div id='divContainerModel' class="table-container12" style="border-style: none">
                                                    <asp:GridView ID="grModel" runat="server" AutoGenerateColumns="False" 
                                                       CellPadding="3" OnRowDataBound="grModel_RowDataBound" DataKeyNames="ParmCode,ModelType,DisplaySequence,IsValueTable,IsAllowBlank"
                                                       BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
                                                        OnPreRender="grModel_PreRender" BackColor="White" 
                                                       >
                                                        <AlternatingRowStyle Font-Size="8pt" BackColor="#D7EBFF"  />
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Seq No" ReadOnly="true">
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ParmCode" HeaderText="Parm Code" SortExpression="STGroupCode"
                                                                Visible="False"></asp:BoundField>
                                                            <asp:BoundField DataField="ParmDescription" HeaderText="Parameter" SortExpression="GroupName">
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ModelType" HeaderText="Model Type" SortExpression="ParmCode"
                                                                Visible="False"></asp:BoundField>
                                                            <asp:BoundField DataField="DisplaySequence" HeaderText="Display Sequence" SortExpression="ParmDesc"
                                                                Visible="False"></asp:BoundField>
                                                            <asp:TemplateField HeaderText="Value">
                                                                <ItemTemplate>
                                                                    <asp:UpdatePanel runat="server" ID="UpId" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                                                        <ContentTemplate>
                                                                            <asp:DropDownList ID="ddlValue" runat="server" Font-Names="Times New Roman" Font-Size="9pt">
                                                                            </asp:DropDownList>
                                                                            <asp:TextBox ID="txtValue" runat="server" CssClass="textbox" Font-Names="Times New Roman"
                                                                                 OnTextChanged="SetTotalHeightText" AutoPostBack="true" Font-Size="9pt" MaxLength="50"
                                                                                Width="235px"></asp:TextBox>
                                                                                <asp:HiddenField ID="hdnParmFormat" runat="server" Value='<%#Eval("ParmFormat") %>' />
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="IsValueTable" HeaderText="IsValueTable" Visible="False">
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ParmUOM" HeaderText="UOM"></asp:BoundField>
                                                            <asp:BoundField DataField="ParmRemarks" HeaderText="Comments"></asp:BoundField>
                                                            <asp:TemplateField HeaderText="Non Std">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbNS" runat="server" AutoPostBack="True" OnCheckedChanged="cbNS_CheckedChanged" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="IsAllowBlank" HeaderText="Is Allow Blank" Visible="False">
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Is Allow Blank" >
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="cbAllowBlank" runat="server" AutoPostBack="False" Enabled="False"  />
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                                        <HeaderStyle BackColor="#3399FF" Font-Bold="True" ForeColor="White" 
                                                            HorizontalAlign="Left" />
                                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                        <RowStyle Font-Size="8pt" ForeColor="#000066" />
                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                    </asp:GridView>
                                                     </div>
                                             </asp:Panel>
                                            <asp:Panel ID="pnlTemplate02" runat="server" Style="height: 600px; width: 1000px"
                                                ScrollBars="Auto" BorderStyle="None">
                                                <div id='divContainer02' class="table-container12" style="border-style: none">
                                                    <asp:GridView ID="grTemplate02" runat="server" AutoGenerateColumns="False" 
                                                        CellPadding="3" OnRowDataBound="grTemplate02_RowDataBound" 
                                                        DataKeyNames="ParmCode,ModelType,DisplaySequence,IsValueTable,IsAllowBlank" 
                                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                                        OnPreRender="grTemplate02_PreRender" CssClass="grTemplate02" 
                                                        BackColor="White">
                                                        <AlternatingRowStyle Font-Size="8pt"  BackColor="#D7EBFF" />
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Seq No" ReadOnly="true">
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:BoundField>
                                                             
                                                            <asp:BoundField DataField="ParmCode" HeaderText="Parm Code" SortExpression="STGroupCode"
                                                                Visible="False" />
                                                            <asp:BoundField DataField="ParmDescription" HeaderText="Parameter" SortExpression="GroupName" />
                                                            <asp:BoundField DataField="ModelType" HeaderText="Model Type" SortExpression="ParmCode"
                                                                Visible="False" />
                                                            <asp:BoundField DataField="DisplaySequence" HeaderText="Display Sequence" SortExpression="ParmDesc"
                                                                Visible="False" />
                                                            <asp:TemplateField HeaderText="Value">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlValue" runat="server" Font-Size="9pt">
                                                                    </asp:DropDownList>
                                                                    <asp:TextBox ID="txtValue" runat="server" CssClass="textbox" Font-Size="9pt" MaxLength="50"
                                                                        Width="235px"></asp:TextBox>
                                                                        <asp:HiddenField ID="hdnParmFormat" runat="server" Value='<%#Eval("ParmFormat") %>' />
                                                                    <itemstyle horizontalalign="Left" />
                                                                </ItemTemplate>
                                                                <ItemStyle Width="280px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="IsValueTable" HeaderText="IsValueTable" Visible="False" />
                                                            <asp:BoundField DataField="ParmUOM" HeaderText="UOM" />
                                                            <asp:BoundField DataField="ParmRemarks" HeaderText="Comments" />
                                                            <asp:TemplateField HeaderText="Non Std">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbNS" runat="server" AutoPostBack="True" OnCheckedChanged="cbNS_CheckedChanged" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="IsAllowBlank" HeaderText="Is Allow Blank" />
                                                        </Columns>
                                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                                        <HeaderStyle BackColor="#3399FF" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                        <RowStyle Font-Size="8pt" ForeColor="#000066" />
                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                    </asp:GridView>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlTemplate03" runat="server" Style="height: 600px; width: 1000px"
                                                ScrollBars="Auto" BorderStyle="None">
                                                <div id='divContainer03' class="table-container12" style="border-style: none">
                                                    <asp:GridView ID="grTemplate03" runat="server" AutoGenerateColumns="False" 
                                                        CellPadding="3" OnRowDataBound="grTemplate03_RowDataBound" 
                                                        DataKeyNames="ParmCode,ModelType,DisplaySequence,IsValueTable,IsAllowBlank" 
                                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                                        CssClass="grTemplate03" OnPreRender="grTemplate03_PreRender" 
                                                        BackColor="White">
                                                        <AlternatingRowStyle Font-Size="8pt" BackColor="#D7EBFF"  />
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Seq No" ReadOnly="true">
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:BoundField>
                                                          
                                                            <asp:BoundField DataField="ParmCode" HeaderText="Parm Code" SortExpression="STGroupCode"
                                                                Visible="False" />
                                                            <asp:BoundField DataField="ParmDescription" HeaderText="Parameter" SortExpression="GroupName" />
                                                            <asp:BoundField DataField="ModelType" HeaderText="Model Type" SortExpression="ParmCode"
                                                                Visible="False" />
                                                            <asp:BoundField DataField="DisplaySequence" HeaderText="Display Sequence" SortExpression="ParmDesc"
                                                                Visible="False" />
                                                            <asp:TemplateField HeaderText="Value">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlValue" runat="server" Font-Size="9pt">
                                                                    </asp:DropDownList>
                                                                    <asp:TextBox ID="txtValue" runat="server" CssClass="textbox" Font-Size="9pt" MaxLength="50"
                                                                        Width="235px"></asp:TextBox>
                                                                        <asp:HiddenField ID="hdnParmFormat" runat="server" Value='<%#Eval("ParmFormat") %>' />
                                                                    <itemstyle horizontalalign="Left" />
                                                                </ItemTemplate>
                                                                <ItemStyle Width="280px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="IsValueTable" HeaderText="IsValueTable" Visible="False" />
                                                            <asp:BoundField DataField="ParmUOM" HeaderText="UOM" />
                                                            <asp:BoundField DataField="ParmRemarks" HeaderText="Comments" />
                                                            <asp:TemplateField HeaderText="Non Std">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbNS" runat="server" AutoPostBack="True" OnCheckedChanged="cbNS_CheckedChanged" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="IsAllowBlank" HeaderText="Is Allow Blank" />
                                                        </Columns>
                                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                                        <HeaderStyle BackColor="#3399FF" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                        <RowStyle Font-Size="8pt" ForeColor="#000066" />
                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                    </asp:GridView>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlTemplate04" runat="server" Style="height: 600px; width: auto" 
                                                ScrollBars="Auto" BorderStyle="None">
                                                <div id='divContainer04' class="table-container12" style="border-style: none">
                                                    <asp:GridView ID="grTemplate04" runat="server" AutoGenerateColumns="False" 
                                                        CellPadding="3" OnRowDataBound="grTemplate04_RowDataBound" 
                                                        DataKeyNames="ParmCode,ModelType,DisplaySequence,IsValueTable,IsAllowBlank" 
                                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                                        CssClass="grTemplate04" OnPreRender="grTemplate04_PreRender" 
                                                        BackColor="White">
                                                        <AlternatingRowStyle Font-Size="8pt" BackColor="#D7EBFF" />
                                                        <Columns>
                                                           <asp:BoundField HeaderText="Seq No" ReadOnly="true">
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:BoundField>
                                                          
                                                            <asp:BoundField DataField="ParmCode" HeaderText="Parm Code" SortExpression="STGroupCode"
                                                                Visible="False" />
                                                            <asp:BoundField DataField="ParmDescription" HeaderText="Parameter" SortExpression="GroupName" />
                                                            <asp:BoundField DataField="ModelType" HeaderText="Model Type" SortExpression="ParmCode"
                                                                Visible="False" />
                                                            <asp:BoundField DataField="DisplaySequence" HeaderText="Display Sequence" SortExpression="ParmDesc"
                                                                Visible="False" />
                                                            <asp:TemplateField HeaderText="Value">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlValue" runat="server" Font-Size="9pt">
                                                                    </asp:DropDownList>
                                                                    <asp:TextBox ID="txtValue" runat="server" CssClass="textbox" Font-Size="9pt" MaxLength="50"
                                                                        Width="235px"></asp:TextBox>
                                                                        <asp:HiddenField ID="hdnParmFormat" runat="server" Value='<%#Eval("ParmFormat") %>' />
                                                                    <itemstyle horizontalalign="Left" />
                                                                </ItemTemplate>
                                                                <ItemStyle Width="280px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="IsValueTable" HeaderText="IsValueTable" Visible="False" />
                                                            <%--<asp:BoundField DataField="IsAllowBlank" HeaderText="Is Allow Blank" />--%>
                                                            <asp:BoundField DataField="ParmUOM" HeaderText="UOM" />
                                                            <asp:BoundField DataField="ParmRemarks" HeaderText="Comments" />
                                                            <asp:TemplateField HeaderText="Non Std">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbNS" runat="server" AutoPostBack="True" OnCheckedChanged="cbNS_CheckedChanged" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                           <asp:BoundField DataField="IsAllowBlank" HeaderText="Is Allow Blank" />
                                                        </Columns>
                                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                                        <HeaderStyle BackColor="#3399FF" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                        <RowStyle Font-Size="8pt" ForeColor="#000066" />
                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                    </asp:GridView>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlTemplate05" runat="server" Style="height: 600px; width: 1000px"
                                                ScrollBars="Auto" BorderStyle="None">
                                                <div id='divContainer05' class="table-container12" style="border-style: none">
                                                    <asp:GridView ID="grTemplate05" runat="server" AutoGenerateColumns="False" 
                                                        CellPadding="3" OnRowDataBound="grTemplate05_RowDataBound" 
                                                        DataKeyNames="ParmCode,ModelType,DisplaySequence,IsValueTable,IsAllowBlank" 
                                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                                        CssClass="grTemplate05" OnPreRender="grTemplate05_PreRender" 
                                                        BackColor="White">
                                                        <AlternatingRowStyle Font-Size="8pt" BackColor="#D7EBFF" />
                                                        <Columns>
                                                             <asp:BoundField HeaderText="Seq No" ReadOnly="true">
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:BoundField>
                                                           
                                                            <asp:BoundField DataField="ParmCode" HeaderText="Parm Code" SortExpression="STGroupCode"
                                                                Visible="False" />
                                                            <asp:BoundField DataField="ParmDescription" HeaderText="Parameter" SortExpression="GroupName" />
                                                            <asp:BoundField DataField="ModelType" HeaderText="Model Type" SortExpression="ParmCode"
                                                                Visible="False" />
                                                            <asp:BoundField DataField="DisplaySequence" HeaderText="Display Sequence" SortExpression="ParmDesc"
                                                                Visible="False" />
                                                            <asp:TemplateField HeaderText="Value">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlValue" runat="server" Font-Size="9pt">
                                                                    </asp:DropDownList>
                                                                    <asp:TextBox ID="txtValue" runat="server" CssClass="textbox" Font-Size="9pt" MaxLength="50"
                                                                        Width="235px"></asp:TextBox>
                                                                        <asp:HiddenField ID="hdnParmFormat" runat="server" Value='<%#Eval("ParmFormat") %>' />
                                                                    <itemstyle horizontalalign="Left" />
                                                                </ItemTemplate>
                                                                <ItemStyle Width="280px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="IsValueTable" HeaderText="IsValueTable" Visible="False" />
                                                            <asp:BoundField DataField="ParmUOM" HeaderText="UOM" />
                                                            <asp:BoundField DataField="ParmRemarks" HeaderText="Comments" />
                                                            <asp:TemplateField HeaderText="Non Std">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbNS" runat="server" AutoPostBack="True" OnCheckedChanged="cbNS_CheckedChanged" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="IsAllowBlank" HeaderText="Is Allow Blank" />
                                                        </Columns>
                                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                                        <HeaderStyle BackColor="#3399FF" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                        <RowStyle Font-Size="8pt" ForeColor="#000066" />
                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                    </asp:GridView>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlTemplate06" runat="server" Style="height: 600px; width: auto" 
                                                ScrollBars="Auto" BorderStyle="None">
                                                <div id='divContainer06' class="table-container12" 
                                                    style="border-style: none; top: 0px; left: 0px;">
                                                    <asp:GridView ID="grTemplate06" runat="server" AutoGenerateColumns="False" 
                                                        CellPadding="3" OnRowDataBound="grTemplate06_RowDataBound" 
                                                        DataKeyNames="ParmCode,ModelType,DisplaySequence,IsValueTable,IsAllowBlank" 
                                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                                        CssClass="grTemplate06" OnPreRender="grTemplate06_PreRender" 
                                                        BackColor="White">
                                                        <AlternatingRowStyle Font-Size="8pt" BackColor="#D7EBFF" />
                                                        <Columns>
                                                             <asp:BoundField HeaderText="Seq No" ReadOnly="true">
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:BoundField>
                                                         
                                                            <asp:BoundField DataField="ParmCode" HeaderText="Parm Code" SortExpression="STGroupCode"
                                                                Visible="False" />
                                                            <asp:BoundField DataField="ParmDescription" HeaderText="Parameter" SortExpression="GroupName" />
                                                            <asp:BoundField DataField="ModelType" HeaderText="Model Type" SortExpression="ParmCode"
                                                                Visible="False" />
                                                            <asp:BoundField DataField="DisplaySequence" HeaderText="Display Sequence" SortExpression="ParmDesc"
                                                                Visible="False" />
                                                            <asp:TemplateField HeaderText="Value">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlValue" runat="server" Font-Size="9pt"  OnSelectedIndexChanged="SetNADDLDefault">
                                                                    </asp:DropDownList>
                                                                    <asp:TextBox ID="txtValue" runat="server" CssClass="textbox" Font-Size="9pt" MaxLength="50"
                                                                        Width="235px"></asp:TextBox>
                                                                        <asp:HiddenField ID="hdnParmFormat" runat="server" Value='<%#Eval("ParmFormat") %>' />
                                                                    <itemstyle horizontalalign="Left" />
                                                                </ItemTemplate>
                                                                <ItemStyle Width="280px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="IsValueTable" HeaderText="IsValueTable" Visible="False" />
                                                            <asp:BoundField DataField="ParmUOM" HeaderText="UOM" />
                                                            <asp:BoundField DataField="ParmRemarks" HeaderText="Comments" />
                                                            <asp:TemplateField HeaderText="Non Std">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbNS" runat="server" AutoPostBack="True" OnCheckedChanged="cbNS_CheckedChanged" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="IsAllowBlank" HeaderText="Is Allow Blank" />
                                                        </Columns>
                                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                                        <HeaderStyle BackColor="#3399FF" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                        <RowStyle Font-Size="8pt" ForeColor="#000066" />
                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                    </asp:GridView>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlTemplate07" runat="server" Style="height: 600px; width: auto" 
                                                ScrollBars="Auto" BorderStyle="None">
                                                <div id='divContainer07' class="table-container12" style="border-style: none">
                                                    <asp:GridView ID="grTemplate07" runat="server" AutoGenerateColumns="False" 
                                                        CellPadding="3" OnRowDataBound="grTemplate07_RowDataBound" 
                                                        DataKeyNames="ParmCode,ModelType,DisplaySequence,IsValueTable,IsAllowBlank" 
                                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                                        CssClass="grTemplate07" OnPreRender="grTemplate07_PreRender" 
                                                        BackColor="White">
                                                        <AlternatingRowStyle Font-Size="8pt" BackColor="#D7EBFF"  />
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Seq No" ReadOnly="true">
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:BoundField>
                                                          
                                                            <asp:BoundField DataField="ParmCode" HeaderText="Parm Code" SortExpression="STGroupCode"
                                                                Visible="False" />
                                                            <asp:BoundField DataField="ParmDescription" HeaderText="Parameter" SortExpression="GroupName" />
                                                            <asp:BoundField DataField="ModelType" HeaderText="Model Type" SortExpression="ParmCode"
                                                                Visible="False" />
                                                            <asp:BoundField DataField="DisplaySequence" HeaderText="Display Sequence" SortExpression="ParmDesc"
                                                                Visible="False" />
                                                            <asp:TemplateField HeaderText="Value">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlValue" runat="server" Font-Size="9pt">
                                                                    </asp:DropDownList>
                                                                    <asp:TextBox ID="txtValue" runat="server" CssClass="textbox" Font-Size="9pt" MaxLength="50"
                                                                        Width="235px"></asp:TextBox>
                                                                        <asp:HiddenField ID="hdnParmFormat" runat="server" Value='<%#Eval("ParmFormat") %>' />
                                                                    <itemstyle horizontalalign="Left" />
                                                                </ItemTemplate>
                                                                <ItemStyle Width="280px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="IsValueTable" HeaderText="IsValueTable" Visible="False" />
                                                            <asp:BoundField DataField="ParmUOM" HeaderText="UOM" />
                                                            <asp:BoundField DataField="ParmRemarks" HeaderText="Comments" />
                                                            <asp:TemplateField HeaderText="Non Std">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbNS" runat="server" AutoPostBack="True" OnCheckedChanged="cbNS_CheckedChanged" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="IsAllowBlank" HeaderText="Is Allow Blank" />
                                                        </Columns>
                                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                                        <HeaderStyle BackColor="#3399FF" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                        <RowStyle Font-Size="8pt" ForeColor="#000066" />
                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                    </asp:GridView>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="PnlExecute" runat="server">
                                                <table id="Table15" border="0" cellspacing="1" cellpadding="1" width="100%">
                                                    <tr>
                                                        <td>
                                                            <ul id="navPnlExecute" class="NavMain">
                                                                <li >
                                                                    <asp:LinkButton ID="btnRefreshModel" runat="server" OnClick="btnRefreshModel_Click">Referesh</asp:LinkButton>  
                                                                </li>
	                                                            <li >
                                                                    <asp:LinkButton ID="btnExecuteModel" runat="server" OnClick="btnExecuteModel_Click">Compute</asp:LinkButton>  
                                                                </li>
	                                                            <li >
                                                                    <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click">Save</asp:LinkButton>  
                                                                </li>
	                                                            <li >
                                                                    <asp:LinkButton ID="btnPreviousModel" runat="server" OnClick="btnPreviousModel_Click">Previous</asp:LinkButton>  
                                                                </li>
	                                                            <li >
                                                                    <asp:LinkButton ID="btnExitModel" runat="server" OnClientClick="GoHome('Model');return false;">Cancel</asp:LinkButton>  
                                                                </li>	                                                            	                                                               
                                                            </ul>	
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnNextMain" />
                                <asp:PostBackTrigger ControlID="btnMainSpec" />
                                <asp:PostBackTrigger ControlID="grModel" />
                                <asp:PostBackTrigger ControlID="btnMacSpec" />
                                <asp:PostBackTrigger ControlID="btnCopOpFn" />
                                <asp:PostBackTrigger ControlID="btnHallSpec" />
                                <asp:PostBackTrigger ControlID="btnCarSpec" />
                                <asp:PostBackTrigger ControlID="btnHoistSpec" />
                                <asp:PostBackTrigger ControlID="btnRemarks" />
                                <asp:PostBackTrigger ControlID="btnSave" />
                                <asp:PostBackTrigger ControlID="btnExecuteModel" />
                            </Triggers>
                        </asp:UpdatePanel>                                                  
                    </td>
                </tr>
            </table>                
            </td>           
        </tr>               
    </table>
    </form>
</body>
</html>
