using InventoryAPI.Helper;
using InventoryAPI.Interface;
using InventoryAPI.Model.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _repo;
        public UserController(IUserService repo) 
        {
            _repo = repo;
        }

        [HttpPost("RegisterAdmin")]

        public async Task<IActionResult> RegisterAdmin(Register register)
        {
            try
            {
                var res = await _repo.RegisterAdmin(register);
                return Ok(res);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("RegisterStaff")]

        public async Task<ActionResult> RegisterStaff([FromBody]Register register)
        {
            try
            {
                var res = await _repo.RegisterStaff(register);
                return Ok(res);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("RegisterCustomer")]

        public async Task<IActionResult> RegisterCustomer(Register register)
        {
            try
            {
                var res = await _repo.RegisterCustomer(register);
                return Ok(res);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Login")]

        public  async Task<IActionResult> Login(Login login)
        {
            try
            {
                var res = await _repo.Login(login);
                return Ok(res);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
