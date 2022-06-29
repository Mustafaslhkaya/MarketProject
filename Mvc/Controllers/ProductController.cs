
using Microsoft.AspNetCore.Mvc;
using Mvc.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Mvc.Controllers
{
    public class ProductController : Controller
    {
        
        Uri baseAdress = new Uri("https://localhost:44320");
        HttpClient client;
        public ProductController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAdress;
            
        }
        public IActionResult Index()
        {

            List<ProductViewModel> modelList=new List<ProductViewModel>();
            
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Product").Result;
            
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                JsonConvert.DeserializeObject<List<ProductViewModel>>(data);

            }
            return View(modelList);
        }
    }
}
