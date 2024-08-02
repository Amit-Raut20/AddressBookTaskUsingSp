using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmployeeDirectory.Models
{
    public class Employee
    {
        [Key]
        //[JsonIgnore]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
            
        public string? Mobile { get; set; }
        public string? Landline { get; set; }
            
        public string? Website { get; set; }
            
        public string? Address { get; set; }
    }
}
