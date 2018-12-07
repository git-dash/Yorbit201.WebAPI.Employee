using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Employee.Repository.Models;
using Employee.Repository.Services;
namespace Employee.Repository.Controllers
{
    // [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;
        //public static List<String> errorLog = new List<string>();

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }


        // GET api/EmpMgt/getAllEmpDetails
        /// <summary>
        /// API is used to get all employee details 
        /// </summary>
        /// <returns>All Employee</returns>
        [HttpGet]
        [Route("api/EmpMgt/getAllEmpDetails")]
        public ActionResult<IEnumerable<EmployeeEntity>> GetAllEmployeeDetails()
        {
            var items = _service.GetAllItems();
            return Ok(items);
        }

        /// <summary>
        /// Get Employee with specified id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Employee</returns>
        [HttpGet("{id}")]
        [Route("api/EmpMgt/getByEmpId")]
        public ActionResult<EmployeeEntity> GetEmployeeById([FromQuery]string id)
        {
            var testId = new Guid(id);
            var item = _service.GetById(testId);

            if (item == null)
            {
                return NotFound($"No Employee found with id {id}");
            }

            return Ok(item);
        }

        /// <summary>
        ///  Use to check valid user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>ok if user is authenticate else 403</returns>
        [HttpPost]
        [Route("api/EmpMgt/checkLogin")]
        public ActionResult PostCheckLogin(string username, string password)
        {

            var user = _service.checkLogin(username, password);
            if (user == null)
            {
                return BadRequest("Invalid Username or Password.");
            }
            else
            {
                return Ok("Employee has authenticated successfully");
            }
        }

        // POST api/shoppingcart
        [HttpPut]
        [Route("api/EmpMgt/addEmp")]
        public ActionResult AddEmployee([FromBody] EmployeeEntity value)
        {
            if (ModelState.IsValid)
            {
                //return BadRequest(ModelState);
                var item = _service.Add(value);

                if (item == null)
                    return BadRequest("Input Data Mismatch or Username already Exist");
                else
                   return  Ok("Employee data inserted successfully.");

                //return CreatedAtAction("Get", new { id = item.Id }, item);
            }
            return BadRequest("Input Data Mismatch or Username already Exist");
        }

        // DELETE api/shoppingcart/5
        [HttpPut("{id}")]
        [Route("api/EmpMgt/deleteEmp")]
        public ActionResult Remove([FromQuery] string id)
        {
            var testId = new Guid(id);
            var existingItem = _service.GetById(testId);
            

            if (existingItem == null)
            {
                return NotFound("Invalid User Id");
            }

            _service.Remove(testId);
            return Ok("Employee data deleted successfully");
        }

        //[HttpGet]
        //[Route("api/EmpMgt/getErrorLog")]
        //public ActionResult getErrorLog()
        //{
        //    if (errorLog.Count() > 0)
        //    {
        //        return Ok(errorLog.ToList());
        //    }
        //    else
        //    {
        //        return NotFound("No Error Has been logged yet ");
        //    }
        //}
    }
}