using DataAccessLayer.Contexts;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DataAccessLayer.Utils
{
    public class EmployeeUtils
    {
        public static bool AuthenticationEmployee(string name, string pwd)
        {
            bool result;
            using (var ctx=new NorthwindContext())
            {
                var query = (from emp in ctx.Employees
                            where emp.Name==name &&
                                   emp.IsDeleted == false
                            select emp.Password).ToList();

                if((query.FirstOrDefault()??"").ToString()==pwd)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            return Convert.ToBoolean(result);
        }

        public static List<Employee> GetAllEmplyees()
        {
            List<Employee> empContextList = new List<Employee>();
            using(var ctx = new NorthwindContext())
            {
                var query = from emp in ctx.Employees.Include(r => r.WorkPlace) select emp;
                empContextList.AddRange(query);
            }
            return empContextList;
        }
    }
}
