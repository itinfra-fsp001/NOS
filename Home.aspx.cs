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

namespace NewOrderingSystem
{
	/// <summary>
	/// Summary description for Home.
	/// </summary>
	public class Home : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox txtMultiline;
		protected System.Web.UI.WebControls.Label lblUserName;
		protected System.Web.UI.WebControls.LinkButton lbkBtnChangePassword;
		protected System.Web.UI.WebControls.Label lblError;
        protected System.Web.UI.WebControls.LinkButton lnkBtnVersion;
		private  DataTable dtLogin;
		private void Page_Load(object sender, System.EventArgs e)
		{
			/*Response.ContentType= "application/ms-excel";
			Response.AddHeader("Content-disposition","attachment;filename=TestContract-L10.xls");*/
				
			// Put user code to initialize the page here
			if (Request.QueryString["UserID"]!=null)
			{
				if (Request.QueryString["UserID"].ToString()!="")
				{
					dtLogin=BLL.Login.GetUserDetails(Request.QueryString["UserID"].ToString()).Tables[0];
					if (dtLogin.Rows.Count>0)
					{
						Session["UserId"] = dtLogin.Rows[0].ItemArray[0].ToString();
						Session["UserName"] = dtLogin.Rows[0].ItemArray[1].ToString();
						Session["UserEmail"] = dtLogin.Rows[0].ItemArray[3].ToString();
						Session["CountryCode"] = dtLogin.Rows[0].ItemArray[4].ToString();
						Session["DeptCode"] = dtLogin.Rows[0].ItemArray[5].ToString();
						Session["RoldID"] = dtLogin.Rows[0].ItemArray[6].ToString();
						Session["IsAccountLocked"] = dtLogin.Rows[0].ItemArray[7].ToString();
						Session["LogNo"]= BLL.Login.InsertLogHistory("U",Session["UserId"].ToString(),"","");
					}
					else
					{
						lblError.Text="You dont have permission for this site, Please contact administrator";
						Session["UserId"] =null;
						return;					
					}
				}
			}
			if (Session["UserId"]==null)
				Response.Redirect("Login.aspx");
			//InitProperties();				
			if(!IsPostBack)
			{
				lblUserName.Text= "WELCOME " + Session["UserName"].ToString();	
			
			}
			lbkBtnChangePassword.Attributes.Add("OnClick","OpenWindow('ChangePassword.aspx?FromPage=Document');");
			txtMultiline.Attributes.Add("onKeyPress", "MultilineTextControl('txtMultiline',50);");
            lnkBtnVersion.Text = ConfigurationSettings.AppSettings["NOSVersion"].ToString();

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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
