using System;
using System.Configuration;
using SQLHelper.DataAccessLayer;
using System.Data; 

namespace NewOrderingSystem.BLL
{	
	public class Signature
	{
		public Signature()
		{}
		public static DataSet GetHeadPassword(string DeptCode)
		{
			return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"],"SP_GetHeadPassword",DeptCode);
		}		
	}
}
