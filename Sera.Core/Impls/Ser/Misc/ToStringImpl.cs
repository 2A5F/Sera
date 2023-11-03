﻿using System.Runtime.CompilerServices;

namespace Sera.Core.Impls.Ser;

public readonly struct ToStringImpl<T> : ISeraVision<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public R Accept<R, V>(V visitor, T value) where V : ASeraVisitor<R>
        => visitor.VString($"{value}");
}
