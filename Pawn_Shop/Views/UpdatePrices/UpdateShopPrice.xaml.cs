using Pawn_Shop.Dto;
using Pawn_Shop.Dto.ShopPrice;
using Pawn_Shop.Dto.ShopPrice.UpdateShopPrice;
using Pawn_Shop.IServices.UpdatePrices;
using Pawn_Shop.Services.UpdatePrices;
using Pawn_Shop.Utilities;
using Pawn_Shop.Utilities.IUtilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Pawn_Shop.Views.UpdatePrices
{
    /**
     * TODO: <UpdateShopPrice.xaml.cs>
     *   { M: Mendatory, O: Optional, F: Finished }
     * - [M] How to get text from textchangedevent e
     * - [M] 28737834.23 ဒသမရဲ့ အရှေ့ ၃ နေရာမြောက်ကိုကြည့်ပြီး ၄ နေရာမြောက်ကို round ယူအောင်လုပ်ပါ.
     */

    public sealed partial class UpdateShopPrice : Page
    {
        private readonly IShopPriceService shopPriceService = new ShopPriceService();
        private readonly IUpdateShopPriceService updatingShopPriceService = new UpdateShopPriceService();
        private readonly IShopPriceCalculator shopPriceCalculator = new ShopPriceCalculator();

        private const int PERCENT = 100;

        public UpdateShopPrice()
        {
            this.InitializeComponent();

            ComboBox_ReducedPurchasingType.SelectedIndex = 0;
            ComboBox_ReducedPawningType.SelectedIndex = 0;
            ComboBox_ReducedDownPurchasingType.SelectedIndex = 0;
            ComboBox_ReducedDownSaleType.SelectedIndex = 0;

            _SetLatestGoldPrice();
        }

        private async void _SetLatestGoldPrice()
        {
            var result = await shopPriceService.GetLatestGoldPrice<ShopPrice>();

            if (result != null)
            {
                decimal worldGoldPrice = result.worldGoldPrice;
                decimal ygnGoldPrice = result.ygnGoldPrice;
                decimal usDollars = result.usDollars;
                DateTime updatedAt = result.updatedDate;

                TextBlock_WorldGoldPrice.Text = worldGoldPrice.ToString();
                TextBlock_YgnGoldPrice.Text = ygnGoldPrice.ToString();
                TextBlock_DollarPrice.Text = usDollars.ToString();
                TextBlock_UpdatedAt.Text = updatedAt.ToString();
            }
        }

        private async void ButtonClick_Refresh(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            _SetLatestGoldPrice();

            InAppNotification.Show("Successfully Updated!", 2000);
        }

        private void TextChanged_ExtraCost(object sender, TextChangedEventArgs e)
        {
            string strExtraCost = ((TextBox)sender).Text;

            if (!"".Equals(strExtraCost))
            {
                decimal extraCost = Convert.ToDecimal(strExtraCost);
                decimal ygnGoldPrice = Convert.ToDecimal(TextBlock_YgnGoldPrice.Text);

                if (extraCost != 0)
                {
                    decimal SPrice = shopPriceCalculator.CalculateSPrice(ygnGoldPrice, extraCost);

                    // Draft Price
                    TextBox_SDraftPrice.Text = SPrice.ToString();
                    TextBox_ADraftPrice.Text = shopPriceCalculator.CalculateAPrice(SPrice).ToString();
                    TextBox_BDraftPrice.Text = shopPriceCalculator.CalculateBPrice(SPrice).ToString();
                    TextBox_CDraftPrice.Text = shopPriceCalculator.CalculateCPrice(SPrice).ToString();

                    // Real Price
                    TextBox_SPrice.Text = SPrice.ToString();
                    TextBox_APrice.Text = shopPriceCalculator.CalculateAPrice(SPrice).ToString();
                    TextBox_BPrice.Text = shopPriceCalculator.CalculateBPrice(SPrice).ToString();
                    TextBox_CPrice.Text = shopPriceCalculator.CalculateCPrice(SPrice).ToString();
                }
            }
            else
            {
                TextBox_SDraftPrice.Text = "";
                TextBox_ADraftPrice.Text = "";
                TextBox_BDraftPrice.Text = "";
                TextBox_CDraftPrice.Text = "";

                TextBox_SPrice.Text = "";
                TextBox_APrice.Text = "";
                TextBox_BPrice.Text = "";
                TextBox_CPrice.Text = "";
            }
        }

        // [TextBox] Real Sale Price - $
        private void TextChanged_SRealSalePrice(object sender, TextChangedEventArgs e)
        {
            string strSRealSalePrice = ((TextBox)sender).Text;

            var reducedPrices = _TextChanged_RealSalePrice(strSRealSalePrice);

            if (reducedPrices != null)
            {
                if (reducedPrices.ContainsKey("ReducedPurchasingPrice"))
                    TextBox_SReducedPurchasingPrice.Text = reducedPrices["ReducedPurchasingPrice"].ToString();

                if (reducedPrices.ContainsKey("ReducedPawningPrice"))
                    TextBox_SReducedPawningPrice.Text = reducedPrices["ReducedPawningPrice"].ToString();

                if (reducedPrices.ContainsKey("ReducedDownPurchasingPrice"))
                    TextBox_SReducedDownPurchasingPrice.Text = reducedPrices["ReducedDownPurchasingPrice"].ToString();

                if (reducedPrices.ContainsKey("ReducedDownSalePrice"))
                    TextBox_SReducedDownSalePrice.Text = reducedPrices["ReducedDownSalePrice"].ToString();
            }
        }

        // [TextBox] Real Sale Price - A
        private void TextChanged_ARealSalePrice(object sender, TextChangedEventArgs e)
        {
            string strARealSalePrice = ((TextBox)sender).Text;

            var reducedPrices = _TextChanged_RealSalePrice(strARealSalePrice);

            if (reducedPrices != null)
            {
                if (reducedPrices.ContainsKey("ReducedPurchasingPrice"))
                    TextBox_AReducedPurchasingPrice.Text = reducedPrices["ReducedPurchasingPrice"].ToString();

                if (reducedPrices.ContainsKey("ReducedPawningPrice"))
                    TextBox_AReducedPawningPrice.Text = reducedPrices["ReducedPawningPrice"].ToString();

                if (reducedPrices.ContainsKey("ReducedDownPurchasingPrice"))
                    TextBox_AReducedDownPurchasingPrice.Text = reducedPrices["ReducedDownPurchasingPrice"].ToString();

                if (reducedPrices.ContainsKey("ReducedDownSalePrice"))
                    TextBox_AReducedDownSalePrice.Text = reducedPrices["ReducedDownSalePrice"].ToString();
            }
        }

        // [TextBox] Real Sale Price - B
        private void TextChanged_BRealSalePrice(object sender, TextChangedEventArgs e)
        {
            string strBRealSalePrice = ((TextBox)sender).Text;

            var reducedPrices = _TextChanged_RealSalePrice(strBRealSalePrice);

            if (reducedPrices != null)
            {
                if (reducedPrices.ContainsKey("ReducedPurchasingPrice"))
                    TextBox_BReducedPurchasingPrice.Text = reducedPrices["ReducedPurchasingPrice"].ToString();

                if (reducedPrices.ContainsKey("ReducedPawningPrice"))
                    TextBox_BReducedPawningPrice.Text = reducedPrices["ReducedPawningPrice"].ToString();

                if (reducedPrices.ContainsKey("ReducedDownPurchasingPrice"))
                    TextBox_BReducedDownPurchasingPrice.Text = reducedPrices["ReducedDownPurchasingPrice"].ToString();

                if (reducedPrices.ContainsKey("ReducedDownSalePrice"))
                    TextBox_BReducedDownSalePrice.Text = reducedPrices["ReducedDownSalePrice"].ToString();
            }
        }

        // [TextBox] Real Sale Price - C
        private void TextChanged_CRealSalePrice(object sender, TextChangedEventArgs e)
        {
            string strCRealSalePrice = ((TextBox)sender).Text;

            var reducedPrices = _TextChanged_RealSalePrice(strCRealSalePrice);

            if (reducedPrices != null)
            {
                if (reducedPrices.ContainsKey("ReducedPurchasingPrice"))
                    TextBox_CReducedPurchasingPrice.Text = reducedPrices["ReducedPurchasingPrice"].ToString();

                if (reducedPrices.ContainsKey("ReducedPawningPrice"))
                    TextBox_CReducedPawningPrice.Text = reducedPrices["ReducedPawningPrice"].ToString();

                if (reducedPrices.ContainsKey("ReducedDownPurchasingPrice"))
                    TextBox_CReducedDownPurchasingPrice.Text = reducedPrices["ReducedDownPurchasingPrice"].ToString();

                if (reducedPrices.ContainsKey("ReducedDownSalePrice"))
                    TextBox_CReducedDownSalePrice.Text = reducedPrices["ReducedDownSalePrice"].ToString();
            }
        }

        // [TextBox] Reducing Price - Purchasing
        private void TextChanged_ReducingPurchasingValue(object sender, TextChangedEventArgs e)
        {
            string strReducingPurchasingValue = ((TextBox)sender).Text;

            if (!"".Equals(strReducingPurchasingValue))
            {
                string reducedType = _GetReducedPurchasingType();
                decimal reducingPurchasingValue = Convert.ToDecimal(strReducingPurchasingValue);

                decimal SRealSalePrice = Convert.ToDecimal(TextBox_SPrice.Text);
                decimal ARealSalePrice = Convert.ToDecimal(TextBox_APrice.Text);
                decimal BRealSalePrice = Convert.ToDecimal(TextBox_BPrice.Text);
                decimal CRealSalePrice = Convert.ToDecimal(TextBox_CPrice.Text);

                decimal SReducedPurchasingPrice = _CalculateReducingPrice(reducedType, reducingPurchasingValue, SRealSalePrice);
                decimal AReducedPurchasingPrice = _CalculateReducingPrice(reducedType, reducingPurchasingValue, ARealSalePrice);
                decimal BReducedPurchasingPrice = _CalculateReducingPrice(reducedType, reducingPurchasingValue, BRealSalePrice);
                decimal CReducedPurchasingPrice = _CalculateReducingPrice(reducedType, reducingPurchasingValue, CRealSalePrice);

                TextBox_SReducedPurchasingPrice.Text = SReducedPurchasingPrice.ToString();
                TextBox_AReducedPurchasingPrice.Text = AReducedPurchasingPrice.ToString();
                TextBox_BReducedPurchasingPrice.Text = BReducedPurchasingPrice.ToString();
                TextBox_CReducedPurchasingPrice.Text = CReducedPurchasingPrice.ToString();
            }
            else
            {
                TextBox_SReducedPurchasingPrice.Text = "";
                TextBox_AReducedPurchasingPrice.Text = "";
                TextBox_BReducedPurchasingPrice.Text = "";
                TextBox_CReducedPurchasingPrice.Text = "";
            }
        }

        // [TextBox] Reducing Price - Pawning
        private void TextChanged_ReducingPawningValue(object sender, TextChangedEventArgs e)
        {
            string strReducingPawningValue = ((TextBox)sender).Text;

            if (!"".Equals(strReducingPawningValue))
            {
                string reducedType = _GetReducedPawningType();
                decimal reducingPawningValue = Convert.ToDecimal(strReducingPawningValue);

                decimal SRealSalePrice = Convert.ToDecimal(TextBox_SPrice.Text);
                decimal ARealSalePrice = Convert.ToDecimal(TextBox_APrice.Text);
                decimal BRealSalePrice = Convert.ToDecimal(TextBox_BPrice.Text);
                decimal CRealSalePrice = Convert.ToDecimal(TextBox_CPrice.Text);

                decimal SReducedPawningPrice = _CalculateReducingPrice(reducedType, reducingPawningValue, SRealSalePrice);
                decimal AReducedPawningPrice = _CalculateReducingPrice(reducedType, reducingPawningValue, ARealSalePrice);
                decimal BReducedPawningPrice = _CalculateReducingPrice(reducedType, reducingPawningValue, BRealSalePrice);
                decimal CReducedPawningPrice = _CalculateReducingPrice(reducedType, reducingPawningValue, CRealSalePrice);

                TextBox_SReducedPawningPrice.Text = SReducedPawningPrice.ToString();
                TextBox_AReducedPawningPrice.Text = AReducedPawningPrice.ToString();
                TextBox_BReducedPawningPrice.Text = BReducedPawningPrice.ToString();
                TextBox_CReducedPawningPrice.Text = CReducedPawningPrice.ToString();
            }
            else
            {
                TextBox_SReducedPawningPrice.Text = "";
                TextBox_AReducedPawningPrice.Text = "";
                TextBox_BReducedPawningPrice.Text = "";
                TextBox_CReducedPawningPrice.Text = "";
            }
        }

        // [TextBox] Reducing Price - Down Purchasing
        private void TextChanged_ReducingDownPurchasingValue(object sender, TextChangedEventArgs e)
        {
            string strReducingDownPurchasingValue = ((TextBox)sender).Text;

            if (!"".Equals(strReducingDownPurchasingValue))
            {
                string reducedType = _GetReducedDownPurchasingType();
                decimal reducingDownPurchasingValue = Convert.ToDecimal(strReducingDownPurchasingValue);

                decimal SRealSalePrice = Convert.ToDecimal(TextBox_SPrice.Text);
                decimal ARealSalePrice = Convert.ToDecimal(TextBox_APrice.Text);
                decimal BRealSalePrice = Convert.ToDecimal(TextBox_BPrice.Text);
                decimal CRealSalePrice = Convert.ToDecimal(TextBox_CPrice.Text);

                decimal SReducedDownPurchasingPrice = _CalculateReducingPrice(reducedType, reducingDownPurchasingValue, SRealSalePrice);
                decimal AReducedDownPurchasingPrice = _CalculateReducingPrice(reducedType, reducingDownPurchasingValue, ARealSalePrice);
                decimal BReducedDownPurchasingPrice = _CalculateReducingPrice(reducedType, reducingDownPurchasingValue, BRealSalePrice);
                decimal CReducedDownPurchasingPrice = _CalculateReducingPrice(reducedType, reducingDownPurchasingValue, CRealSalePrice);

                TextBox_SReducedDownPurchasingPrice.Text = SReducedDownPurchasingPrice.ToString();
                TextBox_AReducedDownPurchasingPrice.Text = AReducedDownPurchasingPrice.ToString();
                TextBox_BReducedDownPurchasingPrice.Text = BReducedDownPurchasingPrice.ToString();
                TextBox_CReducedDownPurchasingPrice.Text = CReducedDownPurchasingPrice.ToString();
            }
            else
            {
                TextBox_SReducedDownPurchasingPrice.Text = "";
                TextBox_AReducedDownPurchasingPrice.Text = "";
                TextBox_BReducedDownPurchasingPrice.Text = "";
                TextBox_CReducedDownPurchasingPrice.Text = "";
            }
        }

        // [TextBox] Reducing Price - Down Sale
        private void TextChanged_ReducingDownSaleValue(object sender, TextChangedEventArgs e)
        {
            string strReducingDownSaleValue = ((TextBox)sender).Text;

            if (!"".Equals(strReducingDownSaleValue))
            {
                string reducedType = _GetReducedDownSaleType();
                decimal reducingDownSaleValue = Convert.ToDecimal(strReducingDownSaleValue);

                decimal SRealSalePrice = Convert.ToDecimal(TextBox_SPrice.Text);
                decimal ARealSalePrice = Convert.ToDecimal(TextBox_APrice.Text);
                decimal BRealSalePrice = Convert.ToDecimal(TextBox_BPrice.Text);
                decimal CRealSalePrice = Convert.ToDecimal(TextBox_CPrice.Text);

                decimal SReducedDownSalePrice = _CalculateReducingPrice(reducedType, reducingDownSaleValue, SRealSalePrice);
                decimal AReducedDownSalePrice = _CalculateReducingPrice(reducedType, reducingDownSaleValue, ARealSalePrice);
                decimal BReducedDownSalePrice = _CalculateReducingPrice(reducedType, reducingDownSaleValue, BRealSalePrice);
                decimal CReducedDownSalePrice = _CalculateReducingPrice(reducedType, reducingDownSaleValue, CRealSalePrice);

                TextBox_SReducedDownSalePrice.Text = SReducedDownSalePrice.ToString();
                TextBox_AReducedDownSalePrice.Text = AReducedDownSalePrice.ToString();
                TextBox_BReducedDownSalePrice.Text = BReducedDownSalePrice.ToString();
                TextBox_CReducedDownSalePrice.Text = CReducedDownSalePrice.ToString();
            }
            else
            {
                TextBox_SReducedDownSalePrice.Text = "";
                TextBox_AReducedDownSalePrice.Text = "";
                TextBox_BReducedDownSalePrice.Text = "";
                TextBox_CReducedDownSalePrice.Text = "";
            }
        }

        // Text Changed On Real Sale Price Textboxes
        private Dictionary<string, decimal> _TextChanged_RealSalePrice(string strRealSalePrice)
        {
            string strReducingPurchasingValue = TextBox_ReducedPurchasingValue.Text;
            string strReducingPawningValue = TextBox_ReducedPawningValue.Text;
            string strReducingDownPurchasingValue = TextBox_ReducedDownPurchasingValue.Text;
            string strReducingDownSaleValue = TextBox_ReducedDownSaleValue.Text;

            if (!"".Equals(strRealSalePrice))
            {
                _IsEnabledAllPriceFields(true);

                decimal realSalePrice = Convert.ToDecimal(strRealSalePrice);

                var prices = new Dictionary<string, decimal>();

                if ("".Equals(strReducingPurchasingValue)
                    && "".Equals(strReducingPawningValue)
                    && "".Equals(strReducingDownPurchasingValue)
                    && "".Equals(strReducingDownSaleValue)
                    ) return null;

                if (!"".Equals(strReducingPurchasingValue))
                {
                    string reducedType = _GetReducedPurchasingType();
                    decimal reducingPurchasingValue = Convert.ToDecimal(strReducingPurchasingValue);

                    decimal reducedPurchasingPrice = _CalculateReducingPrice(reducedType, reducingPurchasingValue, realSalePrice);
                    prices.Add("ReducedPurchasingPrice", reducedPurchasingPrice);
                }

                if (!"".Equals(strReducingPawningValue))
                {
                    string reducedType = _GetReducedPawningType();
                    decimal reducingPawningValue = Convert.ToDecimal(strReducingPawningValue);

                    decimal reducedPawningPrice = _CalculateReducingPrice(reducedType, reducingPawningValue, realSalePrice);
                    prices.Add("ReducedPawningPrice", reducedPawningPrice);
                }

                if (!"".Equals(strReducingDownPurchasingValue))
                {
                    string reducedType = _GetReducedDownPurchasingType();
                    decimal reducingDownPurchasingValue = Convert.ToDecimal(strReducingDownPurchasingValue);

                    decimal reducedDownPurchasingPrice = _CalculateReducingPrice(reducedType, reducingDownPurchasingValue, realSalePrice);
                    prices.Add("ReducedDownPurchasingPrice", reducedDownPurchasingPrice);
                }

                if (!"".Equals(strReducingDownSaleValue))
                {
                    string reducedType = _GetReducedDownPurchasingType();
                    decimal reducingDownSaleValue = Convert.ToDecimal(strReducingDownSaleValue);

                    decimal reducedDownSalePrice = _CalculateReducingPrice(reducedType, reducingDownSaleValue, realSalePrice);
                    prices.Add("ReducedDownSalePrice", reducedDownSalePrice);
                }

                return prices;
            }
            else
            {
                _CleanAllPriceFields();
                _IsEnabledAllPriceFields(false);

                return null;
            }
        }

        private decimal _CalculateReducingPrice(string reducedType, decimal reducingValue, decimal basePrice)
        {
            decimal result = 0;

            if ("percent".Equals(reducedType))
            {
                result = basePrice - ((basePrice / PERCENT) * reducingValue);
            }
            else if ("amount".Equals(reducedType))
            {
                result = basePrice - reducingValue;
            }

            return result;
        }

        private void SelectedChanged_ReducedPurchasingType(object sender, SelectionChangedEventArgs e)
        {
            TextBox_ReducedPurchasingValue.Text = "";
            TextBox_SReducedPurchasingPrice.Text = "";
            TextBox_AReducedPurchasingPrice.Text = "";
            TextBox_BReducedPurchasingPrice.Text = "";
            TextBox_CReducedPurchasingPrice.Text = "";
        }

        private void SelectedChanged_ReducedPawningType(object sender, SelectionChangedEventArgs e)
        {
            TextBox_ReducedPawningValue.Text = "";
            TextBox_SReducedPawningPrice.Text = "";
            TextBox_AReducedPawningPrice.Text = "";
            TextBox_BReducedPawningPrice.Text = "";
            TextBox_CReducedPawningPrice.Text = "";
        }

        private void SelectedChanged_ReducedDownPurchasingType(object sender, SelectionChangedEventArgs e)
        {
            TextBox_ReducedDownPurchasingValue.Text = "";
            TextBox_SReducedDownPurchasingPrice.Text = "";
            TextBox_AReducedDownPurchasingPrice.Text = "";
            TextBox_BReducedDownPurchasingPrice.Text = "";
            TextBox_CReducedDownPurchasingPrice.Text = "";
        }

        private void SelectedChanged_ReducedDownSaleType(object sender, SelectionChangedEventArgs e)
        {
            TextBox_ReducedDownSaleValue.Text = "";
            TextBox_SReducedDownSalePrice.Text = "";
            TextBox_AReducedDownSalePrice.Text = "";
            TextBox_BReducedDownSalePrice.Text = "";
            TextBox_CReducedDownSalePrice.Text = "";
        }

        private void _IsEnabledAllPriceFields(bool v)
        {
            var textBoxes = new List<TextBox> {
                TextBox_ReducedPurchasingValue,
                TextBox_SReducedPurchasingPrice,
                TextBox_AReducedPurchasingPrice,
                TextBox_BReducedPurchasingPrice,
                TextBox_CReducedPurchasingPrice,
                TextBox_ReducedPawningValue,
                TextBox_SReducedPawningPrice,
                TextBox_AReducedPawningPrice,
                TextBox_BReducedPawningPrice,
                TextBox_CReducedPawningPrice,
                TextBox_ReducedDownPurchasingValue,
                TextBox_SReducedDownPurchasingPrice,
                TextBox_AReducedDownPurchasingPrice,
                TextBox_BReducedDownPurchasingPrice,
                TextBox_CReducedDownPurchasingPrice,
                TextBox_ReducedDownSaleValue,
                TextBox_SReducedDownSalePrice,
                TextBox_AReducedDownSalePrice,
                TextBox_BReducedDownSalePrice,
                TextBox_CReducedDownSalePrice
            };

            textBoxes.ForEach(textBox => textBox.IsEnabled = v);
        }

        private void _CleanAllPriceFields()
        {
            var textBoxes = new List<TextBox> {
                TextBox_ReducedPurchasingValue,
                TextBox_SReducedPurchasingPrice,
                TextBox_AReducedPurchasingPrice,
                TextBox_BReducedPurchasingPrice,
                TextBox_CReducedPurchasingPrice,
                TextBox_ReducedPawningValue,
                TextBox_SReducedPawningPrice,
                TextBox_AReducedPawningPrice,
                TextBox_BReducedPawningPrice,
                TextBox_CReducedPawningPrice,
                TextBox_ReducedDownPurchasingValue,
                TextBox_SReducedDownPurchasingPrice,
                TextBox_AReducedDownPurchasingPrice,
                TextBox_BReducedDownPurchasingPrice,
                TextBox_CReducedDownPurchasingPrice,
                TextBox_ReducedDownSaleValue,
                TextBox_SReducedDownSalePrice,
                TextBox_AReducedDownSalePrice,
                TextBox_BReducedDownSalePrice,
                TextBox_CReducedDownSalePrice
            };

            textBoxes.ForEach(textBox => textBox.Text = "");
        }

        private async void ButtonClick_Continue(object sender, RoutedEventArgs e)
        {
            // TODO: Refactor is needed !
            // Extra Cost and Draft Prices
            string strExtraCost = TextBox_ExtraCost.Text;
            string strSDraftPrice = TextBox_SDraftPrice.Text;
            string strADraftPrice = TextBox_ADraftPrice.Text;
            string strBDraftPrice = TextBox_BDraftPrice.Text;
            string strCDraftPrice = TextBox_CDraftPrice.Text;
            // Real Price
            string strSPrice = TextBox_SPrice.Text;
            string strAPrice = TextBox_APrice.Text;
            string strBPrice = TextBox_BPrice.Text;
            string strCPrice = TextBox_CPrice.Text;
            // Purchase Price
            string strReducedPurchasing = TextBox_ReducedPurchasingValue.Text + ("percent".Equals(_GetReducedPurchasingType()) ? "%" : "");
            string strSReducedPurchasingPrice = TextBox_SReducedPurchasingPrice.Text;
            string strAReducedPurchasingPrice = TextBox_AReducedPurchasingPrice.Text;
            string strBReducedPurchasingPrice = TextBox_BReducedPurchasingPrice.Text;
            string strCReducedPurchasingPrice = TextBox_CReducedPurchasingPrice.Text;
            // Pawning Price
            string strReducedPawning = TextBox_ReducedPawningValue.Text + ("percent".Equals(_GetReducedPawningType()) ? "%" : "");
            string strSReducedPawningPrice = TextBox_SReducedDownPurchasingPrice.Text;
            string strAReducedPawningPrice = TextBox_AReducedDownPurchasingPrice.Text;
            string strBReducedPawningPrice = TextBox_BReducedDownPurchasingPrice.Text;
            string strCReducedPawningPrice = TextBox_CReducedDownPurchasingPrice.Text;
            // Down Purchasing Price
            string strReducedDownPurchasing = TextBox_ReducedDownPurchasingValue.Text + ("percent".Equals(_GetReducedDownPurchasingType()) ? "%" : "");
            string strSReducedDownPurchasingPrice = TextBox_SReducedDownPurchasingPrice.Text;
            string strAReducedDownPurchasingPrice = TextBox_AReducedDownPurchasingPrice.Text;
            string strBReducedDownPurchasingPrice = TextBox_BReducedDownPurchasingPrice.Text;
            string strCReducedDownPurchasingPrice = TextBox_CReducedDownPurchasingPrice.Text;
            // Down Sale Price
            string strReducedDownSale = TextBox_ReducedDownSaleValue.Text + ("percent".Equals(_GetReducedDownSaleType()) ? "%" : "");
            string strSReducedDownSalePrice = TextBox_SReducedDownSalePrice.Text;
            string strAReducedDownSalePrice = TextBox_AReducedDownSalePrice.Text;
            string strBReducedDownSalePrice = TextBox_BReducedDownSalePrice.Text;
            string strCReducedDownSalePrice = TextBox_CReducedDownSalePrice.Text;

            // Extra Cost and Draft Prices
            DialogTextBlock_ExtraCost.Text = strExtraCost;
            DialogTextBlock_SDraftPrice.Text = strSDraftPrice;
            DialogTextBlock_ADraftPrice.Text = strADraftPrice;
            DialogTextBlock_BDraftPrice.Text = strBDraftPrice;
            DialogTextBlock_CDraftPrice.Text = strCDraftPrice;
            // Real Price
            DialogTextBlock_SPrice.Text = strSPrice;
            DialogTextBlock_APrice.Text = strAPrice;
            DialogTextBlock_BPrice.Text = strBPrice;
            DialogTextBlock_CPrice.Text = strCPrice;
            // Purchase Price
            Dialog_ReducedPurchasing.Text = strReducedPurchasing;
            Dialog_SReducedPurchasingPrice.Text = strSReducedPurchasingPrice;
            Dialog_AReducedPurchasingPrice.Text = strAReducedPurchasingPrice;
            Dialog_BReducedPurchasingPrice.Text = strBReducedPurchasingPrice;
            Dialog_CReducedPurchasingPrice.Text = strCReducedPurchasingPrice;
            // Pawning Price
            Dialog_ReducedPawning.Text = strReducedPawning;
            Dialog_SReducedPawningPrice.Text = strSReducedPawningPrice;
            Dialog_AReducedPawningPrice.Text = strAReducedPawningPrice;
            Dialog_BReducedPawningPrice.Text = strBReducedPawningPrice;
            Dialog_CReducedPawningPrice.Text = strCReducedPawningPrice;
            // Down Purchasing Price
            Dialog_ReducedDownPurchasingValue.Text = strReducedDownPurchasing;
            Dialog_SReducedDownPurchasingPrice.Text = strSReducedDownPurchasingPrice;
            Dialog_AReducedDownPurchasingPrice.Text = strAReducedDownPurchasingPrice;
            Dialog_BReducedDownPurchasingPrice.Text = strBReducedDownPurchasingPrice;
            Dialog_CReducedDownPurchasingPrice.Text = strCReducedDownPurchasingPrice;
            // Down Sale Price
            Dialog_ReducedDownSaleValue.Text = strReducedDownSale;
            Dialog_SReducedDownSalePrice.Text = strSReducedDownSalePrice;
            Dialog_AReducedDownSalePrice.Text = strAReducedDownSalePrice;
            Dialog_BReducedDownSalePrice.Text = strBReducedDownSalePrice;
            Dialog_CReducedDownSalePrice.Text = strCReducedDownSalePrice;

            ContentDialogResult contentDialogResult = await ContentDialog_Save.ShowAsync();

            if ("Primary".Equals(contentDialogResult.ToString()))
            {
                decimal extraCost = Convert.ToDecimal(strExtraCost);
                decimal SDraftPrice = Convert.ToDecimal(strSDraftPrice);
                decimal ADraftPrice = Convert.ToDecimal(strADraftPrice);
                decimal BDraftPrice = Convert.ToDecimal(strBDraftPrice);
                decimal CDraftPrice = Convert.ToDecimal(strCDraftPrice);
                // Extra cost and Real Price
                decimal SPrice = Convert.ToDecimal(strSPrice);
                decimal APrice = Convert.ToDecimal(strAPrice);
                decimal BPrice = Convert.ToDecimal(strBPrice);
                decimal CPrice = Convert.ToDecimal(strCPrice);
                // Purchase Price            
                var reducedPurchasing = _GetReducedTypeAndReducedValue(strReducedPurchasing);
                string reducedPurchasingType = reducedPurchasing.type;
                decimal reducedPurchasingValue = reducedPurchasing.value;
                decimal SReducedPurchasingPrice = Convert.ToDecimal(strSReducedPurchasingPrice);
                decimal AReducedPurchasingPrice = Convert.ToDecimal(strAReducedPurchasingPrice);
                decimal BReducedPurchasingPrice = Convert.ToDecimal(strBReducedPurchasingPrice);
                decimal CReducedPurchasingPrice = Convert.ToDecimal(strCReducedPurchasingPrice);
                // Pawning Price
                var reducedPawning = _GetReducedTypeAndReducedValue(strReducedPawning);
                string reducedPawningType = reducedPawning.type;
                decimal reducedPawningValue = reducedPawning.value;
                decimal SReducedPawningPrice = Convert.ToDecimal(strSReducedPawningPrice);
                decimal AReducedPawningPrice = Convert.ToDecimal(strAReducedPawningPrice);
                decimal BReducedPawningPrice = Convert.ToDecimal(strBReducedPawningPrice);
                decimal CReducedPawningPrice = Convert.ToDecimal(strCReducedPawningPrice);
                // Down Purchasing Price
                var reducedDownPurchasing = _GetReducedTypeAndReducedValue(strReducedDownPurchasing);
                string reducedDownPurchasingType = reducedDownPurchasing.type;
                decimal reducedDownPurchasingValue = reducedDownPurchasing.value;
                decimal SReducedDownPurchasingPrice = Convert.ToDecimal(strSReducedDownPurchasingPrice);
                decimal AReducedDownPurchasingPrice = Convert.ToDecimal(strAReducedDownPurchasingPrice);
                decimal BReducedDownPurchasingPrice = Convert.ToDecimal(strBReducedDownPurchasingPrice);
                decimal CReducedDownPurchasingPrice = Convert.ToDecimal(strCReducedDownPurchasingPrice);
                // Down Sale Price
                var reducedDownSale = _GetReducedTypeAndReducedValue(strReducedDownSale);
                string reducedDownSaleType = reducedDownSale.type;
                decimal reducedDownSaleValue = reducedDownSale.value;
                decimal SReducedDownSalePrice = Convert.ToDecimal(strSReducedDownSalePrice);
                decimal AReducedDownSalePrice = Convert.ToDecimal(strAReducedDownSalePrice);
                decimal BReducedDownSalePrice = Convert.ToDecimal(strBReducedDownSalePrice);
                decimal CReducedDownSalePrice = Convert.ToDecimal(strCReducedDownSalePrice);

                var updatingShopPrice = new UpdatingShopPrice
                {
                    extraPrice = extraCost,
                    htdGoldPrice = SDraftPrice,
                    htdTypeAPrice = ADraftPrice,
                    htdTypeBPrice = BDraftPrice,
                    htdTypeCPrice = CDraftPrice,
                    htdRealSalePriceRequest = new RealSalePrice
                    {
                        rsPrice = SPrice,
                        rsTypeAPrice = APrice,
                        rsTypeBPrice = BPrice,
                        rsTypeCPrice = CPrice
                    },
                    htdPurchasePriceRequest = new PurchasingPrice
                    {
                        purchasePriceType = reducedPurchasingType,
                        purchaseValue = reducedPurchasingValue,
                        purchaseTypeSPrice = SReducedPurchasingPrice,
                        purcahseTypeAPrice = AReducedPurchasingPrice,
                        purchaseTypeBPrice = BReducedPurchasingPrice,
                        purcahseTypeCPrice = CReducedPurchasingPrice
                    },
                    htdPawnPriceRequest = new PawningPrice
                    {
                        pawnPriceType = reducedPawningType,
                        pawnValue = reducedPawningValue,
                        pawnTypeSPrice = SReducedPawningPrice,
                        pawnTypeAPrice = AReducedPawningPrice,
                        pawnTypeBPrice = BReducedPawningPrice,
                        pawnTypeCPrice = CReducedPawningPrice
                    },
                    downPurchasePriceRequest = new DownPurchasingPrice
                    {
                        downPurchasePriceType = reducedDownPurchasingType,
                        downPurchaseValue = reducedDownPurchasingValue,
                        downPurcahseTypeSPrice = SReducedDownPurchasingPrice,
                        downPurcahseTypeAPrice = AReducedDownPurchasingPrice,
                        downPurchaseTypeBPrice = BReducedDownPurchasingPrice,
                        downPurcahseTypeCPrice = CReducedDownPurchasingPrice
                    },
                    downSalePriceRequest = new DownSalePrice
                    {
                        downSalePriceType = reducedDownSaleType,
                        downSaleValue = reducedDownSaleValue,
                        downSaleTypeSPrice = SReducedDownSalePrice,
                        downSaleTypeAPrice = AReducedDownSalePrice,
                        downSaleTypeBPrice = BReducedDownSalePrice,
                        downSaleTypeCPrice = CReducedDownSalePrice
                    }
                };

                bool isSaved = await updatingShopPriceService.Save(updatingShopPrice);

                if (isSaved)
                {
                    InAppNotification.Show("Successfully Saved!", 2000);
                }
                else
                {
                    InAppNotification.Show("Error! Something Went Wrong.", 4000);
                }
            }
        }

        private async void ButtonClick_Clear(object sender, RoutedEventArgs e)
        {
            // ...
        }

        private dynamic _GetReducedTypeAndReducedValue(string text)
        {
            dynamic result = new ExpandoObject();

            if (text.Contains("%"))
            {
                result.type = "percent";
                result.value = Convert.ToDecimal(text.Trim('%'));
            }
            else
            {
                result.type = "amount";
                result.value = Convert.ToDecimal(text);
            }

            return result;
        }

        private string _GetReducedPurchasingType()
        {
            return ((ComboBoxItem)ComboBox_ReducedPurchasingType.SelectedItem).Tag.ToString();
        }

        private string _GetReducedPawningType()
        {
            return ((ComboBoxItem)ComboBox_ReducedPawningType.SelectedItem).Tag.ToString();
        }

        private string _GetReducedDownPurchasingType()
        {
            return ((ComboBoxItem)ComboBox_ReducedDownPurchasingType.SelectedItem).Tag.ToString();
        }

        private string _GetReducedDownSaleType()
        {
            return ((ComboBoxItem)ComboBox_ReducedDownSaleType.SelectedItem).Tag.ToString();
        }
    }
}
