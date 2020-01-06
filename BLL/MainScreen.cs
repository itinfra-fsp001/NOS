using System;
using System.Data;
using SQLHelper.DataAccessLayer;
using System.Configuration;

namespace NewOrderingSystem.BLL
{
    /// <summary>
    /// Summary description for MainScreen.
    /// </summary>
    public class MainScreen
    {
        public MainScreen()
        { }
        //		public static DataSet GetScreenTemplate(string ModelType,int Optional,int HideFlag,Int32 RefNo)
        //		{
        //			return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"],"SP_GetScreenTemplate",ModelType,Optional,HideFlag,RefNo);
        //		}
        public static DataSet GetScreenTemplate(string ModelType, string ContractNo, Int16 SpecID)
        {
            return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"], "SP_GetScreenTemplateForModel", ModelType, ContractNo, SpecID);
        }
        public static DataSet FetchParmValues(int ParmCode, string ModelType)
        {
            ////return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"],"SP_FetchParmValues",ParmCode,ModelType);
            return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"], "SP_GetParmValuesForModel", ParmCode, ModelType);
        }
        public static string GetDefault(int ParmCode, string ModelType)
        {
            return SqlHelper.ExecuteScalar(ConfigurationSettings.AppSettings["SQLConn"], "SP_GetDefault", ParmCode, ModelType).ToString();
        }
        /*public static int InsertTempMaster(int RefNo, string LastUpdateBy, string CountryCode,string DeptCode,string ContractNo,string Mode,string ModelType,string SubmitBy)
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConfigurationSettings.AppSettings["SQLConn"],"SP_InsertTempMaster",RefNo,LastUpdateBy,CountryCode,DeptCode,ContractNo,Mode,ModelType,SubmitBy));
        }*/
        /*public static int UpdateTempMaster(Int32 RefNo, string LastUpdateBy, string CountryCode,string DeptCode,string ContractNo,string ModelType)
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConfigurationSettings.AppSettings["SQLConn"],"SP_UpdateTempMaster",RefNo,LastUpdateBy,CountryCode,DeptCode,ContractNo,ModelType));
        }*/
        public static int UpdateTempMaster(Int32 RefNo, string ContractNo, string ModelType, Int32 NoOfEquip, string UpdatedBy)
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConfigurationSettings.AppSettings["SQLConn"], "SP_UpdateTempMaster", RefNo, ContractNo, ModelType, NoOfEquip, UpdatedBy));
        }

        public static void InsertTempSpec(Int32 RefNo, int ParmCode, string ParmValue, int DisplaySeq, string ModelType, bool IsValueTable, bool IsNonStd)
        {
            SqlHelper.ExecuteNonQuery(ConfigurationSettings.AppSettings["SQLConn"], "SP_InsertTempSpec", RefNo, ParmCode, ParmValue, DisplaySeq, ModelType, IsValueTable,IsNonStd);
        }

        public static void UpdateTempSpec(Int32 RefNo, int ParmCode, string ParmValue, string ResultValue, int DisplaySeq, string ModelType, bool IsValueTable, string Action)
        {
            SqlHelper.ExecuteNonQuery(ConfigurationSettings.AppSettings["SQLConn"], "SP_UpdateTempSpec", RefNo, ParmCode, ParmValue, ResultValue, DisplaySeq, ModelType, IsValueTable, Action);
        }
        public static void UpdateTempResult(int RefNo, int NodeID, int FunctionID, int ParmCode, string ParmValue, string Action)
        {
            SqlHelper.ExecuteNonQuery(ConfigurationSettings.AppSettings["SQLConn"], "SP_UpdateTempResult", RefNo, NodeID, FunctionID, ParmCode, ParmValue, Action);
        }

        public static void UpdateStatuTempMaster(int TempRefNo, string Status, string UpdatedBy)
        {
            SqlHelper.ExecuteNonQuery(ConfigurationSettings.AppSettings["SQLConn"], "SP_UpdateStatuTempMaster", TempRefNo, Status, UpdatedBy);
        }
        public static void UpdateStatuForCompute(int TempRefNo, string Status, string SubmitBy)
        {
            SqlHelper.ExecuteNonQuery(ConfigurationSettings.AppSettings["SQLConn"], "SP_UpdateStatuForCompute", TempRefNo, Status, SubmitBy);
        }
        public static string GetStatuTempMaster(Int32 RefNo)
        {
            return SqlHelper.ExecuteScalar(ConfigurationSettings.AppSettings["SQLConn"], "SP_GetStatuTempMaster", RefNo).ToString();
        }
        //public static DataSet GetContractNo(string CountryCode, string DeptCode)
        //{
        //    return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"], "SP_GetContractNo", CountryCode, DeptCode);
        //}
        public static DataSet FetchTempSpec(Int32 RefNo, int IsForLoad, string ModelType, int Optional, int HideFlag)
        {
            return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"], "SP_FetchTempSpec", RefNo, IsForLoad, ModelType, Optional, HideFlag);
        }
        public static DataSet GetModelExplorer(Int32 RefNo, string OutputType)
        {
            return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn4Rpt"], "SP_RepModelExplorer", RefNo, OutputType);
        }
        public static void DeleteTempSpec(Int32 RefNo)
        {
            SqlHelper.ExecuteNonQuery(ConfigurationSettings.AppSettings["SQLConn"], "SP_DeleteTempSpec", RefNo);
        }
        public static string GetContractNoForXL(Int32 RefNo, int ParmCodeCNo, int ParmCodeLNo)
        {
            return SqlHelper.ExecuteScalar(ConfigurationSettings.AppSettings["SQLConn"], "SP_GetContractNoForXL", RefNo, ParmCodeCNo, ParmCodeLNo).ToString();
        }
        /*public static void UpdateResultValueForTempSpec(int RefNo)
        {
            SqlHelper.ExecuteNonQuery(ConfigurationSettings.AppSettings["SQLConn"],"SP_UpdateResultValueForTempSpec",RefNo);
        }*/
        //public static bool IsContractInUse(string Dept, string ContractNo)
        /*public static bool IsContractInUse(int RefNo)
        {
            //return Convert.ToBoolean(SqlHelper.ExecuteScalar(ConfigurationSettings.AppSettings["SQLConn"],"SP_IsContractInUse",Dept,ContractNo));
            return Convert.ToBoolean(SqlHelper.ExecuteScalar(ConfigurationSettings.AppSettings["SQLConn"],"SP_IsContractInUse",RefNo));
        }
        public static DataSet GetForOptional(int RefNo, string ModelType)
        {
            return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"],"SP_GetForOptional",RefNo,ModelType);
        }*/
        public static void DeleteTempMaster(Int32 RefNo)
        {
            SqlHelper.ExecuteNonQuery(ConfigurationSettings.AppSettings["SQLConn"], "SP_DeleteTempMaster", RefNo);
        }
        public static bool IsDTProcessing()
        {
            return Convert.ToBoolean(SqlHelper.ExecuteScalar(ConfigurationSettings.AppSettings["SQLConn"], "SP_IsDTProcessing"));
        }

        public static bool IsContractNoExists(Int32 RefNo, string ContractNo)
        {
            return Convert.ToBoolean(SqlHelper.ExecuteScalar(ConfigurationSettings.AppSettings["SQLConn"], "SP_IsContractNoExists", RefNo, ContractNo));
        }
        public static bool IsCommonContractNoExists(string ContractNo)
        {
            return Convert.ToBoolean(SqlHelper.ExecuteScalar(ConfigurationSettings.AppSettings["SQLConn"], "SP_IsCommonContractNoExists", ContractNo));
        }
        public static bool IsPartNoAllocationExists(Int32 nRefNo, string sProjectNo, string sPartNoAllocation)
        {
            return Convert.ToBoolean(SqlHelper.ExecuteScalar(ConfigurationSettings.AppSettings["SQLConn"], "SP_IsPartNoAllocationExists",
                                                                                nRefNo, sProjectNo, sPartNoAllocation));
        }
        public static DataSet GetPortNames(string sCountryCode)
        {
            return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"], "SP_GetPortNames", sCountryCode);
        }
        public static string GetCountryCode(string sCountryName)
        {
            return SqlHelper.ExecuteScalar(ConfigurationSettings.AppSettings["SQLConn"], "SP_GetCountryCode", sCountryName).ToString();
        }
        
    }
}
