using OrderMngmt.Business.Models;

namespace OrderMngmt.Business.Interfaces
{
    public interface IOrderService
    {
        IQueryable<OrderModel> GetOrders(PaginationFilter? paginationFilter, SortFilter? sortFilter);
        Task<OrderModel?> GetOrderById(int id);
        Task<OrderModel> AddOrder(OrderModel order);
        Task<int> SaveChanges();
    }
}