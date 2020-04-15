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
    public class MenuAccessController : ApiController
    {
        IMenuAccessService _menuAccessService;
        public MenuAccessController(IMenuAccessService menuAccessService)
        {
            _menuAccessService = menuAccessService;
        }
        // GET api/menuaccess
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/menuaccess/5
        [Route("api/menuaccess/accesscheck/{ctr}/{act}/{id}")]
        public async Task<mstrolemenuaccess> Get(string ctr, string act, int id)
        {
            return await _menuAccessService.GetByUserId(string.Format("/{0}/{1}", ctr, act), id);
        }

        // GET api/menuaccess/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/menuaccess
        public void Post([FromBody]string value)
        {
        }

        // PUT api/menuaccess/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/menuaccess/5
        public void Delete(int id)
        {
        }
    }
}
