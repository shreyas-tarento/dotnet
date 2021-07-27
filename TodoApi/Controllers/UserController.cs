using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Interfaces;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController:ControllerBase
    {
        private readonly IUser _iUser;
        public UserController(IUser iUser)
        {
            _iUser = iUser;
        }
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(_iUser.GetAllUsers());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id) 
        {
            var existingUserInfo = await _iUser.GetUserInfo(id);
            if (existingUserInfo == null)
                return NotFound($"User Details for id:#{id} not found");

            return Ok(await _iUser.GetUser(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDetails userDetails)
        {
            if (userDetails == null)
                return BadRequest();

            var doesUseExistOnUserName = await _iUser.UserExistOnUserName(userDetails.UserName);
            if (doesUseExistOnUserName)
                return BadRequest($"{userDetails.UserName} user name already exists");

            return new JsonResult(await _iUser.CreateUser(userDetails))
            {
                StatusCode = 201
            };
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute]int id, [FromBody]UserDetails userDetails)
        {
            if (id != userDetails.UserId)
                return BadRequest("UserId mismatch");

            var existingUserDetails = await _iUser.GetUserInfo(id);
            if (existingUserDetails == null)
                return NotFound($"User Details for id:#{id} not found");

            return Ok(await _iUser.UpdateUser(id, userDetails));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute]int id)
        {
            var existingUserInfo = await _iUser.GetUserInfo(id);
            if (existingUserInfo == null)
                return NotFound($"User Details for id:#{id} not found");

            return Ok(await _iUser.DeleteUser(id));
        }

    }
}
