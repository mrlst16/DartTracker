﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace DartTracker.Mobile.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Command GoToGameCommand { get; protected set; }

        private int _numPlayers = 1;

        public int NumberOfPlayers
        {
            get => _numPlayers;
            set
            {
                _numPlayers = value;
                var args = new PropertyChangedEventArgs(nameof(NumberOfPlayers));
                PropertyChanged?.Invoke(this, args);
            }
        }

        public MainPageViewModel()
        {
            GoToGameCommand = new Command(async () =>
            {
                var vm = new CricketGameViewModel(NumberOfPlayers);
                var page = new CricketView(vm.GameService);
                page.BindingContext = vm;
                await Application.Current.MainPage.Navigation.PushAsync(page);
            });
        }
    }
}
