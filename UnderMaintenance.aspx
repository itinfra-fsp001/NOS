<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnderMaintenance.aspx.cs" Inherits="NewOrderingSystem.UnderMaintenance" %>

<%--<%@ Register Assembly="IdeaSparx.CoolControls.Web" Namespace="IdeaSparx.CoolControls.Web"
    TagPrefix="cc1" %>--%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script src="Scripts/jquery-1.7.1.min.js"></script>
    <script language="javascript" >
    $(document).ready(function () {
    var gridHeader = $('#<%=grModel.ClientID%>').clone(true); // Here Clone Copy of Gridview with style
        $(gridHeader).find("tr:gt(0)").remove(); // Here remove all rows except first row (header row)
        $('#<%=grModel.ClientID%> tr th').each(function(i) {
            // Here Set Width of each th from gridview to new table(clone table) th 
            $("th:nth-child(" + (i + 1) + ")", gridHeader).css('width', ($(this).width()).toString() + "px");
        });
        $("#GHead").append(gridHeader);
        $('#GHead').css('position', 'absolute');
        $('#GHead').css('top', $('#<%=grModel.ClientID%>').offset().top);
 
    });
</script> 
<%--   		 <script type='text/javascript' src='Styles/x.js'></script>
            <script type='text/javascript' src='Styles/xtableheaderfixed.js'></script>    
<script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
<script src="Scripts/ScrollableGridPlugin.js" type="text/javascript"></script>
<script type = "text/javascript">
$(document).ready(function () {
    $('#<%=grModelCool.ClientID %>').Scrollable({
        ScrollHeight: 700
    });
});
</script>--%>
  <script type='text/javascript'>
      //xAddEventListener(window, 'load', function() { new xTableHeaderFixed('grScreenTemplate', 'divContainerMain', 0); }, false);
//      xAddEventListener(window, 'load', function() { new xTableHeaderFixed('grModel', 'divContainerModel', 0); }, false);
//      xAddEventListener(window, 'load', function() { new xTableHeaderFixed('grTemplate02', 'divContainer02', 0); }, false);
//      xAddEventListener(window, 'load', function() { new xTableHeaderFixed('grTemplate03', 'divContainer03', 0); }, false);
//      xAddEventListener(window, 'load', function() { new xTableHeaderFixed('grTemplate04', 'divContainer04', 0); }, false);
//      xAddEventListener(window, 'load', function() { new xTableHeaderFixed('grTemplate05', 'divContainer05', 0); }, false);
//      xAddEventListener(window, 'load', function() { new xTableHeaderFixed('grTemplate06', 'divContainer06', 0); }, false);
//      xAddEventListener(window, 'load', function() { new xTableHeaderFixed('grTemplate07', 'divContainer07', 0); }, false);
//           </script> 
//             <script type="text/javascript">
//                 //http://go4answers.webhost4life.com/Example/maintain-scroll-position-postback-159338.aspx maintain scroll in panel
//                 var hid;
//                 var scrop;
//                 function Pageload() {
//                     hid = document.getElementById("HiddenField1");

//                     scrop = document.getElementById("pnlModel");
//                     if (scrop == null)
//                         scrop = document.getElementById("pnlTemplate02");
//                     if (scrop == null)
//                         scrop = document.getElementById("pnlTemplate03");
//                     if (scrop == null)
//                         scrop = document.getElementById("pnlTemplate04");
//                     if (scrop == null)
//                         scrop = document.getElementById("pnlTemplate05");
//                     if (scrop == null)
//                         scrop = document.getElementById("pnlTemplate06");
//                     if (scrop == null)
//                         scrop = document.getElementById("pnlTemplate07");

//                     hid.value = hid.value;
//                 }

//                 function SetPosition() {
//                     if (scrop != null) {
//                         hid.value = scrop.scrollTop;
//                     }
//                 }

//                 function GetPosition() {
//                     if (scrop != null) {
//                         scrop.scrollTop = hid.value;
//                     }
//                 }

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
        </script>

    <style type="text/css">


