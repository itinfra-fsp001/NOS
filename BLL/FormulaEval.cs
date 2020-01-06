using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Web.Hosting;
using System.Collections;
using Microsoft.VisualBasic;
using System.Web.UI.WebControls;
namespace NewOrderingSystem.BLL
{
    public class FormulaEval
    {
        private static XmlDocument xmlDoc;
        private static ArrayList alControls;
        private static Hashtable  htControls;
        private static FormulaEval objFormula=null;
        private  FormulaEval()
        {
            try
            {
                xmlDoc = new XmlDocument();
                xmlDoc.Load(System.Web.HttpContext.Current.Server.MapPath("Formulas.xml"));
                alControls = new ArrayList();
                htControls = new Hashtable();
                

            }
            catch (Exception ex)
            {
                
                
            }
        }
        public static FormulaEval  GetInstance()
        {
            if(objFormula==null)
                    objFormula=new FormulaEval();
                return objFormula ;           
        }
        public void AddControlArray(ArrayList alCtrol,String sKey)
        {
            if (!htControls.Contains(sKey))
                htControls.Add(sKey, alCtrol);
        }
        public ArrayList getControlArray(String sKey)
        {
            if (htControls.Contains(sKey))
                return (ArrayList)htControls[sKey];
            else
                return new ArrayList();
        }
        public String getFormula(String sID)
        {
            String sRetVal = "";
            try
            {
                XmlNode nodeFormula = xmlDoc.SelectSingleNode("/formulas/formula[@id='" + sID + "']");
                if (nodeFormula != null)
                {
                    sRetVal = nodeFormula.Attributes.GetNamedItem("value").Value.ToString();
                }
            }
            catch (Exception ex)
            { 
                
               
            }
            
            return sRetVal;

        }
        public XmlNodeList getNA(String sGridID,String sID)
        {
             XmlNodeList  ndListRetVal =null;
            try
            {
                XmlNode nodeNA = xmlDoc.SelectSingleNode("/formulas/NAFields/NAGrids/NAGrid[@id='" + sGridID + "']/NA[@id='" + sID + "']");
                if (nodeNA != null)
                {
                    ndListRetVal = nodeNA.ChildNodes;                    
                }
            }
            catch (Exception ex)
            {


            }

            return ndListRetVal;

        }
        public XmlNodeList getNAParents(String sGridID)
        {
            XmlNodeList ndListRetVal = null;
            try
            {
                XmlNode nodeNAParent = xmlDoc.SelectSingleNode("/formulas/NAFields/NAGrids/NAGrid[@id='" + sGridID + "']");
                if (nodeNAParent != null)
                {
                    ndListRetVal = nodeNAParent.ChildNodes;
                }
            }
            catch (Exception ex)
            {


            }

            return ndListRetVal;

        }
        public XmlNodeList getNAGrid(String sID)
        {
            XmlNodeList ndListRetVal = null;
            try
            {
                XmlNode nodeGrid = xmlDoc.SelectSingleNode("/formulas/NAFields/NAGrids[@id='" + sID + "']");
                if (nodeGrid != null)
                {
                    ndListRetVal = nodeGrid.ChildNodes;
                }
            }
            catch (Exception ex)
            {


            }

            return ndListRetVal;

        }
    }
}
