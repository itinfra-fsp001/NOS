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
    public partial class ZeroCost : System.Web.UI.Page
    {
        private Boolean bIsExceptionClick = true;
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
                cbAV.Checked = true;
                cbMatCatAll.Checked = true;
                
                if (!ClientScript.IsStartupScriptRegistered("SelectAllCheckBoxLists"))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(),
                        "SelectAllCheckBoxLists", "SelectAllVendor();"+  
                        "SelectAllMatCat('cbMatCat','cbMatCatAll');", true);
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

            dvVersionNo.Visible = true;
            dvProjectNo.Visible = true;
            lblPrjNo.Text = Convert.ToString(BLL.Release.GetProjectNumber(Convert.ToInt32(ViewState["RefNo"])));
            
        }
      
        private void PopulateVendor()
        { 
            DataTable dt=new DataTable();
            dt = BLL.Release.GetContractVendor(Convert.ToInt32(ViewState["RefNo"])).Tables[0];
            cbVL.DataSource = dt;
            cbVL.DataValueField = "VendorNo";
            cbVL.DataTextField = "VendorName";
            cbVL.DataBind();            
        }
        private void PopulateMaterialCategory()
        { 
            DataTable dt=new DataTable();
            dt = BLL.Release.GetMaterialCategory("").Tables[0];
            cbMatCat.DataSource = dt;
            cbMatCat.DataValueField = "MaterialCategoryID";
            cbMatCat.DataTextField = "MaterialCategoryName";
            cbMatCat.DataBind();            
        }
        
        private void ResetScreenOnError(String sErrMsg)
        {
            grEx.DataSource = null;
            grEx.DataBind();
            //btnCan.Visible = false;
            //btnSav.Visible = false;
            navPnlBtn.Visible = false;
            lblError.Text = sErrMsg;
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
        protected void btnMntExp_Click(object sender, EventArgs e)
        {
            isChangedComBtnClick = false;
            string Vendor="",sMatCatList="";
            if(bIsExceptionClick)
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
                    sMatCatList=GetMatCatList();
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
                    ResetScreenOnError("Please select at least one material category");
                }
                else
                {
                    ResetScreenOnError("Please select at least one vendor and one material category");
                }
                
            }
        }
       
        private bool IsItemsSelected()
        {
            bool Result=false;
            for (int i = 0; i < cbVL.Items.Count; i++)
            {
                if (cbVL.Items[i].Selected)
                {
                    Result = true;
                }
            }
            return Result;
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
        
        private void BindException(int pRefNo, string Vendor, String sMatCatList)
        {
            lblHeading.Visible = true;
            lblHeading.Text = "Maintain Zero Cost Exception";
        
            DataTable dtExp = new DataTable();
            
            try
            {
                dtExp = BLL.Release.GetZeroCostException(pRefNo, Vendor, sMatCatList).Tables[0];
                
                if (dtExp.Rows.Count > 0)
                {
                    grEx.DataSource = dtExp;
                    grEx.DataBind();
                    pnlEx.Visible = true;
                    //btnCan.Visible = true;
                    //btnSav.Visible = true;
                    navPnlBtn.Visible = true;
                                        
                    cbVL.Enabled = false;
                    cbAV.Disabled = true;
                   btnMntExp.Enabled = false;
                   btnChangeComp.Enabled = false;

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
                   
                    cbVL.Enabled = true;
                    cbAV.Disabled = false;
                    btnMntExp.Enabled = true;
                    btnChangeComp.Enabled = true;
                    pnlEx.Visible = false;
                    cbMatCat.Enabled = true;
                    cbMatCatAll.Disabled = false;                    
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

        private void BindChangeComponent(int pRefNo, string Vendor, String sMatCatList)
        {
            DataTable dtExp = new DataTable();

            try
            {
                dtExp = BLL.Release.GetChangeComponent(pRefNo, Vendor, sMatCatList).Tables[0];

                if (dtExp.Rows.Count > 0)
                {
                    grEx.DataSource = dtExp;
                    grEx.DataBind();
                    pnlEx.Visible = true;
                    //btnCan.Visible = true;
                    //btnSav.Visible = true;
                    navPnlBtn.Visible = true;

                    cbVL.Enabled = false;
                    cbAV.Disabled = true;
                    btnMntExp.Enabled = false;
                    btnChangeComp.Enabled = false;

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

                    cbVL.Enabled = true;
                    cbAV.Disabled = false;
                    btnMntExp.Enabled = true;
                    btnChangeComp.Enabled = true;
                    pnlEx.Visible = false;
                    cbMatCat.Enabled = true;
                    cbMatCatAll.Disabled = false;
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
        bool isChangedComBtnClick = false;
        protected void grEx_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                DataRowView drView = (DataRowView)e.Row.DataItem;
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtDirectMtl = (TextBox)e.Row.FindControl("txtDirectMtl");
                    TextBox txtLabor = (TextBox)e.Row.FindControl("txtLabor");
                    TextBox txtSubcon = (TextBox)e.Row.FindControl("txtSubcon");
                    TextBox txtSubconBatam = (TextBox)e.Row.FindControl("txtSubconBatam");
                    TextBox txtDMFJ = (TextBox)e.Row.FindControl("txtDMFJ");
                    TextBox txtQty = (TextBox)e.Row.FindControl("txtQty");
                    TextBox txtComponentNo = (TextBox)e.Row.FindControl("txtComponentNo");
                    TextBox txtDrawingNo = (TextBox)e.Row.FindControl("txtDrawingNo");
                    TextBox txtRevNo = (TextBox)e.Row.FindControl("txtRevNo");

                    String sVendorType = Convert.ToString(drView["VendorType"]);
                   
                  

                    double lDirectMtl=0, lLabor=0, lSubcon=0, lSubconBatam=0, lDMFJ=0,lQty=0;
                    Color colTextDefault = txtDirectMtl.BorderColor ;
                    txtDirectMtl.Enabled = true;
                    txtDirectMtl.BorderColor = Color.Red; 
                    txtLabor.Enabled = true;
                    txtLabor.BorderColor = Color.Red; 
                    txtSubcon.Enabled = true;
                    txtSubcon.BorderColor = Color.Red; 
                    txtSubconBatam.Enabled = true;
                    txtSubconBatam.BorderColor = Color.Red; 
                    txtDMFJ.Enabled = true;
                    txtDMFJ.BorderColor = Color.Red;
                    txtQty.Enabled = true;
                    txtQty.BorderColor = Color.Red;

                    txtDirectMtl.Text = "0.0";
                    txtLabor.Text = "0.0";
                    txtSubcon.Text = "0.0";
                    txtSubconBatam.Text = "0.0";
                    txtDMFJ.Text = "0.0";
                    txtQty.Text = "0";
                    

                    txtDirectMtl.Attributes.Add("onkeypress", "return IsDecimal(this," + e.Row .RowIndex + ");");
                    txtLabor.Attributes.Add("onkeypress", "return IsDecimal(this," + e.Row.RowIndex + ");");
                    txtSubcon.Attributes.Add("onkeypress", "return IsDecimal(this," + e.Row.RowIndex + ");");
                    txtSubconBatam.Attributes.Add("onkeypress", "return IsDecimal(this," + e.Row.RowIndex + ");");
                    txtDMFJ.Attributes.Add("onkeypress", "return IsDecimal(this," + e.Row.RowIndex + ");");
                  

                    txtDirectMtl.Attributes.Add("onpaste", "return IsDecimalcPaste(this," + e.Row.RowIndex + ");");
                    txtLabor.Attributes.Add("onpaste", "return IsDecimalcPaste(this," + e.Row.RowIndex + ");");
                    txtSubcon.Attributes.Add("onpaste", "return IsDecimalcPaste(this," + e.Row.RowIndex + ");");
                    txtSubconBatam.Attributes.Add("onpaste", "return IsDecimalcPaste(this," + e.Row.RowIndex + ");");
                    txtDMFJ.Attributes.Add("onpaste", "return IsDecimalcPaste(this," + e.Row.RowIndex + ");");

                    txtQty.Attributes.Add("onkeypress", "return IsNumeric(this," + e.Row.RowIndex + ");");
                    txtQty.Attributes.Add("onpaste", "return IsNumericPaste(this," + e.Row.RowIndex + ");");

                    txtComponentNo.Attributes.Add("onkeypress", "return IsValueChange(this," + e.Row.RowIndex + ");");
                    txtComponentNo.Attributes.Add("onpaste", "return IsValueChangePaste(this," + e.Row.RowIndex + ");");

                    txtDrawingNo.Attributes.Add("onkeypress", "return IsValueChange(this," + e.Row.RowIndex + ");");
                    txtDrawingNo.Attributes.Add("onpaste", "return IsValueChangePaste(this," + e.Row.RowIndex + ");");

                    txtRevNo.Attributes.Add("onkeypress", "return IsValueChange(this," + e.Row.RowIndex + ");");
                    txtRevNo.Attributes.Add("onpaste", "return IsValueChangePaste(this," + e.Row.RowIndex + ");");

                    if (!String.IsNullOrEmpty(Convert.ToString(drView["Qty"])))
                    {
                        Double.TryParse(Convert.ToString(drView["Qty"]), out lQty);
                        if (lQty > 0)
                        {
                            txtQty.Enabled = false;
                            txtQty.BorderColor = colTextDefault;
                            txtQty.Text = Convert.ToString(lQty);
                        }
                    }

                    if (!String.IsNullOrEmpty(Convert.ToString(drView["DirectMtl"])))
                    {                        
                        Double.TryParse(Convert.ToString(drView["DirectMtl"]), out lDirectMtl);
                        if (lDirectMtl > 0)
                        {
                            txtDirectMtl.Enabled = false;
                            txtDirectMtl.BorderColor = colTextDefault;
                            txtDirectMtl.Text = Convert.ToString(lDirectMtl);
                        }
                    }
                    if (!String.IsNullOrEmpty(Convert.ToString(drView["Labor"])))
                    {
                        Double.TryParse(Convert.ToString(drView["Labor"]), out lLabor);
                        if (lLabor > 0)
                        {
                            txtLabor.Enabled = false;
                            txtLabor.BorderColor = colTextDefault;
                            txtLabor.Text = Convert.ToString(lLabor);
                        }
                    }
                    if (!String.IsNullOrEmpty(Convert.ToString(drView["Subcon"])))
                    {
                        Double.TryParse(Convert.ToString(drView["Subcon"]), out lSubcon);
                        if (lSubcon > 0)
                        {
                            txtSubcon.Enabled = false;
                            txtSubcon.BorderColor = colTextDefault;
                            txtSubcon.Text = Convert.ToString(lSubcon);
                        }
                    }
                    if (!String.IsNullOrEmpty(Convert.ToString(drView["SubconBatam"])))
                    {
                        double.TryParse(Convert.ToString(drView["SubconBatam"]), out lSubconBatam);
                        if (lSubconBatam > 0)
                        {
                            txtSubconBatam.Enabled = false;
                            txtSubconBatam.BorderColor = colTextDefault;
                            txtSubconBatam.Text = Convert.ToString(lSubconBatam);
                        }
                    }
                    if (!String.IsNullOrEmpty(Convert.ToString(drView["DMFJ"])))
                    {
                        double.TryParse(Convert.ToString(drView["DMFJ"]), out lDMFJ);
                        if (lDMFJ > 0)
                        {
                            txtDMFJ.Enabled = false;
                            txtDMFJ.BorderColor = colTextDefault;
                            txtDMFJ.Text = Convert.ToString(lDMFJ);
                        }
                    }
                    if (sVendorType.ToUpper() != "I")
                    {                        
                        txtLabor.Enabled = false;
                        txtLabor.BorderColor = colTextDefault;
                        txtSubcon.Enabled = false;
                        txtSubcon.BorderColor = colTextDefault;
                        txtSubconBatam.Enabled = false;
                        txtSubconBatam.BorderColor = colTextDefault;
                        txtDMFJ.Enabled = false;
                        txtDMFJ.BorderColor = colTextDefault;
                    }
                    txtComponentNo.Text = Convert.ToString(drView["ComponentNo"]);
                    txtDrawingNo.Text = Convert.ToString(drView["DrawingNo"]);
                    txtRevNo.Text = Convert.ToString(drView["RevNo"]);

                    if (isChangedComBtnClick)
                    {
                        txtDirectMtl.Enabled = false;
                        txtLabor.Enabled = false;
                        txtSubcon.Enabled = false;
                        txtSubconBatam.Enabled = false;
                        txtDMFJ.Enabled = false;
                        txtQty.Enabled = false;
                        txtDirectMtl.BorderColor = colTextDefault;
                        txtLabor.BorderColor = colTextDefault;
                        txtSubcon.BorderColor = colTextDefault;
                        txtSubconBatam.BorderColor = colTextDefault;
                        txtDMFJ.BorderColor = colTextDefault;
                        txtQty.BorderColor = colTextDefault;
                        txtComponentNo.Enabled = true;
                        txtDrawingNo.Enabled = true;
                        txtRevNo.Enabled = true;
                    }
                    else
                    {
                        txtComponentNo.Enabled = false;
                        txtDrawingNo.Enabled = false;
                        txtRevNo.Enabled = false;
                    }
                }
                
            }
            catch (Exception)
            {
                
               
            }

        }
        protected void grEx_PreRender(object sender, EventArgs e)
        {
            // You only need the following 2 lines of code if you are not 
            // using an ObjectDataSource of SqlDataSource
            if (!Page.IsPostBack)
            {
                BindException(Convert.ToInt32(ViewState["RefNo"]), "","");                
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

        private bool UpdateException()
        {
            bool isChangebtn = false;
               
            try
            {
                lblError.Text="";
                String sCurrencyID = "", sVendorType = "";
                String sUserID =Convert.ToString( Session["UserId"]);
                double lDirectMtl=0, lLabor=0, lSubcon=0, lSubconBatam=0, lDMFJ=0;
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


                        TextBox txtDirectMtl = (TextBox)grEx.Rows[nRowIdx].FindControl("txtDirectMtl");
                        TextBox txtLabor = (TextBox)grEx.Rows[nRowIdx].FindControl("txtLabor");
                        TextBox txtSubcon = (TextBox)grEx.Rows[nRowIdx].FindControl("txtSubcon");
                        TextBox txtSubconBatam = (TextBox)grEx.Rows[nRowIdx].FindControl("txtSubconBatam");
                        TextBox txtDMFJ = (TextBox)grEx.Rows[nRowIdx].FindControl("txtDMFJ");
                        TextBox txtQty = (TextBox)grEx.Rows[nRowIdx].FindControl("txtQty");
                        TextBox txtComponentNo = (TextBox)grEx.Rows[nRowIdx].FindControl("txtComponentNo");
                        TextBox txtDrawingNo = (TextBox)grEx.Rows[nRowIdx].FindControl("txtDrawingNo");
                        TextBox txtRevNo = (TextBox)grEx.Rows[nRowIdx].FindControl("txtRevNo");

                        if (txtDirectMtl.Enabled || txtLabor.Enabled ||
                               txtSubcon.Enabled || txtSubconBatam.Enabled || txtDMFJ.Enabled || txtQty.Enabled)
                        {
                            if (!String.IsNullOrEmpty(txtDirectMtl.Text))
                            {
                                Double.TryParse(Convert.ToString(txtDirectMtl.Text), out lDirectMtl);
                            }
                            if (!String.IsNullOrEmpty(txtLabor.Text))
                            {
                                Double.TryParse(Convert.ToString(txtLabor.Text), out lLabor);
                            }
                            if (!String.IsNullOrEmpty(txtSubcon.Text))
                            {
                                Double.TryParse(Convert.ToString(txtSubcon.Text), out lSubcon);
                            }
                            if (!String.IsNullOrEmpty(txtSubconBatam.Text))
                            {
                                Double.TryParse(Convert.ToString(txtSubconBatam.Text), out lSubconBatam);
                            }
                            if (!String.IsNullOrEmpty(txtDMFJ.Text))
                            {
                                Double.TryParse(Convert.ToString(txtDMFJ.Text), out lDMFJ);
                            }
                            sCurrencyID = grEx.DataKeys[nRowIdx].Values[3].ToString();
                            sVendorType = grEx.DataKeys[nRowIdx].Values[4].ToString();
                            
                            BLL.Release.UpdateZeroCostException(Convert.ToInt32(grEx.DataKeys[nRowIdx].Values[0].ToString()),
                                                                 Convert.ToInt32(grEx.DataKeys[nRowIdx].Values[1].ToString()),
                                                                 Convert.ToInt32(grEx.DataKeys[nRowIdx].Values[2].ToString()),
                                                                 txtQty.Text, sCurrencyID,
                                                                 lDirectMtl, lLabor, lSubcon, lSubconBatam, lDMFJ, sVendorType, sUserID, Convert.ToInt32(grEx.DataKeys[nRowIdx].Values[5].ToString()));
                        }
                        else if (txtComponentNo.Enabled)
                        {
                            isChangebtn = true;
                            if (!String.IsNullOrEmpty(txtComponentNo.Text))

                            {
                                string sVendorName = grEx.Rows[nRowIdx].Cells[0].Text;
                                string sComponentName = grEx.Rows[nRowIdx].Cells[3].Text;
                                int sQty = Convert.ToInt32(txtQty.Text.Trim());
                                sCurrencyID = grEx.DataKeys[nRowIdx].Values[3].ToString();
                                sVendorType = grEx.DataKeys[nRowIdx].Values[4].ToString();
                                BLL.Release.UpdateComponentNo(Convert.ToInt32(grEx.DataKeys[nRowIdx].Values[0].ToString()),
                                                                 Convert.ToInt32(grEx.DataKeys[nRowIdx].Values[1].ToString()),
                                                                 Convert.ToInt32(grEx.DataKeys[nRowIdx].Values[2].ToString()),
                                                               txtComponentNo.Text.Trim(), sCurrencyID, sVendorType, sUserID, sVendorName, sComponentName, Convert.ToInt32(grEx.DataKeys[nRowIdx].Values[5].ToString()), txtDrawingNo.Text.Trim(), txtRevNo.Text.Trim(),sQty);
                            }
                        }
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
          
            return isChangebtn;
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
            
            bool isChangeBtn=UpdateException();
            if (!isChangeBtn)
            {
                bIsExceptionClick = false;
                btnMntExp_Click(btnMntExp, EventArgs.Empty);
            }
            else
            {
                BindComponentNumber();
                if (lblError.Text == "")
                {
                    lblError.Text = "Updation successful...";
                    lblError.ForeColor = System.Drawing.Color.Green;
                }
                
            }
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

        protected void btnCan_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            pnlEx.Visible = false;
            //btnCan.Visible = false;
            //btnSav.Visible = false;
            navPnlBtn.Visible = false;

            cbVL.Enabled = true;
            cbAV.Disabled = false;
            btnMntExp.Enabled = true;
            btnChangeComp.Enabled = true;

            cbMatCat.Enabled = true;
            cbMatCatAll.Disabled = false;

            btnMntExp.Visible = true;
            btnChangeComp.Visible = true;

            lblHeading.Visible = true;
            lblHeading.Text = "";
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
                        TextBox txtDirectMtl = (TextBox)grEx.Rows[nRowIdx].FindControl("txtDirectMtl");
                        TextBox txtLabor = (TextBox)grEx.Rows[nRowIdx].FindControl("txtLabor");
                        TextBox txtSubcon = (TextBox)grEx.Rows[nRowIdx].FindControl("txtSubcon");
                        TextBox txtSubconBatam = (TextBox)grEx.Rows[nRowIdx].FindControl("txtSubconBatam");
                        TextBox txtDMFJ = (TextBox)grEx.Rows[nRowIdx].FindControl("txtDMFJ");
                        TextBox txtQty = (TextBox)grEx.Rows[nRowIdx].FindControl("txtQty");

                        sInput = Convert.ToString(100 + nRowIdx);
                        if (txtDirectMtl.Enabled)
                            txtDirectMtl.Text = sInput;
                        if (txtLabor.Enabled)
                            txtLabor.Text = sInput;
                        if (txtSubcon.Enabled)
                            txtSubcon.Text = sInput;
                        if (txtSubconBatam.Enabled)
                            txtSubconBatam.Text = sInput;
                        if (txtDMFJ.Enabled)
                            txtDMFJ.Text = sInput;
                        if (txtQty.Enabled)
                            txtQty.Text = "10";

                        if (txtDirectMtl.Enabled || txtLabor.Enabled ||
                               txtSubcon.Enabled || txtSubconBatam.Enabled || txtDMFJ.Enabled || txtQty.Enabled)
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

        protected void btnChangeComp_Click(object sender, EventArgs e)
        {
            lblHeading.Visible = true;
            lblHeading.Text = "Change Componet";
           BindComponentNumber();

        }

        private void BindComponentNumber()
        {
            isChangedComBtnClick = true;
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
                    BindChangeComponent(Convert.ToInt32(ViewState["RefNo"]), Vendor.Substring(0, Vendor.LastIndexOf("','")), sMatCatList);
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
                    ResetScreenOnError("Please select at least one material category");
                }
                else
                {
                    ResetScreenOnError("Please select at least one vendor and one material category");
                }

            }
        }      
        
    }
}
