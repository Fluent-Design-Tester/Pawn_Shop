using Pawn_Shop.Pages;
using Windows.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Pawn_Shop.Utilities;
using System.Diagnostics;
using Pawn_Shop.Pages.UpdatePrices;

namespace Pawn_Shop
{

    public sealed partial class MainPage : Windows.UI.Xaml.Controls.Page
    {
        public MainPage()
        { 
            this.InitializeComponent();

            // Set theme according to the previous load
            FrameworkElement root = (FrameworkElement)Window.Current.Content;
            root.RequestedTheme = AppSettings.Theme;

            // Set sound according to the previous load
            ElementSoundPlayer.State = AppSettings.Sound;


        }

        private void MainPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Get the current Windows Size
            var bounds = Window.Current.Bounds;
            double height = bounds.Height;
            // Debug.WriteLine(height);
            double width = bounds.Width;
            Debug.WriteLine(width);
        }

        private void NavigationView_Loaded(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(Pawns));
        }

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                sender.Header = "Settings";
                ContentFrame.Navigate(typeof(Settings));
            }
            else
            {
                NavigationViewItem item = args.SelectedItem as NavigationViewItem;

                switch (item.Tag.ToString())
                {
                    case "gold_calculator":
                        sender.Header = "Gold Calculator";
                        ContentFrame.Navigate(typeof(CalculateGold));
                        break;
                    case "accept_pawn":
                        sender.Header = "အပေါင်ပစ္စည်းများ";
                        ContentFrame.Navigate(typeof(Pawns));
                        break;
                    case "accept_pawns":
                        sender.Header = "အပေါင်ပစ္စည်းများ";
                        ContentFrame.Navigate(typeof(Pages.AcceptPawns.IndexPage));
                        break;
                    case "return_pawns":
                        sender.Header = "အပေါင်ပစ္စည်းရွေးခြင်း";
                        ContentFrame.Navigate(typeof(ReturnPawns));
                        break; 
                    case "kill_interest":
                        sender.Header = "အတိုးသတ်ခြင်း";
                        ContentFrame.Navigate(typeof(KillInterest));
                        break;
                    case "app_data":
                        sender.Header = "Setup Data";
                        ContentFrame.Navigate(typeof(Pages.AppData.IndexPage));
                        break;
                    case "update_gold_price":
                        sender.Header = "ရွှေဈေးနှုန်း";
                        ContentFrame.Navigate(typeof(UpdateGoldPrice));
                        break;
                    case "update_shop_price":
                        sender.Header = "ဆိုင်ဈေးနှုန်း ပြင်ဆင်သတ်မှတ်ရန်";
                        ContentFrame.Navigate(typeof(UpdateShopPrice));
                        break;
                    case "app_data_pawns_types":
                        sender.Header = "အပေါင်ပစ္စည်းအမျိုးအမည်များ";
                        ContentFrame.Navigate(typeof(Pages.AppData.PawnCategories));
                        break;
                    case "app_data_nrc":
                        sender.Header = "မှတ်ပုံတင်အချက်အလက်များ";
                        ContentFrame.Navigate(typeof(Pages.AppData.NRC));
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
