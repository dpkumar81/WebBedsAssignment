using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebBedsAssignment.Helper
{
    public static class ClientService
    {
        public static HttpClient Initialize(string argBaseAddress)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(argBaseAddress);

            return client;

        }
    }
}
