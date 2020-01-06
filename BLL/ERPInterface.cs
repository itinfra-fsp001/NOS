using System;
using System.Data;
using System.Configuration;
using SQLHelper.DataAccessLayer;
namespace NewOrderingSystem.BLL
{
    public class ERPInterface
    {
        static Boolean bLog = false;
        public static DataSet GetKitDetailsForERP(int nWBSRefNo, string sProjectName, string sMatCatList)
        {
            return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"], "SP_GetKitDetailsForERP",
                                                nWBSRefNo, sProjectName, sMatCatList);
        }
        public static DataSet GetProjectNamesFromOrder(int nWBSRefNo)
        {
            return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"], "SP_GetProjectNamesFromOrder",
                                                nWBSRefNo);
        }
        public static DataSet GetMatCategoryFromOrder(int nWBSRefNo, string sMatCatID)
        {
            return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"], "SP_GetMatCategoryFromOrder",
                                                nWBSRefNo, sMatCatID);
        }
        
        public static string UpdateERPInterfaceStatus(int nWBSRefNo, String sProjectName, String sMatCatList, int nTransActions, String sUpdateBy)
        {
            string sRetVal = "";
            if (bLog)
                General.WriteSQLQueryToText("SP_UpdateERPInterfaceStatus " + nWBSRefNo + "," + sProjectName + ","
                                                    + sMatCatList + "," + nTransActions + "," + sUpdateBy);
            else
                sRetVal = SqlHelper.ExecuteScalar(ConfigurationSettings.AppSettings["SQLConn"], "SP_UpdateERPInterfaceStatus",
                                                            nWBSRefNo, sProjectName, sMatCatList, nTransActions, sUpdateBy).ToString();
            return sRetVal;

        }
        public static string CreateERPEBOMInterface(int nBatchNo, String sProjectName, String sMaterialCategoryID,
                                                    String sPartNumber, Double lQty, String sUpdateBy)
        {
            string sRetVal = "";
            sRetVal = SqlHelper.ExecuteScalar(ConfigurationSettings.AppSettings["SQLConn"], "SP_CreateERPEBOMInterface",
                                                            nBatchNo, sProjectName, sMaterialCategoryID, sPartNumber, lQty, sUpdateBy).ToString();
            return sRetVal;
        }
        public static DataSet GetEBOMInterfaceHeaderDetails(int nWBSRefNo)
        {
            return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"], "SP_GetEBOMInterfaceHeaderDetails",
                                                nWBSRefNo);
        }
        public static DataSet GetGetEBOMInterfaceBatchDetails(int nBatchNo, string sType)
        {
            return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"], "SP_GetEBOMInterfaceBatchDetails",
                                                nBatchNo, sType);
        }
    }
}
