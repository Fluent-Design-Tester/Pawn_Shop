using Pawn_Shop.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Pawn_Shop.Utilities;
using System.Diagnostics;
using Pawn_Shop.Pages.AppData;

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
                    case "accept_pawn":
                        sender.Header = "အပေါင်ပစ္စည်းများ";
                        ContentFrame.Navigate(typeof(Pawns));
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
                        ContentFrame.Navigate(typeof(IndexPage));
                        break;
                }
            }
        }
    }
}
