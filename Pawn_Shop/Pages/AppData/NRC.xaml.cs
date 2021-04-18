using Pawn_Shop.Dtos;
using Pawn_Shop.Services.AppData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Pawn_Shop.Pages.AppData
{
    public sealed partial class NRC : Page
    {
        private readonly (string New, string Update, string Delete) titles = ("မြို့နယ် အသစ်ထည့်ပါ", "မြို့နယ် ပြင်ဆင်ပါ", "မြို့နယ် ဖျက်ပါ");

        public NRC()
        {
            this.InitializeComponent();

            ComboBox_NRCRegion.SelectedIndex = 0;

            _LoadNRCTownshipData("1");
        }

        private async void _LoadNRCTownshipData(string regionId)
        {
            ObservableCollection<NRCTownship> list = new ObservableCollection<NRCTownship>();
            
            NRCTownshipService townshipService = new NRCTownshipService();
            DataGrid_NRCTownships.ItemsSource = await townshipService.GetByRegionId(list, regionId);
        }

        private void SelectionChanged_NRCRegion(object sender, SelectionChangedEventArgs e)
        {
            _LoadNRCTownshipData(_GetSelectedNRCRegionId());

            if (Grid_ManageNRCTownships.Visibility == Visibility.Visible)
            {
                if (TextBlock_Title.Text == titles.New) 
                    TextBox_NRCRegion.Text = _GetSelectedNRCRegionItem();
                else 
                    Grid_ManageNRCTownships.Visibility = Visibility.Collapsed;
            }
        }

        private async void TextChanged_AutoSuggestBox(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            string keyword = sender.Text.ToLower().Trim();
            var matchedItems = new List<NRCTownship>();

            ObservableCollection<NRCTownship> list = new ObservableCollection<NRCTownship>();
            NRCTownshipService townshipService = new NRCTownshipService();
            ObservableCollection<NRCTownship> townships = await townshipService.GetByRegionId(list, _GetSelectedNRCRegionId());

            foreach(NRCTownship township in townships)
            {
                // Searchable Fields: Name, Description
                if (township.name.ToLower().Contains(keyword)) matchedItems.Add(township);
                else if (township.description.ToLower().Contains(keyword)) matchedItems.Add(township);
            }

            var bindingList = new BindingList<NRCTownship>(matchedItems);
            DataGrid_NRCTownships.ItemsSource = bindingList;
        }

        private void ButtonClick_Add(object sender, RoutedEventArgs e)
        {
            Grid_ManageNRCTownships.Visibility = Visibility.Visible;
            TextBlock_Title.Text = titles.New;
            TextBox_NRCRegion.Text = _GetSelectedNRCRegionItem();
            TextBox_Name.IsEnabled = true;
            TextBox_Description.IsEnabled = true;
            TextBox_Name.Text = "";
            TextBox_Description.Text = "";

            Button_Save.Visibility = Visibility.Visible;
            Button_Cancel.Visibility = Visibility.Visible;
            Button_ConfirmDelete.Visibility = Visibility.Collapsed;
            Button_Update.Visibility = Visibility.Collapsed;
            TextBox_No.Visibility = Visibility.Collapsed;

            TextBox_Name.Description = "";
            TextBox_Description.Description = "";
            TextBox_Name.Focus(FocusState.Programmatic);
        }

        private void ButtonClick_Edit(object sender, RoutedEventArgs e)
        {
            NRCTownship selectedRow = (NRCTownship)DataGrid_NRCTownships.SelectedItem;

            if (selectedRow != null)
            {
                Grid_ManageNRCTownships.Visibility = Visibility.Visible;
                TextBlock_Title.Text = titles.Update;
                TextBox_NRCRegion.Text = _GetSelectedNRCRegionItem();
                TextBox_Name.IsEnabled = true;
                TextBox_Description.IsEnabled = true;

                TextBox_No.Text = selectedRow.displayNo.ToString();
                TextBlock_NRCTownshipId.Text = selectedRow.id.ToString();
                TextBox_Name.Text = selectedRow.name;
                TextBox_Description.Text = selectedRow.description;

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
            NRCTownship selectedRow = (NRCTownship) DataGrid_NRCTownships.SelectedItem;

            if (selectedRow != null)
            {
                Grid_ManageNRCTownships.Visibility = Visibility.Visible;
                TextBlock_Title.Text = titles.Delete;
                TextBox_NRCRegion.Text = _GetSelectedNRCRegionItem();
                TextBox_Name.IsEnabled = false;
                TextBox_Description.IsEnabled = false;

                TextBlock_NRCTownshipId.Text = selectedRow.id.ToString();
                TextBox_No.Text = selectedRow.displayNo.ToString();
                TextBox_Name.Text = selectedRow.name;
                TextBox_Description.Text = selectedRow.description;

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
            Grid_ManageNRCTownships.Visibility = Visibility.Collapsed;
        }

        private async void ButtonClick_Save(object sender, RoutedEventArgs e)
        {
            string newTownship = TextBox_Name.Text;
            string description = TextBox_Description.Text;

            // Construct the content to Post
            NRCTownship newNrcTownship = new NRCTownship();
            newNrcTownship.name = newTownship;
            newNrcTownship.description = description;
            newNrcTownship.nrcRegionId = Convert.ToInt32(_GetSelectedNRCRegionId());

            NRCTownshipService townshipService = new NRCTownshipService();
            bool isAdded = await townshipService.Save(newNrcTownship);

            if(isAdded)
            {
                Noti_Success.Show(2000);

                // Clear the inputs and prepare for the next input
                TextBox_Name.Text = "";
                TextBox_Description.Text = "";
                TextBox_Name.Focus(FocusState.Programmatic);
                
                _LoadNRCTownshipData(_GetSelectedNRCRegionId());
            }
            else
            {
                Noti_Error.Show(2000);
            }
        }

        private async void ButtonClick_Update(object sender, RoutedEventArgs e)
        {
            int townshipId = Convert.ToInt32(TextBlock_NRCTownshipId.Text);
            string updatedName = TextBox_Name.Text;
            string updatedDescription = TextBox_Description.Text;

            // Construct the data to Update
            NRCTownship updatedNrcTownship = new NRCTownship();
            updatedNrcTownship.id = Convert.ToInt32(townshipId);
            updatedNrcTownship.name = updatedName;
            updatedNrcTownship.description = updatedDescription;

            NRCTownshipService townshipService = new NRCTownshipService();
            bool isUpdated = await townshipService.Update(updatedNrcTownship);

            if (isUpdated)
            {
                Noti_Success.Show(2000);

                // Clear the inputs and close the form
                TextBox_Name.Text = "";
                TextBox_Name.Description = "";
                Grid_ManageNRCTownships.Visibility = Visibility.Collapsed;

                _LoadNRCTownshipData(_GetSelectedNRCRegionId());
            }
            else
            {
                Noti_Error.Show(2000);
            }
            
        }

        private async void ButtonClick_ConfirmDelete(object sender, RoutedEventArgs e)
        {
            ContentDialogResult contentDialogResult = await cd.ShowAsync();
            
            if ("Primary".Equals(contentDialogResult.ToString()))
            {
                NRCTownship selectedRow = (NRCTownship) DataGrid_NRCTownships.SelectedItem;

                NRCTownshipService townshipService = new NRCTownshipService();
                bool isDeleted = await townshipService.Delete(selectedRow.id);

                if (isDeleted)
                {
                    Noti_Success.Show(2000);

                    _LoadNRCTownshipData(_GetSelectedNRCRegionId());

                    Grid_ManageNRCTownships.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Noti_Error.Show(2000);
                }
            }
        }

        private void SelectionChanged_DataGrid(object sender, SelectionChangedEventArgs e)
        {
            if (Grid_ManageNRCTownships.Visibility == Visibility.Visible && e.AddedItems.Count == 1)
            {
                if (TextBlock_Title.Text == titles.Update || TextBlock_Title.Text == titles.Delete)
                {
                    NRCTownship selectedRow = (NRCTownship)e.AddedItems[0];
                    TextBox_Name.Text = selectedRow.name;
                    TextBox_Description.Text = selectedRow.description;
                    TextBlock_NRCTownshipId.Text = selectedRow.id.ToString();
                    TextBox_No.Text = selectedRow.displayNo.ToString();
                }
            }
        }

        private string _GetSelectedNRCRegionId()
        {
            return ((ComboBoxItem)ComboBox_NRCRegion.SelectedItem).Tag.ToString();
        }

        private string _GetSelectedNRCRegionItem()
        {
            return ((ComboBoxItem)ComboBox_NRCRegion.SelectedItem).Content.ToString();
        }
    }
}
