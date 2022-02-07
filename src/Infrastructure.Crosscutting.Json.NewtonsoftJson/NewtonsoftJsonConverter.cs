using System;
using System.IO;
using System.Xml;
using MySvc.Framework.Infrastructure.Crosscutting.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MySvc.Framework.Infrastructure.NewtonsoftJson
{
    /// <summary>
    /// 基于JSON.NET 实现JSON序列化/反序列化
    /// </summary>
    public class NewtonsoftJsonConverter : IJsonConverter
    {
        private JsonSerializerSettings SerializerSettings { get; set; }

        public NewtonsoftJsonConverter()
        {
        }

        public NewtonsoftJsonConverter(JsonSerializerSettings serializerSettings)
        {
            SerializerSettings = serializerSettings;
        }

        /// <summary>
        /// 序列化对象成JSON字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>序列化后的json字符串</returns>
        public string SerializeObject(object obj)
        {
            if (SerializerSettings != null)
            {
                return JsonConvert.SerializeObject(obj, SerializerSettings);
            }

            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// Json字符串反序列化为对象
        /// </summary>
        /// <param name="value">json字符串</param>
        /// <typeparam name="T">反序列化的类型</typeparam>
        /// <returns>反序列化出的对象</returns>
        public T DeserializeObject<T>(string value)
        {
            if (SerializerSettings != null)
            {
                return JsonConvert.DeserializeObject<T>(value, SerializerSettings);
            }

            return JsonConvert.DeserializeObject<T>(value);
        }

        /// <summary>
        /// 从Stream流读取，进行反序列化
        /// </summary>
        /// <param name="type">反序列化的类型</param>
        /// <param name="stream">Stream流</param>
        /// <returns>反序列化出的对象</returns>
        public object DeserializeFromStream(Type type, Stream stream)
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                using (JsonTextReader jsonReader = new JsonTextReader(reader))
                {
                    var serializer = SerializerSettings != null ?
                        JsonSerializer.Create(SerializerSettings) : JsonSerializer.CreateDefault();
                    return new JsonSerializer().Deserialize(jsonReader, type);
                }
            }
        }

        /// <summary>
        /// Json字符串反序列化为指定Type类型的对象
        /// </summary>
        /// <param name="value">JSON字符串</param>
        /// <param name="type">数据类型</param>
        /// <returns>反序列化出的对象</returns>
        public object DeserializeFromString(string value, Type type)
        {
            if (SerializerSettings != null)
            {
                return JsonConvert.DeserializeObject(value, type, SerializerSettings);
            }
            return JsonConvert.DeserializeObject(value, type);
        }

        public dynamic DeserializeFromJsonString(string value)
        {
            return JObject.Parse(value);
        }

        /// <summary>
        /// 序列化对象成JSON字符串，输出到Stream流
        /// </summary>
        /// <param name="value">需要被序列化的对象</param>
        /// <param name="type">对象类型</param>
        /// <param name="stream">Stream流</param>
        public void SerializeToStream(object value, Type type, Stream stream)
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                using (JsonTextWriter jsonWriter = new JsonTextWriter(writer))
                {
                    var serializer = SerializerSettings != null ?
                        JsonSerializer.Create(SerializerSettings) : JsonSerializer.CreateDefault();
                    serializer.Serialize(jsonWriter, value, type);
                    jsonWriter.Flush();
                }
            }
        }

        /// <summary>
        /// XML序列化为JSON字符串
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <returns></returns>
        public string SerializeXmlNode(XmlDocument xmlDoc)
        {
            return JsonConvert.SerializeXmlNode(xmlDoc);
        }

    }
}
