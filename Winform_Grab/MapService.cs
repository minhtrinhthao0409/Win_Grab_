using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Winform_Grab
{
    
    public abstract class MapService
    {
        protected readonly HttpClient HttpClient;
        public string ApiKey { get; }

        protected MapService(string apiKey)
        {
            HttpClient = new HttpClient();
            ApiKey = apiKey;
        }
        public abstract Task<string> GetDataAsync(string url);
    }
    
}
