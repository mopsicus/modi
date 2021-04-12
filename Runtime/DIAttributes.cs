// ----------------------------------------------------------------------------
// The MIT License
// MoDI. Lightweight IoC container for Unity. https://github.com/mopsicus/modi
// Copyright (c) 2021 Mopsicus <mail@mopsicus.ru>
// ----------------------------------------------------------------------------

using System;

namespace MoDI {

    /// <summary>
    /// Attribute for inject fields of DIMonoBehaviour
    /// Can receive argument to get object by id
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public class InjectAttribute : Attribute {

        /// <summary>
        /// Attribute Id
        /// </summary>
        public readonly string Id;

        /// <summary>
        /// Attribute constructor
        /// </summary>
        /// <param name="id">Option Id param for inject</param>
        public InjectAttribute(string id = null) {
            Id = id;
        }

    }

    /// <summary>
    /// Attribute for ignore inject some fields when injecting whole class
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public class IgnoreInjectAttribute : Attribute { }

}