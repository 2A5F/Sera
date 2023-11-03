﻿namespace Sera

open System
open System.IO
open System.Runtime.CompilerServices
open System.Text
open Microsoft.FSharp.Core
open Sera.Core
open Sera.Core.Builders
open Sera.Core.Builders.Outputs
open Sera.FSharp.Builders
open Sera.Runtime

module RuntimeStaticSerFs =
    [<NoCompilerInlining>]
    let SerOutputToStream<'b, 'a, 'i when 'b :> ISerBuilder and 'b :> IStreamOutput and 'i :> ISeraVision<'a>>
        (b: 'b)
        (v: 'a)
        (i: 'i)
        stream
        =
        let a = SeraBuilderForStaticSer.StaticAccept(v, i)
        b.BuildStreamOutput(a, SeraBuilderForRuntimeSer.RuntimeOutputBuildParam, stream)

    [<NoCompilerInlining>]
    let SerOutputToString<'b, 'a, 'i when 'b :> ISerBuilder and 'b :> IStringBuilderOutput and 'i :> ISeraVision<'a>>
        (b: 'b)
        (v: 'a)
        (i: 'i)
        sb
        =
        let a = SeraBuilderForStaticSer.StaticAccept(v, i)
        b.BuildStringBuilderOutput(a, SeraBuilderForRuntimeSer.RuntimeOutputBuildParam, sb)

    [<NoCompilerInlining>]
    let SerOutputToStreamAsync<'b, 'a, 'i when 'b :> ISerBuilder and 'b :> IAsyncStreamOutput and 'i :> ISeraVision<'a>>
        (b: 'b)
        (v: 'a)
        (i: 'i)
        stream
        =
        let a = SeraBuilderForStaticSer.AsyncStaticAccept(v, i)
        b.BuildAsyncStreamOutput(a, SeraBuilderForRuntimeSer.RuntimeOutputBuildParam, stream)

module RuntimeSerFs =
    [<NoCompilerInlining>]
    let SerOutputToStream<'b, 'a when 'b :> ISerBuilder and 'b :> IStreamOutput>
        (b: 'b)
        (v: 'a)
        (styles: SeraStyles Nullable)
        stream
        =
        let a = SeraBuilderForRuntimeSer.RuntimeAccept(v, styles)
        b.BuildStreamOutput(a, SeraBuilderForRuntimeSer.RuntimeOutputBuildParam, stream)

    [<NoCompilerInlining>]
    let SerOutputToString<'b, 'a when 'b :> ISerBuilder and 'b :> IStringBuilderOutput>
        (b: 'b)
        (v: 'a)
        (styles: SeraStyles Nullable)
        sb
        =
        let a = SeraBuilderForRuntimeSer.RuntimeAccept(v, styles)
        b.BuildStringBuilderOutput(a, SeraBuilderForRuntimeSer.RuntimeOutputBuildParam, sb)

    [<NoCompilerInlining>]
    let SerOutputToStreamAsync<'b, 'a when 'b :> ISerBuilder and 'b :> IAsyncStreamOutput>
        (b: 'b)
        (v: 'a)
        (styles: SeraStyles Nullable)
        stream
        =
        let a = SeraBuilderForRuntimeSer.AsyncRuntimeAccept(v, styles)
        b.BuildAsyncStreamOutput(a, SeraBuilderForRuntimeSer.RuntimeOutputBuildParam, stream)

[<Extension; Sealed; AbstractClass>]
type SeraBuilderForRuntimeStaticSerFs =
    [<Extension; CustomOperation("ToStream")>]
    static member inline OutputToStream<'s, 'b, 'a, 'i
        when 's :> ISerBuilder and 'b :> ISerBuilder and 'b :> IStreamOutput and 'i :> ISeraVision<'a>>
        (
            __: 's,
            (b, v, i, _): 'b * 'a * 'i * FsUse,
            stream: Stream
        ) =
        RuntimeStaticSerFs.SerOutputToStream b v i stream

    [<Extension; CustomOperation("ToString")>]
    static member inline OutputToString<'s, 'b, 'a, 'i
        when 's :> ISerBuilder and 'b :> ISerBuilder and 'b :> IStringBuilderOutput and 'i :> ISeraVision<'a>>
        (
            __: 's,
            (b, v, i, _): 'b * 'a * 'i * FsUse,
            sb: StringBuilder
        ) =
        RuntimeStaticSerFs.SerOutputToString b v i sb

    [<Extension; CustomOperation("ToString")>]
    static member inline OutputToString(__: #ISerBuilder, b: 'b * 'a * 'i * FsUse) =
        let sb = StringBuilder()
        __.OutputToString(b, sb) |> ignore
        sb.ToString()

    [<Extension; CustomOperation("ToStreamAsync")>]
    static member inline OutputToStreamAsync<'s, 'b, 'a, 'i
        when 's :> ISerBuilder and 'b :> ISerBuilder and 'b :> IAsyncStreamOutput and 'i :> ISeraVision<'a>>
        (
            __: 's,
            (b, v, i, _): 'b * 'a * 'i * FsUse,
            stream: Stream
        ) =
        RuntimeStaticSerFs.SerOutputToStreamAsync b v i stream

[<Extension; Sealed; AbstractClass>]
type SeraBuilderForRuntimeSerFs =
    [<Extension; CustomOperation("Styles")>]
    static member inline Styles(__: #ISerBuilder, (b, v): #ISerBuilder * 'a, styles: SeraStyles) = (b, v, styles)

    [<Extension; CustomOperation("ToStream")>]
    static member inline OutputToStream<'s, 'b, 'a, 'i
        when 's :> ISerBuilder and 'b :> ISerBuilder and 'b :> IStreamOutput and 'i :> ISeraVision<'a>>
        (
            __: 's,
            (b, v, styles): 'b * 'a * SeraStyles,
            stream: Stream
        ) =
        RuntimeSerFs.SerOutputToStream b v (Nullable styles) stream

    [<Extension; CustomOperation("ToString")>]
    static member inline OutputToString<'s, 'b, 'a, 'i
        when 's :> ISerBuilder and 'b :> ISerBuilder and 'b :> IStringBuilderOutput and 'i :> ISeraVision<'a>>
        (
            __: 's,
            (b, v, styles): 'b * 'a * SeraStyles,
            sb: StringBuilder
        ) =
        RuntimeSerFs.SerOutputToString b v (Nullable styles) sb

    [<Extension; CustomOperation("ToString")>]
    static member inline OutputToString(__: #ISerBuilder, b: 'b * 'a * SeraStyles) =
        let sb = StringBuilder()
        __.OutputToString(b, sb) |> ignore
        sb.ToString()

    [<Extension; CustomOperation("ToStreamAsync")>]
    static member inline OutputToStreamAsync<'s, 'b, 'a, 'i
        when 's :> ISerBuilder and 'b :> ISerBuilder and 'b :> IAsyncStreamOutput and 'i :> ISeraVision<'a>>
        (
            __: 's,
            (b, v, styles): 'b * 'a * SeraStyles,
            stream: Stream
        ) =
        RuntimeSerFs.SerOutputToStreamAsync b v (Nullable styles) stream
