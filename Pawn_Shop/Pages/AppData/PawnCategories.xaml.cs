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

            if (TextBlock_Category.Visibility == Visibility.Visible)
            {
                TextBlock_Category.Text = Category_ComboBox.SelectedItem.ToString();
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

            TextBlock_Category.Text = Category_ComboBox.SelectedItem.ToString();
        }

        private void ButtonClick_Clear(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonClick_Save(object sender, RoutedEventArgs e)
        {

        }
    }
}
