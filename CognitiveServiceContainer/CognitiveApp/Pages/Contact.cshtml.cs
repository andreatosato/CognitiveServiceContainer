using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CognitiveApp.Cognitive;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace CognitiveApp.Pages
{
    public class ContactModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
            Message = "Your contact page.";
        }

        public async Task OnPostProva()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:5000/text/analytics/v2.0/");
                CognitiveData data = new CognitiveData()
                {
                    Documents = new[]
                    {
                        new Document()
                        {
                            Language = "it",
                            Text = "Ciao sono Cristiano"
                        }
                    }.ToList()
                };
                
                var httpResult = await client.PostAsJsonAsync("sentiment", JsonConvert.SerializeObject(data));
                if (httpResult.IsSuccessStatusCode)
                {
                    var r = httpResult.Content;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }
    }
}
