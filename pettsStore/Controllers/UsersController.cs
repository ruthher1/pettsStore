﻿using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Linq.Expressions;
using System.Text.Json;
using DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace pettsStore.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserService userService;
        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> Get()
        {
            return await userService.getAllUsers();
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> Get(int id)
        {
            return await userService.getUserById(id);
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult<UserDTO>> Post([FromBody] UserRegisterDTO user)
        {
            try
            { 
             UserDTO newUser =await userService.addUser(user);
            //return CreatedAtAction(nameof(Get), new { id = user.Id }, newUser);
            return newUser;

            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login([FromBody] UserLoginDTO newUser)
        {

            UserDTO user =await userService.login(newUser);
            if (user != null)
            {
                return Ok(user);

            }
          return NotFound(new { Message = "User not found." });
        }

        [Route("password")]
        [HttpPost]
        public bool CheckPasswordStrength([FromBody] string password)
        {
            bool strength = userService.GetPassStrength(password);
            return strength;
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<UserDTO>> Put(int id, [FromBody] UserRegisterDTO updateUser)
        {
            try
            {
                 UserDTO user = await userService.updateUser(id, updateUser);
                 if (user != null)
                        {
                          return Ok(user);
                        }
                   return NotFound(new { Message = "User not found." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

    }
}
