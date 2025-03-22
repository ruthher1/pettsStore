﻿using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace pettsStore.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private static readonly List<User> users = new List<User>();
        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return users;
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsersController>
        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            int numberOfUsers = System.IO.File.ReadLines("C:\\Users\\This User\\Desktop\\web api\\users.txt").Count();
            user.userId = numberOfUsers + 1;
            string userJson = JsonSerializer.Serialize(user);
            System.IO.File.AppendAllText("C:\\Users\\This User\\Desktop\\web api\\users.txt", userJson + Environment.NewLine);
            return CreatedAtAction(nameof(Get), new { id = user.userId }, user);

        }

        [HttpPost("login")]
        public ActionResult<User> Login([FromBody] User newUser)
        {

            using (StreamReader reader = System.IO.File.OpenText("C:\\Users\\This User\\Desktop\\web api\\users.txt"))
            {
                string? currentUserInFile;
                while ((currentUserInFile = reader.ReadLine()) != null)
                {
                    User user = JsonSerializer.Deserialize<User>(currentUserInFile);
                    if (user.username == newUser.username && user.password == newUser.password)
                        //return CreatedAtAction(nameof(Get), new { id = user.userId }, user);
                        return Ok(user);
                    
                }
            }
          return NotFound(new { Message = "User not found." });
        }


        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]User updateUser)
        {

            string textToReplace = string.Empty;
            using (StreamReader reader = System.IO.File.OpenText("C:\\Users\\This User\\Desktop\\web api\\users.txt"))
            {
                string currentUserInFile;
                while ((currentUserInFile = reader.ReadLine()) != null)
                {
                    User user = JsonSerializer.Deserialize<User>(currentUserInFile);
                    if (user.userId == id)
                        textToReplace = currentUserInFile;
                }
            }

            if (textToReplace != string.Empty)
            {
                string text = System.IO.File.ReadAllText("C:\\Users\\This User\\Desktop\\web api\\users.txt");
                //text = text.Replace(textToReplace, JsonSerializer.Serialize(updateUser));
                text = text.Replace(textToReplace, JsonSerializer.Serialize(updateUser));

                System.IO.File.WriteAllText("C:\\Users\\This User\\Desktop\\web api\\users.txt", text);
            }

        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
