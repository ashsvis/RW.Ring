﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1022
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JetExcelOleDbImport.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("JetExcelOleDbImport.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to Import from Excel.
        /// </summary>
        internal static string EXCEL_IMPORT {
            get {
                return ResourceManager.GetString("EXCEL_IMPORT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to File {0} is opened in Excel. It may cause unstable worling of the system.
        ///Close Excel and try again.
        /// </summary>
        internal static string FILE_OPENED {
            get {
                return ResourceManager.GetString("FILE_OPENED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Import from {0}.
        /// </summary>
        internal static string IMPORT_FROM {
            get {
                return ResourceManager.GetString("IMPORT_FROM", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Matrix import from Excel.
        /// </summary>
        internal static string MATRIX_EXCEL_IMPORT {
            get {
                return ResourceManager.GetString("MATRIX_EXCEL_IMPORT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cell name is not set.
        /// </summary>
        internal static string NO_CELL {
            get {
                return ResourceManager.GetString("NO_CELL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Column name is not set.
        /// </summary>
        internal static string NO_COLUMN {
            get {
                return ResourceManager.GetString("NO_COLUMN", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Excel file is not selected.
        /// </summary>
        internal static string NO_EXCEL_FILE {
            get {
                return ResourceManager.GetString("NO_EXCEL_FILE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Row name is not set.
        /// </summary>
        internal static string NO_ROW {
            get {
                return ResourceManager.GetString("NO_ROW", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Table is not selected.
        /// </summary>
        internal static string NO_TABLE {
            get {
                return ResourceManager.GetString("NO_TABLE", resourceCulture);
            }
        }
    }
}