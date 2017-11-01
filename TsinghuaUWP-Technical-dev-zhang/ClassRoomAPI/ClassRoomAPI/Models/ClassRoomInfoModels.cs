using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassRoomAPI.Models
{
    public class PerformanceData
    {
        public string PerformanceName;
        public string PerformanceTime;
        public string PerformanceAddress;
        public string PerformanceState;
        public static int page;
    }
    public class ClassBuildingData
    {
        public string BuildingName;
        public string Date;
        public string ClassRoomName;

        //6节课1-6
        public List<string> ListClassStatus;
    }
}
