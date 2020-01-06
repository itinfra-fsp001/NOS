using System;
using System.Data;
using System.Data.SqlClient;
using SQLHelper.DataAccessLayer;
using System.Configuration;

namespace NewOrderingSystem.BLL
{
	/// <summary>
	/// Summary description for PCESInterface.
	/// </summary>
	public class PCESInterface
	{
		public PCESInterface()
		{}
		public static string GetFunctionalLocation(Int32 RefNo,int ParmCode)
		{
			return SqlHelper.ExecuteScalar(ConfigurationSettings.AppSettings["SQLConn"],"SP_GetFunctionalLocation",RefNo,ParmCode).ToString();
		}
		public static DataSet GetWBSElementForCNW(string JobLocation,string Model)
		{
			return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["PCESSQLConn"],"SP_GetWBSElementForCNW",JobLocation,Model);
		}
		public static void UpdateParmValue(string WBSElement, int GroupCode,string ParmDesc,string ParmValue,int SourceCode,string LastUpdateBy, string UserAction,int VersionNO)
		{
			SqlHelper.ExecuteNonQuery(ConfigurationSettings.AppSettings["PCESSQLConn"],"SP_UpdateParmValueForCNW",WBSElement,GroupCode,ParmDesc,ParmValue,SourceCode,LastUpdateBy,UserAction,VersionNO);
		}
		public static void InterfaceExternalApplication(string CharDesc, string CharValue,string TableName)
		{
			SqlHelper.ExecuteNonQuery(ConfigurationSettings.AppSettings["PCESSQLConn"],"SP_InterfaceExternalApplication", CharDesc,CharValue,TableName);
		}
		public static DataSet GetExternalApplication(int EquipmentCode, string ModelType,string TableName)
		{
			return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["PCESSQLConn"],"SP_GetExternalApplication", EquipmentCode,ModelType,TableName);
		}
		public static string GetVersionNo(string WBSElement,int SourceCode)
		{
			return (SqlHelper.ExecuteScalar(ConfigurationSettings.AppSettings["PCESSQLConn"],"SP_GetVersionNO",WBSElement,SourceCode)).ToString();
		}
	}
}
