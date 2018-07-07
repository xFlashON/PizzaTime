﻿using AdminUWP.Infrastructure;
using AdminUWP.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace AdminUWP.ViewModels
{
    public class MainPageViewModel : ObservableObject
    {

        private IDataService _dataService;

        private bool _isAuthorised;
        public bool IsAuthorised { get => _isAuthorised; private set { _isAuthorised = value; OnPropertyChanged("IsAuthorised"); } }

        private SolidColorBrush _bgColour;
        public SolidColorBrush BgColour { get => _bgColour; set { _bgColour = value; OnPropertyChanged("BgColour"); } }

        private string _login;
        public string Login
        {
            get => _login; set
            {
                _login = value;
                OnPropertyChanged("Login");
                ApplicationData.Current.LocalSettings.Values["Login"] = value;
            }
        }

        private string _password;
        public string Password
        {
            get => _password; set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        private string _apiUrl;
        public string ApiUrl
        {
            get => _apiUrl; set
            {
                _apiUrl = value;
                OnPropertyChanged("ApiUrl");
                ApplicationData.Current.LocalSettings.Values["ApiUrl"] = value;
            }
        }

        public DelegateCommand SelectPage { get; set; }

        public DelegateCommand CloseApp { get => new DelegateCommand((p) => { App.Current.Exit(); }); }

        private DelegateCommand _authorise { get; set; }
        public DelegateCommand Authorise
        {
            get => _authorise ?? (_authorise = new DelegateCommand(async (p) =>
            {
                IsAuthorised = await _dataService.Authorise(Login,Password, ApiUrl);

                if (!IsAuthorised)
                {
                    BgColour.Color = Colors.Red;
                    DispatcherTimer t = new DispatcherTimer();
                    t.Interval = new TimeSpan(0, 0, 3);
                    t.Tick += (p1, p2) =>
                    {
                        BgColour.Color = Colors.Blue;
                        ((DispatcherTimer)p1).Stop();
                    };
                    t.Start();
                }
                else
                {
                    SelectPage.Execute("PizzaListView");
                }

                SelectPage.RaiseCanExecuteChanged();

            }));
        }

        public MainPageViewModel()
        {
            _dataService = DependencyResolver.Resolve<IDataService>();

            BgColour = new SolidColorBrush(Colors.Blue);

            if (ApplicationData.Current.LocalSettings.Values.ContainsKey("Login"))
                Login = ApplicationData.Current.LocalSettings.Values["Login"].ToString();

            if (ApplicationData.Current.LocalSettings.Values.ContainsKey("ApiUrl"))
                ApiUrl = ApplicationData.Current.LocalSettings.Values["ApiUrl"].ToString();
            else
                ApiUrl = "http://localhost:8080/";
        }

        public bool MenuCommandCanExecute(object o)
        {
            return _isAuthorised;
        }


    }
}
