using System;
using System.Data;
using SQLHelper.DataAccessLayer;
using System.Configuration;

namespace NewOrderingSystem.BLL
{
	/// <summary>
	/// Summary description for Copy.
	/// </summary>
	public class Copy
	{
		public Copy()
		{}
		public static string CopyContract(int FromRefNo, string ToContractNo, string	ToLiftNo, string CopyBy)
		{
			return SqlHelper.ExecuteScalar(ConfigurationSettings.AppSettings["SQLConn"],"SP_CopyContract",FromRefNo,ToContractNo,ToLiftNo,CopyBy).ToString();
		}
		public static DataSet GetContractNoLiftNo(Int32 RefNo, int ContractNoParmCode, int LiftNoParmCode)
		{
			return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"],"SP_GetContractNoLiftNo",RefNo,ContractNoParmCode,LiftNoParmCode);
		}
	}
}
