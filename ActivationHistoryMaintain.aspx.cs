using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Drawing;

namespace NewOrderingSystem
{
    public partial class ActivationHistoryMaintain : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["UserId"] == null)
                Response.Redirect("Login.aspx");	
            if (!IsPostBack)
            {
                lblUserName.Text = "WELCOME " + Session["UserName"].ToString();
                Session["RefNo"] = base.Request.QueryString["RefNo"].ToUpper();
                Session["ActivateVersionNo"] = base.Request.QueryString["VersionNo"].ToUpper();
                populateKSetNames();                
                InitProperties();
            }
            lblVersion.Text = ConfigurationSettings.AppSettings["NOSVersion"].ToString();
            txtRevReason.Attributes.Add("maxLength", "300");
        }
        private void InitProperties()
        {
            
        }
        private void populateKSetNames()
        {
            String sRefNo=base.Request.QueryString["RefNo"].ToUpper();
            lblError.Text = "";
            
            try
            {
                DataTable dtKSetNames = new DataTable();
                dtKSetNames = BLL.General.GetPartNo4RevActivate(Convert.ToInt32(sRefNo));
                if (dtKSetNames.Rows.Count > 0)
                {
                    grPrjRev.DataSource = dtKSetNames;
                    grPrjRev.DataBind();
                    PnlBtn.Visible = true;
                }
                else
                {
                    grPrjRev.DataSource = null;
                    grPrjRev.DataBind();
                    lblError.ForeColor= Color.Red;
                    lblError.Text = "The List is empty...";
                    PnlBtn.Visible = false ;
                }

            }
            catch (Exception ex)
            {
                
            }
        }
        protected void grPrjRev_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string strRdButton;
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                RadioButton rb = (RadioButton)e.Row.FindControl("rdbContract");
                rb.Attributes.Add("onClick", "RadioCheck(this,'" + dr["PartNo"].ToString().Trim() + "');");
                //strRdButton = "<input type='radio' name='rdContract' AutoPostBack='true' \">";
                //strRdButton = @"<input type=radio name=rdContract onClick=GetContract('" + dr["RefNo"].ToString().Trim() + "','"+ dr["Mode"].ToString().Trim()+ "','" + dr["Status"].ToString().Trim()+"','" + dr["ModelType"].ToString().Trim() +"')>";								
               // e.Row.Cells[0].Text = strRdButton;

            }
        }

        protected void grPrjRev_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grPrjRev.PageIndex = e.NewPageIndex;
                populateKSetNames();
                SetSelectedRecord(HidPartNo.Value);

            }
            catch (Exception)
            {
                                
            }
        }
        protected void OnProjectRevSelect(object sender, EventArgs e)
        {
            try
            {
                String sRefNo, sPartNo,sRevDescription="";
                Int32  nRevNo=0;
                sRefNo = Convert.ToString(Session["RefNo"]);
                int nRefNo = Convert.ToInt32(sRefNo);
                foreach (GridViewRow objRow in grPrjRev.Rows)
                    {
                        if (objRow.RowType == DataControlRowType.DataRow)
                        {
                            RadioButton rb = (RadioButton)objRow.FindControl("rdbContract");

                             if (rb != null)
                             {

                                 if (rb.Checked)
                                 {
                                     sPartNo = objRow.Cells[1].Text;
                                     if (!String.IsNullOrEmpty(objRow.Cells[3].Text))
                                     {
                                         Int32.TryParse(Convert.ToString(objRow.Cells[3].Text), out nRevNo);
                                        
                                     }
                                     sRevDescription = BLL.Release.GetTempProjectRevDetails(nRefNo, sPartNo, nRevNo);
                                     
                                     break;
                                 }
                             }                             
                            
                        }
                    }
                
                txtRevReason.Text = sRevDescription;
                
                
            }
            catch (Exception ex)
            {
                
                
            }
        }

        protected void btnSaveKSet_Click(object sender, EventArgs e)
        {
            try
            {
                lblError.Text="";
                
                String sRefNo, sPartNo, sRevReason = "", sResult="";
                Int32 nRevNo = 0,nActivateVersionNo=0;
                sRefNo = Convert.ToString(Session["RefNo"]);
                nActivateVersionNo = Convert.ToInt32(Convert.ToString(Session["ActivateVersionNo"]));
                int nRefNo = Convert.ToInt32(sRefNo);
                String sUserID = Convert.ToString(Session["UserId"]);
                foreach (GridViewRow objRow in grPrjRev.Rows)
                {
                    if (objRow.RowType == DataControlRowType.DataRow)
                    {
                        RadioButton rb = (RadioButton)objRow.FindControl("rdbContract");

                        if (rb != null)
                        {

                            if (rb.Checked)
                            {
                                sPartNo = objRow.Cells[1].Text;
                                if (!String.IsNullOrEmpty(objRow.Cells[3].Text))
                                {
                                    Int32.TryParse(Convert.ToString(objRow.Cells[3].Text), out nRevNo);

                                }
                                sRevReason = txtRevReason.Text;
                                sResult = BLL.Release.UpdateProjectRevision(nRefNo,nActivateVersionNo,sPartNo, sRevReason,sUserID);
                                if (sResult.ToUpper() == "SUCCESS")
                                {
                                    
                                    populateKSetNames();
                                    SetSelectedRecord(sPartNo);
                                    lblError.ForeColor = Color.Green;
                                    lblError.Text = "Successfully updated revision details";
                                }
                                else
                                {
                                    lblError.ForeColor = Color.Red;
                                    lblError.Text = "Failed to update revision details";
                                }
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


        private void SetSelectedRecord(string sPartNo)
        {
            foreach (GridViewRow objRow in grPrjRev.Rows)
            {
                if (objRow.RowType == DataControlRowType.DataRow)
                {
                    RadioButton rb = (RadioButton)objRow.FindControl("rdbContract");
                    if (sPartNo == objRow.Cells[1].Text)
                    {
                        rb.Checked = true;
                        break;
                    }

                }
            }

        }

    }
}
