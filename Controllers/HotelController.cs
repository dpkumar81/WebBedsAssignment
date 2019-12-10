using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using WebBedsAssignment.Helper;
using WebBedsAssignment.Models;

namespace WebBedsAssignment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelController : ControllerBase
    {

        [HttpGet("GetHotels")]
        public async Task<IActionResult> GetHotelsAsync(int argNoOfNights)
        {
            try
            {
                List<HotelInfo> lstHotelInfo = null;
                var httpClient = ClientService.Initialize("https://webbedsdevtest.azurewebsites.net/api/");

                // Query string parameters
                var queryString = new Dictionary<string, string>()
                {
                    { "destinationId", "279" },
                    { "nights", argNoOfNights.ToString() },
                    { "code", "aWH1EX7ladA8C/oWJX5nVLoEa4XKz2a64yaWVvzioNYcEo8Le8caJw==" }
                };

                var requestUri = QueryHelpers.AddQueryString("findBargain", queryString);
                var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

                // Setup header(s)
                request.Headers.Add("Accept", "application/json");
                var res = await httpClient.GetAsync(requestUri);

                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    lstHotelInfo = JsonConvert.DeserializeObject<List<HotelInfo>>(result);

                    foreach (var hotelitem in lstHotelInfo.Where(x => x.Rates.Any(y => y.RateType == "PerNight")))
                    {
                        hotelitem.Rates.Select(z => { 
                            z.Value = (argNoOfNights * z.Value);
                            return z; }
                        ).ToList();
                    }
                }

                return Ok(lstHotelInfo);
            }
            catch (Exception ex)
            {
                return BadRequest($"Exception is {ex.Message}");
            }
        }
    }
}