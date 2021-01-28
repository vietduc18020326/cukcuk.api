using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore;
using MISA.ApplicationCore.Interface;
using MISA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        IEmployeeBL _employeeBL;
        public EmployeesController(IEmployeeBL employeeBL)
        {
            _employeeBL = employeeBL;
        }
        // GET: api/<EmployeesController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_employeeBL.GetEmployees<Employee>());
        }
        [HttpGet("count")]
        public IActionResult GetCount()
        {
            return Ok(_employeeBL.GetCountData<Employee>());
        }
        [HttpGet("page/{offset}&{size}")]
        public IActionResult GetPage(int offset,int size)
        {
            return Ok(_employeeBL.GetDataPage<Employee>(offset,size));
        }


        [HttpGet("department")]
        public IActionResult Get2()
        {
            return Ok(_employeeBL.GetEmployees<EmployeeDepartment>());
        }
        [HttpGet("position")]
        public IActionResult Get3()
        {
            return Ok(_employeeBL.GetEmployees<EmployeePosition>());
        }
        [HttpGet("max")]
        public IActionResult Get4()
        {
            return Ok(_employeeBL.GetEmployeeCodeM<Employee>());
        }


        // GET api/<EmployeesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(_employeeBL.GetDataById<Employee>(id.ToString()));
        }

        // POST api/<EmployeesController>
        [HttpPost]
        public IActionResult Post(Employee employee)
        {
            var rowEffect = _employeeBL.InsertEmployee(employee);
            if (rowEffect == -2)
            {
                return BadRequest("Chứng minh thư bị trùng");
            }
            if (rowEffect == -1)
            {
                return BadRequest("Số điện thoại bị trùng");
            }
            
            return Ok(rowEffect);
        }

        // PUT api/<EmployeesController>/5
        [HttpPut]
        public IActionResult Put(Employee employee)
        {
            var rowEffect = _employeeBL.UpdateEmployee(employee);
            if (rowEffect == -2)
            {
                return BadRequest("Chứng minh thư bị trùng");
            }
            if (rowEffect == -1)
            {
                return BadRequest("Số điện thoại bị trùng");
            }
            return Ok(rowEffect);
        }

        // DELETE api/<EmployeesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            Employee employee = new Employee();
            employee.EmployeeId = Guid.Parse(id);
            var effect = _employeeBL.DeleteEmployee(employee);
            if (effect == -1)
                return BadRequest("Id không tồn tại");
            return Ok(effect);
        }


    }
}
