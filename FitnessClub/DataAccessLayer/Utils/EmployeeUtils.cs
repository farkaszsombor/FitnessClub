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
            bool result;
            using (var ctx=new NorthwindContext())
            {
                var query = (from emp in ctx.Employees
                            where emp.Name==name &&
                                   emp.IsDeleted == false
                            select emp.Password.ToString()).ToList();

                if(query.FirstOrDefault().ToString()==pwd)
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
    }
}
