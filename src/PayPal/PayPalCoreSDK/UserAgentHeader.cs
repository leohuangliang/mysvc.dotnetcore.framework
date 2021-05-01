using System;
using System.Collections.Generic;
using System.Management;
using System.Text;
using System.Runtime.InteropServices;

namespace PayPal
{
    /// <summary>
    /// PayPal User-Agent Header implementation class
    /// </summary>
    public class UserAgentHeader
    {
        /// <summary>
        /// Product Id
        /// </summary>
        private string productId;

        /// <summary>
        /// Product Version
        /// </summary>
        private string productVersion;

        /// <summary>
        /// UserAgentHeader Constructor
        /// </summary>
        /// <param name="productId">Product Id, defaults to empty string if null or empty</param>
        /// <param name="productVersion">Product Version, defaults to empty string if null or empty</param>
        public UserAgentHeader(string productId, string productVersion)
        {
            this.productId = productId;
            this.productVersion = productVersion;
        }

        /// <summary>
        /// Returns a PayPal specific User-Agent HTTP Header
        /// </summary>
        /// <returns>Dictionary containing User-Agent HTTP Header</returns>
        public Dictionary<string, string> GetHeader()
        {
            Dictionary<string, string> userAgentDictionary = new Dictionary<string, string>();
            userAgentDictionary.Add(BaseConstants.UserAgentHeader, this.GetUserAgentHeader());
            return userAgentDictionary;
        }

        /// <summary>
        /// Creates the signature for the UserAgent header.
        /// </summary>
        /// <returns>A string containing the signature for the UserAgent header.</returns>
        private string GetUserAgentHeader()
        {
            StringBuilder header = new StringBuilder("PayPalSDK/");
            header.Append(this.productId);
            header.Append(" " + this.productVersion);
            header.Append(" (");

            header.Append(string.Join(";", new string[] 
            {
                FormatUserAgentParameter("core", BaseConstants.SdkVersion),
                FormatUserAgentParameter("lang", "DOTNET"),
                FormatUserAgentParameter("v", DotNetVersion),
                FormatUserAgentParameter("clr", DotNetClrVersion),
                FormatUserAgentParameter("bit", OperatingSystemBitness),
                FormatUserAgentParameter("os", OperatingSystemName)
            }));
            header.Append(")");
            return header.ToString();
        }

        /// <summary>
        /// Formats a parameter name and value to be used in the signature of a UserAgent header.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>A formatted string containing both the parameter name and value.</returns>
        private string FormatUserAgentParameter(string name, object value)
        {
            return string.Format("{0}={1}", name, value);
        }



        /// <summary>
        /// Returns whether or not the operating system is 64-bit.
        /// </summary>
        /// <returns>True = 64-bit, False = 32-bit</returns>
        private static bool Is64Bit()
        {

            return Environment.Is64BitOperatingSystem;
        }

        /// <summary>
        /// Gets the bitness of the operating system.
        /// </summary>
        private static int OperatingSystemBitness { get { return Is64Bit() ? 64 : 32; } }

        /// <summary>
        /// Gets the name of the operating system.
        /// </summary>
        private static string OperatingSystemName { get { return Environment.OSVersion.ToString(); } }

        /// <summary>
        /// Gets the version of the current .NET common language runtime environment.
        /// </summary>
        private static string DotNetClrVersion { get { return Environment.Version.ToString().Trim(); } }

        /// <summary>
        /// Gets the version of the current .NET environment.
        /// </summary>
        private static string DotNetVersion 
        {
            get
            {
                return "5.0";
            }
        }
    }
}
