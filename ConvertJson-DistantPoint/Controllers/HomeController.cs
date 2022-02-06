using ConvertJson_DistantPoint.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ConvertJson_DistantPoint.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            Jsonfile Result = new Jsonfile();

            return View(Result);
        }

        [HttpPost]
        public IActionResult Index(IFormFile file)
        {
            if (file != null && Path.GetExtension(file.FileName) == ".json")
            {
                Stream str = file.OpenReadStream();
                string jsonResult = string.Empty;

                using (StreamReader sr = new StreamReader(str))
                {
                    jsonResult += sr.ReadToEnd();
                }
                string resultString = string.Empty;                

                List<Claims> jsonObjects = new List<Claims>();

                JArray jsonArray = JArray.Parse(jsonResult);
                dynamic data = JObject.Parse(jsonArray[0].ToString());
                var js = jsonArray.OfType<JObject>().ToList();
                //var js = jsonArray.OfType<JObject>().Where(x => x.Properties().Value == new JToken() { }).ToList();

                foreach (var item in js)
                {
                    var itemStringified = item.ToString();
                    var sr = itemStringified.Split(",");

                    foreach (var part in sr)
                    {
                        part.Replace("{", "");
                        part.Replace("}", "");

                        var obj = part.Split(":");

                        Claims jsonObject = new Claims();
                        jsonObject.ClaimType = obj[0];
                        if (obj.Length != 1)
                        {
                            jsonObject.Value = obj[1];

                        }
                        jsonObjects.Add(jsonObject);
                    }

                }               

                string str2 = "";                
                foreach (var property in jsonObjects)
                {
                    str2 += $"Claims: {property.ClaimType}, Value: {property.Value} \x0A \n \xA";
                    str2 += "\x0A \n \xA"; 
                }

                Console.WriteLine(str2);

                Jsonfile jsonRes = new Jsonfile();

                jsonRes.Content = str2;


                return View(jsonRes);
            }
            else
            {
                return RedirectToPage("Error", "Enter FIle");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
