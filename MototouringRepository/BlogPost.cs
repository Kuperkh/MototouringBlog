using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class BlogPost
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }

        public DateTime Published { get; set; }

        public DateTime Updated { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
