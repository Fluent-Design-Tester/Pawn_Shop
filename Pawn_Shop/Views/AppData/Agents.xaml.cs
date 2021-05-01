using Pawn_Shop.Dto;
using Pawn_Shop.IServices.AppData;
using Pawn_Shop.Services.AppData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace Pawn_Shop.Views.AppData
{
    /**
     * TODO: <Agents.xaml.cs>
     *   { M: Mendatory, O: Optional, F: Finished }
     * - [M] To add displayNo in data grid
     * - [M] To validate inputs
     */

    public sealed partial class Agents : Page
    {
        private readonly IAgentService agentService = new AgentService();

        private readonly (string New, string Update, string Delete) titles = ("အသစ်ထည့်ပါ", "ပြင်ဆင်ပါ", "ဖျက်ပါ");

        public Agents()
        {
            this.InitializeComponent();

            _LoadData();
        }

        private async void _LoadData()
        {
            var list = new ObservableCollection<Agent>();

            var result = await agentService.GetAllAgents(list);
            if (result != null)
            {
                DataGrid_Agents.ItemsSource = result;
                TextBlock_TotalRows.Text = result.Count.ToString();
            }
        }

        private void ButtonClick_Add(object sender, RoutedEventArgs e)
        {
            Grid_ManageAgents.Visibility = Visibility.Visible;
            TextBlock_Title.Text = titles.New;
            TextBox_No.Visibility = Visibility.Collapsed;
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
            Agent selectedRow = (Agent)DataGrid_Agents.SelectedItem;

            if (selectedRow != null)
            {
                Grid_ManageAgents.Visibility = Visibility.Visible;
                TextBlock_Title.Text = titles.Update;

                TextBox_Name.IsEnabled = true;
                TextBox_ShortName.IsEnabled = true;
                TextBox_Phone.IsEnabled = true;

                // TextBox_No.Text = selectedRow.displayNo.ToString();
                TextBlock_AgentId.Text = selectedRow.id.ToString();
                TextBox_Name.Text = selectedRow.name;
                TextBox_ShortName.Text = selectedRow.shortName;
                TextBox_Phone.Text = selectedRow.phone;

                TextBox_No.Visibility = Visibility.Visible;
                Button_Update.Visibility = Visibility.Visible;
                Button_Cancel.Visibility = Visibility.Visible;
                Button_Save.Visibility = Visibility.Collapsed;
                Button_ConfirmDelete.Visibility = Visibility.Collapsed;
            }
            else
            {
                Noti_Info.Show(2000);
            }
        }

        private void ButtonClick_Delete(object sender, RoutedEventArgs e)
        {
            Agent selectedRow = (Agent)DataGrid_Agents.SelectedItem;

            if (selectedRow != null)
            {
                Grid_ManageAgents.Visibility = Visibility.Visible;
                TextBlock_Title.Text = titles.Delete;

                TextBox_Name.IsEnabled = false;
                TextBox_ShortName.IsEnabled = false;
                TextBox_Phone.IsEnabled = false;

                // TextBox_No.Text = selectedRow.displayNo.ToString();
                TextBlock_AgentId.Text = selectedRow.id.ToString();
                TextBox_Name.Text = selectedRow.name;
                TextBox_ShortName.Text = selectedRow.shortName;
                TextBox_Phone.Text = selectedRow.phone;

                TextBox_No.Visibility = Visibility.Visible;
                Button_ConfirmDelete.Visibility = Visibility.Visible;
                Button_Cancel.Visibility = Visibility.Visible;
                Button_Update.Visibility = Visibility.Collapsed;
                Button_Save.Visibility = Visibility.Collapsed;
            }
            else
            {
                Noti_Info.Show(2000);
            }
        }

        private void SelectionChanged_DataGrid(object sender, SelectionChangedEventArgs e)
        {
            if (Grid_ManageAgents.Visibility == Visibility.Visible && e.AddedItems.Count == 1)
            {
                if (TextBlock_Title.Text == titles.Update || TextBlock_Title.Text == titles.Delete)
                {
                    Agent selectedRow = (Agent)e.AddedItems[0];
                    TextBox_Name.Text = selectedRow.name;
                    TextBox_ShortName.Text = selectedRow.shortName;
                    TextBox_Phone.Text = selectedRow.phone;

                    TextBlock_AgentId.Text = selectedRow.id.ToString();
                    // TextBox_No.Text = selectedRow.displayNo.ToString();
                }
            }
        }

        private async void ButtonClick_Save(object sender, RoutedEventArgs e)
        {
            string name = TextBox_Name.Text;
            string shortName = TextBox_ShortName.Text;
            string phone = TextBox_Phone.Text;

            var newAgent = new Agent
            {
                name = name,
                shortName = shortName,
                phone = phone
            };

            bool isAdded = await agentService.Save(newAgent);

            if (isAdded)
            {
                Noti_Success.Show(2000);

                _LoadData();

                _ClearInputs();
            }
            else
            {
                Noti_Error.Show(2000);
            }
        }

        private async void ButtonClick_Update(object sender, RoutedEventArgs e)
        {
            int agentId = Convert.ToInt32(TextBlock_AgentId.Text);
            string updatedName = TextBox_Name.Text;
            string updatedShortName = TextBox_ShortName.Text;
            string updatedPhone = TextBox_Phone.Text;

            // Construct the data to Update
            var updatedAgent = new Agent
            {
                id = agentId,
                name = updatedName,
                shortName = updatedShortName,
                phone = updatedPhone
            };

            bool isUpdated = await agentService.Update(updatedAgent);

            if (isUpdated)
            {
                Noti_Success.Show(2000);

                // Clear the inputs and close the form
                TextBox_Name.Text = "";
                TextBox_ShortName.Text = "";
                TextBox_Phone.Text = "";
                Grid_ManageAgents.Visibility = Visibility.Collapsed;

                _LoadData();
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
                var selectedRow = (Agent)DataGrid_Agents.SelectedItem;

                bool isDeleted = await agentService.Delete(selectedRow.id);

                if (isDeleted)
                {
                    Noti_Success.Show(2000);

                    _LoadData();

                    Grid_ManageAgents.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Noti_Error.Show(2000);
                }
            }
        }

        private void ButtonClick_Cancel(object sender, RoutedEventArgs e)
        {
            Grid_ManageAgents.Visibility = Visibility.Collapsed;
        }

        private async void TextChanged_AutoSuggestBox(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            string keyword = sender.Text.ToLower().Trim();
            var matchedItems = new List<Agent>();

            var list = new ObservableCollection<Agent>();
            var agents = await agentService.GetAllAgents(list);

            foreach (Agent agent in agents)
            {
                // Searchable Fields: Name, ShortName, Phone
                if (agent.name.ToLower().Contains(keyword)) matchedItems.Add(agent);
                else if (agent.shortName.ToLower().Contains(keyword)) matchedItems.Add(agent);
                else if (agent.phone.ToLower().Contains(keyword)) matchedItems.Add(agent);
            }

            var bindingList = new BindingList<Agent>(matchedItems);
            DataGrid_Agents.ItemsSource = bindingList;
            TextBlock_TotalRows.Text = bindingList.Count.ToString();
        }

        private void _ClearInputs()
        {
            TextBox_Name.Text = "";
            TextBox_ShortName.Text = "";
            TextBox_Phone.Text = "";

            TextBox_Name.Focus(FocusState.Programmatic);
        }
    }
}
