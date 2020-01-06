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
using System.Configuration;
using CrystalDecisions.ReportSource;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using NewOrderingSystem.Reports;
using System.IO;

namespace NewOrderingSystem
{	
	public class Output : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblError;
		protected System.Web.UI.WebControls.Button btnPrevious;
		protected System.Web.UI.WebControls.Button btnViewOutput;
		protected System.Web.UI.WebControls.Button btnMCApp;
		protected System.Web.UI.WebControls.Button btnLayout;
		protected System.Web.UI.WebControls.Button btnLogonToPCES;
		protected System.Web.UI.WebControls.Button btnInterfaceToPCES;
		protected CrystalDecisions.Web.CrystalReportViewer CrystalReportViewer1;
		protected System.Web.UI.WebControls.Button btnExportToXL;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidContractNo;				
		protected System.Web.UI.WebControls.Label lblUserName;
	
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{			
			InitializeComponent();
			base.OnInit(e);
		}
				
		private void InitializeComponent()
		{   
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion				
		
		string Model,ContractNo;		
		DataTable dtTempSpec=new DataTable();
		
		private void Page_Load(object sender, System.EventArgs e)
		{		
			if (Session["UserId"]==null)
				Response.Redirect("Login.aspx");
			InitProperties();			
			LoadReport();
		}

		private void LoadReport()
		{
            ReportClass objViewReport = null;
            
            SqlConnection selectConnection = new SqlConnection
            {
                ConnectionString = ConfigurationSettings.AppSettings["SQLConn4Rpt"].ToString()
            };
            selectConnection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("Exec SP_RptSummaryDetails " + this.Session["RefNo"].ToString(), selectConnection);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "repDS");
            objViewReport = new rptSummaryDetails
            {
                PrintOptions = { PaperSize = PaperSize.PaperA4 }
            };
            ParameterDiscreteValue val = new ParameterDiscreteValue
            {
                Value = this.Session["RefNo"].ToString()
            };
            objViewReport.SetParameterValue("@RefNo", val);

            TableLogOnInfo Loginfo = new TableLogOnInfo();
            foreach (CrystalDecisions.CrystalReports.Engine.Table oTable in objViewReport.Database.Tables)
            {
                Loginfo = oTable.LogOnInfo;
                Loginfo.ConnectionInfo.ServerName = ConfigurationSettings.AppSettings["ReportServer"];
                Loginfo.ConnectionInfo.DatabaseName = ConfigurationSettings.AppSettings["ReportDB"];
                Loginfo.ConnectionInfo.UserID = ConfigurationSettings.AppSettings["ReportUserID"];
                Loginfo.ConnectionInfo.Password = ConfigurationSettings.AppSettings["ReportPassword"];
                Loginfo.TableName = oTable.Name;
                oTable.ApplyLogOnInfo(Loginfo);
            }

            CrystalReportViewer1.ReportSource = objViewReport;
            CrystalReportViewer1.DataBind();				
		}
		private void InitProperties()
		{			
			//btnInput.Attributes.Add("onClick","return confirmInput();");			
			//Model=Request.QueryString["LIFT MODEL"].ToString();						
			//ContractNo=Request.QueryString["PROJECT NO"].ToString();
            hidContractNo.Value = Request.QueryString["ContractNo"].ToString();	
			//btnInterfaceToPCES.Attributes.Add("OnClick","OpenWindow('PCESInteface.aspx?Model="+Model+"')");					
            //if(Request.QueryString["OutputType"].ToString()=="1") //"1" for output "3" for ViewCostData
            //{
            //    if (Model.ToUpper() =="ECEEDIII" || Model.ToUpper() =="EXGL")
            //    {
            //        MakeVisibleButtons(true,true,true,true,true,false,false,false,true);
            //    }
            //    else
            //        MakeVisibleButtons(true,true,true,true,true,true,false,false,true);
            //}
            //else if(Request.QueryString["OutputType"].ToString()=="3") //"1" for output "3" for ViewCostData
            //    MakeVisibleButtons(false,false,false,false,true,false,true,true,false);
		}		
			
		private void MakeVisibleButtons(bool p_btnInput,bool p_btnSave,bool p_btnNew,bool p_btnLoad,bool p_tnViewOutput,bool p_btnMCApp,bool p_LogonToPCES,bool p_InterfaceToPCES,bool p_btnLayout)
		{
			//btnInput.Visible =p_btnInput;
			//btnSave.Visible=p_btnSave;
			//btnNew.Visible=p_btnNew;
			//btnLoad.Visible=p_btnLoad;
			btnViewOutput.Visible=p_tnViewOutput;
			btnMCApp.Visible=p_btnMCApp;
			btnLogonToPCES.Visible=p_LogonToPCES;
			btnInterfaceToPCES.Visible=p_InterfaceToPCES;
			btnLayout.Visible=p_btnLayout;
		}		
	}
}
