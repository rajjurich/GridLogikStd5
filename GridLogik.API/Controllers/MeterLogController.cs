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
    public class MeterLogController : ApiController
    {
        IMeterLogService _meterlogservice;

        public MeterLogController(IMeterLogService meterlogservice)
        {
            _meterlogservice = meterlogservice;
        }

        [HttpGet]
        public IQueryable<htalarm> MeterLog()
        {
            return _meterlogservice.GetMeterLog();
        }

        [HttpGet]
        [Route("api/MeterLog/Niulog")]
        public IQueryable<htalarm> Niulog()
        {
            return _meterlogservice.GetNiuLog();
        }

        [HttpGet]
        [Route("api/MeterLog/group")]
        public IQueryable<metergroup> group()
        {
            return _meterlogservice.group();
        }

        [HttpPost]
        [Route("api/MeterLog/meter")]

        public IQueryable<meter> meter(metergroup obj)
        {
            return _meterlogservice.meters(obj.id);
        }

        [HttpPost]
        [Route("api/MeterLog/meterfilter")]
        public IQueryable<htalarm> meterfilters(HtAlarmext obj)
        {
            return _meterlogservice.meterfilter(obj);
        }

        [HttpPost]
        [Route("api/MeterLog/NiuLogAPIFilter")]
        public IQueryable<htalarm> meterfilter(HtAlarmext obj)
        {
            return _meterlogservice.Niufilter(obj);
        }
        [Route("api/getwebalarms/{id?}")]
        public async Task<IHttpActionResult> GetWebAlarms(int? id = null)
        {
            return Ok(await _meterlogservice.GetWebAlarms(id));
        }


    }
}
