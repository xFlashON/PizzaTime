using AdminUWP.Infrastructure;
using AdminUWP.Interfaces;
using AdminUWP.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace AdminUWP.ViewModels
{
    public class PizzaListViewModel : ObservableObject
    {
        IDataService _dataService;
        INavigation _navigationService;

        public ObservableCollection<PizzaViewModel> PizzaList { get; private set; }

        public PizzaViewModel SelectedItem { get; set; }

        private DelegateCommand _addCommand;

        public ICommand AddCommand
        {
            get => _addCommand ?? (_addCommand = new DelegateCommand(p => AddItem()));
        }

        private DelegateCommand _editCommand;

        public ICommand EditCommand
        {
            get => _editCommand ?? (_editCommand = new DelegateCommand(p => EditItem((PizzaViewModel)p)));
        }

        private DelegateCommand _deleteCommand;

        public ICommand DeleteCommand
        {
            get => _deleteCommand ?? (_deleteCommand = new DelegateCommand(p => DeleteItem((PizzaViewModel)p)));
        }

        public PizzaListViewModel()
        {

            _dataService = DependencyResolver.Resolve<IDataService>();
            _navigationService = DependencyResolver.Resolve<INavigation>();

            var data = Task.Run(() => _dataService.GetPizzaListAsync()).Result;

            PizzaList = Mapper.Map<ObservableCollection<PizzaViewModel>>(data);

        }

        private void AddItem()
        {
            _navigationService.NavigateTo("PizzaView");
        }

        private void EditItem(PizzaViewModel item)
        {
            if (item != null)

                _navigationService.NavigateTo("PizzaView", item);
        }

        async private void DeleteItem(PizzaViewModel item)
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

                bool result = await _dataService.DeletePizzaAsync(Mapper.Map<Pizza>(item));

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
