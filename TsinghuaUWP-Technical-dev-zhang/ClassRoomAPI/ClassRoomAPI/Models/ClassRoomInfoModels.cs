using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassRoomAPI.Models
{
    public class ClassBuildingData
    {
        public string BuildingName;
        public string Date;
        public string ClassRoomName;

        //6节课1-6
        public List<string> ListClassStatus;
        public static int BuildingSelected;
        public static bool HasChanged = false;
    }
}
