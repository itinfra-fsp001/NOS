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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using NewOrderingSystem.Reports;
using System.Data.SqlClient;
using System.Configuration;
using SQLHelper.DataAccessLayer;

namespace NewOrderingSystem
{
    /// <summary>
    /// Summary description for ViewReports.
    /// </summary>
    public class ViewReports : System.Web.UI.Page
    {
        protected CrystalDecisions.Web.CrystalReportViewer CrystalReportViewer1;
        //protected TextBox txtPrinter;
        protected DropDownList ddlVendor;
        protected Panel pnlVendor;
        protected Panel pnlMatCategory;
        protected DropDownList ddlMatCategory;

        protected Panel pnlComponent;
        protected TextBox txtComponentNo;


        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            if (!Page.IsPostBack)
            {
                Session["REP"] = null;
            }

        }
        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {

            if (Session["REP"] == null)
            {
                ShowReports();
            }
            else
            {
                this.CrystalReportViewer1.ReportSource = Session["REP"];
                this.CrystalReportViewer1.HasToggleGroupTreeButton = false;
                this.CrystalReportViewer1.DataBind();
            }
        }

        private void ShowReports()
        {
            ReportClass class2 = null;
            string selectCommandText = "";
            bool bHasRecords = true;
            string RptName = base.Request.QueryString["ReportName"].ToString();

            SqlConnection connection;
            SqlDataAdapter adapter;
            DataSet set;
            if (RptName == "Production")
            {
                connection = new SqlConnection
                {
                    ConnectionString = ConfigurationSettings.AppSettings["SQLConn4Rpt"].ToString()
                };
                connection.Open();
                adapter = new SqlDataAdapter(base.Request.QueryString["SpName"] + " ' ','" + base.Request.QueryString["ContractNo"] + "'", connection);
                set = new DataSet();
                adapter.Fill(set, "repDS");
                class2 = new repProductionSpec();
                class2.SetParameterValue("@ModelType", base.Request.QueryString["Model"]);
                class2.SetParameterValue("@ContractNo", base.Request.QueryString["ContractNo"]);
            }
            else if (RptName == "Exception")
            {
                connection = new SqlConnection
                {
                    ConnectionString = ConfigurationSettings.AppSettings["SQLConn4Rpt"].ToString()
                };
                connection.Open();
                selectCommandText = base.Request.QueryString["StrSql"];
                adapter = new SqlDataAdapter(selectCommandText, connection);
                set = new DataSet();
                adapter.Fill(set, "repDS");
                class2 = new repExceptions();
                ParameterDiscreteValue val = new ParameterDiscreteValue
                {
                    Value = base.Request.QueryString["RefNo"].ToUpper()
                };
                class2.SetParameterValue("@RefNo", val);
            }
            else if (RptName == "Output" || RptName == "PDFOutput")
            {
                connection = new SqlConnection
                {
                    ConnectionString = ConfigurationSettings.AppSettings["SQLConn4Rpt"].ToString()
                };
                connection.Open();
                selectCommandText = base.Request.QueryString["StrSql"];
                adapter = new SqlDataAdapter(selectCommandText, connection);
                set = new DataSet();
                adapter.Fill(set, "repDS");
                class2 = new rptSummaryDetails();
                ParameterDiscreteValue val = new ParameterDiscreteValue
                {
                    Value = base.Request.QueryString["RefNo"].ToUpper()
                };
                class2.SetParameterValue("@RefNo", val);
                if (Session["VendorName"] == null)
                    class2.SetParameterValue("@VendorName", "All");
                else
                    class2.SetParameterValue("@VendorName", Session["VendorName"]);
            }
            else if (RptName == "OutputList" || RptName == "PDFOutputList")
            {
                connection = new SqlConnection
                {
                    ConnectionString = ConfigurationSettings.AppSettings["SQLConn4Rpt"].ToString()
                };
                connection.Open();
                selectCommandText = base.Request.QueryString["StrSql"];
                adapter = new SqlDataAdapter(selectCommandText, connection);
                set = new DataSet();
                adapter.Fill(set, "repDS");
                class2 = new rptKSetDetails();
                ParameterDiscreteValue val = new ParameterDiscreteValue
                {
                    Value = base.Request.QueryString["RefNo"].ToUpper()
                };
                class2.SetParameterValue("@RefNo", val);

                if (Session["VendorName"] == null)
                    class2.SetParameterValue("@VendorName", "All");
                else
                    class2.SetParameterValue("@VendorName", Session["VendorName"]);
                if (Session["MaterialCategoryID"] == null)
                    class2.SetParameterValue("@MaterialCategoryID", "All");
                else
                    class2.SetParameterValue("@MaterialCategoryID", Session["MaterialCategoryID"].ToString());
            }

            else if (RptName == "OutputPO" || RptName == "PDFOutputPO")
            {
                connection = new SqlConnection
                {
                    ConnectionString = ConfigurationSettings.AppSettings["SQLConn4Rpt"].ToString()
                };
                connection.Open();
                selectCommandText = base.Request.QueryString["StrSql"];
                adapter = new SqlDataAdapter(selectCommandText, connection);
                set = new DataSet();
                adapter.Fill(set, "repDS");
                class2 = new rptWBSDetails();
                ParameterDiscreteValue val = new ParameterDiscreteValue
                {
                    Value = base.Request.QueryString["RefNo"].ToUpper()
                };
                class2.SetParameterValue("@WBSRefNo", val);
                if (Session["VendorName"] == null)
                    class2.SetParameterValue("@VendorName", "All");
                else
                    class2.SetParameterValue("@VendorName", Session["VendorName"]);

                class2.SetParameterValue("@PartNo", "");
            }
            else if (RptName == "OutputListPO" || RptName == "PDFOutputListPO")
            {
                connection = new SqlConnection
                {
                    ConnectionString = ConfigurationSettings.AppSettings["SQLConn4Rpt"].ToString()
                };
                connection.Open();
                selectCommandText = base.Request.QueryString["StrSql"];
                adapter = new SqlDataAdapter(selectCommandText, connection);
                set = new DataSet();
                adapter.Fill(set, "repDS");
                class2 = new rptKSetWBSDetails();
                ParameterDiscreteValue val = new ParameterDiscreteValue
                {
                    Value = base.Request.QueryString["RefNo"].ToUpper()
                };
                class2.SetParameterValue("@RefNo", val);

                if (Session["VendorName"] == null)
                    class2.SetParameterValue("@VendorName", "All");
                else
                    class2.SetParameterValue("@VendorName", Session["VendorName"]);
            }
            else if (RptName == "CostReport")
            {
                connection = new SqlConnection
                {
                    ConnectionString = ConfigurationSettings.AppSettings["SQLConn4Rpt"].ToString()
                };
                connection.Open();
                selectCommandText = base.Request.QueryString["StrSql"];
                adapter = new SqlDataAdapter(selectCommandText, connection);
                set = new DataSet();
                adapter.Fill(set, "repDS");
                bHasRecords = HasRecords(set);
                class2 = new rptCostReport();
                ParameterDiscreteValue val = new ParameterDiscreteValue
                {
                    Value = base.Request.QueryString["RefNo"].ToUpper()
                };
                class2.SetParameterValue("@RefNo", val);

            }
            else if (RptName == "ZeroCostReport")
            {
                connection = new SqlConnection
                {
                    ConnectionString = ConfigurationSettings.AppSettings["SQLConn4Rpt"].ToString()
                };
                connection.Open();
                selectCommandText = base.Request.QueryString["StrSql"];
                adapter = new SqlDataAdapter(selectCommandText, connection);
                set = new DataSet();
                adapter.Fill(set, "repDS");
                bHasRecords = HasRecords(set);
                class2 = new rptZeroCostReport();
                ParameterDiscreteValue val = new ParameterDiscreteValue
                {
                    Value = base.Request.QueryString["RefNo"].ToUpper()
                };
                class2.SetParameterValue("@RefNo", val);

            }


            else if (RptName == "CWTReport")
            {
                connection = new SqlConnection
                {
                    ConnectionString = ConfigurationSettings.AppSettings["SQLConn4Rpt"].ToString()
                };
                connection.Open();
                selectCommandText = base.Request.QueryString["StrSql"];
                adapter = new SqlDataAdapter(selectCommandText, connection);
                set = new DataSet();
                adapter.Fill(set, "repDS");
                bHasRecords = HasRecords(set);
                class2 = new rptCWTWeight();
                ParameterDiscreteValue val = new ParameterDiscreteValue
                {
                    Value = base.Request.QueryString["RefNo"].ToUpper()
                };
                class2.SetParameterValue("@RefNo", val);

            }

            else if (RptName == "ComponentReport" && txtComponentNo != null && txtComponentNo.Text != "")
            {
                connection = new SqlConnection
                {
                    ConnectionString = ConfigurationSettings.AppSettings["SQLConn4Rpt"].ToString()
                };
                connection.Open();
                selectCommandText = base.Request.QueryString["StrSql"];
                adapter = new SqlDataAdapter(selectCommandText + " '" + Convert.ToString(txtComponentNo.Text) + "'", connection);
                set = new DataSet();
                adapter.Fill(set, "repDS");
                bHasRecords = HasRecords(set);
                class2 = new rptComponentKitProject();
                ParameterDiscreteValue val = new ParameterDiscreteValue
                {
                    Value = txtComponentNo.Text.ToUpper()
                };
                class2.SetParameterValue("@ComponentNo", val);

            }

            if (class2 == null)
            {
                return;
            }

            TableLogOnInfo logonInfo = new TableLogOnInfo();
            foreach (CrystalDecisions.CrystalReports.Engine.Table oTable in class2.Database.Tables)
            {
                logonInfo = oTable.LogOnInfo;
                logonInfo.ConnectionInfo.ServerName = ConfigurationSettings.AppSettings["ReportServer"];
                logonInfo.ConnectionInfo.DatabaseName = ConfigurationSettings.AppSettings["ReportDB"];
                logonInfo.ConnectionInfo.UserID = ConfigurationSettings.AppSettings["ReportUserID"];
                logonInfo.ConnectionInfo.Password = ConfigurationSettings.AppSettings["ReportPassword"];
                logonInfo.TableName = oTable.Name;
                oTable.ApplyLogOnInfo(logonInfo);
            }

            //System.Drawing.Printing.PrinterSettings.StringCollection printer = System.Drawing.Printing.PrinterSettings.InstalledPrinters;
            //for (int i = 0; i < printer.Count; i++)
            //{
            //    txtPrinter.Text += class2.PrintOptions.PrinterName;
            //    txtPrinter.Text += printer[i].ToString()+";";
            //    //Console.WriteLine(printer[i].ToString());
            //}
            if (Session["REP"] == null)
                Session["REP"] = class2;
            if (Convert.ToBoolean(Request.QueryString["IsPDF"]))
            {
                //class2.PrintOptions.PrinterName = "CutePDF Writer";
                //class2.PrintToPrinter(1, true, 0, 0);
                //class2.ExportToDisk(ExportFormatType.PortableDocFormat, "c:\\ReportOutput.pdf");
                class2.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "");
            }
            else
            {
                if (bHasRecords)
                {
                    this.CrystalReportViewer1.ReportSource = class2;
                    this.CrystalReportViewer1.HasToggleGroupTreeButton = false;
                    this.CrystalReportViewer1.DataBind();
                    if (Session["REP"] == null)
                        Session["REP"] = class2;
                }
                else
                {
                    if (RptName == "CostReport")
                    {
                        if (!ClientScript.IsStartupScriptRegistered("ExitReport"))
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(),
                                "ExitReport", "ExitReportForNoCostReport();", true);
                        }
                    }
                    else if (RptName == "ZeroCostReport")
                    {
                        if (!ClientScript.IsStartupScriptRegistered("ExitZeroCostReport"))
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(),
                                "ExitZeroCostReport", "ExitReportForZeroCostReport();", true);
                        }
                    }
                    else if (RptName == "ComponentReport")
                    {
                        this.CrystalReportViewer1.ReportSource = null;
                        this.CrystalReportViewer1.HasToggleGroupTreeButton = false;
                        this.CrystalReportViewer1.DataBind();

                        if (!ClientScript.IsStartupScriptRegistered("NoRecordComponentReport"))
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(),
                                "NoRecordComponentReport", "NoRecordForComponentReport();", true);
                        }
                    }
                }
            }
            //ReportClass objReport=null;
            //string strsql = "";

            //switch (Request.QueryString["ReportName"].ToString())
            //{
            //    case "Exception":
            //    {
            //        SqlConnection objConn = new SqlConnection();
            //        SqlDataAdapter objDa;
            //        DataSet objDs;
            //        objConn.ConnectionString = ConfigurationSettings.AppSettings["SQLConn4Rpt"].ToString();
            //        objConn.Open();
            //        strsql = Request.QueryString["StrSql"];
            //        objDa = new SqlDataAdapter(strsql, objConn);
            //        objDs = new DataSet();
            //        objDa.Fill(objDs, "repDS");	
            //        objReport=new repExceptions();
            //        //objReport.PrintOptions.PaperSize = PaperSize.PaperA4;
            //        //objReport.PrintOptions.PaperOrientation = PaperOrientation.Landscape;
            //        ParameterDiscreteValue objParamer=new ParameterDiscreteValue();
            //        objParamer.Value = Request.QueryString["RefNo"].ToUpper();
            //        objReport.SetParameterValue("@RefNo",objParamer);
            //        break;
            //    }	
            //    case "Production":
            //    {
            //        SqlConnection objConn = new SqlConnection();
            //        SqlDataAdapter objDa;
            //        DataSet objDs;
            //        objConn.ConnectionString = ConfigurationSettings.AppSettings["SQLConn4Rpt"].ToString();
            //        objConn.Open();
            //        strsql = Request.QueryString["SpName"]+" ' ','"+Request.QueryString["ContractNo"]+"'";
            //        objDa = new SqlDataAdapter(strsql, objConn);
            //        objDs = new DataSet();
            //        objDa.Fill(objDs, "repDS");	

            //        objReport=new repProductionSpec();										
            //        //ParameterDiscreteValue objParm=new ParameterDiscreteValue();										
            //        //objParm.Value = "";//Request.QueryString["ModelType"].ToUpper();
            //        objReport.SetParameterValue("@ModelType",Request.QueryString["Model"]);

            //        //ParameterDiscreteValue objContractNo=new ParameterDiscreteValue();
            //        //objParm.Value = Request.QueryString["ContractNo"].ToUpper();
            //        objReport.SetParameterValue("@ContractNo",Request.QueryString["ContractNo"]);					
            //        break;
            //    }	
            //}
            //TableLogOnInfo Loginfo = new TableLogOnInfo();
            //foreach(CrystalDecisions.CrystalReports.Engine.Table oTable in objReport.Database.Tables)
            //{
            //    Loginfo = oTable.LogOnInfo;
            //    Loginfo.ConnectionInfo.ServerName = ConfigurationSettings.AppSettings["ReportServer"]; 
            //    Loginfo.ConnectionInfo.DatabaseName = ConfigurationSettings.AppSettings["ReportDB"]; 
            //    Loginfo.ConnectionInfo.UserID = ConfigurationSettings.AppSettings["ReportUserID"]; 
            //    Loginfo.ConnectionInfo.Password = ConfigurationSettings.AppSettings["ReportPassword"]; 
            //    Loginfo.TableName = oTable.Name;
            //    oTable.ApplyLogOnInfo(Loginfo);
            //}
            //CrystalReportViewer1.ReportSource = objReport;
            //CrystalReportViewer1.HasToggleGroupTreeButton=false;
            //CrystalReportViewer1.DataBind();
        }

        private bool HasRecords(DataSet dataSet)
        {
            foreach (DataTable dt in dataSet.Tables) if (dt.Rows.Count > 0) return true;
            return false;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string sRptName = base.Request.QueryString["ReportName"].ToString();
                //Session["REP"] = null;
                pnlMatCategory.Visible = false;
                if (sRptName == "OutputList" ||
                        sRptName == "PDFOutputList" ||
                        sRptName == "Output" ||
                        sRptName == "PDFOutput" ||
                        sRptName == "OutputPO" ||
                        sRptName == "PDFOutputPO" ||
                        sRptName == "OutputListPO" ||
                        sRptName == "PDFOutputListPO")
                {
                    pnlComponent.Visible = false;
                    PopulateVendor();
                    pnlVendor.Visible = true;

                    if (sRptName == "OutputList" || sRptName == "PDFOutputList")
                    {
                        PopulateMaterialCategory();
                    }
                }
                else if (sRptName == "ComponentReport")
                {
                    pnlVendor.Visible = false;
                    pnlComponent.Visible = true;
                }
                else
                {
                    pnlVendor.Visible = false;
                    pnlComponent.Visible = false;
                }
                //ShowReports();                 
            }

        }

        protected void btnShowComponent_Click(object sender, EventArgs e)
        {
            string str = txtComponentNo.Text;
            ShowReports();
        }

        private void PopulateVendor()
        {
            ddlVendor.DataSource = SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"], "SP_GetVendor", 1).Tables[0];
            ddlVendor.DataTextField = "VendorName";
            ddlVendor.DataValueField = "VendorNo";
            ddlVendor.DataBind();
        }
        private void PopulateMaterialCategory()
        {
            pnlMatCategory.Visible = true;
            String sRefNo = base.Request.QueryString["RefNo"].ToUpper();
            int nRefNo = 0;
            Int32.TryParse(sRefNo, out nRefNo);
            ddlMatCategory.DataSource = SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"],
                                                                "SP_GetMatCategoryFromOutput", nRefNo).Tables[0];
            ddlMatCategory.DataValueField = "MaterialCategoryID";
            ddlMatCategory.DataTextField = "MaterialCategoryName";
            ddlMatCategory.DataBind();
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            Session["VendorName"] = ddlVendor.SelectedItem.Text;
            if (pnlMatCategory.Visible)
            {
                Session["MaterialCategoryID"] = ddlMatCategory.SelectedValue.ToString();
            }
            Session["REP"] = null;
            ShowReports();
            Session["VendorName"] = null;
            Session["MaterialCategoryID"] = null;

        }
    }
}
