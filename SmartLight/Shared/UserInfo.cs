using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SmartLight.Shared
{
    public class UserInfo
    {
        private string userName;
        private string iD;

        public event Action OnChange;

        [JsonProperty(PropertyName = "id")]
        public string ID { get => nullCheck(iD); set => iD = value; }

        [Required]
        [JsonProperty(PropertyName = "userName")]
        public string UserName { get => nullCheck(userName); set => userName = value; }

        [System.Text.Json.Serialization.JsonIgnore]
        public bool IsValid
        {
            get
            {
                return !(string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(iD));
            }
        }

        private string nullCheck(string s)
        {
            NotifyStateChanged();
            return string.IsNullOrEmpty(s) ? "" : s;
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}