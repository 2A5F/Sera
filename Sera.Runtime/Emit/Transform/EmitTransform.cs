﻿using System;
using Sera.Core.Impls;

namespace Sera.Runtime.Emit.Transform;

internal abstract record EmitTransform
{
    public static EmitTransform[] EmptyTransforms { get; } = Array.Empty<EmitTransform>();
    
    public abstract Type TransformType(EmitMeta target, Type prevType);
    public abstract object TransformInst(EmitMeta target, Type type, Type prevType, object prevInst);
}

internal record EmitTransformReferenceTypeWrapperSerializeImpl : EmitTransform
{
    public override Type TransformType(EmitMeta target, Type prevType)
    {
        return typeof(ReferenceTypeWrapperSerializeImpl<,>).MakeGenericType(target.Type, prevType);
    }

    public override object TransformInst(EmitMeta target, Type type, Type prevType, object prevInst)
    {
        var ctor = type.GetConstructor(new[] { prevType })!;
        return ctor.Invoke(new[] { prevInst });
    }
}

internal record EmitTransformNullableReferenceTypeImpl : EmitTransform
{
    public override Type TransformType(EmitMeta target, Type prevType)
    {
        return typeof(NullableReferenceTypeImpl<,>).MakeGenericType(target.Type, prevType);
    }

    public override object TransformInst(EmitMeta target, Type type, Type prevType, object prevInst)
    {
        var ctor = type.GetConstructor(new[] { prevType })!;
        return ctor.Invoke(new[] { prevInst });
    }
}
