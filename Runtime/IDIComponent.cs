// ----------------------------------------------------------------------------
// The MIT License
// MoDI. Lightweight IoC container for Unity. https://github.com/mopsicus/modi
// Copyright (c) 2021 Mopsicus <mail@mopsicus.ru>
// ----------------------------------------------------------------------------

using System;

namespace MoDI {

    [Flags]
    public enum ConstructionMode {
        Default = 1,
        FromInstance = 2,
        WithArguments = 4
    }

    public enum ScopeMode {
        Default,
        AsSingle
    }

    public interface IDIComponent {
        string Id { get; }
        ScopeMode Scope { get; }
        ConstructionMode Construction { get; }
        object[] Arguments { get; }
        object Instance { get; }
        Type Type { get; }
        bool ContainsType(Type type);
    }

}