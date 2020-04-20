using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DartTracker.Mobile.ViewModels
{
    public class SaveGameViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _saveAs = string.Empty;
        public string SaveAs
        {
            get => _saveAs;
            set
            {
                _saveAs = value
                    ?.Replace(" ", "")
                    ?.Replace(System.Environment.NewLine, "");
                var args = new PropertyChangedEventArgs(nameof(SaveAs));
                PropertyChanged?.Invoke(this, args);
            }
        }
    }
}
