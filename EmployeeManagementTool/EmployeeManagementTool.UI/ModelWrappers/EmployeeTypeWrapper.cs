using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EmployeeManagementTool.DataModel;
using EmployeeManagementTool.ViewModels.Impls;


namespace EmployeeManagementTool.ModelWrappers
{
    public class EmployeeTypeWrapper : ViewModelBase
    {
        private readonly EmployeeType _employeeType;

        public EmployeeTypeWrapper(EmployeeType employeeType)
        {
            _employeeType = employeeType;
        }

        public int Id
        {
            get { return _employeeType.Id; }
        }

        public string JobTitle
        {
            get { return _employeeType.JobTitle; }
            set
            {
                _employeeType.JobTitle = value;
                OnPropertyChanged();
            }
        }
    }
}
