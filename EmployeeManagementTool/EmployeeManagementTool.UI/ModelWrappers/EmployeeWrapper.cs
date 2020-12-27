using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EmployeeManagementTool.DataModel;
using EmployeeManagementTool.ViewModels.Impls;


namespace EmployeeManagementTool.ModelWrappers
{
    public class EmployeeWrapper : ViewModelBase
    {
        private readonly Employee _employee;
        public EmployeeWrapper(Employee employee)
        {
            _employee = employee;
        }

        public int Id => _employee.Id;

        public string FirstName
        {
            get => _employee.FirstName;
            set
            {
                _employee.FirstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get => _employee.LastName;
            set
            {
                _employee.LastName = value;
                OnPropertyChanged();
            }
        }

        public EmployeeType EmployeeType
        {
            get => _employee.EmployeeType;
            set
            {
                _employee.EmployeeType = value;
                OnPropertyChanged();
            }
        }
    }
}
