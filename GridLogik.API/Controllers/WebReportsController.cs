using Domain.Entities;
using Domain.Extension;
using Domain.Model;
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
    public class WebReportsController : ApiController
    {
        private IWebReportsService webReportsService;
        private IRoleService roleService;
        public WebReportsController(IWebReportsService webReportsService
            , IRoleService roleService)
        {
            this.webReportsService = webReportsService;
            this.roleService = roleService;
        }
        // GET api/webreports
        public IQueryable<ClientFolderMap> Get()
        {
            IQueryable<clientfoldermap> clientfoldermaps = webReportsService.GetAll();

            return ToClientFolderMaps(clientfoldermaps);
        }

        private IQueryable<ClientFolderMap> ToClientFolderMaps(IQueryable<clientfoldermap> clientfoldermaps)
        {
            List<ClientFolderMap> clientFolderMaps = new List<ClientFolderMap>();
            foreach (var clientfoldermap in clientfoldermaps)
            {
                ClientFolderMap clientFolderMap = new ClientFolderMap();
                clientFolderMap.id = clientfoldermap.id;
                clientFolderMap.folderpath = clientfoldermap.folderpath;
                clientFolderMap.roleid = clientfoldermap.roleid;
                clientFolderMap.RoleName = roleService.Get(Convert.ToInt32(clientfoldermap.roleid)).Result.rolname;

                clientFolderMaps.Add(clientFolderMap);
            }
            return clientFolderMaps.AsQueryable();
        }

        // GET api/webreports/5
        public async Task<IHttpActionResult> Get(int id)
        {
            return Ok(await webReportsService.Get(id));
        }

        // GET api/webreports/GetPathsByRole/5
        [Route("api/webreports/GetPathsByRole/{id}")]
        public IQueryable<ClientFolderMap> GetPathsByRole(int id)
        {
            var clientfoldermaps = webReportsService.FindBy(x => x.roleid == id);
            return ToClientFolderMaps(clientfoldermaps);
        }


        [HttpPost]
        [Route("api/webreports/WebReportFiles")]
        public IQueryable<FileInformation> WebReportFiles([FromBody]dynamic requestInfo)
        {
            return webReportsService.GetFiles(requestInfo);
        }


        // POST api/webreports
        public async Task<IHttpActionResult> Post([FromBody]clientfoldermap _clientfoldermap)
        {
            var clientfoldermap = await webReportsService.Add(_clientfoldermap);

            return CreatedAtRoute("DefaultApi", new { id = clientfoldermap.id }, clientfoldermap);
        }

        // PUT api/webreports/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]clientfoldermap _clientfoldermap)
        {
            var clientfoldermap = await webReportsService.Edit(_clientfoldermap);
            return Ok(clientfoldermap);
        }

        // DELETE api/webreports/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            var _clientfoldermap = await webReportsService.Get(id);

            var clientfoldermap = await webReportsService.Remove(_clientfoldermap);
            return Ok(clientfoldermap);
        }
    }
}
