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
    public class ReportManuallyController : ApiController
    {
        IReportManuallyService _reportService;

        public ReportManuallyController(IReportManuallyService reportService)
        {
            _reportService = reportService;
        }
        //Get api/manualreport
        [Route("api/ReportManually/GetReportStatus/{id}")]
        public IQueryable<manualreport> GetReportStatus(int id)
        {
            return _reportService.GetAll().Where(x => x.reptmid == id);
        }

        public IQueryable<manualreport> Get()
        {
            return _reportService.GetAll();
        }

        
        

        public async Task<IHttpActionResult> Get(int id)
        {
            var manualreportmodel = await _reportService.Get(id);
            return Ok(manualreportmodel);
        }

        

        // POST api/manualreport
        public async Task<IHttpActionResult> Post([FromBody]manualreport _mstmodel)
        {
            var mstmodel = await _reportService.Add(_mstmodel);

            return CreatedAtRoute("DefaultApi", new { id = mstmodel.rlistid }, mstmodel);
        }

        // PUT api/mstmodel/5
        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, [FromBody]manualreport _mstmodel)
        {
            if (id != _mstmodel.rlistid)
            {
                throw new Exception("Invalid Meter Model");
            }
            var mstmodel = await _reportService.Edit(_mstmodel);

            return Ok(mstmodel);
        }

        
    }
}
