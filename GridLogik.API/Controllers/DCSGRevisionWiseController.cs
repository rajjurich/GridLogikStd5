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
    public class DCSGRevisionWiseController : ApiController
    {
        IDCSGFuelStagedService _DCSGFuelStagedService;
        IPrmGlobalService prmGlobalService;

        public DCSGRevisionWiseController(IDCSGFuelStagedService DCSGFuelStagedService, IPrmGlobalService prmGlobalService)
        {
            _DCSGFuelStagedService = DCSGFuelStagedService;
            this.prmGlobalService = prmGlobalService;
        }

        public IQueryable<dcsg> Get()
        {
            return _DCSGFuelStagedService.GetAll().Where(model => model.isdeleted == 1);
        }

        public async Task<IHttpActionResult> Get(int id)
        {
            var dcgmodel = await _DCSGFuelStagedService.Get(id);
            return Ok(dcgmodel);
        }

        [Route("api/DCSGRevisionWise/CheckUpload/{tstamp}")]
        public IQueryable<dcsg> CheckUpload(string tstamp)
        {
            tstamp = tstamp + " 12:00 AM";

            string[] parts = tstamp.Split('-');
            string newdt = parts[0] + "/" + parts[1] + "/" + parts[2];

            var dateformat = prmGlobalService.FindBy(x => x.prmunit == "DateFieldcs").Select(x => x.prmvalue).FirstOrDefault();
            dateformat = dateformat == null ? "dd/MM/yyyy hh:mm tt" : dateformat;

            var tstamps = DateTime.ParseExact(newdt, dateformat, CultureInfo.InvariantCulture).AddMinutes(15);

            var dcgmodel = _DCSGFuelStagedService.GetAll().Where(model => model.tstamp == tstamps);
            return dcgmodel;
        }

        [Route("api/DCSGRevisionWise/MaxRevisionDetail/{tstamp}/{stageid}")]
        public int MaxRevisionDetail(DateTime tstamp, string stageid)
        {

            int Istageid = Convert.ToInt32(stageid);

            

            string tempdate = tstamp.ToString("dd/MM/yyyy hh:mm tt");

            

            var dateformat = prmGlobalService.FindBy(x => x.prmunit == "DateFieldcs").Select(x => x.prmvalue).FirstOrDefault();
            dateformat = dateformat == null ? "dd/MM/yyyy hh:mm tt" : dateformat;


            DateTime newdate = Convert.ToDateTime(tempdate);
            DateTime Nextdate = Convert.ToDateTime(tempdate).AddDays(1).AddMinutes(15);

            List<dcsg> records = _DCSGFuelStagedService.GetAll().Where(model =>model.tstamp > newdate && model.tstamp < Nextdate && (model.stageid ==Istageid)).ToList();


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

        [Route("api/DCSGRevisionWise/GetData/{tstamp}/{stageid}")]
        public IQueryable<dcsg> GetData(string tstamp, string stageid)
        {
            tstamp = tstamp + " 12:00 AM";

            string[] parts = tstamp.Split('-');
            string newdt = parts[0] + "/" + parts[1] + "/" + parts[2];

            var dateformat = prmGlobalService.FindBy(x => x.prmunit == "DateFieldcs").Select(x => x.prmvalue).FirstOrDefault();
            dateformat = dateformat == null ? "dd/MM/yyyy hh:mm tt" : dateformat;


            DateTime newdate = DateTime.ParseExact(newdt, dateformat, CultureInfo.InvariantCulture);
            DateTime Nextdate = DateTime.ParseExact(newdt, dateformat, CultureInfo.InvariantCulture).AddDays(1).AddMinutes(15);

            var stageids = Convert.ToInt64(stageid);
            var dcgmodel = _DCSGFuelStagedService.GetAll().Where(model => model.stageid == stageids && (model.tstamp > newdate && model.tstamp < Nextdate));
            
            return dcgmodel;
        }


        #region dcsgNormal api
        // dcsg page start
        [Route("api/DCSGRevisionWise/GetDataNormal/{tstamp}")]
        public IQueryable<dcsg> GetDataNormal(string tstamp)
        {
            tstamp = tstamp + " 12:00 AM";

            string[] parts = tstamp.Split('-');
            string newdt = parts[0] + "/" + parts[1] + "/" + parts[2];

            var dateformat = prmGlobalService.FindBy(x => x.prmunit == "DateFieldcs").Select(x => x.prmvalue).FirstOrDefault();
            dateformat = dateformat == null ? "dd/MM/yyyy hh:mm tt" : dateformat;


            DateTime newdate = DateTime.ParseExact(newdt, dateformat, CultureInfo.InvariantCulture);
            DateTime Nextdate = DateTime.ParseExact(newdt, dateformat, CultureInfo.InvariantCulture).AddDays(1).AddMinutes(15);

            int RevisionNo = MaxRevisionDetailNormal(newdate, Nextdate);


            var dcgmodel = _DCSGFuelStagedService.GetAll().Where(model => model.stageid == 1 && (model.tstamp > newdate && model.tstamp < Nextdate) && model.revision == RevisionNo);
            return dcgmodel;
        }


        public int MaxRevisionDetailNormal(DateTime newdate, DateTime Nextdate)
        {

            //int Istageid = Convert.ToInt32(stageid);
            List<dcsg> records = _DCSGFuelStagedService.GetAll().Where(model => model.tstamp > newdate && model.tstamp < Nextdate).ToList();


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


        // dcsg page end
        #endregion 



        #region dcsgfuel api
        // dcsgFuel stage start
        [Route("api/DCSGRevisionWise/GetDatafuelstaged/{tstamp}/{stageid}")]
        public IQueryable<dcsg> GetDatafuelstaged(string tstamp, string stageid)
        {
            tstamp = tstamp + " 12:00 AM";
            string[] parts = tstamp.Split('-');
            string newdt = parts[0] + "/" + parts[1] + "/" + parts[2];

            var dateformat = prmGlobalService.FindBy(x => x.prmunit == "DateFieldcs").Select(x => x.prmvalue).FirstOrDefault();
            dateformat = dateformat == null ? "dd/MM/yyyy hh:mm tt" : dateformat;


            DateTime newdate = DateTime.ParseExact(newdt, dateformat, CultureInfo.InvariantCulture);
            DateTime Nextdate = DateTime.ParseExact(newdt, dateformat, CultureInfo.InvariantCulture).AddDays(1).AddMinutes(15);

            var stageids = Convert.ToInt64(stageid);
            
            int RevisionNo = MaxRevisionDetailfuel(Convert.ToString(stageids), newdate, Nextdate);

            var dcgmodel = _DCSGFuelStagedService.GetAll().Where(model => model.stageid == stageids && (model.tstamp > newdate && model.tstamp < Nextdate) && model.revision == RevisionNo);
            return dcgmodel;
        }

        public int MaxRevisionDetailfuel(string stageid, DateTime newdate, DateTime Nextdate)
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


        [Route("api/DCSGRevisionWise/GetDataRevisionfuel/{revisionid}")]
        public IQueryable<dcsg> GetDataRevisionfuel(int revisionid)
        {
            var dcgmodel = _DCSGFuelStagedService.GetAll().Where(model => model.revision == revisionid);
            return dcgmodel;
        }

        // dcsgFuel stage end
#endregion 


        



        [Route("api/DCSGRevisionWise/GetDataDistinct/{tstamp}/{stageid}")]
        public IQueryable<dcsg> GetDataDistinct(string tstamp, string stageid)
        {
            tstamp = tstamp + " 12:00 AM";

            string[] parts = tstamp.Split('-');
            string newdt = parts[0] + "/" + parts[1] + "/" + parts[2];

            var dateformat = prmGlobalService.FindBy(x => x.prmunit == "DateFieldcs").Select(x => x.prmvalue).FirstOrDefault();
            dateformat = dateformat == null ? "dd/MM/yyyy hh:mm tt" : dateformat;


            DateTime newdate = DateTime.ParseExact(newdt, dateformat, CultureInfo.InvariantCulture);
            DateTime Nextdate = DateTime.ParseExact(newdt, dateformat, CultureInfo.InvariantCulture).AddDays(1).AddMinutes(15);


            var stageids = Convert.ToInt64(stageid);
            var dcgmodel = _DCSGFuelStagedService.GetAllDistinct().Where(model => model.stageid == stageids && (model.tstamp > newdate && model.tstamp < Nextdate));

            return dcgmodel;
        }


        [Route("api/DCSGRevisionWise/GetDataRevision/{tstamp}/{stageid}/{revisionid}")]
        public IQueryable<dcsg> GetDataRevision(string tstamp, string stageid, int revisionid)
        {
            tstamp = tstamp + " 12:00 AM";

            string[] parts = tstamp.Split('-');
            string newdt = parts[0] + "/" + parts[1] + "/" + parts[2];

            var dateformat = prmGlobalService.FindBy(x => x.prmunit == "DateFieldcs").Select(x => x.prmvalue).FirstOrDefault();
            dateformat = dateformat == null ? "dd/MM/yyyy hh:mm tt" : dateformat;


            DateTime newdate = DateTime.ParseExact(newdt, dateformat, CultureInfo.InvariantCulture);
            DateTime Nextdate = DateTime.ParseExact(newdt, dateformat, CultureInfo.InvariantCulture).AddDays(1).AddMinutes(15);

            var stageids = Convert.ToInt64(stageid);
            var dcgmodel = _DCSGFuelStagedService.GetAll().Where(model => model.revision == revisionid && (model.tstamp > newdate && model.tstamp < Nextdate) && model.stageid == stageids);
            return dcgmodel;
        }


        public async Task<IHttpActionResult> Post([FromBody]List<dcsg> _dcsgmodel)
        {

            var dateformat = prmGlobalService.FindBy(x => x.prmunit == "DateFieldcs").Select(x => x.prmvalue).FirstOrDefault();
            dateformat = dateformat == null ? "dd/MM/yyyy hh:mm tt" : dateformat;

            DateTime temdata = (DateTime)_dcsgmodel[0].tstamp;

            string tempdate = temdata.ToString("dd/MM/yyyy hh:mm tt");

            //logic for max revision
            DateTime dt = Convert.ToDateTime(tempdate);
            int stage = Convert.ToInt32(_dcsgmodel[0].stageid);
            int RevisionNo = MaxRevisionDetail(dt, Convert.ToString(_dcsgmodel[0].stageid));
            
            
            // logic for update old one to is deleted 1
            DateTime newdate = Convert.ToDateTime(tempdate);
            DateTime Nextdate = Convert.ToDateTime(tempdate).AddDays(1).AddMinutes(15);

            IEnumerable<dcsg> listdcsgupdate=_DCSGFuelStagedService.GetAll().Where(model => model.tstamp > newdate && model.tstamp < Nextdate);

            foreach(var j in listdcsgupdate)
            {
                j.isdeleted = 1;
                var ds = await _DCSGFuelStagedService.Edit(j);
            }

            foreach (var i in _dcsgmodel)
            {
                i.isdeleted = 0;
                i.revision = RevisionNo + 1;
                var dcsgmodel = await _DCSGFuelStagedService.Add(i);
            }
            return Ok(_dcsgmodel);
        }



        [Route("api/DCSGRevisionWise/GetData/{tstamp}/{stageid}/{revision}")]
        public IQueryable<dcsg> GetData(string tstamp, string stageid,int revision)
        {
            tstamp = tstamp + " 12:00 AM";

            string[] parts = tstamp.Split('-');
            string newdt = parts[0] + "/" + parts[1] + "/" + parts[2];
            var dateformat = prmGlobalService.FindBy(x => x.prmunit == "DateFieldcs").Select(x => x.prmvalue).FirstOrDefault();
            dateformat = dateformat == null ? "dd/MM/yyyy hh:mm tt" : dateformat;


            DateTime newdate = DateTime.ParseExact(newdt, dateformat, CultureInfo.InvariantCulture);
            DateTime Nextdate = DateTime.ParseExact(newdt, dateformat, CultureInfo.InvariantCulture).AddDays(1).AddMinutes(15);


            var stageids = Convert.ToInt64(stageid);
            var dcgmodel = _DCSGFuelStagedService.GetAll().Where(model => model.stageid == stageids && (model.tstamp > newdate && model.tstamp < Nextdate) && model.revision==revision);
            return dcgmodel;
        }

    }
}
