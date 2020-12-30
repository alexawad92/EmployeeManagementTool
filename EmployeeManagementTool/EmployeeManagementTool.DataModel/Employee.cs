using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementTool.DataModel
{
    public class Employee
    {

        public Employee()
        {
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        public int EmployeeTypeId { get; set; }

        public EmployeeType EmployeeType { get; set; }

        public int? TeamId { get; set; }

        public DateTime DateOfBirth{ get; set; }

        public Gender Gender { get; set; }

    }
}
