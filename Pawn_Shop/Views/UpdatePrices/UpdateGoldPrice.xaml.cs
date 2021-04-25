﻿using Pawn_Shop.Dto;
using Pawn_Shop.IServices.UpdatePrices;
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
        private static string uri = "/api/gold_prices/createdDate/";
        private readonly IGoldPriceService goldPriceService = new GoldPriceService(uri);

        public UpdateGoldPrice()
        {
            this.InitializeComponent();

            ComboBox_Filter.SelectedIndex = 0;

            TextBlock_Today.Text = DateTime.Today.ToShortDateString() + " " + DateTime.Today.DayOfWeek;

            _LoadGoldPricesDataByDate(_GetTodayDate());
        }

        private async void _LoadGoldPricesDataByDate(string date)
        {
            ObservableCollection<GoldPrice> list = new ObservableCollection<GoldPrice>();

            var result = await goldPriceService.GetByDate(list, date);
            if (result != null)
            {
                DataGrid_GoldPrices.ItemsSource = result;
                TextBlock_TotalRows.Text = result.Count.ToString();
            }
        }

        private async void _LoadGoldPricesDataByDateRange(string fromDate, string toDate)
        {
            ObservableCollection<GoldPrice> list = new ObservableCollection<GoldPrice>();

            var result = await goldPriceService.GetByDateRange(list, fromDate, toDate);
            if (result != null)
            {
                DataGrid_GoldPrices.ItemsSource = result;
                TextBlock_TotalRows.Text = result.Count.ToString();
            }
        }

        private void SelectionChanged_DataGrid(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void ButtonClick_Save(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            int ygnGoldPrice = Convert.ToInt32(TextBox_YangonGP.Text);
            int worldGoldPrice = Convert.ToInt32(TextBox_WorldGP.Text);
            int dollarPrice = Convert.ToInt32(TextBox_DollarPrice.Text);
            int differenceGoldPrice = Convert.ToInt32(TextBox_DollarPrice.Text);

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
            string[] dates = _GetDatesFromFilterComboBox();

            if (dates.Length == 1)
            {
                _LoadGoldPricesDataByDate(dates[0]);
            }
            else if (dates.Length == 2)
            {
                _LoadGoldPricesDataByDateRange(dates[0], dates[1]);
            }
        }

        private async void ButtonClick_Filter(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var fromDate = Convert.ToDateTime(DatePicker_FromDate.Date.ToString()).ToString("dd-MM-yyyy");
            var toDate = Convert.ToDateTime(DatePicker_ToDate.Date.ToString()).ToString("dd-MM-yyyy");

            var list = new ObservableCollection<GoldPrice>();
            var goldPriceService = new GoldPriceService(uri);
            // DataGrid_GoldPrices.ItemsSource = await goldPriceService.FilterByDateRange(list, fromDate, toDate);
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

        private string[] _GetDatesFromFilterComboBox()
        {
            /*
             * CURRENT IMPLEMENTATION:
             * 1. Today => Today
             * 2. Yesterday => Yesterday
             * 3. Last 3 days => FROM (Today - 3 days) TO Yesterday
             * 4. Last 7 days => FROM (Today - 7 days) TO Yesterday
             * 5. Last Month  => FROM (Today - 30 days) TO Yesterday
             */

            int selectedItem = _GetSelectedIndexFromFilterBox();

            switch (selectedItem)
            {
                case 0:
                    return new string[] { _GetTodayDate() };
                case 1:
                    return new string[] { _GetYesterdayDate() };
                case 2:
                    return new string[] { _GetLast3DaysDate(), _GetYesterdayDate() };
                case 3:
                    return new string[] { _GetLast7DaysDate(), _GetYesterdayDate() };
                case 4:
                    return new string[] { _GetLastMonthDate(), _GetYesterdayDate() };
            }

            return null;
        }

        private int _GetSelectedIndexFromFilterBox()
        {
            return ComboBox_Filter.SelectedIndex;
        }

        private string _GetTodayDate()
        {
            return Convert.ToDateTime(DateTime.Today.ToShortDateString()).ToString("dd-MM-yyyy");
        }

        private string _GetYesterdayDate()
        {
            return Convert.ToDateTime(DateTime.Today.AddDays(-1).ToShortDateString()).ToString("dd-MM-yyyy");
        }

        private string _GetLast3DaysDate()
        {
            return Convert.ToDateTime(DateTime.Today.AddDays(-3).ToShortDateString()).ToString("dd-MM-yyyy");
        }

        private string _GetLast7DaysDate()
        {
            return Convert.ToDateTime(DateTime.Today.AddDays(-7).ToShortDateString()).ToString("dd-MM-yyyy");
        }

        private string _GetLastMonthDate()
        {
            return Convert.ToDateTime(DateTime.Today.AddMonths(-1).ToShortDateString()).ToString("dd-MM-yyyy");
        }
    }
}