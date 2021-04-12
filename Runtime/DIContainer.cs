// ----------------------------------------------------------------------------
// The MIT License
// MoDI. Lightweight IoC container for Unity. https://github.com/mopsicus/modi
// Copyright (c) 2021 Mopsicus <mail@mopsicus.ru>
// ----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Reflection;

namespace MoDI {

    /// <summary>
    /// Container to store all components and manage them
    /// </summary>
    public class DIContainer : IDIContainer {

        /// <summary>
        /// List of components
        /// </summary>
        readonly List<DIComponent> _components = null;

        /// <summary>
        /// List of cached single types
        /// </summary>
        readonly Dictionary<Type, object> _cache = null;

        /// <summary>
        /// Build container with tag and capacity for component's list
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="capacity"></param>
        public DIContainer(string tag, int capacity) {
            _components = new List<DIComponent>(capacity);
            _cache = new Dictionary<Type, object>(capacity);
        }

        /// <summary>
        /// Bind instance 
        /// Type will be get from itself
        /// </summary>
        /// <param name="instance"></param>
        public IDIComponentBuilder BindInstance(object instance) {
            if (instance == null) {
                throw new Exception($"Can't bind NULL instance");
            }
            return Bind(instance.GetType(), instance);
        }

        /// <summary>
        /// Bind type
        /// Instance is NULL by default
        /// </summary>
        /// <param name="type">Type to bind</param>
        /// <param name="instance">Instance to bind</param>
        public IDIComponentBuilder Bind(Type type, object instance = null) {
            DIComponent component = new DIComponent(type, instance);
            _components.Add(component);
            return component;
        }

        /// <summary>
        /// Bind type by generic
        /// </summary>
        /// <typeparam name="T">Type to bind</typeparam>
        public IDIComponentBuilder Bind<T>() {
            return Bind(typeof(T));
        }

        /// <summary>
        /// Resolve object for field
        /// Check id if need get by custom bind id
        /// </summary>
        /// <param name="field">FieldInfo from DIMonoBehaviour</param>
        public object Resolve(FieldInfo field) {
            Type type = field.FieldType;
            InjectAttribute attribute = field.GetCustomAttribute<InjectAttribute>();
            string id = (attribute != null && !string.IsNullOrEmpty(attribute.Id)) ? attribute.Id : null;
            return GetByType(type, id);
        }

        /// <summary>
        /// Resolve object for type
        /// Id is optional
        /// </summary>
        /// <param name="type">Type to resolve</param>
        /// <param name="id">Custom bind id</param>
        public object Resolve(Type type, string id = null) {
            return GetByType(type, id);
        }

        /// <summary>
        /// Resolve object for type by generic
        /// Id is optional
        /// </summary>
        /// <typeparam name="T">Type to resolve</typeparam>
        /// <param name="id">Custom bind id</param>
        public T Resolve<T>(string id = null) {
            return (T)Resolve(typeof(T), id);
        }

        /// <summary>
        /// Resolve object by type
        /// Get object from cache is as binded as single
        /// Or build with component's options
        /// </summary>
        /// <param name="type">Type to resolve</param>
        /// <param name="id">Custom bind id</param>
        object GetByType(Type type, string id) {
            IDIComponent component = default(IDIComponent);
            for (int i = 0; i < _components.Count; i++) {
                if (_components[i].ContainsType(type)) {
                    if (!string.IsNullOrEmpty(id)) {
                        if (string.IsNullOrEmpty(_components[i].Id) || !_components[i].Id.Equals(id)) {
                            continue;
                        }
                        component = _components[i];
                        break;
                    }
                    component = _components[i];
                    break;
                }
            }
            if (component == null) {
                throw new Exception($"Can't find bind for type '{type.Name}' and id = '{id}'");
            }
            return GetByScope(component);
        }

        /// <summary>
        /// Get component from cache or build
        /// </summary>
        /// <param name="component">Component to build</param>
        object GetByScope(IDIComponent component) {
            switch (component.Scope) {
                case ScopeMode.Default:
                    return Build(component);
                case ScopeMode.AsSingle:
                    if (!_cache.ContainsKey(component.Type)) {
                        _cache.Add(component.Type, Build(component));
                    }
                    return _cache[component.Type];
                default:
                    return null;
            }
        }

        /// <summary>
        /// Build object with component's options
        /// </summary>
        /// <param name="component">Component to build</param>
        object Build(IDIComponent component) {
            if (component.Construction.HasFlag(ConstructionMode.FromInstance)) {
                if (component.Instance == null) {
                    throw new Exception($"Instance is NULL for type '{component.Type.Name}' and id = '{component.Id}'");
                }
                return component.Instance;
            }
            if (component.Construction.HasFlag(ConstructionMode.WithArguments)) {
                if (component.Arguments == null) {
                    throw new Exception($"Arguments is NULL for type '{component.Type.Name}' and id = '{component.Id}'");
                }
                return Activator.CreateInstance(component.Type, component.Arguments);
            }
            return Activator.CreateInstance(component.Type);
        }

        /// <summary>
        /// Clear container
        /// </summary>
        public void Clear() {
            _components.Clear();
            _cache.Clear();
        }
    }

}