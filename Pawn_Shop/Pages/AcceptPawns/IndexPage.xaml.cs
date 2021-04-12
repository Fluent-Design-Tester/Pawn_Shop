using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Pawn_Shop.Pages.AcceptPawns
{
    public sealed partial class IndexPage : Page
    {
        public IndexPage()
        {
            this.InitializeComponent();
        }

        private void GridTapped_Gold(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AcceptGold));
        }

        private void GridTapped_Vehicle(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AcceptVehicle));
        }

        private void GridTapped_House(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AcceptHouse));
        }

        private void GridTapped_Ground(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AcceptGround));
        }
    }
}
