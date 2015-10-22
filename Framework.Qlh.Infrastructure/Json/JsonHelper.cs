using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Json;

namespace WindowsServer.Json
{
    public static class JsonHelper
    {
        public static T FromJsonString<T>(string jsonString)
        {
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                return (T)jsonSerializer.ReadObject(ms);
            }
        }

        public static string JsonSerializer<T>(T obj)
        {
            string jsonString = string.Empty;
            try
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));

                using (MemoryStream ms = new MemoryStream())
                {
                    serializer.WriteObject(ms, obj);
                    jsonString = Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            catch(Exception e)
            {
                jsonString = string.Empty;
            }
            return jsonString;
        }

        public static T FromJsonString<T>(Stream jsonStream)
        {
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(T));
            return (T)jsonSerializer.ReadObject(jsonStream);
        }

        public static void OverrideJsonObject(JsonObject overridedJsonObject, JsonObject addOnJsonObject)
        {
            foreach (var item in addOnJsonObject)
            {
                overridedJsonObject[item.Key] = item.Value;
            }
        }
    }
}