.textbox
{
	font-size: 12px;
	color: black;
	font-style: normal;
	font-family: Arial,Verdana, Helvetica, sans-serif;
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

        .style1
        {
            height: 179px;
            border-collapse: separate;
            border-left: 1px solid #596380;
            border-right: 1px Solid #CCCCCC;
            border-top: 1px solid #596380;
            border-bottom: 1px Solid #CCCCCC;
            background-color: White;
        }
        .style2
        {
            border-collapse: separate;
            border-left: 1px solid #596380;
            border-right: 1px None #CCCCCC;
            border-top: 1px solid #596380;
            border-bottom: 1px None #CCCCCC;
            background-color: White;
        }
    </style>
</head>
<body >
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    
    <div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
             <ContentTemplate>
     <table width="100%">
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>            
                 <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Maintenance.jpg" />
             </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>            
                                                 <%--   <asp:GridView ID="grTemplate02" 
                     runat="server" AutoGenerateColumns="False" 
                                                        CellPadding="3" OnRowDataBound="grTemplate02_RowDataBound" 
                                                        DataKeyNames="ParmCode,ModelType,DisplaySequence,IsValueTable,IsAllowBlank" 
                                                        BorderColor="#CCCCCC" 
                     BorderStyle="None" BorderWidth="1px"
                                                        OnPreRender="grTemplate02_PreRender" CssClass="grTemplate02" 
                                                        BackColor="White">
                                                        <AlternatingRowStyle Font-Size="8pt"  BackColor="#D7EBFF" />
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Seq No" ReadOnly="true">
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:BoundField>
                                                             
                                                            <asp:BoundField DataField="ParmCode" HeaderText="Parm Code" SortExpression="STGroupCode"
                                                                Visible="False" />
                                                            <asp:BoundField DataField="ParmDescription" HeaderText="Parameter" 
                                                                SortExpression="GroupName" />
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
                                                                    <itemstyle horizontalalign="Left" />
                                                                </ItemTemplate>
                                                                <ItemStyle Width="280px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="IsValueTable" HeaderText="IsValueTable" 
                                                                Visible="False" />
                                                            <asp:BoundField DataField="ParmUOM" HeaderText="UOM" />
                                                            <asp:BoundField DataField="ParmRemarks" HeaderText="Comments" />
                                                            <asp:TemplateField HeaderText="Non Std">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbNS" runat="server" AutoPostBack="True" 
                                                                        OnCheckedChanged="cbNS_CheckedChanged" />
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
                                                    </asp:GridView>--%>
             </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>            
                 <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
             </td>
            <td>
                &nbsp;</td>
        </tr>
         <tr>
             <td>
                 &nbsp;</td>
             <td>
                <%-- <cc1:CoolGridView ID="grModelCool" runat="server" AutoGenerateColumns="False" 
                     BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" 
                     CellPadding="3" Width="700px" 
                     DataKeyNames="ParmCode,ModelType,DisplaySequence,IsValueTable,IsAllowBlank" 
                     OnPreRender="grModel_PreRender" OnRowDataBound="grModel_RowDataBound">
                     <Columns>
                         <asp:BoundField HeaderText="Seq No" ReadOnly="true">
                             <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                         </asp:BoundField>
                         <asp:BoundField DataField="ParmCode" HeaderText="Parm Code" 
                             SortExpression="STGroupCode" Visible="False" />
                         <asp:BoundField DataField="ParmDescription" HeaderText="Parameter" 
                             SortExpression="GroupName" />
                         <asp:BoundField DataField="ModelType" HeaderText="Model Type" 
                             SortExpression="ParmCode" Visible="False" />
                         <asp:BoundField DataField="DisplaySequence" HeaderText="Display Sequence" 
                             SortExpression="ParmDesc" Visible="False" />
                         <asp:TemplateField HeaderText="Value">
                             <ItemTemplate>
                                 <asp:DropDownList ID="ddlValue" runat="server" Font-Names="Times New Roman" 
                                     Font-Size="9pt">
                                 </asp:DropDownList>
                                 <asp:TextBox ID="txtValue" runat="server" CssClass="textbox" 
                                     Font-Names="Times New Roman" Font-Size="9pt" MaxLength="50" Width="235px"></asp:TextBox>
                             </ItemTemplate>
                             <ItemStyle HorizontalAlign="Left" />
                         </asp:TemplateField>
                         <asp:BoundField DataField="IsValueTable" HeaderText="IsValueTable" 
                             Visible="False" />
                         <asp:BoundField DataField="ParmUOM" HeaderText="UOM" />
                         <asp:BoundField DataField="ParmRemarks" HeaderText="Comments" />
                         <asp:TemplateField HeaderText="Non Std">
                             <ItemTemplate>
                                 <asp:CheckBox ID="cbNS" runat="server" AutoPostBack="True" />
                             </ItemTemplate>
                             <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                         </asp:TemplateField>
                         <asp:BoundField DataField="IsAllowBlank" HeaderText="Is Allow Blank" />
                     </Columns>
                     <AlternatingRowStyle BackColor="#D7EBFF" Font-Size="8pt" />
                     <FooterStyle BackColor="White" ForeColor="#000066" />
                     <HeaderStyle BackColor="#3399FF" Font-Bold="True" ForeColor="White" 
                         HorizontalAlign="Left" />
                     <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                     <RowStyle Font-Size="8pt" ForeColor="#000066" />
                     <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                 </cc1:CoolGridView>--%>
             </td>
             <td>
                 &nbsp;</td>
         </tr>
        <tr>
            <td  >
            </td>
            <td >
             
    <div style="width:1180px;">
    <div id="GHead"></div> 
    <%-- This GHead is added for Store Gridview Header  --%>
     <div style="height:700px; overflow:auto"> 
     <asp:Panel ID="pnlModel" runat="server" Style="height: 600px; width: auto" 
                                                 ScrollBars="Auto" BorderStyle="None"> 
                                                    <asp:GridView ID="grModel" runat="server" AutoGenerateColumns="False" 
                                                        CellPadding="3" 
                   Width="1200px" OnRowDataBound="grModel_RowDataBound" DataKeyNames="ParmCode,ModelType,DisplaySequence,IsValueTable,IsAllowBlank"
                                                         BorderColor="#CCCCCC" 
                    BorderStyle="Solid" BorderWidth="1px"
                                                        CssClass="grModel" 
                    OnPreRender="grModel_PreRender" BackColor="White">
                                                        <AlternatingRowStyle Font-Size="8pt" BackColor="#D7EBFF"  />
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Seq No" ReadOnly="true">
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ParmCode" HeaderText="Parm Code" SortExpression="STGroupCode"
                                                                Visible="False"></asp:BoundField>
                                                            <asp:BoundField DataField="ParmDescription" HeaderText="Parameter" 
                                                                SortExpression="GroupName">
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ModelType" HeaderText="Model Type" SortExpression="ParmCode"
                                                                Visible="False"></asp:BoundField>
                                                            <asp:BoundField DataField="DisplaySequence" HeaderText="Display Sequence" SortExpression="ParmDesc"
                                                                Visible="False"></asp:BoundField>
                                                            <asp:TemplateField HeaderText="Value">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlValue" runat="server" Font-Names="Times New Roman" 
                                                                        Font-Size="9pt">
                                                                    </asp:DropDownList>
                                                                    <asp:TextBox ID="txtValue" runat="server" CssClass="textbox" Font-Names="Times New Roman"
                                                                        Font-Size="9pt" MaxLength="50" Width="235px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="IsValueTable" HeaderText="IsValueTable" 
                                                                Visible="False">
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ParmUOM" HeaderText="UOM"></asp:BoundField>
                                                            <asp:BoundField DataField="ParmRemarks" HeaderText="Comments"></asp:BoundField>
                                                            <asp:TemplateField HeaderText="Non Std">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbNS" runat="server" AutoPostBack="True" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="IsAllowBlank" HeaderText="Is Allow Blank">
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                                        <HeaderStyle BackColor="#3399FF" Font-Bold="True" ForeColor="White" 
                                                            HorizontalAlign="Left" />
                                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                        <RowStyle Font-Size="8pt" ForeColor="#000066" />
                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                    </asp:GridView>
                                                    </asp:Panel> 
                                                     </div>

</div> 
              </td>
            <td style="height: 23px">
            </td>
        </tr>
    </table> 
    </ContentTemplate>
  </asp:UpdatePanel>   

    </div>
    </form>
</body>
</html>
