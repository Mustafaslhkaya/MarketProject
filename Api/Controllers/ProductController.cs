
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        public List<Product> GetAllProducts()
        {
            
            return _productDal.GetAll();
        }
       
        
        [HttpGet("{id}")]
        public Product GetId(int id)
        {
            return _productDal.GetById(id);
        }
       
        
        [HttpPost]
        public void AddProduct([FromBody] Product product)
        {
            _productDal.Add(product);
        }
        
        
        [HttpPut]
        public void UpdateProduct([FromBody] Product product)
        {
            _productDal.Update(product);
        }


        [HttpDelete("{id}")]
        public void DeleteProduct(int id)
        {

            _productDal.Delete(id);
        }
    }
}
