using Domain.Entities;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GridLogik.API.Controllers
{
    public class ManualReportListController : ApiController
    {
        IManualReportListService _manualreportService;

        public ManualReportListController(IManualReportListService manualreportService)
        {
            _manualreportService = manualreportService;
        }

        [Route("api/ManualReportList/GetReportName")]
        public IQueryable<manualrptlist> GetReportName()
        {
            return _manualreportService.GetReportAll();
        }
    }
}
