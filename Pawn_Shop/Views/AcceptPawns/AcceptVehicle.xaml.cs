using Pawn_Shop.Utilities;
using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

/**
 * TODO - get tag of combobox item in uwp
 */

namespace Pawn_Shop.Views.AcceptPawns
{
    public sealed partial class AcceptVehicle : Page
    {
        public AcceptVehicle()
        {
            this.InitializeComponent();
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

        private void SelectionChanged_PawnCategory(object sender, SelectionChangedEventArgs e)
        {
            /*ComboBox_PawnType.IsEnabled = true;

            PawnTypeModel pawnTypeModel = new PawnTypeModel();
            var list = pawnTypeModel.selectAll(_getSelectedPawnCategory());

            ComboBox_PawnType.Items.Clear();
            list.ForEach(item =>
            {
                ComboBox_PawnType.Items.Add(new ComboBoxItem { Tag = item.type_id, Content = item.name.ToString() });
            });*/
        }

        private void Toggled_IsLatePawnMoney(object sender, RoutedEventArgs e)
        {
            if (((ToggleSwitch)sender).IsOn)
            {
                TextBox_MaximumEligibleAmount.Visibility = Visibility.Visible;
                TextBox_MaximumEligibleAmount.Text = "";
                TextBox_MaximumEligibleAmount.Focus(FocusState.Programmatic);
            }
            else
            {
                TextBox_MaximumEligibleAmount.Visibility = Visibility.Collapsed;
            }
        }

        private void TextChanged_TakenAmount(object sender, TextChangedEventArgs e)
        {
            string takenAmount = TextBox_TakenAmount.Text;

            string moneyInMM = MMMoneyConverter.ConvertToMoneyInMM(takenAmount);
            TextBox_TakenAmountMM.Visibility = Visibility.Visible;
            TextBox_TakenAmountMM.Text = moneyInMM;
        }

        private void SelectionChanged_ExpiredDurationInMonth(object sender, SelectionChangedEventArgs e)
        {
            int expiredDurationInMonth = _getSelectedExpiredDurationInMonth();

            DateTime expiredDate = DateTime.Today.AddMonths(expiredDurationInMonth);

            TextBox_ExpiredDate.Visibility = Visibility.Visible;
            TextBox_ExpiredDate.Text = expiredDate.ToShortDateString();
        }

        private int _getSelectedExpiredDurationInMonth()
        {
            return ComboBox_ExpiredDurationInMonth.SelectedIndex + 2;
        }

        private int _getSelectedNRCRegionId()
        {
            return ComboBox_NRCRegion.SelectedIndex + 1;
        }

        private int _getSelectedPawnCategory()
        {
            return ComboBox_PawnCategory.SelectedIndex + 2; // cycle's id in db is 2 and car's is 3.
        }
    }
}
