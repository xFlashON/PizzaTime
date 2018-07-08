using AdminUWP.Infrastructure;
using AdminUWP.Interfaces;
using AdminUWP.Model;
using AdminUWP.Models;
using AdminUWP.Views;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

        private string _description = string.Empty;
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
        public BitmapImage Image
        {
            get
            {
                if (_image != null)
                    return _image;
                else
                {
                    _image = new BitmapImage();

                    if (ImageUrl == string.Empty)
                        return _image;


                    //var arr = _dataService.GetImageData(ImageUrl).Result;

                    //using (MemoryStream ms = new MemoryStream(arr))
                    //{

                    //   _image.SetSourceAsync(ms.AsRandomAccessStream());
                    //}

                    Image.UriSource = new Uri(_dataService.GetApiUrl() + ImageUrl);

                    return _image;

                }

            }
            set
            {
                _image = value;
                OnPropertyChanged("Image");
            }
        }

        private Byte[] _imageData;
        private string _imageType;

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

        private DelegateCommand _saveItemCmd;

        public DelegateCommand SaveItemCmd { get => _saveItemCmd ?? (_saveItemCmd = new DelegateCommand((p => SaveItem()))); }

        private DelegateCommand _cancelCmd;

        public DelegateCommand CancelCmd { get => _cancelCmd ?? (_cancelCmd = new DelegateCommand((p => { _navigation.Return(); }))); }

        public DelegateCommand SelectImageFile
        {
            get => new DelegateCommand(async (p) =>
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

                stream.Seek(0);

                _imageData = new byte[stream.Size];

                await stream.ReadAsync(_imageData.AsBuffer(), (uint)stream.Size, Windows.Storage.Streams.InputStreamOptions.None);

                switch (file.FileType)
                {
                    case ".jpg":
                        _imageType = "image/jpeg";
                        break;

                    case ".png":
                        _imageType = "image/png";
                        break;

                    default:
                        _imageType = "";
                        break;
                }

                OnPropertyChanged("Image");

            });
        }

        private DelegateCommand _addPriceCmd;
        public DelegateCommand AddPriceCmd { get => _addPriceCmd ?? (_addPriceCmd = new DelegateCommand((p) => AddPrice((PizzaViewModel)p))); }

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

        public async void SaveItem()
        {

            var result = await _dataService.SavePizzaAsync(Mapper.Map<Pizza>(this));

            if (result is null)
            {
                ContentDialog dlg = new ContentDialog();
                dlg.Content = "Item is not saved!";
                dlg.PrimaryButtonText = "Ok";

                await dlg.ShowAsync();

                return;

            }

            if (_imageChanged)
            {

                var isSaved = await _dataService.SaveImageAsync(result.Id, _imageData, _imageType);
               
                Id = result.Id;

                ImageUrl = "api/data/getImage/" + Id;

                await _dataService.SavePizzaAsync(Mapper.Map<Pizza>(this));

            }

            _navigation.NavigateTo("PizzaListView");

        }

        private async void AddPrice(PizzaViewModel p)
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

                var response = await _dataService.SavePizzaPriceAsync(Mapper.Map<Pizza>(p), datePicker.Date.Date, price);

                if (response != true)
                {
                    dlg = new ContentDialog();
                    dlg.Content = "Price is not saved!";
                    dlg.PrimaryButtonText = "Ok";

                    await dlg.ShowAsync();
                }

                _navigation.NavigateTo("PizzaListView");

            }


        }

    }
}
