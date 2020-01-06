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
using System.Configuration;
using System.Data.SqlClient;

namespace NewOrderingSystem
{
	/// <summary>
	/// Summary description for PCESInteface.
	/// </summary>
	public partial class PCESInteface : System.Web.UI.Page
	{
		
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
		string Model;
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			InitProperties();
			if (!IsPostBack)
			{
				PopulateWBSElement();
			}
		}
		private void InitProperties()
		{					
			Model=Request.QueryString["LIFT MODEL"].ToString();	
		}
		private void PopulateWBSElement()
		{
			DataTable dtWBSElement=new DataTable();			
			string JobLocation=PCESInterface.GetFunctionalLocation(Convert.ToInt32(Session["RefNo"]),Convert.ToInt32(ConfigurationSettings.AppSettings["PROJECT LOCATION"]));
			lblCountryCode.Text=JobLocation;
			dtWBSElement = PCESInterface.GetWBSElementForCNW(JobLocation,Model).Tables[0];
			if (dtWBSElement.Rows.Count>0)
			{
				ddlWBSElement.DataSource=dtWBSElement;
				ddlWBSElement.DataTextField="WBSElement";
				ddlWBSElement.DataValueField="WBSElement";
				ddlWBSElement.DataBind();
				ddlWBSElement.Items.Insert(0,new ListItem("Select WBSElement","Select WBSElement"));
				ddlWBSElement.SelectedIndex=0;
			}
			else
			{
				lblError.Text="No WBSElement found for selected Country and Model";
			}
		}

		protected void btnConfirm_Click(object sender, System.EventArgs e)
		{
			if (ddlWBSElement.SelectedIndex <1)
			{
				lblError.Text="Please Select WBSElement";
				return;
			}
			//DataTable dsTemplate ;
			DataTable dt=BLL.MainScreen.GetModelExplorer(Convert.ToInt32(Session["RefNo"]),"3").Tables[0];
			string Result="";
			for (int i=0;i<=dt.Rows.Count-1;i++)
			{
				try
				{	
					BLL.PCESInterface.UpdateParmValue(ddlWBSElement.SelectedValue.ToString(),0,dt.Rows[i]["DisplayName"].ToString() ,dt.Rows[i]["ParmValue"].ToString(),1,Session["UserName"].ToString(),"A",Convert.ToInt32(hidVersionNo.Value));
					//BLL.PCESInterface.InterfaceExternalApplication(dt.Rows[i]["ParmDescription"].ToString(),dt.Rows[i]["ParmValue"].ToString(),ddlWBSElement.SelectedValue.ToString()+Session["UserID"].ToString());
					Result="Success";
					//dsTemplate = BLL.PCESInterface.GetExternalApplication(1,Model,ddlWBSElement.SelectedValue.ToString()+Session["UserID"].ToString());
				}
				catch(SqlException sqEx)
				{
					lblError.Text += "SQL Error : "+sqEx.Message; 
					//Result="Error";
					//return;
				}
				catch(Exception Ex)
				{
					lblError.Text += "Error : "+Ex.Message; 
					//Result="Error";
					//return;
				}
			}
			if (Result=="Success")
			{
				lblError.Text = "Interface Successful";
			}
		}

		protected void ddlWBSElement_SelectedIndexChanged(object sender, System.EventArgs e)
		{			
			hidVersionNo.Value= BLL.PCESInterface.GetVersionNo(ddlWBSElement.SelectedValue.ToString(),1);
		}
	}
}
