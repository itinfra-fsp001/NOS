using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;

namespace NewOrderingSystem
{
    public partial class ReportExport : System.Web.UI.Page
    {
        private int nRefNo=0;
        private String sPrjDocNo="";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
                Response.Redirect("Login.aspx");
            if (!IsPostBack)
            {
                nRefNo = Convert.ToInt32(base.Request.QueryString["RefNo"]);
                sPrjDocNo = Convert.ToString(base.Request.QueryString["PrjDocNo"]);
                grdReportExcel.Visible = false;
                ExportReportToExcel();
            }
            
            
        }
        private void ExportReportToExcel()
        {
            try
            {            
                
                if (!BindReportToGrid())
                    return;
                String sFileName = "";
                String sInvalidFileName = new string(Path.GetInvalidFileNameChars());
                
                //--- Set the file name----
                if (sPrjDocNo != null)
                {
                    sFileName = sPrjDocNo;
                    foreach (char c in sInvalidFileName)
                    {
                        sFileName = sFileName.Replace(c.ToString(), "_");
                    }
                    sFileName = "Report_" + sFileName + "_" + Convert.ToString(nRefNo) + ".xls";
                }
                else
                {
                    sFileName = "ExportOutput";
                    sFileName = "Report_" + sFileName + ".xls";
                }


                

                //---- Set the report headers----
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.AddHeader(
                    "content-disposition", string.Format("attachment; filename={0}", sFileName));
                HttpContext.Current.Response.ContentType = "application/ms-excel";

                //--- Render the data to excel----------
                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                    {
                        Table objTable = new Table();
                        objTable.GridLines = GridLines.Both;
                        objTable.Rows.Add(grdReportExcel.HeaderRow);
                        objTable.Rows[0].BackColor = grdReportExcel.HeaderStyle.BackColor;
                        objTable.Rows[0].ForeColor = grdReportExcel.HeaderStyle.ForeColor;
                        objTable.Rows[0].Font.Size  = grdReportExcel.HeaderStyle.Font.Size ;
                        objTable.Rows[0].Font.Name  = grdReportExcel.HeaderStyle.Font.Name;
                        objTable.Rows[0].Font.Bold = true;
                        int nRowIdx = 1;
                        foreach (GridViewRow row in grdReportExcel.Rows)
                        {
                            objTable.Rows.Add(row);
                            if ((nRowIdx % 2) == 0)
                            {
                                objTable.Rows[nRowIdx].BackColor = grdReportExcel.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                objTable.Rows[nRowIdx].BackColor = grdReportExcel.RowStyle.BackColor;
                            }

                            
                            objTable.Rows[nRowIdx].ForeColor = grdReportExcel.RowStyle.ForeColor;
                            objTable.Rows[nRowIdx].Font.Size = grdReportExcel.RowStyle.Font.Size;
                            objTable.Rows[nRowIdx].Font.Name = grdReportExcel.RowStyle.Font.Name;
                            nRowIdx++;
                        }
                        objTable.RenderControl(htw);
                        
                        HttpContext.Current.Response.Write(sw.ToString());
                        HttpContext.Current.Response.Flush();
                        HttpContext.Current.Response.End();
                    }
                }
                //----------

            }
            catch (Exception )
            {

                
            }
        }
        
        private Boolean BindReportToGrid()
        {
            Boolean bRetVal = false;
            try
            {                
                DataTable dtReport = new DataTable();
                if ( sPrjDocNo != null )
                {
                    dtReport = BLL.IssuePO.GetWBSReportDetails(nRefNo, "All");
                }
                else
                {
                    dtReport = BLL.IssuePO.GetOutputReportDetails(nRefNo);
                }


                if (dtReport.Rows.Count <= 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Records Found');", true);
                }
                else
                {
                    grdReportExcel.DataSource = dtReport;
                    grdReportExcel.DataBind();
                    bRetVal = true;
                }
                
                
            }
            catch (SqlException sqEx)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert("+sqEx.Message+");", true);
                //System.Windows.Forms.MessageBox.Show("BindReportToGrid->SQLError : " + sqEx.Message);
            }
            catch (Exception Ex)
            {
                //System.Windows.Forms.MessageBox.Show("BindReportToGrid->Error : " + Ex.Message);
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert(" + Ex.Message + ");", true);
            }
            return bRetVal;
        }        
    }
}
