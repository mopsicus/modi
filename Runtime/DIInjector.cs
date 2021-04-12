// ----------------------------------------------------------------------------
// The MIT License
// MoDI. Lightweight IoC container for Unity. https://github.com/mopsicus/modi
// Copyright (c) 2021 Mopsicus <mail@mopsicus.ru>
// ----------------------------------------------------------------------------

using System;
using System.Reflection;

namespace MoDI {

    /// <summary>
    /// Tool to inject objects to fields from container by attributes
    /// </summary>
    public class DIInjector {

        /// <summary>
        /// Inject objects to fields with attributes
        /// </summary>
        /// <param name="instance">Instance to get fields</param>
        /// <param name="container">Container to resolve objects</param>
        public void Inject(object instance, DIContainer container) {
            Type type = instance.GetType();
            Type injectType = typeof(InjectAttribute);
            Type ignoreType = typeof(IgnoreInjectAttribute);
            bool isInjectClass = type.IsDefined(injectType);
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (FieldInfo field in fields) {
                if (field.IsStatic) {
                    continue;
                }
                if (isInjectClass) {
                    if (field.IsDefined(ignoreType)) {
                        continue;
                    }
                    field.SetValue(instance, container.Resolve(field));
                } else {
                    if (field.IsDefined(injectType)) {
                        field.SetValue(instance, container.Resolve(field));
                    }
                }
            }
        }

    }

}