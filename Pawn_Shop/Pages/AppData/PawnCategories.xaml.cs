﻿using Microsoft.Toolkit.Uwp.UI.Controls;
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

        private readonly string[] titles = { "ပစ္စည်းအမျိုးအစား အသစ်ထည့်ပါ", "ပစ္စည်းအမျိုးအစား ပြင်ဆင်ပါ", "ပစ္စည်းအမျိုးအစား ဖျက်ပါ" };

        public PawnCategories()
        {
            this.InitializeComponent();

            ComboBox_Category.SelectedIndex = 0;

            PawnTypeModel pawnType = new PawnTypeModel();
            PawnTypesList = pawnType.selectAll(1);
        }

        private void SelectionChanged_CategoryComboBox(object sender, SelectionChangedEventArgs e)
        {
            int category = ComboBox_Category.SelectedIndex + 1;

            PawnTypeModel pawnType = new PawnTypeModel();
            List<PawnType> items = pawnType.selectAll(category);

            var bindingList = new BindingList<PawnType>(items);
            DataGrid_PawnTypes.ItemsSource = bindingList;

            if (Grid_ManagePawnTypes.Visibility == Visibility.Visible)
            {
                if (TextBlock_Title.Text == titles[0])
                {
                    TextBox_Category.Text = ComboBox_Category.SelectedItem.ToString();
                }
                else
                {
                    Grid_ManagePawnTypes.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void TextChanged_AutoSuggestBox(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
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
            Grid_ManagePawnTypes.Visibility = Visibility.Visible;
            TextBlock_Title.Text = titles[0];
            TextBox_Category.Text = ComboBox_Category.SelectedItem.ToString();
            TextBox_Name.IsEnabled = true;
            TextBox_Name.Text = "";

            Button_ConfirmDelete.Visibility = Visibility.Collapsed;
            Button_Save.Visibility = Visibility.Visible;
            Button_Cancel.Visibility = Visibility.Visible;
            Button_Update.Visibility = Visibility.Collapsed;
            TextBox_No.Visibility = Visibility.Collapsed;

            TextBox_Name.Description = "";

            TextBox_Name.Focus(FocusState.Programmatic);
        }

        private void ButtonClick_Edit(object sender, RoutedEventArgs e)
        {
            TextBox_Name.Description = "";

            PawnType selectedRow = (PawnType) DataGrid_PawnTypes.SelectedItem;

            if (selectedRow != null)
            {
                TextBox_Name.IsEnabled = true;
                Grid_ManagePawnTypes.Visibility = Visibility.Visible;
                TextBlock_Title.Text = titles[1];
                TextBox_Category.Text = ComboBox_Category.SelectedItem.ToString();
                TextBox_Name.Text = selectedRow.name;
                TextBlock_TypeId.Text = selectedRow.type_id.ToString();
                TextBox_No.Text = selectedRow.display_no.ToString();

                Button_ConfirmDelete.Visibility = Visibility.Collapsed;
                Button_Save.Visibility = Visibility.Collapsed;
                Button_Update.Visibility = Visibility.Visible;
                Button_Cancel.Visibility = Visibility.Visible;
                TextBox_No.Visibility = Visibility.Visible;


            }
        }

        private void ButtonClick_Update(object sender, RoutedEventArgs e)
        {

            int typeId = Convert.ToInt32(TextBlock_TypeId.Text);
            string updatedText = TextBox_Name.Text;

            if (!"".Equals(updatedText))
            {
                if (!isPawnTypeAlreadyExist(updatedText))
                {
                    PawnTypeModel pawnType = new PawnTypeModel();
                    Boolean isUpdated = pawnType.update(typeId, updatedText);

                    if (isUpdated)
                    {
                        int categoryId = ComboBox_Category.SelectedIndex + 1;
                        var bindingList = new BindingList<PawnType>(pawnType.selectAll(categoryId));
                        DataGrid_PawnTypes.ItemsSource = bindingList;

                        TextBox_Name.Text = "";
                        TextBox_Name.Description = "";
                        Grid_ManagePawnTypes.Visibility = Visibility.Collapsed;
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

        private void ButtonClick_Cancel(object sender, RoutedEventArgs e)
        {
            Grid_ManagePawnTypes.Visibility = Visibility.Collapsed;
        }

        private void ButtonClick_Save(object sender, RoutedEventArgs e)
        {
            string newType = TextBox_Name.Text;

            if (!"".Equals(newType))
            {
                if (!isPawnTypeAlreadyExist(newType))
                {
                    int categoryId = ComboBox_Category.SelectedIndex + 1;

                    PawnTypeModel pawnType = new PawnTypeModel();
                    Boolean isAdded = pawnType.add(categoryId, newType);

                    if (isAdded)
                    {
                        var bindingList = new BindingList<PawnType>(pawnType.selectAll(categoryId));
                        DataGrid_PawnTypes.ItemsSource = bindingList;

                        TextBox_Name.Text = "";
                        TextBox_Name.Description = "";
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

        private Boolean isPawnTypeAlreadyExist(String text)
        {
            int categoryId = ComboBox_Category.SelectedIndex + 1;

            PawnTypeModel pawnType = new PawnTypeModel();
            List<PawnType> items = pawnType.selectAll(categoryId);

            return items.FindIndex(item => item.name.ToLower().Equals(text)) >= 0 ? true : false;
        }

        private void ButtonClick_Delete(object sender, RoutedEventArgs e)
        {
            TextBox_Name.Description = "";

            PawnType selectedRow = (PawnType)DataGrid_PawnTypes.SelectedItem;

            if (selectedRow != null)
            {
                TextBlock_Title.Text = titles[2];
                TextBox_Name.Text = selectedRow.name;
                TextBox_Name.IsEnabled = false;
                TextBox_Name.Text = selectedRow.name;
                TextBlock_TypeId.Text = selectedRow.type_id.ToString();
                TextBox_No.Text = selectedRow.display_no.ToString();

                Grid_ManagePawnTypes.Visibility = Visibility.Visible;
                Button_ConfirmDelete.Visibility = Visibility.Visible;
                Button_Save.Visibility = Visibility.Collapsed;
                Button_Update.Visibility = Visibility.Collapsed;
                TextBox_No.Visibility = Visibility.Visible;
            }
        }

        private void SelectionChanged_DataGrid(object sender, SelectionChangedEventArgs e)
        {
            if (Grid_ManagePawnTypes.Visibility == Visibility.Visible && e.AddedItems.Count == 1)
            {
                if (TextBlock_Title.Text == titles[1] || TextBlock_Title.Text == titles[2])
                {
                    PawnType selectedRow = (PawnType) e.AddedItems[0];
                    TextBlock_TypeId.Text = selectedRow.type_id.ToString();
                    TextBox_Name.Text = selectedRow.name;
                    TextBox_No.Text = selectedRow.display_no.ToString();
                }
            }
        }

        private void ButtonClick_ConfirmDelete(object sender, RoutedEventArgs e)
        {
            PawnType selectedRow = (PawnType)DataGrid_PawnTypes.SelectedItem;
            int typeId = selectedRow.type_id;

            PawnTypeModel pawnType = new PawnTypeModel();
            Boolean isDeleted = pawnType.delete(typeId);

            if (isDeleted)
            {
                int categoryId = ComboBox_Category.SelectedIndex + 1;

                var bindingList = new BindingList<PawnType>(pawnType.selectAll(categoryId));
                DataGrid_PawnTypes.ItemsSource = bindingList;

                TextBox_Name.Text = "";
                Grid_ManagePawnTypes.Visibility = Visibility.Collapsed;
            }
        }
    }
}
