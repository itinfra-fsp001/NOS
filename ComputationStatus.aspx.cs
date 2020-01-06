using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
//using C1.Web.C1WebGrid;
using NewOrderingSystem.BLL;

namespace NewOrderingSystem
{
	/// <summary>
	/// Summary description for ComputationStatus.
	/// </summary>
	public class ComputationStatus : System.Web.UI.Page
	{
        //protected System.Web.UI.WebControls.Label lblUserName;
        //protected System.Web.UI.WebControls.Label lblError;
        ////protected C1.Web.C1WebGrid.C1WebGrid wgComputeStatus;
        //protected System.Web.UI.WebControls.Button btnRefresh;
        ////protected C1.Web.C1WebGrid.C1WebGrid C1WebGrid1;
        //static DataSet dsComputeStatus=new DataSet();
		

        //#region Web Form Designer generated code
        //override protected void OnInit(EventArgs e)
        //{
        //    //
        //    // CODEGEN: This call is required by the ASP.NET Web Form Designer.
        //    //
        //    InitializeComponent();
        //    base.OnInit(e);
        //}
		
        ///// <summary>
        ///// Required method for Designer support - do not modify
        ///// the contents of this method with the code editor.
        ///// </summary>
        //private void InitializeComponent()
        //{    
        //    this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
        //    this.Load += new System.EventHandler(this.Page_Load);

        //}
        //#endregion

        //private void Page_Load(object sender, System.EventArgs e)
        //{
        //    if (Session["UserId"]==null)
        //        Response.Redirect("Login.aspx");

        //    if(!IsPostBack)
        //    {
        //        lblUserName.Text= "WELCOME " + Session["UserName"].ToString();	
        //        BingWebGridComputeStatus();
        //    }
        //}
        //private void BingWebGridComputeStatus()
        //{
        //    try
        //    {	
        //        dsComputeStatus=General.GetComputeStatus();			
        //        //wgComputeStatus.DataSource=dsComputeStatus;
        //        //wgComputeStatus.DataBind();									
        //    }
        //    catch(SqlException sqEx)
        //    {
        //        lblError.Text = "Error : " + sqEx.Message;
        //    }						
        //}
		
        ////private void wgComputeStatus_PageIndexChanged(object sender, C1.Web.C1WebGrid.C1PageChangedEventArgs e)
        ////{
        ////    try
        ////    {
        ////        wgComputeStatus.CurrentPageIndex= e.NewPageIndex;
        ////    }
        ////    catch(Exception ex)
        ////    {
        ////        wgComputeStatus.CurrentPageIndex=0;
        ////    }
        ////    BingWebGridComputeStatus();
        ////}
		
        //void UpdateDataView()
        //{
        //    DataSet ds = (DataSet) dsComputeStatus;
        //    DataView dv = ds.Tables[0].DefaultView;

        //    // Apply sort information to the view
        //    dv.Sort = PrepareSortExpression();	

        //    // Re-bind data 
        //    wgComputeStatus.DataSource = dv;
        //    wgComputeStatus.DataBind();
        //}
        //protected String PrepareSortExpression()
        //{
        //    // i.e. INPUT from Attributes  --> lastname,firstname AND asc,desc
        //    // i.e. OUTPUT to the DataView --> lastname asc,firstname desc
        //    String strSortInfo = "";

        //    // Read current settings for fields and orders (DESC/ASC)
        //    String strSortExpressions = (String) ViewState["SortingFields"];
        //    String strSortOrders = (String) ViewState["SortingOrders"];
	
        //    // Build the string to sort the datagrid by
        //    try 
        //    {
        //        Char[] aSplitterChar = new Char[] {','};
        //        String[] aSortExpressions = strSortExpressions.Split(aSplitterChar);
        //        String[] aSortOrders = strSortOrders.Split(aSplitterChar);	
	
        //        for (int i=0; i<aSortExpressions.Length; i++)
        //            strSortInfo += aSortExpressions[i] + " " + aSortOrders[i] + ",";
        //        strSortInfo = strSortInfo.Trim(aSplitterChar);
        //    }
        //    catch {}

        //    // Persists the CURRENT sort expression in Attributes
        //    ViewState["CurrentSortExpression"] = strSortInfo;
        //    return strSortInfo;
        //}
        //protected void ProcessSortExpression(String strNewSortExpr)
        //{
        //    // Internal-use constants
        //    const String ORDER_DESC = " desc";
        //    const String ORDER_ASC = " asc";
        //    const String CONCAT_ORDER_DESC = "desc,";
        //    const String CONCAT_ORDER_ASC = "asc,";
		
