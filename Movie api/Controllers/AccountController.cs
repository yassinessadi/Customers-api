using Microsoft.AspNetCore.Mvc;
using Movie_api.Models;
using Movie_api.Repository;
using System.Threading.Tasks;

namespace Movie_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _Service;

        public AccountController(IAccountRepository Service)
        {
            _Service = Service;
        }

        [HttpPost("singup")]
        public async Task<IActionResult> SingUp([FromBody]RegisterModel register)
        {
            var result = await _Service.SingUpAsync(register);
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }
            return Unauthorized();
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]SingInModel singIn)
        {
            var result = await _Service.SingInAsync(singIn);
            if (string.IsNullOrEmpty(result))
            {
                 return Unauthorized();
            }
            return Ok(result);
        }
    }
}
