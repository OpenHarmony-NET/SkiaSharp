using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Threading;

namespace SkiaSharp
{
	public unsafe abstract class SKAbstractManagedStream : SKStreamAsset
	{
		private static readonly SKManagedStreamDelegates delegates;

		private int fromNative;

		static SKAbstractManagedStream ()
		{
			delegates = new SKManagedStreamDelegates {
#if !NET6_0
				fRead = ReadInternal,
				fPeek = PeekInternal,
				fIsAtEnd = IsAtEndInternal,
				fHasPosition = HasPositionInternal,
				fHasLength = HasLengthInternal,
				fRewind = RewindInternal,
				fGetPosition = GetPositionInternal,
				fSeek = SeekInternal,
				fMove = MoveInternal,
				fGetLength = GetLengthInternal,
				fDuplicate = DuplicateInternal,
				fFork = ForkInternal,
				fDestroy = DestroyInternal,
#else
				fRead = (nint)(delegate* unmanaged[Cdecl]<nint, void*, void*, nint, nint>)&ReadInternal,
				fPeek = (nint)(delegate* unmanaged[Cdecl]<nint, void*, void*, nint, nint>)&PeekInternal,
				fIsAtEnd = (nint)(delegate* unmanaged[Cdecl]<nint, void*, bool>)&IsAtEndInternal,
				fHasPosition = (nint)(delegate* unmanaged[Cdecl]<nint, void*, bool>)&HasPositionInternal,
				fHasLength = (nint)(delegate* unmanaged[Cdecl]<nint, void*, bool>)&HasLengthInternal,
				fRewind = (nint)(delegate* unmanaged[Cdecl]<nint, void*, bool>)&RewindInternal,
				fGetPosition = (nint)(delegate* unmanaged[Cdecl]<nint, void*, nint>)&GetPositionInternal,
				fSeek = (nint)(delegate* unmanaged[Cdecl]<nint, void*, nint, bool>)&SeekInternal,
				fMove = (nint)(delegate* unmanaged[Cdecl]<nint, void*, int, bool>)&MoveInternal,
				fGetLength = (nint)(delegate* unmanaged[Cdecl]<nint, void*, nint>)&GetLengthInternal,
				fDuplicate = (nint)(delegate* unmanaged[Cdecl]<nint, void*, nint>)&DuplicateInternal,
				fFork = (nint)(delegate* unmanaged[Cdecl]<nint, void*, nint>)&ForkInternal,
				fDestroy = (nint)(delegate* unmanaged[Cdecl]<nint, void*, void>)&DestroyInternal
#endif
			};
			SkiaApi.sk_managedstream_set_procs (delegates);
		}

		protected SKAbstractManagedStream ()
			: this (true)
		{
		}

		protected SKAbstractManagedStream (bool owns)
			: base (IntPtr.Zero, owns)
		{
			var ctx = DelegateProxies.CreateUserData (this, true);
			Handle = SkiaApi.sk_managedstream_new ((void*)ctx);
		}

		protected override void Dispose (bool disposing) =>
			base.Dispose (disposing);

		protected override void DisposeNative ()
		{
			if (Interlocked.CompareExchange (ref fromNative, 0, 0) == 0)
				SkiaApi.sk_managedstream_destroy (Handle);
		}

		protected abstract IntPtr OnRead (IntPtr buffer, IntPtr size);

		protected abstract IntPtr OnPeek (IntPtr buffer, IntPtr size);

		protected abstract bool OnIsAtEnd ();

		protected abstract bool OnHasPosition ();

		protected abstract bool OnHasLength ();

		protected abstract bool OnRewind ();

		protected abstract IntPtr OnGetPosition ();

		protected abstract IntPtr OnGetLength ();

		protected abstract bool OnSeek (IntPtr position);

		protected abstract bool OnMove (int offset);

		protected abstract IntPtr OnCreateNew ();

		protected virtual IntPtr OnFork ()
		{
			var stream = OnCreateNew ();
			SkiaApi.sk_stream_seek (stream, SkiaApi.sk_stream_get_position (Handle));
			return stream;
		}