        //    // Read current settings for fields and orders (DESC/ASC)
        //    String strSortExpressions = (String) ViewState["SortingFields"];
        //    String strSortOrders = (String) ViewState["SortingOrders"];
		
        //    // Parse the new sorting string. This string can only be one of 
        //    // the strings read from the SortExpression attribute for datagrid's
        //    // columns. It has any of the forms: 
        //    // "FieldName", "FieldName DESC", "FieldName ASC",
        //    // "Field1,Field2 DESC", "Field1 ASC,Field2 DESC", "Field1,Field2".
        //    // The function splits on the comma, trims all strings and 
        //    // builds up the two string to store in the Attributes repository.
	
        //    // Separate field names from the information regarding the order.
        //    // Use strNewSortExpr to build the order-less string of fields 
        //    Char[] aSplitterChar = new Char[] {','};
        //    String[] aSortExpressions = strNewSortExpr.Split(aSplitterChar);

        //    // Needed in case you want to show a glyph on the caption. In this 
        //    // case you need to know about the original expression 
        //    ViewState["ColumnSortExpression"] = strNewSortExpr;
	
        //    // Separates fields from orders and builds up two distinct, comma-sep strings
        //    // i.e. INPUT  --> lastname,firstname DESC
        //    // i.e. OUTPUT --> lastname,firstname AND asc,desc

        //    String strNewSortExpressions = "";
        //    String strNewSortOrders = "";
        //    for (int i=0; i<aSortExpressions.Length; i++)
        //    {
        //        String tmp1 = aSortExpressions[i].ToLower().Trim();
		
        //        int nPos;
        //        if (tmp1.EndsWith(ORDER_DESC))	
        //        {
        //            strNewSortOrders += CONCAT_ORDER_DESC;
        //            nPos = tmp1.LastIndexOf(ORDER_DESC);
        //            strNewSortExpressions += tmp1.Substring(0,nPos).Trim() + ",";
        //        }
        //        else		// ASC or nothing specified, as ASC is the default
        //        {
        //            strNewSortOrders += CONCAT_ORDER_ASC;
        //            try 
        //            {
        //                nPos = tmp1.LastIndexOf(ORDER_ASC);
        //                strNewSortExpressions += tmp1.Substring(0,nPos).Trim() + ",";
        //            }
        //            catch 
        //            {
        //                strNewSortExpressions += tmp1 + ",";
        //            }
        //        }		
        //    }
	
        //    // Cuts the final "," down
        //    strNewSortExpressions = strNewSortExpressions.Trim(aSplitterChar);
        //    strNewSortOrders = strNewSortOrders.Trim(aSplitterChar);
	
        //    // Compare current sorting fields with the new one. If it is 
        //    // the same, and we have sorted already on this fields, 
        //    // then ASC and DESC are inverted in strNewSortOrders.
        //    if (strSortExpressions == strNewSortExpressions && strSortOrders != "")
        //    {
        //        String[] aSortOrders;
        //        aSortOrders = strSortOrders.Split(aSplitterChar);
	
        //        strNewSortOrders = "";
        //        for (int i=0; i<aSortOrders.Length; i++)
        //        {
        //            if (aSortOrders[i] == "desc")
        //                strNewSortOrders += CONCAT_ORDER_ASC; 
        //            else
        //                strNewSortOrders += CONCAT_ORDER_DESC;
        //        }

        //        strNewSortOrders = strNewSortOrders.Trim(aSplitterChar);
        //    }

        //    // Stores the sorting settings to the Attributes repository.
        //    // This information will be retrieved later when the datagrid rebinds
        //    ViewState["SortingFields"] = strNewSortExpressions;
        //    ViewState["SortingOrders"] = strNewSortOrders;
        //}

        //private void wgComputeStatus_GroupColumnMoved(object sender, C1.Web.C1WebGrid.C1GroupColMoveEventArgs e)
        //{
        //    if( e.GroupMove != GroupMoveEnum.FromGroup )
        //    {
        //        e.Column.GroupInfo.HeaderStyle.BackColor = this.wgComputeStatus.GroupingStyle.BackColor;
        //        e.Column.GroupInfo.HeaderStyle.ForeColor = Color.Black;
        //    }
        //    BingWebGridComputeStatus();
        //}

        //private void wgComputeStatus_ColumnMoved(object sender, C1.Web.C1WebGrid.C1ColMoveEventArgs e)
        //{
        //    BingWebGridComputeStatus();
        //}

        //private void btnRefresh_Click(object sender, System.EventArgs e)
        //{
        //    BingWebGridComputeStatus();
        //}
	}
}
