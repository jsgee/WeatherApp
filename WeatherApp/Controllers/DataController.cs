using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WeatherApp.Extension;
using WeatherApp.Models;
using WeatherApp.Session;
using Extensions = WeatherApp.Session.Extensions;

namespace WeatherApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly string _appKey = "2e08ef3bf97ee87a640689823bfbce4b";


        // GET api/values
        [HttpGet]
        public async Task<ActionResult> GetWeather(ISession session, int? zipCode, string city = "Chicago")
        {

            using (var client = new HttpClient())
            {

                try
                {
                    string cityNameOrZipParam = "q";
                    if (zipCode.HasValue)
                        cityNameOrZipParam = "zip";

                    client.BaseAddress = new Uri("http://api.openweathermap.org");
                    var response = await client.GetStringAsync($"/data/2.5/forecast?{cityNameOrZipParam}={city},us&appid={_appKey}&units=imperial&mode=xml");
                    var xdoc = XDocument.Parse(response);
                    var weeklyForecast = GetForcastsFromXmlResponse(xdoc, 5, session);
                    var json = JsonConvert.SerializeObject(weeklyForecast);

                    return Ok(json);
                }

                catch (HttpRequestException httpRequestException)
                {
                    return BadRequest($"Error getting weather from OpenWeather: {httpRequestException.Message}");
                }

            }
        }

        private List<Day> GetForcastsFromXmlResponse(XDocument xdoc, int weekForecastListNumOfDays, ISession session)
        {

            List<Day> listOfDays = new List<Day>();
            for (var i = 0; i <= weekForecastListNumOfDays; i++)
            {

                var today = new DateTime();
                var cutOff = DateTime.Now;
                cutOff.ChangeTime(17, 0, 0, 0);

                today = DateTime.Now >= cutOff ? DateTime.Now.Date.AddDays(i + 1) : DateTime.Now.Date.AddDays(i);

                var dayTemps = xdoc.Descendants("forecast").Elements("time").Where(x => Convert.ToDateTime(x.Attribute("from").Value).Date == today).Elements("temperature");

                if (dayTemps == null || !dayTemps.Any())
                    return listOfDays;


                listOfDays.Add(new Day
                {
                    DayDate = today,
                    DailyTemp = new Temperature
                    {

                        max = dayTemps.Select(w => w.Attributes("max").Select(attr => Convert.ToDouble(attr.Value)).FirstOrDefault()).ToList().Max(),
                        min = dayTemps.Select(w => w.Attributes("min").Select(attr => Convert.ToDouble(attr.Value)).FirstOrDefault()).ToList().Min(),
                        avg = dayTemps.Select(w => w.Attributes("value").Select(attr => Convert.ToDouble(attr.Value)).FirstOrDefault()).ToList().Average()

                    }
                });

                var day = listOfDays.LastOrDefault();
                if (day != null)
                    Extensions.SetObjectAsJson(session, $"forecast_{i.ToString()}", day);

            }

            return listOfDays;

        }







        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return $"value{id}";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


    }
}