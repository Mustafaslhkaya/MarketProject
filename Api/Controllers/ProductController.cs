
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Mvc.Models;
using System.Collections.Generic;
using System.Linq;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private IProductDal _productDal;

        
        public ProductController()
        {
            _productDal = new ProductDal();

            
        }
        
        
        [HttpGet]
        public List<ProductViewModel> GetAllProducts()
        {
            List<Product> products = _productDal.GetAll();
            return this.buildProductViewModels(products);
        }
       
        
        [HttpGet("{id}")]
        public Product GetId(int id)
        {
            return _productDal.GetById(id);
        }


        [HttpGet("SearchByName/{word}")]
        public List<ProductViewModel> SearchByName(string word)
        {
            List<Product> products = _productDal.SearchByName(word);
            return this.buildProductViewModels(products);
        }


        [HttpPost("Create")]
        public void AddProduct([FromBody] Product product)
        {
            _productDal.Add(product);
        }
        
        
        [HttpPut("Update")]
        public void UpdateProduct([FromBody] Product product)
        {
            _productDal.Update(product);
        }


        [HttpDelete("Delete/{id}")]
        public void DeleteProduct(int id)
        {

            _productDal.Delete(id);
        }

        private List<ProductViewModel> buildProductViewModels(List<Product> products)
        {
            return products.ConvertAll(product => this.buildProductViewModel(product)).ToList();
        }

        private ProductViewModel buildProductViewModel(Product product)
        {
            int id = product.Id;
            string name = product.Name;
            decimal price = product.Price;
            return new ProductViewModel(id, name, price);
        }
    }
}
