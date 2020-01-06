using System;
using System.Data;
using SQLHelper.DataAccessLayer;
using System.Configuration; 

namespace NewOrderingSystem.BLL
{
	/// <summary>
	/// Summary description for Login.
	/// </summary>
	public class Login
	{		
		public Login()
		{
			// TODO: Add constructor logic here
		}
		//Get the user details, if the username or password is wrong it will return 'wrong user' or 'wrong Password'
		public static DataSet GetUserDetails(string UserName , string Password) 
		{
			return SqlHelper.ExecuteDataset( ConfigurationSettings.AppSettings["SQLConn"],"SP_GetUserByUserID",UserName,Password);
		}
		public static DataSet GetUserDetails(string UserID) 
		{
			return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"],"SP_GetUserList",UserID);
		}		
		public static DataSet IsBackendAccess()
		{					
			return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"],"SP_IsBackendAccess");
		}
		public static string InsertLogHistory(string LogType,string LoginBy, string StatusCode,string Reference)
		{
			return (SqlHelper.ExecuteScalar(ConfigurationSettings.AppSettings["SQLConn"],"SP_InsertLogHistory",LogType, LoginBy, StatusCode,Reference).ToString());
		}
	}
}
