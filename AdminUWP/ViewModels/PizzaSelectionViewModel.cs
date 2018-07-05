using AdminUWP.Infrastructure;
using AdminUWP.Interfaces;
using AdminUWP.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminUWP.ViewModels
{
    public class PizzaSelectionViewModel
    {

        private IDataService _dataService; 

        public ObservableCollection<PizzaViewModel> ItemsList { get; set; }

        public PizzaViewModel SelectedItem { get; set; }

        public PizzaSelectionViewModel()
        {
            _dataService = DependencyResolver.Resolve<IDataService>();

            var data = Task.Run(() => _dataService.GetIngredientListAsync()).Result;

            ItemsList = Mapper.Map<ObservableCollection<PizzaViewModel>> (data);

        }


    }
}
