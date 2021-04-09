using Microsoft.Toolkit.Uwp.UI.Controls;
using Pawn_Shop.Dto;
using Pawn_Shop.Models;
using System;
using System.Collections.Generic;
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

namespace Pawn_Shop.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class KillInterest : Page
    {
        public List<Major> MajorList;

        public KillInterest()
        {
            this.InitializeComponent();

            /*MajorModel major = new MajorModel();
            MajorList = major.getMajors();*/
        }

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            /*if (e.PropertyDescriptor is PropertyDescriptor descriptor)
            {
                e.Column.Header = descriptor.DisplayName ?? descriptor.Name;
            }*/

           /* switch (e.PropertyName)
            {
                case nameof(Major.major_id):
                    e.Column.Header = "No.";
                    break;

                case nameof(Major.short_name):
                    e.Column.Header = "Short Name";
                    break;
            }*/
        }
    }
}
