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
    public class ClassRoomAPIService
    {
        //class ClassRoom
        //{
        //    public static List<ClassRoomData> GetClassNames()
        //    {

        //        string html = "http://jxgl.cic.tsinghua.edu.cn/jxpg/f/wxjwxs/jsxx?menu=false";
        //        HtmlWeb web = new HtmlWeb();
        //        var htmlDoc = web.Load(html);
        //        var htmlNodes = htmlDoc.DocumentNode.SelectNodes("//html/body/div/div/div[@class='list-block list-class']/div/ul/li");

        //        var Data = new List<ClassRoomData>();
        //        for (int i = 1; i < htmlNodes.Count; i++)
        //        {
        //            string uri = htmlNodes[i].ChildNodes[1].Attributes["href"].Value;
        //            string PosName = htmlNodes[i].ChildNodes[1].ChildNodes[1].ChildNodes[1].ChildNodes[1].InnerText;
        //            Data.Add(new ClassRoomData
        //            {
        //                DetailUri = uri,
        //                PositionName = PosName
        //            }
        //            );
        //        }
        //        return Data;
        //    }

        //}

        public static class Performance
        {
            
            public static List<PerformanceData> GetListShow()
            {
                
                //string html = ("http://www.hall.tsinghua.edu.cn/columnEx/pwzx_hdap/yc-dy-px-zl-jz/" + PerformanceData.page.ToString());
                string html = ("http://www.hall.tsinghua.edu.cn/columnEx/pwzx_hdap/yc-dy-px-zl-jz/1");
                HtmlWeb web = new HtmlWeb();
                var htmlDoc = web.Load(html);
                var htmlNodes = htmlDoc.DocumentNode.SelectNodes("/html/body/div[2]/div/div[2]/div/div");//timelist
                var InnerTest = htmlNodes[0].InnerHtml;
                Regex.Replace(InnerTest, "::after", "");//Remove after using System.Text.RegularExpressions;
                var doc = new HtmlDocument();
                doc.LoadHtml(InnerTest);
                var ListNodes = doc.DocumentNode.SelectNodes("/div");
                //ParseDataHere
                //int j = ListNodes.Count;

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



        private static DateTime lastLogin = DateTime.MinValue;
        private static int LOGIN_TIMEOUT_MINUTES = 1;

        public static class ParseBuildingClassData
        {
           
            public static async Task<List<ClassBuildingData>> GetListBuildingInfoAsync()
            {
                if ((DateTime.Now - lastLogin).TotalMinutes < LOGIN_TIMEOUT_MINUTES)
                {
                    Debug.WriteLine("[login] reuses recent session");
                    var TempData = await CacheHelper.ReadCache("ClassBuildingData");
                    var ReturnData = JSONHelper.Parse<List<ClassBuildingData>>(TempData);
                    lastLogin = DateTime.Now;
                    return ReturnData;

                }
                else
                {
                    lastLogin = DateTime.Now;
                }

                try
                {
                    string html = "http://jxgl.cic.tsinghua.edu.cn/jxpg/f/wxjwxs/jsxx/cx?classroom=六教&weeknumber=5&mobile=true";
                    HtmlWeb web = new HtmlWeb();
                    var htmlDoc = web.Load(html);

                    var _BuildingName = htmlDoc.DocumentNode.SelectNodes("//html/body/div/div/div[1]/div[1]/span/span")[0].InnerText;
                    var _InnerTextDate = htmlDoc.DocumentNode.SelectSingleNode("//html/body/div/div/div[1]/ul/li[1]/div[2]/div/select").InnerHtml;

                    Regex regexObj = new Regex(@"<option selected=""selected\"">(?<mycontent>[\s\S].*?)\r\n    \r\n    <option>");
                    var resultString = regexObj.Match(_InnerTextDate).Groups["mycontent"].Value;
                    var _Date = resultString;

                    var _NodeClassRoom = htmlDoc.DocumentNode.SelectNodes("/html/body/div/div/div[1]/ul/li[2]/div[@class='card-footer no-border']");

                    var Data = new List<ClassBuildingData>();
                    for (int i = 0; i < _NodeClassRoom.Count; i++)
                    {
                        var _NodeSpanClassRoom = _NodeClassRoom[i].ChildNodes;
                        var _ClassRoomName = _NodeSpanClassRoom[1].InnerText;

                        var _NodeClassState = _NodeSpanClassRoom[3].SelectNodes("i");

                        var _ListClassState = new List<string>();

                        for (int j = 0; j < _NodeClassState.Count; j++)
                        {
                            var StatueOfClassRoom = _NodeClassState[j].Attributes["class"].Value;
                            if (Regex.IsMatch(StatueOfClassRoom, "ico_zy"))//占用
                            {
                                string item = "占用";
                                _ListClassState.Add(item);
                            }
                            else
                            {
                                string item = "空闲";
                                _ListClassState.Add(item);
                            }

                        }


                        Data.Add(new ClassBuildingData
                        {
                            BuildingName = _BuildingName,
                            Date = _Date,
                            ListClassStatus = _ListClassState,
                            ClassRoomName = _ClassRoomName
                        }
                        );
                    }
                    var StringfiedData = JSONHelper.Stringify(Data);
                    await CacheHelper.WriteCache("ClassBuildingData", StringfiedData);
                    return Data;
                }
                catch
                {
                    var TempData = await CacheHelper.ReadCache("ClassBuildingData");
                    var ReturnData = JSONHelper.Parse<List<ClassBuildingData>>(TempData);
                    lastLogin = DateTime.MinValue;
                    return ReturnData;
                }

               


                
            }

        }
       
    }
    

}
