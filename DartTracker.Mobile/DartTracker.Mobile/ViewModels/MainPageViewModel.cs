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

        public ObservableCollection<PlayerScoreboardVM> PlayerScoreboards { get; }
            = new ObservableCollection<PlayerScoreboardVM>()
            {
                new PlayerScoreboardVM(){
                    PlayerName = "1",
                    Score = 0
                },
                new PlayerScoreboardVM(){
                    PlayerName = "2",
                    Score = 0
                }
            };

        public Command Add5PointsCommand { get; }

        public MainPageViewModel()
        {
            Add5PointsCommand = new Command(() =>
            {
                for (int i = 0; i < PlayerScoreboards.Count; i++)
                {
                    var x = PlayerScoreboards[i];
                    x.Score += 5;
                    PlayerScoreboards[i] = x;
                }
                var args = new PropertyChangedEventArgs(nameof(PlayerScoreboards));
                PropertyChanged?.Invoke(this, args);
            });
        }
    }
}
