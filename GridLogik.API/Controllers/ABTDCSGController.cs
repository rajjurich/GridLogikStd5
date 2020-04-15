using Domain.Entities;
using Domain.Model;
using Domain.Services;
using GridLogik.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GridLogik.API.Controllers
{
    public class ABTdcsgController : ApiController
    {
        IDCSGFuelStagedService dcsgFuelStagedService;
        public ABTdcsgController(IDCSGFuelStagedService dcsgFuelStagedService)
        {
            this.dcsgFuelStagedService = dcsgFuelStagedService;
        }
        // GET api/abtdcsg
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/abtdcsg/5
        public string Get(int id)
        {
            return "value";
        }

        // GET api/abtdcsg
        [Route("api/abtdcsg/getnextblocks/")]
        public IEnumerable<dcsg> GetNextBlock(NextBlock nextBlock)
        {
            List<dcsg> dcsgList = new List<dcsg>();

            if (nextBlock.stages.Count < 1)
            {
                nextBlock.stages.Add(1);
            }

            DateTime dtNow = DateTime.Now;
            DateTime dtNextDay = dtNow.AddDays(1);

            var dscgs = dcsgFuelStagedService.FindBy(x => x.tstamp > dtNow && x.tstamp < dtNextDay && nextBlock.stages.Contains(x.stageid)).OrderBy(x => x.tstamp).Take(nextBlock.numberOfBlocks).ToList();

            var DSCGs = from d in dscgs
                        select new dcsg
                        {
                            agc = d.agc,
                            tstamp = d.tstamp,
                            blockno = d.blockno,
                            dcvalue = d.dcvalue,
                            fuelcost = d.fuelcost,
                            id = d.id,
                            iex = d.iex,
                            isdeleted = d.isdeleted,
                            revision = d.revision,
                            rras = d.rras,
                            sced = d.sced,
                            sgppa = d.sgppa,
                            sgvalue = d.sgvalue,
                            stageid = d.stageid,
                            timestampid = d.timestampid,
                            upload_date = d.upload_date,
                            urs = d.urs
                        };
            dcsgList.AddRange(DSCGs);
            return dcsgList;
        }

        // POST api/abtdcsg
        public void Post([FromBody]string value)
        {
        }

        // PUT api/abtdcsg/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/abtdcsg/5
        public void Delete(int id)
        {
        }
    }
}
