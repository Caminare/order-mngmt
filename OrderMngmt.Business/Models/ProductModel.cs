using OrderMngmt.Data.Models;

namespace OrderMngmt.Business.Models
{
    public class ProductModel : BaseModel
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }

        public ProductModel()
        {
        }

        public ProductModel(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Price = product.Price;
        }

        public Product ToEntity()
        {
            return new Product
            {
                Id = Id,
                Name = Name,
                Price = Price
            };
        }
    }
}