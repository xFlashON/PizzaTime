using AdminUWP.Infrastructure;
using AdminUWP.Interfaces;
using AdminUWP.Model;
using AdminUWP.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace AdminUWP.ViewModels
{
    public class IngredientListViewModel:ObservableObject
    {
        IDataService _dataService;
        INavigation _navigationService;

        public ObservableCollection<IngredientViewModel> IngredientList { get; private set; }

        public IngredientViewModel SelectedItem { get; set; }

        private DelegateCommand _addCommand;

        public ICommand AddCommand
        {
            get => _addCommand ?? (_addCommand = new DelegateCommand(p => AddItem()));
        }

        private DelegateCommand _editCommand;

        public ICommand EditCommand
        {
            get => _editCommand ?? (_editCommand = new DelegateCommand(p => EditItem((IngredientViewModel)p)));
        }

        private DelegateCommand _deleteCommand;

        public ICommand DeleteCommand
        {
            get => _deleteCommand ?? (_deleteCommand = new DelegateCommand(p => DeleteItem((IngredientViewModel)p)));
        }

        public IngredientListViewModel()
        {

            _dataService = DependencyResolver.Resolve<IDataService>();
            _navigationService = DependencyResolver.Resolve<INavigation>();

            var data = Task.Run(() => _dataService.GetIngredientListAsync()).Result;

            IngredientList = Mapper.Map<ObservableCollection<IngredientViewModel>>(data);

        }

        private void AddItem()
        {
            _navigationService.NavigateTo("IngredientView");
        }

        private void EditItem(IngredientViewModel item)
        {
            if (item != null)

                _navigationService.NavigateTo("IngredientView", item);
        }

        async private void DeleteItem(IngredientViewModel item)
        {
            if (item != null)
            {

                ContentDialog deleteFileDialog = new ContentDialog()
                {
                    Title = "Alert",
                    Content = "Delete current item?",
                    PrimaryButtonText = "ОК",
                    SecondaryButtonText = "Cancel"
                };
                var selection = await deleteFileDialog.ShowAsync();

                if (selection == ContentDialogResult.Secondary)
                    return;

                bool result = await _dataService.DeleteIngredientAsync(Mapper.Map<Ingredient>(item));

                if (result == false)
                {
                    deleteFileDialog = new ContentDialog()
                    {
                        Title = "Alert",
                        Content = "This item can not be deleted!",
                        PrimaryButtonText = "ОК"
                    };
                    await deleteFileDialog.ShowAsync();
                }

            }

        }
    }
}
