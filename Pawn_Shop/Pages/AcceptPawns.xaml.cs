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
            int a = Category_ComboBox.SelectedIndex;
            Debug.WriteLine(a);

            String b = Category_ComboBox.SelectedItem.ToString();
            Debug.WriteLine(b);
        }
    }
}
