using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EmployeeManagementTool.DataModel;
using EmployeeManagementTool.ViewModels.Impls;


namespace EmployeeManagementTool.ModelWrappers
{
    public class EmployeeWrapper : WrapperBase<Employee>
    {
        public EmployeeWrapper(Employee employee):base(employee)
        {
        }

        public int Id => Model.Id;

        public string FirstName
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
            }
        }

        public string LastName
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
            }
        }

        public int EmployeeTypeId
        {
            get { return GetValue<int>(); }
            set
            {
                SetValue(value);
            }
        }

        public DateTime DateOfBirth
        {
            get { return GetValue<DateTime>(); }
            set
            {
                SetValue(value);
            }
        }

        public Gender Gender
        {
            get { return GetValue<Gender>(); }
            set
            {
                SetValue(value);
            }
        }

   
    }
}
