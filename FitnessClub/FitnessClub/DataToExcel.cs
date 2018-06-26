using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.IO;
using System.Web.Mvc;
using System.Linq;
using System.Web;
using Microsoft.Office.Interop.Excel;

namespace FitnessClub
{
    public class DataToExcel
    { 
        public static System.Data.DataTable ConvertToDataTable<T>(List<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            System.Data.DataTable table = new System.Data.DataTable();
            foreach(PropertyDescriptor prop in properties)
            {
                if (prop.Name.ToLower() == "password" || prop.Name.ToLower() == "passwrodrep")
                {
                    continue;
                }
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            foreach(T item in data)
            {
                DataRow row = table.NewRow();
                foreach(PropertyDescriptor prop in properties)
                {
                    if (prop.Name.ToLower() == "password" || prop.Name.ToLower() == "passwrodrep")
                    {
                        continue;
                    }
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }
                table.Rows.Add(row);
            }
            return table;
        }
        
        public static bool FlushToExcel<T>(System.Data.DataTable dt)
        {
            PropertyDescriptorCollection propertyDescriptorCollection = TypeDescriptor.GetProperties(typeof(T));
            try
            {
                var excel = new Application { Visible = false };
                var misValue = System.Reflection.Missing.Value;
                var workbook = excel.Workbooks.Add(misValue);
                Worksheet sheet = workbook.Sheets.Add();
                sheet.Columns.ColumnWidth = 17;
                sheet.Columns.NumberFormat = "@";
                sheet.Name = "Teszt";
                int i = 0;
                foreach(PropertyDescriptor prop in propertyDescriptorCollection)
                {
                    if (prop.Name.ToLower() == "password" || prop.Name.ToLower() == "passwrodrep")
                    {
                        continue;
                    }
                    sheet.Cells[1, Convert.ToChar(65 + i).ToString()].Value2 += prop.Name.ToString();
                    ++i;
                }
                
                for(i = 0; i < dt.Rows.Count; ++i)
                {
                    for(int j = 0; j < dt.Columns.Count; ++j)
                    {
                        sheet.Cells[i+2, Convert.ToChar(65+j).ToString()].Value2 += dt.Rows[i][j].ToString();
                    }
                }
                workbook.SaveAs(System.Web.HttpContext.Current.Server.MapPath("~/Content/Export"), XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                workbook.Close(true);
                excel.Quit();
            }
            catch (Exception)
            {
                throw;
            }

            return true;
        }
    }
}