		protected virtual IntPtr OnDuplicate () => OnCreateNew ();

#if !NET6_0
		[MonoPInvokeCallback (typeof (SKManagedStreamReadProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static IntPtr ReadInternal (IntPtr s, void* context, void* buffer, IntPtr size)
		{
			var stream = DelegateProxies.GetUserData<SKAbstractManagedStream> ((IntPtr)context, out _);
			return stream.OnRead ((IntPtr)buffer, size);
		}

#if !NET6_0
		[MonoPInvokeCallback (typeof (SKManagedStreamPeekProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static IntPtr PeekInternal (IntPtr s, void* context, void* buffer, IntPtr size)
		{
			var stream = DelegateProxies.GetUserData<SKAbstractManagedStream> ((IntPtr)context, out _);
			return stream.OnPeek ((IntPtr)buffer, size);
		}

#if !NET6_0
		[MonoPInvokeCallback (typeof (SKManagedStreamIsAtEndProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static bool IsAtEndInternal (IntPtr s, void* context)
		{
			var stream = DelegateProxies.GetUserData<SKAbstractManagedStream> ((IntPtr)context, out _);
			return stream.OnIsAtEnd ();
		}

#if !NET6_0
		[MonoPInvokeCallback (typeof (SKManagedStreamHasPositionProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static bool HasPositionInternal (IntPtr s, void* context)
		{
			var stream = DelegateProxies.GetUserData<SKAbstractManagedStream> ((IntPtr)context, out _);
			return stream.OnHasPosition ();
		}

#if !NET6_0
		[MonoPInvokeCallback (typeof (SKManagedStreamHasLengthProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static bool HasLengthInternal (IntPtr s, void* context)
		{
			var stream = DelegateProxies.GetUserData<SKAbstractManagedStream> ((IntPtr)context, out _);
			return stream.OnHasLength ();
		}

#if !NET6_0
		[MonoPInvokeCallback (typeof (SKManagedStreamRewindProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static bool RewindInternal (IntPtr s, void* context)
		{
			var stream = DelegateProxies.GetUserData<SKAbstractManagedStream> ((IntPtr)context, out _);
			return stream.OnRewind ();
		}

#if !NET6_0
		[MonoPInvokeCallback (typeof (SKManagedStreamGetPositionProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static IntPtr GetPositionInternal (IntPtr s, void* context)
		{
			var stream = DelegateProxies.GetUserData<SKAbstractManagedStream> ((IntPtr)context, out _);
			return stream.OnGetPosition ();
		}

#if !NET6_0
		[MonoPInvokeCallback (typeof (SKManagedStreamSeekProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static bool SeekInternal (IntPtr s, void* context, IntPtr position)
		{
			var stream = DelegateProxies.GetUserData<SKAbstractManagedStream> ((IntPtr)context, out _);
			return stream.OnSeek (position);
		}

#if !NET6_0
		[MonoPInvokeCallback (typeof (SKManagedStreamMoveProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static bool MoveInternal (IntPtr s, void* context, int offset)
		{
			var stream = DelegateProxies.GetUserData<SKAbstractManagedStream> ((IntPtr)context, out _);
			return stream.OnMove (offset);
		}

#if !NET6_0
		[MonoPInvokeCallback (typeof (SKManagedStreamGetLengthProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static IntPtr GetLengthInternal (IntPtr s, void* context)
		{
			var stream = DelegateProxies.GetUserData<SKAbstractManagedStream> ((IntPtr)context, out _);
			return stream.OnGetLength ();
		}

#if !NET6_0
		[MonoPInvokeCallback (typeof (SKManagedStreamDuplicateProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static IntPtr DuplicateInternal (IntPtr s, void* context)
		{
			var stream = DelegateProxies.GetUserData<SKAbstractManagedStream> ((IntPtr)context, out _);
			return stream.OnDuplicate ();
		}

#if !NET6_0
		[MonoPInvokeCallback (typeof (SKManagedStreamForkProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static IntPtr ForkInternal (IntPtr s, void* context)
		{
			var stream = DelegateProxies.GetUserData<SKAbstractManagedStream> ((IntPtr)context, out _);
			return stream.OnFork ();
		}

#if !NET6_0
		[MonoPInvokeCallback (typeof (SKManagedStreamDestroyProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static void DestroyInternal (IntPtr s, void* context)
		{
			var stream = DelegateProxies.GetUserData<SKAbstractManagedStream> ((IntPtr)context, out var gch);
			if (stream != null) {
				Interlocked.Exchange (ref stream.fromNative, 1);
				stream.Dispose ();
			}
			gch.Free ();
		}
	}
}
