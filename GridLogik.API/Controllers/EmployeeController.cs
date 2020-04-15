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
    public class EmployeeController : ApiController
    {
        IEmployeeService _employeeService;
        IUserService _userService;
        public EmployeeController(IEmployeeService employeeService, IUserService userService)
        {
            _employeeService = employeeService;
            _userService = userService;
        }
        // GET api/employee
        public IQueryable<mstemployee> Get()
        {
            return _employeeService.GetAll().Where(x => x.empisdeleted == 0 || x.empisdeleted == null);
        }

        // GET api/employee/getunusedemployee/
        [Route("api/employee/getunusedemployee/")]
        public IQueryable<mstemployee> GetUnusedEmployee()
        {
            return _employeeService.GetUnusedEmployees();
        }

        // GET api/employee/5
        public async Task<IHttpActionResult> Get(int id)
        {
            var mstemployee = await _employeeService.Get(id);
            return Ok(mstemployee);
        }

        // POST api/employee
        public async Task<IHttpActionResult> Post([FromBody]mstemployee _mstemployee)
        {
            Check(_mstemployee);
            var mstemployee = await _employeeService.Add(_mstemployee);

            return CreatedAtRoute("DefaultApi", new { id = mstemployee.emprecid }, mstemployee);

        }

        private void Check(mstemployee _mstemployee)
        {
            var check = _employeeService.FindBy(x => x.empid.ToLower() == _mstemployee.empid.ToLower() && x.emprecid != _mstemployee.emprecid && (x.empisdeleted == 0 || x.empisdeleted == null)).Count() > 0;
            if (check)
            {
                throw new Exception("Employee Id Already Exists!");
            }

            var check2 = _employeeService.FindBy(x => x.empemailid.ToLower() == _mstemployee.empemailid.ToLower() && x.emprecid != _mstemployee.emprecid && (x.empisdeleted == 0 || x.empisdeleted == null)).Count() > 0;
            if (check2)
            {
                throw new Exception("Email Id Already Exists!");
            }
        }

        // PUT api/employee/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]mstemployee _mstemployee)
        {
            if (!(ModelState.IsValid))
            {
                throw new Exception("Invalid Object");
            }
            if (id != _mstemployee.emprecid)
            {
                throw new Exception("Invalid Employee");
            }
            Check(_mstemployee);

            var mstemployee = await _employeeService.Edit(_mstemployee);

            return Ok(mstemployee);
        }

        // DELETE api/employee/5
        public async Task<IHttpActionResult> Delete(int id)
        {

            var _mstemployee = await _employeeService.Get(id);
            if (_mstemployee == null)
            {
                throw new Exception("Invalid Employee");
            }

            CheckEmployeeAccess(_mstemployee);
            var mstuser = await _employeeService.Delete(_mstemployee);

            return Ok(mstuser);
        }

        private void CheckEmployeeAccess(mstemployee _mstemployee)
        {
            var empstr = _mstemployee.emprecid.ToString();
            var check = _userService.FindBy(x => x.usremployeeid == empstr).Count() > 0;// _mstuser.usremployeeid == null || _mstuser.usremployeeid == "0" ? false : true;
            if (check)
            {
                throw new Exception("Can't delete,already in use");
            }
        }
    }
}
