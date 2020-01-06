using System;
using System.Data;
using System.Configuration;
using SQLHelper.DataAccessLayer;

namespace NewOrderingSystem.BLL
{
    public class Release
    {
        static Boolean bLog = false;
        public Release()
        { }
        public static DataSet GetContractVendor(int RefNo)
        {
            return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"], "SP_GetContractVendor", RefNo);
        }
        public static DataSet GetCompException(int RefNo, string VendorList)
        {
            return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"], "SP_ComponentException", RefNo, VendorList);

        }
        //Added on 15/Sep/2014 - for MaterialCategory 
        public static DataSet GetMatCatCompException(int RefNo, string VendorList, string sMatCatList)
        {
            return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"], "SP_ComponentException", RefNo, VendorList, sMatCatList);
        }
        public static void UpdateExceptionInProjectBOM(int RefNo, int NodeID, int FunctionID, string PartNo,
                                                            string DwgNo, string RevNo, string Qty, Double TotalCost,
                                                            String CurrencyID, String sUpdateBy, Boolean bIsZeroQty)
        {
            if (bLog)
                General.WriteSQLQueryToText("SP_UpdateExceptionInProjectBOM " + RefNo + "," + NodeID + "," + FunctionID + ","
                                                    + PartNo + "," + DwgNo + "," + RevNo + ","
                                                    + Qty + "," + TotalCost + "," + CurrencyID + "," + sUpdateBy + "," + bIsZeroQty);
            else
                SqlHelper.ExecuteNonQuery(ConfigurationSettings.AppSettings["SQLConn"], "SP_UpdateExceptionInProjectBOM", RefNo,
                                            NodeID, FunctionID, PartNo, DwgNo, RevNo, Qty, TotalCost, CurrencyID, sUpdateBy, bIsZeroQty);
        }
        //Removed second parameter string LiftNo  on 17/Sep/2014
        //Modified VendorName to MaterialCategoryID on 11/Mar/2015
        //public static string CreateWBS(string ProjectNo,string VendorName, string VersionNo, int ProjRefNo, string ModelType, string ReleaseBy)
        public static DataSet CreateWBS(string ProjectNo, string sMatCatID, string VersionNo, int ProjRefNo, string ModelType, string ReleaseBy)
        {
            //return SqlHelper.ExecuteScalar(ConfigurationSettings.AppSettings["SQLConn"], "SP_CreateWBS", ProjectNo, VendorName, VersionNo, ProjRefNo, ModelType, ReleaseBy).ToString();
            return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"], "SP_CreateWBS", ProjectNo, sMatCatID, VersionNo, ProjRefNo, ModelType, ReleaseBy);
        }
        //Added on 15/Sep/2014 - for MaterialCategory 
        public static DataSet GetMaterialCategory(string sMaterialCategoryID)
        {
            return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"], "SP_GetMaterialCategory", sMaterialCategoryID);
        }
        //Added on 17/Sep/2014 - for Project Number
        public static string GetProjectNumber(int nRefNo)
        {
            return SqlHelper.ExecuteScalar(ConfigurationSettings.AppSettings["SQLConn"], "SP_GetProjectNumber", nRefNo).ToString();
        }
        //Added on 17/Sep/2014 - for Project Names 
        public static DataSet GetProjectNames(string sProjectNumber)
        {
            return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"], "SP_GetProjectNames", sProjectNumber);
        }
        //Added on 18/Sep/2014 - for Creating OrderHeader
        public static string CreateOrderHeaderAndComponent(String sProjectName, String sDocumentNo, String sUpdateBy)
        {
            return SqlHelper.ExecuteScalar(ConfigurationSettings.AppSettings["SQLConn"], "SP_CreateOrderHeaderAndComponent", sProjectName, sDocumentNo, sUpdateBy).ToString();
        }
        //Added on 22/Sep/2014 - for Creating OrderKit
        public static string CreateOrderKit(String sDocumentNo, String sUpdateBy)
        {
            return SqlHelper.ExecuteScalar(ConfigurationSettings.AppSettings["SQLConn"], "SP_CreateOrderKit", sDocumentNo, sUpdateBy).ToString();
        }
        //Added on 20/Oct/2014 - for ZeroCostException 
        public static DataSet GetZeroCostException(int RefNo, string VendorList, string sMatCatList)
        {
            return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"], "SP_GetZeroCostException", RefNo, VendorList, sMatCatList);
        }
        public static DataSet GetChangeComponent(int RefNo, string VendorList, string sMatCatList)
        {
            return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"], "SP_GetChangeComponent", RefNo, VendorList, sMatCatList);
        }
        public static void UpdateZeroCostException(int RefNo, int NodeID, int FunctionID, string sQty, string sCurrencyID,
                                                    Double lDirectMtl, Double lLabor, Double lSubcon, Double lSubconBatam,
                                                    Double lDMFJ, string sVendorType, String sUpdateBy, int sCablesRopeQty)
        {
            SqlHelper.ExecuteNonQuery(ConfigurationSettings.AppSettings["SQLConn"], "SP_UpdateZeroCostException",
                                                RefNo, NodeID, FunctionID, sQty, sCurrencyID,
                                                lDirectMtl, lLabor, lSubcon, lSubconBatam, lDMFJ, sVendorType, sCablesRopeQty, sUpdateBy);
        }
        public static void UpdateComponentNo(int RefNo, int NodeID, int FunctionID, string txtComponent, string sCurrencyID,
            string sVendorType, string sUpdateBy, string sVendorName, string sComponentName, int sCablesRopeQty, string txtDrawingNo, string txtRevNo, int Qty)
        {
            SqlHelper.ExecuteNonQuery(ConfigurationSettings.AppSettings["SQLConn"], "SP_UpdateComponentNo",
                                                RefNo, NodeID, FunctionID, txtComponent, sCurrencyID, sVendorType, sVendorName, sComponentName, sCablesRopeQty, txtDrawingNo, txtRevNo, Qty, sUpdateBy);
        }
        public static string CreateOrderInterface(int nWBSRefNo, String sProjectName, String sUpdateBy)
        {
            return SqlHelper.ExecuteScalar(ConfigurationSettings.AppSettings["SQLConn"], "SP_CreateOrderInterface", nWBSRefNo, sProjectName, sUpdateBy).ToString();
        }
        //Added on 24/Dec/2014 - for TempProjectRevision 
        public static string GetTempProjectRevDetails(int RefNo, string sPartNumber, int nRevNo)
        {
            return SqlHelper.ExecuteScalar(ConfigurationSettings.AppSettings["SQLConn"], "SP_GetTempProjectRevDetails",
                                                    RefNo, sPartNumber, nRevNo).ToString();
        }
        //Added on 24/Dec/2014 - for TempProjectRevision 
        public static string UpdateProjectRevision(int RefNo, int nActivateVersionNo, string sPartNumber, string sRevDescription, string sUpdateBy)
        {
            return SqlHelper.ExecuteScalar(ConfigurationSettings.AppSettings["SQLConn"], "SP_UpdateProjectRevision",
                                                    RefNo, nActivateVersionNo, sPartNumber, sRevDescription, sUpdateBy).ToString();
        }
        //Added on 10/Mar/2015 - for MaterialCategory From TempOutput 
        public static DataSet GetMatCategoryFromOutput(int nRefNo)
        {
            return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"], "SP_GetMatCategoryFromOutput", nRefNo);
        }
        //Added on 10/Mar/2015 - for getting the exception list from tempoutput for the given refno and MatCatList
        public static DataSet GetTempOutputComponentException(int RefNo, string VendorList, string sMatCatList)
        {
            return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"], "SP_GetTempOutputComponentException",
                                                                                                    RefNo, VendorList, sMatCatList);
        }
        public static void UpdateExceptionTempOutput(int RefNo, int NodeID, int FunctionID, string PartNo,
                                                            string DwgNo, string RevNo, string Qty, Double TotalCost,
                                                            String CurrencyID, String sUpdateBy, Boolean bIsZeroQty, String sUOM)
        {
            if (bLog)
                General.WriteSQLQueryToText("SP_UpdateExceptionTempOutput " + RefNo + "," + NodeID + "," + FunctionID + ","
                                                    + PartNo + "," + DwgNo + "," + RevNo + ","
                                                    + Qty + "," + TotalCost + "," + CurrencyID + "," + sUpdateBy + "," + bIsZeroQty + "," + sUOM);
            else
                SqlHelper.ExecuteNonQuery(ConfigurationSettings.AppSettings["SQLConn"], "SP_UpdateExceptionTempOutput", RefNo,
                                            NodeID, FunctionID, PartNo, DwgNo, RevNo, Qty, TotalCost, CurrencyID, sUpdateBy, bIsZeroQty, sUOM);
        }
        //Added on 17/Sep/2014 - for Project Number
        public static string GetWBSMasterVersionNumber(String sProjectNo, String sMaterialCategoryID, int nRefNo)
        {
            return SqlHelper.ExecuteScalar(ConfigurationSettings.AppSettings["SQLConn"], "SP_GetWBSMasterVersionNumber",
                                                                                                sProjectNo, sMaterialCategoryID, nRefNo).ToString();
        }
        //Added on 12/Mar/2015 - for getting the released project names from orderheader for the MatCatID and contract
        public static DataSet GetWBSMasterActiveProjectList(string sProjectNo, string sMatCatID, int nRefNo)
        {
            return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"], "SP_GetWBSMasterActiveProjectList",
                                                                                                    sProjectNo, sMatCatID, nRefNo);
        }
        //Added on 12/Mar/2015 - for getting the Last WBSRefNo from WBSMaster for the History purpose
        public static string GetWBSMasterActiveRefNumber(string sProjectNo, string sMatCatID, int nRefNo)
        {
            return SqlHelper.ExecuteScalar(ConfigurationSettings.AppSettings["SQLConn"], "SP_GetWBSMasterActiveRefNumber",
                                                                                                    sProjectNo, sMatCatID, nRefNo).ToString(); ;
        }
        //Added on 12/Mar/2015 - To get the parent part number details from OrderKit for WBSRefNo
        public static DataSet GetOrderKitParentPartNos(int nWBSRefNo)
        {
            return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"], "SP_GetOrderKitParentPartNos",
                                                                                                    nWBSRefNo);
        }
        //Added on 13/Mar/2015 - To Update the Order History of K-Set for subsequent recompute and release 
        public static string UpdateWBSMasterKSetHistory(int nWBSRefNo, string sParentPartNo, int nVersionNo,
                                                                    string sRevisedBy, string sModificationType, string sDisplaySeq,
                                                                    string sRevDescription, string sRevType)
        {
            return SqlHelper.ExecuteScalar(ConfigurationSettings.AppSettings["SQLConn"], "SP_UpdateWBSMasterKSetHistory",
                                                    nWBSRefNo, sParentPartNo, nVersionNo, sRevisedBy, sModificationType,
                                                    sDisplaySeq, sRevDescription, sRevType).ToString();
        }
        //Added on 13/Mar/2015 - To get the parent part number details from OrderKit for WBSRefNo
        public static DataSet GetOrdComponentDetailsForParentPartNo(int nWBSRefNo, string sParentPartNo)
        {
            return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"], "SP_GetOrdComponentDetailsForParentPartNo",
                                                                                                    nWBSRefNo, sParentPartNo);
        }
        //Added on 18/Mar/2015 - To Get the only the cleared exception list from tempoutput for the given refno and MatCatList
        public static DataSet GetTempOutputClearedComponentException(int RefNo, string VendorList, string sMatCatList)
        {
            return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"], "SP_GetTempOutputClearedComponentException",
                                                                                                    RefNo, VendorList, sMatCatList);
        }
        //Added on 18/Mar/2015 - To Get the exception list from tempoutput for the given refno,vendorlist 
        //			and MatCatlist , If exception cleared exist in WBSBOM replace those in Tempoutput
        public static DataSet GetTempOutputWBSBOMComponentException(int RefNo, string VendorList, string sMatCatList, string sProjNumber, string sModelType)
        {
            return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"], "SP_GetTempOutputWBSBOMComponentException",
                                                                                                    RefNo, VendorList, sMatCatList, sProjNumber, sModelType);
        }
        //Added on 26/Aug/2015 - To get the UOM values from Parameters for Qty
        public static DataSet GetParmUOMValuesForQty()
        {
            return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"], "SP_GetParmUOMValuesForQty");
        }

        public static string InsertKSetDetailsPDFGeneration(string refNo, string userId)
        {
            return Convert.ToString(SqlHelper.ExecuteScalar(ConfigurationSettings.AppSettings["SQLConn"], "SP_InsertKSetDetailsPDFGeneration", refNo, userId));
        }
        public static DataTable GetRptKSetWBSList(string wbsRefNo, string vendorName)
        {
            return SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings["SQLConn"], "SP_RptKSetWBSList", wbsRefNo, vendorName).Tables[0];
        }

        public static void InsertKSetDetailsPDFGenerationDetails(string headerId, string documentNo, string modelType, string materialCat, string version, string userId, int proRefNo, int wbsRefNo, string partNo)
        {
            SqlHelper.ExecuteNonQuery(ConfigurationSettings.AppSettings["SQLConn"], "SP_InsertKSetDetailsPDFGenerationDetails", headerId, documentNo, modelType, materialCat, version, userId, proRefNo, wbsRefNo, partNo);
        }
        public static string UpdateKSetDetailsPDFGeneration(string id, string status, string userId, string type, int subId)
        {
            return Convert.ToString(SqlHelper.ExecuteScalar(ConfigurationSettings.AppSettings["SQLConn"], "SP_UpdateKSetDetailsPDFGeneration", id, status, userId, type, subId));
        }
    }
}
