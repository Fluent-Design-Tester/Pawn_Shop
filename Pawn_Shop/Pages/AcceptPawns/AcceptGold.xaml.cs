using Pawn_Shop.Dto;
using Pawn_Shop.Models;
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

namespace Pawn_Shop.Pages.AcceptPawns
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AcceptGold : Page
    {

        public AcceptGold()
        {
            this.InitializeComponent();

            _loadPawnTypes();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Pages.AcceptPawns.IndexPage));
        }

        private void SelectionChanged_NRCRegion(object sender, SelectionChangedEventArgs e)
        {
            ComboBox_NRCTownship.IsEnabled = true;

            NRCTownshipModel nrcTownshipModel = new NRCTownshipModel();
            var list = nrcTownshipModel.selectAll(_getSelectedNRCRegionId());

            ComboBox_NRCTownship.Items.Clear();
            list.ForEach(item =>
            {
                ComboBox_NRCTownship.Items.Add(new ComboBoxItem { Tag = item.nrc_township_id, Content = item.name.ToString() });
            });
        }

        private void SelectionChanged_NRCTownship(object sender, SelectionChangedEventArgs e)
        {
            ComboBox_NRCType.IsEnabled = true;
        }

        private void SelectionChanged_NRCType(object sender, SelectionChangedEventArgs e)
        {
            TextBox_NRCNumber.IsEnabled = true;
        }

        private void TextChanged_GoldYwae(object sender, TextChangedEventArgs e)
        {
            TextBox_CurrentValue.Visibility = Visibility.Visible;
            _calculateCurrentMarketValue();
        }

        private void TextChanged_GoldPae(object sender, TextChangedEventArgs e)
        {
            TextBox_CurrentValue.Visibility = Visibility.Visible;
            _calculateCurrentMarketValue();
        }

        private void TextChanged_GoldKyat(object sender, TextChangedEventArgs e)
        {
            TextBox_CurrentValue.Visibility = Visibility.Visible;
            _calculateCurrentMarketValue();
        }

        private int _getSelectedNRCRegionId()
        {
            return ComboBox_NRCRegion.SelectedIndex + 1;
        }

        private void Toggled_IsLatePawnMoney(object sender, RoutedEventArgs e)
        {
            if (((ToggleSwitch)sender).IsOn)
            {
                TextBox_MaximumEligibleAmount.Visibility = Visibility.Visible;
            }
            else
            {
                TextBox_MaximumEligibleAmount.Visibility = Visibility.Collapsed;
            }
        }

        private void _loadPawnTypes()
        {
            PawnTypeModel pawnType = new PawnTypeModel();
            List<PawnType> types = pawnType.selectAll(1); // loads category 1 - Gold

            types.ForEach(item =>
            {
                ComboBox_PawnType.Items.Add(new ComboBoxItem { Tag = item.category_id, Content = item.name.ToString() });
            });
        }

        private void _calculateCurrentMarketValue()
        {
            double kyat = 0, pae = 0, ywae = 0;
            double kyat1 = 0, pae1 = 0, ywae1 = 0;
            double currentMarketValuePerKyat = 1275000;

            string strKyat = TextBox_GoldKyat.Text.ToString();
            string strPae = TextBox_GoldPae.Text.ToString();
            string strYwae = TextBox_GoldYwae.Text.ToString();

            if (!"".Equals(strKyat))
            {
                kyat = Convert.ToDouble(strKyat);
                kyat1 = kyat * currentMarketValuePerKyat;
            }

            if (!"".Equals(strPae)) { 
                pae = Convert.ToDouble(strPae);
                pae1 = (currentMarketValuePerKyat / 16) * pae;
            }

            if (!"".Equals(strYwae))
            {
                ywae = Convert.ToDouble(strYwae);
                ywae1 = (currentMarketValuePerKyat / 128) * ywae;
            }

            double currentMarketValue = kyat1 + pae1 + ywae1;

            TextBox_CurrentValue.Text = currentMarketValue.ToString();
        }
    }
}
