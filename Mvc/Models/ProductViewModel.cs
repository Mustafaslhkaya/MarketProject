namespace Mvc.Models
{
    public class ProductViewModel
    {
        public ProductViewModel(int id,string name,decimal price)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
