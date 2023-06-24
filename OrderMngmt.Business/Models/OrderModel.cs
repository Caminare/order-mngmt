using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderMngmt.Data.Models;

namespace OrderMngmt.Business.Models
{
    public class OrderModel : BaseModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int UserId { get; set; }

        public OrderModel()
        {
        }

        public OrderModel(Order order)
        {
            ProductId = order.ProductId;
            Quantity = order.Quantity;
            UserId = order.UserId;
            Id = order.Id;
        }

        public Order ToEntity()
        {
            return new Order
            {
                Id = Id,
                ProductId = ProductId,
                Quantity = Quantity,
                UserId = UserId
            };
        }
    }
}