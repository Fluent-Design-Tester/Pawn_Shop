using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;

namespace Pawn_Shop.Utilities
{
    class AppSettings
    {
        public const ElementTheme DEFAULT_THEME = ElementTheme.Light;
        public const ElementTheme NON_DEFAULT_THEME = ElementTheme.Dark;

        public const ElementSoundPlayerState ON = ElementSoundPlayerState.On;
        public const ElementSoundPlayerState OFF = ElementSoundPlayerState.Off;

        const string KEY_THEME = "appColourMode";
        const string KEY_SOUND = "appSoundMode";
        static ApplicationDataContainer LOCAL_SETTINGS = ApplicationData.Current.LocalSettings;

        /// <summary>
        /// Gets or sets the current app colour setting from memory (light or dark mode).
        /// </summary>
        public static ElementTheme Theme
        {
            get
            {
                // Never set: default theme
                if (LOCAL_SETTINGS.Values[KEY_THEME] == null)
                {
                    LOCAL_SETTINGS.Values[KEY_THEME] = (int)DEFAULT_THEME;
                    return DEFAULT_THEME;
                }
                // Previously set to default theme
                else if ((int)LOCAL_SETTINGS.Values[KEY_THEME] == (int)DEFAULT_THEME)
                    return DEFAULT_THEME;
                // Previously set to non-default theme
                else
                    return NON_DEFAULT_THEME;
            }
            set
            {
                // Error check
                if (value == ElementTheme.Default)
                    throw new System.Exception("Only set the theme to light or dark mode!");
                // Never set
                else if (LOCAL_SETTINGS.Values[KEY_THEME] == null)
                    LOCAL_SETTINGS.Values[KEY_THEME] = (int)value;
                // No change
                else if ((int)value == (int)LOCAL_SETTINGS.Values[KEY_THEME])
                    return;
                // Change
                else
                    LOCAL_SETTINGS.Values[KEY_THEME] = (int)value;
            }
        }

        public static ElementSoundPlayerState Sound
        {
            get
            {
                if (LOCAL_SETTINGS.Values[KEY_SOUND] == null)
                {
                    LOCAL_SETTINGS.Values[KEY_SOUND] = (int)OFF;
                    return OFF;
                }
                // Previously set to OFF
                else if ((int)LOCAL_SETTINGS.Values[KEY_SOUND] == (int)OFF)
                    return OFF;
                // Previously set to On
                else
                    return ON;
            }

            set
            {
                if (LOCAL_SETTINGS.Values[KEY_SOUND] == null)
                    LOCAL_SETTINGS.Values[KEY_SOUND] = (int)value;
                else if ((int)value == (int)LOCAL_SETTINGS.Values[KEY_SOUND])
                    return;
                else
                    LOCAL_SETTINGS.Values[KEY_SOUND] = (int)value;
            }
        }
    }
}
