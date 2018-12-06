using System;
using System.Collections.Generic;
using System.Text;
using Employee.Repository.Controllers;
using Employee.Repository.Services;
using Employee.Repository.Models;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Employee.Repository.Test
{
    public class PostEndPointsTest
    {

        EmployeeController _controller;
        EmployeeServiceFake _service;

        public PostEndPointsTest()
        {
            _service = new EmployeeServiceFake();
            _controller = new EmployeeController(_service);

        }

        [Fact]
        public void AddEmployee_ReturnsOkResult()
        {
            EmployeeEntity newEmployee = new EmployeeEntity
            {
                Id = Guid.NewGuid(),
                FullName = "t1",
                Password = "t1",
                Username = "t1",
                DateOfBirth = DateTime.Now,
                EmailID = "t1@gmail.com",
                Gender = "Male",
                SecurityQuestion = "t1",
                SecurityAnswer = "t1"
            };

            var response = _controller.AddEmployee(newEmployee);
            Assert.IsType<OkObjectResult>(response);
        }

        [Fact]
        public void AddEmployee_ReturnsBadResult()
        {
            EmployeeEntity newEmployee = new EmployeeEntity
            {
                Id = Guid.NewGuid(),                
                //Name = "t1",
                Password = "t1",                
                Username = "t1"
            };
            _controller.ModelState.AddModelError("Name", "Required");

            var badResult = _controller.AddEmployee(newEmployee);
            Assert.IsType<BadRequestObjectResult>(badResult);
        }

        [Fact]
        public void checkLogin_ReturnsOkResult()
        {
            var okResult = _controller.PostCheckLogin("a", "a");

            Assert.IsType<OkObjectResult>(okResult);
        }
        [Fact]
        public void checkLogin_ReturnsErrorResult()
        {
            var badResult = _controller.PostCheckLogin("a23", "a") as BadRequestObjectResult;

            Assert.IsType<BadRequestObjectResult>(badResult);
            Assert.Equal(400, badResult.StatusCode);
            Assert.Equal("Invalid Username or Password.", badResult.Value);
        }


        [Fact]
        public void remove_WhenExistReturnsOkResult()
        {

            Guid TestGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");
            var okResult = _controller.Remove(TestGuid) as OkObjectResult;
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal("Employee data deleted successfully", okResult.Value);
        }
        [Fact]
        public void remove_WhenNotExistReturnsBadResult()
        {
            Guid TestGuid = new Guid("ab2cd817-98cd-4cf3-a80a-53ea0cd9c200");

            var badResult = _controller.Remove(TestGuid) as ObjectResult;

            Assert.IsType<NotFoundObjectResult>(badResult);
            Assert.Equal(404, badResult.StatusCode);
            Assert.Equal("Invalid User Id", badResult.Value);
        }

    }
}
