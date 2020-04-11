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
        public static Game Game { get; set; }

        public MainPage()
        {


            InitializeComponent();
        }

    }
}
