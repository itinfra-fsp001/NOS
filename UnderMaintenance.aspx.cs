using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace NewOrderingSystem
{
    public partial class UnderMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //grModel.DataSource = BLL.MainScreen.GetScreenTemplate("ECEEDIII", "MYT000002T-L001", 1).Tables[0];
                //grModel.DataBind();

                //pnlModel.Visible = false;

                //grTemplate02.DataSource = BLL.MainScreen.GetScreenTemplate("ECEEDIII", "MYT000002T-L001", 5).Tables[0];
                //grTemplate02.DataBind();

                grModel.DataSource = BLL.MainScreen.GetScreenTemplate("ECEEDIII", "MYT000002T-L001", 1).Tables[0];
                grModel.DataBind();

            }
        }

        protected void grModel_RowDataBound(object sender, GridViewRowEventArgs e)
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
                   dsParmValus = BLL.MainScreen.FetchParmValues(Convert.ToInt32(dr["ParmCode"]), "ECEEDIII");

                    ddlpValue.DataSource = dsParmValus;
                    ddlpValue.DataValueField = "ValueCode";
                    ddlpValue.DataTextField = "ValueDescription";
                    ddlpValue.DataBind();
                    ddlpValue.Items.Insert(0, new ListItem("", ""));
                }
                else if (Convert.ToInt16(dr["IsValueTable"]) == 1)
                {
                    DataSet dsParmValus = new DataSet();
                   dsParmValus = BLL.MainScreen.FetchParmValues(Convert.ToInt32(dr["ParmCode"]), "ECEEDIII");

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
                //if (Convert.ToBoolean(dr["HideFlag"]))
                //    e.Row.Visible = false;

                //if (Convert.ToBoolean(dr["BIsNonStd"]))
                //{
                //    cbNStd.Enabled = true;
                //    int n = GetColumnIndexByName(grModel, "NON STD");
                //    e.Row.Cells[n].BackColor = System.Drawing.Color.SteelBlue;


                //}
                //else
                //{
                //    cbNStd.Enabled = false;
                //}
                if (e.Row.RowIndex == 0)
                {
                    e.Row.Cells[0].Text = "" + (e.Row.RowIndex + 1);
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
            }
        }

        protected void grModel_PreRender(object sender, EventArgs e)
        {

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            // this is required for avoid error (control must be placed inside form tag)
        }

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
                    dsParmValus = BLL.MainScreen.FetchParmValues(Convert.ToInt32(dr["ParmCode"]), "ECEEDIII");

                    ddlpValue.DataSource = dsParmValus;
                    ddlpValue.DataValueField = "ValueCode";
                    ddlpValue.DataTextField = "ValueDescription";
                    ddlpValue.DataBind();
                    ddlpValue.Items.Insert(0, new ListItem("", ""));
                }
                else if (Convert.ToInt16(dr["IsValueTable"]) == 1)
                {
                    DataSet dsParmValus = new DataSet();
                    dsParmValus = BLL.MainScreen.FetchParmValues(Convert.ToInt32(dr["ParmCode"]), "ECEEDIII");

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

                //if (Convert.ToBoolean(dr["BIsNonStd"]))
                //{
                //    cbNStd.Enabled = true;
                //    int n = GetColumnIndexByName(grTemplate02, "NON STD");
                //    e.Row.Cells[n].BackColor = System.Drawing.Color.SteelBlue;

                //}
                //else
                //    cbNStd.Enabled = false;

                //if (e.Row.RowIndex == 0)
                //{
                //    e.Row.Cells[0].Text = "" + (e.Row.RowIndex + 1);
                //}
                //else if (Convert.ToBoolean(dr["HideFlag"]))
                //{
                //    int iSeqNo = Convert.ToInt16(grTemplate02.Rows[e.Row.RowIndex - 1].Cells[0].Text.ToString().Trim());
                //    e.Row.Cells[0].Text = "" + iSeqNo;
                //}
                //else if (!Convert.ToBoolean(dr["HideFlag"]))
                //{
                //    int iSeqNo = Convert.ToInt16(grTemplate02.Rows[e.Row.RowIndex - 1].Cells[0].Text.ToString().Trim()) + 1;
                //    e.Row.Cells[0].Text = "" + iSeqNo;
                //}
            }
        }

        protected void grTemplate02_PreRender(object sender, EventArgs e)
        {

        }

        protected void cbNS_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            grModel.DataSource = BLL.MainScreen.GetScreenTemplate("ECEEDIII", "MYT000002T-L001", 1).Tables[0];
            grModel.DataBind();

            //pnlModel.Visible = true;

            //grTemplate02.DataSource = BLL.MainScreen.GetScreenTemplate("ECEEDIII", "MYT000002T-L001", 5).Tables[0];
            //grTemplate02.DataBind();

            //grTemplate02.Visible = false;
        }
    }
}
