using System;
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

        public ObservableCollection<CricketPlayerScoreboardVM> PlayerScoreboards { get; }
            = new ObservableCollection<CricketPlayerScoreboardVM>()
            {
                new CricketPlayerScoreboardVM(){
                    PlayerName = "1",
                    Score = 0
                },
                new CricketPlayerScoreboardVM(){
                    PlayerName = "2",
                    Score = 0
                }
            };

        public Command Add5PointsCommand { get; }

        public MainPageViewModel()
        {
            HitSingleCommand = new Command((i) =>
            {

                var args = new PropertyChangedEventArgs(nameof(PlayerScoreboards));
                PropertyChanged?.Invoke(this, args);
            });
        }

        public Command HitSingleCommand { get; }
    }
}
