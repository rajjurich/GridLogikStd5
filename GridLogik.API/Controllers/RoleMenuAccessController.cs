using Domain.Core;
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
    public class RoleMenuAccessController : ApiController
    {
        IRoleMenuAccessService _roleMenuAccessService;
        IUnitOfWork _unitOfWork;
        public RoleMenuAccessController(IRoleMenuAccessService roleMenuAccessService, IUnitOfWork unitOfWork)
        {
            _roleMenuAccessService = roleMenuAccessService;
            _unitOfWork = unitOfWork;
        }
        // GET api/rolemenuaccess
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/rolemenuaccess/GetRoleMenuAccessByRole/5
        [Route("api/rolemenuaccess/GetRoleMenuAccessByRole/{id}")]
        public IQueryable<mstrolemenuaccess> GetRoleMenuAccessByRole(int id)
        {
            return _roleMenuAccessService.GetRoleMenuAccessByRole(id);
        }

        // GET api/rolemenuaccess/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/rolemenuaccess
        public async Task<IHttpActionResult> Post([FromBody]IEnumerable<mstrolemenuaccess> mstrolemenuaccesses)
        {
            _unitOfWork.BeginTransaction();
            foreach (var mstrolemenuaccess in mstrolemenuaccesses)
            {
                if (mstrolemenuaccess.rmarecid > 0)
                {
                    await _roleMenuAccessService.Edit(mstrolemenuaccess);
                }
                else if (mstrolemenuaccess.rmarecid == 0)
                {
                    await _roleMenuAccessService.Add(mstrolemenuaccess);
                }
                //break;
            }
            //await _roleMenuAccessService.RemoveRange(mstrolemenuaccesses);
            //await _roleMenuAccessService.AddRange(mstrolemenuaccesses);
            return Ok(mstrolemenuaccesses);
        }

        // PUT api/rolemenuaccess/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/rolemenuaccess/5
        public void Delete(int id)
        {
        }


    }
}
