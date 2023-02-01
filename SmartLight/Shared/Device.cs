using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmartLight.Shared
{
    public class Device
    {
        private string id;
        private string ip;
        private string name;
        private string note;

        [JsonProperty(PropertyName = "id")]
        public string ID { get => nullCheck(id); set => id = value; }

        [Required]
        [JsonProperty(PropertyName = "ip")]
        public string IP
        {
            get
            {
                return nullCheck(ip);
            }
            set
            {
                IPAddress t;
                if (IPAddress.TryParse(value, out t))
                {
                    ip = t.ToString();
                }
                else
                {
                    ip = "";
                }
            }
        }

        [Required]
        [JsonProperty(PropertyName = "name")]
        public string Name { get => nullCheck(name); set => name = value; }

        [JsonProperty(PropertyName = "note")]
        public string Note { get => nullCheck(note); set => note = value; }

        private string nullCheck(string s) => string.IsNullOrEmpty(s) ? "" : s;
    }
}