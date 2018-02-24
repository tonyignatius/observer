using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net.Http;
using System.Text;

namespace observer.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        // GET api/mail
        [HttpGet("mail")]
        public string Mail()
        {
            try
            {
                //SmtpClient client = new SmtpClient("smtp-mail.outlook.com");
                SmtpClient client = new SmtpClient("smtpapps.tus.ams1907.com");
                //SmtpClient client = new SmtpClient("mail.post.000webhost.com");
                client.UseDefaultCredentials = true;
                client.EnableSsl = true;
                client.Port = 587;
                //client.Credentials = new NetworkCredential("tonyignatius@live.com", "tripplehorn#1");
                MailMessage mailmessage = new MailMessage();
                mailmessage.From = new MailAddress("donotreply@ups.com");
                mailmessage.To.Add("cyberhero.tony@gmail.com");
                mailmessage.Body =  "Body of the Message";
                mailmessage.Subject = "Sent from API";
                client.Send(mailmessage);
                return "SentMail";                
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

           
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet("geocode/{lat}/{longi}")] //usage http://localhost:52542/api/values/geocode/40.730610/-73.935242
        public string Geocode(string lat, string longi)
        {
            using (var client = new HttpClient())
            {
                try
                {

                    string req = locatorRequestJson(lat, longi);
                    HttpContent content = new StringContent(req, Encoding.UTF8, "application/json");
                    client.BaseAddress = new Uri("https://onlinetools.ups.com/rest/Locator");
                    var response = client.PostAsync("https://onlinetools.ups.com/rest/Locator", content).Result;
                    string gcol = "[";
                    if (response.IsSuccessStatusCode)
                    {

                        var jsonresp = GetResponse(response).Result;
                        Newtonsoft.Json.Linq.JObject json = Newtonsoft.Json.Linq.JObject.Parse(jsonresp);
                        Console.Write("Success");
                        foreach (var result in json["LocatorResponse"]["SearchResults"]["DropLocation"])
                        {
                            gcol = gcol + "{\"Latitude\":\"" + result["Geocode"]["Latitude"].ToString();
                            gcol = gcol + "\",\"Longitude\":\"" + result["Geocode"]["Longitude"].ToString() + "\"},";
                        }
                        return gcol.Substring(0, gcol.Length - 1) + "]";
                    }
                    else
                        Console.Write("Error");

                    return GetResponse(response).Result;
                }
                catch(Exception ex) { return ex.Message; }
                }            
        }

        

        async Task<string> GetResponse(HttpResponseMessage rspm)
        {
            string response = await rspm.Content.ReadAsStringAsync();
            return response;
        }
        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        public string locatorRequestJson(string latitude, string longitude)
        {
            string req = @"{
  'AccessRequest': {
    'AccessLicenseNumber': 'ED3E55FEC0B4BE8C',
    'UserId': 'searchqv',
    'Password': 'password'
  },
  'LocatorRequest': {
    'Request': {
      'RequestOption': [
        '1'
      ],
      'TransactionReference': {
        'CustomerContext': 'Android_Locator'
      },
      'RequestAction': 'Locator'
    },
    'OriginAddress': {
      'Geocode': {
        'Latitude': '" + latitude + @"',
        'Longitude': '" + longitude + @"'
      },
      'AddressKeyFormat': {}
    },
    'Translate': {
      'LanguageCode': 'ENG',
      'Locale': 'en_US'
    },
    'UnitOfMeasurement': {
      'Code': 'MI'
    },
    'LocationSearchCriteria': {
      'SearchOption': [
        {
          'OptionType': {
            'Code': '01'
          },
          'OptionCode': [
            {
              'Code': '003'
            }
          ]
        }
      ],
      'MaximumListSize': '10',
      'SearchRadius': '25'
    },
    'SortCriteria': {
      'SortType': '01'
    }
  }
}";
            return req;
        }


        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
       
    }
}
