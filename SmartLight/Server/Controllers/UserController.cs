using Microsoft.AspNetCore.Mvc;
using SmartLight.Server.Service;
using SmartLight.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartLight.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        CosmosDB db;

        public UserController(CosmosDB database)
        {
            db = database;
        }
        
        [HttpGet]
        [Route("Validate")]
        public bool Validate(UserInfo user)
        {
            return !(string.IsNullOrEmpty(user.ID) || string.IsNullOrEmpty(user.UserName));
        }

        [HttpPost]
        [Route("AddUser")]
        public async Task AddUser(UserInfo user)
        {
            DeviceUserBinding binding = new();
            binding.ID = Guid.NewGuid().ToString();
            binding.Users.Add(user);
            await db.AddItemAsync(binding);
        }
    }
}