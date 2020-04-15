using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class ListtoDataTableConverter
    {
        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }

            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            //put a breakpoint here and check datatable
            return dataTable;
        }
        public void RemoveColmnName(string ColumnList, ref DataTable dt)
        {
            try
            {
                // string ColumnList = "$id,ContentEncoding,ContentType,Data,result,PLF,JsonRequestBehavior,MaxJsonLength,RecursionLimit";
                string[] ColList = ColumnList.Split(',');
                if (dt != null && dt.Columns.Count > 0)
                {
                    for (int i = 0; i < ColList.Length; i++)
                    {

                        if (dt.Columns.Contains(ColList[i]))
                            dt.Columns.Remove(ColList[i]);

                        dt.AcceptChanges();
                    }
                }
            }
            catch
            {
            }

        }
        public void ChangeColumnName(string ColumnList, ref DataTable dt)
        {
            try
            {
                string[] ColList = ColumnList.Split(',');
                if (dt != null && dt.Columns.Count > 0)
                {
                    for (int i = 0; i < ColList.Length; i++)
                    {
                        foreach (DataColumn dc in dt.Columns)
                        {
                            if (dc.ColumnName == ColList[i].Split('~')[0])
                            {
                                dc.ColumnName = Convert.ToString(ColList[i].Split('~')[1]);
                                dt.AcceptChanges();
                                break;
                            }
                        }

                    }
                }
            }
            catch
            {
            }

        }
    }
}
