// ----------------------------------------------------------------------------
// The MIT License
// MoDI. Lightweight IoC container for Unity. https://github.com/mopsicus/modi
// Copyright (c) 2021 Mopsicus <mail@mopsicus.ru>
// ----------------------------------------------------------------------------

using UnityEngine;

namespace MoDI {

    public static class DI {

        /// <summary>
        /// Tag for manager gameobject
        /// </summary>
        public const string TAG = "MoDI";

        /// <summary>
        /// Tag for default container name
        /// </summary>
        public const string CONTAINER_TAG = "default";

        /// <summary>
        /// Link to manager instance
        /// </summary>
        private static DIManager _manager = null;

        /// <summary>
        /// Initialize DIManager and return link
        /// </summary>
        public static DIManager Manager {
            get {
                if (_manager == null) {
                    _manager = new GameObject(TAG).AddComponent<DIManager>().Initialize();
                }
                return _manager;
            }
        }

        /// <summary>
        /// Get container for bind or resolve
        /// </summary>
        /// <param name="tag">Container tag</param>
        /// <returns>DIContainer for a work</returns>
        public static DIContainer Get(string tag = CONTAINER_TAG) {
            return Manager.Get(tag);
        }

    }

}