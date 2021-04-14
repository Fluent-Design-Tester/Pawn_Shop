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

namespace Pawn_Shop.Pages.AppData
{
    public sealed partial class PawnCategories : Page
    {
        private List<PawnType> PawnTypesList;
        private readonly (string New, string Update, string Delete) titles = ("ပစ္စည်းအမျိုးအစား အသစ်ထည့်ပါ", "ပစ္စည်းအမျိုးအစား ပြင်ဆင်ပါ", "ပစ္စည်းအမျိုးအစား ဖျက်ပါ");

        public PawnCategories()
        {
            this.InitializeComponent();

            ComboBox_Category.SelectedIndex = 0;

            PawnTypeModel pawnType = new PawnTypeModel();
            PawnTypesList = pawnType.selectAll(1); // loads category 1 - Gold - by default
        }

        private void SelectionChanged_CategoryComboBox(object sender, SelectionChangedEventArgs e)
        {
            TextBlock_ErrorMessage.Visibility = Visibility.Collapsed;

            PawnTypeModel pawnType = new PawnTypeModel();
            List<PawnType> items = pawnType.selectAll(_getSelectedCategoryId());

            var bindingList = new BindingList<PawnType>(items);
            DataGrid_PawnTypes.ItemsSource = bindingList;

            if (Grid_ManagePawnTypes.Visibility == Visibility.Visible)
            {
                if (TextBlock_Title.Text == titles.New)
                    TextBox_Category.Text = _getSelectedCategoryItem();
                else
                    Grid_ManagePawnTypes.Visibility = Visibility.Collapsed;
            }
        }

        private void TextChanged_AutoSuggestBox(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            string searchString = sender.Text.ToLower().Trim();
            var matchedItems = new List<PawnType>();

            PawnTypesList.ForEach(type =>
            {
                // Searchable Fields: Name
                if (type.name.ToLower().Contains(searchString)) matchedItems.Add(type);
            });

            var bindingList = new BindingList<PawnType>(matchedItems);
            DataGrid_PawnTypes.ItemsSource = bindingList;
        }

        private void ButtonClick_Add(object sender, RoutedEventArgs e)
        {
            Grid_ManagePawnTypes.Visibility = Visibility.Visible;
            TextBlock_ErrorMessage.Visibility = Visibility.Collapsed;
            TextBlock_Title.Text = titles.New;
            TextBox_Category.Text = _getSelectedCategoryItem();
            TextBox_Name.IsEnabled = true;
            TextBox_Name.Text = "";
            TextBox_ShortName.IsEnabled = true;
            TextBox_ShortName.Text = "";

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
            PawnType selectedRow = (PawnType)DataGrid_PawnTypes.SelectedItem;

            if (selectedRow != null)
            {
                Grid_ManagePawnTypes.Visibility = Visibility.Visible;
                TextBlock_ErrorMessage.Visibility = Visibility.Collapsed;
                TextBlock_Title.Text = titles.Update;
                TextBox_Category.Text = _getSelectedCategoryItem();
                TextBox_Name.IsEnabled = true;
                TextBox_ShortName.IsEnabled = true;

                TextBox_No.Text = selectedRow.display_no.ToString();
                TextBlock_TypeId.Text = selectedRow.type_id.ToString();
                TextBox_Name.Text = selectedRow.name;
                TextBox_ShortName.Text = selectedRow.short_name;

                TextBox_No.Visibility = Visibility.Visible;
                Button_Update.Visibility = Visibility.Visible;
                Button_Cancel.Visibility = Visibility.Visible;
                Button_Save.Visibility = Visibility.Collapsed;
                Button_ConfirmDelete.Visibility = Visibility.Collapsed;

                TextBox_Name.Description = "";
            }
            else
            {
                Noti_Info.Show(2000);
            }
        }

        private void ButtonClick_Delete(object sender, RoutedEventArgs e)
        {
            PawnType selectedRow = (PawnType)DataGrid_PawnTypes.SelectedItem;

            if (selectedRow != null)
            {
                Grid_ManagePawnTypes.Visibility = Visibility.Visible;
                TextBlock_ErrorMessage.Visibility = Visibility.Collapsed;
                TextBlock_Title.Text = titles.Delete;
                TextBox_Category.Text = _getSelectedCategoryItem();
                TextBox_Name.IsEnabled = false;
                TextBox_ShortName.IsEnabled = false;

                TextBlock_TypeId.Text = selectedRow.type_id.ToString();
                TextBox_No.Text = selectedRow.display_no.ToString();
                TextBox_Name.Text = selectedRow.name;
                TextBox_ShortName.Text = selectedRow.short_name;

                Button_ConfirmDelete.Visibility = Visibility.Visible;
                TextBox_No.Visibility = Visibility.Visible;
                Button_Save.Visibility = Visibility.Collapsed;
                Button_Update.Visibility = Visibility.Collapsed;

                TextBox_Name.Description = "";
            }
            else
            {
                Noti_Info.Show(2000);
            }
        }

        private void ButtonClick_Cancel(object sender, RoutedEventArgs e)
        {
            Grid_ManagePawnTypes.Visibility = Visibility.Collapsed;
        }

