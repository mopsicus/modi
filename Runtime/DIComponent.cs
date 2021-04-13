// ----------------------------------------------------------------------------
// The MIT License
// MoDI. Lightweight IoC container for Unity. https://github.com/mopsicus/modi
// Copyright (c) 2021 Mopsicus <mail@mopsicus.ru>
// ----------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace MoDI {

    /// <summary>
    /// Component to store bind options
    /// </summary>
    public class DIComponent : IDIComponent, IDIComponentBuilder {

        /// <summary>
        /// List of binded types
        /// </summary>
        private List<Type> _types = null;

        /// <summary>
        /// Optional component's id
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Scope mode for single or default use
        /// </summary>
        public ScopeMode Scope { get; private set; }

        /// <summary>
        /// Construction mode to set build variant of component
        /// </summary>
        public ConstructionMode Construction { get; private set; }

        /// <summary>
        /// Optional component's arguments
        /// </summary>
        public object[] Arguments { get; private set; }

        /// <summary>
        /// Component's instance
        /// </summary>
        public object Instance { get; private set; }

        /// <summary>
        /// Type of component
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Build component with type, instance and default options
        /// </summary>
        /// <param name="type">Type of component</param>
        /// <param name="instance">Instance of component</param>
        public DIComponent(Type type, object instance) {
            Id = null;
            Scope = ScopeMode.Default;
            Construction = (instance == null) ? ConstructionMode.Default : ConstructionMode.FromInstance;
            Arguments = null;
            Instance = instance;
            Type = type;
        }

        /// <summary>
        /// Check type in component's types list
        /// </summary>
        /// <param name="type">Type to check</param>
        /// <returns>Contains or not</returns>
        public bool ContainsType(Type type) {
            if (_types == null) {
                return (Type == type);
            }
            return _types.Contains(type);
        }

        /// <summary>
        /// Bind as type
        /// </summary>
        /// <param name="type">Type to bind</param>
        public IDIComponentBuilder As(Type type) {
            if (_types == null) {
                _types = new List<Type>();
            }
            _types.Add(type);
            return this;
        }

        /// <summary>
        /// Bind as type by generic
        /// </summary>
        /// <typeparam name="T">Type to bind</typeparam>
        public IDIComponentBuilder As<T>() {
            return As(typeof(T));
        }

        /// <summary>
        /// Bind type from selected instance
        /// </summary>
        /// <param name="instance">Instance to bind</param>
        public IDIComponentBuilder FromInstance(object instance) {
            Instance = instance;
            Construction = ConstructionMode.FromInstance;
            return this;
        }

        /// <summary>
        /// Use bind as single
        /// All other binds with this type will be ignored
        /// </summary>
        public IDIComponentBuilder AsSingle() {
            Scope = ScopeMode.AsSingle;
            return this;
        }

        /// <summary>
        /// Pass arguments to constructor
        /// </summary>
        /// <param name="list">Arguments</param>
        public IDIComponentBuilder WithArguments(params object[] list) {
            Arguments = list;
            Construction = (ConstructionMode.Default | ConstructionMode.WithArguments);
            return this;
        }

        /// <summary>
        /// Define custom id for bind
        /// </summary>
        /// <param name="id">Custom bind id</param>
        public IDIComponentBuilder WithId(string id) {
            Id = id;
            return this;
        }
    }

}