using Pawn_Shop.Dto;
using Pawn_Shop.Services.AppData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Pawn_Shop.Pages.AppData
{
    public sealed partial class PawnCategories : Page
    {
        private readonly (string New, string Update, string Delete) titles = ("အသစ်ထည့်ပါ", "ပြင်ဆင်ပါ", "ဖျက်ပါ");

        public PawnCategories()
        {
            this.InitializeComponent();

            ComboBox_Category.SelectedIndex = 0;

            _LoadPawnTypeData("1");
        }

        private async void _LoadPawnTypeData(string categoryId)
        {
            ObservableCollection<PawnType> list = new ObservableCollection<PawnType>();

            PawnTypeService typeService = new PawnTypeService();
            DataGrid_PawnTypes.ItemsSource = await typeService.GetByCategoryId(list, categoryId);
        }

        private void SelectionChanged_CategoryComboBox(object sender, SelectionChangedEventArgs e)
        {
            _LoadPawnTypeData(_GetSelectedCategoryId());
            
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

            ObservableCollection<PawnType> list = new ObservableCollection<PawnType>();
            PawnTypeService typeService = new PawnTypeService();
            ObservableCollection<PawnType> types = await typeService.GetByCategoryId(list, _GetSelectedCategoryId());

            foreach (PawnType type in types)
            {
                // Searchable Fields: Name, ShortName
                if (type.name.ToLower().Contains(keyword)) matchedItems.Add(type);
                else if (type.shortName.ToLower().Contains(keyword)) matchedItems.Add(type);
            }

            var bindingList = new BindingList<PawnType>(matchedItems);
            DataGrid_PawnTypes.ItemsSource = bindingList;
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
            PawnType selectedRow = (PawnType)DataGrid_PawnTypes.SelectedItem;

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
            PawnType selectedRow = (PawnType)DataGrid_PawnTypes.SelectedItem;

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

            // Construct the content to Post
            PawnType newPawnType = new PawnType();
            newPawnType.name = name;
            newPawnType.shortName = shortName;
            newPawnType.categoryId = Convert.ToInt32(_GetSelectedCategoryId());

            PawnTypeService typeService = new PawnTypeService();
            bool isAdded = await typeService.Save(newPawnType);

            if (isAdded)
            {
                Noti_Success.Show(2000);

                // Clear the inputs and prepare for the next input
                TextBox_Name.Text = "";
                TextBox_ShortName.Text = "";
                TextBox_Name.Focus(FocusState.Programmatic);

                _LoadPawnTypeData(_GetSelectedCategoryId());
            }
            else
            {
                Noti_Error.Show(2000);
            }
        }

        private async void ButtonClick_Update(object sender, RoutedEventArgs e)
        {
            int typeId = Convert.ToInt32(TextBlock_TypeId.Text);
            string updatedName = TextBox_Name.Text;
            string updatedShortName = TextBox_ShortName.Text;

            // Construct the data to Update
            PawnType updatedPawnType = new PawnType();
            updatedPawnType.id = Convert.ToInt32(typeId);
            updatedPawnType.name = updatedName;
            updatedPawnType.shortName = updatedShortName;

            PawnTypeService typeService = new PawnTypeService();
            bool isUpdated = await typeService.Update(updatedPawnType);

            if (isUpdated)
            {
                Noti_Success.Show(2000);

                // Clear the inputs and close the form
                TextBox_Name.Text = "";
                TextBox_ShortName.Text = "";
                Grid_ManagePawnTypes.Visibility = Visibility.Collapsed;

                _LoadPawnTypeData(_GetSelectedCategoryId());
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
               PawnType selectedRow = (PawnType) DataGrid_PawnTypes.SelectedItem;

                PawnTypeService typeService = new PawnTypeService();
                bool isDeleted = await typeService.Delete(selectedRow.id);

                if (isDeleted)
                {
                    Noti_Success.Show(2000);

                    _LoadPawnTypeData(_GetSelectedCategoryId());

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
                    PawnType selectedRow = (PawnType)e.AddedItems[0];
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
