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
