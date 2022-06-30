using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Mvc.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;


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
        public IActionResult Index(string name)
        {
            List<ProductViewModel> productViewModels = new List<ProductViewModel>();
            if (name == null)
            {
           
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "Product").Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                   productViewModels = JsonConvert.DeserializeObject<List<ProductViewModel>>(data);
                }
            }
            else
            {
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "Product/SearchByName/" + name).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                   productViewModels = JsonConvert.DeserializeObject<List<ProductViewModel>>(data);
                }
            }

            return View(productViewModels);
        }


        [HttpPost]
        public IActionResult SearchProduct(string name)
        {
            return RedirectToAction(nameof(Index), new { name });

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateProduct(string name, decimal price)
        {
            Product product = new Product();
            product.Name = name;
            product.Price = price;

            String serializedProduct = JsonConvert.SerializeObject(product);
            HttpContent requestContent = new StringContent(serializedProduct, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "Product/Create", requestContent).Result;

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Edit(int id)
        {
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "Product/"+id).Result;
            
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                ProductViewModel productViewModel = JsonConvert.DeserializeObject<ProductViewModel>(data);
                return View(productViewModel);
            }
            else
            {
                return View();
            }
        }


        [HttpPost]
        public IActionResult Edit(int id, string name, decimal price)
        {
            Product product = new Product();
            product.Id = id;
            product.Name = name;
            product.Price = price;

            String serializedProduct = JsonConvert.SerializeObject(product);
            HttpContent requestContent = new StringContent(serializedProduct, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "Product/Update", requestContent).Result;

            return RedirectToAction(nameof(Index));
        }



        public IActionResult Delete(int id)
        {
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "Product/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                ProductViewModel productViewModel = JsonConvert.DeserializeObject<ProductViewModel>(data);
                return View(productViewModel);
            }
            else
            {
                return View();
            }

        }


        [HttpPost]
        public IActionResult DeleteProduct(int id)
        {
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "Product/Delete/" + id).Result;
            return RedirectToAction(nameof(Index));
        }
    }
}
