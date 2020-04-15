using Domain.Entities;
using IBM.Data.DB2;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public static class DatabaseHandler
    {
        public static async Task<DataTable> Handler(etools_devEntities db, string str)
        {
            DataTable dt = new DataTable();
            var conn = db.Database.Connection;
            var connectionState = conn.State;
            try
            {
                if (connectionState != ConnectionState.Open) conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = str;
                    cmd.CommandType = CommandType.Text;
                    using (var reader = cmd.ExecuteReader())
                    {
                        await Task.Run(() => dt.Load(reader));
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
        public static async Task<DataTable> Handler(string str)
        {
            DB2Connection conn = new DB2Connection(ConfigurationManager.AppSettings["ConnString"]);
            DataTable dt = new DataTable();            
            var connectionState = conn.State;
            try
            {
                if (connectionState != ConnectionState.Open) conn.Open();
                DB2Command cmd = new DB2Command(str, conn);
                DB2DataAdapter da = new DB2DataAdapter(cmd);
                await Task.Run(() =>  da.Fill(dt));                
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
