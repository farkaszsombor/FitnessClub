using DataAccessLayer.Contexts;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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
                ctx.Database.Connection.Open();
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

        public static Employee GetEmployeeByName(string name)
        {
            Employee e;
            using(var ctx = new NorthwindContext())
            {
                e = (from emp in ctx.Employees where emp.Name == name select emp).First();
            }
            return e;
        }

        public static bool UpdateEmployee(Employee Employee)
        {
            bool result = false;
            using(var ctx = new NorthwindContext())
            {
                using(var dbContextTransaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        Employee query = (from e in ctx.Employees where e.Id == Employee.Id select e).FirstOrDefault();
                        ctx.Entry(query).CurrentValues.SetValues(Employee);
                        ctx.SaveChanges();
                        dbContextTransaction.Commit();
                        result = true;
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        result = false;
                    }
                }
            }
            return result;
        }

        public static bool InsertEmployee(Employee Employee)
        {
            bool result = false;
            using(var ctx = new NorthwindContext())
            {
                using(var dbContextTransaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        var query = (from r in ctx.Rooms where r.Name == Employee.WorkPlace.Name select r).FirstOrDefault();
                        Employee.WorkPlace = query;
                        ctx.Employees.Add(Employee);
                        ctx.SaveChanges();
                        dbContextTransaction.Commit();
                        result = true;
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        result = false;
                    }
                }
            }
            return result;
        }

        public static bool DeleteEmployee(int Id)
        {
            bool result = false;
            using(var ctx = new NorthwindContext())
            {
                using(var dbContextTransaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        var query = (from emp in ctx.Employees where emp.Id == Id select emp).FirstOrDefault();
                        if (!query.IsDeleted)
                        {
                            query.IsDeleted = true;
                            ctx.SaveChanges();
                            dbContextTransaction.Commit();
                            result = true;
                        }
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        result = false;
                    }
                }
            }
            return result;
        }
    }
}
