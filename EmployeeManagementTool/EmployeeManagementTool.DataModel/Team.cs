using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementTool.DataModel
{
    public class Team
    {
        public Team()
        {
            Employees = new List<Employee>();
        }
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
