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
using System.Configuration;

namespace NewOrderingSystem
{
    public partial class ViewOutput : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.DropDownList DropDownList1;
        protected System.Web.UI.WebControls.DropDownList DropDownList2;
        protected System.Web.UI.WebControls.DropDownList DropDownList3;

        DataTable dtContractNo = new DataTable();
        DataTable dtUsers = new DataTable();

        private System.Messaging.MessageQueue MtlCEQ = new System.Messaging.MessageQueue();
        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            //this.grViewOutput.ItemDataBound += new C1.Web.C1WebGrid.C1ItemEventHandler(this.grViewOutput_ItemDataBound);

        }
        #endregion


        protected void Page_Load(object sender, System.EventArgs e)
        {
            Session["REP"] = null;
            if (Session["UserId"] == null)
                Response.Redirect("Login.aspx");
            btnUpdateCost.Click += new EventHandler(btnUpdateCost_Click);
            lnkBtnUpdateCost.Click += new EventHandler(lnkBtnUpdateCost_Click);
            InitProperties();
            lblVersion.Text = ConfigurationSettings.AppSettings["NOSVersion"].ToString();
            if (!IsPostBack)
            {
                lblUserName.Text = "WELCOME " + Session["UserName"].ToString();
                HidSearchText.Value = txtSe.Text.Trim();
                BindGrViewOutput(txtSe.Text.Trim());
            }
        }

        void lnkBtnUpdateCost_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedStr=null;
                for (int i = 0; i < cbUCLifts.Items.Count; i++)
                {
                    if (cbUCLifts.Items[i].Selected)
                    {
                        selectedStr = (string.IsNullOrEmpty(selectedStr) ? Convert.ToString(cbUCLifts.Items[i].Value) : selectedStr + "," + Convert.ToString(cbUCLifts.Items[i].Value));
                    }
                }
                if(!string.IsNullOrEmpty(selectedStr))
                {
                    selectedStr = selectedStr + ",";
                    BLL.ViewOutPut.UpdateCost(selectedStr, Convert.ToString(Session["UserId"]));
                    Response.Write("<Script>alert('Cost updated successfully');location.href='ViewOutput.aspx';</Script>");
                }
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        void btnUpdateCost_Click(object sender, EventArgs e)
        {
            try
            {
                AjaxControlToolkit.ModalPopupExtender modalPop = ((AjaxControlToolkit.ModalPopupExtender)
                                                    (this.FindControl("mpeUpdateCost")));
                PopulateProjectLifts(HidContractNo.Value);
                lblUCProjectInfo.Text = "Contract No : <b> " + HidContractNo.Value + "</b>";
                modalPop.Show();



            }
            catch (Exception ex)
            {
                lblError.Text = "Error : " + ex.Message;
            }
        }

        private void InitProperties()
        {
            btnDelete.Attributes.Add("OnClick", "return CanProcess('DELETE');");
            btnChangeInputData.Attributes.Add("OnClick", "return CanProcess('CHANGEINPUTDATA');");
            btnCopy.Attributes.Add("OnClick", "return CanProcess('COPY');");
            btnCopy.Attributes.Add("OnClick", "return OpenWindowForCopy('Copy.aspx');");
            btnCompute.Attributes.Add("OnClick", "return CanProcess('COMPUTE');");
            btnRelease.Attributes.Add("OnClick", "return CanProcess('RELEASE');");
            btnAct.Attributes.Add("OnClick", "return CanProcess('ACTIVATE');");
            btnMaintainZeroCost.Attributes.Add("OnClick", "return IsItemSelected();");

        }

        private void BindGrViewOutput(string ContractNo)
        {
            DataTable dtViewOutput = new DataTable();
            lblError.Text = "";
            try
            {
                dtViewOutput = BLL.ViewOutPut.GetViewOutput(ContractNo, Session["UserName"].ToString()).Tables[0];
                if (dtViewOutput.Rows.Count > 0)
                {
                    grViewOutput.DataSource = dtViewOutput;
                    grViewOutput.DataBind();
                    //grViewOutputNew.DataSource=dtViewOutput;
                    //grViewOutputNew.DataBind();
                }
                else
                {
                    grViewOutput.DataSource = dtViewOutput;
                    grViewOutput.DataBind();
                    lblError.Text = "The List is empty...";
                }
                ////				MakeVisibleButtons();
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


        protected void btnRefresh_Click(object sender, System.EventArgs e)
        {
            lblError.Text = "";
            BindGrViewOutput(HidSearchText.Value);
            HidRefNo.Value = "";
        }
        private void ViewReport(string OutputType)
        {
            lblError.Text = "";
            Session["RefNo"] = HidRefNo.Value;
            Session["Mode"] = HidMode.Value;
            Session["LoadContract"] = HidContractNo.Value;
            if (OutputType == "1")
            {
                if (HidStatus.Value.ToUpper() == "JOBQUEUE" || HidStatus.Value.ToUpper() == "PROCESSING") //check for Show Output or not
                {
                    lblError.Text = "Contract is in " + HidStatus.Value + " for Selected item, You cannot Output at this time";
                    HidRefNo.Value = "";
                    return;
                }
                else
                {
                    Response.Redirect("Output.aspx?Mode=" + Session["Mode"].ToString() + "&Model=" + HidModel.Value + "&OutputType=" + OutputType + "&RevisionNo=''&IssueName=''&IssueDate=''&CheckName=''&CheckDate=''&ApproverName=''&ReportFor=P&ContractNo=" + HidContractNo.Value);
                    HidRefNo.Value = "";
                }
            }
            else
            {
                if (HidStatus.Value.ToUpper() != "SAVED") //check for Show Output or not
                {
                    lblError.Text = "You can view cost data, only after SAVE";
                    HidRefNo.Value = "";
                    return;
                }
                else
                {
                    Response.Redirect("Output.aspx?Mode=" + Session["Mode"].ToString() + "&Model=" + HidModel.Value + "&OutputType=" + OutputType + "&RevisionNo=''&IssueName=''&IssueDate=''&CheckName=''&CheckDate=''&ApproverName=''&ReportFor=P&ContractNo=" + HidContractNo.Value);
                    HidRefNo.Value = "";
                }
            }

        }

        protected void btnDelete_Click(object sender, System.EventArgs e)
        {
            lblError.Text = "";
            /*if (HidStatus.Value.ToUpper()=="PROCESSING") 
            {		
                lblError.Text ="Contract is in "+ HidStatus.Value +" for Selected item, You cannot Delete at this time";				
                HidRefNo.Value="";	
                return;
            }
            else
            {
                if (HidIsSaveData.Value.ToUpper()=="TRUE")
                {
                    string Status;
                    Status=BLL.MainScreen.GetStatuTempMaster(Convert.ToInt32(HidRefNo.Value));
                    if (Status.ToUpper()=="PROCESS")
                        lblError.Text ="Contract is in Precessing for Selected item,You cannot Delete at this time";
                    else
                    {						
                        //BLL.MainScreen.UpdateContractMaster(Convert.ToInt32(HidRefNo.Value),Session["CountryCode"].ToString(),Session["DeptCode"].ToString(),HidModel.Value,Session["UserName"].ToString(),Convert.ToInt32(ConfigurationSettings.AppSettings["CONTRACTNO"]),Convert.ToInt32(ConfigurationSettings.AppSettings["LIFTNO"]));
                        //BLL.MainScreen.UpdateContractSpecification(Convert.ToInt32(HidRefNo.Value),Session["UserName"].ToString(),HidMode.Value);							
                    }
                }
                else
                {
                    string Result;
                    Result=BLL.ViewOutPut.DeleteTempContract(Convert.ToInt32(HidRefNo.Value),Convert.ToString(HidMode.Value));
                    if (Result.ToUpper()== "CANNOT DELETE")
                        lblError.Text ="Contract is in Precessing for Selected item,You cannot Delete at this time";
                }				
                BindGrViewOutput();
                HidRefNo.Value="";	
            }*/
            try
            {
                BLL.MainScreen.DeleteTempMaster(Convert.ToInt32(HidRefNo.Value));
                Response.Write("<Script>alert('Contracted Deleted successfully');</Script>");
                BindGrViewOutput(txtSe.Text.Trim());
                HidRefNo.Value = "";
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

        protected void btnViewCostData_Click(object sender, System.EventArgs e)
        {
            ViewReport("3");
        }

        //private void grViewOutput_ItemDataBound(object sender, C1.Web.C1WebGrid.C1ItemEventArgs e)
        //{
        //    string strRdButton;
        //    DataRowView dr= (DataRowView) e.Item.DataItem;					
        //    if(e.Item.ItemType==C1ListItemType.AlternatingItem || e.Item.ItemType== C1ListItemType.Item)
        //    {
        //        strRdButton = @"<input type=radio name=rdContract onClick=GetContract('" + dr["RefNo"].ToString().Trim() + "','"+ dr["Mode"].ToString().Trim()+ "','" + dr["Status"].ToString().Trim()+"','" + dr["ModelType"].ToString().Trim() + "','" + Server.HtmlEncode(dr["ContractNo"].ToString().Trim()) +"')>";								
        //        //strRdButton = @"<input type=radio name=rdContract onClick=GetContract('" + dr["RefNo"].ToString().Trim() + "','"+ dr["Mode"].ToString().Trim()+ "','" + dr["Status"].ToString().Trim()+"','" + dr["ModelType"].ToString().Trim() +"')>";								
        //        e.Item.Cells[0].Text = strRdButton;
        //    }
        //}

        protected void btnNew_Click(object sender, System.EventArgs e)
        {
            Session["RefNo"] = null;
            Session["Released"] = false;
            Response.Redirect("MainNewScreen.aspx?Mode=New");
        }


        private void btnGo_Click(object sender, System.EventArgs e)
        {
            /*lblError.Text="";
            Session["LastViewedDept"]=ddlDeptCode.SelectedItem.Text;
            Session["LastViewedUser"]=ddlUser.SelectedItem.Text;
            Session["LastViewedStatus"]=ddlStatus.SelectedValue.ToString();
            BindGrViewOutput();
            MakeVisibleButtons();*/
        }

        protected void btnChangeInputData_Click(object sender, System.EventArgs e)
        {
            Session["RefNo"] = HidRefNo.Value;
            Session["Released"] = BLL.ViewOutPut.IsProjectReleased(Convert.ToInt32(Session["RefNo"]));
            /*if (HidStatus.Value.ToUpper()=="PROCESSING" || HidStatus.Value.ToUpper()=="JOBQUEUE") 
            {		
                lblError.Text ="Contract is in "+ HidStatus.Value +" for Selected item, You cannot Change at this time";				
                HidRefNo.Value="";	
                return;
            }*/

            Session["Mode"] = "LOAD";
            Response.Redirect("MainNEWScreen.aspx?Mode=LOAD&Model=" + HidModel.Value + "&ContractNo=" + HidContractNo.Value);
        }

        //protected void btnSave_Click(object sender, System.EventArgs e)
        //{
        //    Session["RefNo"]=HidRefNo.Value;			
        //    BLL.MainScreen.UpdateStatuTempMaster(Convert.ToInt32(Session["RefNo"]),"SVD",Session["UserName"].ToString());
        //    Response.Write("<script>alert('Datas Saved Successfully');</script>");
        //    BindGrViewOutput(txtSe.Text.Trim());
        //    HidRefNo.Value="";	
        //}

        protected void btnViewInputData_Click(object sender, System.EventArgs e)
        {
            Response.Write("<Script>window.open('ViewReports.aspx?ReportName=Production&SpName=EXEC SP_RptGetScreenTemplateForModel&Model=" + HidModel.Value + "&ContractNo=" + HidContractNo.Value + "')</script>");
        }

        protected void btnCopy_Click(object sender, System.EventArgs e)
        {
            Session["RefNo"] = HidRefNo.Value;
        }

        protected void grViewOutput_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string strRdButton;
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (dr["RefNo"].ToString().Trim() == HidRefNo.Value.ToString().Trim())
                {
                    strRdButton = @"<input type=radio name=rdContract onClick=GetContract('" + dr["RefNo"].ToString().Trim()
                                + "','" + dr["Mode"].ToString().Trim() + "','" + dr["Status"].ToString().Trim()
                                + "','" + dr["ModelType"].ToString().Trim()
                                + "','" + Server.HtmlEncode(dr["ContractNo"].ToString().Trim())
                                + "','" + Server.HtmlEncode(dr["VersionNo"].ToString().Trim())
                                + "','" + e.Row.ClientID + "')  checked='checked' >";
                    HidStatus.Value = dr["Status"].ToString().Trim();



                }
                else
                    strRdButton = @"<input type=radio name=rdContract onClick=GetContract('" + dr["RefNo"].ToString().Trim()
                                + "','" + dr["Mode"].ToString().Trim() + "','" + dr["Status"].ToString().Trim()
                                + "','" + dr["ModelType"].ToString().Trim()
                                + "','" + Server.HtmlEncode(dr["ContractNo"].ToString().Trim())
                                + "','" + Server.HtmlEncode(dr["VersionNo"].ToString().Trim())
                                + "','" + e.Row.ClientID + "')  >";
                //strRdButton = @"<input type=radio name=rdContract onClick=GetContract('" + dr["RefNo"].ToString().Trim() + "','"+ dr["Mode"].ToString().Trim()+ "','" + dr["Status"].ToString().Trim()+"','" + dr["ModelType"].ToString().Trim() +"')>";								
                e.Row.Cells[0].Text = strRdButton;

                if (dr["Status"].ToString().Trim() == "Error")
                    e.Row.ForeColor = Color.Red;
                else if (dr["Status"].ToString().Trim() == "End")
                    e.Row.ForeColor = Color.Green;
                else if (dr["Status"].ToString().Trim() == "Exception")
                    e.Row.ForeColor = Color.Blue;
                else if (dr["Status"].ToString().Trim() == "Activated")
                    e.Row.ForeColor = Color.Purple;

                if (dr["RefNo"].ToString().Trim() == HidRefNo.Value.ToString().Trim())
                {
                    // e.Row.BackColor  = Color.FromName("#BCD0F1");  
                    if (!ClientScript.IsStartupScriptRegistered("GetContract"))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(),
                            "GetContract", "GetContract('" + dr["RefNo"].ToString().Trim()
                                + "','" + dr["Mode"].ToString().Trim() + "','" + dr["Status"].ToString().Trim()
                                + "','" + dr["ModelType"].ToString().Trim()
                                + "','" + Server.HtmlEncode(dr["ContractNo"].ToString().Trim())
                                + "','" + Server.HtmlEncode(dr["VersionNo"].ToString().Trim())
                                + "','" + e.Row.ClientID + "');", true);
                    }
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            HidSearchText.Value = txtSe.Text.Trim();
            BindGrViewOutput(txtSe.Text.Trim());
        }

        protected void btnCompute_Click(object sender, EventArgs e)
        {
            lblError.Text = "";

            //System.Threading.Thread.Sleep(5000);

            if (BLL.MainScreen.IsDTProcessing())
            {
                lblError.Text = "Datatransfer is in processing, please try again later";
                return;
            }
            try
            {
                PopUpComputeMaterialCategory();
                ///// Commented Below By Pradeep S on 04/Mar2015 For Selective Material Computation
                //////BLL.ViewOutPut.UpdateTempMaster4VOCompute(Convert.ToInt32(HidRefNo.Value));
                //////if (lblError.Text == "")
                //////{
                //////    BLL.Login.InsertLogHistory("S", Session["UserId"].ToString(), "JBQ", HidRefNo.Value.ToString());
                //////    MtlCEQ.Path = "FormatName:DIRECT=OS:" + ConfigurationSettings.AppSettings["QueuePath"] + "\\private$\\mtlcequeue";
                //////    MtlCEQ.Send(HidRefNo.Value.ToString() + "^" + ConfigurationSettings.AppSettings["SQLConnWS"] + "^" + ConfigurationSettings.AppSettings["LoadingClassParmCode"], "MtlCEQ");
                //////    BLL.MainScreen.UpdateStatuForCompute(Convert.ToInt32(HidRefNo.Value), "JBQ", Session["UserID"].ToString());
                //////    //lblStatus.Text = "Status : " + BLL.MainScreen.GetStatuTempMaster(Convert.ToInt32(Session["RefNo"]));
                //////    //                    MakeVisibleByStatus(lblStatus.Text);
                //////    Response.Write("<Script>alert('Submited for Computation');location.href='ViewOutput.aspx';</Script>");
                //////    //ShowAlertMessage("Submited for Computation");				
                //////}                
            }
            catch (System.Messaging.MessageQueueException MQEx)
            {
                lblError.Text = "Error in Message Queue : " + MQEx.Message;
            }
            catch (Exception ex)
            {
                lblError.Text = "Error : " + ex.Message;
            }
        }

        protected void grViewOutput_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grViewOutput.PageIndex = e.NewPageIndex;
            BindGrViewOutput(txtSe.Text.Trim());
        }

        //protected void grViewOutput_PreRender(object sender, EventArgs e)
        //{
        //    // You only need the following 2 lines of code if you are not 
        //    // using an ObjectDataSource of SqlDataSource
        //    if (!Page.IsPostBack)
        //    {
        //        BindGrViewOutput(txtSe.Text.Trim());
        //    }
        //    if (grViewOutput.Rows.Count > 0)
        //    {
        //        //This replaces <td> with <th> and adds the scope attribute
        //        grViewOutput.UseAccessibleHeader = true;

        //        //This will add the <thead> and <tbody> elements
        //        grViewOutput.HeaderRow.TableSection = TableRowSection.TableHeader;

        //        //This adds the <tfoot> element. 
        //        //Remove if you don't have a footer row
        //        //gvTheGrid.FooterRow.TableSection = TableRowSection.TableFooter;
        //    }
        //}

        protected void btnRelease_Click(object sender, EventArgs e)
        {
            Response.Redirect("Release.aspx?RefNo=" + HidRefNo.Value + "&ContractNo=" + HidContractNo.Value + "&VersionNo=" + HidVersionNo.Value + "&Model=" + HidModel.Value);
        }

        protected void btnAct_Click(object sender, EventArgs e)
        {

        }
        private void PopUpComputeMaterialCategory()
        {
            try
            {
                AjaxControlToolkit.ModalPopupExtender modalPop = ((AjaxControlToolkit.ModalPopupExtender)
                                                    (this.FindControl("mpeCompute")));
                PopulateMaterialCategory(HidModel.Value);
                PopulateExcludedSets(HidContractNo.Value, HidModel.Value);
                modalPop.Show();
                BindGrViewOutput(HidSearchText.Value);
            }
            catch (SqlException Sqex)
            {
                lblError.ForeColor = Color.Red;
                lblError.Text = "SQL Error : " + Sqex.Message;
            }
            catch (Exception ex)
            {
                lblError.ForeColor = Color.Red;
                lblError.Text = "Error : " + ex.Message;
            }
        }
        protected void btnMaintainZeroCost_Click(object sender, EventArgs e)
        {
            Response.Redirect("ZeroCost.aspx?RefNo=" + HidRefNo.Value + "&ContractNo=" + HidContractNo.Value + "&VersionNo=" + HidVersionNo.Value + "&Model=" + HidModel.Value);
        }
        //Added on 04/Mar/2015 - for Selective MaterialCategory Computaion
        protected void lnkBtnCompute_Click(object sender, EventArgs e)
        {
            try
            {
                if (UpdateMatCatListForCompute())
                {
                    if (UpdateExcludedFunctionIDForCompute())
                    {
                        BLL.ViewOutPut.UpdateTempMaster4VOCompute(Convert.ToInt32(HidRefNo.Value));
                        if (lblError.Text == "")
                        {
                            BLL.Login.InsertLogHistory("S", Session["UserId"].ToString(), "JBQ", HidRefNo.Value.ToString());
                            //psp need to uncomment later
                            if (!Convert.ToBoolean(ConfigurationSettings.AppSettings["NOSComputeManually"]))
                            {
                                //MtlCEQ.Path = "FormatName:DIRECT=OS:" + ConfigurationSettings.AppSettings["QueuePath"] + "\\private$\\mtlcequeue";
                                MtlCEQ.Path = ConfigurationSettings.AppSettings["QueuePathCE"];
                                MtlCEQ.Send(HidRefNo.Value.ToString() + "^" + ConfigurationSettings.AppSettings["SQLConnWS"] + "^" + ConfigurationSettings.AppSettings["LoadingClassParmCode"], "MtlCEQ");
                            }
                            BLL.MainScreen.UpdateStatuForCompute(Convert.ToInt32(HidRefNo.Value), "JBQ", Session["UserID"].ToString());
                            Response.Write("<Script>alert('Submited for Computation');location.href='ViewOutput.aspx';</Script>");

                        }
                    }
                    else
                        Response.Write("<Script>alert('Failed to Submit for Computation');location.href='ViewOutput.aspx';</Script>");
                }
                else
                    Response.Write("<Script>alert('Failed to Submit for Computation');location.href='ViewOutput.aspx';</Script>");
            }
            catch (System.Messaging.MessageQueueException MQEx)
            {
                lblError.Text = "Error in Message Queue : " + MQEx.Message;
            }
            catch (Exception ex)
            {
                lblError.Text = "Error : " + ex.Message;
            }
        }
        //Added on 04/Mar/2015 - for MaterialCategory 
        private void PopulateMaterialCategory(string sModelType)
        {
            DataTable dt = new DataTable();
            dt = BLL.ViewOutPut.GetFunctionNodeForProject(sModelType).Tables[0];
            cbMatCat.DataSource = dt;
            cbMatCat.DataValueField = "NodeID";
            cbMatCat.DataTextField = "Node";
            cbMatCat.DataBind();
        }

        private void PopulateProjectLifts(string refNo)
        {
            DataTable dt = new DataTable();
            dt = BLL.ViewOutPut.GetAllComputedLifts(refNo).Tables[0];
            cbUCLifts.DataSource = dt;
            cbUCLifts.DataValueField = "RefNo";
            cbUCLifts.DataTextField = "ContractNo";
            cbUCLifts.DataBind();
        }
        //--Pradeep S -- Added on 08/June/2015 - for ExcludedComponents 
        private void PopulateExcludedSets(string sContractNo, string sModelType)
        {
            DataTable dt = new DataTable();
            sContractNo = sContractNo.Substring(0, 2);
            dt = BLL.ViewOutPut.GetExcludedSets4Project(sContractNo, sModelType).Tables[0];
            cbExcludedSet.DataSource = dt;
            cbExcludedSet.DataValueField = "MaterialCategoryID";
            cbExcludedSet.DataTextField = "SetName";
            cbExcludedSet.DataBind();
            cbExcludedSetAll.Checked = false;
        }
        //Added on 04/Mar/2015 - for MaterialCategory 
        protected void cbMatCat_DataBound(object sender, EventArgs e)
        {
            foreach (ListItem item in cbMatCat.Items)
            {
                item.Attributes.Add("onclick", "SetCheckBoxListAllStatus('cbMatCat','cbMatCatAll');SetExcludeSetList('cbMatCat','cbMatCatAll','" + item.Text + "')");
                //item.Attributes.Add("MatIDChecked", "0");
            }
        }
        //Added on 15/Sep/2014 - for MaterialCategory 
        private Boolean UpdateMatCatListForCompute()
        {
            Boolean bRetVal = false;
            String sMatCatID = "", sResult = "";
            int nRefNo = Convert.ToInt32(HidRefNo.Value);
            try
            {
                sResult = BLL.ViewOutPut.UpdateProcessFolder(nRefNo, 0, "", "", "D");
                if (sResult.ToUpper() == "SUCCESS")
                {
                    foreach (ListItem lstMatCat in cbMatCat.Items)
                    {
                        if (lstMatCat.Selected)
                        {
                            sMatCatID = lstMatCat.Value;
                            if (!String.IsNullOrEmpty(sMatCatID))
                            {
                                sResult = BLL.ViewOutPut.UpdateProcessFolder(
                                                            nRefNo, Convert.ToInt32(sMatCatID), lstMatCat.Text, HidModel.Value, "U");

                                if (sResult.ToUpper() == "SUCCESS")
                                    bRetVal = true;
                            }

                        }
                    }
                }

            }
            catch (Exception ex)
            {


            }
            return bRetVal;
        }
        //Added on 11/June/2015 - for ExcludedFunctionID for Selective Computation 
        private Boolean UpdateExcludedFunctionIDForCompute()
        {
            Boolean bRetVal = false, bNotSelectedAny = true;
            String sMatCatID = "", sResult = "";
            int nRefNo = Convert.ToInt32(HidRefNo.Value);

            try
            {
                sResult = BLL.ViewOutPut.UpdateExcludedFunctionID(nRefNo, "", "", "D");
                if (sResult.ToUpper() == "SUCCESS")
                {
                    if (cbExcludedSet.Items.Count <= 0)
                        bRetVal = true;
                    foreach (ListItem lstExcludedSet in cbExcludedSet.Items)
                    {
                        if (lstExcludedSet.Selected)
                        {
                            sMatCatID = lstExcludedSet.Value;
                            if (!String.IsNullOrEmpty(sMatCatID))
                            {
                                bNotSelectedAny = false;
                                sResult = BLL.ViewOutPut.UpdateExcludedFunctionID(
                                                            nRefNo, lstExcludedSet.Text, sMatCatID, "U");

                                if (sResult.ToUpper() == "SUCCESS")
                                    bRetVal = true;
                            }

                        }
                    }
                    if (bNotSelectedAny)
                        bRetVal = true;

                }

            }
            catch (Exception ex)
            {


            }
            return bRetVal;
        }
        protected void cbExcludedSet_DataBound(object sender, EventArgs e)
        {
            foreach (ListItem item in cbExcludedSet.Items)
            {
                item.Attributes.Add("onclick", "SetExcludedSetCheckBoxAllStatus('cbExcludedSet','cbExcludedSetAll');");
                item.Attributes.Add("MatID", item.Value);

            }

        }


        protected void cbUCLifts_DataBound(object sender, EventArgs e)
        {
            //foreach (ListItem item in cbMatCat.Items)
            //{
            //    item.Attributes.Add("onclick", "SetCheckBoxListAllStatus('cbUCLifts','cbUCLiftsAll');SetUpdateCostLiftList('cbUCLifts','cbUCLiftsAll','" + item.Text + "')");
            //    //item.Attributes.Add("MatIDChecked", "0");
            //}
        }
    }
}
