using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using FormulaEvaluator;
using System.Xml;
using System.Web.UI.HtmlControls;
namespace NewOrderingSystem
{
    public static class StringExt
    {
        public static bool IsNumeric(this string text)
        {
            double test;
            return double.TryParse(text, out test);
        }
    }

    public partial class MainNEWScreen : System.Web.UI.Page
    {
        DataTable dtScreenTemplate = new DataTable();
        DataTable dtParmValus = new DataTable();
        private Boolean  bPreRenderGrid=true ;
        private System.Messaging.MessageQueue MtlCEQ =new System.Messaging.MessageQueue();

        private void InitializeComponent()
        {
            this.MtlCEQ.Formatter = new System.Messaging.XmlMessageFormatter(new string[0]);
            this.MtlCEQ.Path = "FormatName:DIRECT=OS:.\\private$\\MtlBOMQueue";
        }
        private void InitProperties()
        {             
            try { Session["Mode"] = Request.QueryString["Mode"].ToString().ToUpper();
          
            }
            catch (Exception ex) { }
            //btnIntCNW.Attributes.Add("OnClick", "OpenWindow('CNWInterface.aspx?JobLoc=" + HidJobLoc.Value + "')");
            //Commented by PSP btnIntCNW.Attributes.Add("OnClick", "OpenWindow()");
            /*try{LoadFrom=Request.QueryString["LoadFrom"].ToString().ToUpper(); 	
            }catch(Exception ex){LoadFrom="";}*/
            ////HidUserDept.Value = Session["DeptCode"].ToString();
////            btnVPT.Attributes.Add("OnClick", "return OpenViewPlatFormTable('" + ConfigurationSettings.AppSettings["PlatFormTableURL"] + "');");
        }

