using AdminUWP.Infrastructure;
using AdminUWP.Interfaces;
using AdminUWP.ViewModels;
using AdminUWP.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AdminUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, INavigation
    {
        public MainPageViewModel Model;

        public MainPage()
        {
            this.InitializeComponent();

            Model = new MainPageViewModel();

            DependencyResolver.AddDependecy<INavigation>(this);

            Model.SelectPage = new DelegateCommand((p) =>
            {
                NavigateTo((string)p);
            }, Model.MenuCommandCanExecute);
        }

        public void NavigateTo(string page, object param = null)
        {
            Type target = Type.GetType($"AdminUWP.Views.{(string)page}");

            if (target != null)
                Content.Navigate(target, param);
        }

        public void Return()
        {
            if (Content.CanGoBack)
                Content.GoBack();
        }
    }
}
