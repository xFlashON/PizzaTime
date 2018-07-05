using AdminUWP.Infrastructure;
using AdminUWP.Interfaces;
using AdminUWP.Model;
using AdminUWP.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace AdminUWP.ViewModels
{
    public class PizzaViewModel : ObservableObject
    {

        private IDataService _dataService;
        private INavigation _navigation;

        private bool _imageChanged;

        public Guid Id { get; set; }

        private string _name;
        public string Name { get => _name; set { _name = value; OnPropertyChanged("Name"); } }

        private decimal _price;
        public decimal Price { get => _price; set { _price = value; OnPropertyChanged("Price"); } }

        private string _description;
        public string Description { get => _description; set { _description = value; OnPropertyChanged("Description"); } }

        public string ImageUrl { get; set; }


        public string IngredientsListDisplay
        {
            get
            {

                return string.Join(Environment.NewLine, Ingredients);
            }
        }

        public ObservableCollection<IngredientViewModel> Ingredients { get; set; }

        public IngredientViewModel SelectedIngredient { get; set; }

        private BitmapImage _image;
        public BitmapImage Image {get=>_image??(_image=new BitmapImage()); set { _image = value; OnPropertyChanged("Image"); } }

        public PizzaViewModel()
        {

            _dataService = DependencyResolver.Resolve<IDataService>();
            _navigation = DependencyResolver.Resolve<INavigation>();

            Ingredients = new ObservableCollection<IngredientViewModel>();

        }

        public override string ToString()
        {
            return _name;
        }

        private DelegateCommand _selectIngredient;

        public DelegateCommand SelectIngredient { get => _selectIngredient ?? (_selectIngredient = new DelegateCommand((p => OpenSelectionDialog()))); }

        private DelegateCommand _deleteIngredient;

        public DelegateCommand DeleteIngredient
        {
            get => _deleteIngredient ?? (_deleteIngredient = new DelegateCommand((p =>
            {
                if (SelectedIngredient != null)
                    Ingredients.Remove(SelectedIngredient);
            })));
        }

        public DelegateCommand SelectImageFile
        {
            get => new DelegateCommand(async (p)  => 
            {
                var dlg = new FileOpenPicker();
                dlg.ViewMode = PickerViewMode.Thumbnail;
                dlg.FileTypeFilter.Add(".jpg");
                dlg.FileTypeFilter.Add(".png");
                dlg.CommitButtonText = "Open";

                var file = await dlg.PickSingleFileAsync();

                if (file is null)
                    return;

                IRandomAccessStream stream = await file.OpenReadAsync();
                _image.SetSource(stream);
                _imageChanged = true;

                OnPropertyChanged("Image");

            });
        }

        public async void OpenSelectionDialog()
        {

            var dialog = new IngredientSelectionView();

            dialog.PrimaryButtonText = "Select";

            dialog.SecondaryButtonText = "Cancel";

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                var selectedItem = dialog.Model.SelectedItem;

                if (selectedItem != null)
                {
                    if (Ingredients.FirstOrDefault(i => i.Name == selectedItem.Name) is null)
                        Ingredients.Add(selectedItem);

                }
            }

        }
    }
}
