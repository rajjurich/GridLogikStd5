using Domain.Entities;
using Domain.Services;
using GridLogik.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GridLogik.API.Controllers
{
    public class AlarmLogController : ApiController
    {
        IAlarmLogService _alarmLogService;

        public AlarmLogController(IAlarmLogService alarmLogService)
        {
            _alarmLogService = alarmLogService;
        }

        [HttpPost]
        [Route("api/AlarmLog/PostAlarmLog")]
        public IQueryable<HTAlarm> PostAlarmLog(HTAlarm obj)
        {
            return _alarmLogService.GetAlarmlogpost(obj);
        }

        [Route("api/AlarmLog/filter")]
        [HttpPost]
        public IQueryable<HTAlarm> Post(HTAlarm h)
        {
            return _alarmLogService.GetAlarmlogpost(h);
        }

        [Route("api/AlarmLog/updateall")]
        public HttpResponseMessage updateall(List<htalarm> sd)
        {
            bool flag = _alarmLogService.updateall(sd);
            if (flag)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, sd);
                response.Headers.Location = new Uri(this.Request.RequestUri.AbsoluteUri + "/");
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
    }
}