        private void ButtonClick_Save(object sender, RoutedEventArgs e)
        {
            string name = TextBox_Name.Text;
            string shortName = TextBox_ShortName.Text;

            if ("".Equals(name) || "".Equals(shortName))
            {
                TextBlock_ErrorMessage.Visibility = Visibility.Visible;
                TextBlock_ErrorMessage.Text = "Name နှင့် Short Name ၂ မျိုးလုံးကို လိုအပ်ပြီး ရှိပြီးသားများနှင့် မတူရပါ.";
                return;
            }

            // if no errors, ...
            int categoryId = _getSelectedCategoryId();

            PawnTypeModel pawnType = new PawnTypeModel();
            bool isAdded = pawnType.add(categoryId, name, shortName);

            if (isAdded)
            {
                var bindingList = new BindingList<PawnType>(pawnType.selectAll(categoryId));
                DataGrid_PawnTypes.ItemsSource = bindingList;

                TextBox_Name.Text = "";
                TextBox_Name.Description = "";
                TextBox_ShortName.Text = "";
                TextBox_ShortName.Description = "";
                TextBlock_ErrorMessage.Visibility = Visibility.Collapsed;

                Noti_Success.Show(2000);
            }
            else
            {
                TextBlock_ErrorMessage.Visibility = Visibility.Visible;
                TextBlock_ErrorMessage.Text = "Name နှင့် Short Name ၂ မျိုးလုံးကို လိုအပ်ပြီး ရှိပြီးသားအချက်အလက်များနှင့် မတူရပါ.";
            }
        }

        private void ButtonClick_Update(object sender, RoutedEventArgs e)
        {
            int typeId = Convert.ToInt32(TextBlock_TypeId.Text);
            string name = TextBox_Name.Text;
            string shortName = TextBox_ShortName.Text;

            if ("".Equals(name) || "".Equals(shortName))
            {
                TextBlock_ErrorMessage.Visibility = Visibility.Visible;
                TextBlock_ErrorMessage.Text = "Name နှင့် Short Name ၂ မျိုးလုံးကို လိုအပ်ပြီး ရှိပြီးသားအချက်အလက်များနှင့် မတူရပါ.";
                return;
            }

            PawnTypeModel pawnType = new PawnTypeModel();
            bool isUpdated = pawnType.update(typeId, name, shortName);

            if (isUpdated)
            {
                var bindingList = new BindingList<PawnType>(pawnType.selectAll(_getSelectedCategoryId()));
                DataGrid_PawnTypes.ItemsSource = bindingList;

                TextBox_Name.Text = "";
                TextBox_Name.Description = "";
                TextBox_ShortName.Text = "";
                TextBox_ShortName.Description = "";
                Grid_ManagePawnTypes.Visibility = Visibility.Collapsed;

                Noti_Success.Show(2000);
            }
            else
            {
                TextBlock_ErrorMessage.Visibility = Visibility.Visible;
                TextBlock_ErrorMessage.Text = "Name နှင့် Short Name ၂ မျိုးလုံးကို လိုအပ်ပြီး ရှိပြီးသားအချက်အလက်များနှင့် မတူရပါ.";
            }
        }

        private async void ButtonClick_ConfirmDelete(object sender, RoutedEventArgs e)
        {
            ContentDialogResult contentDialogResult = await ContentDialog_Delete.ShowAsync();

            if ("Primary".Equals(contentDialogResult.ToString()))
            {
                PawnType selectedRow = (PawnType)DataGrid_PawnTypes.SelectedItem;

                PawnTypeModel pawnType = new PawnTypeModel();
                bool isDeleted = pawnType.delete(selectedRow.type_id);

                if (isDeleted)
                {
                    var bindingList = new BindingList<PawnType>(pawnType.selectAll(_getSelectedCategoryId()));
                    DataGrid_PawnTypes.ItemsSource = bindingList;

                    Grid_ManagePawnTypes.Visibility = Visibility.Collapsed;

                    Noti_Success.Show(2000);
                }
            }
        }

        private void SelectionChanged_DataGrid(object sender, SelectionChangedEventArgs e)
        {
            if (Grid_ManagePawnTypes.Visibility == Visibility.Visible && e.AddedItems.Count == 1)
            {
                if (TextBlock_Title.Text == titles.Update || TextBlock_Title.Text == titles.Delete)
                {
                    PawnType selectedRow = (PawnType)e.AddedItems[0];
                    TextBox_Name.Text = selectedRow.name;
                    TextBox_ShortName.Text = selectedRow.short_name;
                    TextBlock_TypeId.Text = selectedRow.type_id.ToString();
                    TextBox_No.Text = selectedRow.display_no.ToString();
                }
            }
        }

        private int _getSelectedCategoryId()
        {
            return Convert.ToInt32(((ComboBoxItem)ComboBox_Category.SelectedItem).Tag);
        }

        private string _getSelectedCategoryItem()
        {
            return ((ComboBoxItem)ComboBox_Category.SelectedItem).Content.ToString();
        }
    }
}
