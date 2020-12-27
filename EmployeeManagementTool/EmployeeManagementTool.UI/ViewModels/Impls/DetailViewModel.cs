using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EmployeeManagementTool.DataAccessor.Contracts;
using EmployeeManagementTool.ModelWrappers;
using EmployeeManagementTool.ViewModels.Contracts;


namespace EmployeeManagementTool.ViewModels.Impls
{
    public class DetailViewModel : ViewModelBase, IDetailViewModel
    {
        private readonly IEmployeeAccessor _employeeAccessor;
        private EmployeeWrapper _employeeWrapper;

        public EmployeeWrapper EmployeeWrapper
        {
            get { return _employeeWrapper; }
            set
            {
                _employeeWrapper = value;
                OnPropertyChanged();
            }
        }
        public DetailViewModel(IEmployeeAccessor employeeAccessor)
        {
            _employeeAccessor = employeeAccessor;
        }

        public async Task LoadAsync(int id)
        {
            var employee = await _employeeAccessor.GetEmployeeByIdAsync(id);
            EmployeeWrapper = new EmployeeWrapper(employee);
        }
    }
}
