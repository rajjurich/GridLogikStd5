using Domain.Common;
using Domain.Entities;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IDataDynamicService
    {
        Task<DataTable> GetDataDynamic(string tableName, string columnname, MeterList meters, int? meterid);
        Task<DataTable> GetDataDynamicOpenQuery(string query);

    }
    public class DataDynamicService : IDataDynamicService
    {
        etools_devEntities db;
        StringBuilder sb = new StringBuilder();
        public DataDynamicService(DbContext db)
        {
            this.db = (etools_devEntities)db;
        }
        public async Task<DataTable> GetDataDynamic(string tableName, string columnname, MeterList meters, int? meterid)
        {
            DataTable dt = new DataTable();

            string strReturn = string.Empty;

            string queryString = "";
            string groupAndOrderByClause = "";

            if (meters.MeterString != "" || meters.MeterString != null)
            {
                var strCheck = meters.AvgParameter + meters.ColumnParameter + meters.CountParameter +
                   meters.GroupParameter + meters.MaxParameter + meters.MinParameter + meters.SumParameter;

                if (!(strCheck.ToLower().Contains("drop") || strCheck.ToLower().Contains("truncate") ||
                    strCheck.ToLower().Contains("select") || strCheck.ToLower().Contains("update") ||
                    strCheck.ToLower().Contains("delete") || strCheck.ToLower().Contains("alter")))
                {
                    if (!(string.IsNullOrWhiteSpace(meters.ColumnParameter)))
                    {
                        queryString = meters.ColumnParameter;

                        if (!(string.IsNullOrWhiteSpace(meters.GroupByColumnParameter)))
                        {
                            groupAndOrderByClause += string.Format(" group by {0} ", meters.GroupByColumnParameter);
                        }

                        if (!(string.IsNullOrWhiteSpace(meters.OrderByColumnParameter)))
                        {
                            groupAndOrderByClause += string.Format(" order by {0} ", meters.OrderByColumnParameter);
                        }
                    }
                    else
                    {
                        if (!(string.IsNullOrWhiteSpace(meters.AvgParameter)))
                        {
                            queryString += meters.AvgParameter;
                            queryString += ",";
                        }
                        if (!(string.IsNullOrWhiteSpace(meters.CountParameter)))
                        {
                            queryString += meters.CountParameter;
                            queryString += ",";
                        }
                        if (!(string.IsNullOrWhiteSpace(meters.MaxParameter)))
                        {
                            queryString += meters.MaxParameter;
                            queryString += ",";
                        }
                        if (!(string.IsNullOrWhiteSpace(meters.MinParameter)))
                        {
                            queryString += meters.MinParameter;
                            queryString += ",";
                        }
                        if (!(string.IsNullOrWhiteSpace(meters.SumParameter)))
                        {
                            queryString += meters.SumParameter;
                            queryString += ",";
                        }
                        if (!(string.IsNullOrWhiteSpace(meters.GroupParameter)))
                        {
                            queryString += meters.GroupParameter;
                            groupAndOrderByClause = string.Format(" group by {0} order by {0} ;", meters.GroupParameter);
                        }
                    }
                }
                else
                {
                    queryString += "*";
                    groupAndOrderByClause = " order by tstamp desc;";
                }

            }


            sb.Append("select ");

            sb.Append(queryString);

            sb.Append(" from ");
            sb.Append(tableName);
            sb.Append(" where " + columnname + " in (");

            if (meters.MeterString != "" || meters.MeterString != null)
            {
                sb.Append(meters.MeterString);
            }
            else
            {
                sb.Append(meterid == null ? 0 : meterid);
            }
            sb.Append(") ");



            if (meters.FromSelectionFilter != "" && meters.FromSelectionFilter != null
                && meters.ToSelectionFilter != "" && meters.ToSelectionFilter != null
                && meters.FromTime != "" && meters.FromTime != null
                && meters.ToTime != "" && meters.ToTime != null
                )
            {
                if (meters.FromSelectionFilter.Contains("Current Month"))
                {
                    meters.FromSelectionFilter = "Current Month From";
                }
                if (meters.ToSelectionFilter.Contains("Current Month"))
                {
                    meters.ToSelectionFilter = "Current Month To";
                }
                if (meters.FromSelectionFilter.Contains("Previous Month"))
                {
                    meters.FromSelectionFilter = "Previous Month From";
                }
                if (meters.ToSelectionFilter.Contains("Previous Month"))
                {
                    meters.ToSelectionFilter = "Previous Month To";
                }


                if (meters.FromSelectionFilter.Contains("Current Year"))
                {
                    meters.FromSelectionFilter = "Current Year From";
                }
                if (meters.ToSelectionFilter.Contains("Current Year"))
                {
                    meters.ToSelectionFilter = "Current Year To";
                }
                if (meters.FromSelectionFilter.Contains("Previous Year"))
                {
                    meters.FromSelectionFilter = "Previous Year From";
                }
                if (meters.ToSelectionFilter.Contains("Previous Year"))
                {
                    meters.ToSelectionFilter = "Previous Year To";
                }


                if (meters.FromSelectionFilter.Contains("Current Financial Year"))
                {
                    meters.FromSelectionFilter = "Current Financial Year From";
                }
                if (meters.ToSelectionFilter.Contains("Current Financial Year"))
                {
                    meters.ToSelectionFilter = "Current Financial Year To";
                }
                if (meters.FromSelectionFilter.Contains("Previous Financial Year"))
                {
                    meters.FromSelectionFilter = "Previous Financial Year From";
                }
                if (meters.ToSelectionFilter.Contains("Previous Financial Year"))
                {
                    meters.ToSelectionFilter = "Previous Financial Year To";
                }

                sb.Append(" and tstamp > ");
                sb.Append("'" + GetBlockInformation.GetDateCondition(meters.FromSelectionFilter, meters.FromTime) + "'");
                sb.Append(" and tstamp < ");
                sb.Append("'" + GetBlockInformation.GetDateCondition(meters.ToSelectionFilter, meters.ToTime) + "'");
            }
            else
            {
                int DelaySeconds = ConfigurationManager.AppSettings["DelaySeconds"] == null || string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["DelaySeconds"].ToString()) ? 20 : Convert.ToInt32(ConfigurationManager.AppSettings["DelaySeconds"].ToString());
                sb.Append(" and tstamp > '" + DateTime.Now.AddSeconds(-DelaySeconds).ToString("yyyy-MM-dd HH:mm:ss") + "' ");

            }

            sb.Append(groupAndOrderByClause);


            var str = sb.ToString();

            dt = await DatabaseHandler.Handler(db, str);


            return dt;
        }




        public async Task<DataTable> GetDataDynamicOpenQuery(string query)
        {
            DataTable dt = new DataTable();

            if (!(query.ToLower().Contains("drop") || query.ToLower().Contains("truncate") || query.ToLower().Contains("alter")))
            {
                sb.Append(query);
            }

            var str = sb.ToString();
            dt = await DatabaseHandler.Handler(db, str);
            return dt;
        }
    }
}
