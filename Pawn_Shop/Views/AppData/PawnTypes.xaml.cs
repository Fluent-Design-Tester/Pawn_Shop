using Pawn_Shop.Dto;
using Pawn_Shop.IServices.AppData;
using Pawn_Shop.Services.AppData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Pawn_Shop.Views.AppData
{
    /**
     * TODO: <PawnType.xaml.cs>
     *   { M: Mendatory, O: Optional, F: Finished }
     * - [M] To add displayNo in data grid
     * - [M] To validate inputs
     */

    public sealed partial class PawnTypes : Page
    {
        private readonly IPawnTypeService pawnTypeService = new PawnTypeService();

        private readonly (string New, string Update, string Delete) titles = ("အသစ်ထည့်ပါ", "ပြင်ဆင်ပါ", "ဖျက်ပါ");

        public PawnTypes()
        {
            this.InitializeComponent();

            ComboBox_Category.SelectedIndex = 0;

            _LoadDataByCategoryId("1");
        }

        private async void _LoadDataByCategoryId(string categoryId)
        {
            var list = new ObservableCollection<PawnType>();

            var result = await pawnTypeService.GetByCategoryId(list, categoryId);
            if (result != null)
            {
                DataGrid_PawnTypes.ItemsSource = result;
                TextBlock_TotalRows.Text = result.Count.ToString();
            }
        }

        private void SelectionChanged_CategoryComboBox(object sender, SelectionChangedEventArgs e)
        {
            _LoadDataByCategoryId(_GetSelectedCategoryId());
            
            if (Grid_ManagePawnTypes.Visibility == Visibility.Visible)
            {
                if (TextBlock_Title.Text == titles.New)
                    TextBox_Category.Text = _GetSelectedCategoryItem();
                else
                    Grid_ManagePawnTypes.Visibility = Visibility.Collapsed;
            }
        }

        private async void TextChanged_AutoSuggestBox(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            string keyword = sender.Text.ToLower().Trim();
            var matchedItems = new List<PawnType>();

            var list = new ObservableCollection<PawnType>();
            var types = await pawnTypeService.GetByCategoryId(list, _GetSelectedCategoryId());

            foreach (PawnType type in types)
            {
                // Searchable Fields: Name, ShortName
                if (type.name.ToLower().Contains(keyword)) matchedItems.Add(type);
                else if (type.shortName.ToLower().Contains(keyword)) matchedItems.Add(type);
            }

            var bindingList = new BindingList<PawnType>(matchedItems);
            DataGrid_PawnTypes.ItemsSource = bindingList;
            TextBlock_TotalRows.Text = bindingList.Count.ToString();
        }

        private void ButtonClick_Add(object sender, RoutedEventArgs e)
        {
            Grid_ManagePawnTypes.Visibility = Visibility.Visible;
            TextBlock_ErrorMessage.Visibility = Visibility.Collapsed;
            TextBlock_Title.Text = titles.New;
            TextBox_Category.Text = _GetSelectedCategoryItem();
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
            var selectedRow = (PawnType)DataGrid_PawnTypes.SelectedItem;

            if (selectedRow != null)
            {
                Grid_ManagePawnTypes.Visibility = Visibility.Visible;
                TextBlock_ErrorMessage.Visibility = Visibility.Collapsed;
                TextBlock_Title.Text = titles.Update;
                TextBox_Category.Text = _GetSelectedCategoryItem();
                TextBox_Name.IsEnabled = true;
                TextBox_ShortName.IsEnabled = true;

                TextBox_No.Text = selectedRow.displayNo.ToString();
                TextBlock_TypeId.Text = selectedRow.id.ToString();
                TextBox_Name.Text = selectedRow.name;
                TextBox_ShortName.Text = selectedRow.shortName;

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
            var selectedRow = (PawnType)DataGrid_PawnTypes.SelectedItem;

            if (selectedRow != null)
            {
                Grid_ManagePawnTypes.Visibility = Visibility.Visible;
                TextBlock_ErrorMessage.Visibility = Visibility.Collapsed;
                TextBlock_Title.Text = titles.Delete;
                TextBox_Category.Text = _GetSelectedCategoryItem();
                TextBox_Name.IsEnabled = false;
                TextBox_ShortName.IsEnabled = false;

                TextBox_No.Text = selectedRow.displayNo.ToString();
                TextBlock_TypeId.Text = selectedRow.id.ToString();
                TextBox_Name.Text = selectedRow.name;
                TextBox_ShortName.Text = selectedRow.shortName;

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

        private async void ButtonClick_Save(object sender, RoutedEventArgs e)
        {
            string name = TextBox_Name.Text;
            string shortName = TextBox_ShortName.Text;
            int categoryId = Convert.ToInt32(_GetSelectedCategoryId());

            var newPawnType = new PawnType
            {
                name = name,
                shortName = shortName,
                categoryId = categoryId
            };

            bool isAdded = await pawnTypeService.Save(newPawnType);

            if (isAdded)
            {
                Noti_Success.Show(2000);

                // Clear the inputs and prepare for the next input
                TextBox_Name.Text = "";
                TextBox_ShortName.Text = "";
                TextBox_Name.Focus(FocusState.Programmatic);

                _LoadDataByCategoryId(_GetSelectedCategoryId());
            }
            else
            {
                Noti_Error.Show(2000);
            }
        }

        private async void ButtonClick_Update(object sender, RoutedEventArgs e)
        {
            string updatedName = TextBox_Name.Text;
            string updatedShortName = TextBox_ShortName.Text;
            int typeId = Convert.ToInt32(TextBlock_TypeId.Text);

            var updatedPawnType = new PawnType
            {
                id = typeId,
                name = updatedName,
                shortName = updatedShortName
            };

            bool isUpdated = await pawnTypeService.Update(updatedPawnType);

            if (isUpdated)
            {
                Noti_Success.Show(2000);

                // Clear the inputs and close the form
                TextBox_Name.Text = "";
                TextBox_ShortName.Text = "";
                Grid_ManagePawnTypes.Visibility = Visibility.Collapsed;

                _LoadDataByCategoryId(_GetSelectedCategoryId());
            }
            else
            {
                Noti_Error.Show(2000);
            }
        }

        private async void ButtonClick_ConfirmDelete(object sender, RoutedEventArgs e)
        {
            ContentDialogResult contentDialogResult = await ContentDialog_Delete.ShowAsync();

            if ("Primary".Equals(contentDialogResult.ToString()))
            {
               var selectedRow = (PawnType) DataGrid_PawnTypes.SelectedItem;

                bool isDeleted = await pawnTypeService.Delete(selectedRow.id);

                if (isDeleted)
                {
                    Noti_Success.Show(2000);

                    _LoadDataByCategoryId(_GetSelectedCategoryId());

                    Grid_ManagePawnTypes.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Noti_Error.Show(2000);
                }
            }
        }

        private void SelectionChanged_DataGrid(object sender, SelectionChangedEventArgs e)
        {
            if (Grid_ManagePawnTypes.Visibility == Visibility.Visible && e.AddedItems.Count == 1)
            {
                if (TextBlock_Title.Text == titles.Update || TextBlock_Title.Text == titles.Delete)
                {
                    var selectedRow = (PawnType)e.AddedItems[0];
                    TextBox_Name.Text = selectedRow.name;
                    TextBox_ShortName.Text = selectedRow.shortName;
                    TextBlock_TypeId.Text = selectedRow.id.ToString();
                    TextBox_No.Text = selectedRow.displayNo.ToString();
                }
            }
        }

        private string _GetSelectedCategoryId()
        {
            return ((ComboBoxItem)ComboBox_Category.SelectedItem).Tag.ToString();
        }

        private string _GetSelectedCategoryItem()
        {
            return ((ComboBoxItem)ComboBox_Category.SelectedItem).Content.ToString();
        }
    }
}
