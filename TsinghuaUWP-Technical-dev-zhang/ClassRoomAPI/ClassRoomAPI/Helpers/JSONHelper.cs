using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace ClassRoomAPI.Helpers
{
    public class JSONHelper
    {
        public static T Parse<T>(string jsonString)
        {
            try
            {
                using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
                {
                    return (T)new DataContractJsonSerializer(typeof(T)).ReadObject(ms);
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("JSON" + typeof(T).ToString());
                throw new Exception("JSON" + typeof(T).ToString());
            }

        }

        public static string Stringify(object jsonObject)
        {
            using (var ms = new MemoryStream())
            {
                new DataContractJsonSerializer(jsonObject.GetType()).WriteObject(ms, jsonObject);

                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }
    }
}
