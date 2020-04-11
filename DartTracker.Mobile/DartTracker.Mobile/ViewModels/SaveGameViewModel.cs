using DartTracker.Data.Interface.DataServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartTracker.Mobile.ViewModels
{
    public class SaveGameViewModel
    {
        private readonly IGameDataService _gameDateService;

        public SaveGameViewModel(
            IGameDataService gameDataService
            )
        {
            _gameDateService = gameDataService;
        }
    }
}
