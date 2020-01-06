using System;
using System.Configuration;
using SQLHelper.DataAccessLayer;
using System.Data;
using System.IO;

namespace NewOrderingSystem.BLL
{
	/// <summary>
	/// Summary description for General.
	/// </summary>
	public class General
	{
		public General()
		{}
		public static DataSet GetCountry(string CountryCode )
		{
			return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"],"SP_GetCountry", CountryCode );
		}
		public static DataSet GetSpecHistory(string WBSElement,int SourceCode )
		{
			return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"],"SP_GetSpecHistory", WBSElement,SourceCode );
		}
		public static DataSet Logout(int LogNo)
		{
			return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"],"SP_Logout",LogNo);
		}
		public static DataSet GetCostReport(string WBSElement)
		{
			return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"],"SP_RepPCESCostReport",WBSElement);
		}
		public static DataSet GetCostDetails(string WBSElement, string ProjDef)
		{
			return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"],"SP_GetCostDetails",WBSElement,ProjDef);
		}
		public static DataSet GetComputeStatus()
		{
			return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"],"SP_GetComputeStatus");
		}
        public static void WriteSQLQueryToText(string sQuery)
        {
            using (StreamWriter writer = new StreamWriter("log.txt",true))
            {
                writer.WriteLine(sQuery);
            }

        }
        public static DataTable GetPartNo4RevActivate(int nRefNo)
        {
            return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"], "SP_GetPartNo4RevActivate", nRefNo).Tables[0];
        }

	}
}
