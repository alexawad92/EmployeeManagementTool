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

        public int EmployeeTypeId
        {
            get => _employee.EmployeeTypeId;
            set
            {
                _employee.EmployeeTypeId = value;
                OnPropertyChanged();
            }
        }

        public DateTime DateOfBirth
        {
            get => _employee.DateOfBirth;
            set
            {
                _employee.DateOfBirth = value;
                OnPropertyChanged();
            }
        }

        public Gender Gender
        {
            get => _employee.Gender;
            set
            {
                _employee.Gender = value;
                OnPropertyChanged();
            }
        }
    }
}
