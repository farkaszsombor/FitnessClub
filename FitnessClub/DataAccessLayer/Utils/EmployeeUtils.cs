using DataAccessLayer.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Utils
{
    public class EmployeeUtils
    {
        public static bool AuthenticationEmployee(string name, string pwd)
        {
            int result = 0;
            using (var ctx=new NorthwindContext())
            {
                var query = from emp in ctx.Employees
                            where emp.Name==name &&
                                   emp.Password==pwd &&
                                   emp.IsDeleted == false
                            select emp.Id;
                result = query.FirstOrDefault();
            }
            return Convert.ToBoolean(result);

        }
    }
}
