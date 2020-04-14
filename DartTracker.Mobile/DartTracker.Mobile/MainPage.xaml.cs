using DartTracker.Interface.Games;
using DartTracker.Mobile.ViewModels;
using DartTracker.Model.Enum;
using DartTracker.Model.Games;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DartTracker.Mobile
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : TabbedPage
    {
        private readonly MainPageViewModel _viewModel;

        public MainPage(
            MainPageViewModel viewModel
            )
        {
            _viewModel = viewModel;
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            this.LoadGameTab.BindingContext = this.BindingContext;
            base.OnBindingContextChanged();
        }

        private void OnNumberOfPlayersChanged(object sender, ValueChangedEventArgs e)
        {
            _viewModel.NumberOfPlayers = (int)e.NewValue;
        }
    }
}
