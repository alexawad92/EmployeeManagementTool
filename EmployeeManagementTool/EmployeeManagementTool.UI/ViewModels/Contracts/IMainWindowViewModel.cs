﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementTool.ViewModels.Contracts
{
    public interface IMainWindowViewModel : IViewModelBase
    {
        Task LoadAsync();
    }
}
