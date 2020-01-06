using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;

namespace NewOrderingSystem
{
    public partial class IssuePO : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
                Response.Redirect("Login.aspx");	
            if (!IsPostBack)
            {
                lblUserName.Text = "WELCOME " + Session["UserName"].ToString();	
                //populateContractNo();
                PopulateWBS("");
                InitProperties();
            }
            lblVersion.Text = ConfigurationSettings.AppSettings["NOSVersion"].ToString();
            btnShowComponentReport.Click += new EventHandler(btnShowComponentReport_Click);
        }

        protected void btnShowComponentReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewReports.aspx?ReportName=ComponentReport&StrSql=SP_Rpt_GetComponentKitProject");
        }
        private void InitProperties()
        {
            try
            {
                populateModelTypes();
                populateMaterialCategoryIDs();
            }
            catch (Exception)
            {
                
                
            }
        }
        private void populateContractNo()
        {
            /*
            DataTable dtConNo= new DataTable();
            //dtConNo = BLL.IssuePO.GetProjMstContractNo();

            dtConNo = BLL.IssuePO.GetWBSMasterContractNo(Convert.ToString(Session["UserId"]));
            ddlCNo.DataSource = dtConNo;
            ddlCNo.DataValueField = "ProjRefNo";// "RefNo";
            ddlCNo.DataTextField = "WBSNo";// "ContractNo";
            ddlCNo.DataBind();
            ddlCNo.Items.Insert(0, new ListItem("Select One","0"));
            */
        }

        protected void btnGet_Click(object sender, EventArgs e)
        {
            PopulateWBS(txtSearchContract.Text.Trim());
        }
        private void PopulateWBS(String sSearchContract)
        {
            DataTable dtWBS;
            
            //dtWBS = BLL.IssuePO.GetWBSMaster(Convert.ToInt32(ddlCNo.SelectedValue));
            String sModelType = "", sMatCatID = "";
            if(ddlModelType.Items.Count>0)
                sModelType = ddlModelType.SelectedValue;
            if (ddlMatCatID.Items.Count > 0)
                sMatCatID = ddlMatCatID.SelectedValue;
            //Commented Below on 05/June/2015 By Pradeep S
            //Added two more search filter ModelType and MaterialCategoryID
            //dtWBS = BLL.IssuePO.GetWBSMasterReleaseDetails(Convert.ToString(Session["UserId"]),sSearchContract);
            dtWBS = BLL.IssuePO.GetWBSMasterReleaseDetails(Convert.ToString(Session["UserId"]), sSearchContract, sModelType, sMatCatID);
            if (dtWBS.Rows.Count > 0)
            {
                lblError.Text = "";
                
                grWBS.DataSource = dtWBS;
                grWBS.DataBind();
                PnlBtn.Visible = true;
                
            }
            else
            {
                lblError.Text = "No data found";
                grWBS.DataSource = null;
                grWBS.DataBind();                
                PnlBtn.Visible = false;
                
            }            
        }

        protected void grWBS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string strRdButton;
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                strRdButton = "<input type='radio' name='rdContract' onclick=\"GetWBSDetails('" + 
                                                Server.HtmlEncode(dr["DocumentNo"].ToString().Trim()) + "','" 
                                                + dr["WBSRefNo"].ToString().Trim() + "','" 
                                                + Server.HtmlEncode(dr["WBSNo"].ToString().Trim()) 
                                                + "','" + dr["ModelType"].ToString().Trim() + "','"
                                                + Server.HtmlEncode(dr["MaterialCategoryID"].ToString().Trim()) 
                                                + "','" + dr["VersionNo"].ToString().Trim() + "','" 
                                                + dr["Status"].ToString().Trim() + "','" 
                                                + dr["ReleaseBy"].ToString().Trim() + "','" 
                                                + dr["ReleaseDate"].ToString().Trim() + "','" 
                                                + dr["ProjRefNo"].ToString().Trim() + "','" 
                                                + e.Row.ClientID + "')\">";
                //strRdButton = @"<input type=radio name=rdContract onClick=GetContract('" + dr["RefNo"].ToString().Trim() + "','"+ dr["Mode"].ToString().Trim()+ "','" + dr["Status"].ToString().Trim()+"','" + dr["ModelType"].ToString().Trim() +"')>";								
                e.Row.Cells[0].Text = strRdButton;

                //if (dr["Status"].ToString().Trim() == "Error")
                //    e.Row.ForeColor = Color.Red;
                //else if (dr["Status"].ToString().Trim() == "End")
                //    e.Row.ForeColor = Color.Green;
                //else if (dr["Status"].ToString().Trim() == "Exception")
                //    e.Row.ForeColor = Color.Blue;
                //else if (dr["Status"].ToString().Trim() == "Active")
                //    e.Row.ForeColor = Color.Purple;
            }
        }

        protected void btnERPInterface_Click(object sender, EventArgs e)
        {
            Response.Redirect("ERPInterface.aspx?WBSRefNo=" + HidWBSRefNo.Value+
                                    "&ContractNo=" + HidWBSNo.Value + "&DocumentNo=" + HidDocumentNo.Value + "&VersionNo=" + HidVersionNo.Value);
        }

        protected void grWBS_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grWBS.PageIndex = e.NewPageIndex;
                btnGet_Click(btnGet, EventArgs.Empty);

            }
            catch (Exception)
            {
                                
            }
        }

        protected void btnERPInterfaceStatus_Click(object sender, EventArgs e)
        {
            Response.Redirect("ERPInterfaceStatus.aspx?WBSRefNo=" + HidWBSRefNo.Value);
        }
        private void populateModelTypes()
        {
            
            DataTable dtModelTypes= new DataTable();

            dtModelTypes = BLL.IssuePO.GetWBSMasterReleasedModelTypes(Convert.ToString(Session["UserId"]));
            ddlModelType.DataSource = dtModelTypes;
            ddlModelType.DataValueField = "ModelType";
            ddlModelType.DataTextField = "ModelType";
            ddlModelType.DataBind();
            ddlModelType.Items.Insert(0, new ListItem("Select One", ""));
            
        }
        private void populateMaterialCategoryIDs()
        {

            DataTable dtMatCatIDs = new DataTable();

            dtMatCatIDs = BLL.IssuePO.GetWBSMasterReleasedMatCatIDs(Convert.ToString(Session["UserId"]));
            ddlMatCatID.DataSource = dtMatCatIDs;
            ddlMatCatID.DataValueField = "MaterialCategoryID";
            ddlMatCatID.DataTextField = "MaterialCategoryID";
            ddlMatCatID.DataBind();
            ddlMatCatID.Items.Insert(0, new ListItem("Select One", ""));

        }
    }
}
