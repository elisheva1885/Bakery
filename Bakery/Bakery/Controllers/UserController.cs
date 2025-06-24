using DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Net;
using System.Text.Json;
//delete unused code
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bakery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices userServices;//_userServices;
        public UserController(IUserServices _userServices)
        {
            userServices = _userServices;//_userServices= userServices;
        }
        // GET: api/<UserController>//
        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            return await userServices.getUsers();
        }

        // GET api/<UserController>/5//
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
             UserDto user = await userServices.getUserId(id);
            if(user!=null)
            {
                return Ok(user);
            }
            else
            {
                return NoContent();
            }//user!=null ? return Ok(user) : return NoContent();
        }
        [HttpPost]
        [Route("checkPassword")]
        public int CheckPassword([FromBody] String password)
        {
            return userServices.validatepasswordStrong(password);
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] RegisterUserDto user)
        {
            if (user is null)
                return StatusCode(400, "user is required");
           
            try
            {
                await userServices.register(user);
                return CreatedAtAction(nameof(Get),user);
            }
            catch (HttpStatusException e)
            {
                return StatusCode((int)e.StatusCode, e.Message);
            }
         
        }


        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<User>> Login([FromBody] LoginUserDto loginUser)
        {
            try
            {
                UserDto user =await userServices.login(loginUser);
                return Ok(user);
            }
             catch(HttpStatusException e)
            {
                return StatusCode((int)e.StatusCode, e.Message);
            }
         
        }



        // PUT api/<UserController>/5//
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Put( int id, [FromBody]  RegisterUserDto user)
        {
            try
            {
                await userServices.update(id, user);
                return Ok(user);
            }
            catch (HttpStatusException e)
            {
                return StatusCode((int)e.StatusCode, e.Message);
            }

        }

    }
}
