using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementTool.DataModel
{

    public class EmployeeType
    {
        public int Id { get; set; }

        [Required]
        public string JobTitle { get; set; }
    }

    //public enum EmployeeTypeEnum
    //{
    //    FunctionalManager, 
    //    SoftwareEngineer, 
    //    Tester
    //}
}
