using OrderMngmt.Business.Interfaces;
using OrderMngmt.Data.Interfaces;
using OrderMngmt.Business.Models;
using OrderMngmt.Data.Models;



namespace OrderMngmt.Business.Impl
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddOrder(OrderModel order)
        {
            var orderRepository = _unitOfWork.GetRepository<Order>();

            if (!(await ValidOrder(order)))
            {
                throw new ArgumentException("Invalid order");
            }
            // create parameters for stored procedure
            var parameters = new Dictionary<string, object>
            {
                { "@ProductId", order.ProductId },
                { "@Quantity", order.Quantity },
                { "@UserId", order.UserId }
            };

            await orderRepository.ExecuteStoredProc("CreateOrder", parameters);
        }

        public async Task<OrderModel?> GetOrderById(int id)
        {
            var orderRepository = _unitOfWork.GetRepository<Order>();
            var order = await orderRepository.GetById(id);

            return new OrderModel(order);
        }

        public IQueryable<OrderModel> GetOrders(PaginationFilter? paginationFilter, SortFilter? sortFilter)
        {
            var query = _unitOfWork.GetRepository<Order>()
                .GetAll(paginationFilter.PageNumber, paginationFilter.PageSize, sortFilter.SortBy, sortFilter.SortOrder)
                .Select(o => new OrderModel(o));

            return query;
        }

        public async Task<int> SaveChanges()
        {
            return await _unitOfWork.SaveChanges();
        }

        private async Task<bool> ValidOrder(OrderModel order) {
            var product = await _unitOfWork.GetRepository<Product>().GetById(order.ProductId);
            return product != null && order.Quantity > 0;
        }
    }
}