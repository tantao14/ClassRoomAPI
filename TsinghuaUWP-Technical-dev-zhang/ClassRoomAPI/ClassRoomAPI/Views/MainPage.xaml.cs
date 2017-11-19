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
using System.Threading.Tasks;
using ClassRoomAPI.Services;
using ClassRoomAPI.Models;


// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace ClassRoomAPI
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        
        public MainPage()
        {
            this.InitializeComponent();
            //需要考虑没网的情况
            
        }

        private async void HHH_Click(object sender, RoutedEventArgs e)
        {
            var Data=await ClassRoomAPIService.ParseBuildingClassData.GetListBuildingInfoAsync();
            Building_6ListView.ItemsSource = Data; 

        }

        public async void TempRefreshDataCortana()
        {
            var Data = await ClassRoomAPIService.ParseBuildingClassData.GetListBuildingInfoAsync();
            Building_6ListView.ItemsSource = Data;
        }

        private async void ClassRoomInfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClassBuildingData.BuildingSelected = ClassRoomInfo.SelectedIndex + 1;
            ClassBuildingData.HasChanged = true;
            if (ClassBuildingData.BuildingSelected == 1)
            {
                
                var Data = await ClassRoomAPIService.ParseBuildingClassData.GetListBuildingInfoAsync();
                Building_1ListView.ItemsSource = Data;
                //Building_1ListView.ItemsSource = ClassRoomAPIService.ParseBuildingClassData.GetListBuildingInfoAsync();
            }
            if (ClassBuildingData.BuildingSelected == 2)
            {
                
                var Data = await ClassRoomAPIService.ParseBuildingClassData.GetListBuildingInfoAsync();
                Building_2ListView.ItemsSource = Data;
            }
            if (ClassBuildingData.BuildingSelected == 3)
            {
                
                var Data = await ClassRoomAPIService.ParseBuildingClassData.GetListBuildingInfoAsync();
                Building_3ListView.ItemsSource = Data;
            }
            if (ClassBuildingData.BuildingSelected == 4)
            {
               
                var Data = await ClassRoomAPIService.ParseBuildingClassData.GetListBuildingInfoAsync();
                Building_4ListView.ItemsSource = Data;
            }
            if (ClassBuildingData.BuildingSelected == 5)
            {
                
                var Data = await ClassRoomAPIService.ParseBuildingClassData.GetListBuildingInfoAsync();
                Building_5ListView.ItemsSource = Data;
            }
            if (ClassBuildingData.BuildingSelected == 6)
            {
               
                var Data = await ClassRoomAPIService.ParseBuildingClassData.GetListBuildingInfoAsync();
                Building_6ListView.ItemsSource = Data;
            }

        }
    }
}
