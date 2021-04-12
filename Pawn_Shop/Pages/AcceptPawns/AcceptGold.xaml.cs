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

        private void TextChanged_TakenAmount(object sender, TextChangedEventArgs e)
        {
            TextBox_CurrentValue.Visibility = Visibility.Visible;
            _generateMMAmount();
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

        private void _generateMMAmount()
        {
            Dictionary<char, string> numbers = new Dictionary<char, string>();
            numbers.Add('0', "သုည");
            numbers.Add('1', "တစ်");
            numbers.Add('2', "နှစ်");
            numbers.Add('3', "သုံး");
            numbers.Add('4', "လေး");
            numbers.Add('5', "ငါး");
            numbers.Add('6', "ခြောက်");
            numbers.Add('7', "ခုနစ်");
            numbers.Add('8', "ရှစ်");
            numbers.Add('9', "ကိုး");

            Dictionary<int, string> units = new Dictionary<int, string>();
            units.Add(1, "");
            units.Add(2, "ဆယ်");
            units.Add(3, "ရာ");
            units.Add(4, "ထောင်");
            units.Add(5, "သောင်း");
            units.Add(6, "သိန်း");
            units.Add(7, "သန်း");

            string takenAmount = TextBox_TakenAmount.Text; // 1270005

            var aa = takenAmount.ToCharArray();

            var result = "";
            int index = aa.Length;
            var bb = "";
            var cc = "";

            var result1 = ""; 
            var result2 = "";

            if (aa.Length > 6)
            {
                // aa = 1270005
                bb = takenAmount.Substring(2); // 70005
                cc = takenAmount.Substring(0, 2); // 12

                result2 = calculate(cc.ToCharArray(), cc.ToCharArray().Length, numbers, units);
                result1 = calculate(bb.ToCharArray(), bb.ToCharArray().Length, numbers, units);

                Debug.WriteLine(result2);
                Debug.WriteLine(result1);

                TextBox_TakenAmountMM.Visibility = Visibility.Visible;
                TextBox_TakenAmountMM.Text = result2 + "သိန်း" + result1;
            } 
            else
            {
                var result3 = calculate(aa, index, numbers, units);

                TextBox_TakenAmountMM.Visibility = Visibility.Visible;
                TextBox_TakenAmountMM.Text = result3;
            }
        }

        private string calculate(char[] aa, int index, Dictionary<char, string> numbers, Dictionary<int, string> units)
        {
            var result = "";
            foreach (char a in aa)
            {
                if (!'0'.Equals(a))
                {
                    if (index == 1)
                    {
                        result += numbers[a];
                    }
                    else
                    {
                        result += numbers[a] + units[index];
                    }
                }
                index--;
            }

            return result;
        }
    }
}
