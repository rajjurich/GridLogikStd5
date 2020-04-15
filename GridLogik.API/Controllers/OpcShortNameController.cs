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
    public class OpcShortNameController : ApiController
    {
        IOpcShortNameService _OpcShortNameService;

        public OpcShortNameController(IOpcShortNameService OpcShortNameService)
        {
            _OpcShortNameService = OpcShortNameService;
        }

        public IQueryable<clsOpcname> Get()
        {
            return _OpcShortNameService.GetAll();
        }
        [Route("api/OpcShortName/GetMeterOPC")]
        public IQueryable<meter> GetMeterOPC()
        {
            var meters = _OpcShortNameService.GetAllOPCNAME();
            return meters;
        }


        [Route("api/OpcShortName/GetMeterOPCID/{id}")]
        public IQueryable<meter> GetMeterOPCID(int id)
        {
            var meters = _OpcShortNameService.GetAllOPCNAMEWithId(id);
            return meters;
        }
        
        public async Task<IHttpActionResult> Get(int id)
        {
            var opc_meternamemodel = await _OpcShortNameService.Get(id);
            return Ok(opc_meternamemodel);
        }

        // POST api/opc_metername
        public async Task<IHttpActionResult> Post([FromBody]opc_metername _opc_metername)
        {
            Check(_opc_metername);
            CheckMeterName(_opc_metername);
            var opcmetermodel = await _OpcShortNameService.Add(_opc_metername);

            return CreatedAtRoute("DefaultApi", new { id = opcmetermodel.id }, opcmetermodel);
        }

        // PUT api/opc_metername/5
        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, [FromBody]opc_metername _opc_metername)
        {
            if (id != _opc_metername.id)
            {
                throw new Exception("Invalid OPC Model");
            }
            CheckEdit(_opc_metername);
            CheckEditMeterName(_opc_metername);
            var opcmetermodel = await _OpcShortNameService.Edit(_opc_metername);

            return Ok(opcmetermodel);
        }

        // DELETE api/opcmetermodel/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            var opcmetermodel = await _OpcShortNameService.Get(id);
            if (opcmetermodel == null)
            {
                throw new Exception("Invalid OPC Model");
            }

            var mstpassmodel = await _OpcShortNameService.Remove(opcmetermodel);
            return Ok(mstpassmodel);
        }

        private void Check(opc_metername _opc_metername)
        {
            var check = _OpcShortNameService.FindBy(x => x.opc_shortname.ToLower() == _opc_metername.opc_shortname.ToLower()).Count() > 0;
            if (check)
            {
                throw new Exception("OPC Name Already Exists!");
            }
        }

        private void CheckMeterName(opc_metername _opc_metername)
        {
            var check = _OpcShortNameService.FindBy(x => x.meterid == _opc_metername.meterid).Count() > 0;
            if (check)
            {
                throw new Exception("Meter Name Already Taken!");
            }
        }



        private void CheckEdit(opc_metername _opc_metername)
        {
            var tempstring = _opc_metername.opc_shortname;
            var check = _OpcShortNameService.FindBy(x => x.opc_shortname.ToLower() == _opc_metername.opc_shortname.ToLower() && x.id==_opc_metername.id).Count();

            var check2 = _OpcShortNameService.FindBy(x => x.opc_shortname.ToLower() == _opc_metername.opc_shortname.ToLower() && x.id != _opc_metername.id).Count();

            if (check==0 && check2>0)
            {
                throw new Exception("OPC Name Already Exists!");
            }
        }

        private void CheckEditMeterName(opc_metername _opc_metername)
        {
            var check = _OpcShortNameService.FindBy(x => x.meterid == _opc_metername.meterid && x.id == _opc_metername.id).Count();

            var check2 = _OpcShortNameService.FindBy(x => x.meterid == _opc_metername.meterid && x.id != _opc_metername.id).Count();

            if (check == 0 && check2 > 0)
            {
                throw new Exception("Meter Name Already Exists!");
            }
        }

    }
}
