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

                decimal ARealSalePrice = Convert.ToDecimal(TextBox_APrice.Text);
                decimal BRealSalePrice = Convert.ToDecimal(TextBox_BPrice.Text);
                decimal CRealSalePrice = Convert.ToDecimal(TextBox_CPrice.Text);

                decimal AReducedPurchasingPrice = _CalculateReducingPrice(reducedType, reducingPurchasingValue, ARealSalePrice);
                decimal BReducedPurchasingPrice = _CalculateReducingPrice(reducedType, reducingPurchasingValue, BRealSalePrice);
                decimal CReducedPurchasingPrice = _CalculateReducingPrice(reducedType, reducingPurchasingValue, CRealSalePrice);

                TextBox_AReducedPurchasingPrice.Text = AReducedPurchasingPrice.ToString();
                TextBox_BReducedPurchasingPrice.Text = BReducedPurchasingPrice.ToString();
                TextBox_CReducedPurchasingPrice.Text = CReducedPurchasingPrice.ToString();
            }
            else
            {
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

                decimal ARealSalePrice = Convert.ToDecimal(TextBox_APrice.Text);
                decimal BRealSalePrice = Convert.ToDecimal(TextBox_BPrice.Text);
                decimal CRealSalePrice = Convert.ToDecimal(TextBox_CPrice.Text);

                decimal AReducedPawningPrice = _CalculateReducingPrice(reducedType, reducingPawningValue, ARealSalePrice);
                decimal BReducedPawningPrice = _CalculateReducingPrice(reducedType, reducingPawningValue, BRealSalePrice);
                decimal CReducedPawningPrice = _CalculateReducingPrice(reducedType, reducingPawningValue, CRealSalePrice);

                TextBox_AReducedPawningPrice.Text = AReducedPawningPrice.ToString();
                TextBox_BReducedPawningPrice.Text = BReducedPawningPrice.ToString();
                TextBox_CReducedPawningPrice.Text = CReducedPawningPrice.ToString();
            }
            else
            {
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

                decimal ARealSalePrice = Convert.ToDecimal(TextBox_APrice.Text);
                decimal BRealSalePrice = Convert.ToDecimal(TextBox_BPrice.Text);
                decimal CRealSalePrice = Convert.ToDecimal(TextBox_CPrice.Text);

                decimal AReducedDownPurchasingPrice = _CalculateReducingPrice(reducedType, reducingDownPurchasingValue, ARealSalePrice);
                decimal BReducedDownPurchasingPrice = _CalculateReducingPrice(reducedType, reducingDownPurchasingValue, BRealSalePrice);
                decimal CReducedDownPurchasingPrice = _CalculateReducingPrice(reducedType, reducingDownPurchasingValue, CRealSalePrice);

                TextBox_AReducedDownPurchasingPrice.Text = AReducedDownPurchasingPrice.ToString();
                TextBox_BReducedDownPurchasingPrice.Text = BReducedDownPurchasingPrice.ToString();
                TextBox_CReducedDownPurchasingPrice.Text = CReducedDownPurchasingPrice.ToString();
            }
            else
            {
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

                decimal ARealSalePrice = Convert.ToDecimal(TextBox_APrice.Text);
                decimal BRealSalePrice = Convert.ToDecimal(TextBox_BPrice.Text);
                decimal CRealSalePrice = Convert.ToDecimal(TextBox_CPrice.Text);

                decimal AReducedDownSalePrice = _CalculateReducingPrice(reducedType, reducingDownSaleValue, ARealSalePrice);
                decimal BReducedDownSalePrice = _CalculateReducingPrice(reducedType, reducingDownSaleValue, BRealSalePrice);
                decimal CReducedDownSalePrice = _CalculateReducingPrice(reducedType, reducingDownSaleValue, CRealSalePrice);

                TextBox_AReducedDownSalePrice.Text = AReducedDownSalePrice.ToString();
                TextBox_BReducedDownSalePrice.Text = BReducedDownSalePrice.ToString();
                TextBox_CReducedDownSalePrice.Text = CReducedDownSalePrice.ToString();
            }
            else
            {
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
            TextBox_AReducedPurchasingPrice.Text = "";
            TextBox_BReducedPurchasingPrice.Text = "";
            TextBox_CReducedPurchasingPrice.Text = "";
        }

        private void SelectedChanged_ReducedPawningType(object sender, SelectionChangedEventArgs e)
        {
            TextBox_ReducedPawningValue.Text = "";
            TextBox_AReducedPawningPrice.Text = "";
            TextBox_BReducedPawningPrice.Text = "";
            TextBox_CReducedPawningPrice.Text = "";
        }

        private void SelectedChanged_ReducedDownPurchasingType(object sender, SelectionChangedEventArgs e)
        {
            TextBox_ReducedDownPurchasingValue.Text = "";
            TextBox_AReducedDownPurchasingPrice.Text = "";
            TextBox_BReducedDownPurchasingPrice.Text = "";
            TextBox_CReducedDownPurchasingPrice.Text = "";
        }

        private void SelectedChanged_ReducedDownSaleType(object sender, SelectionChangedEventArgs e)
        {
            TextBox_ReducedDownSaleValue.Text = "";
            TextBox_AReducedDownSalePrice.Text = "";
            TextBox_BReducedDownSalePrice.Text = "";
            TextBox_CReducedDownSalePrice.Text = "";
        }

        private void _IsEnabledAllPriceFields(bool v)
        {
            var textBoxes = new List<TextBox> {
                TextBox_ReducedPurchasingValue,
                TextBox_AReducedPurchasingPrice,
                TextBox_BReducedPurchasingPrice,
                TextBox_CReducedPurchasingPrice,
                TextBox_ReducedPawningValue,
                TextBox_AReducedPawningPrice,
                TextBox_BReducedPawningPrice,
                TextBox_CReducedPawningPrice,
                TextBox_ReducedDownPurchasingValue,
                TextBox_AReducedDownPurchasingPrice,
                TextBox_BReducedDownPurchasingPrice,
                TextBox_CReducedDownPurchasingPrice,
                TextBox_ReducedDownSaleValue,
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
                TextBox_AReducedPurchasingPrice,
                TextBox_BReducedPurchasingPrice,
                TextBox_CReducedPurchasingPrice,
                TextBox_ReducedPawningValue,
                TextBox_AReducedPawningPrice,
                TextBox_BReducedPawningPrice,
                TextBox_CReducedPawningPrice,
                TextBox_ReducedDownPurchasingValue,
                TextBox_AReducedDownPurchasingPrice,
                TextBox_BReducedDownPurchasingPrice,
                TextBox_CReducedDownPurchasingPrice,
                TextBox_ReducedDownSaleValue,
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
            DialogTextBlock_ExtraCost.Text = TextBox_ExtraCost.Text;
            DialogTextBlock_SDraftPrice.Text = TextBox_SDraftPrice.Text;
            DialogTextBlock_ADraftPrice.Text = TextBox_ADraftPrice.Text;
            DialogTextBlock_BDraftPrice.Text = TextBox_BDraftPrice.Text;
            DialogTextBlock_CDraftPrice.Text = TextBox_CDraftPrice.Text;
            // Real Price
            DialogTextBlock_SPrice.Text = TextBox_SPrice.Text;
            DialogTextBlock_APrice.Text = TextBox_APrice.Text;
            DialogTextBlock_BPrice.Text = TextBox_BPrice.Text;
            DialogTextBlock_CPrice.Text = TextBox_CPrice.Text;

            // Purchase Price
            Dialog_ReducedPurchasing.Text = TextBox_ReducedPurchasingValue.Text + ("percent".Equals(_GetReducedPurchasingType()) ? "%" : "");
            Dialog_AReducedPurchasingPrice.Text = TextBox_AReducedPurchasingPrice.Text;
            Dialog_BReducedPurchasingPrice.Text = TextBox_BReducedPurchasingPrice.Text;
            Dialog_CReducedPurchasingPrice.Text = TextBox_CReducedPurchasingPrice.Text;
            // Pawning Price
            Dialog_ReducedPawning.Text = TextBox_ReducedPawningValue.Text + ("percent".Equals(_GetReducedPawningType()) ? "%" : "");
            Dialog_AReducedPawningPrice.Text = TextBox_AReducedPawningPrice.Text;
            Dialog_BReducedPawningPrice.Text = TextBox_BReducedPawningPrice.Text;
            Dialog_CReducedPawningPrice.Text = TextBox_CReducedPawningPrice.Text;
            // Down Purchasing Price
            Dialog_ReducedDownPurchasingValue.Text = TextBox_ReducedDownPurchasingValue.Text + ("percent".Equals(_GetReducedDownPurchasingType()) ? "%" : "");
            Dialog_AReducedDownPurchasingPrice.Text = TextBox_AReducedDownPurchasingPrice.Text;
            Dialog_BReducedDownPurchasingPrice.Text = TextBox_BReducedDownPurchasingPrice.Text;
            Dialog_CReducedDownPurchasingPrice.Text = TextBox_CReducedDownPurchasingPrice.Text;
            // Down Sale Price
            Dialog_ReducedDownSaleValue.Text = TextBox_ReducedDownSaleValue.Text + ("percent".Equals(_GetReducedDownSaleType()) ? "%" : "");
            Dialog_AReducedDownSalePrice.Text = TextBox_AReducedDownSalePrice.Text;
            Dialog_BReducedDownSalePrice.Text = TextBox_BReducedDownSalePrice.Text;
            Dialog_CReducedDownSalePrice.Text = TextBox_CReducedDownSalePrice.Text;

            ContentDialogResult contentDialogResult = await ContentDialog_Save.ShowAsync();

            if ("Primary".Equals(contentDialogResult.ToString()))
            {
                decimal extraCost = Convert.ToDecimal(DialogTextBlock_ExtraCost.Text);
                decimal SDraftPrice = Convert.ToDecimal(DialogTextBlock_SDraftPrice.Text);
                decimal ADraftPrice = Convert.ToDecimal(DialogTextBlock_ADraftPrice.Text);
                decimal BDraftPrice = Convert.ToDecimal(DialogTextBlock_BDraftPrice.Text);
                decimal CDraftPrice = Convert.ToDecimal(DialogTextBlock_CDraftPrice.Text);
                // Extra cost and Real Price
                decimal SPrice = Convert.ToDecimal(DialogTextBlock_SPrice.Text);
                decimal APrice = Convert.ToDecimal(DialogTextBlock_APrice.Text);
                decimal BPrice = Convert.ToDecimal(DialogTextBlock_BPrice.Text);
                decimal CPrice = Convert.ToDecimal(DialogTextBlock_CPrice.Text);
                // Purchase Price            
                var reducedPurchasing = _GetReducedTypeAndReducedValue(Dialog_ReducedPurchasing.Text);
                string reducedPurchasingType = reducedPurchasing.type;
                decimal reducedPurchasingValue = reducedPurchasing.value;
                decimal AReducedPurchasingPrice = Convert.ToDecimal(Dialog_AReducedPurchasingPrice.Text);
                decimal BReducedPurchasingPrice = Convert.ToDecimal(Dialog_BReducedPurchasingPrice.Text);
                decimal CReducedPurchasingPrice = Convert.ToDecimal(Dialog_CReducedPurchasingPrice.Text);
                // Pawning Price
                var reducedPawning = _GetReducedTypeAndReducedValue(Dialog_ReducedPawning.Text);
                string reducedPawningType = reducedPawning.type;
                decimal reducedPawningValue = reducedPawning.value;
                decimal AReducedPawningPrice = Convert.ToDecimal(Dialog_AReducedPawningPrice.Text);
                decimal BReducedPawningPrice = Convert.ToDecimal(Dialog_BReducedPawningPrice.Text);
                decimal CReducedPawningPrice = Convert.ToDecimal(Dialog_CReducedPawningPrice.Text);
                // Down Purchasing Price
                var reducedDownPurchasing = _GetReducedTypeAndReducedValue(Dialog_ReducedDownPurchasingValue.Text);
                string reducedDownPurchasingType = reducedDownPurchasing.type;
                decimal reducedDownPurchasingValue = reducedDownPurchasing.value;
                decimal AReducedDownPurchasingPrice = Convert.ToDecimal(Dialog_AReducedDownPurchasingPrice.Text);
                decimal BReducedDownPurchasingPrice = Convert.ToDecimal(Dialog_BReducedDownPurchasingPrice.Text);
                decimal CReducedDownPurchasingPrice = Convert.ToDecimal(Dialog_CReducedDownPurchasingPrice.Text);
                // Down Sale Price
                var reducedDownSale = _GetReducedTypeAndReducedValue(Dialog_ReducedDownSaleValue.Text);
                string reducedDownSaleType = reducedDownSale.type;
                decimal reducedDownSaleValue = reducedDownSale.value;
                decimal AReducedDownSalePrice = Convert.ToDecimal(Dialog_ReducedDownSaleValue.Text);
                decimal BReducedDownSalePrice = Convert.ToDecimal(Dialog_ReducedDownSaleValue.Text);
                decimal CReducedDownSalePrice = Convert.ToDecimal(Dialog_ReducedDownSaleValue.Text);

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
                        purcahseTypeAPrice = AReducedPurchasingPrice,
                        purchaseTypeBPrice = BReducedPurchasingPrice,
                        purcahseTypeCPrice = CReducedPurchasingPrice
                    },
                    htdPawnPriceRequest = new PawningPrice
                    {
                        pawnPriceType = reducedPawningType,
                        pawnValue = reducedPawningValue,
                        pawnTypeAPrice = AReducedPawningPrice,
                        pawnTypeBPrice = BReducedPawningPrice,
                        pawnTypeCPrice = CReducedPawningPrice
                    },
                    downPurchasePriceRequest = new DownPurchasingPrice
                    {
                        downPurchasePriceType = reducedDownPurchasingType,
                        downPurchaseValue = reducedDownPurchasingValue,
                        downPurcahseTypeAPrice = AReducedDownPurchasingPrice,
                        downPurchaseTypeBPrice = BReducedDownPurchasingPrice,
                        downPurcahseTypeCPrice = CReducedDownPurchasingPrice
                    },
                    downSalePriceRequest = new DownSalePrice
                    {
                        downSalePriceType = reducedDownSaleType,
                        downSaleValue = reducedDownSaleValue,
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
