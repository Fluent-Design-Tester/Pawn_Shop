using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Pawn_Shop.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AcceptPawns : Page
    {

        public AcceptPawns()
        {
            this.InitializeComponent();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Pawns));
        }

        private void Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedCategory = Category_ComboBox.SelectedIndex;

            // if category is gold, show weight.
            if (selectedCategory == 0)
            {
                weight.Visibility = Visibility.Visible;
                currentNameForCategory.Visibility = Visibility.Collapsed;
                boardNumber.Visibility = Visibility.Collapsed;

                pawnNameAndCount.PlaceholderText = "လက်စွပ် ၁ ကွင်း";
            }

            // if category is cycle and car, show.
            if (selectedCategory == 1 || selectedCategory == 2)
            {
                weight.Visibility = Visibility.Collapsed;
                currentNameForCategory.Visibility = Visibility.Visible;
                boardNumber.Visibility = Visibility.Visible;

                if (selectedCategory == 1)
                {
                    pawnNameAndCount.PlaceholderText = "Kenbo ၁ စီး";
                }
                else
                {
                    pawnNameAndCount.PlaceholderText = "Parjido ၁ စီး";
                }

            }

            // if category is house, show.
            if (selectedCategory == 3)
            {
                weight.Visibility = Visibility.Collapsed;
                boardNumber.Visibility = Visibility.Collapsed;

                pawnNameAndCount.PlaceholderText = "လုံးချင်း ၁ ထပ်အိပ်";
            }
        }
    }
}
