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
    [Route("api/[controller]")]
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
        public ActionResult<EmployeeEntity> GetEmployeeById(Guid id)
        {
            var item = _service.GetById(id);

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
        public IActionResult PostCheckLogin([FromBody]string username, [FromBody]string password)
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
            if (!ModelState.IsValid)
            {
                //return BadRequest(ModelState);
                return BadRequest("Input Data Mismatch or Username already Exist");
            }
            
            var item = _service.Add(value);

            //return CreatedAtAction("Get", new { id = item.Id }, item);

            return Ok("Employee data inserted successfully.");
        }

        // DELETE api/shoppingcart/5
        [HttpDelete("{id}")]
        public ActionResult Remove(Guid id)
        {
            var existingItem = _service.GetById(id);

            if (existingItem == null)
            {
                return NotFound("Invalid User Id");
            }

            _service.Remove(id);
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