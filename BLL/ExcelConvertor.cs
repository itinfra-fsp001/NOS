using System;
using System.Data;
using System.IO;
using System.Text;

namespace NewOrderingSystem
{
	/// <summary>
	/// To generate excel file.
	/// </summary>
	public class ExcelConvertor
	{
		/// <summary>
		/// To generate excel file.
		/// </summary>
		/// <param name="oDataTable"></param>
		/// <param name="directoryPath"></param>
		/// <param name="fileName"></param>
		/// <returns></returns>
		public string Convert(DataTable oDataTable, string directoryPath, string fileName, string appName)	
		{
			string fullpath = "";

			if (directoryPath.Substring(directoryPath.Length - 1,1) == @"\" ||directoryPath.Substring(directoryPath.Length - 1,1) == "/")	
			{
				fullpath = directoryPath + fileName;
			}
			else	
			{
				fullpath = directoryPath + @"\" + fileName;
			}

			StreamWriter SW;
			SW=File.CreateText(fullpath);
			
			
			StringBuilder oStringBuilder = new StringBuilder();

			/********************************************************
			 * Start, check for border width
			 * ******************************************************/
			int borderWidth =1;

			if (_ShowExcelTableBorder)	
			{
				borderWidth = 1;
			}
			/********************************************************
			 * End, Check for border width
			 * ******************************************************/

			/********************************************************
			 * Start, Check for bold heading
			 * ******************************************************/
			string boldTagStart = "";
			string boldTagEnd = "";
			if (_ExcelHeaderBold)	
			{
				boldTagStart = "<B>";
				boldTagEnd = "</B>";
			}

			/********************************************************
			 * End,Check for bold heading
			 * ******************************************************/

			oStringBuilder.Append("<Table border=" + borderWidth + ">");

			/*******************************************************************
			 * Start, Creating table header
			 * *****************************************************************/

			oStringBuilder.Append("<TR>");

			oDataTable.Columns["DISPLAYNAME"].ColumnName="APPLICATION NAME";
			oDataTable.Columns["PARMVALUE"].ColumnName=appName; //added by Nambi on 02-MAY-2007
            
			foreach(DataColumn oDataColumn in oDataTable.Columns)	
			{
				if (oDataColumn.ColumnName.ToUpper()=="APPLICATION NAME" || oDataColumn.ColumnName.ToUpper()==appName.ToUpper()) //"PARMVALUE")
					oStringBuilder.Append("<TD>" + boldTagStart + oDataColumn.ColumnName + boldTagEnd + "</TD>");
			}

			oStringBuilder.Append("</TR>");

			/*******************************************************************
			 * End, Creating table header
			 * *****************************************************************/

			/*******************************************************************
			 * Start, Creating rows
			 * *****************************************************************/

			foreach(DataRow oDataRow in oDataTable.Rows)	
			{
				oStringBuilder.Append("<TR>");

				foreach(DataColumn oDataColumn in oDataTable.Columns)	
				{
					if (oDataColumn.ColumnName.ToUpper()=="APPLICATION NAME" || oDataColumn.ColumnName.ToUpper()==appName.ToUpper()) //=="PARMVALUE")
					{
						if (oDataRow[oDataColumn.ColumnName] is long)	
						{
							oStringBuilder.Append("<TD align=right>" + oDataRow[oDataColumn.ColumnName] + "</TD>");
						}
						else	
						{
							oStringBuilder.Append("<TD>" + oDataRow[oDataColumn.ColumnName] + "</TD>");
						}	
					}
					
				}

				oStringBuilder.Append("</TR>");
			}
			

			/*******************************************************************
			 * End, Creating rows
			 * *****************************************************************/



			oStringBuilder.Append("</Table>");
			
			SW.WriteLine(oStringBuilder.ToString());
			SW.Close();

			return fullpath;
		}

		private bool _ShowExcelTableBorder = false;

		/// <summary>
		/// To show or hide the excel table border
		/// </summary>
		public bool ShowExcelTableBorder
		{
			get
			{
				return _ShowExcelTableBorder;
			}
			set
			{
				_ShowExcelTableBorder = value;
			}
		}

		private bool _ExcelHeaderBold = true;


		/// <summary>
		/// To make header bold or normal
		/// </summary>
		public bool ExcelHeaderBold 
		{
			get
			{
				return _ExcelHeaderBold;
			}
			set
			{
				ExcelHeaderBold = value;
			}
		}
	}
}
