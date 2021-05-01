using Pawn_Shop.Utilities;
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

namespace Pawn_Shop.Views.AcceptPawns
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
            this.Frame.Navigate(typeof(Views.AcceptPawns.IndexPage));
        }

        private void SelectionChanged_NRCRegion(object sender, SelectionChangedEventArgs e)
        {
            /*ComboBox_NRCTownship.IsEnabled = true;

            NRCTownshipModel nrcTownshipModel = new NRCTownshipModel();
            var list = nrcTownshipModel.selectAll(_getSelectedNRCRegionId());

            ComboBox_NRCTownship.Items.Clear();
            list.ForEach(item =>
            {
                ComboBox_NRCTownship.Items.Add(new ComboBoxItem { Tag = item.nrc_township_id, Content = item.name.ToString() });
            });*/
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

        private void TextChanged_TakenAmount(object sender, TextChangedEventArgs e)
        {
            string takenAmount = TextBox_TakenAmount.Text;

            string moneyInMM = MMMoneyConverter.ConvertToMoneyInMM(takenAmount);
            TextBox_TakenAmountMM.Visibility = Visibility.Visible;
            TextBox_TakenAmountMM.Text = moneyInMM;
        }

        private void Toggled_IsLatePawnMoney(object sender, RoutedEventArgs e)
        {
            if (((ToggleSwitch)sender).IsOn)
            {
                /*TextBox_MaximumEligibleAmount.Visibility = Visibility.Visible;
                TextBox_MaximumEligibleAmount.Text = "";
                TextBox_MaximumEligibleAmount.Focus(FocusState.Programmatic);*/
            }
            else
            {
                // TextBox_MaximumEligibleAmount.Visibility = Visibility.Collapsed;
            }
        }

        private void SelectionChanged_ExpiredDurationInMonth(object sender, SelectionChangedEventArgs e)
        {
            int expiredDurationInMonth = _getSelectedExpiredDurationInMonth();

            DateTime expiredDate = DateTime.Today.AddMonths(expiredDurationInMonth);

            TextBox_ExpiredDate.Visibility = Visibility.Visible;
            TextBox_ExpiredDate.Text = expiredDate.ToShortDateString();
        }

        private void _loadPawnTypes()
        {
            /*PawnTypeModel pawnType = new PawnTypeModel();
            List<PawnType> types = pawnType.selectAll(1); // loads category 1 - Gold

            types.ForEach(item =>
            {
                ComboBox_PawnType.Items.Add(new ComboBoxItem { Tag = item.category_id, Content = item.name.ToString() });
            });*/
        }

        private int _getSelectedNRCRegionId()
        {
            return ComboBox_NRCRegion.SelectedIndex + 1;
        }

        private int _getSelectedExpiredDurationInMonth()
        {
            return ComboBox_ExpiredDurationInMonth.SelectedIndex + 2;
        }

        private void _calculateCurrentMarketValue()
        {
            double currentMarketValuePerKyat = Convert.ToDouble(0);
            string strKyat = TextBox_GoldKyat.Text.ToString();
            string strPae = TextBox_GoldPae.Text.ToString();
            string strYwae = TextBox_GoldYwae.Text.ToString();

            double result = GoldCalculator.CalculateCurrentMarketValue(currentMarketValuePerKyat, strKyat, strPae, strYwae);

            TextBox_CurrentValue.Text = result.ToString();
        }

        private async void ButtonClick_Continue(object sender, RoutedEventArgs e)
        {
            ContentDialogResult contentDialogResult = await ContentDialog_AcceptGold.ShowAsync();
        }
    }
}
