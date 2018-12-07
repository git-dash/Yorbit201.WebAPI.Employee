using Employee.Repository.Controllers;
using Employee.Repository.Services;
using Employee.Repository.Models;
using System;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Employee.Repository.Test
{
    public class GetEndPointsTest
    {
        EmployeeController _controller;
        EmployeeServiceFake _service;


        public GetEndPointsTest()
        {
            _service = new EmployeeServiceFake();
            _controller = new EmployeeController(_service);


        }

        //get  all api
        [Fact]
        public void GetAllEmployeeDetails_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.GetAllEmployeeDetails();

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetAllEmployeeDetails_WhenCalled_ReturnsDefaultAllItems()
        {
            // Act
            var okResult = _controller.GetAllEmployeeDetails().Result as OkObjectResult;

            // Assert
            var items = Assert.IsType<List<EmployeeEntity>>(okResult.Value);
            Assert.Equal(3, items.Count);
        }


        //get employee by id
        [Fact]
        public void GetEmployeeById_WhenCalled_ReturnsOkResult()
        {
            var testId = new string("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");

            // Act
            var okResult = _controller.GetEmployeeById(testId).Result as OkObjectResult;

            // Assert
            Assert.IsType<EmployeeEntity>(okResult.Value);
            Assert.Equal(testId, (okResult.Value as EmployeeEntity).Id.ToString());

        }

        [Fact]
        public void GetEmployeeById_WhenCalled_ReturnsNotFoundResult()
        {
            var testId = new string("ababd817-98cd-4cf3-a80a-53ea0cd9c200");

            // Act
            var NotFoundResult = _controller.GetEmployeeById(testId).Result as ObjectResult;

            // Assert
            //Assert.IsType<EmployeeEntity>(NotFoundResult.Value);

            Assert.Equal($"No Employee found with id {testId}", NotFoundResult.Value);

            Assert.Equal(404, NotFoundResult.StatusCode);

        }




        //// check errorLog book .
        //[Fact]
        //public void Get_All_ErrorLogs()
        //{
        //    // Act
        //    var okResult = _controller.getErrorLog() as ObjectResult;

        //    var statusCode = okResult.StatusCode;
        //    Assert.Equal(404, statusCode);

        //}


    }
}
