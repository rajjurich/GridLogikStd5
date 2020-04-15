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
    public class ChangePasswordController : ApiController
    {
        IChangePasswordService _ChangePasswordService;

        public ChangePasswordController(IChangePasswordService ChangePasswordService)
        {
            _ChangePasswordService = ChangePasswordService;
        }

        public mstuser Get(int id)
        {
            return _ChangePasswordService.GetAll().Where(x => x.usrrecid == id).FirstOrDefault();
        }
        [HttpPost]
        [Route("api/ChangePassword/ResetingPassword/{oldpassword}")]
        public async Task<IHttpActionResult> ResetingPassword(string oldpassword, [FromBody]mstuser _mstuser)
        {
            Check(oldpassword, _mstuser);
            var id=Convert.ToInt32(_mstuser.usrrecid);
            var getuser =  Get(id);
            getuser.usrpassword = _mstuser.usrpassword;
            var userchange = await _ChangePasswordService.Edit(getuser);

            return Ok(userchange);
        }


        private void Check(string password, mstuser _mstuser)
        {
            var check = _ChangePasswordService.FindBy(x => x.usrpassword == password && x.usrrecid == _mstuser.usrrecid && (x.usrisdeleted == 0 || x.usrisdeleted == null)).Count() > 0;
            if (check==false)
            {
                throw new Exception("Old Password Doesnt Match!");
            }
        }
    }
}
