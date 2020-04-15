using Domain.Core;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IManualReportListService
    {
        IQueryable<manualrptlist> GetReportAll();
    }

    public class ManualReportListService : IManualReportListService
    {
        IEntityRepository<manualrptlist> _entityreport;

        public ManualReportListService(IEntityRepository<manualrptlist> entityreport)
        {
            _entityreport = entityreport;
        }



        public IQueryable<manualrptlist> GetReportAll()
        {
            var obj = _entityreport.GetAll();
            return obj;
        }
    }
}
