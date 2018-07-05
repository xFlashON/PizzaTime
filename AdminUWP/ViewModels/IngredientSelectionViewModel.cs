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
    public class IngredientSelectionViewModel
    {

        private IDataService _dataService;

        public ObservableCollection<IngredientViewModel> ItemsList { get; set; }

        public IngredientViewModel SelectedItem { get; set; }

        public IngredientSelectionViewModel()
        {
            _dataService = DependencyResolver.Resolve<IDataService>();

            var data = Task.Run(() => _dataService.GetIngredientListAsync()).Result;

            ItemsList = Mapper.Map<ObservableCollection<IngredientViewModel>>(data);

 

        }

    }
}
