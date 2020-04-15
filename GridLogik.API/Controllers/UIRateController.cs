using Domain.Entities;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GridLogik.API.Controllers
{
    public class UIRateController : ApiController
    {
        IUirateService _UirateService;
        IPrmGlobalService prmGlobalService;

        public UIRateController(IUirateService UirateService, IPrmGlobalService prmGlobalService)
        {
            _UirateService = UirateService;
            this.prmGlobalService = prmGlobalService;
        }

        public IQueryable<uirate> Get()
        {
            return _UirateService.GetAll().Where(model => model.isdeleted == 1);
        }

        public async Task<IHttpActionResult> Get(int id)
        {
            var uiratemodel = await _UirateService.Get(id);
            return Ok(uiratemodel);
        }
        [Route("api/UIRate/CheckUpload/{tstamp}")]
        public IQueryable<uirate> CheckUpload(string tstamp)
        {
            tstamp = tstamp + " 12:00 AM";

            string[] parts = tstamp.Split('-');
            string newdt = parts[0] + "/" + parts[1] + "/" + parts[2];

            var dateformat = prmGlobalService.FindBy(x => x.prmunit == "DateFieldcs").Select(x => x.prmvalue).FirstOrDefault();
            dateformat = dateformat == null ? "dd/MM/yyyy hh:mm tt" : dateformat;

            var tstamps = DateTime.ParseExact(newdt, dateformat, CultureInfo.InvariantCulture).AddMinutes(15);

            var uiratemodel = _UirateService.GetAll().Where(model => model.tstamp == tstamps);
            return uiratemodel;
        }

        [Route("api/UIRate/GetData/{tstamp}/{stageid}")]
        public IQueryable<uirate> GetData(string tstamp, string stageid)
        {
            tstamp = tstamp + " 12:00 AM";

            string[] parts = tstamp.Split('-');
            string newdt = parts[0] + "/" + parts[1] + "/" + parts[2];
            var dateformat = prmGlobalService.FindBy(x => x.prmunit == "DateFieldcs").Select(x => x.prmvalue).FirstOrDefault();
            dateformat = dateformat == null ? "dd/MM/yyyy hh:mm tt" : dateformat;


            DateTime newdate = DateTime.ParseExact(newdt, dateformat, CultureInfo.InvariantCulture).AddMinutes(-30);
            DateTime Nextdate = DateTime.ParseExact(newdt, dateformat, CultureInfo.InvariantCulture).AddDays(1).AddMinutes(15);

            var stageids = Convert.ToInt64(stageid);
            var tstamptosend = Convert.ToDateTime(tstamp);
            DateTime dt = Convert.ToDateTime(tstamp);
            int RevisionNo = MaxRevisionDetail(Convert.ToString(stageids), newdate, Nextdate);

            var uiratemodel = _UirateService.GetAll().Where(model => model.stageid == stageids && (model.tstamp > newdate && model.tstamp < Nextdate) && model.revision == RevisionNo);
            return uiratemodel;
        }

        [Route("api/UIRate/GetData/{tstamp}/{stageid}/{revisionid}")]
        public IQueryable<uirate> GetData(string tstamp, string stageid, int revisionid)
        {
            tstamp = tstamp + " 12:00 AM";

            string[] parts = tstamp.Split('-');
            string newdt = parts[0] + "/" + parts[1] + "/" + parts[2];
            var dateformat = prmGlobalService.FindBy(x => x.prmunit == "DateFieldcs").Select(x => x.prmvalue).FirstOrDefault();
            dateformat = dateformat == null ? "dd/MM/yyyy hh:mm tt" : dateformat;


            DateTime newdate = DateTime.ParseExact(newdt, dateformat, CultureInfo.InvariantCulture).AddMinutes(-30);
            DateTime Nextdate = DateTime.ParseExact(newdt, dateformat, CultureInfo.InvariantCulture).AddDays(1).AddMinutes(15);
            var stageids = Convert.ToInt64(stageid);
            var uiratemodel = _UirateService.GetAll().Where(model => model.revision == revisionid && (model.tstamp > newdate && model.tstamp < Nextdate) && model.stageid == stageids);
            return uiratemodel;
        }




        public int MaxRevisionDetail(string stageid, DateTime newdate, DateTime Nextdate)
        {

            int Istageid = Convert.ToInt32(stageid);
            List<uirate> records = _UirateService.GetAll().Where(model => model.tstamp > newdate && model.tstamp < Nextdate && (model.stageid == Istageid)).ToList();


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

        [Route("api/UIRate/GetDataRevision/{revisionid}")]
        public IQueryable<uirate> GetDataRevision(int revisionid)
        {
            var uiratemodel = _UirateService.GetAll().Where(model => model.revision == revisionid);
            return uiratemodel;
        }

        [Route("api/UIRate/GetDataRevision/{tstamp}/{stageid}/{revisionid}")]
        public IQueryable<uirate> GetDataRevision(string tstamp, string stageid, int revisionid)
        {
            tstamp = tstamp + " 12:00 AM";

            string[] parts = tstamp.Split('-');
            string newdt = parts[0] + "/" + parts[1] + "/" + parts[2];
            var dateformat = prmGlobalService.FindBy(x => x.prmunit == "DateFieldcs").Select(x => x.prmvalue).FirstOrDefault();
            dateformat = dateformat == null ? "dd/MM/yyyy hh:mm tt" : dateformat;


            DateTime newdate = DateTime.ParseExact(newdt, dateformat, CultureInfo.InvariantCulture).AddMinutes(-30);
            DateTime Nextdate = DateTime.ParseExact(newdt, dateformat, CultureInfo.InvariantCulture).AddDays(1).AddMinutes(15);
            var stageids = Convert.ToInt64(stageid);
            var uiratemodel = _UirateService.GetAll().Where(model => model.revision == revisionid && (model.tstamp > newdate && model.tstamp < Nextdate) && model.stageid == stageids);
            return uiratemodel;
        }

        [Route("api/UIRate/GetDataDistinct/{tstamp}/{stageid}")]
        public IQueryable<uirate> GetDataDistinct(string tstamp, string stageid)
        {
            tstamp = tstamp + " 12:00 AM";

            string[] parts = tstamp.Split('-');
            string newdt = parts[0] + "/" + parts[1] + "/" + parts[2];
            var dateformat = prmGlobalService.FindBy(x => x.prmunit == "DateFieldcs").Select(x => x.prmvalue).FirstOrDefault();
            dateformat = dateformat == null ? "dd/MM/yyyy hh:mm tt" : dateformat;


            DateTime newdate = DateTime.ParseExact(newdt, dateformat, CultureInfo.InvariantCulture).AddMinutes(-30);
            DateTime Nextdate = DateTime.ParseExact(newdt, dateformat, CultureInfo.InvariantCulture).AddDays(1).AddMinutes(15);
            var stageids = Convert.ToInt64(stageid);
            var uiratemodel = _UirateService.GetAllDistinct().Where(model => model.stageid == stageids && (model.tstamp > newdate && model.tstamp < Nextdate));

            return uiratemodel;
        }


        public async Task<IHttpActionResult> Post([FromBody]List<uirate> _uiratemodel)
        {
            
            var uiratetageid = _uiratemodel[0].stageid;
            var revision = _uiratemodel[0].revision;



            DateTime temdata = (DateTime)_uiratemodel[0].tstamp;

            string tempdate = temdata.ToString("dd/MM/yyyy hh:mm tt");

            
            var dateformat = prmGlobalService.FindBy(x => x.prmunit == "DateFieldcs").Select(x => x.prmvalue).FirstOrDefault();
            dateformat = dateformat == null ? "dd/MM/yyyy hh:mm tt" : dateformat;


            DateTime newdate = DateTime.ParseExact(tempdate, dateformat, CultureInfo.InvariantCulture).AddMinutes(-30);
            DateTime Nextdate = DateTime.ParseExact(tempdate, dateformat, CultureInfo.InvariantCulture).AddDays(1).AddMinutes(15);
            var uirate = _UirateService.GetAll().Where(model => model.stageid == uiratetageid && (model.tstamp > newdate && model.tstamp < Nextdate) && model.revision == revision);
            if (uirate.Count() > 0)
            {
                foreach (var dcsg in uirate)
                {
                    await _UirateService.Remove(dcsg);
                }
            }

            foreach (var i in _uiratemodel)
            {
                i.upload_date = DateTime.Now;
                var uiratemodel = await _UirateService.Add(i);
            }
            return Ok(_uiratemodel);
        }


        [Route("api/UIRate/PostData")]
        public async Task<IHttpActionResult> PostData([FromBody]List<uirate> _uiratemodel)
        {
            DateTime temdata = (DateTime)_uiratemodel[0].tstamp;

            string tempdate = temdata.ToString("dd/MM/yyyy hh:mm tt");


            var dateformat = prmGlobalService.FindBy(x => x.prmunit == "DateFieldcs").Select(x => x.prmvalue).FirstOrDefault();
            dateformat = dateformat == null ? "dd/MM/yyyy hh:mm tt" : dateformat;


            DateTime newdate = DateTime.ParseExact(tempdate, dateformat, CultureInfo.InvariantCulture).AddMinutes(-30);
            DateTime Nextdate = DateTime.ParseExact(tempdate, dateformat, CultureInfo.InvariantCulture).AddDays(1).AddMinutes(15);

            DateTime dt = DateTime.ParseExact(tempdate, dateformat, CultureInfo.InvariantCulture);
            int stage = Convert.ToInt32(_uiratemodel[0].stageid);
            int RevisionNo = MaxRevisionDetail(dt, Convert.ToString(_uiratemodel[0].stageid));

            var uiratetageid = _uiratemodel[0].stageid;
            
            var uirate = _UirateService.GetAll().Where(model => model.stageid == uiratetageid && (model.tstamp > newdate && model.tstamp < Nextdate) && model.revision == RevisionNo);
            if (uirate.Count() > 0)
            {
                foreach (var j in uirate)
                {
                    j.isdeleted = 1;
                    var ds = await _UirateService.Edit(j);
                }
            }

            foreach (var i in _uiratemodel)
            {
                i.upload_date = DateTime.Now;
                i.revision = RevisionNo + 1;
                var uiratemodel = await _UirateService.Add(i);
            }
            return Ok(_uiratemodel);
        }


        [Route("api/UIRate/MaxRevisionDetail/{tstamp}/{stageid}")]
        public int MaxRevisionDetail(DateTime tstamp, string stageid)
        {

            int Istageid = Convert.ToInt32(stageid);
            string tempdate = tstamp.ToString("dd/MM/yyyy hh:mm tt");



            var dateformat = prmGlobalService.FindBy(x => x.prmunit == "DateFieldcs").Select(x => x.prmvalue).FirstOrDefault();
            dateformat = dateformat == null ? "dd/MM/yyyy hh:mm tt" : dateformat;


            DateTime newdate = DateTime.ParseExact(tempdate, dateformat, CultureInfo.InvariantCulture).AddMinutes(-30);
            DateTime Nextdate = DateTime.ParseExact(tempdate, dateformat, CultureInfo.InvariantCulture).AddDays(1).AddMinutes(15);

            List<uirate> records = _UirateService.GetAll().Where(model => model.tstamp > newdate && model.tstamp < Nextdate && (model.stageid == Istageid)).ToList();


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



        public async Task<IHttpActionResult> Put(int id, [FromBody]uirate _uiratemodel)
        {
            if (id != _uiratemodel.id)
            {
                throw new Exception("Invalid Uirate Model");
            }
            var uiratemodel = await _UirateService.Edit(_uiratemodel);

            return Ok(uiratemodel);
        }


        public async Task<IHttpActionResult> Delete(int id)
        {
            var uiratemodel = await _UirateService.Get(id);
            if (uiratemodel == null)
            {
                throw new Exception("Invalid Uirate Model");
            }
            uiratemodel.isdeleted = 1;
            var mstpassmodel = await _UirateService.Delete(uiratemodel);
            return Ok(mstpassmodel);
        }


    }
}
