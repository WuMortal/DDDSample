using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wigor.Sample.ApplicationContract;
using Wigor.Sample.ApplicationContract.DTO;
using Wigor.Sample.Domain.IRepository;

namespace Wigor.Sample.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        readonly IUserService _userService;

        public ValuesController(IUserService userService)
        {
            _userService = userService;
        }

        // GET api/values
        [HttpGet]
        public async Task<List<UserDTO>> Get()
        {
            return await Task.Run(() => _userService.GetList());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var second = DateTime.Now.Second.ToString("00");
            bool isSuccess = await _userService.Register("Wigor", $"188888888{second}", 22);

            return Ok(isSuccess);
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
