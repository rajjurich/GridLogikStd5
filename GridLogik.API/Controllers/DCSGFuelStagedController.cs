using Domain.Entities;
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
    public class DCSGFuelStagedController : ApiController
    {
        IDCSGFuelStagedService _DCSGFuelStagedService;

        
        public DCSGFuelStagedController(IDCSGFuelStagedService DCSGFuelStagedService)
        {
            _DCSGFuelStagedService = DCSGFuelStagedService;
        }
        //not used 
        public IQueryable<dcsg> Get()
        {
            return _DCSGFuelStagedService.GetAll().Where(model => model.isdeleted == 1);
        }
        //not used 
        public async Task<IHttpActionResult> Get(int id)
        {
            var dcgmodel = await _DCSGFuelStagedService.Get(id);
            return Ok(dcgmodel);
        }

        //done 
        [Route("api/DCSGFuelStaged/CheckUpload/{appdate}")]
        public IQueryable<dcsg> CheckUpload(string appdate)
        {
            string[] parts = appdate.Split('-');
            string newdt = parts[2] + "-" + parts[1] + "-" + parts[0];
            var appdates = Convert.ToDateTime(newdt).AddMinutes(15);
            
            var dcgmodel =  _DCSGFuelStagedService.GetAll().Where(model => model.tstamp==appdates);
            return  dcgmodel;
        }
        //done 
        [Route("api/DCSGFuelStaged/GetData/{appdate}/{stageid}")]
        public IQueryable<dcsg> GetData(string appdate, string stageid)
        {
            string[] parts = appdate.Split('-');
            string newdt = parts[2] + "-" + parts[1] + "-" + parts[0];
            DateTime newdate = Convert.ToDateTime(newdt);
            DateTime Nextdate = Convert.ToDateTime(newdt).AddDays(1).AddMinutes(15); 
            var stageids = Convert.ToInt64(stageid);
            var appdatetosend=Convert.ToDateTime(appdate);
            DateTime dt = Convert.ToDateTime(appdate);
            int RevisionNo = MaxRevisionDetail(Convert.ToString(stageids), newdate, Nextdate);

            var dcgmodel = _DCSGFuelStagedService.GetAll().Where(model => model.stageid == stageids && (model.tstamp > newdate && model.tstamp < Nextdate) && model.revision == RevisionNo);
            return dcgmodel;
        }


        public int MaxRevisionDetail(string stageid,DateTime newdate,DateTime Nextdate)
        {

            int Istageid = Convert.ToInt32(stageid);
            List<dcsg> records = _DCSGFuelStagedService.GetAll().Where(model => model.tstamp > newdate && model.tstamp < Nextdate && (model.stageid == Istageid)).ToList();


            if (records != null && records.Count > 0)
            {
                if (records.Max(x => x.revision) != null)
                    return Convert.ToInt32(records.Max(x => x.revision));
                else
                    return 0;
            }
            else
                return 0;

        }


        [Route("api/DCSGFuelStaged/GetDataRevision/{revisionid}")]
        public IQueryable<dcsg> GetDataRevision(int revisionid)
        {
            var dcgmodel = _DCSGFuelStagedService.GetAll().Where(model=>model.revision==revisionid);
            return dcgmodel;
        }

        public async Task<IHttpActionResult> Post([FromBody]List<dcsg> _dcsgmodel)
        {
            var dcsgstageid = _dcsgmodel[0].stageid;
            var revision = _dcsgmodel[0].revision;
            string appdate = Convert.ToDateTime(_dcsgmodel[0].tstamp).ToString("dd-MM-yyyy");
            string[] parts = appdate.Split('-');
            string newdt = parts[2] + "-" + parts[1] + "-" + parts[0];
            DateTime newdate = Convert.ToDateTime(newdt);
            DateTime Nextdate = Convert.ToDateTime(newdt).AddDays(1).AddMinutes(15);
            var dcsgs = _DCSGFuelStagedService.GetAll().Where(model => model.stageid == dcsgstageid && (model.tstamp > newdate && model.tstamp < Nextdate) && model.revision==revision);
            if (dcsgs.Count() > 0)
            {
                foreach (var dcsg in dcsgs)
                {
                    await _DCSGFuelStagedService.Remove(dcsg);
                }
            }
            
            foreach(var i in _dcsgmodel)
            {
                i.isdeleted = 0;
                var dcsgmodel = await _DCSGFuelStagedService.Add(i);
            }
            return Ok(_dcsgmodel);          
        }
       

        public async Task<IHttpActionResult> Put(int id, [FromBody]dcsg _dcsgmodel)
        {
            if (id != _dcsgmodel.id)
            {
                throw new Exception("Invalid DCSG Model");
            }
            var dcsgmodel = await _DCSGFuelStagedService.Edit(_dcsgmodel);

            return Ok(dcsgmodel);
        }


        

        public async Task<IHttpActionResult> Delete(int id)
        {
            var dcsgmodel = await _DCSGFuelStagedService.Get(id);
            if (dcsgmodel == null)
            {
                throw new Exception("Invalid DCSG Model");
            }
            dcsgmodel.isdeleted = 1;
            var mstpassmodel = await _DCSGFuelStagedService.Delete(dcsgmodel);
            return Ok(mstpassmodel);
        }

       

        
    }
}
