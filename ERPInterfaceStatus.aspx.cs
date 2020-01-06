using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Drawing;
using System.Configuration;
using Microsoft.VisualBasic.Devices;

namespace NewOrderingSystem
{
    public partial class ERPInterfaceStatus : System.Web.UI.Page
    {
        enum Status
        {
            Error
            ,Processed
            ,Default
        };
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
                Response.Redirect("Login.aspx");
            InitProperties();
            if (!ClientScript.IsStartupScriptRegistered("fadeInImages"))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(),
                    "fadeInImages", "fadeInImages();", true);
            }
            if (!IsPostBack)
            {

                lblUserName.Text = "WELCOME " + Session["UserName"].ToString();
                BindERPInterfaceStatus(Convert.ToInt32(ViewState["WBSRefNo"]));
                //TODO
            }
            else
            {
                //TODO

            }

        }
        private void ResetScreenOnError(String sErrMsg,Color objColor)
        {
            grERPStatus.DataSource = null;
            grERPStatus.DataBind();
            pnlERPStatus.Visible = false;

            lblError.ForeColor = objColor;

            //TODO
        }
        private void InitProperties()
        {
            try
            {
                ViewState["WBSRefNo"] = Convert.ToInt32(Request.QueryString["WBSRefNo"]);
                //TODO
            }
            catch (Exception)
            {

            }

        }
        private void BindERPInterfaceStatus(int nWBSRefNo)
        {
            DataTable dtInterfaceHeaderDetails = new DataTable();

            try
            {
                dtInterfaceHeaderDetails = BLL.ERPInterface.GetEBOMInterfaceHeaderDetails(nWBSRefNo).Tables[0];

                if (dtInterfaceHeaderDetails.Rows.Count > 0)
                {
                    grERPStatus.DataSource = dtInterfaceHeaderDetails;
                    grERPStatus.DataBind();
                    pnlERPStatus.Visible = true;
                    if (grERPStatus.Rows.Count < 10)
                    {
                        pnlERPStatus.Height = Unit.Pixel(grERPStatus.Rows.Count * 30 + 50);
                    }
                    else
                        pnlERPStatus.Height = Unit.Pixel(300);
                   
                }
                else
                {
                    lblError.Text = "Interface details not exist...";
                    lblError.ForeColor = Color.Red;
                }
            }
            catch (SqlException sqEx)
            {
                lblError.Text = "SQL Error : " + sqEx.Message;
            }
            catch (Exception Ex)
            {
                lblError.Text = "Error : " + Ex.Message;
            }
        }
        protected void grERPStatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string strRdButton;
            DataRowView dr = (DataRowView)e.Row.DataItem;
            LinkButton btnBatch, btnTranactions, btnProcessed, btnError;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
                if (dr["BatchNo"].ToString().Trim() == HidBatchNo.Value.ToString().Trim())
                {
                    strRdButton = @"<input type=radio name=rbERPStatusRowSel  onClick=GetBatchNumber('" + dr["BatchNo"].ToString().Trim()
                                    + "','" + e.Row.ClientID + "') checked='checked' >";  
                }
                else
                {
                    strRdButton = @"<input type=radio name=rbERPStatusRowSel  onClick=GetBatchNumber('" + dr["BatchNo"].ToString().Trim()
                                    + "','" + e.Row.ClientID + "')>";  
                }
                e.Row.Cells[0].Text = strRdButton;

                int nNoOfTransactions, nProcessed, nError;
                Int32.TryParse(Convert.ToString(dr["Transactions"]), out nNoOfTransactions);
                Int32.TryParse(Convert.ToString(dr["Processed"]), out nProcessed);
                Int32.TryParse(Convert.ToString(dr["Error"]), out nError);

                btnBatch = (LinkButton)e.Row.FindControl("btnBatch");
                btnBatch.Text = dr["BatchNo"].ToString().Trim();

                btnTranactions = (LinkButton)e.Row.FindControl("btnTranactions");
                btnTranactions.Text = dr["Transactions"].ToString().Trim();
                if (nProcessed == 0)
                {
                    e.Row.Cells[9].Text = "";
                }
                else
                {
                    btnProcessed = (LinkButton)e.Row.FindControl("btnProcessed");
                    btnProcessed.Text = dr["Processed"].ToString().Trim();
                }
                if (nError == 0)
                {
                    e.Row.Cells[11].Text = "";
                }
                else
                {
                    btnError = (LinkButton)e.Row.FindControl("btnError");
                    btnError.Text = dr["Error"].ToString().Trim();
                }
                
                

                if (nNoOfTransactions == nProcessed)
                    e.Row.ForeColor = Color.Green;
                else if (nError >0)
                    e.Row.ForeColor = Color.Red;
                else
                    e.Row.ForeColor = Color.Blue;
                /*
                if (dr["BatchNo"].ToString().Trim() == HidBatchNo.Value.ToString().Trim())
                {

                    if (!ClientScript.IsStartupScriptRegistered("GetBatchNumber"))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(),
                            "GetBatchNumber", "GetBatchNumber('" + dr["BatchNo"].ToString().Trim()
                                    + "','" + e.Row.ClientID + "');", true);
                    }
                }
                */

            }
        }
        protected void btnBatch_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                LinkButton btnBatch =  (LinkButton)(sender);
                int nRowIdx = Convert.ToInt32(Convert.ToString(btnBatch.CommandArgument));
                string sBatchNo =Convert.ToString( grERPStatus.DataKeys[nRowIdx].Values[0]);
                BindERPInterfaceStatusDetails(Convert.ToInt32(sBatchNo),"");
                grdrow.Cells[1].BackColor = Color.FromName("#9CB25F");
                HidSelRowIdx.Value=Convert.ToString(nRowIdx);
                HidSelColIdx.Value = Convert.ToString(1);
                
            }
            catch (Exception)
            {
                
                
            }
        }
        protected void btnTranactions_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                LinkButton btnBatch = (LinkButton)(sender);
                int nRowIdx = Convert.ToInt32(Convert.ToString(btnBatch.CommandArgument));
                string sBatchNo = Convert.ToString(grERPStatus.DataKeys[nRowIdx].Values[0]);
                BindERPInterfaceStatusDetails(Convert.ToInt32(sBatchNo),"");
                grdrow.Cells[7].BackColor = Color.FromName("#9CB25F");
                HidSelRowIdx.Value = Convert.ToString(nRowIdx);
                HidSelColIdx.Value = Convert.ToString(7);
            }
            catch (Exception)
            {


            }
        }
        protected void btnProcessed_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                LinkButton btnBatch = (LinkButton)(sender);
                int nRowIdx = Convert.ToInt32(Convert.ToString(btnBatch.CommandArgument));
                string sBatchNo = Convert.ToString(grERPStatus.DataKeys[nRowIdx].Values[0]);
                BindERPInterfaceStatusDetails(Convert.ToInt32(sBatchNo),"P");
                grdrow.Cells[9].BackColor = Color.FromName("#9CB25F");
                HidSelRowIdx.Value = Convert.ToString(nRowIdx);
                HidSelColIdx.Value = Convert.ToString(9);
            }
            catch (Exception)
            {


            }
        }
        protected void btnError_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                LinkButton btnBatch = (LinkButton)(sender);
                int nRowIdx = Convert.ToInt32(Convert.ToString(btnBatch.CommandArgument));
                string sBatchNo = Convert.ToString(grERPStatus.DataKeys[nRowIdx].Values[0]);
                BindERPInterfaceStatusDetails(Convert.ToInt32(sBatchNo),"E");
                grdrow.Cells[11].BackColor = Color.FromName("#9CB25F");
                HidSelRowIdx.Value = Convert.ToString(nRowIdx);
                HidSelColIdx.Value = Convert.ToString(11);
            }
            catch (Exception)
            {


            }
        }
        protected void grERPStatus_PreRender(object sender, EventArgs e)
        {
            // You only need the following 2 lines of code if you are not 
            // using an ObjectDataSource of SqlDataSource
            if (!Page.IsPostBack)
            {
                BindERPInterfaceStatus(Convert.ToInt32(ViewState["WBSRefNo"]));
            }
            if (grERPStatus.Rows.Count > 0)
            {
                //This replaces <td> with <th> and adds the scope attribute
                grERPStatus.UseAccessibleHeader = true;

                //This will add the <thead> and <tbody> elements
                grERPStatus.HeaderRow.TableSection = TableRowSection.TableHeader;
                //This adds the <tfoot> element. 
                //Remove if you don't have a footer row
                //gvTheGrid.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

        //protected void btnShowDetails_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        BindERPInterfaceStatus(Convert.ToInt32(ViewState["WBSRefNo"]));
        //        BindERPInterfaceStatusDetails(Convert.ToInt32(HidBatchNo.Value.ToString().Trim()));
        //    }
        //    catch (Exception)
        //    {
                
        //        throw;
        //    }
        //}
        private void BindERPInterfaceStatusDetails(int nBatchNo,string sType)
        {
            DataTable dtInterfaceBatchDetails = new DataTable();

            try
            {
                dtInterfaceBatchDetails = BLL.ERPInterface.GetGetEBOMInterfaceBatchDetails(nBatchNo, sType).Tables[0];

                if (dtInterfaceBatchDetails.Rows.Count > 0)
                {
                    grERPStatusDetails.DataSource = dtInterfaceBatchDetails;
                    grERPStatusDetails.DataBind();
                    pnlERPStatusDetails.Visible = true;
                    lblERPStatusDetailsHeader.Visible = true;
                }
                else
                {
                    lblError.Text = "Batch details not exist...";
                    lblError.ForeColor = Color.Red;
                    pnlERPStatusDetails.Visible = false;
                    lblERPStatusDetailsHeader.Visible = false;
                }
            }
            catch (SqlException sqEx)
            {
                lblError.Text = "SQL Error : " + sqEx.Message;
            }
            catch (Exception Ex)
            {
                lblError.Text = "Error : " + Ex.Message;
            }
        }

        protected void grERPStatus_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VIEW")
            {
                LinkButton lnkView = (LinkButton)e.CommandSource;
                string dealId = lnkView.CommandArgument;
            }
        }

        protected void grERPStatusDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                DataRowView drView = (DataRowView)e.Row.DataItem;
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    String sError = Convert.ToString(drView["ErrorLog"]);
                    if (String.IsNullOrEmpty(sError))
                        e.Row.ForeColor = Color.Green;
                    else
                        e.Row.ForeColor = Color.Red;
                }
            }
            catch (Exception)
            {
                
               
            }
        }

        protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                BindERPInterfaceStatus(Convert.ToInt32(ViewState["WBSRefNo"]));                
                int nRowIdx,nColIdx;
                Int32.TryParse(HidSelRowIdx.Value.ToString(), out nRowIdx);
                Int32.TryParse(HidSelColIdx.Value.ToString(), out nColIdx);
                string sBatchNo = Convert.ToString(grERPStatus.DataKeys[nRowIdx].Values[0]);
                
                switch (nColIdx)
                {
                    case 1:
                        BindERPInterfaceStatusDetails(Convert.ToInt32(sBatchNo), "");
                        grERPStatus.Rows[nRowIdx].Cells[nColIdx].BackColor = Color.FromName("#9CB25F");
                        break;
                    case 7:
                        BindERPInterfaceStatusDetails(Convert.ToInt32(sBatchNo), "");
                        grERPStatus.Rows[nRowIdx].Cells[nColIdx].BackColor = Color.FromName("#9CB25F");
                        break;
                    case 9:
                        BindERPInterfaceStatusDetails(Convert.ToInt32(sBatchNo), "P");
                        grERPStatus.Rows[nRowIdx].Cells[nColIdx].BackColor = Color.FromName("#9CB25F");
                        break;
                    case 11:
                        BindERPInterfaceStatusDetails(Convert.ToInt32(sBatchNo), "E");
                        grERPStatus.Rows[nRowIdx].Cells[nColIdx].BackColor = Color.FromName("#9CB25F");
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                
               
            }
        }
    }
}
