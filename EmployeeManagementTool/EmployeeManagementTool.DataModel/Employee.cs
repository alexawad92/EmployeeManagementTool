using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementTool.DataModel
{
    public class Employee
    {

        public Employee()
        {
            Teams = new List<Team>();
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }


        public EmployeeType EmployeeType { get; set; }


        public ICollection<Team> Teams { get; set; }

    }
}
