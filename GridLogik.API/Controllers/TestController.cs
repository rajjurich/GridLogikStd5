using IBM.Data.DB2;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GridLogik.API.Controllers
{
    public class TestController : ApiController
    {
        DB2Connection con = new DB2Connection(ConfigurationManager.AppSettings["ConnString"]);
        public TestController()
        {

        }
        // GET api/test
        public DataTable Get()
        {
            //from i in db.evt_monthwise
            //                               join b in db.meters on i.meterid equals b.id
            //                               where i.ts > dtFromDate && i.ts < dtToDate && TagIds.Contains(i.meterid)
            //                               group new { i, b } by i.ts into tsGroup
            //                               select new Consumption1
            //                               {
            //                                   Name = tsGroup.Key,
            //                                   Unit = tsGroup.Sum(x => x.i.kwh_export)
            //                               }).OrderBy(M => M.Name).ToList();

            string query = "select sum(i.kwh_export),i.ts from evt_monthwise i join meters b on b.id=i.meterid where i.ts > '2019-12-01 00:00:00' and i.ts < '2020-02-01 00:00:00' and i.meterid in ('126') group by i.ts;";
            con.Open();
            DB2Command cmd1 = new DB2Command(query, con);
            DB2DataAdapter da = new DB2DataAdapter(cmd1);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        // GET api/test/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/test
        public void Post([FromBody]string value)
        {
        }

        // PUT api/test/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/test/5
        public void Delete(int id)
        {
        }
    }
}
