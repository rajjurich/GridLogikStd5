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
    public class ActionTypeController : ApiController
    {
        IActionTypeService _actiontypeobj;

        public ActionTypeController(IActionTypeService actiontypeobj)
        {
            _actiontypeobj = actiontypeobj;
        }
        //Get api/actiontype
        
        public IQueryable<actiontype> Get()
        {
            return _actiontypeobj.GetAll();
        }
        //Get api/actiontype/1

        public async Task<IHttpActionResult> Get(int id)
        {
            var actiontypemodel = await _actiontypeobj.Get(id);
            return Ok(actiontypemodel);
        }

        // POST api/actiontype
        public async Task<IHttpActionResult> Post([FromBody]actiontype _actiontypemodel)
        {
            var actiontypemodel = await _actiontypeobj.Add(_actiontypemodel);

            return CreatedAtRoute("DefaultApi", new { id = actiontypemodel.id }, actiontypemodel);
        }

        // PUT api/actiontypemodel/5
        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, [FromBody]actiontype _actiontypemodel)
        {
            if (id != _actiontypemodel.id)
            {
                throw new Exception("Invalid ActionType Model");
            }
            var actiontypemodel = await _actiontypeobj.Edit(_actiontypemodel);

            return Ok(actiontypemodel);
        }

        // DELETE api/actiontypemodel/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            var actiontypemodel = await _actiontypeobj.Get(id);
            if (actiontypemodel == null)
            {
                throw new Exception("Invalid ActionType Model");
            }
            actiontypemodel.isdeleted = 1;
            var mstpassmodel = await _actiontypeobj.Delete(actiontypemodel);
            return Ok(mstpassmodel);
        }
    }
}
