using Couchbase.Lite;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DartTracker.Mobile
{
    public partial class App : Application
    {

        private DatabaseConfiguration databaseConfiguration = new DatabaseConfiguration
        {
            Directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "darttracker")
        };

        private Database _database;
        private Database DataBase
        {
            get
            {
                if (_database == null)
                    _database = new Database("darttracker", databaseConfiguration);
                return _database;
            }
        }

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
                            var gameService = DartTracker.Mobile.MainPage.GameService;
                            var gameId = gameService.Game.ID.ToString();

                            MutableDocument gameDocument = new MutableDocument(gameId);
                            gameDocument.SetString("json", JsonConvert.SerializeObject(gameService.Game));
                            DataBase.Save(gameDocument);

                            List<string> indexList = new List<string>();

                            Document gameIndexDocument = DataBase.GetDocument("activegames");
                            if (gameIndexDocument != null)
                            {
                                var indexArray = gameIndexDocument.GetArray("value");
                                indexList = indexArray?.Select(x => x.ToString()).ToList() ?? new List<string>();
                            }
                            if (!indexList.Contains(gameId))
                            {
                                indexList.Add(gameId);
                                var mutableIndexDocument = new MutableDocument("activegames");
                                ArrayObject arrayObject = new MutableArrayObject(indexList);
                                mutableIndexDocument.SetArray("value", arrayObject);
                                DataBase.Save(mutableIndexDocument);
                            }
                        }
                    }
                });
    }
}
