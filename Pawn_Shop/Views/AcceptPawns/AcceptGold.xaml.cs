using Pawn_Shop.Dto;
using Pawn_Shop.Dto.AcceptGold;
using Pawn_Shop.IServices.AppData;
using Pawn_Shop.IServices.UpdatePrices;
using Pawn_Shop.Services.AppData;
using Pawn_Shop.Services.UpdatePrices;
using Pawn_Shop.Utilities;
using Pawn_Shop.Utilities.IUtilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Pawn_Shop.Views.AcceptPawns
{
    public sealed partial class AcceptGold : Page
    {
        private readonly IUpdateShopPriceService updateShopPriceService = new UpdateShopPriceService();
        private readonly INRCTownshipService nrcTownshipService = new NRCTownshipService();
        private readonly IReferalPersonService referalPersonService = new ReferalPersonService();
        private readonly IAgentService agentService = new AgentService();
        // private readonly IPawnTypeService pawnTypeService = new PawnTypeService();

        private readonly IGoldCalculator goldCalculator = new GoldCalculator();
        private readonly Dictionary<int, string> GoldQualities = new Dictionary<int, string>()
        {
            { 0, "$" },
            { 1, "A" },
            { 2, "B" },
            { 3, "C" }
        };

        private readonly IMMMoneyConverter mMMoneyConverter = new MMMoneyConverter();

        public AcceptGold()
        {
            this.InitializeComponent();

            // set latest updated date of shop price
            _LoadLatestShopPrices();

            // set today date
            TextBlock_Today.Text = DateTime.Now.ToString("dd-MM-yyyy (ddd)");

            // set estimated coming vouncher no
            TextBlock_VoucherNo.Text = "20211212_001";

            // _loadPawnTypes();
        }

        private async void _LoadLatestShopPrices()
        {
            var result = await updateShopPriceService.GetLatestShopPrices<LatestShopPrices>();

            // Real sale prices
            decimal SSalePrice = result.htdRealSalePriceResponse.rsPrice;
            decimal ASalePrice = result.htdRealSalePriceResponse.rsTypeAPrice;
            decimal BSalePrice = result.htdRealSalePriceResponse.rsTypeBPrice;
            decimal CSalePrice = result.htdRealSalePriceResponse.rsTypeCPrice;

            // Purchase Prices
            decimal SPurchasePrice = result.htdPurchasePriceResponse.purchaseTypeSPrice;
            decimal APurchasePrice = result.htdPurchasePriceResponse.purcahseTypeAPrice;
            decimal BPurchasePrice = result.htdPurchasePriceResponse.purchaseTypeBPrice;
            decimal CPurchasePrice = result.htdPurchasePriceResponse.purcahseTypeCPrice;

            // Pawn Price
            decimal SPawnPrice = result.htdPawnPriceResponse.pawnTypeSPrice;
            decimal APawnPrice = result.htdPawnPriceResponse.pawnTypeAPrice;
            decimal BPawnPrice = result.htdPawnPriceResponse.pawnTypeBPrice;
            decimal CPawnPrice = result.htdPawnPriceResponse.pawnTypeCPrice;

            TextBlock_SSalePrice.Text = String.Format("{0:N}", SSalePrice);
            TextBlock_ASalePrice.Text = String.Format("{0:N}", ASalePrice);
            TextBlock_BSalePrice.Text = String.Format("{0:N}", BSalePrice);
            TextBlock_CSalePrice.Text = String.Format("{0:N}", CSalePrice);
            TextBlock_SPurchasePrice.Text = String.Format("{0:N}", SPurchasePrice);
            TextBlock_APurchasePrice.Text = String.Format("{0:N}", APurchasePrice);
            TextBlock_BPurchasePrice.Text = String.Format("{0:N}", BPurchasePrice);
            TextBlock_CPurchasePrice.Text = String.Format("{0:N}", CPurchasePrice);
            TextBlock_SPawnPrice.Text = String.Format("{0:N}", SPawnPrice);
            TextBlock_APawnPrice.Text = String.Format("{0:N}", APawnPrice);
            TextBlock_BPawnPrice.Text = String.Format("{0:N}", BPawnPrice);
            TextBlock_CPawnPrice.Text = String.Format("{0:N}", CPawnPrice);
        }

        private void ButtonClick_RefreshLatestShopPrice(object sender, RoutedEventArgs e)
        {
            _LoadLatestShopPrices();
            
            InAppNotification.Show("Successfully Updated!", 2000);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(IndexPage));
        }

        private async void SelectionChanged_NRCRegion(object sender, SelectionChangedEventArgs e)
        {
            ComboBox_NRCTownship.IsEnabled = true;

            string selectedId = ((ComboBoxItem)e.AddedItems[0]).Tag.ToString();
            var list = await nrcTownshipService.GetByRegionId(new ObservableCollection<NRCTownship>(), selectedId);

            if (null != list)
            {
                ComboBox_NRCTownship.Items.Clear();
                foreach (var item in list)
                {
                    ComboBox_NRCTownship.Items.Add(new ComboBoxItem { Tag = item.id, Content = item.name.ToString() });
                }
            }
        }

        private void SelectionChanged_NRCTownship(object sender, SelectionChangedEventArgs e)
        {
            ComboBox_NRCType.IsEnabled = true;
        }

        private void SelectionChanged_NRCType(object sender, SelectionChangedEventArgs e)
        {
            TextBox_NRCNumber.IsEnabled = true;
        }

        private void TextChanged_GoldKyat(object sender, TextChangedEventArgs e)
        {
            _UpdateGoldPricesBasedOnGoldNetWeight();
        }

        private void TextChanged_GoldPae(object sender, TextChangedEventArgs e)
        {
            _UpdateGoldPricesBasedOnGoldNetWeight();
        }

        private void TextChanged_GoldYwae(object sender, TextChangedEventArgs e)
        {
            _UpdateGoldPricesBasedOnGoldNetWeight();
        }

        private void SelectionChanged_GoldQuality(object sender, SelectionChangedEventArgs e)
        {
            _UpdateGoldPricesBasedOnGoldNetWeight();
        }

        private void TextChanged_LendingAmount(object sender, TextChangedEventArgs e)
        {
            string takenAmount = TextBox_LendingAmount.Text;

            if (!"".Equals(takenAmount))
            {
                string moneyInMM = mMMoneyConverter.Convert(takenAmount);
                TextBox_LendingAmountInText.Visibility = Visibility.Visible;
                TextBox_LendingAmountInText.Text = moneyInMM;
            }
            else
            {
                TextBox_LendingAmountInText.Text = "";
            }
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
            /*int expiredDurationInMonth = _getSelectedExpiredDurationInMonth();

            DateTime expiredDate = DateTime.Today.AddMonths(expiredDurationInMonth);

            TextBox_ExpiredDate.Visibility = Visibility.Visible;
            TextBox_ExpiredDate.Text = expiredDate.ToShortDateString();*/
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
            // return ComboBox_NRCRegion.SelectedIndex + 1;
            return 1;
        }

        private int _getSelectedExpiredDurationInMonth()
        {
            // return ComboBox_ExpiredDurationInMonth.SelectedIndex + 2;
            return 1;
        }

        private async void ButtonClick_Continue(object sender, RoutedEventArgs e)
        {
            // ContentDialogResult contentDialogResult = await ContentDialog_AcceptGold.ShowAsync();
        }

        private void Toggled_VouncherType(object sender, RoutedEventArgs e)
        {
            if (((ToggleSwitch)sender).IsOn)
            {
                TextBox_OldVoucherNo.Visibility = Visibility.Visible;
            } 
            else
            {
                TextBox_OldVoucherNo.Visibility = Visibility.Collapsed;
            }
        }

        private async void Toggled_ReferalPerson(object sender, RoutedEventArgs e)
        {
            if (((ToggleSwitch)sender).IsOn)
            {
                ComboBox_ReferalPerson.Visibility = Visibility.Visible;

                // loads referal person
                var list = await referalPersonService.GetAllReferalPersons<ReferalPerson>(new ObservableCollection<ReferalPerson>());

                if (null != list)
                {
                    ComboBox_ReferalPerson.Items.Clear();
                    foreach (var item in list)
                    {
                        ComboBox_ReferalPerson.Items.Add(new ComboBoxItem { Tag = item.id, Content = "(" + item.shortName + ") " + item.name });
                    }
                }
            } 
            else
            {
                ComboBox_ReferalPerson.Visibility = Visibility.Collapsed;
            }
        }

        private async void Toggled_Agent(object sender, RoutedEventArgs e)
        {
            if (((ToggleSwitch)sender).IsOn)
            {
                Grid_Agent.Visibility = Visibility.Visible;

                // loads agents
                var list = await agentService.GetAllAgents<Agent>(new ObservableCollection<Agent>());

                if (null != list)
                {
                    ComboBox_Agent.Items.Clear();
                    foreach (var item in list)
                    {
                        ComboBox_Agent.Items.Add(new ComboBoxItem { Tag = item.id, Content = "(" + item.shortName + ") " + item.name });
                    }
                }
            }
            else
            {
                Grid_Agent.Visibility = Visibility.Collapsed;
            }
        }

        private void Toggled_AgentTakeFee(object sender, RoutedEventArgs e)
        {
            if (((ToggleSwitch)sender).IsOn)
            {
                Grid_AgentFeePayment.Visibility = Visibility.Visible;
            } 
            else
            {
                Grid_AgentFeePayment.Visibility = Visibility.Collapsed;
            }
        }

        private void Toggled_AgentFeePayBy(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonClick_MorePawns(object sender, RoutedEventArgs e)
        {

        }

        private void SelectionChanged_PawnType(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Toggled_KPYorGram(object sender, RoutedEventArgs e)
        {
            if (((ToggleSwitch)sender).IsOn)
            {
                TextBox_GramAmount.Visibility = Visibility.Visible;
                Button_Convert.Visibility = Visibility.Visible;
            } 
            else
            {
                TextBox_GramAmount.Visibility = Visibility.Collapsed;
                Button_Convert.Visibility = Visibility.Collapsed;
            }
        }

        private void TextChanged_GramAmount(object sender, TextChangedEventArgs e)
        {
            
        }

        private void ButtonClick_Convert(object sender, RoutedEventArgs e)
        {
            string strGramAmount = TextBox_GramAmount.Text;

            if (!"".Equals(strGramAmount))
            {
                double gram = Convert.ToDouble(TextBox_GramAmount.Text);

                var weightInKPY = goldCalculator.ConvertFromGramToKPY(gram);
                TextBox_TotalWeightInKyat.Text = weightInKPY["kyat"].ToString();
                TextBox_TotalWeightInPae.Text = weightInKPY["pae"].ToString();
                TextBox_TotalWeightInYwae.Text = weightInKPY["ywae"].ToString();
            }
        }

        private void SelectionChanged_InterestRate(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SelectionChanged_LendingMonths(object sender, SelectionChangedEventArgs e)
        {
            string strMonths = ((ComboBox)sender).SelectedValue.ToString();

            if (!"".Equals(strMonths))
            {
                int months = Convert.ToInt32(strMonths);

                DateTime expiredDate = DateTime.Today.AddMonths(months);

                TextBox_ExpiredDate.Visibility = Visibility.Visible;
                TextBox_ExpiredDate.Text = expiredDate.ToString("dd-MM-yyyy (ddd)");

                // set months to အတိုးကြိုပေးငွေ combobox
                ComboBox_NoOfMonthsForPrePayInterest.Items.Clear();
                for (int i = 1; i < months; i++)
                {
                    ComboBox_NoOfMonthsForPrePayInterest.Items.Add(new ComboBoxItem { Tag = i, Content = i });
                }
            }
        }

        private void SelectionChanged_NoOfMonthsForPrePayInterest(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Toggled_PrePayInterestExists(object sender, RoutedEventArgs e)
        {
            if (((ToggleSwitch)sender).IsOn)
            {
                ComboBox_NoOfMonthsForPrePayInterest.Visibility = Visibility.Visible;
                TextBox_PrePayInterestAmount.Visibility = Visibility.Visible;
            } 
            else
            {
                ComboBox_NoOfMonthsForPrePayInterest.Visibility = Visibility.Collapsed;
                TextBox_PrePayInterestAmount.Visibility = Visibility.Collapsed;
            }
        }

        private void _UpdateGoldPricesBasedOnGoldNetWeight()
        {
            string goldQuality = _GetGoldQuality(); // $, A, B, C

            if (!"".Equals(goldQuality))
            {
                var prices = _CalculateCurrentMarketValue(goldQuality);
                TextBlock_PawnPriceOnWeightAndQuality.Text = prices["pawn"].ToString();
                TextBlock_PurchasePriceOnWeightAndQuality.Text = prices["purchase"].ToString();
            }
        }

        private Dictionary<string, decimal> _CalculateCurrentMarketValue(string goldQuality)
        {
            var prices = new Dictionary<string, decimal>();

            decimal pawnPrice = _GetGoldPriceBasedOnGoldQuality(goldQuality);
            decimal purchasePrice = _GetGoldPriceBasedOnGoldQuality(goldQuality);

            string strKyat = TextBox_NetWeightInKyat.Text.ToString();
            string strPae = TextBox_NetWeightInPae.Text.ToString();
            string strYwae = TextBox_NetWeightInYwae.Text.ToString();

            prices["pawn"] = goldCalculator.CalculateCurrentMarketValue(pawnPrice, strKyat, strPae, strYwae);
            prices["purchase"] = goldCalculator.CalculateCurrentMarketValue(purchasePrice, strKyat, strPae, strYwae);

            return prices;
        }

        private decimal _GetGoldPriceBasedOnGoldQuality(string goldQuality)
        {
            switch (goldQuality)
            {
                case "$": return 1500000;
                case "A": return 1400000;
                case "B": return 1300000;
                case "C": return 1200000;
                default: return default;
            }
        }

        private string _GetGoldQuality()
        {
            int selectedIndex = ComboBox_GoldQuality.SelectedIndex;

            if (selectedIndex > -1)
                return GoldQualities[selectedIndex];
            
            return "";
        }
    }
}
