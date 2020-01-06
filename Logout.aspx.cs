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

namespace NewOrderingSystem
{
	/// <summary>
	/// Summary description for Logout.
	/// </summary>
	public partial class Logout : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblMsg;
        //protected C1.Web.C1WebGrid.C1WebGrid C1WebGrid1;		
		DataSet dsLogout=new DataSet();

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

		}
		#endregion

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if (Session["UserId"]==null)
				Response.Redirect("Login.aspx");	

			//if (!IsRecordToSave())
			//{
				//BLL.Logout.DeleteSavedTempContract(Session["UserID"].ToString());
				dsLogout = BLL.General.Logout(Convert.ToInt32(Session["LogNo"]));
                //dgLogout.DataSource=dsLogout;
                //dgLogout.DataBind();
				Session.Abandon();			
			//}
		}
		/*private bool IsRecordToSave()
		{
			try
			{				
				string Result=BLL.Logout.IsRecordToSave(Session["UserID"].ToString());
				if (Result.ToUpper()=="RecordExits".ToUpper())
				{
					Response.Write("<Script>alert('Some records still to SAVE,Please SAVE or DELETE from viewoutput before logout');window.location='ViewOutput.aspx'</Script>");
					return true;
				}
				else
				{
					return false;
				}
			}
			catch(SqlException sqEx)
			{
				lblError.Text="SQL Error : " + sqEx.Message;
				return true;
			}
			catch(Exception Ex)
			{
				lblError.Text="Error : " + Ex.Message;
				return true;
			}
		}*/
	}
}
