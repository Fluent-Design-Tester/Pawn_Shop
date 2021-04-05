using Pawn_Shop.Utilities;
using System;
using System.Collections.Generic;
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

namespace Pawn_Shop
{
    public sealed partial class Settings : Page
    {
        public Settings()
        {
            this.InitializeComponent();

            // Set app theme
            FrameworkElement root = (FrameworkElement)Window.Current.Content;
            root.RequestedTheme = AppSettings.Theme;
            SetThemeToggle(AppSettings.Theme);

            // Set app sound 
            ElementSoundPlayer.State = AppSettings.Sound;
            SetSoundToggle(AppSettings.Sound);
        }

        // Set the sound toggle to the correct position (off for off sound which is default, and on for on).
        private void SetSoundToggle(ElementSoundPlayerState soundState)
        {
            if (soundState == AppSettings.OFF)
                tglAppSound.IsOn = false;
            else
                tglAppSound.IsOn = true;
        }

        // Set the theme toggle to the correct position (off for Light mode which is default, and on for Dark mode, non-default).
        public void SetThemeToggle(ElementTheme theme)
        {
            if (theme == AppSettings.DEFAULT_THEME)
                tglAppTheme.IsOn = false;
            else
                tglAppTheme.IsOn = true;
        }

        // Switch the app's theme between light mode and dark mode, and save that setting.
        private void ThemeToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            FrameworkElement window = (FrameworkElement)Window.Current.Content;

            if (((Windows.UI.Xaml.Controls.ToggleSwitch)sender).IsOn)
            {
                AppSettings.Theme = AppSettings.NON_DEFAULT_THEME;
                window.RequestedTheme = AppSettings.NON_DEFAULT_THEME;
            }
            else
            {
                AppSettings.Theme = AppSettings.DEFAULT_THEME;
                window.RequestedTheme = AppSettings.DEFAULT_THEME;
            }
        }

        // Switch the app's sound between on and off, and save that setting.
        private void SoundToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (((Windows.UI.Xaml.Controls.ToggleSwitch)sender).IsOn)
            {
                AppSettings.Sound = AppSettings.ON;
                ElementSoundPlayer.State = ElementSoundPlayerState.On;
            }
            else
            {
                AppSettings.Sound = AppSettings.OFF;
                ElementSoundPlayer.State = ElementSoundPlayerState.Off;
            }
        }
    }
}
