using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Meow.Core.Entity;
using MeowWorld.Core.DomainService;
using MeowWorld.Infrastructure.data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("/token")]
    public class tokenController : Controller
    {
        private IRepository<User> repository;
        private IAuthenticationHelper authenticationHelper;

        public tokenController(IRepository<User> repos, IAuthenticationHelper authHelper)
        {
            repository = repos;
            authenticationHelper = authHelper;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginInputModel model)
        {
            var user = repository.GetAll().FirstOrDefault(u => u.Username == model.Username);

            // check if username exists
            if (user == null)
                return Unauthorized();

            // check if password is correct
            if (!authenticationHelper.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
                return Unauthorized();

            // Authentication successful
            return Ok(new
            {
                username = user.Username,
                token = authenticationHelper.GenerateToken(user)
            });
        }

    }
}
