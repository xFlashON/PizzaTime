using AdminUWP.ViewModels;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AdminUWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PizzaView : Page
    {
        public PizzaViewModel Model;
        public PizzaView()
        {
            this.InitializeComponent();

            Model = new PizzaViewModel();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is PizzaViewModel)
                Model = (PizzaViewModel) e.Parameter;
        }
    }
}
