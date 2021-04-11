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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NRC : Page
    {
        private List<NRCTownship> NRCTownshipsList;
        private readonly (string New, string Update, string Delete) titles = ("မြို့နယ် အသစ်ထည့်ပါ", "မြို့နယ် ပြင်ဆင်ပါ", "မြို့နယ် ဖျက်ပါ");

        public NRC()
        {
            this.InitializeComponent();

            ComboBox_NRCRegion.SelectedIndex = 0;

            NRCTownshipModel nrcTownshipModel = new NRCTownshipModel();
            NRCTownshipsList = nrcTownshipModel.selectAll(1); // loads region 1 by default
        }

        private void SelectionChanged_NRCRegion(object sender, SelectionChangedEventArgs e)
        {
            NRCTownshipModel nrcTownshipModel = new NRCTownshipModel();
            List<NRCTownship> items = nrcTownshipModel.selectAll(_getSelectedNRCRegionId());

            var bindingList = new BindingList<NRCTownship>(items);
            DataGrid_NRCTownships.ItemsSource = bindingList;

            if (Grid_ManageNRCTownships.Visibility == Visibility.Visible)
            {
                if (TextBlock_Title.Text == titles.New)
                    TextBox_NRCRegion.Text = _getSelectedNRCRegionItem();
                else
                    Grid_ManageNRCTownships.Visibility = Visibility.Collapsed;
            }
        }

        private void TextChanged_AutoSuggestBox(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            string searchString = sender.Text.ToLower().Trim();
            var matchedItems = new List<NRCTownship>();

            NRCTownshipsList.ForEach(township =>
            {
                // Searchable Fields: Name
                if (township.name.ToLower().Contains(searchString)) matchedItems.Add(township);
                else if (township.description.ToLower().Contains(searchString)) matchedItems.Add(township);
            });

            var bindingList = new BindingList<NRCTownship>(matchedItems);
            DataGrid_NRCTownships.ItemsSource = bindingList;
        }

        private void ButtonClick_Add(object sender, RoutedEventArgs e)
        {
            Grid_ManageNRCTownships.Visibility = Visibility.Visible;
            TextBlock_Title.Text = titles.New;
            TextBox_NRCRegion.Text = _getSelectedNRCRegionItem();
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
            TextBox_Name.Focus(FocusState.Programmatic);
        }

        private void ButtonClick_Edit(object sender, RoutedEventArgs e)
        {
            NRCTownship selectedRow = (NRCTownship)DataGrid_NRCTownships.SelectedItem;

            if (selectedRow != null)
            {
                Grid_ManageNRCTownships.Visibility = Visibility.Visible;
                TextBlock_Title.Text = titles.Update;
                TextBox_NRCRegion.Text = _getSelectedNRCRegionItem();
                TextBox_Name.IsEnabled = false;
                TextBox_Description.IsEnabled = true;

                TextBox_No.Text = selectedRow.display_no.ToString();
                TextBlock_NRCTownshipId.Text = selectedRow.nrc_township_id.ToString();
                TextBox_Name.Text = selectedRow.name;
                TextBox_Description.Text = selectedRow.description;

                TextBox_No.Visibility = Visibility.Visible;
                Button_Update.Visibility = Visibility.Visible;
                Button_Cancel.Visibility = Visibility.Visible;
                Button_Save.Visibility = Visibility.Collapsed;
                Button_ConfirmDelete.Visibility = Visibility.Collapsed;

                TextBox_Name.Description = "";
            }
        }

        private void ButtonClick_Delete(object sender, RoutedEventArgs e)
        {
            NRCTownship selectedRow = (NRCTownship) DataGrid_NRCTownships.SelectedItem;

            if (selectedRow != null)
            {
                Grid_ManageNRCTownships.Visibility = Visibility.Visible;
                TextBlock_Title.Text = titles.Delete;
                TextBox_NRCRegion.Text = _getSelectedNRCRegionItem();
                TextBox_Name.IsEnabled = false;
                TextBox_Description.IsEnabled = false;

                TextBlock_NRCTownshipId.Text = selectedRow.nrc_township_id.ToString();
                TextBox_No.Text = selectedRow.display_no.ToString();
                TextBox_Name.Text = selectedRow.name;
                TextBox_Description.Text = selectedRow.description;

                Button_ConfirmDelete.Visibility = Visibility.Visible;
                TextBox_No.Visibility = Visibility.Visible;
                Button_Save.Visibility = Visibility.Collapsed;
                Button_Update.Visibility = Visibility.Collapsed;

                TextBox_Name.Description = "";
            }
        }

        private void ButtonClick_Cancel(object sender, RoutedEventArgs e)
        {
            Grid_ManageNRCTownships.Visibility = Visibility.Collapsed;
        }

        private void ButtonClick_Save(object sender, RoutedEventArgs e)
        {
            string newTownship = TextBox_Name.Text;
            string description = TextBox_Description.Text;

            if (!"".Equals(newTownship))
            {
                if (!_isNRCTownshipAlreadyExist(newTownship))
                {
                    int nrcRegionId = _getSelectedNRCRegionId();

                    NRCTownshipModel nrcTownshipModel = new NRCTownshipModel();
                    bool isAdded = nrcTownshipModel.add(nrcRegionId, newTownship, description);

                    if (isAdded)
                    {
                        var bindingList = new BindingList<NRCTownship>(nrcTownshipModel.selectAll(nrcRegionId));
                        DataGrid_NRCTownships.ItemsSource = bindingList;

                        TextBox_Name.Text = "";
                        TextBox_Name.Description = "";
                        TextBox_Description.Text = "";
                    }
                }
                else
                {
                    TextBox_Name.Description = "Already exists";
                    TextBox_Name.Focus(FocusState.Programmatic);
                    TextBox_Name.SelectAll();
                }
            }
            else
            {
                TextBox_Name.Description = "Required";
            }
        }

        private void ButtonClick_Update(object sender, RoutedEventArgs e)
        {
            int townshipId = Convert.ToInt32(TextBlock_NRCTownshipId.Text);
            string updatedTownship = TextBox_Name.Text;
            string updatedDescription = TextBox_Description.Text;

            if (!"".Equals(updatedTownship))
            {
                NRCTownshipModel nrcTownshipModel = new NRCTownshipModel();
                bool isUpdated = nrcTownshipModel.update(townshipId, updatedTownship, updatedDescription);

                if (isUpdated)
                {
                    var bindingList = new BindingList<NRCTownship>(nrcTownshipModel.selectAll(_getSelectedNRCRegionId()));
                    DataGrid_NRCTownships.ItemsSource = bindingList;

                    TextBox_Name.Text = "";
                    TextBox_Name.Description = "";
                    Grid_ManageNRCTownships.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                TextBox_Name.Description = "Required";
            }
        }

        private async void ButtonClick_ConfirmDelete(object sender, RoutedEventArgs e)
        {
            ContentDialogResult contentDialogResult = await cd.ShowAsync();
            
            if ("Primary".Equals(contentDialogResult.ToString()))
            {
                NRCTownship selectedRow = (NRCTownship)DataGrid_NRCTownships.SelectedItem;

                NRCTownshipModel nrcTownshipModel = new NRCTownshipModel();
                bool isDeleted = nrcTownshipModel.delete(selectedRow.nrc_township_id);

                if (isDeleted)
                {
                    var bindingList = new BindingList<NRCTownship>(nrcTownshipModel.selectAll(_getSelectedNRCRegionId()));
                    DataGrid_NRCTownships.ItemsSource = bindingList;

                    Grid_ManageNRCTownships.Visibility = Visibility.Collapsed;
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
                    TextBlock_NRCTownshipId.Text = selectedRow.nrc_township_id.ToString();
                    TextBox_No.Text = selectedRow.display_no.ToString();
                }
            }
        }

        private bool _isNRCTownshipAlreadyExist(string text)
        {
            NRCTownshipModel nrcTownshipModel = new NRCTownshipModel();
            List<NRCTownship> items = nrcTownshipModel.selectAll(_getSelectedNRCRegionId());

            return items.FindIndex(item => item.name.ToLower().Equals(text.ToLower())) >= 0 ? true : false;
        }

        private int _getSelectedNRCRegionId()
        {
            return ComboBox_NRCRegion.SelectedIndex + 1;
        }

        private string _getSelectedNRCRegionItem()
        {
            return ComboBox_NRCRegion.SelectedItem.ToString();
        }
    }
}
