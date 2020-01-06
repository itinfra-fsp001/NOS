using System;
using System.Data;
using System.Configuration;
using SQLHelper.DataAccessLayer;

namespace NewOrderingSystem.BLL
{
    public class IssuePO
    {
        public IssuePO() { }

        public static DataTable  GetProjMstContractNo()
        {
           return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"], "SP_GetProjMstContractNo").Tables[0];
        }
        public static DataTable GetWBSMaster(int PrjRefNo)
        {
            return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"], "SP_GetWBSMaster",PrjRefNo).Tables[0];
        }
        public static DataTable GetWBSReportDetails(int nPrjRefNo, String sVendorName)
        {
            return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"], "SP_ExportWBSDetails", nPrjRefNo, sVendorName).Tables[0];
        }

        public static DataTable GetOutputReportDetails(int nPrjRefNo)
        {
            return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"], "SP_ExportOutputDetails", nPrjRefNo).Tables[0];
        }

        public static DataTable GetWBSMasterContractNo(String sUserID)
        {
            return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"], "SP_GetWBSMasterContractNo", sUserID).Tables[0];
        }
        public static DataTable GetWBSMasterReleaseDetails(String sUserID,String sContractNo,String sModelType, String sMatCatID)
        {
            return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"], "SP_GetWBSMasterReleaseDetails", sUserID
                                                    , sContractNo, sModelType, sMatCatID).Tables[0];
        }
        public static DataTable GetWBSMasterReleasedModelTypes(String sUserID)
        {
            return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"], "SP_GetWBSMasterReleasedModelTypes", sUserID).Tables[0];
        }
        public static DataTable GetWBSMasterReleasedMatCatIDs(String sUserID)
        {
            return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"], "SP_GetWBSMasterReleasedMatCatIDs", sUserID).Tables[0];
        }
    }
}
