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

namespace NewOrderingSystem
{
    public partial class Release : System.Web.UI.Page
    {
        private Boolean bIsExceptionClick = true;
        private DataSet dsUOM = new DataSet();
        private ArrayList alUOMException = new ArrayList();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
                Response.Redirect("Login.aspx");
            InitProperties();
            lblVersion.Text = ConfigurationSettings.AppSettings["NOSVersion"].ToString();
            bIsExceptionClick = true;
            if (!IsPostBack)
            {
                lblUserName.Text = "WELCOME " + Session["UserName"].ToString();
                PopulateVendor();
                PopulateMaterialCategory();
                //Added on 12/Sep/2014 to set the select all to checked
                cbAV.Checked = true;
                cbMatCatAll.Checked = true;

                if (!ClientScript.IsStartupScriptRegistered("SelectAllCheckBoxLists"))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(),
                        "SelectAllCheckBoxLists", "SelectAllVendor();" +
                        "SelectAllMatCat('cbMatCat','cbMatCatAll');" +
                        "SelectAllPrjGrpItem('cbPrjGrpItem','cbPrjGrpItemAll');", true);
                }
                //------
            }
            else
            {
                cbVL_DataBound(cbVL, EventArgs.Empty);
                cbMatCat_DataBound(cbMatCat, EventArgs.Empty);

            }

        }

        private void InitProperties()
        {
            lblCN.Text = Request.QueryString["ContractNo"];
            lblVNo.Text = Request.QueryString["VersionNo"];
            ViewState["RefNo"] = Convert.ToInt32(Request.QueryString["RefNo"]);
            ViewState["Model"] = Request.QueryString["Model"];

            dvVersionNo.Visible = false;// !btnRelease.Visible;
            dvProjectNo.Visible = btnRelease.Visible;
            dvPrjGrpItemMainHolder.Visible = btnRelease.Visible;
            chkGetExceptionFromEBOM.Attributes.Add("OnClick", "return UnCheckRevise(true);");
            chkReviseException.Attributes.Add("OnClick", "return UnCheckRevise(false);");


        }

        private void PopulateVendor()
        {
            DataTable dt = new DataTable();
            dt = BLL.Release.GetContractVendor(Convert.ToInt32(ViewState["RefNo"])).Tables[0];
            cbVL.DataSource = dt;
            cbVL.DataValueField = "VendorNo";
            cbVL.DataTextField = "VendorName";
            cbVL.DataBind();
        }
        //Added on 15/Sep/2014 - for MaterialCategory 
        private void PopulateMaterialCategory()
        {
            DataTable dt = new DataTable();
            dt = BLL.Release.GetMatCategoryFromOutput(Convert.ToInt32(ViewState["RefNo"])).Tables[0];
            dt.Rows[0].Delete();
            cbMatCat.DataSource = dt;
            cbMatCat.DataValueField = "MaterialCategoryID";
            cbMatCat.DataTextField = "MaterialCategoryName";
            cbMatCat.DataBind();
        }
        //Added on 15/Sep/2014 - for MaterialCategory 
        private void ResetScreenOnError(String sErrMsg)
        {
            grEx.DataSource = null;
            grEx.DataBind();
            //btnCan.Visible = false;
            //btnSav.Visible = false;
            navPnlBtn.Visible = false;
            lblError.Text = sErrMsg;
        }
        //Added on 15/Sep/2014 - for MaterialCategory 
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
        protected void btnMntExp_Click(object sender, EventArgs e)
        {
            try
            {
                string sVendorList = "All", sMatCatList = "";
                if (bIsExceptionClick)
                    ResetScreenOnError("");
                else
                    ResetScreenOnError(lblError.Text);

                if (IsMaterialCategorySelected())
                {
                    sMatCatList = GetMatCatList();
                    BindException(Convert.ToInt32(ViewState["RefNo"]), sVendorList, sMatCatList);
                }
                else
                {
                    ResetScreenOnError("Please select at least one material category");
                }
            }
            catch (Exception ex)
            {


            }
        }
        protected void btnMntExp_Click_Old(object sender, EventArgs e)
        {
            string Vendor = "", sMatCatList = "";
            if (bIsExceptionClick)
                ResetScreenOnError("");
            else
                ResetScreenOnError(lblError.Text);

            if (IsItemsSelected())
            {
                if (IsMaterialCategorySelected())
                {
                    for (int i = 0; i < cbVL.Items.Count; i++)
                    {
                        if (cbVL.Items[i].Selected)
                        {
                            Vendor += cbVL.Items[i].Text + "','";

                        }
                    }
                    sMatCatList = GetMatCatList();
                    BindException(Convert.ToInt32(ViewState["RefNo"]), Vendor.Substring(0, Vendor.LastIndexOf("','")), sMatCatList);
                }
                else
                {
                    ResetScreenOnError("Please select at least one material category");
                }
            }
            else
            {
                if (IsMaterialCategorySelected())
                {
                    ResetScreenOnError("Please select at least one vendor");
                }
                else
                {
                    ResetScreenOnError("Please select at least one vendor and one material category");
                }
                /*
                grEx.DataSource = null;
                grEx.DataBind();
                btnCan.Visible = false;
                btnSav.Visible = false;
                lblError.Text = "Please select atleast one vendor";
                */
            }
        }

        private bool IsItemsSelected()
        {
            bool Result = false;
            for (int i = 0; i < cbVL.Items.Count; i++)
            {
                if (cbVL.Items[i].Selected)
                {
                    Result = true;
                }
            }
            return Result;
        }
        //Added on 15/Sep/2014 - for MaterialCategory 
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
            foreach (ListItem item in cbPrjGrpItem.Items)
            {
                if (item.Selected)
                {
                    Result = true;
                    break;
                }
            }
            return Result;
        }
        private void BindException(int pRefNo, string Vendor, String sMatCatList)
        {
            DataTable dtExp = new DataTable();

            try
            {
                //dtExp = BLL.Release.GetCompException(pRefNo, Vendor).Tables[0];
                //Commented and added on 15/Sep/2014 - for MaterialCategory 
                dsUOM = new DataSet();
                dsUOM = BLL.Release.GetParmUOMValuesForQty();
                if (chkReviseException.Checked)
                {
                    dtExp = BLL.Release.GetTempOutputClearedComponentException(pRefNo, Vendor, sMatCatList).Tables[0];
                    chkReviseException.Checked = true;
                }
                else if (chkGetExceptionFromEBOM.Checked)
                {

                    string sModelType = ViewState["Model"].ToString();
                    string sProjNumber = lblCN.Text;
                    dtExp = BLL.Release.GetTempOutputWBSBOMComponentException(pRefNo, Vendor, sMatCatList, sProjNumber, sModelType).Tables[0];
                    chkGetExceptionFromEBOM.Checked = true;
                }
                else
                {
                    dtExp = BLL.Release.GetTempOutputComponentException(pRefNo, Vendor, sMatCatList).Tables[0];
                }
                //grEx.Columns.Clear();
                if (dtExp.Rows.Count > 0)
                {
                    grEx.DataSource = dtExp;
                    grEx.DataBind();
                    pnlEx.Visible = true;
                    //btnCan.Visible = true;
                    //btnSav.Visible = true;
                    navPnlBtn.Visible = true;
                    btnProceed.Enabled = false;
                    cbVL.Enabled = false;
                    cbAV.Disabled = true;
                    btnMntExp.Enabled = false;

                    chkGetExceptionFromEBOM.Disabled = true;
                    chkReviseException.Disabled = true;

                    cbMatCat.Enabled = false;
                    cbMatCatAll.Disabled = true;
                }
                else
                {
                    grEx.DataSource = null;
                    grEx.DataBind();
                    //btnCan.Visible = false;
                    //btnSav.Visible = false;
                    navPnlBtn.Visible = false;
                    btnProceed.Enabled = true;
                    cbVL.Enabled = true;
                    cbAV.Disabled = false;
                    btnMntExp.Enabled = true;

                    chkGetExceptionFromEBOM.Disabled = false;
                    chkReviseException.Disabled = false;

                    pnlEx.Visible = false;
                    cbMatCat.Enabled = true;
                    cbMatCatAll.Disabled = false;

                    lblError.Text = "The List is empty...";
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

        protected void grEx_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ////strChkBox = @"<input type=radio name=rdContract onClick=GetContract('" + dr["RefNo"].ToString().Trim() + "','" + dr["Mode"].ToString().Trim() + "','" + dr["Status"].ToString().Trim() + "','" + dr["ModelType"].ToString().Trim() + "','" + Server.HtmlEncode(dr["ContractNo"].ToString().Trim()) + "')>";
                //strChkBox = @"<input id=cbVendor type=checkbox>";
                //e.Row.Cells[0].Text = strChkBox;
                TextBox txtPN = (TextBox)e.Row.FindControl("txtPN");
                TextBox txtDWGNo = (TextBox)e.Row.FindControl("txtDWGNo");
                TextBox txtRev = (TextBox)e.Row.FindControl("txtRev");
                TextBox txtQty = (TextBox)e.Row.FindControl("txtQty");
                CheckBox cbQtyZero = (CheckBox)e.Row.FindControl("cbQtyZero");
                DropDownList ddlUOM = (DropDownList)e.Row.FindControl("ddlUOM");
                String sUOM = Convert.ToString(dr["ParmUOM"]);
                cbQtyZero.Attributes.Add("onClick", "QtyZeroCheck(this,'" + e.Row.ClientID + "'," + e.Row.RowIndex + ",'" + txtQty.ClientID + "');AddRowIndex(" + e.Row.RowIndex + ");");

                txtPN.Text = dr["PartNo"].ToString();
                txtDWGNo.Text = dr["DwgNo"].ToString();
                txtRev.Text = dr["RevNo"].ToString();
                txtQty.Text = dr["Qty"].ToString();

                txtQty.Attributes.Add("onkeypress", "return IsNumeric(this," + e.Row.RowIndex + ");");
                txtQty.Attributes.Add("onpaste", "return IsNumericPaste(this," + e.Row.RowIndex + ");");

                txtRev.Attributes.Add("onkeydown", "onTextKeyDown(this," + e.Row.RowIndex + ");return true;");
                txtRev.Attributes.Add("onpaste", "AddRowIndex(" + e.Row.RowIndex + ");return true;");

                txtPN.Attributes.Add("onkeydown", "onTextKeyDown(this," + e.Row.RowIndex + ");return true;");
                txtPN.Attributes.Add("onpaste", "AddRowIndex(" + e.Row.RowIndex + ");return true;");
                txtDWGNo.Attributes.Add("onkeydown", "onTextKeyDown(this," + e.Row.RowIndex + ");return true;");
                txtDWGNo.Attributes.Add("onpaste", "AddRowIndex(" + e.Row.RowIndex + ");return true;");

                ddlUOM.Attributes.Add("onChange", "AddRowIndex(" + e.Row.RowIndex + ");return true;");



                ddlUOM.DataSource = dsUOM;
                ddlUOM.DataValueField = "ParmUOM";
                ddlUOM.DataTextField = "ParmUOM";
                ddlUOM.DataBind();
                ddlUOM.Items.Insert(0, new ListItem("Select", ""));

                if (!String.IsNullOrEmpty(sUOM))
                {
                    ddlUOM.SelectedValue = sUOM;
                }

                if (chkReviseException.Checked)
                {
                    txtPN.Enabled = true;
                    txtPN.BorderColor = System.Drawing.Color.Red;

                    txtDWGNo.Enabled = true;
                    txtDWGNo.BorderColor = System.Drawing.Color.Red;

                    txtQty.Enabled = true;
                    txtQty.BorderColor = System.Drawing.Color.Red;

                    if (Convert.ToBoolean(dr["IsZeroCost"]))
                    {
                        cbQtyZero.Checked = true;
                        txtQty.Enabled = false;
                    }


                }
                else
                {
                    if (txtPN.Text.Trim() == "#" || txtPN.Text.Trim() == "@"
                            || txtPN.Text.Trim() == "" || txtPN.Text.Trim().StartsWith("("))
                    {
                        txtPN.Enabled = true;
                        txtPN.BorderColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        if (chkGetExceptionFromEBOM.Checked)
                        {
                            if (txtPN.Text.Trim().StartsWith("["))
                            {
                                txtPN.Enabled = true;
                                txtPN.BorderColor = System.Drawing.Color.Cyan;
                            }
                            else
                            {
                                txtPN.Enabled = false;
                                txtPN.Font.Size = FontUnit.Point(8);
                            }
                        }
                        else
                        {
                            txtPN.Enabled = false;
                            txtPN.Font.Size = FontUnit.Point(8);
                        }
                        //txtPN.BorderColor = System.Drawing.Color.Black;
                    }

                    txtRev.BorderColor = System.Drawing.Color.Red;

                    if (txtPN.Enabled)
                    {
                        txtDWGNo.Enabled = true;
                        txtDWGNo.BorderColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        if (txtDWGNo.Text.Trim() == "#" || txtDWGNo.Text.Trim() == "@" || txtDWGNo.Text.Trim() == "")
                        {
                            txtDWGNo.Enabled = true;
                            txtDWGNo.BorderColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                            txtDWGNo.Enabled = false;
                            txtDWGNo.Font.Size = FontUnit.Point(8);
                            //txtDWGNo.BorderColor = System.Drawing.Color.Black;
                        }

                    }

                    if (txtQty.Text.Trim() == "#" || txtQty.Text.Trim() == "@" || txtQty.Text.Trim() == "" || txtQty.Text.Trim() == "0")
                    {
                        txtQty.Enabled = true;
                        txtQty.BorderColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        txtQty.Enabled = false;
                        if (!String.IsNullOrEmpty(sUOM))
                            ddlUOM.Enabled = false;
                        //txtQty.BorderColor = System.Drawing.Color.Black;
                    }
                    if (Convert.ToBoolean(dr["IsZeroCost"]))
                    {
                        cbQtyZero.Checked = true;
                        txtQty.Enabled = false;
                    }
                    if (txtPN.BorderColor == System.Drawing.Color.Cyan)
                    {
                        txtDWGNo.BorderColor = txtPN.BorderColor;
                        txtRev.BorderColor = txtPN.BorderColor;
                        txtQty.Enabled = true;
                        txtQty.BorderColor = txtPN.BorderColor;
                    }
                }
            }
        }

        protected void grEx_PreRender(object sender, EventArgs e)
        {
            // You only need the following 2 lines of code if you are not 
            // using an ObjectDataSource of SqlDataSource
            if (!Page.IsPostBack)
            {
                BindException(Convert.ToInt32(ViewState["RefNo"]), "", "");
            }
            if (grEx.Rows.Count > 0)
            {
                //This replaces <td> with <th> and adds the scope attribute
                grEx.UseAccessibleHeader = true;

                //This will add the <thead> and <tbody> elements
                grEx.HeaderRow.TableSection = TableRowSection.TableHeader;

                //This adds the <tfoot> element. 
                //Remove if you don't have a footer row
                //gvTheGrid.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }
        private void HighlightUOMexception()
        {
            try
            {
                int nRowIdx;
                foreach (var nIdx in alUOMException)
                {
                    if (String.IsNullOrEmpty(Convert.ToString(nIdx)))
                        continue;

                    nRowIdx = Convert.ToInt32(nIdx);
                    if (grEx.Rows[nRowIdx].RowType == DataControlRowType.DataRow)
                    {
                        DropDownList ddlUOM = (DropDownList)grEx.Rows[nRowIdx].FindControl("ddlUOM");

                        if (ddlUOM.Enabled)
                        {
                            if (String.IsNullOrEmpty(ddlUOM.SelectedValue))
                            {
                                ddlUOM.BorderColor = Color.Red;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {


            }
        }

        private void UpdateException()
        {
            Boolean bUOMException = false;
            try
            {
                lblError.Text = "";
                String sCurrencyID = "";
                String sUserID = Convert.ToString(Session["UserId"]);

                alUOMException = new ArrayList();
                ArrayList alEditedRows = new ArrayList();
                alEditedRows.AddRange(hidRowIndex.Value.ToString().Split('|'));
                int nRowIdx;
                foreach (var nIdx in alEditedRows)
                {
                    if (String.IsNullOrEmpty(Convert.ToString(nIdx)))
                        continue;

                    nRowIdx = Convert.ToInt32(nIdx);
                    if (grEx.Rows[nRowIdx].RowType == DataControlRowType.DataRow)
                    {
                        TextBox txtPN = (TextBox)grEx.Rows[nRowIdx].FindControl("txtPN");
                        TextBox txtDWGNo = (TextBox)grEx.Rows[nRowIdx].FindControl("txtDWGNo");
                        TextBox txtRev = (TextBox)grEx.Rows[nRowIdx].FindControl("txtRev");
                        TextBox txtQty = (TextBox)grEx.Rows[nRowIdx].FindControl("txtQty");
                        CheckBox cbQtyZero = (CheckBox)grEx.Rows[nRowIdx].FindControl("cbQtyZero");
                        DropDownList ddlUOM = (DropDownList)grEx.Rows[nRowIdx].FindControl("ddlUOM");

                        Double lTotalCost = 0;
                        if (cbQtyZero.Checked)
                        {
                            txtQty.Text = "0";
                        }
                        else
                        {
                            if (ddlUOM.Enabled)
                            {
                                if (String.IsNullOrEmpty(ddlUOM.SelectedValue))
                                {
                                    bUOMException = true;
                                    alUOMException.Add(nRowIdx);
                                }
                            }
                        }
                        sCurrencyID = grEx.DataKeys[nRowIdx].Values[3].ToString();
                        if (!String.IsNullOrEmpty(Convert.ToString(grEx.DataKeys[nRowIdx].Values[4])))
                        {
                            Double.TryParse(Convert.ToString(grEx.DataKeys[nRowIdx].Values[4]), out lTotalCost);
                        }
                        if (!bUOMException)
                            BLL.Release.UpdateExceptionTempOutput(Convert.ToInt32(grEx.DataKeys[nRowIdx].Values[0].ToString()),
                                                                    Convert.ToInt32(grEx.DataKeys[nRowIdx].Values[1].ToString()),
                                                                    Convert.ToInt32(grEx.DataKeys[nRowIdx].Values[2].ToString()),
                                                                    txtPN.Text, txtDWGNo.Text, txtRev.Text, txtQty.Text,
                                                                    lTotalCost, sCurrencyID, sUserID, cbQtyZero.Checked,
                                                                    Convert.ToString(ddlUOM.SelectedValue));

                    }
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

            if (lblError.Text == "")
            {
                lblError.Text = "Updation successful...";
                lblError.ForeColor = System.Drawing.Color.Green;
            }
            if (bUOMException)
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select valid UOM Value, UOM can not be empty!.');", true);
        }
        private void AutoUpdate()
        {
            try
            {
                String sSelRows = "", sInput = "";
                for (int nRowIdx = 0; nRowIdx < grEx.Rows.Count; nRowIdx++)
                {
                    if (grEx.Rows[nRowIdx].RowType == DataControlRowType.DataRow)
                    {
                        TextBox txtPN = (TextBox)grEx.Rows[nRowIdx].FindControl("txtPN");
                        TextBox txtDWGNo = (TextBox)grEx.Rows[nRowIdx].FindControl("txtDWGNo");
                        TextBox txtRev = (TextBox)grEx.Rows[nRowIdx].FindControl("txtRev");
                        TextBox txtQty = (TextBox)grEx.Rows[nRowIdx].FindControl("txtQty");

                        sInput = Convert.ToString(lblCN.Text.Split('-')[0] + "_ABC");
                        if (txtPN.Enabled)
                            txtPN.Text = sInput;
                        if (txtDWGNo.Enabled)
                            txtDWGNo.Text = sInput;
                        if (txtRev.Enabled)
                            txtRev.Text = "1";
                        if (txtQty.Enabled)
                            txtQty.Text = "10";

                        if (txtPN.Enabled || txtDWGNo.Enabled ||
                               txtRev.Enabled || txtQty.Enabled)
                        {
                            if (String.IsNullOrEmpty(sSelRows))
                                sSelRows = Convert.ToString(nRowIdx);
                            else
                                sSelRows = sSelRows + "|" + Convert.ToString(nRowIdx);
                        }
                    }
                }
                hidRowIndex.Value = sSelRows;
            }
            catch (Exception)
            {

            }

        }
        private void UpdateException_Old()
        {
            try
            {
                lblError.Text = "";
                String sCurrencyID = "";
                String sUserID = Convert.ToString(Session["UserId"]);

                for (int i = 0; i < grEx.Rows.Count; i++)
                {
                    if (grEx.Rows[i].RowType == DataControlRowType.DataRow)
                    {
                        TextBox txtPN = (TextBox)grEx.Rows[i].FindControl("txtPN");
                        TextBox txtDWGNo = (TextBox)grEx.Rows[i].FindControl("txtDWGNo");
                        TextBox txtRev = (TextBox)grEx.Rows[i].FindControl("txtRev");
                        TextBox txtQty = (TextBox)grEx.Rows[i].FindControl("txtQty");
                        CheckBox cbQtyZero = (CheckBox)grEx.Rows[i].FindControl("cbQtyZero");
                        Double lTotalCost = 0;

                        sCurrencyID = grEx.DataKeys[i].Values[3].ToString();
                        if (!String.IsNullOrEmpty(Convert.ToString(grEx.DataKeys[i].Values[4])))
                        {
                            Double.TryParse(Convert.ToString(grEx.DataKeys[i].Values[4]), out lTotalCost);
                            //lTotalCost = Convert.ToDouble(grEx.DataKeys[i].Values[4]);
                        }
                        // if (!String.IsNullOrEmpty(txtTotalCost.Text) && !String.IsNullOrEmpty(sCurrencyID))
                        //if(!String.IsNullOrEmpty(txtTotalCost.Text))

                        BLL.Release.UpdateExceptionInProjectBOM(Convert.ToInt32(grEx.DataKeys[i].Values[0].ToString()),
                                                                    Convert.ToInt32(grEx.DataKeys[i].Values[1].ToString()),
                                                                    Convert.ToInt32(grEx.DataKeys[i].Values[2].ToString()),
                                                                    txtPN.Text, txtDWGNo.Text, txtRev.Text, txtQty.Text,
                                                                    lTotalCost, sCurrencyID, sUserID, cbQtyZero.Checked);

                    }
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

            if (lblError.Text == "")
            {
                lblError.Text = "Updation successful...";
                lblError.ForeColor = System.Drawing.Color.Green;
            }
        }
        private void ShowProjectGroupOnProceed()
        {
            try
            {
                dvVersionNo.Visible = false;
                dvProjectNo.Visible = true;
                lblPrjNo.Text = "";
                dvPrjGrpItemMainHolder.Visible = true;
                lblPrjNo.Text = Convert.ToString(BLL.Release.GetProjectNumber(Convert.ToInt32(ViewState["RefNo"])));

                btnBack.Visible = true;
                btnRelease.Visible = true;
                btnProceed.Visible = false;
                btnMntExp.Visible = false;

                chkGetExceptionFromEBOM.Visible = false;
                chkReviseException.Visible = false;

                lblGetExceptionFromEBOM.Visible = false;
                lblReviseException.Visible = false;

                if (!PopulateProjectGroupNames())
                {
                    btnRelease.Visible = false;
                    lblError.Text = "Project does not exist, can't release the project...";
                    lblError.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    cbVL.Enabled = false;
                    cbAV.Disabled = true;

                    cbMatCat.Enabled = false;
                    cbMatCatAll.Disabled = true;

                    cbPrjGrpItemAll.Checked = true;

                    if (!ClientScript.IsStartupScriptRegistered("SelectAllPrjGrpItem"))
                    {
                        //Page.ClientScript.RegisterStartupScript(this.GetType(),
                        //    "SelectAllPrjGrpItem", "SelectAllPrjGrpItem('cbPrjGrpItem','cbPrjGrpItemAll')", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "SelectAllPrjGrpItem", "SelectAllPrjGrpItem('cbPrjGrpItem','cbPrjGrpItemAll');", true);

                    }
                }

            }
            catch (Exception)
            {


            }
        }
        protected void btnProceed_Click(object sender, EventArgs e)
        {
            try
            {
                ResetScreenOnError("");
                string sVendorList = "All", sMatCatList = "";
                sMatCatList = GetMatCatList();
                DataTable dtExp = BLL.Release.GetTempOutputComponentException(Convert.ToInt32(ViewState["RefNo"]),
                                                                                sVendorList, sMatCatList).Tables[0];


                if (dtExp.Rows.Count > 0)
                    lblError.Text = "Exception exist in current version for selected material category(s), please maintain exception then proceed !";
                else
                {
                    ShowProjectGroupOnProceed();
                }
            }
            catch (Exception ex)
            {


            }
        }
        protected void btnProceed_Click_Old(object sender, EventArgs e)
        {
            string Vendor = "", sMatCatList = "";
            //string[] VendorList;
            ResetScreenOnError("");
            if (IsItemsSelected())
            {
                if (cbMatCatAll.Checked)
                {
                    for (int i = 0; i < cbVL.Items.Count; i++)
                    {
                        if (cbVL.Items[i].Selected)
                        {
                            Vendor += cbVL.Items[i].Text + "','";
                        }
                    }
                    //DataTable dtExp = BLL.Release.GetCompException(Convert.ToInt32(ViewState["RefNo"]), Vendor).Tables[0];
                    //Commented and added on 15/Sep/2014 - for MaterialCategory 
                    sMatCatList = GetMatCatList();
                    DataTable dtExp = BLL.Release.GetMatCatCompException(Convert.ToInt32(ViewState["RefNo"]), Vendor, sMatCatList).Tables[0];
                    ArrayList LiftNos = new ArrayList();
                    // will be remove later
                    if (dtExp.Rows.Count > 0)
                        lblError.Text = "Exception exist in current version for selected vendor(s), please maintain exception then proceed !";
                    else
                    {
                        //LiftNo = lblCN.Text.Split('-')[1];
                        //LiftNos = SplitIntoLiftNo(LiftNo);
                        //VendorList = Vendor.Substring(0, Vendor.LastIndexOf("','")).Split(new string[] { "','" }, StringSplitOptions.None);
                        ShowProjectGroupOnProceed();
                        //GenerateWBSMaster(LiftNos, VendorList);
                    }
                }
                else
                {
                    ResetScreenOnError("Please select all material category to proceed !");
                }
            }
            else
            {
                if (cbMatCatAll.Checked)
                {
                    ResetScreenOnError("Please select at least one material category to proceed !");
                }
                else
                {
                    ResetScreenOnError("Please select at least one vendor and select all material category to proceed !");
                }
                //lblError.Text = "Please select atleast one vendor";
            }

        }
        private ArrayList SplitIntoLiftNo(string LiftNo)
        {
            ArrayList Result = new ArrayList();
            if (!LiftNo.Contains("-") && !LiftNo.Contains(","))
            {
                Result.Add(LiftNo);
            }
            else if (LiftNo.Contains(","))
            {
                for (int i = 0; i < LiftNo.Split(',').Count(); i++)
                {
                    Result.Add(LiftNo.Split(',')[i]);
                }
            }
            else
            {
                string FirstChar = LiftNo.Substring(0, 1);
                int LiftNoFr = Convert.ToInt16(LiftNo.Split('-')[0].Substring(1));
                int LiftNoTo = Convert.ToInt16(LiftNo.Split('-')[1].Substring(1));
                for (int i = LiftNoFr; i <= LiftNoTo; i++)
                {
                    Result.Add(FirstChar + ConverToChar(i));
                }
            }
            return Result;
        }
        private string ConverToChar(int LiftNo)
        {
            string Result = LiftNo.ToString();
            if (Result.Length == 1)
                Result = "00" + LiftNo.ToString();
            else if (Result.Length == 2)
                Result = "0" + LiftNo.ToString();
            else if (Result.Length == 3)
                Result = LiftNo.ToString();
            return Result;
        }
        private void GenerateWBSMaster(ArrayList LftNo, string[] VendorLst)
        {
            string Message = "<font color=green>Release status :</font><br>", Result = "";
            string ProjNo = lblCN.Text.Split('-')[0];
            lblError.Text = "";
            try
            {
                for (int i = 0; i < LftNo.Count; i++)
                {
                    for (int j = 0; j < VendorLst.Count(); j++)
                    {
                        //Result=BLL.Release.CreateWBS(ProjNo, LftNo[i].ToString(), VendorLst[j], lblVNo.Text, Convert.ToInt32(ViewState["RefNo"]), ViewState["Model"].ToString(), Session["UserId"].ToString());
                        if (Result == "Success")
                            Message += "<font color=green>Document No " + ProjNo + "-" + LftNo[i].ToString() + "-" + VendorLst[j] + "-" + lblVNo.Text + " --> generated</font><br>";
                        else
                            Message += "<font color=red>Document No " + ProjNo + "-" + LftNo[i].ToString() + "-" + VendorLst[j] + "-" + lblVNo.Text + " --> already exist</font><br>";
                    }
                }
            }
            catch (SqlException sqEx)
            {
                lblError.Text = "SQL Error :" + sqEx.Message;
            }
            catch (Exception ex)
            {
                lblError.Text = "Error :" + ex.Message;
            }

            if (lblError.Text == "")
            {
                //lblError.Text = "Order is released for procurement";
                //lblError.ForeColor = Color.Green;
                lblError.Text = Message;
            }
        }

        protected void btnSav_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(hidShiftKey.Value) && !String.IsNullOrEmpty(hidAltKey.Value) && !String.IsNullOrEmpty(hidCtrlKey.Value))
                {
                    if (Convert.ToBoolean(hidShiftKey.Value) && Convert.ToBoolean(hidAltKey.Value) && Convert.ToBoolean(hidCtrlKey.Value))
                    {
                        AutoUpdate();
                    }
                }
                hidShiftKey.Value = "false";
                hidCtrlKey.Value = "false";
                hidAltKey.Value = "false";
            }
            catch (Exception)
            {

            }
            UpdateException();
            bIsExceptionClick = false;
            btnMntExp_Click(btnMntExp, EventArgs.Empty);
            HighlightUOMexception();

        }

        protected void btnCan_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            pnlEx.Visible = false;
            //btnCan.Visible = false;
            //btnSav.Visible = false;
            navPnlBtn.Visible = false;
            btnProceed.Enabled = true;
            cbVL.Enabled = true;
            cbAV.Disabled = false;
            btnMntExp.Enabled = true;

            chkReviseException.Disabled = false;
            chkGetExceptionFromEBOM.Disabled = false;


            //Added on 15/Sep/2014 - for MaterialCategory 
            cbMatCat.Enabled = true;
            cbMatCatAll.Disabled = false;

            //Added on 17/Sep/2014 for Release Project
            btnBack.Visible = false;
            btnRelease.Visible = false;
            btnProceed.Visible = true;
            btnMntExp.Visible = true;

            chkReviseException.Visible = true;
            chkGetExceptionFromEBOM.Visible = true;

            lblGetExceptionFromEBOM.Visible = true;
            lblReviseException.Visible = true;


            lblPrjNo.Text = "";

        }

        protected void cbVL_DataBound(object sender, EventArgs e)
        {
            foreach (ListItem item in cbVL.Items)
            {
                item.Attributes.Add("onclick", "ValidateVendorOnSelect();");
            }
        }
        //Added on 15/Sep/2014 - for MaterialCategory 
        protected void cbMatCat_DataBound(object sender, EventArgs e)
        {
            foreach (ListItem item in cbMatCat.Items)
            {
                item.Attributes.Add("onclick", "ValidateCheckBoxListOnSelect('cbMatCat','cbMatCatAll');");
            }
        }

        protected void cbPrjGrpItem_DataBound(object sender, EventArgs e)
        {
            foreach (ListItem item in cbPrjGrpItem.Items)
            {
                item.Attributes.Add("onclick", "ValidateCheckBoxListOnSelect('cbPrjGrpItem','cbPrjGrpItemAll');");
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            btnCan_Click(btnCan, EventArgs.Empty);
            dvPrjGrpItemMainHolder.Visible = false;
        }
        //Added on 17/Sep/2014 - for Release the Projects 
        protected void btnRelease_Click(object sender, EventArgs e)
        {
            try
            {
                PopUpReleaseActivate(); //Added on 11/Mar/2015 for combined activate and release
                //ReleaseProject();

            }
            catch (Exception)
            {

            }
        }
        private String ReleaseOrder(String sDocumentNo, String sUserID)
        {
            String sPrjName = ""
                   , sResult = ""
                   , sRetMessage = ""
                   , sWBSRefNo = "";
            try
            {

                String[] alResult;
                foreach (ListItem lstPrjName in cbPrjGrpItem.Items)
                {
                    if (lstPrjName.Selected)
                    {
                        sPrjName = lstPrjName.Value;
                        sResult = BLL.Release.CreateOrderHeaderAndComponent(sPrjName, sDocumentNo, sUserID);
                        alResult = sResult.Split(new String[] { "_WBSRefNo:" }, StringSplitOptions.None);
                        if (alResult[0].ToUpper() == "SUCCESS")
                        {
                            sWBSRefNo = alResult[1];
                            sResult = BLL.Release.CreateOrderKit(sDocumentNo, sUserID);
                            sResult = BLL.Release.CreateOrderInterface(Convert.ToInt32(sWBSRefNo), sPrjName, sUserID);
                            if (sResult.ToUpper() == "SUCCESS")
                            {
                                sRetMessage += "<font color=green>Project Name: " + sPrjName
                                                    + " & Reference No: " + sWBSRefNo
                                                    + " --> Released</font><br>";
                            }
                            else
                                sRetMessage += "<font color=red>Project Name: " + sPrjName
                                                        + " & Reference No: " + sWBSRefNo
                                                        + " --> Failed To Release Order Kit</font><br>";

                        }
                        else
                        {
                            if (alResult[1].ToUpper() == "NOTEXIST")
                                sRetMessage += "<font color=red>Document No " + sPrjName + " --> does not exist</font><br>";
                            else
                                sRetMessage += "<font color=red>Document No " + sPrjName + " & Reference No: " + alResult[1] + " --> already exist</font><br>";
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                sRetMessage += "<font color=red>Error: " + ex.Message + " --> </font><br>";

            }
            return sRetMessage;
        }
        private void ReleaseProject_Old()
        {
            String Message = "<div id='dvErrorHolder' style='height:300px;width:900px;overflow:auto;background:#E1F0FF' runat='server'>" +
                                    "<font color=green>Release status :</font><br>";

            String[] VendorList;

            String Vendor = "",
                    sDocumentNo = "",
                    Result = "";


            String sVersion = lblVNo.Text,
                    sUserID = Session["UserId"].ToString(),
                    ProjNo = lblCN.Text;

            int nProjRefNo = Convert.ToInt32(ViewState["RefNo"]);


            lblError.Text = "";

            for (int i = 0; i < cbVL.Items.Count; i++)
            {
                if (cbVL.Items[i].Selected)
                {
                    Vendor += cbVL.Items[i].Text + "','";
                }
            }

            VendorList = Vendor.Substring(0, Vendor.LastIndexOf("','")).Split(new String[] { "','" }, StringSplitOptions.None);

            try
            {
                foreach (String sVendorName in VendorList)
                {
                    sDocumentNo = ProjNo + "-" + sVendorName + "-" + sVersion;
                    DataSet dsResult = BLL.Release.CreateWBS(ProjNo, sVendorName, sVersion, nProjRefNo, ViewState["Model"].ToString(), sUserID);

                    if (dsResult != null && dsResult.Tables.Count > 0 && dsResult.Tables[0] != null && dsResult.Tables[0].Rows.Count > 0)
                    {
                        Result = Convert.ToString(dsResult.Tables[0].Rows[0]["Result"]);
                    }

                    if (Result == "Success")
                    {

                        Message += "<font color=green>Document No " + sDocumentNo + " --> generated</font><br>";

                    }
                    else
                    {
                        Message += "<font color=red>Document No " + sDocumentNo + " --> already exist</font><br>";
                    }
                    Message += ReleaseOrder(sDocumentNo, sUserID);
                }

            }
            catch (SqlException sqEx)
            {
                lblError.Text = "SQL Error :" + sqEx.Message;
            }
            catch (Exception ex)
            {
                lblError.Text = "Error :" + ex.Message;
            }

            if (lblError.Text == "")
            {

                lblError.ForeColor = Color.Green;
                lblError.Text = Message + "</Div>";
            }
        }
        private void ReleaseProject()
        {
            String Message = "<div id='dvErrorHolder' style='height:300px;width:900px;overflow:auto;background:#E1F0FF' runat='server'>" +
                                    "<font color=green>Release status :</font><br>";

            String[] sMatCatIDList;

            String sMatCatIDs = "",
                    sDocumentNo = "",
                    Result = "",
                    sPrjName = "";


            String sVersion = lblVNo.Text,
                    sUserID = Session["UserId"].ToString(),
                    ProjNo = lblCN.Text;

            int nProjRefNo = Convert.ToInt32(ViewState["RefNo"]);

            int wbsRefNo = 0;
            lblError.Text = "";

            sMatCatIDs = GetMatCatList();
            sMatCatIDList = sMatCatIDs.Split(new String[] { "','" }, StringSplitOptions.None);
            Dictionary<String, String> dicProjectNames = new Dictionary<string, string>();

            try
            {
                foreach (ListItem lstPrjName in cbPrjGrpItem.Items)
                {
                    if (lstPrjName.Selected)
                    {
                        sPrjName = lstPrjName.Value;
                        if (!dicProjectNames.ContainsKey(sPrjName))
                        {
                            dicProjectNames.Add(sPrjName, sPrjName);
                        }

                    }
                }
                DataTable dtWBSMasterPrjList;
                Boolean bIncompletePrjListForMatCatID = false;
                int nWBSRefNoPrev = 0, nVersionNo = 0;
                string kSetNoForPDFGeneration = null;

                kSetNoForPDFGeneration = BLL.Release.InsertKSetDetailsPDFGeneration(Convert.ToString(Request.QueryString["RefNo"]), Convert.ToString(Session["UserId"]));

                foreach (String sMatCatID in sMatCatIDList)
                {
                    bIncompletePrjListForMatCatID = false;
                    if (cbProjectWarning.Checked)
                    {
                        dtWBSMasterPrjList = BLL.Release.GetWBSMasterActiveProjectList(ProjNo, sMatCatID, nProjRefNo).Tables[0];

                        if (dtWBSMasterPrjList.Rows.Count > 0)
                        {
                            foreach (DataRow oDataRow in dtWBSMasterPrjList.Rows)
                            {
                                sPrjName = oDataRow["ProjectName"].ToString();
                                if (!dicProjectNames.ContainsKey(sPrjName))
                                {
                                    bIncompletePrjListForMatCatID = true;
                                    Message += "<font color=red> Please select Project Name : " + sPrjName + " --> for Material Category :" + sMatCatID + "</font><br>";
                                }
                            }
                        }
                    }
                    if (!bIncompletePrjListForMatCatID)
                    {
                        bool isSuccess = false;

                        sVersion = BLL.Release.GetWBSMasterVersionNumber(ProjNo, sMatCatID, nProjRefNo);

                        nWBSRefNoPrev = Convert.ToInt16(BLL.Release.GetWBSMasterActiveRefNumber(ProjNo, sMatCatID, nProjRefNo));

                        sVersion = Convert.ToString(Convert.ToInt16(sVersion) + 1);
                        nVersionNo = Convert.ToInt16(sVersion);
                        sDocumentNo = ProjNo + "-" + sMatCatID + "-" + sVersion;
                        DataSet dsResult = BLL.Release.CreateWBS(ProjNo, sMatCatID, sVersion, nProjRefNo, ViewState["Model"].ToString(), sUserID);
                        //Result = BLL.Release.CreateWBS(ProjNo, sMatCatID, sVersion, nProjRefNo, ViewState["Model"].ToString(), sUserID);
                        if (dsResult != null && dsResult.Tables.Count > 0 && dsResult.Tables[0] != null && dsResult.Tables[0].Rows.Count > 0)
                        {
                            Result = Convert.ToString(dsResult.Tables[0].Rows[0]["Result"]);
                            wbsRefNo = Convert.ToInt32(dsResult.Tables[0].Rows[0]["WBSRefNo"]);
                        }
                        if (Result == "Success")
                        {

                            Message += "<font color=green>Document No " + sDocumentNo + " --> generated</font><br>";
                            //DataTable dtDocDetails=BLL.Release.get
                            isSuccess = true;

                            
                        }
                        else
                        {
                            Message += "<font color=red>Document No " + sDocumentNo + " --> already exist</font><br>";
                        }
                        Message += ReleaseOrder(sDocumentNo, sUserID);

                        if (nVersionNo > 0)
                        {
                            ProcessOrderHistory(nWBSRefNoPrev, ProjNo, sMatCatID, nProjRefNo, nVersionNo);
                        }

                        if (isSuccess)
                        {
                            DataTable dt = BLL.Release.GetRptKSetWBSList(Convert.ToString(wbsRefNo), "All");
                            DataRow[] dr = dt.Select("IsLooseItem=0 and PartType='Job-Based'");
                            foreach (var item in dr)
                            {
                                BLL.Release.InsertKSetDetailsPDFGenerationDetails(kSetNoForPDFGeneration, Convert.ToString(item["DocumentNo"]), Convert.ToString(item["ModelType"]), sMatCatID, Convert.ToString(item["VersionNo"]), Convert.ToString(Session["UserId"]), Convert.ToInt32(Convert.ToString(Request.QueryString["RefNo"])), wbsRefNo, Convert.ToString(item["PartNo"]));

                            }
                        }

                    }
                }

                string chStatus = BLL.Release.UpdateKSetDetailsPDFGeneration(kSetNoForPDFGeneration, "Job Queue", Convert.ToString(Session["UserId"]),"ALL",0);
                
                // Send Message Queue

                if (chStatus == "Success")
                {
                    System.Messaging.MessageQueue mqPDFGeneration = new System.Messaging.MessageQueue();
                    //mqPDFGeneration.Path = "FormatName:DIRECT=OS:" + ConfigurationSettings.AppSettings["QueuePath"] + "\\private$\\nosksetpdfqueue";
                    mqPDFGeneration.Path = ConfigurationSettings.AppSettings["QueuePathKSetPDF"];
                    mqPDFGeneration.Send(kSetNoForPDFGeneration + "^" + ConfigurationSettings.AppSettings["SQLConn"] + "^" + ConfigurationSettings.AppSettings["ReportServer"] + "^" + ConfigurationSettings.AppSettings["ReportDB"] + "^" + ConfigurationSettings.AppSettings["ReportUserID"] + "^" + ConfigurationSettings.AppSettings["ReportPassword"] + "^" + ConfigurationSettings.AppSettings["KSetPDFGenerationPath"], "NOSKSetPDFQ");
                }

            }
            catch (SqlException sqEx)
            {
                lblError.Text = "SQL Error :" + sqEx.Message;
            }
            catch (Exception ex)
            {
                lblError.Text = "Error :" + ex.Message;
            }

            if (lblError.Text == "")
            {

                lblError.ForeColor = Color.Green;
                lblError.Text = Message + "</Div>";
            }
        }
        private void ProcessOrderHistory(int nWBSRefNoPrev, String sProjNo, String sMatCatID, int nProjRefNo, int nVersionNo)
        {
            try
            {
                int nWBSRefNoCurrent;
                DataTable dtOrdKitParentPartsPrev, dtOrdKitParentPartsCurrent;

                DataTable dtOrdComponentPrev, dtOrdComponentCurrent;

                Dictionary<String, String> dicOrdKitParentPartsPrev = new Dictionary<string, string>();
                Dictionary<String, String> dicOrdKitParentPartsCurrent = new Dictionary<string, string>();

                Dictionary<String, DataRow> dicOrdComponentPrev = new Dictionary<string, DataRow>();
                Dictionary<String, DataRow> dicOrdComponentCurrent = new Dictionary<string, DataRow>();

                Dictionary<String, String> dicParentPartNumbers = new Dictionary<string, string>();

                String sParentPartNo = "", sResult = "", sDisplaySeq = "";
                String sUserID = Session["UserId"].ToString();

                String sComponentNoPrev = "", sComponentNoCurrent = "";
                Double dblQuantityPrev = 0, dblQuantityCurrent = 0;
                String sDrawingNoPrev = "", sDrawingNoCurrent = "";
                String sRevNoPrev = "", sRevNoCurrent = "";

                String sDesc = "";
                String sTmpDesc = "";
                String[] alDesc;
                String[] alTmpDesc;

                nWBSRefNoCurrent = Convert.ToInt16(BLL.Release.GetWBSMasterActiveRefNumber(sProjNo, sMatCatID, nProjRefNo));
                //Previous OrderKit Parent Part Numbers
                dtOrdKitParentPartsPrev = BLL.Release.GetOrderKitParentPartNos(nWBSRefNoPrev).Tables[0];
                //Current OrderKit Parent Part Numbers
                dtOrdKitParentPartsCurrent = BLL.Release.GetOrderKitParentPartNos(nWBSRefNoCurrent).Tables[0];

                foreach (DataRow oDataRow in dtOrdKitParentPartsPrev.Rows)
                {
                    sParentPartNo = oDataRow["ParentPartNo"].ToString();
                    if (!dicOrdKitParentPartsPrev.ContainsKey(sParentPartNo))
                    {
                        //Add Previous OrderKit Parent Part Numbers to the collection
                        dicOrdKitParentPartsPrev.Add(sParentPartNo, sParentPartNo);

                    }
                }

                foreach (DataRow oDataRow in dtOrdKitParentPartsCurrent.Rows)
                {
                    sParentPartNo = oDataRow["ParentPartNo"].ToString();
                    if (!dicOrdKitParentPartsCurrent.ContainsKey(sParentPartNo))
                    {
                        //Add Current OrderKit Parent Part Numbers to the collection
                        dicOrdKitParentPartsCurrent.Add(sParentPartNo, sParentPartNo);

                    }
                    if (dicOrdKitParentPartsPrev.ContainsKey(sParentPartNo))
                    {
                        //Part Number OLD Check anything modified or not
                        //Previous OrderComponent details for Parent Part Number
                        dtOrdComponentPrev = BLL.Release.GetOrdComponentDetailsForParentPartNo(nWBSRefNoPrev, sParentPartNo).Tables[0];
                        //Current OrderComponent details for Parent Part Number
                        dtOrdComponentCurrent = BLL.Release.GetOrdComponentDetailsForParentPartNo(nWBSRefNoCurrent, sParentPartNo).Tables[0];

                        dicOrdComponentPrev.Clear();
                        dicOrdComponentCurrent.Clear();

                        foreach (DataRow oCompoDataRow in dtOrdComponentPrev.Rows)
                        {
                            sDisplaySeq = oCompoDataRow["DisplaySeq"].ToString();
                            if (!dicOrdComponentPrev.ContainsKey(sDisplaySeq))
                            {
                                //Add Previous OrderKit Parent Part Componets to the collection
                                dicOrdComponentPrev.Add(sDisplaySeq, oCompoDataRow);

                            }
                        }
                        foreach (DataRow oCompoDataRow in dtOrdComponentCurrent.Rows)
                        {
                            sDisplaySeq = oCompoDataRow["DisplaySeq"].ToString();
                            if (!dicOrdComponentCurrent.ContainsKey(sDisplaySeq))
                            {
                                //Add Current OrderKit Parent Part Componets to the collection
                                dicOrdComponentCurrent.Add(sDisplaySeq, oCompoDataRow);

                            }
                        }
                        foreach (DataRow oCompoDataRow in dtOrdComponentCurrent.Rows)
                        {
                            sDisplaySeq = oCompoDataRow["DisplaySeq"].ToString();

                            if (dicOrdComponentPrev.ContainsKey(sDisplaySeq))
                            {
                                //Component OLD 
                                sComponentNoPrev = Convert.ToString(dicOrdComponentPrev[sDisplaySeq]["ComponentNo"]);
                                sComponentNoCurrent = Convert.ToString(dicOrdComponentCurrent[sDisplaySeq]["ComponentNo"]);

                                dblQuantityPrev = Convert.ToDouble(dicOrdComponentPrev[sDisplaySeq]["Quantity"]);
                                dblQuantityCurrent = Convert.ToDouble(dicOrdComponentCurrent[sDisplaySeq]["Quantity"]);

                                sDrawingNoPrev = Convert.ToString(dicOrdComponentPrev[sDisplaySeq]["DrawingNo"]);
                                sDrawingNoCurrent = Convert.ToString(dicOrdComponentCurrent[sDisplaySeq]["DrawingNo"]);


                                sRevNoPrev = Convert.ToString(dicOrdComponentPrev[sDisplaySeq]["RevNo"]);
                                sRevNoCurrent = Convert.ToString(dicOrdComponentCurrent[sDisplaySeq]["RevNo"]);

                                if (
                                        (sComponentNoPrev != sComponentNoCurrent)
                                        || (dblQuantityPrev != dblQuantityCurrent)
                                        || (sDrawingNoPrev != sDrawingNoCurrent
                                        || (sRevNoPrev != sRevNoCurrent))
                                    )
                                {
                                    sResult = BLL.Release.UpdateWBSMasterKSetHistory(nWBSRefNoCurrent, sParentPartNo, nVersionNo, sUserID, "M", sDisplaySeq, "", "M");
                                    if (!dicParentPartNumbers.ContainsKey(sParentPartNo))
                                    {
                                        dicParentPartNumbers.Add(sParentPartNo, "M| Modified =" + sDisplaySeq);
                                    }
                                    else
                                    {

                                        sDesc = dicParentPartNumbers[sParentPartNo];
                                        if (sDesc.StartsWith("M|"))
                                        {
                                            alDesc = sDesc.Split(new String[] { "|" }, StringSplitOptions.None);
                                            //sDesc = alDesc[0]+"|"+alDesc[1]+"; "+ sDisplaySeq;
                                            if (alDesc[1].Contains(" Modified ="))
                                            {
                                                sTmpDesc = alDesc[1];
                                                alTmpDesc = sTmpDesc.Split(new String[] { " Modified =" }, StringSplitOptions.None);
                                                sTmpDesc = alTmpDesc[0] + " Modified =" + sDisplaySeq + ";" + alTmpDesc[1];
                                                sDesc = alDesc[0] + "|" + sTmpDesc;
                                            }
                                            else
                                            {
                                                sDesc = alDesc[0] + "|" + alDesc[1] + "; Modified =" + sDisplaySeq;

                                            }
                                            dicParentPartNumbers[sParentPartNo] = sDesc;
                                        }
                                    }
                                }

                            }
                            else
                            {
                                //New Component  
                                sResult = BLL.Release.UpdateWBSMasterKSetHistory(nWBSRefNoCurrent, sParentPartNo, nVersionNo, sUserID, "A", sDisplaySeq, "", "A");
                                if (!dicParentPartNumbers.ContainsKey(sParentPartNo))
                                {
                                    dicParentPartNumbers.Add(sParentPartNo, "M| Added = " + sDisplaySeq);
                                }
                                else
                                {

                                    sDesc = dicParentPartNumbers[sParentPartNo];
                                    if (sDesc.StartsWith("M|"))
                                    {
                                        alDesc = sDesc.Split(new String[] { "|" }, StringSplitOptions.None);
                                        if (alDesc[1].Contains(" Added ="))
                                        {
                                            sTmpDesc = alDesc[1];
                                            alTmpDesc = sTmpDesc.Split(new String[] { " Added =" }, StringSplitOptions.None);
                                            sTmpDesc = alTmpDesc[0] + " Added =" + sDisplaySeq + ";" + alTmpDesc[1];
                                            sDesc = alDesc[0] + "|" + sTmpDesc;
                                            //sDesc = alDesc[0] + "|" + alDesc[1] + "; " + sDisplaySeq;
                                        }
                                        else
                                        {
                                            sDesc = alDesc[0] + "|" + alDesc[1] + "; Added =" + sDisplaySeq;

                                        }
                                        dicParentPartNumbers[sParentPartNo] = sDesc;
                                    }
                                }
                            }
                        }
                        foreach (String sKey in dicOrdComponentPrev.Keys)
                        {
                            if (!dicOrdComponentCurrent.ContainsKey(sKey))
                            {
                                //DELETED Component NUMBER
                                sResult = BLL.Release.UpdateWBSMasterKSetHistory(nWBSRefNoCurrent, sParentPartNo, nVersionNo, sUserID, "D", sKey, "", "D");
                                if (!dicParentPartNumbers.ContainsKey(sParentPartNo))
                                {
                                    dicParentPartNumbers.Add(sParentPartNo, "M| Deleted =" + sKey);
                                }
                                else
                                {

                                    sDesc = dicParentPartNumbers[sParentPartNo];
                                    if (sDesc.StartsWith("M|"))
                                    {
                                        alDesc = sDesc.Split(new String[] { "|" }, StringSplitOptions.None);
                                        if (alDesc[1].Contains(" Deleted ="))
                                        {
                                            sTmpDesc = alDesc[1];
                                            alTmpDesc = sTmpDesc.Split(new String[] { " Deleted =" }, StringSplitOptions.None);
                                            sTmpDesc = alTmpDesc[0] + " Deleted =" + sKey + ";" + alTmpDesc[1];
                                            sDesc = alDesc[0] + "|" + sTmpDesc;
                                            //sDesc = alDesc[0] + "|" + alDesc[1] + "; " + sKey;
                                        }
                                        else
                                        {
                                            sDesc = alDesc[0] + "|" + alDesc[1] + "; Deleted =" + sKey;

                                        }
                                        dicParentPartNumbers[sParentPartNo] = sDesc;
                                    }
                                }
                            }

                        }



                    }
                    else
                    {
                        //New Part 
                        sResult = BLL.Release.UpdateWBSMasterKSetHistory(nWBSRefNoCurrent, sParentPartNo, nVersionNo, sUserID, "A", "00", "Added new K-Set", "A");

                    }
                }
                foreach (String sKey in dicOrdKitParentPartsPrev.Keys)
                {
                    if (!dicOrdKitParentPartsCurrent.ContainsKey(sKey))
                    {
                        //DELETED PART NUMBER
                        sResult = BLL.Release.UpdateWBSMasterKSetHistory(nWBSRefNoCurrent, sKey, nVersionNo, sUserID, "D", "00", "", "D");
                        if (!dicParentPartNumbers.ContainsKey(sParentPartNo))
                        {
                            dicParentPartNumbers.Add(sParentPartNo, "D|K- Set Deleted");
                        }
                    }

                }

                foreach (String sKey in dicParentPartNumbers.Keys)
                {

                    sDesc = dicParentPartNumbers[sKey];
                    alDesc = sDesc.Split(new String[] { "|" }, StringSplitOptions.None);

                    sResult = BLL.Release.UpdateWBSMasterKSetHistory(nWBSRefNoCurrent, sKey, nVersionNo, sUserID, "P", "00", alDesc[1], alDesc[0]);
                }




            }
            catch (Exception ex)
            {


            }
        }
        //Added on 17/Sep/2014 - for Project Name List 
        private Boolean PopulateProjectGroupNames()
        {
            Boolean bRetVal = false;
            try
            {
                DataTable dt = new DataTable();
                dt = BLL.Release.GetProjectNames(lblPrjNo.Text).Tables[0];
                cbPrjGrpItem.DataSource = dt;
                cbPrjGrpItem.DataValueField = "ProjectName";
                cbPrjGrpItem.DataTextField = "ProjectName";
                cbPrjGrpItem.DataBind();
                if (dt.Rows.Count > 0)
                {
                    bRetVal = true;
                }

            }
            catch (Exception)
            {

            }
            return bRetVal;
        }
        ////protected void cbQtyZero_CheckedChanged(object sender, EventArgs e)
        ////{
        ////    try
        ////    {

        ////        CheckBox ckBox = (CheckBox)sender;
        ////        TableCell cell = ckBox.Parent as TableCell;
        ////        //DataGridItem item = cell.Parent as DataGridItem;  //For MS Datagrid            
        ////        GridViewRow item = cell.Parent as GridViewRow ; //For MS GridView
        ////        //C1.Web.C1WebGrid.C1GridItem item = cell.Parent as C1.Web.C1WebGrid.C1GridItem; // For C1WebGrid

        ////        TextBox txtQty = (TextBox)item.FindControl("txtQty");
        ////        txtQty.Enabled = ckBox.Checked;                
        ////    }
        ////    catch (Exception ex)
        ////    {


        ////    }

        ////}
        private void PopUpReleaseActivate()
        {
            try
            {
                AjaxControlToolkit.ModalPopupExtender modalPop = ((AjaxControlToolkit.ModalPopupExtender)
                                                    (this.FindControl("mpeAvtivate")));
                modalPop.Show();
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
        protected void lnkBtnActivate_Click(object sender, EventArgs e)
        {
            try
            {
                String sApprovedBy, sCheckedBy, sIssuedBy, sResult = "";
                String[] alResult;
                sApprovedBy = txtApprovedBy.Text.Trim();
                sCheckedBy = txtCheckedBy.Text.Trim();
                sIssuedBy = Session["UserName"].ToString();
                int nProjRefNo = Convert.ToInt32(ViewState["RefNo"]);
                sResult = BLL.ViewOutPut.Activate(nProjRefNo, txtApprovedBy.Text.Trim()
                                                , txtCheckedBy.Text.Trim(), sIssuedBy);
                alResult = sResult.Split(new String[] { "_VersionNo:" }, StringSplitOptions.None);
                if (alResult[0].ToUpper() == "SUCCESS")
                {
                    ReleaseProject();
                    //if (!ClientScript.IsStartupScriptRegistered("ProceedAfterActivate"))
                    //{
                    //    Page.ClientScript.RegisterStartupScript(this.GetType(),
                    //        "ProceedAfterActivate", "OnActivateComplete('" + HidRefNo.Value + "','" + alResult[1] + "');", true);
                    //}
                }
                else
                {
                    lblError.Text = "Activate failed...";
                    lblError.ForeColor = Color.Red;
                }


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

    }
}
