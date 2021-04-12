// ----------------------------------------------------------------------------
// The MIT License
// MoDI. Lightweight IoC container for Unity. https://github.com/mopsicus/modi
// Copyright (c) 2021 Mopsicus <mail@mopsicus.ru>
// ----------------------------------------------------------------------------

using UnityEngine;

namespace MoDI {

    /// <summary>
    /// Helper class for fields injection
    /// </summary>
    public class DIMonoBehaviour : MonoBehaviour {

        /// <summary>
        /// Resolve objects to fields
        /// </summary>
        public void ApplyInject() {
            DI.Manager.Inject(this);
        }

    }

}