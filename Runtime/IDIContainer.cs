// ----------------------------------------------------------------------------
// The MIT License
// MoDI. Lightweight IoC container for Unity. https://github.com/mopsicus/modi
// Copyright (c) 2021 Mopsicus <mail@mopsicus.ru>
// ----------------------------------------------------------------------------

using System;
using System.Reflection;

namespace MoDI {

    public interface IDIContainer {
        IDIComponentBuilder BindInstance(object instance);
        IDIComponentBuilder Bind(Type type, object instance = null);
        IDIComponentBuilder Bind<T>();
        object Resolve(FieldInfo field);
        object Resolve(Type type, string id = null);
        T Resolve<T>(string id = null);
        void Clear();
    }

}