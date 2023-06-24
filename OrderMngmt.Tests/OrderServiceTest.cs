using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using OrderMngmt.Business.Impl;
using OrderMngmt.Business.Models;
using OrderMngmt.Data.Interfaces;
using OrderMngmt.Data.Models;
using Moq;

namespace OrderMngmt.Tests
{
    public class OrderServiceTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly OrderService _orderService;

        public OrderServiceTest()
        {
            // Arrange
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _orderService = new OrderService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task AddOrder_ShouldAddOrder_WhenOrderIsValid()
        {
            // Arrange
            var order = new OrderModel { ProductId = 1, Quantity = 10, UserId = 1 };
            var product = new Product { Id = 1, Name = "Product 1", Price = 100 };
            _unitOfWorkMock.Setup(x => x.GetRepository<Product>().GetById(order.ProductId)).ReturnsAsync(product);
            _unitOfWorkMock.Setup(x => x.GetRepository<Order>().ExecuteStoredProc("CreateOrder", It.IsAny<Dictionary<string, object>>()));

            // Act
            await _orderService.AddOrder(order);

            // Assert
            _unitOfWorkMock.Verify(x => x.GetRepository<Order>().ExecuteStoredProc("CreateOrder", It.IsAny<Dictionary<string, object>>()), Times.Once);
        }

        [Fact]
        public async Task GetOrderById_ShouldReturnOrder_WhenOrderExists()
        {
            // Arrange
            var order = new Order { Id = 1, ProductId = 1, Quantity = 10, UserId = 1 };
            _unitOfWorkMock.Setup(x => x.GetRepository<Order>().GetById(1)).ReturnsAsync(order);

            // Act
            var result = await _orderService.GetOrderById(1);

            // Assert
            Assert.Equal(order.Id, result.Id);
            Assert.Equal(order.Quantity, result.Quantity);
            Assert.Equal(order.UserId, result.UserId);
        }
    }
}