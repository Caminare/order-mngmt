using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMngmt.Data.Models
{
    public class Order : BaseEntity
    {
        public int UserId { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public decimal Total { get; set; }
    }
}