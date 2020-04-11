using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DartTracker.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var page = new NavigationPage(new MainPage());
            page.Popped += PromptToSaveGame();

            MainPage = page;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private EventHandler<NavigationEventArgs> PromptToSaveGame()
            => new EventHandler<NavigationEventArgs>(async (s, e) =>
                {
                    var title = e.Page.Title;
                    if (title.ToLowerInvariant() == "dartboard")
                    {
                        var result = await MainPage.DisplayAlert("Save game", "Would you like to save that game?", "yes", "No");
                        if (result)
                        {
                            
                        }
                    }
                });
    }
}
