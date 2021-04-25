using Pawn_Shop.Dto;
using Pawn_Shop.Services.AppData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Pawn_Shop.Views.AppData
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ReferalPersons : Page
    {
        private string uri = "/api/referal_persons";

        private readonly (string New, string Update, string Delete) titles = ("အသစ်ထည့်ပါ", "ပြင်ဆင်ပါ", "ဖျက်ပါ");

        public ReferalPersons()
        {
            this.InitializeComponent();

            _LoadData();
        }

        private async void _LoadData()
        {
            var list = new ObservableCollection<ReferalPerson>();

            var referalPersonService = new ReferalPersonService(uri);
            DataGrid_ReferalPersons.ItemsSource = await referalPersonService.GetAllReferalPersons(list);
        }

        private void ButtonClick_Add(object sender, RoutedEventArgs e)
        {
            Grid_ManageReferalPersons.Visibility = Visibility.Visible;
            TextBlock_Title.Text = titles.New;
            TextBox_Name.IsEnabled = true;
            TextBox_Name.Text = "";
            TextBox_ShortName.IsEnabled = true;
            TextBox_ShortName.Text = "";
            TextBox_Phone.IsEnabled = true;
            TextBox_Phone.Text = "";

            Button_Save.Visibility = Visibility.Visible;
            Button_Cancel.Visibility = Visibility.Visible;
            Button_ConfirmDelete.Visibility = Visibility.Collapsed;
            Button_Update.Visibility = Visibility.Collapsed;
            TextBox_No.Visibility = Visibility.Collapsed;

            TextBox_Name.Description = "";
            TextBox_ShortName.Description = "";
            TextBox_Name.Focus(FocusState.Programmatic);
        }

        private void ButtonClick_Edit(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonClick_Delete(object sender, RoutedEventArgs e)
        {

        }

        private void SelectionChanged_DataGrid(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonClick_Save(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonClick_Update(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonClick_ConfirmDelete(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonClick_Cancel(object sender, RoutedEventArgs e)
        {
            Grid_ManageReferalPersons.Visibility = Visibility.Collapsed;
        }

        private void TextChanged_AutoSuggestBox(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {

        }
    }
}
