using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WeatherApp.Models;


namespace WeatherApp.Controllers
{
    public class WeatherController : Controller
    {
        [ProducesResponseType(200, Type = typeof(List<Day>))]
        public async Task<ActionResult<List<Day>>> Index()
        {
            var keyNames = new List<string> { "forecast_0", "forecast_1", "forecast_2", "forecast_3", "forecast_4" };
            ISession session = HttpContext.Session;
            var data = new DataController();

            var forecast = Session.Extensions.GetObjectFromJson<Day>(session, "forecast_0");

            if (forecast != null)
            {
                var sessionModel = Session.Extensions.GetListOfObjects<Day>(session, keyNames);
                return View(sessionModel);
            }

            var result =  data.GetWeather(session, null, "Denver");
            await result;

        //Not good practice, I know ... Short for time ...        
            var model = Session.Extensions.GetListOfObjects<Day>(session, keyNames);
                   
            return View(model);
        }

      
    }

}