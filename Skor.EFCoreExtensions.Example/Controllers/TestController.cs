using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skor.EFCoreExtensions.Example.Models;
using Skor.EFCoreExtensions.Repositories;

namespace Skor.EFCoreExtensions.Example.Controllers
{
    [Produces("application/json")]
    [Route("api/test/[action]")]
    public class TestController : Controller
    {
        private readonly IBaseRepository<Author> author;
        private readonly IBaseRepository<ToiletAuthor> toiletAuthor;
        public TestController(IBaseRepository<Author> author,
            IBaseRepository<ToiletAuthor> toiletAuthor)
        {
            this.author = author;
            this.toiletAuthor = toiletAuthor;
        }
        // GET: api/Test
        [HttpGet]
        public async Task<IEnumerable<Author>>  GetAll()
        {
            return await author.GetAllAsync();
        }

        // GET: api/Test/5
        [HttpGet("{id}")]
        public async Task<Author> Get(long id)
        {
            return await author.GetAsync(id);
        }

        [HttpGet("{id}/{id2}")]
        public async Task<ToiletAuthor> Get2Id(long id, long id2)
        {
            return await toiletAuthor.GetAsync(id,id2);
        }
        // POST: api/Test
        [HttpPost]
        public void Post()
        {

        }
        
        // PUT: api/Test/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            author.Delete(f => f.Id == id);
        }
        [HttpDelete("{id}/{id2}")]
        public bool Delete2Id(long id,long id2)
        {
            var tA = toiletAuthor.Get(id, id2);
            return  toiletAuthor.Delete(tA);
        }
    }
}
