using System;
using System.Web;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using PayPal.OpenIdConnect;
using PayPal.Exception;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace PayPal.Util
{
    public class SDKUtil
    {
        //TODO: To be renamed as 'FormatUriPath' as per .NET Naming Conventions
        /// <summary>
        /// Formats the URI path for REST calls.
        /// </summary>
        /// <param name="pattern">URI path with placeholders that can be replaced with string's Format method</param>
        /// <param name="parameters">Parameters holding actual values for placeholders; They can be wrapper objects for specific query strings like QueryParameters, CreateFromAuthorizationCodeParameters, CreateFromRefreshTokenParameters, UserinfoParameters parameters or a simple Dictionary</param>
        /// <returns>Processed URI path, or null if pattern or parameters is null</returns>
        public static string FormatURIPath(string pattern, Object[] parameters)
        {
            string formatString = pattern;
            if (pattern != null && parameters != null)
            {
                if (parameters != null && parameters.Length == 1 && parameters[0] is QueryParameters)
                {
                    //Form a object array using the passed Map
                    parameters = SplitParameters(pattern, ((QueryParameters)parameters[0]).GetMap());
                }
                else if (parameters != null && parameters.Length == 1 && parameters[0] is CreateFromAuthorizationCodeParameters)
                {
                    //Form a object array using the passed Map
                    parameters = SplitParameters(pattern, ((CreateFromAuthorizationCodeParameters)parameters[0]).ContainerMap);
                }
                else if (parameters != null && parameters.Length == 1 && parameters[0] is CreateFromRefreshTokenParameters)
                {
                    //Form a object array using the passed Map
                    parameters = SplitParameters(pattern, ((CreateFromRefreshTokenParameters)parameters[0]).ContainerMap);
                }
                else if (parameters != null && parameters.Length == 1 && parameters[0] is UserinfoParameters)
                {
                    //Form a object array using the passed Map
                    parameters = SplitParameters(pattern, ((UserinfoParameters)parameters[0]).ContainerMap);
                }
                else if (parameters != null && parameters.Length == 1 && parameters[0] is Dictionary<string, string>)
                {
                    parameters = SplitParameters(pattern, (Dictionary<string, string>)parameters[0]);
                }

                //Perform a simple message formatting
                formatString = string.Format(pattern, parameters);

                //Process the resultant string for removing nulls
                formatString = RemoveNullsFromQueryString(formatString);
            }
            return formatString;
        }

        //TODO: To be renamed as 'FormatUriPath' as per .NET Naming Conventions
        /// <summary>
        /// Formats the URI path for REST calls. Replaces any occurrences of the form
        /// {name} in pattern with the corresponding value of key name in the passed
        /// Dictionary
        /// </summary>
        /// <param name="pattern">URI pattern with named place holders</param>
        /// <param name="pathParameters">Dictionary</param>
        /// <returns>Processed URI path</returns>
        public static string FormatURIPath(string pattern, Dictionary<string, string> pathParameters)
        {
            return FormatURIPath(pattern, pathParameters, null);
        }

        //TODO: To be renamed as 'FormatUriPath' as per .NET Naming Conventions
        /// <summary>
        /// Formats the URI path for REST calls. Replaces any occurrences of the form
        /// {name} in pattern with the corresponding value of key name in the passed
        /// Dictionary. Query parameters are appended to the end of the URI path
        /// </summary>
        /// <param name="pattern">URI pattern with named place holders</param>
        /// <param name="pathParameters">Dictionary of Path parameters</param>
        /// <param name="queryParameters">Dictionary for Query parameters</param>
        /// <returns>Processed URI path</returns>
        public static string FormatURIPath(string pattern, Dictionary<string, string> pathParameters, Dictionary<string, string> queryParameters)
        {
            string formattedURIPath = null;
            if (!String.IsNullOrEmpty(pattern) && pathParameters != null && pathParameters.Count > 0)
            {
                foreach (KeyValuePair<string, string> entry in pathParameters)
                {
                    // do something with entry.Value or entry.Key
                    string placeHolderName = "{" + entry.Key.Trim() + "}";
                    if (pattern.Contains(placeHolderName))
                    {
                        pattern = pattern.Replace(placeHolderName, entry.Value.Trim());
                    }
                }
            }
            formattedURIPath = pattern;
            if (queryParameters != null && queryParameters.Count > 0)
            {
                StringBuilder stringBuilder = new StringBuilder(formattedURIPath);
                if (stringBuilder.ToString().Contains("?"))
                {
                    if (!(stringBuilder.ToString().EndsWith("?") || stringBuilder.ToString().EndsWith("&")))
                    {
                        stringBuilder.Append("&");
                    }
                }
                else
                {
                    stringBuilder.Append("?");
                }
                foreach (KeyValuePair<string, string> entry in queryParameters)
                {
                    stringBuilder.Append(HttpUtility.UrlEncode(entry.Key, Encoding.UTF8)).Append("=").Append(HttpUtility.UrlEncode(entry.Value, Encoding.UTF8)).Append("&");
                }
                formattedURIPath = stringBuilder.ToString();
            }
            if (formattedURIPath.Contains("{") || formattedURIPath.Contains("}"))
            {
                throw new PayPalException("Unable to formatURI Path : "
                    + formattedURIPath
                    + ", unable to replace placeholders with the map : "
                    + pathParameters);
            }
            return formattedURIPath;
        }

        private static string RemoveNullsFromQueryString(string formatString)
        {
            if (formatString != null && formatString.Length != 0)
            {
                string[] parts = formatString.Split('?');

                //Process the query string part
                if (parts.Length == 2)
                {
                    string queryString = parts[1];
                    string[] queryStringSplit = queryString.Split('&');
                    if (queryStringSplit.Length > 0)
                    {
                        StringBuilder builder = new StringBuilder();
                        foreach (string query in queryStringSplit)
                        {
                            string[] valueSplit = query.Split('=');
                            if (valueSplit.Length == 2)
                            {
                                if (valueSplit[1].Trim().ToLower().Equals("null"))
                                {
                                    continue;
                                }
                                else if (valueSplit[1].Trim().Length == 0)
                                {
                                    continue;
                                }
                                else
                                {
                                    builder.Append(query).Append("&");
                                }
                            }
                            else if (valueSplit.Length < 2)
                            {
                                continue;
                            }
                        }
                        formatString = (!builder.ToString().EndsWith("&")) ? builder.ToString()
                            : builder.ToString().Substring(0, builder.ToString().Length - 1);
                    }

                    //Append the query string delimiter
                    formatString = (parts[0].Trim() + "?") + formatString;
                }
            }
            return formatString;
        }

        /// <summary>
        /// Split the URI and form a Object array using the query string and values
        /// in the provided map. The return object array is populated only if the map
        /// contains valid value for the query name. The object array contains null
        /// values if there is no value found in the map
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private static Object[] SplitParameters(string pattern, Dictionary<string, string> parameters)
        {
            List<Object> objectList = new List<Object>();
            string[] query = pattern.Split('?');
            if (query != null && query.Length == 2 && query[1].Contains("={"))
            {
                NameValueCollection queryParts = HttpUtility.ParseQueryString(query[1]);

                foreach (string k in queryParts.AllKeys)
                {
                    string val = string.Empty;
                    if (parameters.TryGetValue(k.Trim(), out val))
                    {
                        objectList.Add(val);
                    }
                    else
                    {
                        objectList.Add(null);
                    }

                }
            }
            return objectList.ToArray();
        }

        /// <summary>
        /// Escapes invalid XML characters using & escapes
        /// </summary>
        /// <param name="textContent">Text content to escape</param>
        /// <returns>Escaped XML string</returns>
        public static string EscapeInvalidXmlCharsRegex(string textContent)
        {
            string response = null;
            if (textContent != null && textContent.Length > 0)
            {
                response = Regex.Replace(
                                Regex.Replace(
                                    Regex.Replace(
                                        Regex.Replace(
                                            Regex.Replace(textContent, "&(?!(amp;|lt;|gt;|quot;|apos;))", "&amp;"),
                                        "<", "&lt;"),
                                    ">", "&gt;"),
                                "\"", "&quot;"),
                           "'", "&apos;");
            }
            return response;
        }

        /// <summary>
        /// Escapes invalid XML characters using & escapes
        /// </summary>
        /// <param name="intContent">Integer content to escape</param>
        /// <returns>Escaped XML string</returns>
        public static string EscapeInvalidXmlCharsRegex(int? intContent)
        {
            string response = null;
            if (intContent != null)
            {
                string textContent = intContent.ToString();
                response = EscapeInvalidXmlCharsRegex(textContent);
            }
            return response;
        }

        /// <summary>
        /// Escapes invalid XML characters using & escapes
        /// </summary>
        /// <param name="boolContent">Boolean content to escape</param>
        /// <returns>Escaped XML string</returns>
        public static string EscapeInvalidXmlCharsRegex(bool? boolContent)
        {
            string response = null;
            if (boolContent != null)
            {
                string textContent = boolContent.ToString();
                response = EscapeInvalidXmlCharsRegex(textContent);
            }
            return response;
        }

        /// <summary>
        /// Escapes invalid XML characters using & escapes
        /// </summary>
        /// <param name="floatContent">Float content to escape</param>
        /// <returns>Escaped XML string</returns>
        public static string EscapeInvalidXmlCharsRegex(float? floatContent)
        {
            string response = null;
            if (floatContent != null)
            {
                string textContent = floatContent.ToString();
                response = EscapeInvalidXmlCharsRegex(textContent);
            }
            return response;
        }

        /// <summary>
        /// Escapes invalid XML characters using & escapes
        /// </summary>
        /// <param name="doubleContent">Double content to escape</param>
        /// <returns>Escaped XML string</returns>
        public static string EscapeInvalidXmlCharsRegex(double? doubleContent)
        {
            string response = null;
            if (doubleContent != null)
            {
                string textContent = doubleContent.ToString();
                response = EscapeInvalidXmlCharsRegex(textContent);
            }
            return response;
        }

        /// <summary>
        /// Gets the version number of the parent assembly for the specified object type.
        /// </summary>
        /// <typeparam name="T">The object type to use in determining which assembly version should be returned.</typeparam>
        /// <returns>A 3-digit version of the parent assembly.</returns>
        public static string GetAssemblyVersionForType(Type type)
        {
            return type.Assembly.GetName().Version.ToString(3);
        }

        /// <summary>
        /// Adds User-Agent information to the specified headers map.
        /// </summary>
        /// <param name="headers">A map of headers to be updated with user-agent information.</param>
        public static void AddUserAgentToHeader(Dictionary<string, string> headers, string name, string version)
        {
            UserAgentHeader userAgentHeader = new UserAgentHeader(name, version);
            foreach (KeyValuePair<string, string> item in userAgentHeader.GetHeader())
            {
                headers[item.Key] = item.Value;
            }
        }

       
    }
}
