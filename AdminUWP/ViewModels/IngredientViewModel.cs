using AdminUWP.Infrastructure;
using AdminUWP.Interfaces;
using AdminUWP.Model;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace AdminUWP.ViewModels
{
    public class IngredientViewModel : ObservableObject
    {
        private IDataService _dataService;
        private INavigation _navigation;

        public Guid Id { get; set; }

        private string _name;
        public string Name { get => _name; set { _name = value; OnPropertyChanged("Name"); } }

        private decimal _price;
        public decimal Price { get => _price; set { _price = value; OnPropertyChanged("Price"); } }

        private ICommand _addPriceCmd;
        public ICommand AddPriceCmd { get => _addPriceCmd ?? (_addPriceCmd = new DelegateCommand((p) => AddPrice((IngredientViewModel)p))); }

        public override string ToString()
        {
            return _name;
        }

        public IngredientViewModel()
        {
            _dataService = DependencyResolver.Resolve<IDataService>();
            _navigation = DependencyResolver.Resolve<INavigation>();
        }

        private DelegateCommand _saveItemCmd;

        public DelegateCommand SaveItemCmd { get => _saveItemCmd ?? (_saveItemCmd = new DelegateCommand((p => SaveItem()))); }

        private DelegateCommand _cancelCmd;

        public DelegateCommand CancelCmd { get => _cancelCmd ?? (_cancelCmd = new DelegateCommand((p => { _navigation.Return(); }))); }

        private async void SaveItem()
        {

            var result = await _dataService.SaveIngredientAsync(Mapper.Map<Ingredient>(this));

            if (result is null)
            {
                ContentDialog dlg = new ContentDialog();
                dlg.Content = "Item is not saved!";
                dlg.PrimaryButtonText = "Ok";

                await dlg.ShowAsync();

                return;

            }

            _navigation.NavigateTo("IngredientListView");

        }

        private async void AddPrice(IngredientViewModel p)
        {

            if (p is null)
                return;

            ContentDialog dlg = new ContentDialog();

            var panel = new StackPanel() { Orientation = Orientation.Vertical };

            panel.Children.Add(new TextBlock() { Text = "Date" });

            var datePicker = new DatePicker();

            panel.Children.Add(datePicker);

            panel.Children.Add(new TextBlock() { Text = "Price" });

            var priceInput = new TextBox() { Text = "0" };

            panel.Children.Add(priceInput);

            dlg.Content = panel;

            dlg.PrimaryButtonText = "Save";
            dlg.CloseButtonText = "Cancel";

            var result = await dlg.ShowAsync();

            decimal price;

            if (result == ContentDialogResult.Primary && decimal.TryParse(priceInput.Text, out price) && price > 0)
            {

                var response = await _dataService.SaveIngredientPriceAsync(Mapper.Map<Ingredient>(p), datePicker.Date.Date, price);

                if (response != true)
                {
                    dlg = new ContentDialog();
                    dlg.Content = "Price is not saved!";
                    dlg.PrimaryButtonText = "Ok";

                    await dlg.ShowAsync();
                }

                _navigation.NavigateTo("IngredientListViewModel");

            }


        }

    }
}
