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
    public class ConsumerController : ApiController
    {
        IConsumerService consumerService;
        public ConsumerController(IConsumerService consumerService)
        {
            this.consumerService = consumerService;
        }
        // GET api/consumer
        public IQueryable<consumer> Get()
        {
            return consumerService.GetAll();
        }

        // GET api/consumer/5
        public async Task<IHttpActionResult> Get(int id)
        {
            var consumer = await consumerService.Get(id);
            return Ok(consumer);
        }

        // POST api/consumer
        public async Task<IHttpActionResult> Post([FromBody]consumer _consumer)
        {
            if (!(ModelState.IsValid))
            {
                throw new Exception("Invalid Model");
            }

            var consumer = await consumerService.Add(_consumer);

            return CreatedAtRoute("DefaultApi", new { id = consumer.id }, consumer);
        }

        // PUT api/consumer/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]consumer _consumer)
        {
            var consumer = await consumerService.Edit(_consumer);
            return Ok(consumer);
        }

        // DELETE api/consumer/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            var _consumer = await consumerService.Get(id);
            if (_consumer == null)
            {
                throw new Exception("Invalid consumer");
            }
            var consumer = await consumerService.Delete(_consumer);
            return Ok(consumer);
        }
    }
}
