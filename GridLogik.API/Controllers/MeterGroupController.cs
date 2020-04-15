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
    public class MeterGroupController : ApiController
    {
        IMeterGroupService meterGroupService;
        IMeterGroupDetailService meterGroupDetailService;
        public MeterGroupController(IMeterGroupService meterGroupDetailService
            , IMeterGroupDetailService groupConfigurationService)
        {
            this.meterGroupService = meterGroupDetailService;
            this.meterGroupDetailService = groupConfigurationService;
        }

        // GET api/metergroup
        public IQueryable<metergroup> Get()
        {
            return meterGroupService.GetAll();
        }

        

        // GET api/metergroup/5
        public async Task<IHttpActionResult> Get(int id)
        {
            var meterGroup = await meterGroupService.Get(id);
            var groupConfigs = meterGroupDetailService.GetGroupConfigByGroupId(id);

            meterGroup.mstmetergroupdetails = groupConfigs.ToList();
            return Ok(meterGroup);
        }

        // POST api/metergroup
        public async Task<IHttpActionResult> Post([FromBody]metergroup _metergroup)
        {
            var metergroup = await meterGroupService.Add(_metergroup);
            await AddGroupConfigs(_metergroup, metergroup);

            return CreatedAtRoute("DefaultApi", new { id = metergroup.id }, metergroup);
        }

        private async Task AddGroupConfigs(metergroup _metergroup, Domain.Entities.metergroup metergroup)
        {
            foreach (var groupconfig in _metergroup.mstmetergroupdetails)
            {
                groupconfig.grpid = metergroup.id;
                await meterGroupDetailService.Add(groupconfig);
            }
        }

        // PUT api/metergroup/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]metergroup _metergroup)
        {
            var metergroup = await meterGroupService.Get(id);
            metergroup.groupname = _metergroup.groupname;
            await meterGroupService.Edit(metergroup);

            await DeleteGroupConfigs(id);

            await AddGroupConfigs(_metergroup, metergroup);
            return Ok(metergroup);
        }

        private async Task DeleteGroupConfigs(int id)
        {
            var groupConfigs = meterGroupDetailService.GetGroupConfigByGroupId(id);
            foreach (var groupConfig in groupConfigs)
            {
                await meterGroupDetailService.Remove(groupConfig);
            }
        }

        // DELETE api/metergroup/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            var _meterGroup = await meterGroupService.Get(id);
            if (_meterGroup == null)
            {
                throw new Exception("Invalid meter group");
            }
            await DeleteGroupConfigs(id);

            var meterGroup = await meterGroupService.Delete(_meterGroup);
            return Ok(meterGroup);
        }
    }
}
