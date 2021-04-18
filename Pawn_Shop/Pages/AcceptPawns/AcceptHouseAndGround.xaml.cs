using Pawn_Shop.Utilities;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Pawn_Shop.Pages.AcceptPawns
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AcceptHouseAndGround : Page
    {
        public AcceptHouseAndGround()
        {
            this.InitializeComponent();
        }

        private void SelectionChanged_NRCRegion(object sender, SelectionChangedEventArgs e)
        {
           /* ComboBox_NRCTownship.IsEnabled = true;

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
           /* ComboBox_PawnType.IsEnabled = true;

            PawnTypeModel pawnTypeModel = new PawnTypeModel();
            var list = pawnTypeModel.selectAll(_getSelectedPawnCategory());

            ComboBox_PawnType.Items.Clear();
            list.ForEach(item =>
            {
                ComboBox_PawnType.Items.Add(new ComboBoxItem { Tag = item.type_id, Content = item.name.ToString() });
            });

            if (ComboBox_PawnCategory.SelectedIndex == 0)
            {
                TextBox_HouseNo.Visibility = Visibility.Visible;
                Grid_PawnWidth.Visibility = Visibility.Collapsed;
            }
            else
            {
                TextBox_HouseNo.Visibility = Visibility.Collapsed;
                Grid_PawnWidth.Visibility = Visibility.Visible;
            }*/
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
            return ComboBox_PawnCategory.SelectedIndex + 4; // cycle's id in db is 2 and car's is 3.
        }
    }
}
