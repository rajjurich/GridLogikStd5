using Domain.Entities;
using Domain.Extension;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GridLogik.API.Controllers
{
    public class OPCServerTagController : ApiController
    {
        IOPCServerTagService iOPCServerTagService;
        IMeterService meterservice;
        public OPCServerTagController(IOPCServerTagService iOPCServerTagService
            , IMeterService meterservice)
        {
            this.iOPCServerTagService = iOPCServerTagService;
            this.meterservice = meterservice;
        }



        
        // GET api/opcservertag
        public IQueryable<opcservertag> Get()
        {
            IQueryable<opc_server_tag> opc_server_tags = iOPCServerTagService.FindBy(x => x.query == null);
            var yx = opc_server_tags.ToList();
            return ToClientFolderMaps(opc_server_tags);
        }

        private IQueryable<opcservertag> ToClientFolderMaps(IQueryable<opc_server_tag> opc_server_tags)
        {
            List<opcservertag> opcservertags = new List<opcservertag>();
            foreach (var opc_server_tag in opc_server_tags)
            {
                string tempstr = meterservice.Get(Convert.ToInt32(opc_server_tag.meterid)).Result.metername;
                opcservertag opcservertag = new opcservertag();
                opcservertag.id = opc_server_tag.id;
                opcservertag.datatype = opc_server_tag.datatype;
                opcservertag.istag = opc_server_tag.istag;
                opcservertag.meterid = opc_server_tag.meterid;
                opcservertag.parameter = opc_server_tag.parameter;
                opcservertag.priority = opc_server_tag.priority;
                opcservertag.tablename = opc_server_tag.tablename;
                opcservertag.tagname = opc_server_tag.tagname;
                opcservertag.Metername = tempstr;
                opcservertag._priority = Convert.ToInt32(opc_server_tag.priority);
                opcservertag.is_tag = opc_server_tag.istag == 1 ? true : false;

                opcservertags.Add(opcservertag);
            }
            return opcservertags.AsQueryable();
        }

        // GET api/opcservertag/ForCalculation
        [Route("api/opcservertag/ForCalculation")]
        public IQueryable<opc_server_tag> GetForCalculation()
        {
            IQueryable<opc_server_tag> opc_server_tags = iOPCServerTagService.FindBy(x => x.query != null);
            var yx = opc_server_tags.ToList();
            return opc_server_tags;
        }

        // GET api/opcservertag/5
        public async Task<IHttpActionResult> Get(int id)
        {
            opc_server_tag opc_server_tag = await iOPCServerTagService.Get(id);


            string tempstr = opc_server_tag.meterid == null ? "" : meterservice.Get(Convert.ToInt32(opc_server_tag.meterid)).Result.metername;
            opcservertag opcservertag = new opcservertag();
            opcservertag.id = opc_server_tag.id;
            opcservertag.datatype = opc_server_tag.datatype;
            opcservertag.istag = opc_server_tag.istag;
            opcservertag.meterid = opc_server_tag.meterid;
            opcservertag.parameter = opc_server_tag.parameter;
            opcservertag.priority = opc_server_tag.priority;
            opcservertag.tablename = opc_server_tag.tablename;
            opcservertag.tagname = opc_server_tag.tagname;
            opcservertag.Metername = tempstr;
            opcservertag._priority = Convert.ToInt32(opc_server_tag.priority);
            opcservertag.is_tag = opc_server_tag.istag == 1 ? true : false;
            opcservertag.query = opc_server_tag.query;
            return Ok(opcservertag);
        }

        // POST api/opcservertag
        public async Task<IHttpActionResult> Post([FromBody]opc_server_tag opc_server_tag)
        {
            opc_server_tag.datatype = (opc_server_tag.datatype == null || opc_server_tag.datatype == string.Empty) ? "X" : opc_server_tag.datatype;
            opc_server_tag.parameter = (opc_server_tag.parameter == null || opc_server_tag.parameter == string.Empty) ? "X" : opc_server_tag.parameter;
            opc_server_tag.tablename = (opc_server_tag.tablename == null || opc_server_tag.tablename == string.Empty) ? "X" : opc_server_tag.tablename;

            if (!(ModelState.IsValid))
            {
                throw new Exception("Invalid Gridlogik calculation Object");
            }
            var opcServerTag = await iOPCServerTagService.Add(opc_server_tag);

            return CreatedAtRoute("Default", new { id = opcServerTag.id }, opcServerTag);
        }

        // PUT api/opcservertag/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]opc_server_tag opc_server_tag)
        {
            opc_server_tag.datatype = (opc_server_tag.datatype == null || opc_server_tag.datatype == string.Empty) ? "X" : opc_server_tag.datatype;
            opc_server_tag.parameter = (opc_server_tag.parameter == null || opc_server_tag.parameter == string.Empty) ? "X" : opc_server_tag.parameter;
            opc_server_tag.tablename = (opc_server_tag.tablename == null || opc_server_tag.tablename == string.Empty) ? "X" : opc_server_tag.tablename;

            if (!(ModelState.IsValid))
            {
                throw new Exception("Invalid Gridlogik calculation Object");
            }
            var opcServerTag = await iOPCServerTagService.Edit(opc_server_tag);
            return Ok(opcServerTag);
        }

        // DELETE api/opcservertag/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            opc_server_tag opc_server_tag = await iOPCServerTagService.Get(id);
            if (opc_server_tag == null)
            {
                throw new Exception("Invalid OPC server Tag");
            }
            var opcServerTag = await iOPCServerTagService.Remove(opc_server_tag);
            return Ok(opcServerTag);
        }
    }
}
