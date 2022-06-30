using DataAccess.Abstract;
using Entities.Concrete;
using Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;



namespace DataAccess.Concrete
{
    public class ProductDal : IProductDal
    {
        
        

        public void Add(Product entity)
        {
            using(var context=new Context())
            {
                context.Products.Add(entity);
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var context = new Context())
            {
                var deleted=GetById(id);
                context.Products.Remove(deleted);
                context.SaveChanges();
            }
        }

        public List<Product> GetAll()
        {
            
            using (var context = new Context())
            {
                return context.Products.ToList();
            }
        }

        public Product GetById(int id)
        {
            using (var context = new Context())
            {
               return context.Products.Find(id);
            }
        }

        public void Update(Product entity)
        {
            using (var context = new Context())
            {
                context.Products.Update(entity);
                context.SaveChanges();
            }
        }
        public List<Product> SearchByName(string word)
        {
            using (var context = new Context())
            {
               return context.Products.Where(p => p.Name.StartsWith(word)).ToList();
            }
        }
    }
}
