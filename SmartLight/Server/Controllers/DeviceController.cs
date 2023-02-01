using Microsoft.AspNetCore.Mvc;
using SmartLight.Server.Service;
using SmartLight.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartLight.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeviceController : ControllerBase
    {
        CosmosDB db;

        public DeviceController(CosmosDB database)
        {
            db = database;
        }

        [HttpGet]
        public async Task<Device> Get()
        {
            return null;
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<Device> GetById(string id)
        {
            return null;
        }

        [HttpPost]
        public async Task<IActionResult> Post(WebPostWithDevice webPost)
        {
            var list = await db.GetItemsAsync($"SELECT * FROM c");
            var binding = list.FirstOrDefault(x => x.Users.Any(y => y.ID == webPost.UserInfo.ID));
            binding.Devices.Add(webPost.Device);
            await db.UpdateItemAsync(binding.ID, binding);
            return Ok();
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update(WebPostWithDevice webPost)
        {
            var list = await db.GetItemsAsync($"SELECT * FROM c");
            var binding = list.FirstOrDefault(x => x.Users.Any(y => y.ID == webPost.UserInfo.ID));
            binding.Devices.Remove(binding.Devices.First(x => x.ID == webPost.Device.ID));
            binding.Devices.Add(webPost.Device);
            await db.UpdateItemAsync(binding.ID, binding);
            return Ok();
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<Device>> GetAll()
        {
            return null;
        }

        [HttpGet]
        [Route("GetByUserID")]
        public async Task<IEnumerable<Device>> GetDevicesByUserID(string id)
        {
            var list = await db.GetItemsAsync($"SELECT * FROM c");
            return list.FirstOrDefault(x => x.Users.Any(y => y.ID == id)).Devices;
        }
    }
}