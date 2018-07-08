using AdminUWP.Infrastructure;
using AdminUWP.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminUWP.ViewModels
{
    public class CustomerListViewModel
    {
        IDataService _dataService;
        INavigation _navigationService;

        public ObservableCollection<CustomerViewModel> CustomersList { get; set; }


        public CustomerListViewModel()
        {
            _dataService = DependencyResolver.Resolve<IDataService>();
            _navigationService = DependencyResolver.Resolve<INavigation>();

            var data = Task.Run(() => _dataService.GetCustomerListAsync()).Result;

            CustomersList = Mapper.Map<ObservableCollection<CustomerViewModel>>(data.OrderBy(c=>c.Role).ThenBy(c=>c.Name));

        }


    }
}
