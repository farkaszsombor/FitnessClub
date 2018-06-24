using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsDeleted { get; set; }
        public string Department { get; set; }
        public Room WorkPlace { get; set; }
        public int StartHour { get; set; }
        public int EndHour { get; set; }
        public string Days { get; set; }
    }
}