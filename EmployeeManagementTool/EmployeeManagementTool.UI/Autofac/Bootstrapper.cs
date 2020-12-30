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

        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<NavigationSelectionChangedEvent>().As<INavigationSelectionChangedEvent>().SingleInstance();
            builder.RegisterType<DetailViewModelSavedEvent>().As<IDetailViewModelSavedEvent>().SingleInstance();
            builder.RegisterType<ManagementViewModelSelectionChangedEvent>().As<IManagementViewModelSelectionChangedEvent>().SingleInstance();
            builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();
            builder.RegisterType<EmployeeManagementViewModel>().Keyed<IMainWindowViewModel>(nameof(EmployeeManagementViewModel));
            builder.RegisterType<TeamManagementViewModel>().Keyed<IMainWindowViewModel>(nameof(TeamManagementViewModel));
            builder.RegisterType<HomeViewModel>().Keyed<IMainWindowViewModel>(nameof(HomeViewModel));
            builder.RegisterType<EmployeeDetailViewModel>().Keyed<IDetailViewModel>(nameof(EmployeeDetailViewModel));
            builder.RegisterType<TeamDetailViewModel>().Keyed<IDetailViewModel>(nameof(TeamDetailViewModel));
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<EmployeeManagementToolDbContext>().AsSelf();
            builder.RegisterType<EmployeeAccessor>().As<IEmployeeAccessor>();
            builder.RegisterType<EmployeeTypeAccessor>().As<IEmployeeTypeAccessor>();
            builder.RegisterType<TeamDataLookupRepository>().AsImplementedInterfaces();
            builder.RegisterType<EmployeeDataLookupRepository>().AsImplementedInterfaces();
            builder.RegisterType<TeamAccessor>().As<ITeamAccessor>();
            return builder.Build();
        }
    }
}
