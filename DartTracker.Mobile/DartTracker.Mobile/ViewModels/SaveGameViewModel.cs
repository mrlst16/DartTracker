using DartTracker.Data.Interface.DataServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace DartTracker.Mobile.ViewModels
{
    public class SaveGameViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IGameDataService _gameDateService;

        public SaveGameViewModel(
            IGameDataService gameDataService
            )
        {
            _gameDateService = gameDataService;
        }


        public string Filename;


    }
}