        private void DoValidation(GridView gr, string txtValueName, string DDLName)
        {
            string Error = "";
            if (gr.ID == "grScreenTemplate" || gr.ID == "grModel")// || gr.ID=="grTemplate02" || gr.ID=="grTemplate03" || gr.ID=="grTemplate04" || gr.ID=="grTemplate05" || gr.ID=="grTemplate06" || gr.ID=="grTemplate07")
            {
                for (int i = 0; i < gr.Rows.Count; i++)
                {
                    if (gr.Rows[i].RowType == DataControlRowType.DataRow)
                    {
                        // if (gr.Items[i].Cells[7].Text=="False")
                        if (gr.DataKeys[i].Values[4].ToString() == "False" && gr.Rows[i].Visible == true) // IsAllowBlank
                        {
                            DropDownList ddlValue = (DropDownList)gr.Rows[i].FindControl(DDLName);
                            TextBox txtValue = (TextBox)gr.Rows[i].FindControl(txtValueName);

                            if (ddlValue.SelectedIndex == -1 || ddlValue.SelectedIndex == 0)
                                if (txtValue.Text == "")
                                {
                                    if ((gr.DataKeys[i].Values[0].ToString() == ConfigurationSettings.AppSettings["PORT NAME"]))
                                    {
                                        if (ddlValue.Visible && ddlValue.Enabled)
                                        {
                                            Error += "<font color=#800080>" + gr.Rows[i].Cells[2].Text + "</font> Can not be blank; <BR>";
                                        }
                                    }
                                    else
                                    {
                                        Error += "<font color=#800080>" + gr.Rows[i].Cells[2].Text + "</font> Can not be blank; <BR>";
                                    }
                                    //Error += "<font color=#800080>" + gr.Rows[i].Cells[1].Text + "</font> Can not be blank; <BR>";
                                }
                                else
                                {
                                    HiddenField hdnParmFormat = (HiddenField)gr.Rows[i].FindControl("hdnParmFormat");
                                    if (Convert.ToString(hdnParmFormat.Value).ToLower() == "numeric")
                                    {
                                        if (!StringExt.IsNumeric(txtValue.Text))
                                        {
                                            Error += "<font color=#800080>" + gr.Rows[i].Cells[2].Text + "</font> Can not contain non numeric text; <BR>";
                                        }
                                    }
                                }
                        }
                    }
                    if (gr.ID == "grScreenTemplate")
                    {
                        if ((gr.DataKeys[i].Values[0].ToString() == ConfigurationSettings.AppSettings["PROJECT NO"]) && (gr.Rows[i].Visible == true)) // Check  for Contract No - ParmCode=129 is CONTRACT NO
                        {
                            TextBox txtVal = (TextBox)grScreenTemplate.Rows[i].FindControl("txtValue");
                            //if (txtVal.Text.IndexOf(' ') >= 0 || txtVal.Text.IndexOf('&') >= 0 || txtVal.Text.IndexOf('/') >= 0 || txtVal.Text.IndexOf(':') >= 0 || txtVal.Text.IndexOf('*') >= 0 || txtVal.Text.IndexOf('?') >= 0 || txtVal.Text.IndexOf('"') >= 0 || txtVal.Text.IndexOf('<') >= 0 || txtVal.Text.IndexOf('>') >= 0 || txtVal.Text.IndexOf('|') >= 0 || txtVal.Text.IndexOf('\\') >= 0)
                            if (txtVal.Text.IndexOf('&') >= 0 || txtVal.Text.IndexOf('/') >= 0 || txtVal.Text.IndexOf(':') >= 0 || txtVal.Text.IndexOf('*') >= 0 || txtVal.Text.IndexOf('?') >= 0 || txtVal.Text.IndexOf('"') >= 0 || txtVal.Text.IndexOf('<') >= 0 || txtVal.Text.IndexOf('>') >= 0 || txtVal.Text.IndexOf('|') >= 0 || txtVal.Text.IndexOf('\\') >= 0)
                            {
                                //Error += "<font color=#800080>" + gr.Rows[i].Cells[1].Text + "</font> should not contains space,/,\\,:,*,?,<,>,|,& and double quotes; <BR>";
                                //Error += "<font color=#800080>" + gr.Rows[i].Cells[1].Text + "</font> should not contains /,\\,:,*,?,<,>,|,& and double quotes; <BR>";
                                Error += "<font color=#800080>" + gr.Rows[i].Cells[2].Text + "</font> should not contains /,\\,:,*,?,<,>,|,& and double quotes; <BR>";
                            }
                        }
                        else if ((gr.DataKeys[i].Values[0].ToString() == ConfigurationSettings.AppSettings["CLIENT EQUIPMENT NO"]) && (gr.Rows[i].Visible == true)) //Check for Lift No ParmCode=131 is LIFT NO
                        {
                            TextBox txtVal = (TextBox)grScreenTemplate.Rows[i].FindControl("txtValue");
                            //if (txtVal.Text.IndexOf(' ') >= 0 || txtVal.Text.IndexOf('&') >= 0 || txtVal.Text.IndexOf('/') >= 0 || txtVal.Text.IndexOf(':') >= 0 || txtVal.Text.IndexOf('*') >= 0 || txtVal.Text.IndexOf('?') >= 0 || txtVal.Text.IndexOf('"') >= 0 || txtVal.Text.IndexOf('<') >= 0 || txtVal.Text.IndexOf('>') >= 0 || txtVal.Text.IndexOf('|') >= 0 || txtVal.Text.IndexOf('\\') >= 0)
                            if (txtVal.Text.IndexOf('&') >= 0 || txtVal.Text.IndexOf('/') >= 0 || txtVal.Text.IndexOf(':') >= 0 || txtVal.Text.IndexOf('*') >= 0 || txtVal.Text.IndexOf('?') >= 0 || txtVal.Text.IndexOf('"') >= 0 || txtVal.Text.IndexOf('<') >= 0 || txtVal.Text.IndexOf('>') >= 0 || txtVal.Text.IndexOf('|') >= 0 || txtVal.Text.IndexOf('\\') >= 0)
                            {
                                //Error += "<font color=#800080>" + gr.Rows[i].Cells[1].Text + "</font> should not contains space,/,\\,:,*,?,<,>,|,& and double quotes; <BR>";
                                //Error += "<font color=#800080>" + gr.Rows[i].Cells[1].Text + "</font> should not contains /,\\,:,*,?,<,>,|,& and double quotes; <BR>";
                                Error += "<font color=#800080>" + gr.Rows[i].Cells[2].Text + "</font> should not contains /,\\,:,*,?,<,>,|,& and double quotes; <BR>";
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < gr.Rows.Count; i++)
                {
                    if (gr.Rows[i].RowType  == DataControlRowType.DataRow)
                    {
                        // if (gr.Items[i].Cells[7].Text=="False")
                        if (gr.DataKeys[i].Values[4].ToString() == "False" && gr.Rows[i].Visible == true) // IsAllowBlank
                        {
                            DropDownList ddlValue = (DropDownList)gr.Rows[i].FindControl(DDLName);
                            TextBox txtValue = (TextBox)gr.Rows[i].FindControl(txtValueName);

                            if (ddlValue.SelectedIndex == -1 || ddlValue.SelectedIndex == 0)
                                if (txtValue.Text == "")
                                {
                                    //if (gr.Rows[i].Cells[3].Text.Trim().Length == 0)
                                    Error += "<font color=#800080>" + gr.Rows[i].Cells[2].Text + "</font> Can not be blank; <BR>";
                                    //Error += "<font color=#800080>" + gr.Rows[i].Cells[1].Text + "</font> Can not be blank; <BR>";
                                }
                                else
                                {
                                    HiddenField hdnParmFormat = (HiddenField)gr.Rows[i].FindControl("hdnParmFormat");
                                    if (Convert.ToString(hdnParmFormat.Value).ToLower() == "numeric")
                                    {
                                        if (!StringExt.IsNumeric(txtValue.Text))
                                        {
                                            Error += "<font color=#800080>" + gr.Rows[i].Cells[2].Text + "</font> Can not contain non numeric text; <BR>";
                                        }
                                    }
                                }
                        }
                    }
                }
            }

            if (Error.Length != 0)
            {
                if (gr.ID == "grScreenTemplate")
                {
                    lblError.Text += "<font color=#0000ff>Error in MainScreen : </font><BR>";
                    lblError.Text += Error;
                }
                else if (gr.ID == "grOptionalForMain")
                {
                    lblError.Text += "<font color=#0000ff>Error in MainScreen - Optional : </font><BR>";
                    lblError.Text += Error;
                }
                else if (gr.ID == "grModel")
                {
                    lblError.Text += "<font color=#0000ff>Error in Main Spec : </font><BR>";
                    lblError.Text += Error;
                }
                else if (gr.ID == "grOptionalForModel")
                {
                    lblError.Text += "<font color=#0000ff>Error in Main Spec - Optional : </font><BR>";
                    lblError.Text += Error;
                }
                else if (gr.ID == "grTemplate02")
                {
                    lblError.Text += "<font color=#0000ff>Error in Machine Spec : </font><BR>";
                    lblError.Text += Error;
                }
                else if (gr.ID == "grTemplate03")
                {
                    lblError.Text += "<font color=#0000ff>Error in COP Operation and Function Spec : </font><BR>";
                    lblError.Text += Error;
                }
                else if (gr.ID == "grTemplate04")
                {
                    lblError.Text += "<font color=#0000ff>Error in Hall Spec : </font><BR>";
                    lblError.Text += Error;
                }
                else if (gr.ID == "grTemplate05")
                {
                    lblError.Text += "<font color=#0000ff>Error in Car Spec : </font><BR>";
                    lblError.Text += Error;
                }
                else if (gr.ID == "grTemplate06")
                {
                    lblError.Text += "<font color=#0000ff>Error in Hoistway Spec : </font><BR>";
                    lblError.Text += Error;
                }
                else if (gr.ID == "grTemplate07")
                {
                    lblError.Text += "<font color=#0000ff>Error in Remarks : </font><BR>";
                    lblError.Text += Error;
                }

                //lblErrorExec.Text = lblError.Text;
            }
        }
        

        private void GetModelType()
        {
            for (int i = 0; i < grScreenTemplate.Rows.Count; i++)
            {
                if (grScreenTemplate.DataKeys[i].Values[0].ToString() == ConfigurationSettings.AppSettings["LIFT MODEL"]) // ParmCode=8 is Model
                {
                    TextBox txtVal = (TextBox)grScreenTemplate.Rows[i].FindControl("txtValue");
                    if (txtVal.Text.Length != 0)
                        HidModelType.Value = txtVal.Text;
                    else
                    {
                        DropDownList ddlVal = (DropDownList)grScreenTemplate.Rows[i].FindControl("ddlValue");
                        HidModelType.Value = ddlVal.SelectedItem.Text;
                    }
                }
            }
        }

        private string NewModelType()
        {
            string ModelType = "";
            for (int i = 0; i < grScreenTemplate.Rows.Count; i++)
            {
                if (grScreenTemplate.DataKeys[i].Values[0].ToString() == ConfigurationSettings.AppSettings["LIFT MODEL"]) // ParmCode=348 is Lift Model
                {
                    DropDownList ddlVal = (DropDownList)grScreenTemplate.Rows[i].FindControl("ddlValue");
                    ModelType = ddlVal.SelectedItem.Text;
                }
            }
            return ModelType;
        }

        private string GenerateContractNo()
        {
            string ContractNo = "";

            for (int i = 0; i < grScreenTemplate.Rows.Count; i++)
            {
                if (grScreenTemplate.DataKeys[i].Values[0].ToString() == ConfigurationSettings.AppSettings["PROJECT NO"]) // Check  for Contract No - ParmCode=41 is CONTRACT NO
                {
                    TextBox txtVal = (TextBox)grScreenTemplate.Rows[i].FindControl("txtValue");
                    ContractNo = txtVal.Text;
                }
                else if (grScreenTemplate.DataKeys[i].Values[0].ToString() == ConfigurationSettings.AppSettings["EQUIPMENT NO"]) //Check for Lift No ParmCode=42 is LIFT NO for Common
                {
                    TextBox txtVal = (TextBox)grScreenTemplate.Rows[i].FindControl("txtValue");
                    ContractNo = ContractNo + "-" + txtVal.Text;
                }
            }
            return ContractNo;
        }
        
        private string CommanContractNo()
        {
            string ComContractNo = "";
            for (int i = 0; i < grScreenTemplate.Rows.Count; i++)
            {
                if (grScreenTemplate.DataKeys[i].Values[0].ToString() == ConfigurationSettings.AppSettings["PROJECT NO (FOR COMMON)"]) // Check  for PROJECT NO (FOR COMMON) - ParmCode=391
                {
                    TextBox txtVal = (TextBox)grScreenTemplate.Rows[i].FindControl("txtValue");
                    ComContractNo = txtVal.Text;
                }                
            }
            return ComContractNo;
        }

        private bool chkCommonContractValidation()
        {
            bool sVal = false;
            for (int i = 0; i < grScreenTemplate.Rows.Count; i++)
            {
                if (grScreenTemplate.DataKeys[i].Values[0].ToString() == ConfigurationSettings.AppSettings["PROJECT NO (FOR COMMON)"]) // Check  for PROJECT NO (FOR COMMON) - ParmCode=391
                {
                    sVal =  grScreenTemplate.Rows[i].Visible ;
                  
                }
            }
            return sVal;
        }

        private int GetNoOfEquip()
        {
            string NoOfE = "";     
            int NoofEquip;
            if (rbE.Checked)
            {
                for (int i = 0; i < grScreenTemplate.Rows.Count; i++)
                {
                    if (grScreenTemplate.DataKeys[i].Values[0].ToString() == ConfigurationSettings.AppSettings["NO OF EQUIPMENTS"]) // ParmCode=377 NO OF EQUIPMENTS
                    {
                        TextBox txtVal = (TextBox)grScreenTemplate.Rows[i].FindControl("txtValue");
                        NoOfE = txtVal.Text;
                    }
                }
            }
            else
            {
                for (int i = 0; i < grScreenTemplate.Rows.Count; i++)
                {
                    if (grScreenTemplate.DataKeys[i].Values[0].ToString() == ConfigurationSettings.AppSettings["NO OF COMMON EQUIPMENTS"]) // ParmCode=410 NO OF EQUIPMENTS
                    {
                        TextBox txtVal = (TextBox)grScreenTemplate.Rows[i].FindControl("txtValue");
                        NoOfE = txtVal.Text;
                    }
                }
            }
            try
            {
                NoofEquip = Convert.ToInt32(NoOfE);
            }
            catch (Exception)
            {
                NoofEquip=0;
            }
            return NoofEquip;
        }

        private void GetJobLocation()
        {
            string JobLoc = "";
            string JobCode = "";

            for (int i = 0; i < grScreenTemplate.Rows.Count; i++)
            {
                if (grScreenTemplate.DataKeys[i].Values[0].ToString() == ConfigurationSettings.AppSettings["PROJECT LOCATION"]) // ParmCode=59 is "PROJECT LOCATION"
                {
                    DropDownList ddlVal = (DropDownList)grScreenTemplate.Rows[i].FindControl("ddlValue");
                    JobLoc = ddlVal.SelectedItem.Text;
                    JobCode = ddlVal.SelectedItem.Value;
                }
            }
            HidJobLoc.Value= JobLoc;
            HidJobCode.Value = JobCode;
        }
        private void SaveScreenTemplate(GridView gr, string txtValueName, string DDLName, Int32 RefNo, bool IsOptional)
        {
            string ResultVal;
            try
            {
                for (int i = 0; i < gr.Rows.Count; i++)
                {
                    if (gr.Rows[i].RowType == DataControlRowType.DataRow)
                    {
                        DropDownList ddlValue = (DropDownList)gr.Rows[i].FindControl(DDLName);
                        TextBox txtValue = (TextBox)gr.Rows[i].FindControl(txtValueName);
                        CheckBox cb = (CheckBox)gr.Rows[i].FindControl("cbNS");
                        if (cb.Checked)
                        {
                            ResultVal = txtValue.Text == "" ? "NULL" : txtValue.Text;
                            //BLL.MainScreen.InsertTempSpec(RefNo,Convert.ToInt32(gr.Items[i].Cells[0].Text),txtValue.Text,Convert.ToInt32(gr.Items[i].Cells[3].Text),gr.Items[i].Cells[2].Text,Convert.ToBoolean(gr.Items[i].Cells[5].Text));
                            BLL.MainScreen.InsertTempSpec(RefNo, Convert.ToInt32(gr.DataKeys[i].Values[0].ToString()), txtValue.Text.ToUpper(), Convert.ToInt32(gr.DataKeys[i].Values[2].ToString()), HidModelType.Value, Convert.ToBoolean(gr.DataKeys[i].Values[3].ToString()), true);
                            //BLL.MainScreen.UpdateTempSpec(RefNo, Convert.ToInt32(gr.DataKeys[i].Values[0].ToString()), txtValue.Text, ResultVal, Convert.ToInt32(gr.DataKeys[i].Values[2].ToString()), HidModelType.Value, Convert.ToBoolean(gr.DataKeys[i].Values[3].ToString()), "U");
                        }
                        else if (ddlValue.SelectedIndex != -1 && ddlValue.SelectedIndex != 0)
                        {

                            if (gr.ID == "grScreenTemplate")
                            {
                                if ((gr.DataKeys[i].Values[0].ToString() == ConfigurationSettings.AppSettings["PORT NAME"]))
                                {
                                    ResultVal = txtValue.Text == "" ? "NULL" : txtValue.Text;
                                    BLL.MainScreen.InsertTempSpec(RefNo, Convert.ToInt32(gr.DataKeys[i].Values[0].ToString()), txtValue.Text.ToUpper(), Convert.ToInt32(gr.DataKeys[i].Values[2].ToString()), HidModelType.Value, Convert.ToBoolean(gr.DataKeys[i].Values[3].ToString()), false);	
                                }
                                else
                                {
                                    ResultVal = ddlValue.SelectedItem.Text == "" ? "NULL" : ddlValue.SelectedItem.Text;
                                    BLL.MainScreen.InsertTempSpec(RefNo, Convert.ToInt32(gr.DataKeys[i].Values[0].ToString()), ddlValue.SelectedItem.Text.ToUpper(), Convert.ToInt32(gr.DataKeys[i].Values[2].ToString()), HidModelType.Value, Convert.ToBoolean(gr.DataKeys[i].Values[3].ToString()), false);
                                }
                            }
                            else
                            {
                                ResultVal = ddlValue.SelectedItem.Text == "" ? "NULL" : ddlValue.SelectedItem.Text;
                                //BLL.MainScreen.InsertTempSpec(RefNo,Convert.ToInt32(gr.Items[i].Cells[0].Text),ddlValue.SelectedItem.Text,Convert.ToInt32(gr.Items[i].Cells[3].Text),gr.Items[i].Cells[2].Text,Convert.ToBoolean(gr.Items[i].Cells[5].Text));	
                                BLL.MainScreen.InsertTempSpec(RefNo, Convert.ToInt32(gr.DataKeys[i].Values[0].ToString()), ddlValue.SelectedItem.Text.ToUpper(), Convert.ToInt32(gr.DataKeys[i].Values[2].ToString()), HidModelType.Value, Convert.ToBoolean(gr.DataKeys[i].Values[3].ToString()), false);
                                //BLL.MainScreen.UpdateTempSpec(RefNo, Convert.ToInt32(gr.DataKeys[i].Values[0].ToString()), ddlValue.SelectedItem.Text, ResultVal, Convert.ToInt32(gr.DataKeys[i].Values[2].ToString()), HidModelType.Value, Convert.ToBoolean(gr.DataKeys[i].Values[3].ToString()), "U");
                            }
                        }
                        else
                        {
                            ResultVal = txtValue.Text == "" ? "NULL" : txtValue.Text;
                            //BLL.MainScreen.InsertTempSpec(RefNo,Convert.ToInt32(gr.Items[i].Cells[0].Text),txtValue.Text,Convert.ToInt32(gr.Items[i].Cells[3].Text),gr.Items[i].Cells[2].Text,Convert.ToBoolean(gr.Items[i].Cells[5].Text));
                            BLL.MainScreen.InsertTempSpec(RefNo, Convert.ToInt32(gr.DataKeys[i].Values[0].ToString()), txtValue.Text.ToUpper(), Convert.ToInt32(gr.DataKeys[i].Values[2].ToString()), HidModelType.Value, Convert.ToBoolean(gr.DataKeys[i].Values[3].ToString()),false);	
                            //BLL.MainScreen.UpdateTempSpec(RefNo, Convert.ToInt32(gr.DataKeys[i].Values[0].ToString()), txtValue.Text, ResultVal, Convert.ToInt32(gr.DataKeys[i].Values[2].ToString()), HidModelType.Value, Convert.ToBoolean(gr.DataKeys[i].Values[3].ToString()), "U");
                        }
                    }
                }
            }
            catch (SqlException sqEx)
            {
                lblError.Text = "SQL Error : " + sqEx.Message;
            }
            catch (Exception ex)
            {
                lblError.Text = "Error : " + ex.Message;
            }
        }

        private void SetHeading()
		{
            if (grScreenTemplate.Visible)
            {
                lblHeading.Text = "Main Screen";
                //SetActiveClass("btnNextMain", "NavMain", "active");
            }
            else if (grModel.Visible)
                lblHeading.Text = "Model : " + HidModelType.Value + " <font color=#8B008B> - Main Specification</font>";                        
            else if (grTemplate02.Visible)
                lblHeading.Text = "Model : " + HidModelType.Value + " <font color=#8B008B> - Machine Spec</font>";
            else if (grTemplate03.Visible)
                lblHeading.Text = "Model : " + HidModelType.Value + " <font color=#8B008B> - COP Operation and Function Spec</font>";
            else if (grTemplate04.Visible)
                lblHeading.Text = "Model : " + HidModelType.Value + " <font color=#8B008B> - Hall Spec</font>";
            else if (grTemplate05.Visible)
                lblHeading.Text = "Model : " + HidModelType.Value + " <font color=#8B008B> - Car Spec</font>";
            else if (grTemplate06.Visible)
                lblHeading.Text = "Model : " + HidModelType.Value + " <font color=#8B008B> - Hoistway Spe</font>c";
            else if (grTemplate07.Visible)
                lblHeading.Text = "Model : " + HidModelType.Value + " <font color=#8B008B> - Remarks</font>";

            if (Session["Mode"].ToString().ToUpper() != "NEW")
            lblHeading.Text = " <font color=#3333CC> Contract No: "+ HidContract.Value + "</font> - " + lblHeading.Text;			
            

		}

        //private string EvaluateFunctionGroup(Int32 RefNo, string FunctionGroup)
        //{
        //    int i;
        //    int j;
        //    string res = "";
        //    ArrayList FunctionGroupResult;
        //    //CNWComputeEngine.clsFunction objFunction = new CNWComputeEngine.clsFunction(ConfigurationSettings.AppSettings["SQLConnWS"]);			
        //    MtlBOMComputeEngine.clsFunction objFunction = new MtlBOMComputeEngine.clsFunction(ConfigurationSettings.AppSettings["SQLConnWS"]);
        //    try
        //    {
        //        // EVALUATION
        //        FunctionGroupResult = objFunction.GetFunctionGroupResult(RefNo, FunctionGroup, 4);
        //        if ((objFunction.Exception == ""))
        //        {
        //            // FunctionResult = objFunction.FunctionGroupResult
        //        }
        //        else
        //        {
        //            res = objFunction.Exception;
        //        }
        //        // RESULTS
        //        if ((FunctionGroupResult.Count > 0))
        //        {
        //            for (i = 0; i <= FunctionGroupResult.Count - 1; i++)
        //            {
        //                ArrayList ArrObjI = (ArrayList)FunctionGroupResult[i];
        //                for (j = 0; j <= ArrObjI.Count - 1; j++)
        //                {
        //                    if (((Microsoft.VisualBasic.Collection)ArrObjI[j])[3].ToString() != "OK")
        //                    {
        //                        res += ((Microsoft.VisualBasic.Collection)ArrObjI[j])[3].ToString().Trim() + "<BR>";
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            res = "";
        //        }
        //        return res;
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}		 

        private void MakeVisibleByStatus(string P_Status)
        {
            switch (P_Status.ToUpper())
            {
                case "STATUS : END":
                    MakeVisibleButtons(true, false, true, true, false);
                    break;
                case "STATUS : ERROR":
                    MakeVisibleButtons(true, true, false, true, false);
                    break;
                case "STATUS : PROCESS":
                    MakeVisibleButtons(false, true, false, true, false);
                    break;
                case "STATUS : JOB QUEUE":
                    MakeVisibleButtons(false, true, false, true, false);
                    break;
                case "INPUTMAIN":
                    MakeVisibleButtons(false, false, false, false, true);
                    break;
                case "INPUTMODEL":
                    MakeVisibleButtons(true, false, true, true, false);
                    break;
                default:
                    MakeVisibleButtons(true, false, true, false, false);
                    break;
            }
            if (lblStatus.Text.ToUpper() == "STATUS : ")
                lblStatus.Text = "";
        }

        private void MakeVisibleButtons(bool P_Execute, bool P_Refresh, bool P_btnOptionalModel, bool P_pnlModelBtn, bool P_pnlExecute)
        {
					
            btnRefreshModel.Visible = P_Refresh;             
////            btnOptionalModel.Visible = false;
            pnlModelBtn.Visible = P_pnlModelBtn;
 
        }

        private void MakeVisible(bool p_pnlMain, bool p_pnlModel, bool p_pnlTemplate02, bool p_pnlTemplate03, bool p_pnlTemplate04, bool p_pnlTemplate05, bool p_pnlTemplate06, bool p_pnlTemplate07, bool p_pnlModelBtn, bool p_pnlExecute)
        {
            pnlMain.Visible = p_pnlMain;
            pnlModel.Visible = p_pnlModel;            
            pnlTemplate02.Visible = p_pnlTemplate02;
            pnlTemplate03.Visible = p_pnlTemplate03;
            pnlTemplate04.Visible = p_pnlTemplate04;
            pnlTemplate05.Visible = p_pnlTemplate05;
            pnlTemplate06.Visible = p_pnlTemplate06;
            pnlTemplate07.Visible = p_pnlTemplate07;
            pnlModelBtn.Visible = p_pnlModelBtn;
            PnlExecute.Visible = p_pnlExecute;
        }	

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
                Response.Redirect("Login.aspx");

            InitProperties();
            lblVersion.Text = ConfigurationSettings.AppSettings["NOSVersion"].ToString();
            if (!IsPostBack)
            {
                lblUserName.Text = "WELCOME " + Session["UserName"].ToString();
                if (Session["Mode"].ToString().ToUpper() == "NEW")
                {
                     Session["RefNo"] = 0;
                     Session["Released"] = false ;   
                    if (rbE.Checked)
                        BindGrids(grScreenTemplate, "MAIN SCREEN", "", 0);
                    else
                        BindGrids(grScreenTemplate, "MAIN SCREEN COMMON", "", 0);

                    //populate the record for ModelType
                    //BindGridForLoad(grModel, HidModelType.Value, HidContract.Value, 1);
                    
                    BindGridForLoad(grTemplate02, HidModelType.Value, "", 2);
                    BindGridForLoad(grTemplate03, HidModelType.Value, "", 3);
                    BindGridForLoad(grTemplate04, HidModelType.Value, "", 4);
                    BindGridForLoad(grTemplate05, HidModelType.Value, "", 5);
                    BindGridForLoad(grTemplate06, HidModelType.Value, "", 6);
                    BindGridForLoad(grTemplate07, HidModelType.Value, "", 7);

                    MakeVisible( true, false,  false, false, false, false, false, false, false, false);
                    SetHeading();
                    MakeVisibleByStatus(lblStatus.Text);
                }
                else
                {
                    HidModelType.Value = Request.QueryString["MODEL"].ToString();
                    HidContract.Value = Request.QueryString["ContractNo"].ToString();

                    if (HidModelType.Value.ToUpper() == "COMMON")
                    {
                        rbE.Checked = false;
                        rbC.Checked = true;
                    }
                    else
                    {
                        rbE.Checked = true;
                        rbC.Checked = false;
                    }

                    if (rbE.Checked)
                        BindGridForLoad(grScreenTemplate, "MAIN SCREEN", HidContract.Value, 0);
                    else
                        BindGridForLoad(grScreenTemplate, "MAIN SCREEN COMMON", HidContract.Value, 0);
                    //populate the record for ModelType
                    BindGridForLoad(grModel, HidModelType.Value, HidContract.Value, 1);

                    BindGridForLoad(grTemplate02, HidModelType.Value, HidContract.Value, 2);
                    BindGridForLoad(grTemplate03, HidModelType.Value, HidContract.Value, 3);
                    BindGridForLoad(grTemplate04, HidModelType.Value, HidContract.Value, 4);
                    BindGridForLoad(grTemplate05, HidModelType.Value, HidContract.Value, 5);
                    BindGridForLoad(grTemplate06, HidModelType.Value, HidContract.Value, 6);
                    BindGridForLoad(grTemplate07, HidModelType.Value, HidContract.Value, 7);

                    MakeVisible( true, false,  false, false, false, false, false, false, false, false);
                    SetHeading();

                    //BLL.MainScreen.UpdateStatuTempMaster(Convert.ToInt32(Session["RefNo"]), "", Session["UserName"].ToString());
                    lblStatus.Text = "Status : " + BLL.MainScreen.GetStatuTempMaster(Convert.ToInt32(Session["RefNo"]));
                    MakeVisibleByStatus("");

                    rbE.Enabled = false;
                    rbC.Enabled = false;
                }
            }
        }

        private void BindGrids(GridView gr, string ModelType, string ContractNo, Int16 SpecID)
        {
            try
            {
                dtScreenTemplate = BLL.MainScreen.GetScreenTemplate(ModelType, ContractNo, SpecID).Tables[0];
                gr.DataSource = dtScreenTemplate;
                gr.DataBind();
                if (gr.ID == "grScreenTemplate")
                {
                    SetProjectLocationIndexChange();
                }
            }
            catch (SqlException sqEx)
            {
                lblError.Text = "SQLError : " + sqEx.Message;
            }
            catch (Exception Ex)
            {
                lblError.Text = "Error : " + Ex.Message;
            }
        }

        private void BindGridForLoad(GridView gr, string ModelType, string ContractNo, Int16 SpecID)
        {
            try
            {
                //gr.DataSource=BLL.MainScreen.FetchTempSpec(RefNo,IsForLoad,ModelType,Optional,HideFlag).Tables[0];
                gr.DataSource = BLL.MainScreen.GetScreenTemplate(ModelType, ContractNo, SpecID).Tables[0];
                gr.DataBind();
                BLL.FormulaEval obj = BLL.FormulaEval.GetInstance();
                XmlNodeList ndlstNAGrid = obj.getNAGrid("NAGrids");
                String sNAGridID = "";
                foreach (XmlNode ndNAGrid in ndlstNAGrid)
                {
                    sNAGridID = ndNAGrid.Attributes.GetNamedItem("id").Value.ToString();
                    if (gr.ID == sNAGridID)
                    {
                        FindNAEvents(gr);
                        break;
                    }
                }
                if (gr.ID == "grScreenTemplate")
                {
                    SetProjectLocationIndexChange();
                }

                
            }
            catch (SqlException sqEx)
            {
                lblError.Text = "SQLError : " + sqEx.Message;
            }
            catch (Exception Ex)
            {
                lblError.Text = "Error : " + Ex.Message;
            }
        }
        private void SetActiveClass(String sID, String sParentClass,String sActiveClass)
        {
            try
            {
                RegisterStartupScript("SetFocusToMainSpec",
                                       "<script language='javascript'> $('#" +
                                       sParentClass + ">li').removeClass('" + sActiveClass + "');" +
                                       "$('#" + sID + "').parent().addClass('" + sActiveClass + "');</script>");
            }
            catch (Exception)
            {               
                
            }
            
        }
        protected void btnMainSpec_Click(object sender, EventArgs e)
        {
            //lblError.Text = "";
            ////DoValidation(grModel,"txtValueModel","ddlValueModel","txtOtherModel");
            ////DoValidation(grModel, "txtValueModel", "ddlValueModel");
            //if (lblError.Text == "")
            //{
                //				if (ModelType!=HidModelType.Value)
                //				{
                //GetModelType();			

                //					if (HidModelType.Value=="" || HidModelType.Value==null)
                //					{
                //						lblError.Text="Model Type cannot be blank";
                //						return;
                //					}

                //if (Session["Mode"].ToString().ToUpper() == "NEW")
                //{
                //    BindGrids(grModel, HidModelType.Value, "", 1);
                //    //BindGrids(grModelHide, HidModelType.Value, "", 0);
                //    //Populate the record for optional		
                //    //BindGrids(grOptionalForModel, HidModelType.Value, "", 0);
                //    //BindGrids(grOptionalForModelHide, HidModelType.Value, "", 0);
                //}
                //else 
            //if (Session["Mode"].ToString().ToUpper()=="LOAD")
            //    {
            //        BindGrids(grModel, HidModelType.Value, HidContract.Value, 1);
            //        //						BindGridForLoad(grModel,HidModelType.Value,HidContract.Value,1);
            //        //						BindGrids(grModelHide,HidModelType.Value,"",0);	
            //        //Populate the record for optional		
            //        //						BindGrids(grOptionalForModel,HidModelType.Value,"",0);
            //        //						BindGrids(grOptionalForModelHide,HidModelType.Value,"",0);
            //    }
            //}
            //if (lblError.Text == "")
            //{
                MakeVisible( false, true,  false, false, false, false, false, false, true , true);
                ////					MakeVisibleByStatus("");	
                SetHeading();
                SetActiveClass("btnMainSpec", "navpnlModelBtn", "active");
                btnMainSpec.Focus();
                //						lblHeading.Text="Model : " + HidModelType.Value + " Detail Specification";
            //}
            //			}
        }
        protected void btnMacSpec_Click(object sender, EventArgs e)
        {
            //lblError.Text = "";
            ////DoValidation(grModel, "txtValueModel", "ddlValueModel");
            //if (lblError.Text == "")
            //{
                //				if (ModelType!=HidModelType.Value)
                //				{
                //GetModelType();			

                //					if (HidModelType.Value=="" || HidModelType.Value==null)
                //					{
                //						lblError.Text="Model Type cannot be blank";
                //						return;
                //					}

            //if (Session["Mode"].ToString().ToUpper() == "NEW" && !IsPostBack)
            //{
            //    BindGrids(grTemplate02, HidModelType.Value, "", 2);
            //    ////						BindGrids(grModelHide,HidModelType.Value,"",2);				
            //    ////						//Populate the record for optional		
            //    ////						BindGrids(grOptionalForModel,HidModelType.Value,"",2);										
            //    ////						BindGrids(grOptionalForModelHide,HidModelType.Value,"",2);	
            //}
            //else if (Session["Mode"].ToString().ToUpper()=="LOAD")
            //    {
            //        BindGrids(grTemplate02, HidModelType.Value, HidContract.Value, 2);
            //        ////						BindGrids(grModelHide,HidModelType.Value,"",2);	
            //        ////						//Populate the record for optional		
            //        ////						BindGrids(grOptionalForModel,HidModelType.Value,"",2);
            //        ////						BindGrids(grOptionalForModelHide,HidModelType.Value,"",2);
            //    }
            //}
            //if (lblError.Text == "")
            //{
                
                MakeVisible( false, false, true, false, false, false, false, false, true, true);
                ////					MakeVisibleByStatus("");
                SetHeading();
                SetActiveClass("btnMacSpec", "navpnlModelBtn", "active");
                btnMacSpec.Focus();
                ////						lblHeading.Text="Model : " + HidModelType.Value + " Machine Spec";
            //}
            //			}
        }
        protected void btnCopOpFn_Click(object sender, EventArgs e)
        {
            //lblError.Text = "";
            ////DoValidation(grModel, "txtValueModel", "ddlValueModel");
            //if (lblError.Text == "")
            //{
                //				if (ModelType!=HidModelType.Value)
                //				{
                //GetModelType();			

                //					if (HidModelType.Value=="" || HidModelType.Value==null)
                //					{
                //						lblError.Text="Model Type cannot be blank";
                //						return;
                //					}

            //if (Session["Mode"].ToString().ToUpper() == "NEW")
            //{
            //    BindGrids(grTemplate03, HidModelType.Value, "", 3);
            //    ////						BindGrids(grModelHide,HidModelType.Value,"",3);				
            //    ////						//Populate the record for optional		
            //    ////						BindGrids(grOptionalForModel,HidModelType.Value,"",3);										
            //    ////						BindGrids(grOptionalForModelHide,HidModelType.Value,"",3);	
            //}
            //else if (Session["Mode"].ToString().ToUpper()=="LOAD")
            //    {
            //        BindGrids(grTemplate03, HidModelType.Value, HidContract.Value, 3);
            //        ////				BindGrids(grModelHide,HidModelType.Value,"",3);	
            //        ////				//Populate the record for optional		
            //        ////				BindGrids(grOptionalForModel,HidModelType.Value,"",3);
            //        ////				BindGrids(grOptionalForModelHide,HidModelType.Value,"",3);
            //    }
            //}
            //if (lblError.Text == "")
            //{
                MakeVisible( false, false, false, true, false, false, false, false, true, true);
                ////				MakeVisibleByStatus("");	
                ////				SetHeading();
                lblHeading.Text = "Model : " + HidModelType.Value + " <font color=#8B008B> - COP Operation and Function Spec</font>";
                SetActiveClass("btnCopOpFn", "navpnlModelBtn", "active");
                btnCopOpFn.Focus();
                if (Session["Mode"].ToString().ToUpper() != "NEW")
                    lblHeading.Text = " <font color=#3333CC> Contract No: " + HidContract.Value + "</font> - " + lblHeading.Text;
            //}
            //			}
        }
        protected void btnHallSpec_Click(object sender, EventArgs e)
        {
            //lblError.Text = "";
            ////DoValidation(grModel, "txtValueModel", "ddlValueModel");
            //if (lblError.Text == "")
            //{
                //				if (ModelType!=HidModelType.Value)
                //				{
                //GetModelType();			

                //					if (HidModelType.Value=="" || HidModelType.Value==null)
                //					{
                //						lblError.Text="Model Type cannot be blank";
                //						return;
                //					}

            //if (Session["Mode"].ToString().ToUpper() == "NEW")
            //{
            //    BindGrids(grTemplate04, HidModelType.Value, "", 4);
            //    ////						BindGrids(grModelHide,HidModelType.Value,"",4);				
            //    ////						//Populate the record for optional		
            //    ////						BindGrids(grOptionalForModel,HidModelType.Value,"",4);										
            //    ////						BindGrids(grOptionalForModelHide,HidModelType.Value,"",4);	
            //}
            //else if (Session["Mode"].ToString().ToUpper()=="LOAD")
            //    {
            //        BindGrids(grTemplate04, HidModelType.Value, HidContract.Value, 4);
            //        ////				BindGrids(grModelHide,HidModelType.Value,"",4);	
            //        ////				//Populate the record for optional		
            //        ////				BindGrids(grOptionalForModel,HidModelType.Value,"",4);
            //        ////				BindGrids(grOptionalForModelHide,HidModelType.Value,"",4);
            //    }
            //}
            //if (lblError.Text == "")
            //{
                MakeVisible( false, false,  false, false, true, false, false, false, true, true);
                ////				MakeVisibleByStatus("");	
                ////				SetHeading();
                lblHeading.Text = "Model : " + HidModelType.Value + " <font color=#8B008B> - Hall Spec</font>";
                SetActiveClass("btnHallSpec", "navpnlModelBtn", "active");
                btnHallSpec.Focus();
                if (Session["Mode"].ToString().ToUpper() != "NEW")
                    lblHeading.Text = " <font color=#3333CC> Contract No: " + HidContract.Value + "</font> - " + lblHeading.Text;
            //}
            //			}
        }
        protected void btnCarSpec_Click(object sender, EventArgs e)
        {
            //lblError.Text = "";
            //DoValidation(grModel, "txtValueModel", "ddlValueModel");
            //if (lblError.Text == "")
            //{
                //				if (ModelType!=HidModelType.Value)
                //				{
                //GetModelType();			

                //					if (HidModelType.Value=="" || HidModelType.Value==null)
                //					{
                //						lblError.Text="Model Type cannot be blank";
                //						return;
                //					}

            //if (Session["Mode"].ToString().ToUpper() == "NEW")
            //{
            //    BindGrids(grTemplate05, HidModelType.Value, "", 5);
            //    ////						BindGrids(grModelHide,HidModelType.Value,"",5);				
            //    ////						//Populate the record for optional		
            //    ////						BindGrids(grOptionalForModel,HidModelType.Value,"",5);										
            //    ////						BindGrids(grOptionalForModelHide,HidModelType.Value,"",5);	
            //}
            //else if (Session["Mode"].ToString().ToUpper()=="LOAD")
            //    {
            //        BindGrids(grTemplate05, HidModelType.Value, HidContract.Value, 5);
            //        ////				BindGrids(grModelHide,HidModelType.Value,"",5);	
            //        ////				//Populate the record for optional		
            //        ////				BindGrids(grOptionalForModel,HidModelType.Value,"",5);
            //        ////				BindGrids(grOptionalForModelHide,HidModelType.Value,"",5);
            //    }
            //}
            //if (lblError.Text == "")
            //{
                MakeVisible( false, false,  false, false, false, true, false, false, true, true);
                ////				MakeVisibleByStatus("");	
                ////				SetHeading();
                lblHeading.Text = "Model : " + HidModelType.Value + " <font color=#8B008B> - Car Spec</font>";
                SetActiveClass("btnCarSpec", "navpnlModelBtn", "active");
                btnCarSpec.Focus();
                if (Session["Mode"].ToString().ToUpper() != "NEW")
                    lblHeading.Text = " <font color=#3333CC> Contract No: " + HidContract.Value + "</font> - " + lblHeading.Text;
            //}
            //			}
        }
        protected void btnHoistSpec_Click(object sender, EventArgs e)
        {
            //lblError.Text = "";
            ////DoValidation(grModel, "txtValueModel", "ddlValueModel");
            //if (lblError.Text == "")
            //{
                //				if (ModelType!=HidModelType.Value)
                //				{
                //GetModelType();			

                //					if (HidModelType.Value=="" || HidModelType.Value==null)
                //					{
                //						lblError.Text="Model Type cannot be blank";
                //						return;
                //					}

            //if (Session["Mode"].ToString().ToUpper() == "NEW")
            //{
            //    BindGrids(grTemplate06, HidModelType.Value, "", 6);
            //    ////						BindGrids(grModelHide,HidModelType.Value,"",6);				
            //    ////						//Populate the record for optional		
            //    ////						BindGrids(grOptionalForModel,HidModelType.Value,"",6);										
            //    ////						BindGrids(grOptionalForModelHide,HidModelType.Value,"",6);	
            //}
            //else if (Session["Mode"].ToString().ToUpper()=="LOAD")
            //    {
            //        BindGrids(grTemplate06, HidModelType.Value, HidContract.Value, 6);
            //        ////				BindGrids(grModelHide,HidModelType.Value,"",6);	
            //        ////				//Populate the record for optional		
            //        ////				BindGrids(grOptionalForModel,HidModelType.Value,"",6);
            //        ////				BindGrids(grOptionalForModelHide,HidModelType.Value,"",6);
            //    }
            //}
            //if (lblError.Text == "")
            //{
                MakeVisible( false, false, false, false, false, false, true, false, true, true);
                ////				MakeVisibleByStatus("");	
                ////				SetHeading();
                lblHeading.Text = "Model : " + HidModelType.Value + " <font color=#8B008B> - Hoistway Spec</font>";
                SetActiveClass("btnHoistSpec", "navpnlModelBtn", "active");
                btnHoistSpec.Focus();                
               
                if (Session["Mode"].ToString().ToUpper() != "NEW")
                    lblHeading.Text = " <font color=#3333CC> Contract No: " + HidContract.Value + "</font> - " + lblHeading.Text;		

            //}
            //			}
        }
        protected void btnRemarks_Click(object sender, EventArgs e)
        {
            //lblError.Text = "";
            ////DoValidation(grModel, "txtValueModel", "ddlValueModel");
            //if (lblError.Text == "")
            //{
                //				if (ModelType!=HidModelType.Value)
                //				{
                //GetModelType();			

                //					if (HidModelType.Value=="" || HidModelType.Value==null)
                //					{
                //						lblError.Text="Model Type cannot be blank";
                //						return;
                //					}

            //if (Session["Mode"].ToString().ToUpper() == "NEW")
            //{
            //    BindGrids(grTemplate07, HidModelType.Value, "", 7);
            //    ////						BindGrids(grModelHide,HidModelType.Value,"",7);				
            //    ////						//Populate the record for optional		
            //    ////						BindGrids(grOptionalForModel,HidModelType.Value,"",7);										
            //    ////						BindGrids(grOptionalForModelHide,HidModelType.Value,"",7);	
            //}
            //else if (Session["Mode"].ToString().ToUpper()=="LOAD")
            //    {
            //        BindGrids(grTemplate07, HidModelType.Value, HidContract.Value, 7);
            //        ////				BindGrids(grModelHide,HidModelType.Value,"",7);	
            //        ////				//Populate the record for optional		
            //        ////				BindGrids(grOptionalForModel,HidModelType.Value,"",7);
            //        ////				BindGrids(grOptionalForModelHide,HidModelType.Value,"",7);
            //    }
            //}
            //if (lblError.Text == "")
            //{
                MakeVisible( false, false, false, false, false, false, false, true, true, true);
                ////				MakeVisibleByStatus("");	
                ////				SetHeading();
                lblHeading.Text = "Model : " + HidModelType.Value + " <font color=#8B008B> - Remarks</font>";
                SetActiveClass("btnRemarks", "navpnlModelBtn", "active");
                btnRemarks.Focus();
                if (Session["Mode"].ToString().ToUpper() != "NEW")
                    lblHeading.Text = " <font color=#3333CC> Contract No: " + HidContract.Value + "</font> - " + lblHeading.Text;
            //}
            //			}
        }            
        protected void btnNextMain_Click(object sender, EventArgs e)
        {
            string ModelType="";
            lblError.Text = "";
            SetPortNameToTextField();        
            DoValidation(grScreenTemplate, "txtValue", "ddlValue");
            
            if (rbE.Checked  )
            {
                if (chkCommonContractValidation())
                {
                    if (!BLL.MainScreen.IsCommonContractNoExists(CommanContractNo()))
                    {
                        lblError.Text = "PROJECT NO (FOR COMMON) is not available, Please check";
                        return;
                    }
                }
            }

            if (lblError.Text == "")
            {
                GetJobLocation();

                if (rbE.Checked)
                {
                    if (BLL.MainScreen.IsContractNoExists(Convert.ToInt32(Session["RefNo"]), GenerateContractNo()))
                    {
                        lblError.Text = "Contract No alread exists, Please select different contract No";
                        return;
                    }
                    ModelType = NewModelType();
                }
                else
                    HidModelType.Value = "COMMON";
                if (chkCommonContractValidation())
                {
                    if (BLL.MainScreen.IsContractNoExists(Convert.ToInt32(Session["RefNo"]), GenerateContractNo()))
                    {
                        lblError.Text = "Contract No alread exists, Please select different contract No";
                        return;
                    }
                }
                //Added By Pradeep S
                //For Part Number Allocation ParmCode Existence Check
                if(!IsValidPartNumberAllocation())
                    return;
                if (ModelType != HidModelType.Value)
                {
                    if (rbE.Checked)
                        GetModelType();
                    else
                        HidModelType.Value = "COMMON";
                }
                if (HidModelType.Value == "" || HidModelType.Value == null)
                {
                    lblError.Text = "Model Type cannot be blank";
                    return;
                }

                if (Session["Mode"].ToString().ToUpper() == "NEW")
                {
                    BindGrids(grModel, HidModelType.Value, "", 1);
                    //populate the record for ModelType
                    //BindGridForLoad(grModel, HidModelType.Value, HidContract.Value, 1);

                    BindGridForLoad(grTemplate02, HidModelType.Value, "", 2);
                    BindGridForLoad(grTemplate03, HidModelType.Value, "", 3);
                    BindGridForLoad(grTemplate04, HidModelType.Value, "", 4);
                    BindGridForLoad(grTemplate05, HidModelType.Value, "", 5);
                    BindGridForLoad(grTemplate06, HidModelType.Value, "", 6);
                    BindGridForLoad(grTemplate07, HidModelType.Value, "", 7);
                }
                else //if (Session["Mode"].ToString().ToUpper()=="LOAD")
                {
                    BindGrids(grModel, HidModelType.Value, HidContract.Value, 1);                    
                    
                    //populate the record for ModelType
                    //BindGridForLoad(grModel, HidModelType.Value, HidContract.Value, 1);

                    BindGridForLoad(grTemplate02, HidModelType.Value, HidContract.Value, 2);
                    BindGridForLoad(grTemplate03, HidModelType.Value, HidContract.Value, 3);
                    BindGridForLoad(grTemplate04, HidModelType.Value, HidContract.Value, 4);
                    BindGridForLoad(grTemplate05, HidModelType.Value, HidContract.Value, 5);
                    BindGridForLoad(grTemplate06, HidModelType.Value, HidContract.Value, 6);
                    BindGridForLoad(grTemplate07, HidModelType.Value, HidContract.Value, 7);
                }
                
                if (lblError.Text == "")
                {                  
                    MakeVisibleByStatus("INPUTMODEL");

                    SetActiveClass("btnMainSpec", "NavMain", "active");
                    
                    if (rbE.Checked)
                    {
                        MakeVisible(false, true, false, false, false, false, false, false, true, true);
                        //Commented by PSP btnIntCNW.Visible = true;
                    }
                    else
                    {
                        MakeVisible(false, true, false, false, false, false, false, false, false, true);
                        //Commented by PSP btnIntCNW.Visible = false;
                    }
                    SetHeading();
                }
            }
        }               
        protected void btnok07_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            //			DoValidation(grOptionalForModel,"txtValueOptModel","ddlValueOptModel","txtOtherOptModel");
            DoValidation(grTemplate07, "txtValue", "ddlValue");
            if (lblError.Text == "")
            {
                MakeVisible(false, true, false, false, false, false, false, false, false, false);
                SetHeading();
            }
        }
        protected void btnok06_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            //			DoValidation(grOptionalForModel,"txtValueOptModel","ddlValueOptModel","txtOtherOptModel");
            DoValidation(grTemplate06, "txtValue", "ddlValue");
            if (lblError.Text == "")
            {
                MakeVisible( false, true, false, false, false, false, false, false, false, false);
                SetHeading();
            }
        }
        protected void btnok05_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            //DoValidation(grOptionalForModel,"txtValueOptModel","ddlValueOptModel","txtOtherOptModel");
            DoValidation(grTemplate05, "txtValue", "ddlValue");
            if (lblError.Text == "")
            {
                MakeVisible( false, true, false, false, false, false, false, false, false, false);
                SetHeading();
            }
        }
        protected void btnok04_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            //			DoValidation(grOptionalForModel,"txtValueOptModel","ddlValueOptModel","txtOtherOptModel");
            DoValidation(grTemplate04, "txtValue", "ddlValue");
            if (lblError.Text == "")
            {
                MakeVisible(false, true, false, false, false, false, false, false, false, false);
                SetHeading();
            }
        }
        protected void btnok03_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            //			DoValidation(grOptionalForModel,"txtValueOptModel","ddlValueOptModel","txtOtherOptModel");
            DoValidation(grTemplate03, "txtValue", "ddlValue");
            if (lblError.Text == "")
            {
                MakeVisible( false, true, false, false, false, false, false, false, false, false);
                SetHeading();
            }
        }
        protected void btnok02_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            //			DoValidation(grOptionalForModel,"txtValueOptModel","ddlValueOptModel","txtOtherOptModel");
            DoValidation(grTemplate02, "txtValue", "ddlValue");
            if (lblError.Text == "")
            {
                MakeVisible( false, true, false, false, false, false, false, false, false, false);
                SetHeading();
            }
        }
        protected void btnRefreshModel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["RefNo"] != null)
                    lblStatus.Text = "Status : " + BLL.MainScreen.GetStatuTempMaster(Convert.ToInt32(Session["RefNo"]));
                MakeVisibleByStatus(lblStatus.Text);
            }
            catch (SqlException sqEx)
            {
                lblError.Text = sqEx.Message;
            }
        }
        protected void btnExecuteModel_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            //System.Threading.Thread.Sleep(5000);

            if (BLL.MainScreen.IsDTProcessing())
            {
                lblError.Text = "Datatransfer is in processing, please try again later";
                return;
            }

            if (rbE.Checked)
            {
                DoValidation(grModel, "txtValue", "ddlValue");
                DoValidation(grTemplate02, "txtValue", "ddlValue");
                DoValidation(grTemplate03, "txtValue", "ddlValue");
                DoValidation(grTemplate04, "txtValue", "ddlValue");
                DoValidation(grTemplate05, "txtValue", "ddlValue");
                DoValidation(grTemplate06, "txtValue", "ddlValue");
                DoValidation(grTemplate07, "txtValue", "ddlValue");
            }
            else
            {
                DoValidation(grModel, "txtValue", "ddlValue");
            }
            try
            {
                if (lblError.Text == "")
                {
                    SaveContract(false);                    
                    
                    if(lblError.Text=="")
                    {
                        PopUpComputeMaterialCategory();
                         /*   
                        BLL.Login.InsertLogHistory("S", Session["UserId"].ToString(), "JBQ", Session["RefNo"].ToString());
                        MtlCEQ.Path = "FormatName:DIRECT=OS:" + ConfigurationSettings.AppSettings["QueuePath"] + "\\private$\\mtlcequeue";
                        MtlCEQ.Send(Session["RefNo"].ToString() + "^" + ConfigurationSettings.AppSettings["SQLConnWS"] + "^" + ConfigurationSettings.AppSettings["LoadingClassParmCode"], "MtlCEQ");
                        BLL.MainScreen.UpdateStatuForCompute(Convert.ToInt32(Session["RefNo"]), "JBQ", Session["UserID"].ToString());
                        lblStatus.Text = "Status : " + BLL.MainScreen.GetStatuTempMaster(Convert.ToInt32(Session["RefNo"]));
                        // MakeVisibleByStatus(lblStatus.Text);
                        //Response.Write("<Script>alert('Submited for Computation');location.href='ViewOutput.aspx';</Script>"); not working in AJAX
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Submited for Computation.');location.href='ViewOutput.aspx';", true);
                        //ShowAlertMessage("Submited for Computation");	
                        */
                    }
                }
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
        //private void ShowAlertMessage(string msg)
        //{
        //    spanMessageArea.InnerText = msg;

        //    System.Web.UI.HtmlControls.HtmlGenericControl body = (System.Web.UI.HtmlControls.HtmlGenericControl)Master.FindControl("MasterBody");

        //    body.Attributes.Add("onload", "javascript:alert('" + msg + "')");

        //    divMessageArea.Visible = true;

        //}
        protected void btnOptionalModel_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            //			DoValidation(grModel,"txtValueModel","ddlValueModel","txtOtherModel");
            DoValidation(grModel, "txtValue", "ddlValue");
            if (lblError.Text == "")
            {
                MakeVisible( false, false, false, false, false, false, false, false, false, false);
                SetHeading();
            }
        }
        protected void btnPreviousModel_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            MakeVisible( true, false, false, false, false, false, false, false, false, false);
            SetHeading();
        }

        protected void grScreenTemplate_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlpValue = (DropDownList)e.Row.FindControl("ddlValue");
                TextBox txtpValue = (TextBox)e.Row.FindControl("txtValue");
                CheckBox cbNStd = (CheckBox)e.Row.FindControl("cbNS");
                CheckBox cbAllowBlank = (CheckBox)e.Row.FindControl("cbAllowBlank");
                cbAllowBlank.Checked = Convert.ToBoolean(dr["IsAllowBlank"]);
                
                if (Convert.ToBoolean(dr["FIsNonStd"]))
                {
                    txtpValue.Visible = true;
                    txtpValue.Text = dr["ValueDescription"].ToString();
                    ddlpValue.Visible = false;
                    cbNStd.Checked = true;

                    DataSet dsParmValus = new DataSet();
                    dsParmValus = BLL.MainScreen.FetchParmValues(Convert.ToInt32(dr["ParmCode"]), HidModelType.Value);

                    ddlpValue.DataSource = dsParmValus;
                    ddlpValue.DataValueField = "ValueCode";
                    ddlpValue.DataTextField = "ValueDescription";
                    ddlpValue.DataBind();
                    ddlpValue.Items.Insert(0, new ListItem("", ""));
                }
                else if (Convert.ToInt16(dr["IsValueTable"]) == 1)
                {
                    dtParmValus = BLL.MainScreen.FetchParmValues(Convert.ToInt32(dr["ParmCode"]), "Main Screen").Tables[0];
                    ddlpValue.DataSource = dtParmValus;
                    ddlpValue.DataValueField = "ValueCode";
                    ddlpValue.DataTextField = "ValueDescription";
                    ddlpValue.DataBind();
                    ddlpValue.Items.Insert(0, new ListItem("", ""));
                    //commented if (dr["DefaultValue"].ToString() != "0") condition as Default value saving as '0'
                    //if (dr["DefaultValue"].ToString() != "0")
                    //{
                        ////ddlpValue.SelectedIndex = ddlpValue.Items.IndexOf(ddlpValue.Items.FindByText(Convert.ToString(dr["DefaultValue"])));							
                        ddlpValue.SelectedIndex = ddlpValue.Items.IndexOf(ddlpValue.Items.FindByText(Convert.ToString(dr["ValueDescription"]).ToUpper()));
                    //}
                    txtpValue.Visible = false;
                    // when Load Model should not be changed
                    //if ((Session["Mode"].ToString().ToUpper()=="LOAD" || LoadFrom=="OUTPUT") && dr["ParmCode"].ToString().ToUpper()==ConfigurationSettings.AppSettings["MODEL"]) // check for Model - ParmCode = 8 is Model
                    if ((Session["Mode"].ToString().ToUpper() == "LOAD") && dr["ParmCode"].ToString().ToUpper() == ConfigurationSettings.AppSettings["LIFT MODEL"]) // check for Model - ParmCode = 8 is Model
                    {
                        ddlpValue.Enabled = false;
                        cbNStd.Enabled = false;
                    }                   
                }
                else
                {
                    txtpValue.Text = dr["ValueDescription"].ToString();
                    txtpValue.Visible = true;
                    ddlpValue.Visible = false;
                    cbNStd.Enabled = true;
                }
                if (Convert.ToBoolean(dr["HideFlag"]))
                    e.Row.Visible = false;

                if (Convert.ToBoolean(dr["BIsNonStd"]))
                {
                    cbNStd.Enabled = true;
                    int n = GetColumnIndexByName(grScreenTemplate, "NON STD");
                    e.Row.Cells[n].BackColor = System.Drawing.Color.SkyBlue;

                }
                else
                    cbNStd.Enabled = false;

                if (e.Row.RowIndex == 0)
                {
                    e.Row.Cells[0].Text = "" + (e.Row.RowIndex + 1);
                }
                else if (Convert.ToBoolean(dr["HideFlag"]))
                {
                    int iSeqNo = Convert.ToInt16(grScreenTemplate.Rows[e.Row.RowIndex - 1].Cells[0].Text.ToString().Trim());
                    e.Row.Cells[0].Text = "" + iSeqNo;
                }
                else if (!Convert.ToBoolean(dr["HideFlag"]))
                {
                    int iSeqNo = Convert.ToInt16(grScreenTemplate.Rows[e.Row.RowIndex - 1].Cells[0].Text.ToString().Trim()) + 1;
                    e.Row.Cells[0].Text = "" + iSeqNo;
                }
                //Added By Pradeep on 23/Mar/2015 
                // Disable ProjectNo,EquipNo and Part Allocation No , If the project already released
                DisableReleasedProjectParams(txtpValue, ddlpValue,Convert.ToString(dr["ParmCode"]));
                SetPortNameList(txtpValue, ddlpValue, Convert.ToString(dr["ParmCode"]));

            }
        }
        protected void SetNADDLDefault(object sender, EventArgs e)
        {
            try
            {
                DropDownList objDDL = (DropDownList)sender;
                GridViewRow gvRow = (GridViewRow)objDDL.NamingContainer;
                GridView ParentGrid = (GridView)gvRow.NamingContainer;
                String sParentID = ParentGrid.ID.ToString();
                
                String sSelVal = Convert.ToString(objDDL.SelectedItem).ToUpper();
                if(!String.IsNullOrEmpty(sSelVal))
                {
                   
                    String sFormula = objDDL.Attributes["Formula"];
                    if (!String.IsNullOrEmpty(sFormula))
                    {
                        GridViewRow objRow;
                        String sParamValue;
                        BLL.FormulaEval objFormulaEval = BLL.FormulaEval.GetInstance();

                        XmlNodeList ndlstNA = objFormulaEval.getNA(sParentID,sFormula);
                        for (int idx = 0; idx < ParentGrid.Rows.Count; idx++)
                        {
                            objRow = ParentGrid.Rows[idx];
                            if (objRow.RowType == DataControlRowType.DataRow)
                            {

                                DropDownList ddlpValue = (DropDownList)objRow.FindControl("ddlValue");
                                TextBox txtValue = (TextBox)objRow.FindControl("txtValue");
                                sParamValue = objRow.Cells[2].Text;

                                sParamValue = sParamValue.ToUpper();
                                String sNAID;
                                foreach (XmlNode ndNA in ndlstNA)
                                {
                                    sNAID = ndNA.Attributes.GetNamedItem("id").Value.ToString().ToUpper(); ;
                                    if (sParamValue == sNAID)
                                    {
                                        if (sSelVal == ndNA.Attributes.GetNamedItem("SelectedVal").Value.ToString().ToUpper())
                                        {
                                            ddlpValue.SelectedValue = ndNA.InnerText.ToString();
                                            ddlpValue.Enabled = false;
                                            bPreRenderGrid = false;
                                        }
                                        else
                                        {
                                            ddlpValue.Enabled = true;
                                            bPreRenderGrid = false;
                                        }

                                    }

                                }

                            }
                        }

                    }
                    if (e != null)
                    {
                        switch (sParentID)
                        {
                            case "grModel":
                                btnMainSpec_Click(btnMainSpec, EventArgs.Empty);
                                break;
                            case "grTemplate02":
                                btnMacSpec_Click(btnMacSpec, EventArgs.Empty);
                                break;
                            case "grTemplate03":
                                btnCopOpFn_Click(btnCopOpFn, EventArgs.Empty);
                                break;
                            case "grTemplate04":
                                btnHallSpec_Click(btnHallSpec, EventArgs.Empty);
                                break;
                            case "grTemplate05":
                                btnCarSpec_Click(btnCarSpec, EventArgs.Empty);
                                break;
                            case "grTemplate06":
                                btnHoistSpec_Click(btnHoistSpec, EventArgs.Empty);
                                break;
                            case "grTemplate07":
                                btnRemarks_Click(btnRemarks, EventArgs.Empty);
                                break;
                            default:
                                break;
                        }
                    }
                }
                
                
                
            }
            catch (Exception ex)
            {               
                
             
            }

        }
        protected void  SetTotalHeightText(object sender, EventArgs e)
        {
            try
            {
                TextBox obj = (TextBox)sender;
                
                String sFormula = obj.Attributes["Formula"];
                btnMainSpec_Click(btnMainSpec, EventArgs.Empty);
                if (!String.IsNullOrEmpty(sFormula))
                {
                    GridViewRow objRow;
                    String sParamValue;
                    BLL.FormulaEval objFormulaEval = BLL.FormulaEval.GetInstance();
                    String sFormulaEval = objFormulaEval.getFormula(sFormula);
                    ArrayList alCtrlObjects = objFormulaEval.getControlArray(sFormula);
                    foreach (Control objItem in alCtrlObjects )
                    {
                        String sVal;
                        if (objItem is TextBox)
                        {
                            String sRowIndex = ((TextBox)objItem).Attributes["RowIndex"];
                            objRow = grModel.Rows[Convert.ToInt32(sRowIndex)];
                            TextBox txtValue = (TextBox)objRow.FindControl("txtValue");
                            sParamValue = objRow.Cells[2].Text;
                            sVal = txtValue.Text;
                            if (String.IsNullOrEmpty(sVal))
                                sVal="0";
                            sFormulaEval = sFormulaEval.Replace("{"+sParamValue+"}", sVal);
                        }
                       
                    }
                    for (int idx = 0; idx < grModel.Rows.Count ; idx++)
                    {
                        objRow = grModel.Rows[idx];
                        if (objRow.RowType == DataControlRowType.DataRow)
                        {
                            sParamValue = objRow.Cells[2].Text;
                            sParamValue = sParamValue.ToUpper();
                            if (sParamValue == sFormula)
                            {
                                mcCalc objMCCalc = new mcCalc();
                                double dblResult = objMCCalc.evaluate(sFormulaEval);
                                TextBox txtValue = (TextBox)objRow.FindControl("txtValue");
                                txtValue.Text =Convert.ToString(dblResult);
                                break;
                            }
 
                        }
                    }
                }
                
                
            }
            catch (Exception ex)
            {
                
                
            }
        }
        private Boolean  SetTotalHeightEventHandler(int nRowIdx, String sParmDesc,Boolean bIsTextBox)
        {
            Boolean bRetVal = false;
            try
            {
                BLL.FormulaEval obj = BLL.FormulaEval.GetInstance();
                String sFormula = obj.getFormula(sParmDesc);
                if (!String.IsNullOrEmpty(sFormula))
                {
                    ArrayList alCtrlObjects = new ArrayList();
                    for (int idx = 0; idx < nRowIdx; idx++)
                    {
                        GridViewRow objRow = grModel.Rows[idx];
                        if (objRow.RowType == DataControlRowType.DataRow)
                        {

                            DropDownList ddlpValue = (DropDownList)objRow.FindControl("ddlValue");
                            TextBox txtValue = (TextBox)objRow.FindControl("txtValue");
                            String sParamValue = objRow.Cells[2].Text;

                            sParamValue = sParamValue.ToUpper();
                            if (sFormula.Contains(sParamValue))
                            {
                                if (!bIsTextBox)
                                {
                                    ddlpValue.Attributes.Add("Formula", sParmDesc);
                                    ddlpValue.Attributes.Add("RowIndex", Convert.ToString(idx));
                                    ddlpValue.SelectedIndexChanged += new EventHandler(SetTotalHeightText);
                                    alCtrlObjects.Add(ddlpValue);
                                    bRetVal = true;
                                }
                                else if (bIsTextBox)
                                {
                                    txtValue.TextChanged += new EventHandler(SetTotalHeightText);
                                    txtValue.AutoPostBack = true;
                                    txtValue.Attributes.Add("Formula", sParmDesc);
                                    txtValue.Attributes.Add("RowIndex", Convert.ToString(idx));
                                    alCtrlObjects.Add(txtValue);
                                    bRetVal = true;
                                }
                            }
                            else
                            {
                                if (bIsTextBox)
                                {
                                    txtValue.AutoPostBack = false;
                                    txtValue.TextChanged -= new EventHandler(SetTotalHeightText);

                                }
                            }

                        }
                    }
                    if (alCtrlObjects.Count > 0)
                    {
                        obj.AddControlArray(alCtrlObjects, sParmDesc);
                    }
                }                
                
            }
            catch (Exception ex)
            {
                
                
            }
            return bRetVal;
        }
        private void FindNAEvents(GridView objGrid)
        {
            String sParmDesc = "";
            Boolean bIsTextBox;
            BLL.FormulaEval obj = BLL.FormulaEval.GetInstance();
            XmlNodeList ndlstNAParent = obj.getNAParents(objGrid.ID);
            String sNAParentID = "";

            ////foreach (GridViewRow objRow in objGrid.Rows)
            ////{
            ////    if (objRow.RowType == DataControlRowType.DataRow)
            ////    {
            ////        DropDownList ddlpValue1 = (DropDownList)objRow.FindControl("ddlValue");
            ////        TextBox txtValue1 = (TextBox)objRow.FindControl("txtValue");
            ////        if (!txtValue1.Visible)
            ////        {
            ////            ddlpValue1.SelectedIndexChanged -= new EventHandler(SetNADDLDefault);
            ////            ddlpValue1.AutoPostBack = false;
            ////        }
            ////    }
            ////}
            foreach (GridViewRow objRow in objGrid.Rows)
            {
                if (objRow.RowType == DataControlRowType.DataRow)
                {
                    sParmDesc = "";
                    bIsTextBox = false;
                    //if (objRow.Visible)
                    {
                        DropDownList ddlpValue = (DropDownList)objRow.FindControl("ddlValue");
                        TextBox txtValue = (TextBox)objRow.FindControl("txtValue");
                        sParmDesc = objRow.Cells[2].Text;
                        bIsTextBox = txtValue.Visible;
                        sParmDesc = sParmDesc.ToUpper();
                        
                        foreach (XmlNode ndNAParent in ndlstNAParent)
                        {
                            sNAParentID = ndNAParent.Attributes.GetNamedItem("id").Value.ToString();
                            if (sParmDesc == sNAParentID)
                            {
                                if (bIsTextBox)
                                {
                                }
                                else
                                {
                                    ddlpValue.Attributes.Add("Formula", sParmDesc);
                                    ddlpValue.Attributes.Add("RowIndex", Convert.ToString(objRow.RowIndex));
                                    ddlpValue.SelectedIndexChanged += new EventHandler(SetNADDLDefault);
                                    ddlpValue.AutoPostBack = true;
                                }
                                if (SetNAEventHandler(objGrid, objRow.RowIndex, sParmDesc, bIsTextBox))
                                    SetNADDLDefault(ddlpValue, null);
                                break;
                            }
                        }                        
                    }
                }
            }            
            
        }
        private Boolean SetNAEventHandler(GridView objGrid,int nRowIdx, String sParmDesc, Boolean bIsTextBox)
        {
            Boolean bRetVal = false;
            try
            {

                BLL.FormulaEval obj = BLL.FormulaEval.GetInstance();
                XmlNodeList ndlstNA = obj.getNA(objGrid.ID,sParmDesc);
                ArrayList alCtrlObjects = new ArrayList();
                for (int idx = 0; idx < objGrid.Rows.Count; idx++)
                {
                    GridViewRow objRow = objGrid.Rows[idx];
                    if (objRow.RowType == DataControlRowType.DataRow)
                    {

                        DropDownList ddlpValue = (DropDownList)objRow.FindControl("ddlValue");
                        TextBox txtValue = (TextBox)objRow.FindControl("txtValue");
                        String sParamValue = objRow.Cells[2].Text;

                        sParamValue = sParamValue.ToUpper();
                        String sNAID;
                        foreach(XmlNode ndNA in ndlstNA)
                        {
                            sNAID = ndNA.Attributes.GetNamedItem("id").Value.ToString().ToUpper(); ;
                            if (sParamValue == sNAID)
                            {
                                if (!bIsTextBox)
                                {                                    
                                    //alCtrlObjects.Add(ddlpValue);
                                    ddlpValue.AutoPostBack = true;
                                    bRetVal = true;
                                }
                                else if (bIsTextBox)
                                {
                                    
                                    //alCtrlObjects.Add(txtValue);
                                }
                            }
                            
                        }                        

                    }
                }
                if (alCtrlObjects.Count > 0)
                {
                    //obj.AddControlArray(alCtrlObjects, sParmDesc);
                }
            }
            catch (Exception ex)
            {


            }
            return bRetVal;
        }
        protected void grModel_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlpValue = (DropDownList)e.Row.FindControl("ddlValue");
                TextBox txtpValue = (TextBox)e.Row.FindControl("txtValue");
                CheckBox cbNStd = (CheckBox)e.Row.FindControl("cbNS");
                CheckBox cbAllowBlank = (CheckBox)e.Row.FindControl("cbAllowBlank");
                cbAllowBlank.Checked = Convert.ToBoolean(dr["IsAllowBlank"]);
                String sParmDesc = Convert.ToString(dr["ParmDescription"]);
                Boolean bIsTextBox = true;
                String sCaption = "";
                sCaption = e.Row.Cells[2].Text;
                if (sCaption.ToUpper() == "CAPACITY")
                {
                    txtpValue.Attributes.Add("onkeypress", "return isNumberKey(event)");

                }
                if (Convert.ToBoolean(dr["FIsNonStd"]))
                {
                    txtpValue.Visible = true;
                    txtpValue.Text = dr["ValueDescription"].ToString();
                    ddlpValue.Visible = false;
                    cbNStd.Checked = true;

                    DataSet dsParmValus = new DataSet();
                    dsParmValus = BLL.MainScreen.FetchParmValues(Convert.ToInt32(dr["ParmCode"]), HidModelType.Value);

                    ddlpValue.DataSource = dsParmValus;
                    ddlpValue.DataValueField = "ValueCode";
                    ddlpValue.DataTextField = "ValueDescription";
                    ddlpValue.DataBind();
                    ddlpValue.Items.Insert(0, new ListItem("", ""));
                }
                else if (Convert.ToInt16(dr["IsValueTable"]) == 1)
                {
                    DataSet dsParmValus = new DataSet();
                    dsParmValus = BLL.MainScreen.FetchParmValues(Convert.ToInt32(dr["ParmCode"]), HidModelType.Value);

                    ddlpValue.DataSource = dsParmValus;
                    ddlpValue.DataValueField = "ValueCode";
                    ddlpValue.DataTextField = "ValueDescription";
                    ddlpValue.DataBind();
                    ddlpValue.Items.Insert(0, new ListItem("", ""));

                    //ddlpValue.SelectedIndex = ddlpValue.Items.IndexOf(ddlpValue.Items.FindByText(Convert.ToString(dr["DefaultValue"])));							
                    ddlpValue.SelectedIndex = ddlpValue.Items.IndexOf(ddlpValue.Items.FindByText(Convert.ToString(dr["ValueDescription"]).ToUpper()));

                    if (dsParmValus.Tables[0].Rows.Count > 0)
                    {
                        ddlpValue.Visible = true;
                        txtpValue.Visible = false;
                        bIsTextBox = false;
                    }
                    else
                    {
                        txtpValue.Visible = true;
                        txtpValue.Text = dr["ValueDescription"].ToString();
                        ddlpValue.Visible = false;
                    }
                }
                else
                {
                    txtpValue.Text = dr["ValueDescription"].ToString();
                    txtpValue.Visible = true;
                    ddlpValue.Visible = false;
                }
                if (Convert.ToBoolean(dr["HideFlag"]))
                    e.Row.Visible = false;

                if (Convert.ToBoolean(dr["BIsNonStd"]))
                {
                    cbNStd.Enabled = true;
                    int n = GetColumnIndexByName(grModel,"NON STD");
                    e.Row.Cells[n].BackColor = System.Drawing.Color.SteelBlue;


                }
                else
                {
                    cbNStd.Enabled = false;
                }
                if (e.Row.RowIndex == 0)
                {
                    e.Row.Cells[0].Text = ""+ (e.Row.RowIndex + 1) ;
                }
                else if (Convert.ToBoolean(dr["HideFlag"]))
                {
                    int iSeqNo = Convert.ToInt16(grModel.Rows[e.Row.RowIndex - 1].Cells[0].Text.ToString().Trim());
                    e.Row.Cells[0].Text = "" + iSeqNo;
                }
                else if (!Convert.ToBoolean(dr["HideFlag"]))
                {
                    int iSeqNo = Convert.ToInt16(grModel.Rows[e.Row.RowIndex - 1].Cells[0].Text.ToString().Trim()) + 1;
                    e.Row.Cells[0].Text = "" + iSeqNo;
                }
                sParmDesc = sParmDesc.ToUpper();
                if (bIsTextBox)
                {
                    txtpValue.AutoPostBack = false;
                    txtpValue.TextChanged -= new EventHandler(SetTotalHeightText);

                }
                if(SetTotalHeightEventHandler(e.Row.RowIndex, sParmDesc, bIsTextBox))
                    txtpValue.Enabled = false;
                
            }
        }
        //protected void grOptionalForMain_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    DataRowView dr = (DataRowView)e.Row.DataItem;
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        DropDownList ddlpValue = (DropDownList)e.Row.FindControl("ddlValueOptMain");
        //        TextBox txtpValue = (TextBox)e.Row.FindControl("txtValueOptMain");
        //        CheckBox cbNStd = (CheckBox)e.Row.FindControl("cbNS");

        //        if (Convert.ToBoolean(dr["FIsNonStd"]))
        //        {
        //            txtpValue.Visible = true;
        //            txtpValue.Text = dr["ValueDescription"].ToString();
        //            ddlpValue.Visible = false;
        //            cbNStd.Checked = true;

        //            DataSet dsParmValus = new DataSet();
        //            dsParmValus = BLL.MainScreen.FetchParmValues(Convert.ToInt32(dr["ParmCode"]), HidModelType.Value);

        //            ddlpValue.DataSource = dsParmValus;
        //            ddlpValue.DataValueField = "ValueCode";
        //            ddlpValue.DataTextField = "ValueDescription";
        //            ddlpValue.DataBind();
        //            ddlpValue.Items.Insert(0, new ListItem("", ""));
        //        }
        //        else if (Convert.ToInt16(dr["IsValueTable"]) == 1)
        //        {
        //            DataSet dsParmValus = new DataSet();
        //            dsParmValus = BLL.MainScreen.FetchParmValues(Convert.ToInt32(dr["ParmCode"]), "Main Screen");

        //            ddlpValue.DataSource = dsParmValus;
        //            ddlpValue.DataValueField = "ValueCode";
        //            ddlpValue.DataTextField = "ValueDescription";
        //            ddlpValue.DataBind();
        //            ddlpValue.Items.Insert(0, new ListItem("", ""));

        //            if (dr["DefaultValue"].ToString() != "0")
        //            {
        //                ////ddlpValue.SelectedIndex = ddlpValue.Items.IndexOf(ddlpValue.Items.FindByText(Convert.ToString(dr["DefaultValue"])));							
        //                ddlpValue.SelectedIndex = ddlpValue.Items.IndexOf(ddlpValue.Items.FindByText(Convert.ToString(dr["ValueDescription"]).ToUpper()));
        //            }
        //            txtpValue.Visible = false;
        //        }
        //        else
        //        {
        //            txtpValue.Text = dr["ValueDescription"].ToString();
        //            txtpValue.Visible = true;
        //            ddlpValue.Visible = false;
        //        }
        //        //e.Item.Cells[5].Text = dr["ResultValue"].ToString();

        //        if (Convert.ToBoolean(dr["HideFlag"]))
        //            e.Row.Visible = false;

        //        if (Convert.ToBoolean(dr["BIsNonStd"]))
        //        {
        //            cbNStd.Enabled = true;
        //            int n = GetColumnIndexByName(grOptionalForMain, "NON STD");
        //            e.Row.Cells[n].BackColor = System.Drawing.Color.SteelBlue;
        //        }
        //        else
        //            cbNStd.Enabled = false;
        //    }
        //}
        //protected void grOptionalForModel_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    DataRowView dr = (DataRowView)e.Row.DataItem;
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        DropDownList ddlpValue = (DropDownList)e.Row.FindControl("ddlValueOptModel");
        //        TextBox txtpValue = (TextBox)e.Row.FindControl("txtValueOptModel");
        //        CheckBox cbNStd = (CheckBox)e.Row.FindControl("cbNS");

        //        if (Convert.ToBoolean(dr["FIsNonStd"]))
        //        {
        //            txtpValue.Visible = true;
        //            txtpValue.Text = dr["ValueDescription"].ToString();
        //            ddlpValue.Visible = false;
        //            cbNStd.Checked = true;

        //            DataSet dsParmValus = new DataSet();
        //            dsParmValus = BLL.MainScreen.FetchParmValues(Convert.ToInt32(dr["ParmCode"]), HidModelType.Value);

        //            ddlpValue.DataSource = dsParmValus;
        //            ddlpValue.DataValueField = "ValueCode";
        //            ddlpValue.DataTextField = "ValueDescription";
        //            ddlpValue.DataBind();
        //            ddlpValue.Items.Insert(0, new ListItem("", ""));
        //        }
        //        else if (Convert.ToInt16(dr["IsValueTable"]) == 1)
        //        {
        //            DataSet dsParmValus = new DataSet();
        //            dsParmValus = BLL.MainScreen.FetchParmValues(Convert.ToInt32(dr["ParmCode"]), HidModelType.Value);

        //            ddlpValue.DataSource = dsParmValus;
        //            ddlpValue.DataValueField = "ValueCode";
        //            ddlpValue.DataTextField = "ValueDescription";
        //            ddlpValue.DataBind();
        //            ddlpValue.Items.Insert(0, new ListItem("", ""));

        //            if (dr["DefaultValue"].ToString() != "0")
        //            {
        //                ////ddlpValue.SelectedIndex = ddlpValue.Items.IndexOf(ddlpValue.Items.FindByText(Convert.ToString(dr["DefaultValue"])));							
        //                ddlpValue.SelectedIndex = ddlpValue.Items.IndexOf(ddlpValue.Items.FindByText(Convert.ToString(dr["ValueDescription"]).ToUpper()));
        //            }
        //            txtpValue.Visible = false;
        //        }
        //        else
        //        {
        //            txtpValue.Text = dr["ValueDescription"].ToString();
        //            txtpValue.Visible = true;
        //            ddlpValue.Visible = false;
        //        }
        //        //e.Item.Cells[5].Text = dr["ResultValue"].ToString();

        //        if (Convert.ToBoolean(dr["HideFlag"]))
        //            e.Row.Visible = false;

        //        if (Convert.ToBoolean(dr["BIsNonStd"]))
        //        {
        //            cbNStd.Enabled = true;
        //            int n = GetColumnIndexByName(grOptionalForModel, "NON STD");
        //            e.Row.Cells[n].BackColor = System.Drawing.Color.SteelBlue;

        //        }
        //        else
        //            cbNStd.Enabled = false;
        //    }
        //}
        protected void grTemplate02_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlpValue = (DropDownList)e.Row.FindControl("ddlValue");
                TextBox txtpValue = (TextBox)e.Row.FindControl("txtValue");
                CheckBox cbNStd = (CheckBox)e.Row.FindControl("cbNS");

                if (Convert.ToBoolean(dr["FIsNonStd"]))
                {
                    txtpValue.Visible = true;
                    txtpValue.Text = dr["ValueDescription"].ToString();
                    ddlpValue.Visible = false;
                    cbNStd.Checked = true;

                    DataSet dsParmValus = new DataSet();
                    dsParmValus = BLL.MainScreen.FetchParmValues(Convert.ToInt32(dr["ParmCode"]), HidModelType.Value);

                    ddlpValue.DataSource = dsParmValus;
                    ddlpValue.DataValueField = "ValueCode";
                    ddlpValue.DataTextField = "ValueDescription";
                    ddlpValue.DataBind();
                    ddlpValue.Items.Insert(0, new ListItem("", ""));
                }
                else if (Convert.ToInt16(dr["IsValueTable"]) == 1)
                {
                    DataSet dsParmValus = new DataSet();
                    dsParmValus = BLL.MainScreen.FetchParmValues(Convert.ToInt32(dr["ParmCode"]), HidModelType.Value);

                    ddlpValue.DataSource = dsParmValus;
                    ddlpValue.DataValueField = "ValueCode";
                    ddlpValue.DataTextField = "ValueDescription";
                    ddlpValue.DataBind();
                    ddlpValue.Items.Insert(0, new ListItem("", ""));

                    //ddlpValue.SelectedIndex = ddlpValue.Items.IndexOf(ddlpValue.Items.FindByText(Convert.ToString(dr["DefaultValue"])));							
                    ddlpValue.SelectedIndex = ddlpValue.Items.IndexOf(ddlpValue.Items.FindByText(Convert.ToString(dr["ValueDescription"]).ToUpper()));

                    if (dsParmValus.Tables[0].Rows.Count > 0)
                    {
                        ddlpValue.Visible = true;
                        txtpValue.Visible = false;
                    }
                    else
                    {
                        txtpValue.Visible = true;
                        txtpValue.Text = dr["ValueDescription"].ToString();
                        ddlpValue.Visible = false;
                    }
                }
                else
                {
                    txtpValue.Text = dr["ValueDescription"].ToString();
                    txtpValue.Visible = true;
                    ddlpValue.Visible = false;
                }

                if (Convert.ToBoolean(dr["HideFlag"]))
                    e.Row.Visible = false;

                if (Convert.ToBoolean(dr["BIsNonStd"]))
                {
                    cbNStd.Enabled = true;
                    int n = GetColumnIndexByName(grTemplate02, "NON STD");
                    e.Row.Cells[n].BackColor = System.Drawing.Color.SteelBlue;

                }
                else
                    cbNStd.Enabled = false;

                if (e.Row.RowIndex == 0)
                {
                    e.Row.Cells[0].Text = "" + (e.Row.RowIndex + 1);
                }
                else if (Convert.ToBoolean(dr["HideFlag"]))
                {
                    int iSeqNo = Convert.ToInt16(grTemplate02.Rows[e.Row.RowIndex - 1].Cells[0].Text.ToString().Trim());
                    e.Row.Cells[0].Text = "" + iSeqNo;
                }
                else if (!Convert.ToBoolean(dr["HideFlag"]))
                {
                    int iSeqNo = Convert.ToInt16(grTemplate02.Rows[e.Row.RowIndex - 1].Cells[0].Text.ToString().Trim()) + 1;
                    e.Row.Cells[0].Text = "" + iSeqNo;
                }
            }
        }
        protected void grTemplate03_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlpValue = (DropDownList)e.Row.FindControl("ddlValue");
                TextBox txtpValue = (TextBox)e.Row.FindControl("txtValue");
                CheckBox cbNStd = (CheckBox)e.Row.FindControl("cbNS");

                if (Convert.ToBoolean(dr["FIsNonStd"]))
                {
                    txtpValue.Visible = true;
                    txtpValue.Text = dr["ValueDescription"].ToString();
                    ddlpValue.Visible = false;
                    cbNStd.Checked = true;

                    DataSet dsParmValus = new DataSet();
                    dsParmValus = BLL.MainScreen.FetchParmValues(Convert.ToInt32(dr["ParmCode"]), HidModelType.Value);

                    ddlpValue.DataSource = dsParmValus;
                    ddlpValue.DataValueField = "ValueCode";
                    ddlpValue.DataTextField = "ValueDescription";
                    ddlpValue.DataBind();
                    ddlpValue.Items.Insert(0, new ListItem("", ""));
                }
                else if (Convert.ToInt16(dr["IsValueTable"]) == 1)
                {
                    DataSet dsParmValus = new DataSet();
                    dsParmValus = BLL.MainScreen.FetchParmValues(Convert.ToInt32(dr["ParmCode"]), HidModelType.Value);

                    ddlpValue.DataSource = dsParmValus;
                    ddlpValue.DataValueField = "ValueCode";
                    ddlpValue.DataTextField = "ValueDescription";
                    ddlpValue.DataBind();
                    ddlpValue.Items.Insert(0, new ListItem("", ""));

                    //ddlpValue.SelectedIndex = ddlpValue.Items.IndexOf(ddlpValue.Items.FindByText(Convert.ToString(dr["DefaultValue"])));							
                    ddlpValue.SelectedIndex = ddlpValue.Items.IndexOf(ddlpValue.Items.FindByText(Convert.ToString(dr["ValueDescription"]).ToUpper()));

                    if (dsParmValus.Tables[0].Rows.Count > 0)
                    {
                        ddlpValue.Visible = true;
                        txtpValue.Visible = false;
                    }
                    else
                    {
                        txtpValue.Visible = true;
                        txtpValue.Text = dr["ValueDescription"].ToString();
                        ddlpValue.Visible = false;
                    }
                }
                else
                {
                    txtpValue.Text = dr["ValueDescription"].ToString();
                    txtpValue.Visible = true;
                    ddlpValue.Visible = false;
                }

                if (Convert.ToBoolean(dr["HideFlag"]))
                    e.Row.Visible = false;

                if (Convert.ToBoolean(dr["BIsNonStd"]))
                {
                    cbNStd.Enabled = true;
                    int n = GetColumnIndexByName(grTemplate03, "NON STD");
                    e.Row.Cells[n].BackColor = System.Drawing.Color.SteelBlue;

                }
                else
                    cbNStd.Enabled = false;

                if (e.Row.RowIndex == 0)
                {
                    e.Row.Cells[0].Text = "" + (e.Row.RowIndex + 1);
                }
                else if (Convert.ToBoolean(dr["HideFlag"]))
                {
                    int iSeqNo = Convert.ToInt16(grTemplate03.Rows[e.Row.RowIndex - 1].Cells[0].Text.ToString().Trim());
                    e.Row.Cells[0].Text = "" + iSeqNo;
                }
                else if (!Convert.ToBoolean(dr["HideFlag"]))
                {
                    int iSeqNo = Convert.ToInt16(grTemplate03.Rows[e.Row.RowIndex - 1].Cells[0].Text.ToString().Trim()) + 1;
                    e.Row.Cells[0].Text = "" + iSeqNo;
                }
            }
        }
        protected void grTemplate04_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlpValue = (DropDownList)e.Row.FindControl("ddlValue");
                TextBox txtpValue = (TextBox)e.Row.FindControl("txtValue");
                CheckBox cbNStd = (CheckBox)e.Row.FindControl("cbNS");

                if (Convert.ToBoolean(dr["FIsNonStd"]))
                {
                    txtpValue.Visible = true;
                    txtpValue.Text = dr["ValueDescription"].ToString();
                    ddlpValue.Visible = false;
                    cbNStd.Checked = true;

                    DataSet dsParmValus = new DataSet();
                    dsParmValus = BLL.MainScreen.FetchParmValues(Convert.ToInt32(dr["ParmCode"]), HidModelType.Value);

                    ddlpValue.DataSource = dsParmValus;
                    ddlpValue.DataValueField = "ValueCode";
                    ddlpValue.DataTextField = "ValueDescription";
                    ddlpValue.DataBind();
                    ddlpValue.Items.Insert(0, new ListItem("", ""));
                }
                else if (Convert.ToInt16(dr["IsValueTable"]) == 1)
                {
                    DataSet dsParmValus = new DataSet();
                    dsParmValus = BLL.MainScreen.FetchParmValues(Convert.ToInt32(dr["ParmCode"]), HidModelType.Value);

                    ddlpValue.DataSource = dsParmValus;
                    ddlpValue.DataValueField = "ValueCode";
                    ddlpValue.DataTextField = "ValueDescription";
                    ddlpValue.DataBind();
                    ddlpValue.Items.Insert(0, new ListItem("", ""));

                    //ddlpValue.SelectedIndex = ddlpValue.Items.IndexOf(ddlpValue.Items.FindByText(Convert.ToString(dr["DefaultValue"])));							
                    ddlpValue.SelectedIndex = ddlpValue.Items.IndexOf(ddlpValue.Items.FindByText(Convert.ToString(dr["ValueDescription"]).ToUpper()));

                    if (dsParmValus.Tables[0].Rows.Count > 0)
                    {
                        ddlpValue.Visible = true;
                        txtpValue.Visible = false;
                    }
                    else
                    {
                        txtpValue.Visible = true;
                        txtpValue.Text = dr["ValueDescription"].ToString();
                        ddlpValue.Visible = false;
                    }
                }
                else
                {
                    txtpValue.Text = dr["ValueDescription"].ToString();
                    txtpValue.Visible = true;
                    ddlpValue.Visible = false;
                }

                if (Convert.ToBoolean(dr["HideFlag"]))
                    e.Row.Visible = false;

                if (Convert.ToBoolean(dr["BIsNonStd"]))
                {
                    cbNStd.Enabled = true;
                    int n = GetColumnIndexByName(grTemplate04, "NON STD");
                    e.Row.Cells[n].BackColor = System.Drawing.Color.SteelBlue;

                }
                else
                    cbNStd.Enabled = false;

                if (e.Row.RowIndex == 0)
                {
                    e.Row.Cells[0].Text = "" + (e.Row.RowIndex + 1);
                }
                else if (Convert.ToBoolean(dr["HideFlag"]))
                {
                    int iSeqNo = Convert.ToInt16(grTemplate04.Rows[e.Row.RowIndex - 1].Cells[0].Text.ToString().Trim());
                    e.Row.Cells[0].Text = "" + iSeqNo;
                }
                else if (!Convert.ToBoolean(dr["HideFlag"]))
                {
                    int iSeqNo = Convert.ToInt16(grTemplate04.Rows[e.Row.RowIndex - 1].Cells[0].Text.ToString().Trim()) + 1;
                    e.Row.Cells[0].Text = "" + iSeqNo;
                }
            }
        }
        protected void grTemplate05_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlpValue = (DropDownList)e.Row.FindControl("ddlValue");
                TextBox txtpValue = (TextBox)e.Row.FindControl("txtValue");
                CheckBox cbNStd = (CheckBox)e.Row.FindControl("cbNS");

                if (Convert.ToBoolean(dr["FIsNonStd"]))
                {
                    txtpValue.Visible = true;
                    txtpValue.Text = dr["ValueDescription"].ToString();
                    ddlpValue.Visible = false;
                    cbNStd.Checked = true;

                    DataSet dsParmValus = new DataSet();
                    dsParmValus = BLL.MainScreen.FetchParmValues(Convert.ToInt32(dr["ParmCode"]), HidModelType.Value);

                    ddlpValue.DataSource = dsParmValus;
                    ddlpValue.DataValueField = "ValueCode";
                    ddlpValue.DataTextField = "ValueDescription";
                    ddlpValue.DataBind();
                    ddlpValue.Items.Insert(0, new ListItem("", ""));
                }
                else if (Convert.ToInt16(dr["IsValueTable"]) == 1)
                {
                    DataSet dsParmValus = new DataSet();
                    dsParmValus = BLL.MainScreen.FetchParmValues(Convert.ToInt32(dr["ParmCode"]), HidModelType.Value);

                    ddlpValue.DataSource = dsParmValus;
                    ddlpValue.DataValueField = "ValueCode";
                    ddlpValue.DataTextField = "ValueDescription";
                    ddlpValue.DataBind();
                    ddlpValue.Items.Insert(0, new ListItem("", ""));

                    //ddlpValue.SelectedIndex = ddlpValue.Items.IndexOf(ddlpValue.Items.FindByText(Convert.ToString(dr["DefaultValue"])));							
                    ddlpValue.SelectedIndex = ddlpValue.Items.IndexOf(ddlpValue.Items.FindByText(Convert.ToString(dr["ValueDescription"]).ToUpper()));

                    if (dsParmValus.Tables[0].Rows.Count > 0)
                    {
                        ddlpValue.Visible = true;
                        txtpValue.Visible = false;
                    }
                    else
                    {
                        txtpValue.Visible = true;
                        txtpValue.Text = dr["ValueDescription"].ToString();
                        ddlpValue.Visible = false;
                    }
                }
                else
                {
                    txtpValue.Text = dr["ValueDescription"].ToString();
                    txtpValue.Visible = true;
                    ddlpValue.Visible = false;
                }

                if (Convert.ToBoolean(dr["HideFlag"]))
                    e.Row.Visible = false;

                if (Convert.ToBoolean(dr["BIsNonStd"]))
                {
                    cbNStd.Enabled = true;
                    int n = GetColumnIndexByName(grTemplate05, "NON STD");
                    e.Row.Cells[n].BackColor = System.Drawing.Color.SteelBlue;

                }
                else
                    cbNStd.Enabled = false;

                if (e.Row.RowIndex == 0)
                {
                    e.Row.Cells[0].Text = "" + (e.Row.RowIndex + 1);
                }
                else if (Convert.ToBoolean(dr["HideFlag"]))
                {
                    int iSeqNo = Convert.ToInt16(grTemplate05.Rows[e.Row.RowIndex - 1].Cells[0].Text.ToString().Trim());
                    e.Row.Cells[0].Text = "" + iSeqNo;
                }
                else if (!Convert.ToBoolean(dr["HideFlag"]))
                {
                    int iSeqNo = Convert.ToInt16(grTemplate05.Rows[e.Row.RowIndex - 1].Cells[0].Text.ToString().Trim()) + 1;
                    e.Row.Cells[0].Text = "" + iSeqNo;
                }
            }
        }
        protected void grTemplate06_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlpValue = (DropDownList)e.Row.FindControl("ddlValue");
                TextBox txtpValue = (TextBox)e.Row.FindControl("txtValue");
                CheckBox cbNStd = (CheckBox)e.Row.FindControl("cbNS");

                if (Convert.ToBoolean(dr["FIsNonStd"]))
                {
                    txtpValue.Visible = true;
                    txtpValue.Text = dr["ValueDescription"].ToString();
                    ddlpValue.Visible = false;
                    cbNStd.Checked = true;

                    DataSet dsParmValus = new DataSet();
                    dsParmValus = BLL.MainScreen.FetchParmValues(Convert.ToInt32(dr["ParmCode"]), HidModelType.Value);

                    ddlpValue.DataSource = dsParmValus;
                    ddlpValue.DataValueField = "ValueCode";
                    ddlpValue.DataTextField = "ValueDescription";
                    ddlpValue.DataBind();
                    ddlpValue.Items.Insert(0, new ListItem("", ""));
                }
                else if (Convert.ToInt16(dr["IsValueTable"]) == 1)
                {
                    DataSet dsParmValus = new DataSet();
                    dsParmValus = BLL.MainScreen.FetchParmValues(Convert.ToInt32(dr["ParmCode"]), HidModelType.Value);

                    ddlpValue.DataSource = dsParmValus;
                    ddlpValue.DataValueField = "ValueCode";
                    ddlpValue.DataTextField = "ValueDescription";
                    ddlpValue.DataBind();
                    ddlpValue.Items.Insert(0, new ListItem("", ""));

                    //ddlpValue.SelectedIndex = ddlpValue.Items.IndexOf(ddlpValue.Items.FindByText(Convert.ToString(dr["DefaultValue"])));							
                    ddlpValue.SelectedIndex = ddlpValue.Items.IndexOf(ddlpValue.Items.FindByText(Convert.ToString(dr["ValueDescription"]).ToUpper()));

                    if (dsParmValus.Tables[0].Rows.Count > 0)
                    {
                        ddlpValue.Visible = true;
                        txtpValue.Visible = false;
                    }
                    else
                    {
                        txtpValue.Visible = true;
                        txtpValue.Text = dr["ValueDescription"].ToString();
                        ddlpValue.Visible = false;
                    }
                }
                else
                {
                    txtpValue.Text = dr["ValueDescription"].ToString();
                    txtpValue.Visible = true;
                    ddlpValue.Visible = false;
                }

                if (Convert.ToBoolean(dr["HideFlag"]))
                    e.Row.Visible = false;

                if (Convert.ToBoolean(dr["BIsNonStd"]))
                {
                    cbNStd.Enabled = true;
                    int n = GetColumnIndexByName(grTemplate06, "NON STD");
                    e.Row.Cells[n].BackColor = System.Drawing.Color.SteelBlue;
                }
                else
                    cbNStd.Enabled = false;

                if (e.Row.RowIndex == 0)
                {
                    e.Row.Cells[0].Text = "" + (e.Row.RowIndex + 1);
                }
                else if (Convert.ToBoolean(dr["HideFlag"]))
                {
                    int iSeqNo = Convert.ToInt16(grTemplate06.Rows[e.Row.RowIndex - 1].Cells[0].Text.ToString().Trim());
                    e.Row.Cells[0].Text = "" + iSeqNo;
                }
                else if (!Convert.ToBoolean(dr["HideFlag"]))
                {
                    int iSeqNo = Convert.ToInt16(grTemplate06.Rows[e.Row.RowIndex - 1].Cells[0].Text.ToString().Trim()) + 1;
                    e.Row.Cells[0].Text = "" + iSeqNo;
                }
                
            }
        }
        protected void grTemplate07_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlpValue = (DropDownList)e.Row.FindControl("ddlValue");
                TextBox txtpValue = (TextBox)e.Row.FindControl("txtValue");
                CheckBox cbNStd = (CheckBox)e.Row.FindControl("cbNS");

                if (Convert.ToBoolean(dr["FIsNonStd"]))
                {
                    txtpValue.Visible = true;
                    txtpValue.Text = dr["ValueDescription"].ToString();
                    ddlpValue.Visible = false;
                    cbNStd.Checked = true;

                    DataSet dsParmValus = new DataSet();
                    dsParmValus = BLL.MainScreen.FetchParmValues(Convert.ToInt32(dr["ParmCode"]), HidModelType.Value);

                    ddlpValue.DataSource = dsParmValus;
                    ddlpValue.DataValueField = "ValueCode";
                    ddlpValue.DataTextField = "ValueDescription";
                    ddlpValue.DataBind();
                    ddlpValue.Items.Insert(0, new ListItem("", ""));
                }
                else if (Convert.ToInt16(dr["IsValueTable"]) == 1)
                {
                    DataSet dsParmValus = new DataSet();
                    dsParmValus = BLL.MainScreen.FetchParmValues(Convert.ToInt32(dr["ParmCode"]), HidModelType.Value);

                    ddlpValue.DataSource = dsParmValus;
                    ddlpValue.DataValueField = "ValueCode";
                    ddlpValue.DataTextField = "ValueDescription";
                    ddlpValue.DataBind();
                    ddlpValue.Items.Insert(0, new ListItem("", ""));

                    //ddlpValue.SelectedIndex = ddlpValue.Items.IndexOf(ddlpValue.Items.FindByText(Convert.ToString(dr["DefaultValue"])));							
                    ddlpValue.SelectedIndex = ddlpValue.Items.IndexOf(ddlpValue.Items.FindByText(Convert.ToString(dr["ValueDescription"]).ToUpper()));

                    if (dsParmValus.Tables[0].Rows.Count > 0)
                    {
                        ddlpValue.Visible = true;
                        txtpValue.Visible = false;
                    }
                    else
                    {
                        txtpValue.Visible = true;
                        txtpValue.Text = dr["ValueDescription"].ToString();
                        ddlpValue.Visible = false;
                    }
                }
                else
                {
                    txtpValue.Text = dr["ValueDescription"].ToString();
                    txtpValue.Visible = true;
                    ddlpValue.Visible = false;
                }

                if (Convert.ToBoolean(dr["HideFlag"]))
                    e.Row.Visible = false;

                if (Convert.ToBoolean(dr["BIsNonStd"]))
                {
                    cbNStd.Enabled = true;
                    int n = GetColumnIndexByName(grTemplate07, "NON STD");
                    e.Row.Cells[n].BackColor = System.Drawing.Color.SteelBlue;
                }
                else
                    cbNStd.Enabled = false;

                if (e.Row.RowIndex == 0)
                {
                    e.Row.Cells[0].Text = "" + (e.Row.RowIndex + 1);
                }
                else if (Convert.ToBoolean(dr["HideFlag"]))
                {
                    int iSeqNo = Convert.ToInt16(grTemplate07.Rows[e.Row.RowIndex - 1].Cells[0].Text.ToString().Trim());
                    e.Row.Cells[0].Text = "" + iSeqNo;
                }
                else if (!Convert.ToBoolean(dr["HideFlag"]))
                {
                    int iSeqNo = Convert.ToInt16(grTemplate07.Rows[e.Row.RowIndex - 1].Cells[0].Text.ToString().Trim()) + 1;
                    e.Row.Cells[0].Text = "" + iSeqNo;
                }
            }
        }

        protected void rbEC_CheckedChanged(object sender, EventArgs e)
        {
            if (Session["Mode"].ToString().ToUpper() == "NEW")
            {
                if (rbE.Checked)
                    BindGrids(grScreenTemplate, "MAIN SCREEN", "", 0);
                else
                    BindGrids(grScreenTemplate, "MAIN SCREEN COMMON", "", 0);
                MakeVisible( true, false,  false, false, false, false, false, false, false, false);
                SetHeading();
                MakeVisibleByStatus(lblStatus.Text);
            }
            else
            {
                if (rbE.Checked)
                    BindGridForLoad(grScreenTemplate, "MAIN SCREEN", HidContract.Value, 0);
                else
                    BindGridForLoad(grScreenTemplate, "MAIN SCREEN COMMON", HidContract.Value, 0);

                MakeVisible(true, false, false, false, false, false, false, false, false, false);
                SetHeading();

                //BLL.MainScreen.UpdateStatuTempMaster(Convert.ToInt32(Session["RefNo"]), "", Session["UserName"].ToString());
                lblStatus.Text = "Status : " + BLL.MainScreen.GetStatuTempMaster(Convert.ToInt32(Session["RefNo"]));
                MakeVisibleByStatus("");
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveContract(true);
            if (grModel.Visible)
                btnMainSpec_Click(btnMainSpec, EventArgs.Empty);
            else if (grTemplate02.Visible)
                btnMacSpec_Click(btnMacSpec, EventArgs.Empty);
            else if (grTemplate03.Visible)
                btnCopOpFn_Click(btnCopOpFn, EventArgs.Empty);
            else if (grTemplate04.Visible)
                btnHallSpec_Click(btnHallSpec, EventArgs.Empty);
            else if (grTemplate05.Visible)
                btnCarSpec_Click(btnCarSpec, EventArgs.Empty);
            else if (grTemplate06.Visible)
                btnHoistSpec_Click(btnHoistSpec, EventArgs.Empty);
            else if (grTemplate07.Visible)
                btnRemarks_Click(btnRemarks, EventArgs.Empty);
        }
        private void SaveContract(bool isSaveOnly)
        {
            try
            {
                string ModelType = "";

                if (rbE.Checked)
                    ModelType = NewModelType();
                else
                    HidModelType.Value = "COMMON";

                if (ModelType != HidModelType.Value)
                {
                    if (rbE.Checked)
                        GetModelType();
                    else
                        HidModelType.Value = "COMMON";
                }
                if (HidModelType.Value == "" || HidModelType.Value == null)
                {
                    lblError.Text = "Model Type cannot be blank";
                    return;
                }


                if (Convert.ToInt32(Session["RefNo"]) > 0)
                    BLL.MainScreen.UpdateTempMaster(Convert.ToInt32(Session["RefNo"]), GenerateContractNo(), HidModelType.Value, GetNoOfEquip(), Session["UserName"].ToString()).ToString();
                else
                    Session["RefNo"] = BLL.MainScreen.UpdateTempMaster(0, GenerateContractNo(), HidModelType.Value, GetNoOfEquip(), Session["UserName"].ToString() ).ToString();

                // Delete the record from TempSpec before Insert
                BLL.MainScreen.DeleteTempSpec(Convert.ToInt32(Session["RefNo"]));

                if (rbE.Checked)
                {
                    SaveScreenTemplate(grScreenTemplate, "txtValue", "ddlValue", Convert.ToInt32(Session["RefNo"]), false);
                    SaveScreenTemplate(grModel, "txtValue", "ddlValue", Convert.ToInt32(Session["RefNo"]), false);
                    SaveScreenTemplate(grTemplate02, "txtValue", "ddlValue", Convert.ToInt32(Session["RefNo"]), false);
                    SaveScreenTemplate(grTemplate03, "txtValue", "ddlValue", Convert.ToInt32(Session["RefNo"]), false);
                    SaveScreenTemplate(grTemplate04, "txtValue", "ddlValue", Convert.ToInt32(Session["RefNo"]), false);
                    SaveScreenTemplate(grTemplate05, "txtValue", "ddlValue", Convert.ToInt32(Session["RefNo"]), false);
                    SaveScreenTemplate(grTemplate06, "txtValue", "ddlValue", Convert.ToInt32(Session["RefNo"]), false);
                    SaveScreenTemplate(grTemplate07, "txtValue", "ddlValue", Convert.ToInt32(Session["RefNo"]), false);
                }
                else
                {
                    SaveScreenTemplate(grScreenTemplate, "txtValue", "ddlValue", Convert.ToInt32(Session["RefNo"]), false);
                    SaveScreenTemplate(grModel, "txtValue", "ddlValue", Convert.ToInt32(Session["RefNo"]), false);
                }

                BLL.MainScreen.UpdateStatuTempMaster(Convert.ToInt32(Session["RefNo"]), "SVD", Session["UserName"].ToString());
                if (isSaveOnly)
                {
                    lblError.Text = "Specification Values Saved";
                    //Response.Write("<Script>alert('Specification Values Saved');location.href='ViewOutput.aspx';</Script>");				
                    //Response.Write("<Script>alert('Specification Values Saved');</Script>"); not working in AJAX
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Specification Values Saved.');", true); // message box for AJAX
                }
            }
            catch (Exception ex)
            {
                lblError.Text += "  " + ex.Message;   
            }            
        }

        protected void cbNS_CheckedChanged(object sender, EventArgs e)
        {
            //IsSelectedIndexChanged = false;
            CheckBox ckBox = (CheckBox)sender;

            TableCell cell = ckBox.Parent as TableCell;
            //DataGridItem item = cell.Parent as DataGridItem;  //For MS Datagrid            
            GridViewRow item = cell.Parent as GridViewRow ; //For MS GridView
            //C1.Web.C1WebGrid.C1GridItem item = cell.Parent as C1.Web.C1WebGrid.C1GridItem; // For C1WebGrid
             
            //System.Web.UI.WebControls.DataGridItem;
            int index = item.RowIndex;
            string content = item.Cells[0].Text;
            CheckBox ckBNS = (CheckBox)item.Cells[3].FindControl("cbNS");

            DropDownList ddlPValue = (DropDownList)item.Cells[4].FindControl("ddlValue");
            TextBox txtPValue = (TextBox)item.Cells[4].FindControl("txtValue");
            if (ckBNS.Checked)
            {
                txtPValue.Visible = true;
                ddlPValue.Visible = false;
            }
            else
            {
                txtPValue.Visible = false;
                ddlPValue.Visible = true;
            }
            //SMSelectedValue = ddlType.SelectedItem.Text;        

            //if (SMSelectedValue == "" || SMSelectedValue == "Select One")
            //{
            //    ddlThickness.Items.Clear();
            //    ddlThickness.Items.Insert(0, new ListItem("", "0"));
            //}
            //else
            //{
            //    dsSplMtlThickness = BLL.ProductSpecification.GetSpecialMaterialThickness(SMSelectedValue.Trim());
            //    ddlThickness.Items.Clear();
            //    ddlThickness.DataSource = dsSplMtlThickness;
            //    ddlThickness.DataValueField = "Thickness";
            //    ddlThickness.DataTextField = "Thickness";
            //    ddlThickness.DataBind();
            //    ddlThickness.Items.Insert(0, new ListItem("Select One", "Select One"));
            //}
            //Response.Write("DDLValue is " + SMSelectedValue+ "\t");						
            //Response.Write(	String.Format("Row {0} contains {1}", index, content)); 
        }

        protected void grModel_PreRender(object sender, EventArgs e)
        {
            // You only need the following 2 lines of code if you are not 
            // using an ObjectDataSource of SqlDataSource
            if (!Page.IsPostBack)
            {
                BindGrids(grModel, HidModelType.Value, "", 1);
            }
            if (grModel.Rows.Count > 0)
            {
                //This replaces <td> with <th> and adds the scope attribute
                grModel.UseAccessibleHeader = true;

                //This will add the <thead> and <tbody> elements
                grModel.HeaderRow.TableSection = TableRowSection.TableHeader;

                //This adds the <tfoot> element. 
                //Remove if you don't have a footer row
                //gvTheGrid.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

        protected void grTemplate02_PreRender(object sender, EventArgs e)
        {
            // You only need the following 2 lines of code if you are not 
            // using an ObjectDataSource of SqlDataSource
            if (!Page.IsPostBack)
            {
                BindGrids(grTemplate02, HidModelType.Value, "", 2);
            }
            if (grTemplate02.Rows.Count > 0)
            {
                //This replaces <td> with <th> and adds the scope attribute
                grTemplate02.UseAccessibleHeader = true;

                //This will add the <thead> and <tbody> elements
                grTemplate02.HeaderRow.TableSection = TableRowSection.TableHeader;

                //This adds the <tfoot> element. 
                //Remove if you don't have a footer row
                //gvTheGrid.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

        protected void grTemplate03_PreRender(object sender, EventArgs e)
        {
            // You only need the following 2 lines of code if you are not 
            // using an ObjectDataSource of SqlDataSource
            if (!Page.IsPostBack)
            {
                BindGrids(grTemplate03, HidModelType.Value, "", 3);
            }
            if (grTemplate03.Rows.Count > 0)
            {
                //This replaces <td> with <th> and adds the scope attribute
                grTemplate03.UseAccessibleHeader = true;

                //This will add the <thead> and <tbody> elements
                grTemplate03.HeaderRow.TableSection = TableRowSection.TableHeader;

                //This adds the <tfoot> element. 
                //Remove if you don't have a footer row
                //gvTheGrid.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

        protected void grTemplate04_PreRender(object sender, EventArgs e)
        {
            // You only need the following 2 lines of code if you are not 
            // using an ObjectDataSource of SqlDataSource
            if (!Page.IsPostBack)
            {
                BindGrids(grTemplate04, HidModelType.Value, "", 4);
            }
            if (grTemplate04.Rows.Count > 0)
            {
                //This replaces <td> with <th> and adds the scope attribute
                grTemplate04.UseAccessibleHeader = true;

                //This will add the <thead> and <tbody> elements
                grTemplate04.HeaderRow.TableSection = TableRowSection.TableHeader;

                //This adds the <tfoot> element. 
                //Remove if you don't have a footer row
                //gvTheGrid.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

        protected void grTemplate05_PreRender(object sender, EventArgs e)
        {
            // You only need the following 2 lines of code if you are not 
            // using an ObjectDataSource of SqlDataSource
            if (!Page.IsPostBack)
            {
                BindGrids(grTemplate05, HidModelType.Value, "", 5);
            }
            if (grTemplate05.Rows.Count > 0)
            {
                //This replaces <td> with <th> and adds the scope attribute
                grTemplate05.UseAccessibleHeader = true;

                //This will add the <thead> and <tbody> elements
                grTemplate05.HeaderRow.TableSection = TableRowSection.TableHeader;

                //This adds the <tfoot> element. 
                //Remove if you don't have a footer row
                //gvTheGrid.FooterRow.TableSection = TableRowSection.TableFooter;
            }

        }

        protected void grTemplate06_PreRender(object sender, EventArgs e)
        {
            // You only need the following 2 lines of code if you are not 
            // using an ObjectDataSource of SqlDataSource
            if (!Page.IsPostBack)
            {
                BindGrids(grTemplate06, HidModelType.Value, "", 6);
            }
            if (grTemplate06.Rows.Count > 0)
            {
                //This replaces <td> with <th> and adds the scope attribute
                grTemplate06.UseAccessibleHeader = true;

                //This will add the <thead> and <tbody> elements
                grTemplate06.HeaderRow.TableSection = TableRowSection.TableHeader;

                //This adds the <tfoot> element. 
                //Remove if you don't have a footer row
                //gvTheGrid.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

        protected void grTemplate07_PreRender(object sender, EventArgs e)
        {
            // You only need the following 2 lines of code if you are not 
            // using an ObjectDataSource of SqlDataSource
            if (!Page.IsPostBack)
            {
                BindGrids(grTemplate07, HidModelType.Value, "", 7);
            }
            if (grTemplate07.Rows.Count > 0)
            {
                //This replaces <td> with <th> and adds the scope attribute
                grTemplate07.UseAccessibleHeader = true;

                //This will add the <thead> and <tbody> elements
                grTemplate07.HeaderRow.TableSection = TableRowSection.TableHeader;

                //This adds the <tfoot> element. 
                //Remove if you don't have a footer row
                //gvTheGrid.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

        protected void grScreenTemplate_PreRender(object sender, EventArgs e)
        {
            // You only need the following 2 lines of code if you are not 
            // using an ObjectDataSource of SqlDataSource
            if (!Page.IsPostBack)
            {
                if (rbE.Checked)
                    BindGridForLoad(grScreenTemplate, "MAIN SCREEN", HidContract.Value, 0);
                else
                    BindGridForLoad(grScreenTemplate, "MAIN SCREEN COMMON", HidContract.Value, 0);
                //BindGrids(grScreenTemplate, "MAIN SCREEN", "", 0);
            }
            if (grScreenTemplate.Rows.Count > 0)
            {
                //This replaces <td> with <th> and adds the scope attribute
                grScreenTemplate.UseAccessibleHeader = true;

                //This will add the <thead> and <tbody> elements
                grScreenTemplate.HeaderRow.TableSection = TableRowSection.TableHeader;

                //This adds the <tfoot> element. 
                //Remove if you don't have a footer row
                //gvTheGrid.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

        private int GetColumnIndexByName(GridView grid, string name)
        {
            foreach (DataControlField col in grid.Columns)
            {
                if (col.HeaderText.ToUpper().Trim() == name.ToUpper().Trim())
                {
                    return grid.Columns.IndexOf(col);
                }
            }

            return -1;
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            // this is required for avoid error (control must be placed inside form tag)
        }
        //Added By Pradeep S
        //For Part Number Allocation ParmCode Existence Check
        private Boolean IsValidPartNumberAllocation()
        {
            Boolean bRetVal = false;
            try
            {

                String sProjectNo = "", sPartNoAllocation = "", sErrMessage = "", sProjectNoCaption="", sPartNoAllocationCaption="";
                int nPartNoAllocation;
                sProjectNo = GetProjectNo();
                sPartNoAllocation = GetPartNoAllocation();
                sProjectNoCaption = GetParamCodeCaption(ConfigurationSettings.AppSettings["PROJECT NO"]);
                sPartNoAllocationCaption = GetParamCodeCaption(ConfigurationSettings.AppSettings["PART NO ALLOCATION"]);

                if (String.IsNullOrEmpty(sProjectNo) && String.IsNullOrEmpty(sPartNoAllocation))
                {
                    sErrMessage += "<font color=#800080>" + sProjectNoCaption + "</font> Can not be blank; <BR>";
                    sErrMessage += "<font color=#800080>" + sPartNoAllocationCaption + "</font> Can not be blank; <BR>";
                }
                else if (String.IsNullOrEmpty(sProjectNo))
                {
                    sErrMessage += "<font color=#800080>" + sProjectNoCaption + "</font> Can not be blank; <BR>";
                }
                else if (String.IsNullOrEmpty(sPartNoAllocation))
                {
                    sErrMessage += "<font color=#800080>" + sPartNoAllocationCaption + "</font> Can not be blank; <BR>";
                }
                else if (!int.TryParse(sPartNoAllocation,out nPartNoAllocation))
                {
                    sErrMessage += "<font color=#800080>" + sPartNoAllocationCaption + "</font> should be numeric; <BR>";
                }
                else
                {
                    if (BLL.MainScreen.IsPartNoAllocationExists(Convert.ToInt32(Session["RefNo"]), sProjectNo, sPartNoAllocation))
                    {
                       sErrMessage += "<font color=#800080>" + sPartNoAllocationCaption + 
                                            "</font> alread exists, Please select different Part Number allocation; <BR>";

                    }
                    else
                    {
                        bRetVal = true;
                    }
                }
                if (!String.IsNullOrEmpty(sErrMessage))
                {
                    lblError.Text += "<font color=#0000ff>Error in MainScreen : </font><BR>";
                    lblError.Text += sErrMessage;
                }
            }
            catch (Exception ex)
            {
                lblError.Text += "<font color=#0000ff>Error in MainScreen : </font><BR>";
                lblError.Text += ex.Message;
                
            }
            return bRetVal;
        }
        private string GetProjectNo()
        {
            string sProjectNo = "";

            for (int i = 0; i < grScreenTemplate.Rows.Count; i++)
            {
                if (grScreenTemplate.DataKeys[i].Values[0].ToString() == ConfigurationSettings.AppSettings["PROJECT NO"]) 
                {
                    TextBox txtVal = (TextBox)grScreenTemplate.Rows[i].FindControl("txtValue");
                    sProjectNo = txtVal.Text;
                    break;                    
                }                
            }
            return sProjectNo;
        }
        private string GetParamCodeCaption(String sCaptionParmCode)
        {
            string sParamCodeCaption = "";

            for (int i = 0; i < grScreenTemplate.Rows.Count; i++)
            {
                if (grScreenTemplate.DataKeys[i].Values[0].ToString() == sCaptionParmCode)
                {                    
                    sParamCodeCaption = grScreenTemplate.Rows[i].Cells[2].Text;
                    break;
                }
            }
            return sParamCodeCaption;
        }
        private string GetPartNoAllocation()
        {
            string sPartNoAllocation = "";
            string sPartNoAllocationParmCode =Convert.ToString( ConfigurationSettings.AppSettings["PART NO ALLOCATION"]);

            for (int i = 0; i < grScreenTemplate.Rows.Count; i++)
            {
                if (grScreenTemplate.DataKeys[i].Values[0].ToString() == sPartNoAllocationParmCode)
                {
                    TextBox txtVal = (TextBox)grScreenTemplate.Rows[i].FindControl("txtValue");
                    sPartNoAllocation = txtVal.Text;
                    break;
                }
            }
            return sPartNoAllocation;
        }

        protected void lnkBtnCompute_Click(object sender, EventArgs e)
        {
            try
            {
                if (UpdateMatCatListForCompute())
                {
                    if (UpdateExcludedFunctionIDForCompute())
                    {
                        BLL.Login.InsertLogHistory("S", Session["UserId"].ToString(), "JBQ", Session["RefNo"].ToString());
                        //psp need to uncomment later
                        if (!Convert.ToBoolean(ConfigurationSettings.AppSettings["NOSComputeManually"]))
                        {
                            //MtlCEQ.Path = "FormatName:DIRECT=OS:" + ConfigurationSettings.AppSettings["QueuePath"] + "\\private$\\mtlcequeue";
                            MtlCEQ.Path = ConfigurationSettings.AppSettings["QueuePathCE"];
                            MtlCEQ.Send(Session["RefNo"].ToString() + "^" + ConfigurationSettings.AppSettings["SQLConnWS"] + "^" + ConfigurationSettings.AppSettings["LoadingClassParmCode"], "MtlCEQ");
                        }
                        BLL.MainScreen.UpdateStatuForCompute(Convert.ToInt32(Session["RefNo"]), "JBQ", Session["UserID"].ToString());
                        lblStatus.Text = "Status : " + BLL.MainScreen.GetStatuTempMaster(Convert.ToInt32(Session["RefNo"]));
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Submited for Computation.');location.href='ViewOutput.aspx';", true);
                    }
                    else
                    {
                        lblError.Text += "<font color=#0000ff>Error in Compute : </font><BR>";
                        lblError.Text += "Failed to submit for Computation";
                        RefreshInputScreen();
                    }
                }
                else
                {                    
                    lblError.Text += "<font color=#0000ff>Error in Compute : </font><BR>";
                    lblError.Text += "Failed to submit for Computation";                    
                    RefreshInputScreen();
                }
            }
            catch (System.Messaging.MessageQueueException MQEx)
            {
                lblError.Text += "<font color=#0000ff>Error in Message Queue : </font><BR>";
                lblError.Text += MQEx.Message;
            }
            catch (Exception ex)
            {
                lblError.Text += "<font color=#0000ff>Error in Compute : </font><BR>";
                lblError.Text += ex.Message;                
            }
        }
        private void PopUpComputeMaterialCategory()
        {
            try
            {
                AjaxControlToolkit.ModalPopupExtender modalPop = ((AjaxControlToolkit.ModalPopupExtender)
                                                    (this.FindControl("mpeCompute")));
                PopulateMaterialCategory(HidModelType.Value);
                PopulateExcludedSets(GenerateContractNo(),HidModelType.Value);   
                modalPop.Show();
                RefreshInputScreen();
                
                        
            }
            catch (SqlException Sqex)
            {
                lblError.Text += "<font color=#0000ff>SQL Error : </font><BR>";
                lblError.Text += Sqex.Message;
            }
            catch (Exception ex)
            {
                lblError.Text += "<font color=#0000ff>SQL Error : </font><BR>";
                lblError.Text += ex.Message;
            }
        }
        private void RefreshInputScreen()
        {
            try
            {
                if (pnlModel.Visible)
                    btnMainSpec_Click(btnMainSpec, EventArgs.Empty);
                else if (pnlTemplate02.Visible)
                    btnMacSpec_Click(btnMacSpec, EventArgs.Empty);
                else if (pnlTemplate03.Visible)
                    btnCopOpFn_Click(btnCopOpFn, EventArgs.Empty);
                else if (pnlTemplate04.Visible)
                    btnHallSpec_Click(btnHallSpec, EventArgs.Empty);
                else if (pnlTemplate05.Visible)
                    btnCarSpec_Click(btnCarSpec, EventArgs.Empty);
                else if (pnlTemplate06.Visible)
                    btnHoistSpec_Click(btnHoistSpec, EventArgs.Empty);
                else if (pnlTemplate07.Visible)
                    btnRemarks_Click(btnRemarks, EventArgs.Empty);
            }
            catch (Exception ex)
            {                
                
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
        //Added on 04/Mar/2015 - for MaterialCategory 
        protected void cbMatCat_DataBound(object sender, EventArgs e)
        {
            foreach (ListItem item in cbMatCat.Items)
            {
                item.Attributes.Add("onclick", "SetCheckBoxListAllStatus('cbMatCat','cbMatCatAll');SetExcludeSetList('cbMatCat','cbMatCatAll','" + item.Text + "')");
            }
        }
        //Added on 15/Sep/2014 - for MaterialCategory 
        private Boolean UpdateMatCatListForCompute()
        {
            Boolean bRetVal = false,bNotSelectedAny = true; ;
            String sMatCatID = "", sResult = "";
            int nRefNo = Convert.ToInt32(Session["RefNo"]);
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
                                bNotSelectedAny = false;
                                sResult = BLL.ViewOutPut.UpdateProcessFolder(
                                                            nRefNo, Convert.ToInt32(sMatCatID), lstMatCat.Text, HidModelType.Value, "U");

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
        //--Pradeep S -- Added on 08/June/2015 - for ExcludedComponents 
        private void PopulateExcludedSets(string sContractNo,string sModelType)
        {
            DataTable dt = new DataTable();
            sContractNo = sContractNo.Substring(0, 2);
            dt = BLL.ViewOutPut.GetExcludedSets4Project(sContractNo,sModelType).Tables[0];
            cbExcludedSet.DataSource = dt;
            cbExcludedSet.DataValueField = "MaterialCategoryID";
            cbExcludedSet.DataTextField = "SetName";
            cbExcludedSet.DataBind();
            cbExcludedSetAll.Checked = false;
        }
        //Added on 11/June/2015 - for ExcludedFunctionID for Selective Computation 
        private Boolean UpdateExcludedFunctionIDForCompute()
        {
            Boolean bRetVal = false;
            String sMatCatID = "", sResult = "";
            int nRefNo = Convert.ToInt32(Session["RefNo"]);
            try
            {
                sResult = BLL.ViewOutPut.UpdateExcludedFunctionID(nRefNo, "", "", "D");
                if (sResult.ToUpper() == "SUCCESS")
                {
                    foreach (ListItem lstExcludedSet in cbExcludedSet.Items)
                    {
                        if (lstExcludedSet.Selected)
                        {
                            sMatCatID = lstExcludedSet.Value;
                            if (!String.IsNullOrEmpty(sMatCatID))
                            {
                                sResult = BLL.ViewOutPut.UpdateExcludedFunctionID(
                                                            nRefNo, lstExcludedSet.Text, sMatCatID, "U");

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
        protected void cbExcludedSet_DataBound(object sender, EventArgs e)
        {
            foreach (ListItem item in cbExcludedSet.Items)
            {
                item.Attributes.Add("onclick", "SetExcludedSetCheckBoxAllStatus('cbExcludedSet','cbExcludedSetAll');");
                item.Attributes.Add("MatID", item.Value);

            }

        }
        //Added By Pradeep on 23/Mar/2015 
        // Disable ProjectNo,EquipNo and Part Allocation No , If the project already released
        private void DisableReleasedProjectParams(TextBox txtValue, DropDownList ddlpValue, String sParamCode)
        {
            
            
            try
            {
                if (Session["Mode"].ToString().ToUpper() != "NEW")
                {
                    if (Convert.ToBoolean(Session["Released"]))
                    {
                        // check for PROJECT NO - ParmCode = 41 is PROJECT NO
                        if (sParamCode == ConfigurationSettings.AppSettings["PROJECT NO"])
                        {
                            txtValue.Enabled = false;
                            ddlpValue.Enabled = false;
                        }
                        // check for EQUIPMENT NO - ParmCode = 42 is EQUIPMENT NO
                        if (sParamCode == ConfigurationSettings.AppSettings["EQUIPMENT NO"])
                        {
                            txtValue.Enabled = false;
                            ddlpValue.Enabled = false;
                        }
                        // check for PART NO ALLOCATION - ParmCode = 598 is PART NO ALLOCATION
                        if (sParamCode == ConfigurationSettings.AppSettings["PART NO ALLOCATION"])
                        {
                            txtValue.Enabled = false;
                            ddlpValue.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }
        private void SetPortNameList(TextBox txtValue, DropDownList ddlpValue, String sParamCode)
        {
            try
            {
                if (sParamCode == ConfigurationSettings.AppSettings["PORT NAME"])
                {
                    

                    string sProjNo = "";
                    string sCountryCode = "";
                    string sProjNoParmCode = Convert.ToString(ConfigurationSettings.AppSettings["PROJECT NO"]);

                    for (int i = 0; i < grScreenTemplate.Rows.Count; i++)
                    {
                        if (grScreenTemplate.DataKeys[i].Values[0].ToString() == sProjNoParmCode)
                        {
                            TextBox txtVal = (TextBox)grScreenTemplate.Rows[i].FindControl("txtValue");
                            sProjNo = txtVal.Text;
                            sCountryCode = sProjNo.Substring(0, 2);
                            break;
                        }
                    }
                    if (sCountryCode.ToUpper() != "SG")
                    {
                        txtValue.Enabled = false;
                        txtValue.Visible = false;
                        ddlpValue.Enabled = true;
                        ddlpValue.Visible = true;

                        DataSet dsParmValus = new DataSet();
                        dsParmValus = BLL.MainScreen.GetPortNames(sCountryCode);

                        ddlpValue.DataSource = dsParmValus;
                        ddlpValue.DataValueField = "PortID";
                        ddlpValue.DataTextField = "PortName";
                        ddlpValue.DataBind();
                        ddlpValue.Items.Insert(0, new ListItem("Select One", ""));
                        if (Session["Mode"].ToString().ToUpper() != "NEW")
                        {
                            //foreach (ListItem  lstItem  in ddlpValue.Items)
                            //{
                            //    if (Convert.ToString(lstItem.) == sSelectedPrj)
                            //    {
                            //        bPrjExist = true;
                            //        break;
                            //    }
                            //    nRowIdx++;
                            //}

                            ddlpValue.SelectedIndex = ddlpValue.Items.IndexOf(ddlpValue.Items.FindByValue(txtValue.Text));
                        }
                        else
                            ddlpValue.SelectedIndex = 0;
                        
                    }
                    else
                    {
                        txtValue.Enabled = false;
                        ddlpValue.Enabled = false;
                        ddlpValue.Visible = false;
                    }


                }
            }
            catch (Exception ex)
            {
                
               
            }
        }
        private void SetProjectLocationIndexChange()
        {
            try
            {

                string sProjectLocation = Convert.ToString(ConfigurationSettings.AppSettings["PROJECT LOCATION"]);
                for (int i = 0; i < grScreenTemplate.Rows.Count; i++)
                {
                    if (grScreenTemplate.DataKeys[i].Values[0].ToString() == sProjectLocation)
                    {
                        TextBox txtVal = (TextBox)grScreenTemplate.Rows[i].FindControl("txtValue");
                        DropDownList ddlpValue = (DropDownList)grScreenTemplate.Rows[i].FindControl("ddlValue");
                        ddlpValue.SelectedIndexChanged += new EventHandler(OnProjectLocationIndexChange);
                        ddlpValue.AutoPostBack = true;
                        break;
                    }
                }

                
            }
            catch (Exception ex)
            {
                
                
            }
        }
        private void SetPortNameToTextField()
        {
            try
            {
                
                string sPortNameParmCode = Convert.ToString(ConfigurationSettings.AppSettings["PORT NAME"]);
                for (int i = 0; i < grScreenTemplate.Rows.Count; i++)
                {
                    if (grScreenTemplate.DataKeys[i].Values[0].ToString() == sPortNameParmCode)
                    {
                        TextBox txtVal = (TextBox)grScreenTemplate.Rows[i].FindControl("txtValue");
                        DropDownList ddlpValue = (DropDownList)grScreenTemplate.Rows[i].FindControl("ddlValue");
                        if (ddlpValue.Visible && ddlpValue.Enabled)
                        {
                            txtVal.Text = ddlpValue.SelectedValue  ;
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                
                
            }
        }
        protected void OnProjectLocationIndexChange(object sender, EventArgs e)
        {
            try
            {
                DropDownList objDDL = (DropDownList)sender;
                GridViewRow gvRow = (GridViewRow)objDDL.NamingContainer;
                GridView ParentGrid = (GridView)gvRow.NamingContainer;
                String sParentID = ParentGrid.ID.ToString();

                String sSelVal = Convert.ToString(objDDL.SelectedItem).ToUpper();
                String sCountryCode = "";
                if (!String.IsNullOrEmpty(sSelVal))
                {

                    sCountryCode = BLL.MainScreen.GetCountryCode(sSelVal);
                    string sPortNameParmCode = Convert.ToString(ConfigurationSettings.AppSettings["PORT NAME"]);
                    for (int i = 0; i < grScreenTemplate.Rows.Count; i++)
                    {
                        if (grScreenTemplate.DataKeys[i].Values[0].ToString() == sPortNameParmCode)
                        {
                            TextBox txtValue = (TextBox)grScreenTemplate.Rows[i].FindControl("txtValue");
                            DropDownList ddlpValue = (DropDownList)grScreenTemplate.Rows[i].FindControl("ddlValue");
                            if (sCountryCode.ToUpper() != "SG")
                            {
                                txtValue.Enabled = false;
                                txtValue.Visible = false;
                                ddlpValue.Enabled = true;
                                ddlpValue.Visible = true;

                                DataSet dsParmValus = new DataSet();
                                dsParmValus = BLL.MainScreen.GetPortNames(sCountryCode);

                                ddlpValue.DataSource = dsParmValus;
                                ddlpValue.DataValueField = "PortID";
                                ddlpValue.DataTextField = "PortName";
                                ddlpValue.DataBind();
                                ddlpValue.Items.Insert(0, new ListItem("Select One", ""));
                                ddlpValue.SelectedIndex = 0;


                            }
                            else
                            {
                                txtValue.Text = "";
                                txtValue.Visible = true;
                                txtValue.Enabled = false;
                                ddlpValue.Enabled = false;
                                ddlpValue.Visible = false;
                            }
                            break;
                        }
                    }
                }

            }
            catch (Exception ex)
            {


            }
        }

        
        
    }
}
