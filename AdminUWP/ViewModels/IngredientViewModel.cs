using AdminUWP.Infrastructure;
using AdminUWP.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminUWP.ViewModels
{
    public class IngredientViewModel:ObservableObject
    {
        private IDataService _dataService;
        private INavigation _navigation;

        public Guid Id { get; set; }

        private string _name;
        public string Name { get=>_name; set{ _name = value; OnPropertyChanged("Name"); } }

        private decimal _price;
        public decimal Price { get => _price; set { _price = value; OnPropertyChanged("Price"); } }

        public override string ToString()
        {
            return _name;
        }

        public IngredientViewModel()
        {
            _dataService = DependencyResolver.Resolve<IDataService>();
            _navigation = DependencyResolver.Resolve<INavigation>();
        }
    }
}
