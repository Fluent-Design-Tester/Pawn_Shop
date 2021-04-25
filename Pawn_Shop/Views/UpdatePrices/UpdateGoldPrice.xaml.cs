using Pawn_Shop.Dto;
using Pawn_Shop.Services.AppData;
using Pawn_Shop.Services.UpdatePrices;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Pawn_Shop.Views.UpdatePrices
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UpdateGoldPrice : Page
    {
        private string uri = "/api/gold_prices";

        public UpdateGoldPrice()
        {
            this.InitializeComponent();

            ComboBox_Filter.SelectedIndex = 0;

            // _LoadPawnTypeData("1");
        }

        private async void _LoadPawnTypeData(string categoryId)
        {
           /* ObservableCollection<PawnType> list = new ObservableCollection<PawnType>();

            PawnTypeService typeService = new PawnTypeService(uri);
            DataGrid_GoldPrices.ItemsSource = await typeService.GetByCategoryId(list, categoryId);*/
        }

        private void SelectionChanged_DataGrid(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void ButtonClick_Save(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            decimal ygnGoldPrice = decimal.Parse(TextBox_YangonGP.Text);
            decimal worldGoldPrice = decimal.Parse(TextBox_WorldGP.Text);
            decimal dollarPrice = decimal.Parse(TextBox_DollarPrice.Text);
            decimal differenceGoldPrice = decimal.Parse(TextBox_DollarPrice.Text);

            var newGoldPrice = new GoldPrice
            {
                ygnGoldPrice = ygnGoldPrice,
                worldGoldPrice = worldGoldPrice,
                usDollars = dollarPrice,
                differenceGoldPrice = differenceGoldPrice
            };

            var goldPriceService = new GoldPriceService(uri);
            bool isAdded = await goldPriceService.Save(newGoldPrice);

            if (isAdded)
            {
                Noti_Success.Show(2000);
            }
            else
            {
                Noti_Error.Show(2000);
            }
        }

        private void ButtonClick_Update(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }

        private void ButtonClick_ConfirmDelete(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }

        private void ButtonClick_Cancel(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }

        private void SelectionChanged_Filter(object sender, SelectionChangedEventArgs e)
        {
        }

        private async void ButtonClick_Filter(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var fromDate = Convert.ToDateTime(DatePicker_FromDate.Date.ToString()).ToString("dd-MM-yyyy");
            var toDate = Convert.ToDateTime(DatePicker_ToDate.Date.ToString()).ToString("dd-MM-yyyy");

            var list = new ObservableCollection<GoldPrice>();
            var goldPriceService = new GoldPriceService(uri);
            DataGrid_GoldPrices.ItemsSource = await goldPriceService.FilterByDateRange(list, fromDate, toDate);
        }

        private void TextBox_WorldGP_TextChanged(object sender, TextChangedEventArgs e)
        {
            _CalculateDifferencePrice();
        }

        private void TextBox_DollarPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            _CalculateDifferencePrice();
        }

        private void _CalculateDifferencePrice()
        {
            string yangonGoldPrice =TextBox_YangonGP.Text;
            string worldGoldPrice = TextBox_WorldGP.Text;
            string dollarPrice = TextBox_DollarPrice.Text;

            int ygnGP = 0, wGP = 0, dP = 0;
            if (!"".Equals(yangonGoldPrice)) ygnGP = Convert.ToInt32(yangonGoldPrice);
            if (!"".Equals(worldGoldPrice)) wGP = Convert.ToInt32(worldGoldPrice);
            if (!"".Equals(dollarPrice)) dP = Convert.ToInt32(dollarPrice);

            double differencePrice = Math.Round((wGP / (1.875 * dP)) - ygnGP);

            TextBox_DifferencePrice.Text = differencePrice.ToString();
        }

        private void Cancel_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }
    }
}
