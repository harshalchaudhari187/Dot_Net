using Employee_Management_MVC.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;

namespace Employee_Management_MVC.DAL
{
    public class EmployeeRepositoryImpl : IEmployeeRepository
    {
        List<Employee> list = new List<Employee>();
        public IEnumerable<Employee> GetAllEmployees()
        {
            //creating a connection and open it
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LabExamDB;Integrated Security=True";
            conn.Open();

            //create the commands to interact with DB
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select * from Employees";

            //Executing the query
            SqlDataReader dr = cmd.ExecuteReader();
            //Reading the data from DB row by row until the data reader empty & add it in the list 
            while (dr.Read())
            {
                list.Add(new Employee {Id = dr.GetInt32(0), Name = dr.GetString(1),City = dr.GetString(2),Address=dr.GetString(3)});
            }
            return list;
        }

        public Employee? GetEmployeeById(int id)
        {
            //creating a connection and open it
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LabExamDB;Integrated Security=True";
            conn.Open();

            //create the commands to interact with DB
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select * from Employees where Id = @Id";
            cmd.Parameters.AddWithValue("@Id", id);

            //Executing the query
            SqlDataReader dr = cmd.ExecuteReader();
            //Reading the data from DB row by row until the data reader empty & add it in the list 
            if (dr.Read())
            {
                Employee employee = new Employee { Id = dr.GetInt32(0), Name = dr.GetString(1), City = dr.GetString(2), Address = dr.GetString(3) };
                return employee;
            }
            else
            {
                return null;
            }
            
        }

        public string UpdateEmployee(Employee employee)
        {
            //creating a connection and open it
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LabExamDB;Integrated Security=True";
            conn.Open();

            SqlTransaction tx = conn.BeginTransaction();

            //create the commands to interact with DB
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.Transaction = tx;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "UpdateEmployee";
            //Path of stored procedure:-  Employee Management MVC\StoredProcedures\UpdateEmployeeProcedure
            //IF not found use this query for updation instead of Line no 77
            // UPDATE Employees set Name =@Name, City =@City, Address = @Address where Id = @Id
            ////and AT line No 76,replace this System.Data.CommandType.StoredProcedure TO System.Data.CommandType.Text


            cmd.Parameters.AddWithValue("@Name", employee.Name);
            cmd.Parameters.AddWithValue("@City", employee.City);
            cmd.Parameters.AddWithValue("@Address", employee.Address);
            cmd.Parameters.AddWithValue("@Id", employee.Id);


            try
            {
                cmd.ExecuteNonQuery();
                tx.Commit();
                return "Employee details update successfully";
            }catch (Exception ex)
            {
                tx.Rollback();
                throw new Exception(ex.Message);
            }

        }
    }
}
