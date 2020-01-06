using System;
using System.Configuration;
using SQLHelper.DataAccessLayer;

namespace NewOrderingSystem.BLL
{
	/// <summary>
	/// Summary description for ChangePassword.
	/// </summary>
	public class ChangePassword
	{
		public ChangePassword()
		{		
			// TODO: Add constructor logic here		
		}
		public static string ChangePasswrod(string UserID,string Passwrod, string NewPassword)
		{
			return (SqlHelper.ExecuteScalar(ConfigurationSettings.AppSettings["SQLConn"],"SP_ChangePasswrod", UserID,Passwrod,NewPassword).ToString());			 							
		}
	}
}
