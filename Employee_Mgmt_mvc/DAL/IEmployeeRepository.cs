using Employee_Management_MVC.Models;

namespace Employee_Management_MVC.DAL
{
    public interface IEmployeeRepository
    {
        //add a method to view all employee in the DB
        IEnumerable<Employee> GetAllEmployees();

        //add a method to update the details of an employee
        string UpdateEmployee(Employee employee);

        //add a method to display the details of choosen employee
        Employee? GetEmployeeById(int id);
    }
}
