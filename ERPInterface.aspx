<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ERPInterface.aspx.cs" Inherits="NewOrderingSystem.ERPInterface" %>
<%@ Register Assembly="Utilities" Namespace="Utilities" TagPrefix="cc1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN"  "http://www.w3.org/TR/html4/loose.dtd">

<html>
<head>
		<title>New Ordering System - ERP Interface</title>
		    
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="C#" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta http-equiv="X-UA-Compatible" content="IE=Edge" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		<link href="CSS/Style.css" type="text/css" rel="stylesheet"  />
		<link href="./CSS/MenuButtonStyle.css" type="text/css" rel="stylesheet"/>
		<link rel='stylesheet' type='text/css' href='Styles/StaticHeader.css' />
		<script type='text/javascript' src='Styles/x.js'></script>
        
        <script src="Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
        <script src="Scripts/Common.js" type="text/javascript"></script>
        
		<script type='text/javascript' language="javascript">		    
		    
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
            function fadeInImages()
            {
                try{
                    
		            $("#Image1").fadeOut(300).fadeIn(500);
		            $("#Home_img").fadeOut(300).fadeIn(500);
		            $("#New_img").fadeOut(300).fadeIn(500);
		            $("#Viewoutput_img").fadeOut(300).fadeIn(500);
		            $("#IssuePO_img").fadeOut(300).fadeIn(500);
		            $("#Logout_img").fadeOut(300).fadeIn(500);
		            
                }catch(e){}   
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
		            location.href = 'Login.aspx';
		        }

		    }
		    function GoHome() {
		        window.location = "Home.aspx";
		    }
		    
		    function display(imgName, imgUrl) {
		        if (document.images && typeof imgUrl != 'undefined')
		            document.Form1[imgName].src = imgUrl.src;
		    }
		    function ConfirmLogout() {
		        if (confirm('Sure do you want to logout?'))
		            location.href = "Logout.aspx";
		    }
		    var previousRow;
		    var previousRowBC;
		    function GetReferenceNumber(sWBSRefNo,sRowID) {
		        try {
		            
		            document.Form1.HidWBSRefNo.value = sWBSRefNo;
		            //If last clicked row and the current clicked row are same
		            if (previousRow == sRowID)
		                return; //do nothing
		            //If there is row clicked earlier
		            else if (document.getElementById(previousRow) != null)
		            //change the color of the previous row back to its own color                           
		                document.getElementById(previousRow).style.backgroundColor = previousRowBC;

		            //change the color of the current row to light yellow
		            previousRowBC = document.getElementById(sRowID).style.backgroundColor;
		            document.getElementById(sRowID).style.backgroundColor = "#9CB25F"; //"#D7F5E0";
		            //assign the current row id to the previous row id 
		            //for next row to be clicked
		            previousRow = sRowID;    
		        } catch (e) { }
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
        
        <input id="HidWBSRefNo" type="hidden" name="HidRefNo" runat="server" designtimedragdrop="710" />
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
                                                <img alt="" src="Images/homeNew.png" border="0" id="Home_img" name="Home_img"  /></a><br />
                                            <br />
                                            <a onmouseover="display('New_img', New_over);" onmouseout="display('New_img', New_out);"
                                                href="MainNewScreen.aspx?Mode=New" onfocus="display('New_img', New_over);" onblur="display('New_img', New_out);" tabindex="2">
                                                <img alt="" src="Images/newNew.png" border="0"id="New_img" name="New_img" /></a><br />
                                            <br />
                                            <a onmouseover="display('Viewoutput_img', Viewoutput_over);" onmouseout="display('Viewoutput_img', Viewoutput_out);"
                                                href="ViewOutput.aspx" onfocus="display('Viewoutput_img', Viewoutput_over);" onblur="display('Viewoutput_img', Viewoutput_out);" tabindex="3">
                                                <img alt="" src="Images/outputNew.png" border="0" id="Viewoutput_img" name="Viewoutput_img" /></a><br />
                                            <br />
                                            <a onmouseover="display('IssuePO_img', IssuePO_over);" onmouseout="display('IssuePO_img',IssuePO_out );"
                                                href="IssuePO.aspx" onfocus="display('IssuePO_img', IssuePO_over);" onblur="display('IssuePO_img',IssuePO_out );" tabindex="4">
                                                <img alt="" src="Images/IssuePO.png" border="0"  id="IssuePO_img" name="IssuePO_img"  /></a><br />
                                            <br />
                                            <a onmouseover="display('Logout_img', Logout_over);" onclick="ConfirmLogout();" onmouseout="display('Logout_img', Logout_out);"
                                                onfocus="display('Logout_img', Logout_over);" onblur="display('Logout_img', Logout_out);" tabindex="5" >
                                                <img alt="" src="Images/LogoutNew.png" border="0" id="Logout_img" name="Logout_img"  /></a>
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
                                            <table bgcolor="#EBEBD8" id="tblHeaderContent">
                                                <tr>
                                                    <td colspan="2" style="font-weight: bold;">
                                                        <span >Contract No : &nbsp;</span><asp:Label ID="lblCN" runat="server" ForeColor="Green" Text="Label"></asp:Label>
                                                    </td>                                                    
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="font-weight: bold;">
                                                        <div id="dvProjectNo" runat="server">
                                                            <span lang="en-us">Document No :&nbsp;</span><asp:Label ID="lblDocNo" runat="server" ForeColor="Green" Text=""></asp:Label>
                                                        </div>
                                                        <div id="dvVersionNo" runat="server">
                                                            <span lang="en-us">Version No :&nbsp;</span><asp:Label ID="lblVNo" runat="server" ForeColor="Green" Text="Label"></asp:Label>
                                                        </div>
                                                        
                                                    </td>                                                    
                                                </tr>
                                                <tr> 
                                                    <td>
                                                        <div id="dvPrjGrpItemMainHolder" runat="server" >
                                                            <asp:Label ID="lblPrjGrpItem" Style="font-weight: bold;" runat="server" Text="Project Name List"></asp:Label>
                                                            <br /> Select A Project
                                                            <br />                                                            
                                                            <div id="dvPrjGrpItem" style="height: 120px; font-size: 11px; width: 300px; padding: 5px;
                                                                overflow: auto; border: 1px solid #ccc;">
                                                                <asp:RadioButtonList ID="rdbPrjGrpItem" runat="server" 
                                                                    onselectedindexchanged="rdbPrjGrpItem_SelectedIndexChanged" style="font-size: 11px;" AutoPostBack="true"  >
                                                                </asp:RadioButtonList>                                                                
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div id="dvMatCatMainHolder" runat="server" >
                                                            <asp:Label ID="lblMaterialCategory" Style="font-weight: bold;" runat="server" Text="Material Category List"></asp:Label>
                                                            <br />                                                            
                                                            <input id="cbMatCatAll" type="checkbox" runat="server" onclick="SetAllCheckBoxListChecked('cbMatCat','cbMatCatAll');" />
                                                            Select All Material Category
                                                            <br />
                                                            <div id="dvMaterialCategory" style="height: 120px; font-size: 11px; width: 300px;
                                                                padding: 5px; overflow: auto; border: 1px solid #ccc;">
                                                                <asp:CheckBoxList ID="cbMatCat" runat="server" style="font-size: 11px;" OnDataBound="cbMatCat_DataBound">
                                                                </asp:CheckBoxList>
                                                            </div>
                                                        </div>
                                                        
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <br />
                                                        <asp:LinkButton ID="btnShowList" runat="server" CssClass="LinkButton" 
                                                            Text="Show Details"  onclick="btnShowList_Click"  
                                                            />                                                        
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;<br /> </td>
                                                </tr>
                                            </table>
                                        </td>                                        
                                    </tr>                                  
                                    <tr style="height:100%">
                                        <td >
                                            <asp:Panel ID="pnlERPStatus" runat="server" Style="height: 320px;" ScrollBars="Auto" Visible="False"
                                                BorderStyle="None">
                                                <div id='divContainer' class="table-container12" style="border-style: none;width:99%;overflow:auto" >
                                                    <asp:GridView ID="grERPStatus" AutoGenerateColumns="False" runat="server" 
                                                        CellPadding="2" BorderColor="#CCCCCC" BorderStyle="None" 
                                                        BorderWidth="1px" OnRowDataBound="grERPStatus_RowDataBound" OnPreRender="grERPStatus_PreRender"
                                                         DataKeyNames="WBSRefNo" BackColor="White">
                                                        <RowStyle Font-Size="8pt"  ForeColor="#000066" />
                                                        <Columns>                                                            
                                                            <asp:BoundField HeaderText="Parent Part No." DataField="ParentPartNo" />
                                                            <asp:BoundField HeaderText="Parent Part Desc." DataField="ParentPartDesc" />                                                            
                                                            <asp:BoundField HeaderText="Group" DataField="GroupName" />                                                            
                                                            <asp:BoundField HeaderText="Material Category" DataField="MaterialCategoryID" />
                                                            <asp:BoundField HeaderText="GLCode" DataField="GLcode" Visible="false" />
                                                            <asp:BoundField HeaderText="Drawing No." DataField="DrawingNumber" />
                                                            <asp:BoundField HeaderText="Revision No." DataField="RevisionNo" />
                                                            <asp:BoundField HeaderText="Order Qty." DataField="OrderQty" />
                                                            <asp:BoundField HeaderText="UOM" DataField="UOM" />
                                                            <asp:BoundField HeaderText="Unit Price" DataField="UnitPrice" />
                                                            <asp:BoundField HeaderText="Update Date" DataField="UpdateDate" />
                                                            <asp:BoundField HeaderText="Update By" DataField="UpdateBy" />
                                                        </Columns>
                                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                        <AlternatingRowStyle BackColor="#EBEBD8" />
                                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" 
                                                            BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"/>
                                                    </asp:GridView>
                                                </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr style="height:10px">
                                        <td >
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr style="height:40px">
                                        <td>
                                            <ul id="navPnlBtn" runat="server" class="NavMain" Visible="False">
                                                <li>
                                                    <asp:LinkButton ID="btnBack" runat="server" CssClass="LinkButton" Text="Cancel" 
                                                        OnClick="btnBack_Click" />
                                                </li>
                                                <li>
                                                    <asp:LinkButton ID="btnSav" runat="server" CssClass="LinkButton" Text="Transfer"
                                                        OnClick="btnSav_Click"  />
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
