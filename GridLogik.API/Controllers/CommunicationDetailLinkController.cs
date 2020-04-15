using Domain.Entities;
using Domain.Services;
using GridLogik.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace GridLogik.API.Controllers
{

    public class CommunicationDetailLinkController : ApiController
    {
        ICommunicationDetailLinkService communicationDetailLinkService;
        ICommunicationDetailService communicationDetailService;
        IMeterService meterService;
        IPrmGlobalService prmGlobalService;
        //
        // GET: /communicationdetaillinkLinkLinkAPI/
        public CommunicationDetailLinkController(ICommunicationDetailLinkService communicationdetaillinkService
            , ICommunicationDetailService communicationDetailService
            , IMeterService meterService
            , IPrmGlobalService prmGlobalService
            )
        {
            this.communicationDetailLinkService = communicationdetaillinkService;
            this.communicationDetailService = communicationDetailService;
            this.meterService = meterService;
            this.prmGlobalService = prmGlobalService;

        }
        // GET api/CommunicationDetailLink
        [HttpGet]
        public IQueryable<communicationdetaillink> Get()
        {
            return communicationDetailLinkService.GetAll();
        }

        // GET api/communicationdetaillink/getalllistmodel
        [HttpGet]
        [Route("api/communicationdetaillink/GetAllListModel")]
        public IQueryable<CommunicationDetailLinkListModel> GetAllListModel()
        {
            var communicationtypeids = GetTCPTypeId();
            IEnumerable<long?> linkedConvertors = communicationDetailLinkService.FindBy(x => communicationtypeids.Contains(x.communicationdetail.communicationtypeid)).Select(x => x.converterid).Distinct();
            List<CommunicationDetailLinkListModel> communicationDetailLinkListModels = GetCommunicationDetailLinkList(linkedConvertors);

            return communicationDetailLinkListModels.AsQueryable();
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

        // GET api/communicationdetaillink/getalllistmodelserial
        [HttpGet]
        [Route("api/communicationdetaillink/GetAllListModelSerial")]
        public IQueryable<CommunicationDetailLinkListModel> GetAllListModelSerial()
        {
            var communicationtypeid = GetSerialTypeId();
            IEnumerable<long?> linkedConvertors = communicationDetailLinkService.FindBy(x => x.communicationdetail.communicationtypeid == communicationtypeid).Select(x => x.converterid).Distinct();
            List<CommunicationDetailLinkListModel> communicationDetailLinkListModels = GetCommunicationDetailLinkList(linkedConvertors);

            return communicationDetailLinkListModels.AsQueryable();
        }

        private long GetSerialTypeId()
        {
            var communicationtypeid = prmGlobalService.FindBy(prm => prm.prmmodule.ToLower() == "communicationtype" && prm.prmidentifier.ToLower() == "serial").Select(x => x.prmvalue).FirstOrDefault();
            return Convert.ToInt64(communicationtypeid);
        }

        // GET api/communicationdetaillink/getalllistmodelbyconvertorid/{id}
        [HttpGet]
        [Route("api/communicationdetaillink/GetByConvertorid/{id}")]
        public async Task<IHttpActionResult> GetByConvertorid(int id)
        {
            CommunicationDetailLinkCreateModel communicationDetailLinkCreateModel = await GetCommunicationDetailLinkCreateModel(id);


            return Ok(communicationDetailLinkCreateModel);
        }

        private async Task<CommunicationDetailLinkCreateModel> GetCommunicationDetailLinkCreateModel(int id)
        {
            IQueryable<communicationdetaillink> communicationDetailLinks = communicationDetailLinkService.FindBy(x => x.converterid == id);
            CommunicationDetailLinkCreateModel communicationDetailLinkCreateModel = new CommunicationDetailLinkCreateModel();
            communicationDetailLinkCreateModel.converterid = communicationDetailLinks.Select(x => x.converterid).FirstOrDefault();

            //var CommunicationDetailLink = await communicationDetailLinkService.Get(Convert.ToInt32(communicationDetailLinks.Select(x => x.id).FirstOrDefault()));

            List<MeterWithModBusId> meterWithModBusIds = new List<MeterWithModBusId>();
            foreach (var communicationDetailLink in communicationDetailLinks)
            {
                MeterWithModBusId meterWithModBusId = new MeterWithModBusId();
                meterWithModBusId.meterId = Convert.ToInt64(communicationDetailLink.meterid);
                meterWithModBusId.modbusid = communicationDetailLink.modbusid;
                var meter = await meterService.Get(Convert.ToInt32(communicationDetailLink.meterid));
                meterWithModBusId.meterName = meter.metername;
                meterWithModBusIds.Add(meterWithModBusId);
            }

            communicationDetailLinkCreateModel.meters = meterWithModBusIds;
            return communicationDetailLinkCreateModel;
        }

        private List<CommunicationDetailLinkListModel> GetCommunicationDetailLinkList(IEnumerable<long?> linkedConvertors)
        {
            List<CommunicationDetailLinkListModel> communicationDetailLinkListModels =
                new List<CommunicationDetailLinkListModel>();

            foreach (var linkedConvertor in linkedConvertors)
            {
                CommunicationDetailLinkListModel communicationDetailLinkListModel = new CommunicationDetailLinkListModel();
                communicationDetailLinkListModel.ConvertorId = linkedConvertor;
                var communicationDetail = communicationDetailService.Get(Convert.ToInt32(linkedConvertor)).Result;
                communicationDetailLinkListModel.IpAddress = communicationDetail.ipaddress;
                communicationDetailLinkListModel.Port = communicationDetail.comport;
                var communicationdetaillinks = communicationDetailLinkService.FindBy(x => x.converterid == linkedConvertor).ToList();

                StringBuilder sb = new StringBuilder();
                foreach (var communicationdetaillink in communicationdetaillinks)
                {
                    var metername = meterService.Get(Convert.ToInt32(communicationdetaillink.meterid)).Result.metername;
                    sb.Append(metername);
                    sb.Append(",");
                }


                int index = sb.ToString().LastIndexOf(',');
                string str = sb.ToString().Remove(index, 1);

                communicationDetailLinkListModel.MeterNames = str;

                communicationDetailLinkListModels.Add(communicationDetailLinkListModel);
            }
            return communicationDetailLinkListModels;
        }

        // GET api/CommunicationDetailLink/5
        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            var CommunicationDetailLink = await communicationDetailLinkService.Get(id);
            return Ok(CommunicationDetailLink);
        }

        // POST api/CommunicationDetailLink
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]CommunicationDetailLinkCreateModel communicationDetailLinkCreateModel)
        {
            await AddCommunicationDetailLink(communicationDetailLinkCreateModel);

            return CreatedAtRoute("DefaultApi", new { id = 1 }, communicationDetailLinkCreateModel);
        }

        private async Task AddCommunicationDetailLink(CommunicationDetailLinkCreateModel communicationDetailLinkCreateModel)
        {
            foreach (var item in communicationDetailLinkCreateModel.meters)
            {
                communicationdetaillink _CommunicationDetailLink = new communicationdetaillink()
                {
                    active = 1,
                    converterid = communicationDetailLinkCreateModel.converterid,
                    meterid = item.meterId,
                    modbusid = item.modbusid
                };
                var communicationdetaillink = await communicationDetailLinkService.Add(_CommunicationDetailLink);
            }
        }

        // PUT api/communicationdetaillinkLink/5
        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, [FromBody]CommunicationDetailLinkCreateModel communicationDetailLinkCreateModel)
        {
            await DeleteCommunicationDetailLink(id);

            await AddCommunicationDetailLink(communicationDetailLinkCreateModel);
            return Ok(communicationDetailLinkCreateModel);
        }

        private async Task DeleteCommunicationDetailLink(int id)
        {
            IQueryable<communicationdetaillink> communicationDetailLinks = communicationDetailLinkService.FindBy(x => x.converterid == id);
            foreach (var communicationDetailLink in communicationDetailLinks)
            {
                await communicationDetailLinkService.Remove(communicationDetailLink);
            }
        }

        // DELETE api/communicationdetaillinkLink/5
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            await DeleteCommunicationDetailLink(id);

            CommunicationDetailLinkCreateModel communicationDetailLinkCreateModel = await GetCommunicationDetailLinkCreateModel(id);


            return Ok(communicationDetailLinkCreateModel);
        }
    }
}