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
    public class ConsumerCategoryController : ApiController
    {
        IConsumerCategoryService consumerCategoryService;
        public ConsumerCategoryController(IConsumerCategoryService consumerCategoryService)
        {
            this.consumerCategoryService = consumerCategoryService;
        }
        // GET api/consumercategory
        public IQueryable<consumercategory> Get()
        {
            return consumerCategoryService.GetAll();
        }

        // GET api/consumercategory/5
        public async Task<IHttpActionResult> Get(int id)
        {
            var consumercategory = await consumerCategoryService.Get(id);
            return Ok(consumercategory);
        }

        // POST api/consumercategory
        public async Task<IHttpActionResult> Post([FromBody]consumercategory _consumercategory)
        {
            if (!(ModelState.IsValid))
            {
                throw new Exception("Invalid Model");
            }

            var consumercategory = await consumerCategoryService.Add(_consumercategory);

            return CreatedAtRoute("DefaultApi", new { id = consumercategory.categoryid }, consumercategory);
        }

        // PUT api/consumercategory/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]consumercategory _consumercategory)
        {
            var consumercategory = await consumerCategoryService.Edit(_consumercategory);
            return Ok(consumercategory);
        }

        // DELETE api/consumercategory/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            var _consumercategory = await consumerCategoryService.Get(id);
            if (_consumercategory == null)
            {
                throw new Exception("Invalid consumer category");
            }
            var consumercategory = await consumerCategoryService.Delete(_consumercategory);
            return Ok(consumercategory);
        }
    }
}
