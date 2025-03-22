using Xunit;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SchoolApi.API.Controllers;
using SchoolApi.Application.DTOs;
using SchoolApi.Application.Interfaces;
using SchoolApi.Domain.Entities;

namespace SchoolApi.Tests.Controllers
{
    public class TeacherControllerTests
    {
        private readonly Mock<ITeacherService> _mockService;
        private readonly TeacherController _controller;

        public TeacherControllerTests()
        {
            _mockService = new Mock<ITeacherService>();
            _controller = new TeacherController(_mockService.Object);
        }

        [Fact]
        public async Task Create_ValidTeacher_ReturnsStatusCode200()
        {
            // Arrange
            var dto = new TeacherCreateDto { Name = "Test Teacher" };
            var createdTeacher = new Teacher { Id = 1, Name = dto.Name };

            _mockService.Setup(s => s.CreateAsync(It.IsAny<Teacher>()))
                        .ReturnsAsync(createdTeacher);

            // Act
            var result = await _controller.Create(dto);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            var response = objectResult.Value;

            var statusProp = response?.GetType().GetProperty("status")?.GetValue(response);
            var messageProp = response?.GetType().GetProperty("message")?.GetValue(response);
            var dataProp = response?.GetType().GetProperty("data")?.GetValue(response);

            Assert.Equal(200, statusProp);
            Assert.Equal("Teacher created successfully", messageProp);
            Assert.NotNull(dataProp);
        }

        [Fact]
        public async Task Create_TeacherCreationFails_ReturnsServerError()
        {
            // Arrange
            var dto = new TeacherCreateDto { Name = "Test Teacher" };
            _mockService.Setup(s => s.CreateAsync(It.IsAny<Teacher>()))
                        .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _controller.Create(dto);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);

            var response = objectResult.Value;

            var statusProp = response?.GetType().GetProperty("status")?.GetValue(response);
            var messageProp = response?.GetType().GetProperty("message")?.GetValue(response);
            var dataProp = response?.GetType().GetProperty("data")?.GetValue(response);

            Assert.Equal(500, statusProp);
            Assert.NotNull(messageProp);
            Assert.Contains("Internal Server Error", messageProp.ToString());
            Assert.Null(dataProp);
        }


        [Fact]
        public async Task Create_InvalidModelState_ReturnsBadRequest()
        {
            // Arrange
            var dto = new TeacherCreateDto { Name = "" };
            _controller.ModelState.AddModelError("Name", "Name is required");

            // Act
            var result = await _controller.Create(dto);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequest.StatusCode);
        }

        [Fact]
        public async Task GetAll_ReturnsListOfTeachers()
        {
            // Arrange
            var teachers = new List<Teacher>
            {
                new Teacher { Id = 1, Name = "Teacher A" },
                new Teacher { Id = 2, Name = "Teacher B" }
            };

            _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(teachers);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = okResult.Value as IEnumerable<Teacher>;
            Assert.NotNull(data);
        }

        [Fact]
        public async Task GetById_TeacherExists_ReturnsTeacher()
        {
            // Arrange
            var teacher = new Teacher { Id = 1, Name = "Teacher A" };

            _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(teacher);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = okResult.Value as Teacher;
            Assert.Equal("Teacher A", data!.Name);
        }

        [Fact]
        public async Task GetById_TeacherNotFound_ReturnsNotFound()
        {
            // Arrange
            _mockService.Setup(s => s.GetByIdAsync(999)).ReturnsAsync((Teacher?)null);

            // Act
            var result = await _controller.GetById(999);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
        }
    }
}
