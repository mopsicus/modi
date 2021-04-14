// ----------------------------------------------------------------------------
// The MIT License
// MoDI. Lightweight IoC container for Unity. https://github.com/mopsicus/modi
// Copyright (c) 2021 Mopsicus <mail@mopsicus.ru>
// ----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security;
using UnityEngine;

namespace MoDI {

    /// <summary>
    /// Manager to control containers, binds and whatever :)
    /// </summary>
    public class DIManager : MonoBehaviour {

        /// <summary>
        /// Default container capacity
        /// </summary>
        private const int CONTAINER_CAPACITY = 64;

        /// <summary>
        /// Max count of containers
        /// </summary>
        private const int CONTAINERS_MAX_CAPACITY = 16;

        /// <summary>
        /// Flag to check manager initilize
        /// </summary>
        private static bool _isInitilized = false;

        /// <summary>
        /// Injector instance
        /// </summary>
        readonly DIInjector _injector = new DIInjector();

        /// <summary>
        /// Containers list
        /// </summary>
        readonly Dictionary<string, DIContainer> _containers = new Dictionary<string, DIContainer>(CONTAINERS_MAX_CAPACITY);

        /// <summary>
        /// Initialize DI manager
        /// Create default container
        /// </summary>
        public DIManager Initialize() {
            if (_isInitilized) {
                throw new Exception($"{DI.TAG} already initalized");
            }
            DontDestroyOnLoad(gameObject);
            Add(DI.CONTAINER_TAG);
            _isInitilized = true;
            return this;
        }

        /// <summary>
        /// Add new container to manager
        /// </summary>
        /// <param name="tag">Container tag</param>
        /// <param name="capacity">Container capacity</param>
        /// <returns>New container</returns>
        public DIContainer Add(string tag, int capacity = CONTAINER_CAPACITY) {
            if (_containers.ContainsKey(tag)) {
                throw new Exception($"DIContainer with tag '{tag}' already exists");
            }
            DIContainer container = new DIContainer(tag, capacity);
            _containers.Add(tag, container);
            return _containers[tag];
        }

        /// <summary>
        /// Get container for a work by tag
        /// </summary>
        /// <param name="tag">Container tag</param>
        /// <returns>Container for a work</returns>
        public DIContainer Get(string tag) {
            if (!_containers.ContainsKey(tag)) {
                throw new Exception($"DIContainer with tag '{tag}' not found");
            }
            return _containers[tag];
        }

        /// <summary>
        /// Inject objects to fields with attributes
        /// </summary>
        /// <param name="sender">Instance to get fields</param>
        /// <param name="tag">Container tag</param>
        public void Inject(object sender, string tag = DI.CONTAINER_TAG) {
            if (!_containers.ContainsKey(tag)) {
                throw new Exception($"DIContainer with tag '{tag}' not found");
            }
            _injector.Inject(sender, _containers[tag]);
        }

        /// <summary>
        /// Inject for all DIMonoBehaviours on scene
        /// </summary>
        public void ApplyInject() {
            DIMonoBehaviour[] list = FindObjectsOfType<DIMonoBehaviour>();
            foreach (DIMonoBehaviour item in list) {
                item.ApplyInject();
            }
        }

        /// <summary>
        /// ACHTUNG!
        /// Clear all binds and remove all containers
        /// </summary>
        public void Clear() {
            foreach (DIContainer item in _containers.Values) {
                item.Clear();
            }
            _containers.Clear();
        }

    }

}