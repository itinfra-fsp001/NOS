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
    public partial class ERPInterface : System.Web.UI.Page
    {
        enum ErrorMsg
        {
            ProjectNameNotExist,
            MatCategoryNotExist,
            UpdateSuccess,
            Default
        };
        private Hashtable htProjectNames;        
        private string  sSelectedPrj;
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
                //TODO
                lblUserName.Text = "WELCOME " + Session["UserName"].ToString();
                lblCN.Text = Request.QueryString["ContractNo"];
                lblDocNo.Text = Request.QueryString["DocumentNo"];
                lblVNo.Text = Request.QueryString["VersionNo"];

                LoadProjectNamesAndMatCategory("");
            }
            else
            {
                //TODO
                //if (PopulateProjectGroupNames())
                if(rdbPrjGrpItem.Items.Count>0)
                {
                    cbMatCat_DataBound(cbMatCat, EventArgs.Empty);
                }
                else
                {
                    ResetScreenOnError("",ErrorMsg.ProjectNameNotExist, Color.Red);
                }

            }
            
        }
        private void ResetScreenOnError(String sErrMsg, ErrorMsg err,Color objColor)
        {
            grERPStatus.DataSource = null;
            grERPStatus.DataBind();
            pnlERPStatus.Visible = false;
            //btnBack.Visible = false;
            //btnSav.Visible = false;
            navPnlBtn.Visible = false;
            lblError.ForeColor = objColor;

            switch (err)
            {
                case ErrorMsg.ProjectNameNotExist:
                    lblError.Text = sErrMsg + " Project names doesn't exist to interface!";
                    dvPrjGrpItemMainHolder.Visible = false;
                    dvMatCatMainHolder.Visible = false;
                    btnShowList.Visible = false;
                    break;
                case ErrorMsg.MatCategoryNotExist:
                    lblError.Text = sErrMsg + " Material Category doesn't exist to interface!";
                    dvPrjGrpItemMainHolder.Visible = false;
                    dvMatCatMainHolder.Visible = false;
                    btnShowList.Visible = false;
                    break;
                case ErrorMsg.UpdateSuccess:
                    lblError.Text = "Updation successful...";
                    btnBack_Click(btnBack, EventArgs.Empty);
                    break;
                default:
                    lblError.Text = sErrMsg;
                    break;
            }
        }
        private void InitProperties()
        {
            
            ViewState["WBSRefNo"] = Convert.ToInt32(Request.QueryString["WBSRefNo"]);
            dvPrjGrpItemMainHolder.Visible = true;
            dvMatCatMainHolder.Visible = true;
            btnShowList.Visible = true;
            htProjectNames = new Hashtable();
            sSelectedPrj = "";
        }
        
        
        private void BindERPStatus(int nWBSRefNo,String sProjectName,String sMatCatList)
        {
            DataTable dtERPKitDetails = new DataTable();
            
            try
            {
                dtERPKitDetails = BLL.ERPInterface.GetKitDetailsForERP(nWBSRefNo, sProjectName, sMatCatList).Tables[0];

                if (dtERPKitDetails.Rows.Count > 0)
                {
                    grERPStatus.DataSource = dtERPKitDetails;
                    grERPStatus.DataBind();
                    pnlERPStatus.Visible = true;
                    //btnBack.Visible = true;
                    //btnSav.Visible = true;
                    navPnlBtn.Visible = true;
                    cbMatCat.Enabled = false;
                    cbMatCatAll.Disabled = true;
                    rdbPrjGrpItem.Enabled = false;
                    btnShowList.Enabled = false;
                    
                }
                else
                {
                    btnBack_Click(btnBack, EventArgs.Empty);                  
                    lblError.Text = "The List is empty...";
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
            //string strRdButton;
            //DataRowView dr = (DataRowView)e.Row.DataItem;
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    strRdButton = @"<input type=radio name=rdContract onClick=GetReferenceNumber('" + dr["WBSRefNo"].ToString().Trim() 
            //                        + "','" + e.Row.ClientID + "')>";                
            //    e.Row.Cells[0].Text = strRdButton;
               
            //}
        }
        protected void grERPStatus_PreRender(object sender, EventArgs e)
        {
            // You only need the following 2 lines of code if you are not 
            // using an ObjectDataSource of SqlDataSource
            if (!Page.IsPostBack)
            {
                BindERPStatus(Convert.ToInt32(ViewState["WBSRefNo"]),"","");                
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
        
        protected void btnSav_Click(object sender, EventArgs e)
        {
            try
            {
                lblError.Text = "";
                string sMatCatList = "", sProjectName = "", sResult = "",sERPEBOMMsg="";
                String[] alResult;
                String sMaterialCategoryID,sPartNumber;
                Double lQty;
                int nBatchNo;
                int nWBSRefNo = Convert.ToInt32(ViewState["WBSRefNo"]);
                sProjectName = rdbPrjGrpItem.SelectedValue;
                sMatCatList = GetMatCatList();
                String sUserID = Convert.ToString(Session["UserId"]);
                sResult = BLL.ERPInterface.UpdateERPInterfaceStatus(nWBSRefNo, sProjectName, sMatCatList, grERPStatus.Rows.Count, sUserID);
                alResult = sResult.Split(new String[] {"_BatchNo:"}, StringSplitOptions.None );
                if (alResult[0].ToUpper() == "SUCCESS")
                {
                    nBatchNo = Convert.ToInt32(alResult[1]);
                    foreach (GridViewRow objRow in grERPStatus.Rows)
                    {
                        if (objRow.RowType == DataControlRowType.DataRow)
                        {
                            sPartNumber = objRow.Cells[0].Text;
                            sMaterialCategoryID = objRow.Cells[3].Text;
                            lQty = 0;
                            if (!String.IsNullOrEmpty(objRow.Cells[7].Text))
                            {
                                Double.TryParse(Convert.ToString(objRow.Cells[7].Text), out lQty);
                            }
                            sResult = BLL.ERPInterface.CreateERPEBOMInterface(nBatchNo, sProjectName, sMaterialCategoryID,
                                                        sPartNumber, lQty, sUserID);
                            if (sResult.ToUpper() != "SUCCESS")
                            {
                                sERPEBOMMsg = sERPEBOMMsg + "Failed to insert: <span style='color:green'>MaterialCategoryID:" + sMaterialCategoryID +
                                                        ", sPartNumber:" + sPartNumber + "</span><br/>";
                            }
                        }
                    }
                }
                else
                {
                    if (alResult.Length > 1)
                    {
                        nBatchNo = Convert.ToInt32(alResult[1]);
                        if (nBatchNo == 0)
                        {
                            sERPEBOMMsg = "Failed to insert data into EBOMBatchHeader";
                        }
                        else
                        {
                            sERPEBOMMsg = "Failed to update status in Order Interface for the Batch No:" + nBatchNo + " Generated";
                        }
                    }
                }
                sSelectedPrj = rdbPrjGrpItem.SelectedValue;                
                ResetScreenOnError("", ErrorMsg.UpdateSuccess, Color.Green);
                if(String.IsNullOrEmpty(sERPEBOMMsg))
                    LoadProjectNamesAndMatCategory("<span style='color:green'>Updation successful...</span>");
                else
                    LoadProjectNamesAndMatCategory("<span style='color:red'>"+sERPEBOMMsg+"</span>");

            }            
            catch (SqlException sqEx)
            {
                lblError.Text = "SQL Error : " + sqEx.Message;
                lblError.ForeColor = Color.Red;
            }
            catch (Exception Ex)
            {
                lblError.Text = "Error : " + Ex.Message;
                lblError.ForeColor = Color.Red;
            }            

        }       
        protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {                
                grERPStatus.DataSource = null;
                grERPStatus.DataBind();
                //btnBack.Visible = false;
                //btnSav.Visible = false;
                navPnlBtn.Visible = false;

                cbMatCat.Enabled = true;
                cbMatCatAll.Disabled = false;
                rdbPrjGrpItem.Enabled = true;
                btnShowList.Enabled = true;

                pnlERPStatus.Visible = false;  
                
            }
            catch (Exception)
            {
                                
            }
        }

        protected void btnShowList_Click(object sender, EventArgs e)
        {
            ResetScreenOnError("", ErrorMsg.Default, Color.Green);
            if (IsProjectNameSelected())
            {
                if (IsMaterialCategorySelected())
                {
                    string sMatCatList = "",sProjectName="";
                    int nWBSRefNo = Convert.ToInt32(ViewState["WBSRefNo"]);
                    sProjectName = rdbPrjGrpItem.SelectedValue;
                    sMatCatList = GetMatCatList();
                    BindERPStatus(nWBSRefNo, sProjectName, sMatCatList);
                }
                else
                {
                    ResetScreenOnError("Please select at least one material category",ErrorMsg.Default,Color.Red );
                }
            }
            else
            {
                if (IsMaterialCategorySelected())
                {
                    ResetScreenOnError("Please select at least one project name", ErrorMsg.Default, Color.Red);
                }
                else
                {
                    ResetScreenOnError("Please select at least one project name and one material category", ErrorMsg.Default, Color.Red);
                }
            }
        }
        private Boolean PopulateMaterialCategory()
        {
            Boolean bRetVal = false;
            try
            {
                DataTable dt = new DataTable();
                dt = BLL.ERPInterface.GetMatCategoryFromOrder(Convert.ToInt32(ViewState["WBSRefNo"]), rdbPrjGrpItem.SelectedValue).Tables[0];
                //Test dt = BLL.Release.GetMatCategoryFromOrder(Convert.ToInt32(11)).Tables[0];
                
                DataTable dtTmp, dtTmp2;
                String sProjName ="";
                if (dt.Rows.Count > 0)
                {
                    bRetVal = true;
                    /*
                    foreach (DataRow row in dt.Rows )
                    {
                        if(String.IsNullOrEmpty(sProjName))
                            sProjName=row[0].ToString();
                        if (!htProjectNames.Contains(row[0].ToString()))
                        {
                            dtTmp = new DataTable(row[0].ToString());
                            dtTmp.Columns.Add("MaterialCategoryID", typeof(string));
                            dtTmp.Columns.Add("MaterialCategoryName", typeof(string));
                            htProjectNames.Add(row[0].ToString(), dtTmp);
                        }
                        dtTmp2 = (DataTable)htProjectNames[row[0].ToString()];
                        dtTmp2.Rows.Add(row[1].ToString(), row[2].ToString());
                    }
                    */
                }
                
                //if (htProjectNames.Contains(sProjName))
                //    dt = (DataTable)htProjectNames[sProjName];
                cbMatCat.DataSource = dt;
                cbMatCat.DataValueField = "MaterialCategoryID";
                cbMatCat.DataTextField = "MaterialCategoryName";
                cbMatCat.DataBind();
            }
            catch (Exception)
            {
                
            }
            return bRetVal;
        }
        //Added on 15/Sep/2014 - for MaterialCategory 
        protected void cbMatCat_DataBound(object sender, EventArgs e)
        {
            foreach (ListItem item in cbMatCat.Items)
            {
                item.Attributes.Add("onclick", "SetCheckBoxListAllStatus('cbMatCat','cbMatCatAll');");
            }
        }
        private Boolean PopulateProjectGroupNames()
        {
            Boolean bRetVal = false;
            try
            {
                DataTable dt = new DataTable();
                dt = BLL.ERPInterface.GetProjectNamesFromOrder(Convert.ToInt32(ViewState["WBSRefNo"])).Tables[0];
                rdbPrjGrpItem.DataSource = dt;
                rdbPrjGrpItem.DataValueField = "ProjectName";
                rdbPrjGrpItem.DataTextField = "ProjectName";
                rdbPrjGrpItem.DataBind();
                int nRowIdx = 0;
                Boolean bPrjExist = false;
                if (dt.Rows.Count > 0)
                {
                    bRetVal = true;
                    foreach (DataRow  dr in dt.Rows)
                    {
                        if (Convert.ToString(dr[0]) == sSelectedPrj)
                        {
                            bPrjExist = true;
                            break;
                        }
                        nRowIdx++; 
                    }
                    if(bPrjExist)
                        rdbPrjGrpItem.SelectedIndex = nRowIdx;
                    else
                        rdbPrjGrpItem.SelectedIndex = 0;
                }

            }
            catch (Exception)
            {

            }
            return bRetVal;
        }
        private bool IsMaterialCategorySelected()
        {
            bool Result = false;
            foreach (ListItem item in cbMatCat.Items)
            {
                if (item.Selected)
                {
                    Result = true;
                    break;
                }
            }
            return Result;
        }
        //Added on 17/Sep/2014 - for ProjectNames 
        private bool IsProjectNameSelected()
        {
            bool Result = false;
            if(rdbPrjGrpItem.SelectedIndex>=0)
                Result = true;            
            return Result;
        }
        private string GetMatCatList()
        {
            string sMatCatList = "", sSep = "','";
            foreach (ListItem lstMatCat in cbMatCat.Items)
            {
                if (lstMatCat.Selected)
                {
                    if (sMatCatList == "")
                    {
                        sMatCatList = lstMatCat.Value;
                    }
                    else
                    {
                        sMatCatList += sSep + lstMatCat.Value;
                    }

                }
            }
            return sMatCatList;
        }

        protected void rdbPrjGrpItem_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            if (PopulateMaterialCategory())
            {
                cbMatCatAll.Checked = true;
                if (!ClientScript.IsStartupScriptRegistered("SetAllCheckBoxListChecked"))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(),
                        "SetAllCheckBoxListChecked", "SetAllCheckBoxListChecked('cbMatCat','cbMatCatAll');", true);
                }
            }
            else
            {
                ResetScreenOnError("", ErrorMsg.MatCategoryNotExist, Color.Red);
            }
        }
        private void LoadProjectNamesAndMatCategory(String sMessage)
        {
            try
            {
                if (PopulateProjectGroupNames())
                {
                    if (PopulateMaterialCategory())
                    {
                        cbMatCatAll.Checked = true;
                        if (!ClientScript.IsStartupScriptRegistered("SetAllCheckBoxListChecked"))
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(),
                                "SetAllCheckBoxListChecked", "SetAllCheckBoxListChecked('cbMatCat','cbMatCatAll')", true);
                        }
                    }
                    else
                    {
                        
                        ResetScreenOnError(sMessage, ErrorMsg.MatCategoryNotExist, Color.Red);                        
                    }
                }
                else
                {                    
                       ResetScreenOnError(sMessage, ErrorMsg.ProjectNameNotExist, Color.Red);                    
                }
            }
            catch (Exception ex)
            {                               
                
            }
        }
    }
}
