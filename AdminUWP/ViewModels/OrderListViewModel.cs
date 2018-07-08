using AdminUWP.Infrastructure;
using AdminUWP.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace AdminUWP.ViewModels
{
    public class OrderListViewModel : ObservableObject
    {

        IDataService _dataService;
        INavigation _navigationService;

        public ObservableCollection<OrderViewModel> OrdersList { get; set; }

        private OrderViewModel _selectedOrder;
        public OrderViewModel SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                _selectedOrder = value;
                OnPropertyChanged("OrderDetalisVisibility");
            }
        }

        public Visibility OrderDetalisVisibility
        {
            get
            {
                return (SelectedOrder is null ? Visibility.Collapsed : Visibility.Visible);
            }
        }

        public OrderListViewModel()
        {
            _dataService = DependencyResolver.Resolve<IDataService>();
            _navigationService = DependencyResolver.Resolve<INavigation>();

            var data = Task.Run(() => _dataService.GetOrderListAsync()).Result;

            OrdersList = Mapper.Map<ObservableCollection<OrderViewModel>>(data);
        }

    }
}
