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
using System.Data.Entity;

namespace GridLogik.API.Controllers
{
    public class RoleController : ApiController
    {
        IRoleService _roleservice;
        IUnitOfWork _unitOfWork;
        IUserService _userService;
        public RoleController(IRoleService roleservice, IUnitOfWork unitOfWork, IUserService userService)
        {
            _roleservice = roleservice;
            _unitOfWork = unitOfWork;
            _userService = userService;
        }
        // GET api/role
        public IQueryable<mstrole> Get()
        {
            return _roleservice.GetAll();
        }

        // GET api/role/5
        public async Task<IHttpActionResult> Get(int id)
        {
            var mstrole = await _roleservice.Get(id);
            return Ok(mstrole);
        }

        // POST api/role
        public async Task<IHttpActionResult> Post([FromBody]mstrole _mstrole)
        {
            Check(_mstrole);

            var mstrole = await _roleservice.Add(_mstrole);

            return CreatedAtRoute("DefaultApi", new { id = mstrole.rolrecid }, mstrole);
        }

        // PUT api/role/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]mstrole _mstrole)
        {
            Check(_mstrole);

            if (id != _mstrole.rolrecid)
            {
                throw new Exception("Invalid Role");
            }
            var mstrole = await _roleservice.Edit(_mstrole);

            return Ok(mstrole);
        }

        private void Check(mstrole _mstrole)
        {
            var check = _roleservice.FindBy(x => x.rolname.ToUpper() == _mstrole.rolname.ToUpper() && x.rolrecid != _mstrole.rolrecid && (x.rolisdeleted == 0 || x.rolisdeleted == null)).Count() > 0;
            if (check)
            {
                throw new Exception("Role Name Already Exists!");
            }
        }

        // DELETE api/role/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            var _role = await _roleservice.Get(id);
            if (_role == null)
            {
                throw new Exception("Invalid Role");
            }

            CheckRoleAccess(_role);

            var role = await _roleservice.Delete(_role);
            return Ok(role);
        }

        private void CheckRoleAccess(mstrole _role)
        {
            var check = _userService.FindBy(x => x.usrroleid == _role.rolrecid).Count() > 0;
            if (check)
            {
                throw new Exception("Can't delete,already in use");
            }
        }
    }
}
