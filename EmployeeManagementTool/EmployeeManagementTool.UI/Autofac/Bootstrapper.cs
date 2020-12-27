using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autofac;

using EmployeeManagementTool.DataAccess;
using EmployeeManagementTool.DataAccessor.Contracts;
using EmployeeManagementTool.DataAccessor.Impls;
using EmployeeManagementTool.Events.Contracts;
using EmployeeManagementTool.Events.Impls;
using EmployeeManagementTool.ViewModels.Contracts;
using EmployeeManagementTool.ViewModels.Impls;


namespace EmployeeManagementTool.Autofac
{
    public class Bootstrapper
    {

        public IContainer Boostrap()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<NavigationSelectionChangedEvent>().As<INavigationSelectionChangedEvent>().SingleInstance();
            builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<EmployeeManagementToolDbContext>().AsSelf();
            builder.RegisterType<EmployeeAccessor>().As<IEmployeeAccessor>();
            return builder.Build();
        }
    }
}
