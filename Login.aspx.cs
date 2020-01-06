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
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace NewOrderingSystem
{
	/// <summary>
	/// Summary description for WebForm1.
	/// </summary>
	public class Login : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.LinkButton  btnCancel;
        protected System.Web.UI.WebControls.LinkButton btnSubmit;
		protected System.Web.UI.WebControls.TextBox txtPassword;
		protected System.Web.UI.WebControls.TextBox txtUserName;
		protected System.Web.UI.WebControls.Label lblError;
        protected System.Web.UI.WebControls.Label lblVersion;
		StringBuilder strBuilder=new StringBuilder();
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				Session.Abandon();
			}
			// Put user code to initialize the page here			
			/*if (!Page.IsPostBack)
			{
				mnuMain.DataSource = Server.MapPath("MenuItems.xml");
				mnuMain.DataBind();
				Menu1.DataSource = Server.MapPath("MenuItems.xml");
				Menu1.DataBind();
			}*/
			//Response.Write("<script>document.form1.txtUserName.focus()</script>");			
			/*strBuilder.Append("<script language='javascript'>");
			strBuilder.Append("document.getElementById('txtUserName').focus()");
			strBuilder.Append("</script>");
            
			RegisterStartupScript("Focus",strBuilder.ToString());*/
			RegisterStartupScript("Focus","<script language='javascript'> document.getElementById('txtUserName').focus();</script>");
            //RegisterStartupScript("Focus", "<script language='javascript'>document.Form1.txtUserName.value='SG5072';document.Form1.txtPassword.value='SG5072'; document.Form1.txtUserName.focus();</script>");
            this.lblVersion.Text = ConfigurationSettings.AppSettings["NOSVersion"].ToString();
		}

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
			this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);            
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			this.Load += new System.EventHandler(this.Page_Load);
            //Added for Enter Key to raise the submit option on 16/Oct/2014
            this.Page.Form.DefaultButton = this.btnSubmit.UniqueID ;
            this.Page.Form.DefaultFocus = this.txtUserName.UniqueID;

		}
		#endregion

		private void btnSubmit_Click(object sender, System.EventArgs e)
		{
			DataSet dsLogin,dsBackendAccess;
			lblError.Text="";
			try
			{				
				if (txtUserName.Text.Trim() == string.Empty)
				{
					lblError.Text = "UserName cannot be empty";
                    txtUserName.Focus();
					return;
				}
				if (txtPassword.Text.Trim() == string.Empty)
				{
					lblError.Text = "Password cannot be empty";
                    txtPassword.Focus();
					return;
				}

				dsLogin = BLL.Login.GetUserDetails(txtUserName.Text.Trim(), txtPassword.Text.Trim());
				dsBackendAccess = BLL.Login.IsBackendAccess();
				
				if (ConfigurationSettings.AppSettings["Environment"].ToUpper()=="PRD")
				{
					if (dsBackendAccess.Tables[0].Rows[0].ItemArray[0].ToString() == "Yes")
					{
						lblError.Text = "Sorry,Datatransfer is being process, please try again later";
						return;
					}
				}

				if (dsLogin.Tables[0].Rows[0].ItemArray[8].ToString().Trim() =="Wrong User")
				{
					lblError.Text="Wrong User";
                    txtUserName.Focus();
				}
				else if (dsLogin.Tables[0].Rows[0].ItemArray[8].ToString().Trim() =="Wrong Password")
				{
					lblError.Text="Wrong Password";
                    txtPassword.Focus();
				}
				else if (dsLogin.Tables[0].Rows[0].ItemArray[8].ToString().Trim() =="First Login")
				{  					
					Session["UserId"] = txtUserName.Text.Trim();
					//Response.Write("<script>newwindow=window.open('ChangePassword.aspx?FromPage=Login','ChangePassword','width=470,height=250,left=250,top=200,resizable=no,scrollbars=no,model=yes');if (window.focus()) {newwindow.focus()};</script>");					
					RegisterStartupScript("ChangePass","<script>newwindow=window.open('ChangePassword.aspx?FromPage=Login','ChangePassword','width=470,height=250,left=250,top=200,resizable=no,scrollbars=no');if (window.focus) newwindow.focus();</script>");
				}
				else if (dsLogin.Tables[0].Rows[0].ItemArray[8].ToString().Trim() =="Correct User")
				{
					Session["UserId"] = dsLogin.Tables[0].Rows[0].ItemArray[0].ToString();
					Session["UserName"] = dsLogin.Tables[0].Rows[0].ItemArray[1].ToString();
					Session["UserEmail"] = dsLogin.Tables[0].Rows[0].ItemArray[3].ToString();
					Session["CountryCode"] = dsLogin.Tables[0].Rows[0].ItemArray[4].ToString();
					Session["DeptCode"] = dsLogin.Tables[0].Rows[0].ItemArray[5].ToString();
					Session["RoldID"] = dsLogin.Tables[0].Rows[0].ItemArray[6].ToString();
					Session["IsAccountLocked"] = dsLogin.Tables[0].Rows[0].ItemArray[7].ToString();
					Session["LogNo"]= BLL.Login.InsertLogHistory("U",Session["UserId"].ToString(),"","");
					Response.Redirect("Home.aspx");
				}
			}
			catch(SqlException sqEx)
			{
				lblError.Text ="Error " + sqEx.Number + " "+ sqEx.Message;
			}
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			lblError.Text="";
			txtUserName.Text="";
			txtPassword.Text="";
		}

        
	}
}
