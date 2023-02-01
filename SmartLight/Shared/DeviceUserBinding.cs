using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SmartLight.Shared
{
    public class DeviceUserBinding
    {
        [JsonProperty(PropertyName = "id")]
        public string ID { get; set; }

        [JsonProperty(PropertyName = "users")]
        public List<UserInfo> Users { get; set; }

        [JsonProperty(PropertyName = "devices")]
        public List<Device> Devices { get; set; }

        public DeviceUserBinding()
        {
            Users = new();
            Devices = new();
        }
    }
}
