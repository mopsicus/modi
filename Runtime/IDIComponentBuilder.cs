// ----------------------------------------------------------------------------
// The MIT License
// MoDI. Lightweight IoC container for Unity. https://github.com/mopsicus/modi
// Copyright (c) 2021 Mopsicus <mail@mopsicus.ru>
// ----------------------------------------------------------------------------

using System;

namespace MoDI {

    public interface IDIComponentBuilder {
        IDIComponentBuilder As(Type type);
        IDIComponentBuilder As<T>();
        IDIComponentBuilder FromInstance(object instance);
        IDIComponentBuilder AsSingle();
        IDIComponentBuilder WithArguments(params object[] list);
        IDIComponentBuilder WithId(string id);
    }

}