using Domain.Core;
using Domain.Entities;
using Domain.Services;
using GridLogik.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GridLogik.API.Controllers
{
    public class MeterController : ApiController
    {
        IMeterService meterservice;
        IUnitOfWork unitOfWork;
        IParameterMFService parameterMFService;
        ICommunicationDetailLinkService communicationDetailLinkService;
        IPrmGlobalService prmGlobalService;
        public MeterController(IMeterService meterservice, IUnitOfWork unitOfWork
            , IParameterMFService parameterMFService
            , ICommunicationDetailLinkService communicationDetailLinkService
            , IPrmGlobalService prmGlobalService
            )
        {
            this.meterservice = meterservice;
            this.unitOfWork = unitOfWork;
            this.parameterMFService = parameterMFService;
            this.communicationDetailLinkService = communicationDetailLinkService;
            this.prmGlobalService = prmGlobalService;

        }
        // GET api/meter
        public IQueryable<meter> Get()
        {
            var meters = meterservice.FindBy(x => (x.isdeleted == 0 || x.isdeleted == null) && x.isactive == 1);
            return meters;
        }

        


        // GET api/meter/tcplinked
        [Route("api/meter/tcplinked/{id}")]
        public IQueryable<meter> GetTCPLinked(int? id)
        {
            var communicationtypeid = GetSerialTypeId();
            List<long?> linkedMetersSerial = communicationDetailLinkService
                .FindBy(x => communicationtypeid.Contains(x.communicationdetail.communicationtypeid))
                .Select(x => x.meterid).ToList();

            var linkedMetersById = GetMetersLinkedById(id);
            linkedMetersSerial.AddRange(linkedMetersById);
            var meters = meterservice.FindBy(x => (x.isdeleted == 0 || x.isdeleted == null) && !linkedMetersSerial.Contains(x.id));
            return meters;
        }

        private List<long?> GetMetersLinkedById(int? id)
        {
            var linkedMetersById = communicationDetailLinkService.FindBy(x => x.converterid != id).Select(x => x.meterid).ToList();
            return linkedMetersById;
        }

        // GET api/meter/seriallinked
        [Route("api/meter/seriallinked/{id}")]
        public IQueryable<meter> GetSerialLinked(int? id)
        {
            var communicationtypeid = GetTCPTypeId();
            List<long?> linkedMetersTCP = communicationDetailLinkService
                .FindBy(x => communicationtypeid.Contains(x.communicationdetail.communicationtypeid))
                .Select(x => x.meterid).ToList();
            var linkedMetersById = GetMetersLinkedById(id);
            linkedMetersTCP.AddRange(linkedMetersById);
            var meters = meterservice.FindBy(x => (x.isdeleted == 0 || x.isdeleted == null) && !linkedMetersTCP.Contains(x.id));
            return meters;
        }

        private List<long> GetTCPTypeId()
        {
            List<long> communicationtypeIds = new List<long>();
            var communicationtypeids = prmGlobalService.FindBy(prm => prm.prmmodule.ToLower() == "communicationtype" && (prm.prmidentifier.ToLower() == "tcp" || prm.prmidentifier.ToLower() == "modbus/tcp")).Select(x => x.prmvalue).ToList();
            foreach (var item in communicationtypeids)
            {
                communicationtypeIds.Add(Convert.ToInt64(item));
            }
            return communicationtypeIds;
        }

        private List<long> GetSerialTypeId()
        {
            List<long> communicationtypeIds = new List<long>();
            var communicationtypeids = prmGlobalService.FindBy(prm => prm.prmmodule.ToLower() == "communicationtype" && prm.prmidentifier.ToLower() == "serial").Select(x => x.prmvalue).ToList();
            foreach (var item in communicationtypeids)
            {
                communicationtypeIds.Add(Convert.ToInt64(item));
            }
            return communicationtypeIds;
        }

        // GET api/meter
        [Route("api/meter/unlinked")]
        public IQueryable<meter> GetUnlinked()
        {
            IEnumerable<long?> linkedMeters = communicationDetailLinkService.GetAll().Select(x => x.meterid);
            var meters = meterservice.GetUnlinked(linkedMeters);
            return meters;
        }

        // GET api/meter
        [Route("api/meter/GetMeterDetails/{Id}")]
        public IQueryable<meter> GetMeterDetails(long Id)
        {
            var meters = meterservice.GetMeterDetails(Id);
            return meters;
        }

        // GET api/meter/5
        public async Task<IHttpActionResult> Get(int id)
        {
            var meter = await meterservice.Get(id);
            return Ok(meter);
        }



        [Route("api/Meter/GetMetersByGroupID/{id}")]
        public List<MeterVM> GetMetersByGroupID(string id)
        {

            return meterservice.GetMetersByGroupID(id);
        }


        [Route("api/Meter/GetMetersByMultipleGroupID/{id}")]
        public List<MeterVM> GetMetersByMultipleGroupID(string id)
        {
            return meterservice.GetMetersByMultipleGroupID(id);
        }

        // GET api/meter/GetMeterWithParameterValues/5
        [Route("api/meter/GetMeterWithParameterValues/{id}")]
        public async Task<IHttpActionResult> GetMeterWithParameterValues(int id)
        {
            var meter = await meterservice.Get(id);
            meter.parametermfs = parameterMFService.GetParameterMFWithValuesByMeterId(meter.id).ToList();
            return Ok(meter);
        }

        // POST api/meter
        public async Task<IHttpActionResult> Post([FromBody]meter _meter)
        {
            if (!(ModelState.IsValid))
            {
                throw new Exception("Invalid Model");
            }


            var meter = await meterservice.Add(_meter);

            //foreach (var _parametermf in _meter.parametermfs)
            //{
            //    if (_parametermf.multiplicationfactor != null)
            //    {
            //        _parametermf.meterid = meter.id;
            //        //await _parameterMFService.Add(_parametermf);
            //    }
            //}


            return CreatedAtRoute("DefaultApi", new { id = meter.id }, meter);

        }

        // PUT api/meter/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]meter _meter)
        {
            var meter = await meterservice.Edit(_meter);
            foreach (var _parametermf in _meter.parametermfs)
            {
                await parameterMFService.Edit(_parametermf);
            }

            return Ok(meter);
        }

        // DELETE api/meter/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            var _meter = await meterservice.Get(id);
            if (_meter == null)
            {
                throw new Exception("Invalid Meter");
            }
            var metermodel = await meterservice.Delete(_meter);
            return Ok(metermodel);
        }
    }
}
