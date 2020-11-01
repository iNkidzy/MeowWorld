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
    [Route("api/[controller]")]
    [ApiController]
    public class ownersController : ControllerBase
    {
        private readonly IOwnerService _ownerService;

        public ownersController(IOwnerService ownerService)
        {

            _ownerService = ownerService;
        }

        // GET: api/owners
        [HttpGet]
        public IEnumerable<Owner> Get()
        {
            return _ownerService.ReadAllOwners();
        }

        // GET: api/owners/5
        [HttpGet("{id}")]
        public ActionResult<Owner> Get(int id)
        {
            return _ownerService.FindOwnerById(id);
        }

        // POST: api/owners
        [HttpPost]
        public ActionResult<Owner> Post([FromBody] Owner owner)
        {
            return _ownerService.Create(owner);
        }

        // PUT: api/owners/5
        [HttpPut("{id}")]
        public ActionResult<Owner> Put(int id, [FromBody] Owner owner)
        {
            return _ownerService.UpdateOwner(owner);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult<Owner> Delete(int id)
        {
            return _ownerService.DeleteOwner(id);
        }
    }
}
