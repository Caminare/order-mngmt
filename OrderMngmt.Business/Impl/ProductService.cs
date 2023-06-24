using OrderMngmt.Data.Interfaces;
using OrderMngmt.Business.Interfaces;
using OrderMngmt.Business.Models;
using OrderMngmt.Data.Models;

namespace OrderMngmt.Business.Impl
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IQueryable<ProductModel> GetProducts(PaginationFilter? paginationFilter, SortFilter? sortFilter)
        {
            var query = _unitOfWork.GetRepository<Product>()
                .GetAll(paginationFilter.PageNumber, paginationFilter.PageSize, sortFilter.SortBy, sortFilter.SortOrder)
                .Select(p => new ProductModel(p));
            

            return query;
        }

        public async Task<ProductModel?> GetProductById(int id)
        {
            var product = await _unitOfWork.GetRepository<Product>().GetById(id);

            return new ProductModel(product);
        }

        public async Task AddProduct(ProductModel product)
        {
            await _unitOfWork.GetRepository<Product>().Add(product.ToEntity());
        }

        public async Task<int> SaveChanges()
        {
            return await _unitOfWork.SaveChanges();
        }

    }
}