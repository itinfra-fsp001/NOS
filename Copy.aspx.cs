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
namespace NewOrderingSystem
{
	/// <summary>
	/// Summary description for Copy.
	/// </summary>
	public class Copy : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label lblTo;
		protected System.Web.UI.WebControls.Label lblFrom;
		protected System.Web.UI.WebControls.TextBox txtLiftNo;
		protected System.Web.UI.WebControls.TextBox txtContractNo;
		protected System.Web.UI.WebControls.Label lblContractNo;
		protected System.Web.UI.WebControls.Button btnCopy;
		protected System.Web.UI.WebControls.Label lblError;
		protected System.Web.UI.WebControls.Label lblMsg;
		protected System.Web.UI.WebControls.Label lblLiftNo;
		protected System.Web.UI.WebControls.Label Label6;
			
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
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion

		DataSet dsConLift;
		private void Page_Load(object sender, System.EventArgs e)
		{
			InitProperties();
		}
		private void InitProperties()
		{
			//lblContractNo.Text=Request.QueryString["ContractNo"].ToString();
			PopulateContractNoLiftNo();
			btnCopy.Attributes.Add("Onclick","return CanCopy();");
		}
		 
		private void CopyContract()
		{
			string Result;
			lblError.Text="";
			try	
			{
									
				if (txtContractNo.Text.IndexOf(' ')>=0 || txtContractNo.Text.IndexOf('&')>=0 || txtContractNo.Text.IndexOf('/')>=0 || txtContractNo.Text.IndexOf(':')>=0 || txtContractNo.Text.IndexOf('*')>=0 || txtContractNo.Text.IndexOf('?')>=0 || txtContractNo.Text.IndexOf('"')>=0 || txtContractNo.Text.IndexOf('<')>=0 || txtContractNo.Text.IndexOf('>')>=0 || txtContractNo.Text.IndexOf('|')>=0 || txtContractNo.Text.IndexOf('\\')>=0)
				{
					lblError.Text += "<font color=#800080> Contrat No </font> should not contains space,/,\\,:,*,?,<,>,|,& and double quotes; <BR>";
				}											
				if (txtLiftNo.Text.IndexOf(' ')>=0 || txtLiftNo.Text.IndexOf('&')>=0 || txtLiftNo.Text.IndexOf('/')>=0 || txtLiftNo.Text.IndexOf(':')>=0 || txtLiftNo.Text.IndexOf('*')>=0 || txtLiftNo.Text.IndexOf('?')>=0 || txtLiftNo.Text.IndexOf('"')>=0 || txtLiftNo.Text.IndexOf('<')>=0 || txtLiftNo.Text.IndexOf('>')>=0 || txtLiftNo.Text.IndexOf('|')>=0 || txtLiftNo.Text.IndexOf('\\')>=0)
				{
					lblError.Text += "<font color=#800080>Lift No </font> should not contains space,/,\\,:,*,?,<,>,|,& and double quotes; <BR>";
				}	
				if (lblError.Text=="")
				{
					Result=BLL.Copy.CopyContract(Convert.ToInt32(Request.QueryString["RefNo"].ToString()),txtContractNo.Text.Trim(),txtLiftNo.Text.Trim(),Session["UserName"].ToString());
					if (Result.ToUpper()=="EXIST")
					{
						lblError.Text="ContractNo is already exists, please select another...";
					}
					else
					{												
						Response.Write("<Script>alert('Copy Successful');window.opener.location.href='ViewOutput.aspx';self.close();</Script>");						
					}
				}
			}
			catch(SqlException sqEx)
			{
				lblError.Text="SQL Error : " + sqEx.Message;
			}
			catch(Exception Ex)
			{
				lblError.Text="Error : " + Ex.Message;
			}

		}
		private void PopulateContractNoLiftNo()
		{ 
			dsConLift=new DataSet();
            dsConLift = BLL.Copy.GetContractNoLiftNo(Convert.ToInt32(Request.QueryString["RefNo"]), Convert.ToInt32(ConfigurationSettings.AppSettings["PROJECT NO"]), Convert.ToInt32(ConfigurationSettings.AppSettings["EQUIPMENT NO"]));
			lblContractNo.Text=dsConLift.Tables[0].Rows[0][0].ToString();
			lblLiftNo.Text =dsConLift.Tables[1].Rows[0][0].ToString();
		}

        protected void btnCopy_Click(object sender, EventArgs e)
        {
            CopyContract();
        }   
	}
}
