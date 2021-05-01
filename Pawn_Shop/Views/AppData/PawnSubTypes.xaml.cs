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
    public sealed partial class PawnSubTypes : Page
    {
        private readonly IPawnTypeService pawnTypeService = new PawnTypeService();
        private readonly IPawnSubTypeService pawnSubTypeService = new PawnSubTypeService();

        private readonly (string New, string Update, string Delete) titles = ("အသစ်ထည့်ပါ", "ပြင်ဆင်ပါ", "ဖျက်ပါ");

        private ObservableCollection<PawnType> PawnTypes;

        public PawnSubTypes()
        {
            this.InitializeComponent();

            _LoadPawnTypes();

            ComboBox_Type.SelectedIndex = 0;
        }

        private async void _LoadPawnTypes()
        {
            var list = new ObservableCollection<PawnType>();

            var result = await pawnTypeService.GetByCategoryId(list, "1");
            if (result != null)
            {
                foreach (var item in result)
                {
                    ComboBox_Type.Items.Add(new ComboBoxItem { Content = item.shortName, Tag = item.id });
                }
            }
        }

        private async void _LoadDataByTypeId(string typeId)
        {
            var list = new ObservableCollection<PawnSubType>();

            var result = await pawnSubTypeService.GetByTypeId(list, typeId);
            if (result != null)
            {
                DataGrid_PawnSubTypes.ItemsSource = result;
                TextBlock_TotalRows.Text = result.Count.ToString();
            }
        }

        private void SelectionChanged_CategoryComboBox(object sender, SelectionChangedEventArgs e)
        {
            _LoadDataByTypeId(_GetSelectedTypeId());

            if (Grid_ManagePawnSubTypes.Visibility == Visibility.Visible)
            {
                if (TextBlock_Title.Text == titles.New)
                    TextBox_Type.Text = _GetSelectedTypeItem();
                else
                    Grid_ManagePawnSubTypes.Visibility = Visibility.Collapsed;
            }
        }

        private async void TextChanged_AutoSuggestBox(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            string keyword = sender.Text.ToLower().Trim();
            var matchedItems = new List<PawnSubType>();

            var list = new ObservableCollection<PawnSubType>();
            var subTypes = await pawnSubTypeService.GetByTypeId(list, _GetSelectedTypeId());

            foreach (PawnSubType subType in subTypes)
            {
                // Searchable Fields: Name, ShortName
                if (subType.name.ToLower().Contains(keyword)) matchedItems.Add(subType);
            }

            var bindingList = new BindingList<PawnSubType>(matchedItems);
            DataGrid_PawnSubTypes.ItemsSource = bindingList;
            TextBlock_TotalRows.Text = bindingList.Count.ToString();
        }

        private void ButtonClick_Add(object sender, RoutedEventArgs e)
        {
            Grid_ManagePawnSubTypes.Visibility = Visibility.Visible;
            TextBlock_ErrorMessage.Visibility = Visibility.Collapsed;
            TextBlock_Title.Text = titles.New;
            TextBox_Type.Text = _GetSelectedTypeItem();
            TextBox_Name.IsEnabled = true;
            TextBox_Name.Text = "";

            Button_Save.Visibility = Visibility.Visible;
            Button_Cancel.Visibility = Visibility.Visible;
            Button_ConfirmDelete.Visibility = Visibility.Collapsed;
            Button_Update.Visibility = Visibility.Collapsed;
            TextBox_No.Visibility = Visibility.Collapsed;

            TextBox_Name.Description = "";
            TextBox_Name.Focus(FocusState.Programmatic);
        }

        private void ButtonClick_Edit(object sender, RoutedEventArgs e)
        {
            var selectedRow = (PawnSubType)DataGrid_PawnSubTypes.SelectedItem;

            if (selectedRow != null)
            {
                Grid_ManagePawnSubTypes.Visibility = Visibility.Visible;
                TextBlock_ErrorMessage.Visibility = Visibility.Collapsed;
                TextBlock_Title.Text = titles.Update;
                TextBox_Type.Text = _GetSelectedTypeItem();
                TextBox_Name.IsEnabled = true;

                TextBox_No.Text = selectedRow.displayNo.ToString();
                TextBlock_SubTypeId.Text = selectedRow.id.ToString();
                TextBox_Name.Text = selectedRow.name;

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
            var selectedRow = (PawnSubType)DataGrid_PawnSubTypes.SelectedItem;

            if (selectedRow != null)
            {
                Grid_ManagePawnSubTypes.Visibility = Visibility.Visible;
                TextBlock_ErrorMessage.Visibility = Visibility.Collapsed;
                TextBlock_Title.Text = titles.Delete;
                TextBox_Type.Text = _GetSelectedTypeItem();
                TextBox_Name.IsEnabled = false;

                TextBox_No.Text = selectedRow.displayNo.ToString();
                TextBlock_SubTypeId.Text = selectedRow.id.ToString();
                TextBox_Name.Text = selectedRow.name;

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

        private void SelectionChanged_DataGrid(object sender, SelectionChangedEventArgs e)
        {
            if (Grid_ManagePawnSubTypes.Visibility == Visibility.Visible && e.AddedItems.Count == 1)
            {
                if (TextBlock_Title.Text == titles.Update || TextBlock_Title.Text == titles.Delete)
                {
                    var selectedRow = (PawnSubType)e.AddedItems[0];
                    TextBox_Name.Text = selectedRow.name;
                    TextBlock_SubTypeId.Text = selectedRow.id.ToString();
                    TextBox_No.Text = selectedRow.displayNo.ToString();
                }
            }
        }

        private async void ButtonClick_Save(object sender, RoutedEventArgs e)
        {
            string name = TextBox_Name.Text;
            int typeId = Convert.ToInt32(_GetSelectedTypeId());

            bool isAdded = await pawnSubTypeService.Save(new PawnSubType { name = name, typeId = typeId });

            if (isAdded)
            {
                Noti_Success.Show(2000);

                // Clear the inputs and prepare for the next input
                TextBox_Name.Text = "";
                TextBox_Name.Focus(FocusState.Programmatic);

                _LoadDataByTypeId(_GetSelectedTypeId());
            }
            else
            {
                Noti_Error.Show(2000);
            }
        }

        private async void ButtonClick_Update(object sender, RoutedEventArgs e)
        {
            string updatedName = TextBox_Name.Text;
            int subTypeId = Convert.ToInt32(TextBlock_SubTypeId.Text);

            bool isUpdated = await pawnSubTypeService.Update(new PawnSubType { id = subTypeId, name = updatedName });

            if (isUpdated)
            {
                Noti_Success.Show(2000);

                // Clear the inputs and close the form
                TextBox_Name.Text = "";
                Grid_ManagePawnSubTypes.Visibility = Visibility.Collapsed;

                _LoadDataByTypeId(_GetSelectedTypeId());
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
                var selectedRow = (PawnSubType)DataGrid_PawnSubTypes.SelectedItem;

                bool isDeleted = await pawnSubTypeService.Delete(selectedRow.id);

                if (isDeleted)
                {
                    Noti_Success.Show(2000);

                    _LoadDataByTypeId(_GetSelectedTypeId());

                    Grid_ManagePawnSubTypes.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Noti_Error.Show(2000);
                }
            }
        }

        private void ButtonClick_Cancel(object sender, RoutedEventArgs e)
        {
            Grid_ManagePawnSubTypes.Visibility = Visibility.Collapsed;
        }

        private string _GetSelectedTypeItem()
        {
            return ((ComboBoxItem)ComboBox_Type.SelectedItem).Content.ToString();
        }

        private string _GetSelectedTypeId()
        {
            return ((ComboBoxItem)ComboBox_Type.SelectedItem).Tag.ToString();
        }
    }
}
