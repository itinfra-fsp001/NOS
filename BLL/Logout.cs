using System;
using System.Data;
using SQLHelper.DataAccessLayer;
using System.Configuration;

namespace NewOrderingSystem.BLL
{
	/// <summary>
	/// Summary description for Logout.
	/// </summary>
	public class Logout
	{
		public Logout()
		{}
		/*public static string IsRecordToSave(string UserID)
		{
			return SqlHelper.ExecuteScalar(ConfigurationSettings.AppSettings["SQLConn"],"SP_IsRecordToSave",UserID).ToString();
		}
		public static void DeleteSavedTempContract(string UserID)
		{
			SqlHelper.ExecuteNonQuery(ConfigurationSettings.AppSettings["SQLConn"],"SP_DeleteSavedTempContract",UserID);
		}*/
	}
}
