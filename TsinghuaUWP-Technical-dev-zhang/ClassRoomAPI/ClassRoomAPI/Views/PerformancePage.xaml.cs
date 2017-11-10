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
using ClassRoomAPI.Services;
using ClassRoomAPI.Models;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace ClassRoomAPI.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class HallInfo : Page
    {
        public HallInfo()
        {
            this.InitializeComponent();
            PerformanceListView.ItemsSource = PerformanceAPIService.Performance.GetListShow();
        }

        

        private void FrontPage_Click(object sender, RoutedEventArgs e)
        {
            if (PerformanceData.page > 1)
            {
                PerformanceData.page -= 1;
                PerformanceListView.ItemsSource = PerformanceAPIService.Performance.GetListShow();
            }
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            PerformanceData.page += 1;
            PerformanceListView.ItemsSource = PerformanceAPIService.Performance.GetListShow();
        }

        private void ComboBox_SortOfTimeChanged(object sender, SelectionChangedEventArgs e)
        {
            //ComboBoxItem item = Time.SelectedItem as ComboBoxItem;
            //if (item != null) { string content = item.Content.ToString(); }
            //int i = 0;
            var j = Time1.SelectedIndex;

        }

        private void ComboBox_SortOfStatusChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBox_SortOfPlaceChanged(object sender, SelectionChangedEventArgs e)
        {
            
            
        }

        private void PerformanceInfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //PerformanceData.SelectedItem = PerformanceInfo.Name.ToString();
            PerformanceData.SelectedItem = PerformanceInfo.SelectedIndex;
            if (PerformanceData.SelectedItem == 0)
            {
                PerformanceListView.ItemsSource = PerformanceAPIService.Performance.GetListShow();
            }
            if (PerformanceData.SelectedItem == 1)
            {
                MovieListView.ItemsSource = PerformanceAPIService.Performance.GetListShow();
            }
            if (PerformanceData.SelectedItem == 2)
            {
                LectureListView.ItemsSource = PerformanceAPIService.Performance.GetListShow();
            }
            if (PerformanceData.SelectedItem == 3)
            {
                ArtListView.ItemsSource = PerformanceAPIService.Performance.GetListShow();
            }
        }
    }
    
}
