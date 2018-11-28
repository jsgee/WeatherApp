using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using WeatherApp.Models;

namespace WeatherApp.Session
{
    public static class Extensions
    {

        public static void SetObjectAsJson<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? default(T) :
                JsonConvert.DeserializeObject<T>(value);
        }


        public static List<T> GetListOfObjects<T>(this ISession session, List<string> keyNames)
        {
            List<T> listOfObjects = new List<T>();
            foreach (var key in keyNames)
            {
                var value = session.GetString(key);
                if (value != null)
                {
                    var obj = JsonConvert.DeserializeObject<T>(value);
                    listOfObjects.Add(obj);
                }
            }
            return listOfObjects;


        }
    }
}

