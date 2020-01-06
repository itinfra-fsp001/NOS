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
using NewOrderingSystem.BLL;
using CrystalDecisions.ReportSource;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.SqlClient;
using System.Configuration;
using NewOrderingSystem.Reports;

namespace NewOrderingSystem
{
	/// <summary>
	/// Summary description for Signature.
	/// </summary>
	public class Signature : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.TextBox txtRevisionNo;
		protected System.Web.UI.WebControls.TextBox txtIssueName;
		protected System.Web.UI.WebControls.TextBox txtIssueDate;
		protected System.Web.UI.WebControls.TextBox txtCheckName;
		protected System.Web.UI.WebControls.TextBox txtCheckDate;
		protected System.Web.UI.WebControls.Button btnOk;
		protected System.Web.UI.WebControls.Button btnCancel;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.CheckBox cbApprove;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.TextBox txtPassword;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label lblError;
		protected System.Web.UI.WebControls.Label Label5;
	
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
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (Session["UserId"]==null)
				Response.Redirect("<script>window.close();</script>");
			Inintproperties();
			if (!IsPostBack)
			{
				GetHeadPassword();
			}
		}
		private void Inintproperties()
		{
			btnOk.Attributes.Add("Onclick","return ValidateForm();");			
			btnCancel.Attributes.Add("Onclick","return CloseMe();");
			cbApprove.Attributes.Add("Onclick","EnableDisablePassword();");			
		}
		private void btnOk_Click(object sender, System.EventArgs e)
		{
            //if (cbApprove.Checked)
            //{				
            //    if (txtPassword.Text.Trim()==ViewState["Pass"].ToString().Trim())
            //    {
            //        //BLL.Signature.UpdateSignature(Convert.ToInt32(Request.QueryString["RefNo"]),txtIssueDate.Text.Trim(),txtIssueName.Text.Trim(),txtCheckDate.Text.Trim(),txtCheckName.Text.Trim(),Session["DeptCode"].ToString());
            //        //Response.Write("<script>opener.location.href='Output.aspx?Model="+Request.QueryString["Model"].ToString()+"&RefNo="+Request.QueryString["RefNo"].ToString() +"&OpenFrom=sig&OutputType=1&RevisionNo="+ txtRevisionNo.Text +"&IssueName="+txtIssueName.Text+"&IssueDate="+txtIssueDate.Text+"&CheckName="+txtCheckName.Text+"&CheckDate="+ txtCheckDate.Text+"&ApproverName="+ViewState["Head"].ToString() +" &ReportFor=A';window.close();</script>");
            //        ReportClass objViewReport = null;
            //        string strsql = "";
            //        SqlConnection objConn = new SqlConnection();
            //        SqlDataAdapter objDa;
            //        DataSet objDs; 
            //        objConn.ConnectionString = ConfigurationSettings.AppSettings["SQLConn4Rpt"].ToString();
            //        objConn.Open();
            //        /*if (Request.QueryString["OpenFrom"]==null)//|| Request.QueryString["OpenFrom"].ToString().ToUpper()=="SIG")
            //            strsql = "Exec SP_RepModelExplorer "+Session["RefNo"].ToString()+",1"; //Request.QueryString["StrSql"];				
            //        else*/
            //            strsql = "Exec SP_RepModelExplorer "+Request.QueryString["RefNo"].ToString()+",1"; //Request.QueryString["StrSql"];
            //        objDa = new SqlDataAdapter(strsql, objConn);
            //        objDs = new DataSet();
            //        objDa.Fill(objDs, "repDS");	

            //        objViewReport = new repModelExplorer();
            //        objViewReport.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4;			
            //        //objViewReport.PrintOptions.PrinterName="CutePDF Writer";
			
            //        //objViewReport.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
            //        ParameterDiscreteValue pRefNo = new ParameterDiscreteValue();
			
            //        /*if (Request.QueryString["OpenFrom"]==null)
            //            pRefNo.Value =  Session["RefNo"].ToString();				
            //        else*/
            //            pRefNo.Value = Request.QueryString["RefNo"].ToString();

            //        objViewReport.SetParameterValue("@RefNo", pRefNo);

            //        ParameterDiscreteValue pOutputType=new ParameterDiscreteValue();
            //        pOutputType.Value=Request.QueryString["OutputType"].ToString();
            //        objViewReport.SetParameterValue("@OutputType",pOutputType);

            //        ParameterDiscreteValue pRevisionNo=new ParameterDiscreteValue();
            //        pRevisionNo.Value=txtRevisionNo.Text;
            //        objViewReport.SetParameterValue("RevisionNo",pRevisionNo);
			
            //        ParameterDiscreteValue pIssueName=new ParameterDiscreteValue();
            //        pIssueName.Value=txtIssueName.Text; 
            //        objViewReport.SetParameterValue("IssueName",pIssueName);

            //        ParameterDiscreteValue pIssueDate=new ParameterDiscreteValue();
            //        pIssueDate.Value=txtIssueDate.Text;
            //        objViewReport.SetParameterValue("IssueDate",pIssueDate);

            //        ParameterDiscreteValue pCheckName=new ParameterDiscreteValue();
            //        pCheckName.Value=txtCheckName.Text;
            //        objViewReport.SetParameterValue("CheckName",pCheckName);

            //        ParameterDiscreteValue pCheckDate=new ParameterDiscreteValue();
            //        pCheckDate.Value=txtCheckDate.Text;
            //        objViewReport.SetParameterValue("CheckDate",pCheckDate);

            //        ParameterDiscreteValue pApproverName=new ParameterDiscreteValue();
            //        pApproverName.Value=ViewState["Head"].ToString();
            //        objViewReport.SetParameterValue("ApprovedName",pApproverName);

            //        ParameterDiscreteValue pReportFor=new ParameterDiscreteValue();
            //        pReportFor.Value="";
            //        objViewReport.SetParameterValue("ReportFor",pReportFor);

			
            //        TableLogOnInfo Loginfo = new TableLogOnInfo();
            //        foreach(CrystalDecisions.CrystalReports.Engine.Table oTable in objViewReport.Database.Tables)
            //        {
            //            Loginfo = oTable.LogOnInfo;
            //            Loginfo.ConnectionInfo.ServerName = ConfigurationSettings.AppSettings["ReportServer"]; 
            //            Loginfo.ConnectionInfo.DatabaseName = ConfigurationSettings.AppSettings["ReportDB"]; 
            //            Loginfo.ConnectionInfo.UserID = ConfigurationSettings.AppSettings["ReportUserID"]; 
            //            Loginfo.ConnectionInfo.Password = ConfigurationSettings.AppSettings["ReportPassword"]; 
            //            Loginfo.TableName = oTable.Name;
            //            oTable.ApplyLogOnInfo(Loginfo);
            //        }
            //        /*if (Request.QueryString["OpenFrom"]==null)
            //        {
            //            CrystalReportViewer1.HasPrintButton=true;
            //            CrystalReportViewer1.HasExportButton=false;
            //        }
            //        else
            //        {
            //            CrystalReportViewer1.HasPrintButton=true;
            //            CrystalReportViewer1.HasExportButton=true;
            //        }*/

            //        //CrystalReportViewer1.ReportSource = objViewReport;
            //        //CrystalReportViewer1.DataBind();				

            //        //string strExportFile = (Server.MapPath(".") + "/XLFile/CNWExport.doc");
            //        string strExportFile = BLL.MainScreen.GetContractNoForXL(Convert.ToInt32(Request.QueryString["RefNo"]),Convert.ToInt32(ConfigurationSettings.AppSettings["PROJECT NO"]),Convert.ToInt32(ConfigurationSettings.AppSettings["CLIENT LIFT NO"]))+ "(" + txtRevisionNo.Text.Trim() +").doc";
            //        objViewReport.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            //        objViewReport.ExportOptions.ExportFormatType = ExportFormatType.WordForWindows;
            //        DiskFileDestinationOptions objOptions = new DiskFileDestinationOptions();
            //        objOptions.DiskFileName = Server.MapPath(".")+ "\\XLFile\\"+strExportFile;
            //        objViewReport.ExportOptions.DestinationOptions = objOptions;
            //        //rptExcel.ExportToDisk(ExportFormatType.WordForWindows,strExportFile);
            //        objViewReport.ExportToStream(ExportFormatType.WordForWindows);
            //        objViewReport.Export();
            //        objOptions = null;
            //        objViewReport = null;					
            //        Response.Redirect("XLFile\\"+strExportFile);				
            //        //Response.Write("<script>window.close();</script>");
            //    }
            //    else
            //    {
            //        cbApprove.Checked=false;
            //        lblError.Text="Wrong password";
            //    }
            //}
            //else
            //{
            //    /*DateTime IssuDt;
            //    IssuDt=Convert.ToDateTime(txtIssueDate.Text);*/
            //    //Response.Write("<script>opener.location.href='Output.aspx?Model="+Request.QueryString["Model"].ToString()+"&RefNo="+Request.QueryString["RefNo"].ToString() +"&OpenFrom=sig&OutputType=1&RevisionNo="+ txtRevisionNo.Text +"&IssueName="+txtIssueName.Text+"&IssueDate="+txtIssueDate.Text+"&CheckName="+txtCheckName.Text+"&CheckDate="+ txtCheckDate.Text+"&ApproverName= &ReportFor=NA';window.close();</script>");
            //    ReportClass objViewReport = null;
            //    string strsql = "";
            //    SqlConnection objConn = new SqlConnection();
            //    SqlDataAdapter objDa;
            //    DataSet objDs; 
            //    objConn.ConnectionString = ConfigurationSettings.AppSettings["SQLConn4Rpt"].ToString();
            //    objConn.Open();
            //    /*if (Request.QueryString["OpenFrom"]==null)//|| Request.QueryString["OpenFrom"].ToString().ToUpper()=="SIG")
            //        strsql = "Exec SP_RepModelExplorer "+Request.QueryString["RefNo"].ToString()+",1"; //Request.QueryString["StrSql"];				
            //    else*/
            //        strsql = "Exec SP_RepModelExplorer "+Request.QueryString["RefNo"].ToString()+",1"; //Request.QueryString["StrSql"];
            //    objDa = new SqlDataAdapter(strsql, objConn);
            //    objDs = new DataSet();
            //    objDa.Fill(objDs, "repDS");	

            //    objViewReport = new repModelExplorer();
            //    objViewReport.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4;			
            //    //objViewReport.PrintOptions.PrinterName="CutePDF Writer";
			
            //    //objViewReport.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
            //    ParameterDiscreteValue pRefNo = new ParameterDiscreteValue();
			
            //    /*if (Request.QueryString["OpenFrom"]==null)
            //        pRefNo.Value =  Session["RefNo"].ToString();				
            //    else*/
            //        pRefNo.Value = Request.QueryString["RefNo"].ToString();

            //    objViewReport.SetParameterValue("@RefNo", pRefNo);

            //    ParameterDiscreteValue pOutputType=new ParameterDiscreteValue();
            //    pOutputType.Value=Request.QueryString["OutputType"].ToString();
            //    objViewReport.SetParameterValue("@OutputType",pOutputType);

            //    ParameterDiscreteValue pRevisionNo=new ParameterDiscreteValue();
            //    pRevisionNo.Value=txtRevisionNo.Text.Trim();
            //    objViewReport.SetParameterValue("RevisionNo",pRevisionNo);
			
            //    ParameterDiscreteValue pIssueName=new ParameterDiscreteValue();
            //    pIssueName.Value=txtIssueName.Text.Trim();
            //    objViewReport.SetParameterValue("IssueName",pIssueName);

            //    ParameterDiscreteValue pIssueDate=new ParameterDiscreteValue();
            //    pIssueDate.Value=txtIssueDate.Text.Trim();
            //    objViewReport.SetParameterValue("IssueDate",pIssueDate);

            //    ParameterDiscreteValue pCheckName=new ParameterDiscreteValue();
            //    pCheckName.Value=txtCheckName.Text.Trim();
            //    objViewReport.SetParameterValue("CheckName",pCheckName);

            //    ParameterDiscreteValue pCheckDate=new ParameterDiscreteValue();
            //    pCheckDate.Value=txtCheckDate.Text.Trim();
            //    objViewReport.SetParameterValue("CheckDate",pCheckDate);

            //    ParameterDiscreteValue pApproverName=new ParameterDiscreteValue();
            //    pApproverName.Value="";
            //    objViewReport.SetParameterValue("ApprovedName",pApproverName);

            //    ParameterDiscreteValue pReportFor=new ParameterDiscreteValue();
            //    pReportFor.Value="NA";
            //    objViewReport.SetParameterValue("ReportFor",pReportFor);

			
            //    TableLogOnInfo Loginfo = new TableLogOnInfo();
            //    foreach(CrystalDecisions.CrystalReports.Engine.Table oTable in objViewReport.Database.Tables)
            //    {
            //        Loginfo = oTable.LogOnInfo;
            //        Loginfo.ConnectionInfo.ServerName = ConfigurationSettings.AppSettings["ReportServer"]; 
            //        Loginfo.ConnectionInfo.DatabaseName = ConfigurationSettings.AppSettings["ReportDB"]; 
            //        Loginfo.ConnectionInfo.UserID = ConfigurationSettings.AppSettings["ReportUserID"]; 
            //        Loginfo.ConnectionInfo.Password = ConfigurationSettings.AppSettings["ReportPassword"]; 
            //        Loginfo.TableName = oTable.Name;
            //        oTable.ApplyLogOnInfo(Loginfo);
            //    }
            //    /*if (Request.QueryString["OpenFrom"]==null)
            //        {
            //            CrystalReportViewer1.HasPrintButton=true;
            //            CrystalReportViewer1.HasExportButton=false;
            //        }
            //        else
            //        {
            //            CrystalReportViewer1.HasPrintButton=true;
            //            CrystalReportViewer1.HasExportButton=true;
            //        }*/

            //    //CrystalReportViewer1.ReportSource = objViewReport;
            //    //CrystalReportViewer1.DataBind();				

            //    //string strExportFile = (Server.MapPath(".") + "/XLFile/CNWExport.doc");
            //    string strExportFile = BLL.MainScreen.GetContractNoForXL(Convert.ToInt32(Request.QueryString["RefNo"]),Convert.ToInt32(ConfigurationSettings.AppSettings["PROJECT NO"]),Convert.ToInt32(ConfigurationSettings.AppSettings["CLIENT LIFT NO"]))+ "(" + txtRevisionNo.Text.Trim() +").doc";
            //    objViewReport.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            //    objViewReport.ExportOptions.ExportFormatType = ExportFormatType.WordForWindows;
            //    DiskFileDestinationOptions objOptions = new DiskFileDestinationOptions();
            //    objOptions.DiskFileName = Server.MapPath(".")+ "\\XLFile\\"+strExportFile;
            //    objViewReport.ExportOptions.DestinationOptions = objOptions;
            //    //rptExcel.ExportToDisk(ExportFormatType.WordForWindows,strExportFile);
            //    objViewReport.ExportToStream(ExportFormatType.WordForWindows);
            //    objViewReport.Export();
            //    objOptions = null;
            //    objViewReport = null;				
            //    Response.Redirect("XLFile\\"+strExportFile,true);				
            //    //Response.Write("<script>window.close();</script>");
            //}			
		}
		private void GetHeadPassword()
		{
			DataTable dtHeadPass=new DataTable();
			dtHeadPass=BLL.Signature.GetHeadPassword(Session["DeptCode"].ToString()).Tables[0];
			ViewState["Head"]=dtHeadPass.Rows[0][0].ToString();
			ViewState["Pass"]=dtHeadPass.Rows[0][1].ToString();			
		}
	}
}
