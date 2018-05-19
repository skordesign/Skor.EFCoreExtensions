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
    [Route("api/Test")]
    public class TestController : Controller
    {
        private readonly IBaseRepository<Author> baseRepository;
        public TestController(IBaseRepository<Author> @base)
        {
            baseRepository = @base;
        }
        // GET: api/Test
        [HttpGet]
        public async Task<IEnumerable<Author>>  Get()
        {
            return await baseRepository.GetAllAsync();
        }

        // GET: api/Test/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Test
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Test/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
