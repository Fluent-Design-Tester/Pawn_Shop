using Microsoft.Toolkit.Uwp.UI.Controls;
using Pawn_Shop.Dto;
using Pawn_Shop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Pawn_Shop.Pages.AppData
{
    public sealed partial class PawnCategories : Page
    {
        private List<PawnType> PawnTypesList;

        public PawnCategories()
        {
            this.InitializeComponent();

            Category_ComboBox.SelectedIndex = 0;

            PawnTypeModel pawnType = new PawnTypeModel();
            PawnTypesList = pawnType.getPawnTypes(1);
        }

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(PawnType.type_id):
                    e.Column.Header = "No.";
                    break;

                case nameof(PawnType.name):
                    e.Column.Header = "Name";
                    break;
            }
        }

        private void Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int category = Category_ComboBox.SelectedIndex + 1;

            PawnTypeModel pawnType = new PawnTypeModel();
            List<PawnType> items = pawnType.getPawnTypes(category);

            var bindingList = new BindingList<PawnType>(items);
            DataGrid_PawnTypes.ItemsSource = bindingList;

            if (Grid_Manage_Category.Visibility == Visibility.Visible)
            {
                if (TextBlock_Title.Text == "ပစ္စည်းအမျိုးအစား အသစ်ထည့်ပါ")
                {
                    TextBox_Category.Text = Category_ComboBox.SelectedItem.ToString();
                }
                else
                {
                    Grid_Manage_Category.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            string searchString = sender.Text.ToLower().Trim();
            var matchedItems = new List<PawnType>();

            // Searchable Fields: Name
            PawnTypesList.ForEach(type =>
            {
                if (type.name.ToLower().Contains(searchString)) matchedItems.Add(type);
            });

            var bindingList = new BindingList<PawnType>(matchedItems);
            DataGrid_PawnTypes.ItemsSource = bindingList;
        }

        private void ButtonClick_Add(object sender, RoutedEventArgs e)
        {
            Grid_Manage_Category.Visibility = Visibility.Visible;
            TextBlock_Title.Text = "ပစ္စည်းအမျိုးအစား အသစ်ထည့်ပါ";
            TextBox_Category.Text = Category_ComboBox.SelectedItem.ToString();
            TextBox_Name.IsEnabled = true;
            TextBox_Name.Text = "";

            Button_ConfirmDelete.Visibility = Visibility.Collapsed;
            Button_Save.Visibility = Visibility.Visible;
            Button_ClearField.Visibility = Visibility.Visible;
        }

        private void ButtonClick_Edit(object sender, RoutedEventArgs e)
        {
            PawnType selectedRow = (PawnType) DataGrid_PawnTypes.SelectedItem;

            if (selectedRow != null)
            {
                TextBox_Name.IsEnabled = true;
                Grid_Manage_Category.Visibility = Visibility.Visible;
                TextBlock_Title.Text = "ပစ္စည်းအမျိုးအစား ပြင်ဆင်ပါ";
                TextBox_Category.Text = Category_ComboBox.SelectedItem.ToString();
                TextBox_Name.Text = selectedRow.name;

                Button_ConfirmDelete.Visibility = Visibility.Collapsed;
                Button_Save.Visibility = Visibility.Visible;
                Button_ClearField.Visibility = Visibility.Visible;
            }
        }

        private void ButtonClick_Clear(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonClick_Save(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonClick_Delete(object sender, RoutedEventArgs e)
        {
            PawnType selectedRow = (PawnType)DataGrid_PawnTypes.SelectedItem;

            if (selectedRow != null)
            {
                TextBlock_Title.Text = "ပစ္စည်းအမျိုးအစား ဖျက်မည်";
                TextBox_Name.Text = selectedRow.name;
                TextBox_Name.IsEnabled = false;
                TextBox_Name.Text = selectedRow.name;

                Button_ConfirmDelete.Visibility = Visibility.Visible;
                Button_Save.Visibility = Visibility.Collapsed;
                Button_ClearField.Visibility = Visibility.Collapsed;
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Grid_Manage_Category.Visibility == Visibility.Visible && e.AddedItems.Count == 1)
            {
                if (TextBlock_Title.Text == "ပစ္စည်းအမျိုးအစား ပြင်ဆင်ပါ" || TextBlock_Title.Text == "ပစ္စည်းအမျိုးအစား ဖျက်မည်")
                {
                    PawnType selectedRow = (PawnType) e.AddedItems[0];
                    TextBox_Name.Text = selectedRow.name;
                }
            }
        }

        private void ButtonClick_ConfirmDelete(object sender, RoutedEventArgs e)
        {

        }
    }
}
