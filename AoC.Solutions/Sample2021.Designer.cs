﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AoC.Solutions {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Sample2021 {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Sample2021() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("AoC.Solutions.Sample2021", typeof(Sample2021).Assembly);
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
        ///   Looks up a localized string similar to 7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1
        ///
        ///22 13 17 11  0
        /// 8  2 23  4 24
        ///21  9 14 16  7
        /// 6 10  3 18  5
        /// 1 12 20 15 19
        ///
        /// 3 15  0  2 22
        /// 9 18 13 17  5
        ///19  8  7 25 23
        ///20 11 10 24  4
        ///14 21 16 12  6
        ///
        ///14 21 17 24  4
        ///10 16 15  9 19
        ///18  8 23 26 20
        ///22 11 13  6  5
        /// 2  0 12  3  7.
        /// </summary>
        internal static string Day3 {
            get {
                return ResourceManager.GetString("Day3", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 0,9 -&gt; 5,9
        ///8,0 -&gt; 0,8
        ///9,4 -&gt; 3,4
        ///2,2 -&gt; 2,1
        ///7,0 -&gt; 7,4
        ///6,4 -&gt; 2,0
        ///0,9 -&gt; 2,9
        ///3,4 -&gt; 1,4
        ///0,0 -&gt; 8,8
        ///5,5 -&gt; 8,2.
        /// </summary>
        internal static string Day5 {
            get {
                return ResourceManager.GetString("Day5", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 3,4,3,1,2.
        /// </summary>
        internal static string Day6 {
            get {
                return ResourceManager.GetString("Day6", resourceCulture);
            }
        }
    }
}
