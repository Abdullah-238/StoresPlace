using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StoresPlace_Business.clsPerson;

namespace StoresPlace_Business
{
    public class SendMessage
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Tagname { get; set; }
        public string RecepientNumber { get; set; }
        public string VariableList { get; set; }
        public string ReplacementList { get; set; }
        public string Message { get; set; }
        public long SendDateTime { get; set; }
        public bool EnableDR { get; set; }


        private static readonly HttpClient client = new HttpClient();

        public async Task SendSMSAsync(SendMessage smsRequest)
        {
            var url = "https://api.yamamah.com/SendSMS/";

           // string jsonBody = JsonConvert.SerializeObject(smsRequest);  // Using JsonConvert.SerializeObject

            //var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            //HttpResponseMessage response = await client.PostAsync(url, content);

            //if (response.IsSuccessStatusCode)
            //{
            //    // Success
            //    //Console.WriteLine("SMS sent successfully.");
            //}
            //else
            //{
            //    // Handle failure
            //    //Console.WriteLine($"Error: {response.StatusCode}, {await response.Content.ReadAsStringAsync()}");
            //}
        }
    }
}
