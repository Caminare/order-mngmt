using OrderMngmt.Business.Models;

namespace OrderMngmt.Business.Interfaces
{
    public interface IProductService
    {
        IQueryable<ProductModel> GetProducts(PaginationFilter? paginationFilter, SortFilter? sortFilter);
        Task<ProductModel?> GetProductById(int id);
        Task<ProductModel> AddProduct(ProductModel product);
        Task<int> SaveChanges();
    }
}