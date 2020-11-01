using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Meow.Core.Entity;
using MeowWorld.Core.ApplicationService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class catsController : ControllerBase
    {
        private readonly ICatService _catService;

        public catsController(ICatService catService)
        {
            _catService = catService;
        }


        // GET: api/cats
        //[Authorize(Roles = "Administrator")]
        [HttpGet]
        public IEnumerable<Cat> Get()
        {
            return _catService.ReadAllCats();
        }

        // GET: api/cats/5
        //[Authorize]
        [HttpGet("{id}")]
        public ActionResult<Cat> Get(int id)
        {
            return _catService.FindCatById(id);
        }

        // POST: api/cats
        //[Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(Cat), 201)]
        [ProducesResponseType(typeof(Cat), 400)]
        public ActionResult<Cat> Post([FromBody] Cat cat)
        {
            return Ok(_catService.CreateCat(cat));
        }

        // PUT: api/cats/5
        //[Authorize]
        [HttpPut("{id}")]
        public ActionResult<Cat> Put(int id, [FromBody] Cat cat)
        {
            return _catService.UpdateCat(cat);
        }

        // DELETE: api/ApiWithActions/5
        //[Authorize]
        [HttpDelete("{id}")]
        public ActionResult<Cat> Delete(int id)
        {
            return _catService.DeleteCat(id);
        }
    }
}
