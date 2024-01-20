using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiEmp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        // GET: api/<EmployeeController>
        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return Employee.GetAllEmployees();
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public Employee Get(int id)
        {
            return Employee.GetSingleEmployee(id);
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public void Post([FromBody] Employee value)
        {
            Employee.InsertEmployee(value);
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Employee value)
        {
            Employee.UpdateEmployee(value);
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Employee.DeleteEmployee(id);
        }
    }
}
