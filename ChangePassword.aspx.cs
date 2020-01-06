using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace NewOrderingSystem
{
	/// <summary>
	/// Summary description for Document.
	/// </summary>
	public class ChangePasswrod : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblCountry;
		protected System.Web.UI.WebControls.Label lblEquipment;
		protected System.Web.UI.WebControls.Label lblModel;
		protected System.Web.UI.WebControls.Label lblDocumentType;
		protected System.Web.UI.WebControls.Label lblDocumentNo;
		protected System.Web.UI.WebControls.DropDownList ddlCountry;
		protected System.Web.UI.WebControls.DropDownList ddlEquipment;
		protected System.Web.UI.WebControls.DropDownList ddlModel;
		protected System.Web.UI.WebControls.DropDownList ddlDocumentType;
		protected System.Web.UI.WebControls.TextBox txtDocumentNo;
		protected System.Web.UI.WebControls.DropDownList ddlDocumentNo;
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected System.Web.UI.WebControls.RangeValidator RangeValidator1;
		protected System.Web.UI.WebControls.TextBox Textbox2;
		protected System.Web.UI.WebControls.TextBox txtNewPassword;
		protected System.Web.UI.WebControls.Button btnChange;
		protected System.Web.UI.WebControls.TextBox txtOldPasswrod;
		protected System.Web.UI.WebControls.Label lblError;
		protected System.Web.UI.WebControls.Label lblMsg;
		protected System.Web.UI.WebControls.TextBox txtReEnterPasswrod;
		
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
			this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion			

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (Session["UserId"]==null)
				Response.Write("<script>self.close();</script>");
				InitProperties();	
			RegisterStartupScript("Focus","<script language='javascript'> document.Form1.txtOldPasswrod.focus();</script>");
		}
		private void InitProperties()
		{
			if (Request.QueryString["FromPage"].ToString()=="Login")
				lblMsg.Visible=true;
			else
				lblMsg.Visible=false;
			lblError.Text="";
			//btnCancel.Attributes.Add("OnClick","CloseWindow();");
			btnChange.Attributes.Add("OnClick","return IsRePasswordCorrect()");
		}

		private void btnChange_Click(object sender, System.EventArgs e)
		{
			lblError.Text = BLL.ChangePassword.ChangePasswrod(Session["UserID"].ToString(),txtOldPasswrod.Text.Trim(),txtNewPassword.Text.Trim());
			if (lblError.Text=="Password Changed")
			{	
				Response.Write("<script>alert('Password Changed Successfully');self.close();window.opener.document.forms[0].submit();</script>");
				if (Request.QueryString["FromPage"].ToString()=="Login")
				{
					try
					{
						BLL.Login.InsertLogHistory("U",Session["UserId"].ToString(),"ED","");
					}
					catch(SqlException sqEx)
					{
						lblError.Text="Error : " + sqEx.Message;
					}
				}
				Session.Clear();
				//Response.Write("<body><script>window.opener.location.reload();window.close();</script></body>");				
				//Response.Write("<body><script>window.close();window.location='Login.aspx'</script></body>");				
			}
		}
	}
}
