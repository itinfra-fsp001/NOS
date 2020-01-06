using System;
using System.Data;
using System.Configuration;
using SQLHelper.DataAccessLayer;


namespace NewOrderingSystem.BLL
{
    /// <summary>
    /// Summary description for ViewOutPut.
    /// </summary>
    public class ViewOutPut
    {
        public ViewOutPut()
        { }
        public static DataSet GetViewOutput(string ContractNo, string User)
        {
            return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"], "dbo.SP_GetViewOutput", ContractNo, User);
        }
        public static DataSet GetDepartment()
        {
            return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"], "SP_GetDepartment");
        }
        public static string GetControlDept()
        {
            return SqlHelper.ExecuteScalar(ConfigurationSettings.AppSettings["SQLConn"], "SP_GetControlDept").ToString();
        }
        public static DataSet GetContractUser(string DetpCode)
        {
            return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"], "SP_GetContractUser", DetpCode);
        }
        public static void UpdateTempMaster4VOCompute(Int32 RefNo)
        {
            SqlHelper.ExecuteNonQuery(ConfigurationSettings.AppSettings["SQLConn"], "SP_UpdateTempMaster4VOCompute", RefNo);
        }

        public static void UpdateCost(string RefNoList, string userID)
        {
            SqlHelper.ExecuteNonQuery(ConfigurationSettings.AppSettings["SQLConn"], "SP_UpdateTempOutputCost", RefNoList, userID);
        }


        public static string Activate(Int32 RefNo, String sApprovedBy, String sCheckedBy, String sIssuedBy)
        {
            return SqlHelper.ExecuteScalar(ConfigurationSettings.AppSettings["SQLConn"], "SP_Activate", RefNo,
                                                        sApprovedBy, sCheckedBy, sIssuedBy).ToString();
        }
        //Added on 04/Mar/2015 - for getting function node for compute 
        public static DataSet GetFunctionNodeForProject(string sModelType)
        {
            return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"], "SP_GetFunctionNodeForProject", sModelType);
        }

        public static DataSet GetAllComputedLifts(string contractNo)
        {
            return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"], "SP_GetAllComputedLifts", contractNo);
        }



        //Added on 04/Mar/2015 - for Updating ProcessFolder for selective compuation
        public static string UpdateProcessFolder(int nRefNo, int nNodeID, string sNode, string sModelType, string sAction)
        {
            return SqlHelper.ExecuteScalar(ConfigurationSettings.AppSettings["SQLConn"],
                                            "SP_UpdateProcessFolder", nRefNo, nNodeID, sNode, sModelType, sAction).ToString();
        }
        //Added on 23/Mar/2015 - To verify whether project has been released or not
        public static bool IsProjectReleased(Int32 nRefNo)
        {
            return Convert.ToBoolean(SqlHelper.ExecuteScalar(ConfigurationSettings.AppSettings["SQLConn"], "SP_IsProjectReleased",
                                                                                nRefNo));
        }
        //--Pradeep S -- Added on 08/June/2015 - for getting excluded componets for the selected country
        public static DataSet GetExcludedSets4Project(string sCountryCode, string sModelType)
        {
            return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"], "SP_GetExcludedSets4Project", sCountryCode, sModelType);
        }
        //Added on 11/Jun/2015 - for Updating ExcludedFunctionID for Selective Computation
        public static string UpdateExcludedFunctionID(int nRefNo, string sFunctionGroup, string sMaterialCategoryID, string sAction)
        {
            return SqlHelper.ExecuteScalar(ConfigurationSettings.AppSettings["SQLConn"],
                                            "SP_UpdateExcludedFunctionID", nRefNo, sFunctionGroup, sMaterialCategoryID, sAction).ToString();
        }
    }
}
