using ClassRoomAPI.Helpers;
using ClassRoomAPI.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClassRoomAPI.Services
{
    public class PerformanceAPIService
    {
        public static class Performance
        {
            public static List<PerformanceData> GetListShow()
            {
                string URL = "";
                if (PerformanceData.SelectedItem == 0) URL = "http://www.hall.tsinghua.edu.cn/columnEx/pwzx_hdap/yc/";
                if (PerformanceData.SelectedItem == 1) URL = "http://www.hall.tsinghua.edu.cn/columnEx/pwzx_hdap/dy/";
                if (PerformanceData.SelectedItem == 2) URL = "http://www.hall.tsinghua.edu.cn/columnEx/pwzx_hdap/jz/";
                if (PerformanceData.SelectedItem == 3) URL = "http://www.hall.tsinghua.edu.cn/columnEx/pwzx_hdap/zl/";
                string html = (URL + PerformanceData.page.ToString());
                HtmlWeb web = new HtmlWeb();
                var htmlDoc = web.Load(html);
                var htmlNodes = htmlDoc.DocumentNode.SelectNodes("/html/body/div[2]/div/div[2]/div/div");//timelist
                var InnerTest = htmlNodes[0].InnerHtml;
                Regex.Replace(InnerTest, "::after", "");//Remove after using System.Text.RegularExpressions;
                var doc = new HtmlDocument();
                doc.LoadHtml(InnerTest);
                var ListNodes = doc.DocumentNode.SelectNodes("/div");

                var Data = new List<PerformanceData>();
                for (int i = 1; i < ListNodes.Count; i++)
                {
                    string PerDay = ListNodes[i].ChildNodes[1].ChildNodes[3].ChildNodes[1].ChildNodes[0].InnerText; //演出的日
                    string PerDate = ListNodes[i].ChildNodes[1].ChildNodes[3].ChildNodes[1].ChildNodes[1].InnerText; //演出的年/月
                    string PerHour = ListNodes[i].ChildNodes[1].ChildNodes[3].ChildNodes[1].ChildNodes[3].InnerText;//演出在几点

                    PerHour = PerHour.Replace(" ", "");
                    string PerTime = PerDate + "-" + PerDay + "    " + PerHour;
                    PerTime = PerTime.Replace("\r", "");
                    PerTime = PerTime.Replace("\n", "");
                    PerTime = PerTime.Replace("\t", "");
                    string PerName = ListNodes[i].ChildNodes[1].ChildNodes[5].ChildNodes[1].InnerText;
                    string PerAddress = ListNodes[i].ChildNodes[1].ChildNodes[5].ChildNodes[5].InnerText;
                    string PerState = ListNodes[i].ChildNodes[1].ChildNodes[5].ChildNodes[7].InnerText;
                    PerState = PerState.Replace("\r", "");
                    PerState = PerState.Replace("\n", "");
                    PerState = PerState.Replace("\t", "");
                    Data.Add(new PerformanceData
                    {
                        PerformanceTime = PerTime,
                        PerformanceName = PerName,
                        PerformanceAddress = PerAddress,
                        PerformanceState = PerState
                    }
                    );
                }
                return Data;
            }
        }
    }
}
