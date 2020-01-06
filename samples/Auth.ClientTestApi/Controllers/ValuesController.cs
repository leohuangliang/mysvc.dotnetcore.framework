using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MySvc.DotNetCore.Framework.Infrastructure.Authorization.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.ClientTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ValuesController : ControllerBase
    {
        private readonly IUserIdentityService _userIdentityService;

        public ValuesController(IUserIdentityService userIdentityService)
        {
            _userIdentityService = userIdentityService ?? throw new ArgumentNullException(nameof(userIdentityService));
        }
        // GET api/values
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(string))]
        public async Task<IActionResult> Get()
        {
            var  userIdentity = await _userIdentityService.GetUserIdentityAsync();
            return Ok(userIdentity.UserName);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
