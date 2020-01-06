using System;
using System.IO;
using System.Xml;

namespace MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Json
{
    /// <summary>
    /// JSON序列化/反序列化 转换器
    /// </summary>
    public interface IJsonConverter
    {
        /// <summary>
        /// 序列化对象成JSON字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>序列化后的json字符串</returns>
        string SerializeObject(object obj);

        /// <summary>
        /// Json字符串反序列化为对象
        /// </summary>
        /// <param name="value">json字符串</param>
        /// <typeparam name="T">反序列化的类型</typeparam>
        /// <returns>反序列化出的对象</returns>
        T DeserializeObject<T>(string value);

        /// <summary>
        /// 从Stream流读取，进行反序列化
        /// </summary>
        /// <param name="type">反序列化的类型</param>
        /// <param name="stream">Stream流</param>
        /// <returns>反序列化出的对象</returns>
        object DeserializeFromStream(Type type, Stream stream);

        /// <summary>
        /// Json字符串反序列化为指定Type类型的对象
        /// </summary>
        /// <param name="value">JSON字符串</param>
        /// <param name="type">数据类型</param>
        /// <returns>反序列化出的对象</returns>
        object DeserializeFromString(string value, Type type);

        /// <summary>
        /// 反序列化为动态对象
        /// </summary>
        /// <param name="value">Json字符串</param>
        /// <returns></returns>
        dynamic DeserializeFromJsonString(string value);

        /// <summary>
        /// 序列化对象成JSON字符串，输出到Stream流
        /// </summary>
        /// <param name="value">需要被序列化的对象</param>
        /// <param name="type">对象类型</param>
        /// <param name="stream">Stream流</param>
        void SerializeToStream(object value, Type type, Stream stream);

        /// <summary>
        /// XML序列化为JSON字符串
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <returns></returns>
        string SerializeXmlNode(XmlDocument xmlDoc);
    }
}