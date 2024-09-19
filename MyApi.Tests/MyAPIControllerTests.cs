using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TestEmploSkill.Service;
using TestEmploSkill.Controllers;
using TestEmploSkill.Model.Entities;
using Moq;

namespace MyApi.Tests
{
    [TestFixture]
    public class MyAPIControllerTests
    {
        private Mock<IProductService> _mockProductService;
        private ProductsController _controller;

        [SetUp]
        public void SetUp()
        {
            // Initialize the mock ProductService before each test
            _mockProductService = new Mock<IProductService>();
            _controller = new ProductsController(_mockProductService.Object);
        }

        [TestCase]
        public void GetItems_ReturnsOkResponse()
        {
            // Arrange
            var products = new List<Product>{};
            _mockProductService.Setup(service => service.GetAllProducts()).Returns(products);

            // Act
            var result = _controller.GetProducts();

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestCase]
        public void GetProductById_ReturnsOk_WhenProductExists()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Laptop", Price = 999.99M };
            _mockProductService.Setup(service => service.GetProductById(1)).Returns(product);

            // Act
            var result = _controller.GetProduct(1);

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var returnedProduct = okResult.Value as Product;
            Assert.IsNotNull(returnedProduct);
            Assert.AreEqual(1, returnedProduct.Id);
        }

        [TestCase]
        public void GetProductById_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            _mockProductService.Setup(service => service.GetProductById(999)).Returns((Product)null);

            // Act
            var result = _controller.GetProduct(999);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [TestCase]
        public void PostProduct_ReturnsCreated_WhenProductIsAdded()
        {
            // Arrange
            var newProduct = new Product { Name = "Tablet", Price = 499.99M };
            var addedProduct = new Product { Id = 1, Name = "Tablet", Price = 499.99M };
            _mockProductService.Setup(service => service.AddProduct(It.IsAny<Product>())).Returns(addedProduct);

            // Act
            var result = _controller.PostProduct(newProduct);

            // Assert
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdAtActionResult);
            Assert.AreEqual(201, createdAtActionResult.StatusCode);

            var returnedProduct = createdAtActionResult.Value as Product;
            Assert.IsNotNull(returnedProduct);
            Assert.AreEqual(1, returnedProduct.Id);
        }

        [TestCase]
        public void PutProduct_ReturnsNoContent_WhenProductIsUpdated()
        {
            // Arrange
            var updatedProduct = new Product { Id = 1, Name = "Updated Laptop", Price = 1099.99M };
            _mockProductService.Setup(service => service.UpdateProduct(1, updatedProduct)).Returns(true);

            // Act
            var result = _controller.PutProduct(1, updatedProduct);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [TestCase]
        public void PutProduct_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var updatedProduct = new Product { Id = 999, Name = "Non-Existing Product", Price = 1099.99M };
            _mockProductService.Setup(service => service.UpdateProduct(999, updatedProduct)).Returns(false);

            // Act
            var result = _controller.PutProduct(999, updatedProduct);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [TestCase]
        public void DeleteProduct_ReturnsNoContent_WhenProductIsDeleted()
        {
            // Arrange
            _mockProductService.Setup(service => service.DeleteProduct(1)).Returns(true);

            // Act
            var result = _controller.DeleteProduct(1);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [TestCase]
        public void DeleteProduct_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            _mockProductService.Setup(service => service.DeleteProduct(999)).Returns(false);

            // Act
            var result = _controller.DeleteProduct(999);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}