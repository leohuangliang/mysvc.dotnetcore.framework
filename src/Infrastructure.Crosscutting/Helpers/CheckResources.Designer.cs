﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MySvc.Framework.Infrastructure.Crosscutting.Helpers {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class CheckResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal CheckResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MySvc.Framework.Infrastructure.Crosscutting.Helpers.CheckResources", typeof(CheckResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to \&quot;{0}\&quot; cannot be null..
        /// </summary>
        internal static string ArgumentCannotBeNull {
            get {
                return ResourceManager.GetString("ArgumentCannotBeNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to \&quot;{0}\&quot; cannot be neither null nor an empty array..
        /// </summary>
        internal static string ArgumentCannotBeNullOrEmptyArray {
            get {
                return ResourceManager.GetString("ArgumentCannotBeNullOrEmptyArray", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to \&quot;{0}\&quot; cannot be neither null nor an empty collection..
        /// </summary>
        internal static string ArgumentCannotBeNullOrEmptyCollection {
            get {
                return ResourceManager.GetString("ArgumentCannotBeNullOrEmptyCollection", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to \&quot;{0}\&quot; cannot be neither null nor an empty string..
        /// </summary>
        internal static string ArgumentCannotBeNullOrEmptyString {
            get {
                return ResourceManager.GetString("ArgumentCannotBeNullOrEmptyString", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to \&quot;{0}\&quot; must be between \&quot;{1}\&quot; and \&quot;{2}\&quot;..
        /// </summary>
        internal static string ArgumentMustBeInRange {
            get {
                return ResourceManager.GetString("ArgumentMustBeInRange", resourceCulture);
            }
        }
    }
}
