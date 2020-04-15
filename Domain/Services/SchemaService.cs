using Domain.Entities;
using Domain.Model;
using GridLogik.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface ISchemaService
    {
        IQueryable<TblData> GetSchemas(string tableName);

        DataTable GetQueryView(QueryViewModel history, string reportDisplayDate, string DateFormat);
    }
    public class SchemaService : ISchemaService
    {
        etools_devEntities db;        
        public SchemaService(DbContext db)
        {
            this.db = (etools_devEntities)db;            
        }
        public IQueryable<TblData> GetSchemas(string tableName)
        {
            List<TblData> tblDatas = new List<TblData>();
            string query = string.Empty;
            query = " SELECT t.tabname as tableName,c.colname as columnName FROM informix.systables  AS t  " +
                     " JOIN informix.syscolumns AS c ON t.tabid = c.tabid  where 1=1 and t.tabtype = 'T' " +
                     " AND t.tabid >= 100 and t.tabname = '" + tableName + "' and  c.colname not like 'timestampid'   and  c.colname not like 'tstamp'  ";

            var datas = db.Database.SqlQuery<TblData>(query).ToList();

            foreach (var data in datas)
            {
                TblData tblData = new TblData();
                tblData.tableName = data.tableName;
                tblData.columnName = data.columnName;
                tblDatas.Add(tblData);
            }

            return tblDatas.AsQueryable();



        }


        public DataTable GetQueryView(QueryViewModel history, string reportDisplayDate, string DateFormat)
        {   
            
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();

            if (history.Meters == null)
            {
                history.Meters = new List<string>();
            }



            sb.Append("select TO_CHAR(tstamp::datetime year to second,'" + reportDisplayDate + "') as tstamp,");

            if (history.Columns.Count > 0)
            {
                foreach (var column in history.Columns)
                {
                    sb.Append(column + ",");
                }

                sb.Remove(sb.Length - 1, 1);
            }
            else
            {
                sb.Append(" * ");
            }

            sb.Append(" from ");
            sb.Append(history.TableName);
            sb.Append(" where tstamp > ");
            var ft = DateTime.ParseExact(history.FromTime, DateFormat, CultureInfo.InvariantCulture);
            var tt = DateTime.ParseExact(history.ToTime, DateFormat, CultureInfo.InvariantCulture);
            sb.Append(" '" + ft.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            sb.Append(" and tstamp < ");
            sb.Append(" '" + tt.ToString("yyyy-MM-dd HH:mm:ss") + "' ");

            if (history.Meters.Count > 0)
            {
                sb.Append(" and meterid in (");
                foreach (var item in history.Meters)
                {
                    sb.Append(item + ",");
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append(") ");
            }
            //if (history.Interval.ToString().ToLower() == "h")
            //{
            //    sb.Append(" and mod(blockno,4)=0 ");
            //}
            //else if (history.Interval.ToString().ToLower() == "a")
            //{
            //    sb.Append(" and mod(blockno,2)=0 ");
            //}
            //else if (history.Interval.ToString().ToLower() == "d")
            //{
            //    sb.Append(" and blockno=96 ");
            //}

            //sb.Append("  and meterid=2 ");

            sb.Append("  order by tstamp ;");

            var query = sb.ToString();


            var conn = db.Database.Connection;
            var connectionState = conn.State;
            try
            {
                if (connectionState != ConnectionState.Open) conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.Text;
                    using (var reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                // error handling
                Console.WriteLine(ex);
                throw ex;
            }
            finally
            {
                if (connectionState != ConnectionState.Closed) conn.Close();
            }
            return dt;
        }

    }

}
