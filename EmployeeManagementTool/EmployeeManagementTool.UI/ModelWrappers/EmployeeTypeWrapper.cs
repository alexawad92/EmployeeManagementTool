using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EmployeeManagementTool.DataModel;
using EmployeeManagementTool.ViewModels.Impls;


namespace EmployeeManagementTool.ModelWrappers
{
    public class EmployeeTypeWrapper : WrapperBase<EmployeeType>
    {

        public EmployeeTypeWrapper(EmployeeType employeeType):base(employeeType)
        {
        }

        public int Id
        {
            get { return Model.Id; }
        }

        public string JobTitle
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
            }
        }
    }
}
