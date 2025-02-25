using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Threading;

namespace SkiaSharp
{
	public unsafe abstract class SKAbstractManagedWStream : SKWStream
	{
		private static readonly SKManagedWStreamDelegates delegates;

		private int fromNative;

		static SKAbstractManagedWStream ()
		{
			delegates = new SKManagedWStreamDelegates {
#if !NET6_0
				fWrite = WriteInternal,
				fFlush = FlushInternal,
				fBytesWritten = BytesWrittenInternal,
				fDestroy = DestroyInternal,
#else
				fWrite = (nint)(delegate* unmanaged[Cdecl]<nint, void*, void*, nint, bool>)&WriteInternal,
				fFlush = (nint)(delegate* unmanaged[Cdecl]<nint, void*, void>)&FlushInternal,
				fBytesWritten = (nint)(delegate* unmanaged[Cdecl]<nint, void*, nint>)&BytesWrittenInternal,
				fDestroy = (nint)(delegate* unmanaged[Cdecl]<nint, void*, void>)&DestroyInternal,
#endif
			};

			SkiaApi.sk_managedwstream_set_procs (delegates);
		}

		protected SKAbstractManagedWStream ()
			: this (true)
		{
		}

		protected SKAbstractManagedWStream (bool owns)
			: base (IntPtr.Zero, owns)
		{
			var ctx = DelegateProxies.CreateUserData (this, true);
			Handle = SkiaApi.sk_managedwstream_new ((void*)ctx);
		}

		protected override void Dispose (bool disposing) =>
			base.Dispose (disposing);

		protected override void DisposeNative ()
		{
			if (Interlocked.CompareExchange (ref fromNative, 0, 0) == 0)
				SkiaApi.sk_managedwstream_destroy (Handle);
		}

		protected abstract bool OnWrite (IntPtr buffer, IntPtr size);

		protected abstract void OnFlush ();

		protected abstract IntPtr OnBytesWritten ();

#if !NET6_0
		[MonoPInvokeCallback (typeof (SKManagedWStreamWriteProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static bool WriteInternal (IntPtr s, void* context, void* buffer, IntPtr size)
		{
			var stream = DelegateProxies.GetUserData<SKAbstractManagedWStream> ((IntPtr)context, out _);
			return stream.OnWrite ((IntPtr)buffer, size);
		}

#if !NET6_0
		[MonoPInvokeCallback (typeof (SKManagedWStreamFlushProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static void FlushInternal (IntPtr s, void* context)
		{
			var stream = DelegateProxies.GetUserData<SKAbstractManagedWStream> ((IntPtr)context, out _);
			stream.OnFlush ();
		}

#if !NET6_0
		[MonoPInvokeCallback (typeof (SKManagedWStreamBytesWrittenProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static IntPtr BytesWrittenInternal (IntPtr s, void* context)
		{
			var stream = DelegateProxies.GetUserData<SKAbstractManagedWStream> ((IntPtr)context, out _);
			return stream.OnBytesWritten ();
		}

#if !NET6_0
		[MonoPInvokeCallback (typeof (SKManagedWStreamDestroyProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static void DestroyInternal (IntPtr s, void* context)
		{
			var stream = DelegateProxies.GetUserData<SKAbstractManagedWStream> ((IntPtr)context, out var gch);
			if (stream != null) {
				Interlocked.Exchange (ref stream.fromNative, 1);
				stream.Dispose ();
			}
			gch.Free ();
		}
	}
}
