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
    public class CommunicationDetailController : ApiController
    {
        ICommunicationDetailService communicationDetailService;
        ICommunicationDetailLinkService communicationDetailLinkService;
        IPrmGlobalService prmGlobalService;
        public CommunicationDetailController(ICommunicationDetailService communicationDetailService
            , ICommunicationDetailLinkService communicationDetailLinkService
            , IPrmGlobalService prmGlobalService)
        {
            this.communicationDetailService = communicationDetailService;
            this.communicationDetailLinkService = communicationDetailLinkService;
            this.prmGlobalService = prmGlobalService;
        }
        // GET api/communicationdetail
        [HttpGet]
        public IQueryable<communicationdetail> Get()
        {
            //var communicationtypeid = GetTCPTypeId();
            return communicationDetailService.FindBy(x => x.isdeleted == 0 || x.isdeleted == null);
        }

        // GET api/communicationdetail
        [HttpGet]
        [Route("api/communicationdetail/tcp")]
        public IQueryable<communicationdetail> GetTcp()
        {
            var communicationtypeid = GetTCPTypeId();
            return communicationDetailService.FindBy(x => x.isdeleted == 0 || x.isdeleted == null && communicationtypeid.Contains(x.communicationtypeid));
        }

        // GET api/communicationdetail
        [HttpGet]
        [Route("api/communicationdetail/serial")]
        public IQueryable<communicationdetail> GetSerial()
        {
            var communicationtypeid = GetSerialTypeId();
            return communicationDetailService.FindBy(x => x.isdeleted == 0 || x.isdeleted == null && communicationtypeid.Contains(x.communicationtypeid));
        }

        // GET api/communicationdetail
        [HttpGet]
        [Route("api/communicationdetail/unlinked")]
        public IQueryable<communicationdetail> GetUnlinked()
        {
            IEnumerable<long?> linkedConvertors = communicationDetailLinkService.GetAll().Select(x => x.converterid);
            var communicationtypeid = GetTCPTypeId();
            //var communicationtypeid = prmGlobalService.FindBy(prm => prm.prmmodule.ToLower() == "communicationtype" && prm.prmvalue == "TCP").Select(x => x.prmrecid).FirstOrDefault();
            IQueryable<communicationdetail> communicationDetails = communicationDetailService.GetUnlinked(linkedConvertors, communicationtypeid);
            return communicationDetails;
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

        [HttpGet]
        [Route("api/communicationdetail/unlinkedserial")]
        public IQueryable<communicationdetail> GetUnlinkedSerial()
        {
            IEnumerable<long?> linkedConvertors = communicationDetailLinkService.GetAll().Select(x => x.converterid);
            var communicationtypeid = GetSerialTypeId();
            //var communicationtypeid = prmGlobalService.FindBy(prm => prm.prmmodule.ToLower() == "communicationtype" && prm.prmvalue == "Serial").Select(x => x.prmrecid).FirstOrDefault();
            IQueryable<communicationdetail> communicationDetails = communicationDetailService.GetUnlinked(linkedConvertors, communicationtypeid);
            return communicationDetails;
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

        // GET api/communicationdetail/5
        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            var CommunicationDetail = await communicationDetailService.Get(id);
            return Ok(CommunicationDetail);
        }

        // POST api/communicationdetail
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]communicationdetail _CommunicationDetail)
        {
            var CommunicationDetail = await communicationDetailService.Add(_CommunicationDetail);
            return CreatedAtRoute("DefaultApi", new { id = CommunicationDetail.id }, CommunicationDetail);
        }

        // PUT api/communicationdetail/5
        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, [FromBody]communicationdetail _CommunicationDetail)
        {
            if (id != _CommunicationDetail.id)
            {
                throw new Exception("Invalid Meter Model");
            }
            var CommunicationDetail = await communicationDetailService.Edit(_CommunicationDetail);
            return Ok(CommunicationDetail);
        }

        // DELETE api/communicationdetail/5
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var _CommunicationDetail = await communicationDetailService.Get(id);
            if (_CommunicationDetail == null)
            {
                throw new Exception("Invalid Meter Model");
            }
            CheckIsActive(_CommunicationDetail);
            var CommunicationDetail = await communicationDetailService.Delete(_CommunicationDetail);
            return Ok(CommunicationDetail);
        }
        private void CheckIsActive(communicationdetail _mstuser)
        {
            var check = _mstuser.isactive == null || _mstuser.isactive == 0 ? false : true;
            if (check)
            {
                throw new Exception("Can't delete,already in use");
            }
        }
    }
}