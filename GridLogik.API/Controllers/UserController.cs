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
using System.Data.Entity;
using System.Reflection;
namespace GridLogik.API.Controllers
{
    public class UserController : ApiController
    {
        IUserService _userService;
        IUnitOfWork _unitOfWork;

        public UserController(IUserService userService, IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        // GET api/user
        public IQueryable<mstuser> Get()
        {
            return _userService.GetAll();
        }

        // GET api/user/5
        public async Task<IHttpActionResult> Get(int id)
        {
            var mstuser = await _userService.Get(id);
            if (mstuser == null)
            {
                throw new Exception("Invalid User"); 
            }
            else
            {
                return Ok(mstuser);
            }
        }

        [Route("api/authentication/login")]
        public async Task<IHttpActionResult> Post([FromBody]UserLogin _mstuser)
        {

            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            string versiontrimed = version.Substring(0, 4);
            if (versiontrimed != _mstuser.verionsofwebapp)
            {
                throw new Exception("Version Doesnt Match");
            }
            mstuser mstuser = await _userService.FindBy(x => x.usrid == _mstuser.usrid && x.usrpassword == _mstuser.usrpassword).FirstOrDefaultAsync();
            if (mstuser == null)
            {
                throw new Exception("Invalid User");
            }
            else
            {
                return Ok(mstuser);                
            }
        }

        // POST api/user
        public async Task<IHttpActionResult> Post([FromBody]mstuser _mstuser)
        {
            Check(_mstuser);

            var mstuser = await _userService.Add(_mstuser);

            return CreatedAtRoute("DefaultApi", new { id = mstuser.usrrecid }, mstuser);
        }
        private void Check(mstuser _mstrole)
        {
            var check = _userService.FindBy(x => x.usrid.ToLower() == _mstrole.usrid.ToLower() && x.usrrecid != _mstrole.usrrecid && (x.usrisdeleted == 0 || x.usrisdeleted == null)).Count() > 0;
            if (check)
            {
                throw new Exception("User Id Already Exists!");
            }
        }
        // PUT api/user/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]mstuser _mstuser)
        {
            if (!(ModelState.IsValid))
            {
                throw new Exception("Invalid Object");
            }
            if (id != _mstuser.usrrecid)
            {
                throw new Exception("Invalid user");
            }
            Check(_mstuser);
            
            var mstuser = await _userService.Edit(_mstuser);

            return Ok(mstuser);
        }

        // DELETE api/user/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            var _mstuser = await _userService.Get(id);
            if (_mstuser == null)
            {
                throw new Exception("Invalid user");
            }

            CheckUserAccess(_mstuser); 
            var mstuser = await _userService.Delete(_mstuser);

            return Ok(mstuser);
        }

        private void CheckUserAccess(mstuser _mstuser)
        {
            var check = _mstuser.usremployeeid == null || _mstuser.usremployeeid == "0" ? false : true;
            if (check)
            {
                throw new Exception("Can't delete,already in use");
            }
        }
        
    }
}
