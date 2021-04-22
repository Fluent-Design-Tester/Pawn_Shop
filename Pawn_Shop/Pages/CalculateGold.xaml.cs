using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Pawn_Shop.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CalculateGold : Page
    {
        public CalculateGold()
        {
            this.InitializeComponent();
        }

        private void ToggleSwitch_GM_Toggled(object sender, RoutedEventArgs e)
        {
            if (((ToggleSwitch)sender).IsOn)
            {
                TextBox_Gram.Visibility = Visibility.Visible;
                Button_CalculateToKPY.Visibility = Visibility.Visible;
            }
            else
            {
                TextBox_Gram.Visibility = Visibility.Collapsed;
                Button_CalculateToKPY.Visibility = Visibility.Collapsed;
            }
        }

    }
}
