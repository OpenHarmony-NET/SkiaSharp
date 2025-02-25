using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace SkiaSharp
{
	// public delegates

	public delegate void SKBitmapReleaseDelegate (IntPtr address, object context);

	public delegate void SKDataReleaseDelegate (IntPtr address, object context);

	public delegate void SKImageRasterReleaseDelegate (IntPtr pixels, object context);

	public delegate void SKImageTextureReleaseDelegate (object context);

	public delegate void SKSurfaceReleaseDelegate (IntPtr address, object context);

	[EditorBrowsable (EditorBrowsableState.Never)]
	[Obsolete ("Use GRGlGetProcedureAddressDelegate instead.")]
	public delegate IntPtr GRGlGetProcDelegate (object context, string name);

	public delegate IntPtr GRGlGetProcedureAddressDelegate (string name);

	public delegate IntPtr GRVkGetProcedureAddressDelegate (string name, IntPtr instance, IntPtr device);

	public delegate void SKGlyphPathDelegate (SKPath path, SKMatrix matrix);

	internal unsafe static partial class DelegateProxies
	{
#if !NET6_0
		// references to the proxy implementations
		public static readonly SKBitmapReleaseProxyDelegate SKBitmapReleaseDelegateProxy = SKBitmapReleaseDelegateProxyImplementation;
		public static readonly SKDataReleaseProxyDelegate SKDataReleaseDelegateProxy = SKDataReleaseDelegateProxyImplementation;
		public static readonly SKImageRasterReleaseProxyDelegate SKImageRasterReleaseDelegateProxy = SKImageRasterReleaseDelegateProxyImplementation;
		public static readonly SKImageRasterReleaseProxyDelegate SKImageRasterReleaseDelegateProxyForCoTaskMem = SKImageRasterReleaseDelegateProxyImplementationForCoTaskMem;
		public static readonly SKImageTextureReleaseProxyDelegate SKImageTextureReleaseDelegateProxy = SKImageTextureReleaseDelegateProxyImplementation;
		public static readonly SKSurfaceRasterReleaseProxyDelegate SKSurfaceReleaseDelegateProxy = SKSurfaceReleaseDelegateProxyImplementation;
		public static readonly GRGlGetProcProxyDelegate GRGlGetProcDelegateProxy = GRGlGetProcDelegateProxyImplementation;
		public static readonly GRVkGetProcProxyDelegate GRVkGetProcDelegateProxy = GRVkGetProcDelegateProxyImplementation;
		public static readonly SKGlyphPathProxyDelegate SKGlyphPathDelegateProxy = SKGlyphPathDelegateProxyImplementation;
#else
		public static readonly nint SKBitmapReleaseDelegateProxy = (nint)(delegate* unmanaged[Cdecl]<void*, void*, void>)&SKBitmapReleaseDelegateProxyImplementation;
		public static readonly nint SKDataReleaseDelegateProxy = (nint)(delegate* unmanaged[Cdecl]<void*, void*, void>)&SKDataReleaseDelegateProxyImplementation;
		public static readonly nint SKImageRasterReleaseDelegateProxy = (nint)(delegate* unmanaged[Cdecl]<void*, void*, void>)&SKImageRasterReleaseDelegateProxyImplementation;
		public static readonly nint SKImageRasterReleaseDelegateProxyForCoTaskMem = (nint)(delegate* unmanaged[Cdecl]<void*, void*, void>)&SKImageRasterReleaseDelegateProxyImplementationForCoTaskMem;
		public static readonly nint SKImageTextureReleaseDelegateProxy = (nint)(delegate* unmanaged[Cdecl]<void*, void>)&SKImageTextureReleaseDelegateProxyImplementation;
		public static readonly nint SKSurfaceReleaseDelegateProxy = (nint)(delegate* unmanaged[Cdecl]<void*, void*, void>)&SKSurfaceReleaseDelegateProxyImplementation;
		public static readonly nint GRGlGetProcDelegateProxy = (nint)(delegate* unmanaged[Cdecl]<void*, void*, nint>)&GRGlGetProcDelegateProxyImplementation;
		public static readonly nint GRVkGetProcDelegateProxy = (nint)(delegate* unmanaged[Cdecl]<void*, void* , nint, nint, nint>)&GRVkGetProcDelegateProxyImplementation;
		public static readonly nint SKGlyphPathDelegateProxy = (nint)(delegate* unmanaged[Cdecl]<nint, SKMatrix*, void*, void>)&SKGlyphPathDelegateProxyImplementation;
#endif
		// internal proxy implementations

#if !NET6_0
		[MonoPInvokeCallback (typeof (SKBitmapReleaseProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static void SKBitmapReleaseDelegateProxyImplementation (void* address, void* context)
		{
			var del = Get<SKBitmapReleaseDelegate> ((IntPtr)context, out var gch);
			try {
				del.Invoke ((IntPtr)address, null);
			} finally {
				gch.Free ();
			}
		}

#if !NET6_0
		[MonoPInvokeCallback (typeof (SKDataReleaseProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static void SKDataReleaseDelegateProxyImplementation (void* address, void* context)
		{
			var del = Get<SKDataReleaseDelegate> ((IntPtr)context, out var gch);
			try {
				del.Invoke ((IntPtr)address, null);
			} finally {
				gch.Free ();
			}
		}

#if !NET6_0
		[MonoPInvokeCallback (typeof (SKImageRasterReleaseProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static void SKImageRasterReleaseDelegateProxyImplementationForCoTaskMem (void* pixels, void* context)
		{
			Marshal.FreeCoTaskMem ((IntPtr)pixels);
		}

#if !NET6_0
		[MonoPInvokeCallback (typeof (SKImageRasterReleaseProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static void SKImageRasterReleaseDelegateProxyImplementation (void* pixels, void* context)
		{
			var del = Get<SKImageRasterReleaseDelegate> ((IntPtr)context, out var gch);
			try {
				del.Invoke ((IntPtr)pixels, null);
			} finally {
				gch.Free ();
			}
		}

#if !NET6_0
		[MonoPInvokeCallback (typeof (SKImageTextureReleaseProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static void SKImageTextureReleaseDelegateProxyImplementation (void* context)
		{
			var del = Get<SKImageTextureReleaseDelegate> ((IntPtr)context, out var gch);
			try {
				del.Invoke (null);
			} finally {
				gch.Free ();
			}
		}

#if !NET6_0
		[MonoPInvokeCallback (typeof (SKSurfaceRasterReleaseProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static void SKSurfaceReleaseDelegateProxyImplementation (void* address, void* context)
		{
			var del = Get<SKSurfaceReleaseDelegate> ((IntPtr)context, out var gch);
			try {
				del.Invoke ((IntPtr)address, null);
			} finally {
				gch.Free ();
			}
		}

#if !NET6_0
		[MonoPInvokeCallback (typeof (GRGlGetProcProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static IntPtr GRGlGetProcDelegateProxyImplementation (void* context, void* name)
		{
			var del = Get<GRGlGetProcedureAddressDelegate> ((IntPtr)context, out _);
			return del.Invoke (Marshal.PtrToStringAnsi ((IntPtr)name));
		}

#if !NET6_0
		[MonoPInvokeCallback (typeof (GRVkGetProcProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static IntPtr GRVkGetProcDelegateProxyImplementation (void* context, void* name, IntPtr instance, IntPtr device)
		{
			var del = Get<GRVkGetProcedureAddressDelegate> ((IntPtr)context, out _);

			return del.Invoke (Marshal.PtrToStringAnsi ((IntPtr)name), instance, device);
		}

#if !NET6_0
		[MonoPInvokeCallback (typeof (SKGlyphPathProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static void SKGlyphPathDelegateProxyImplementation (IntPtr pathOrNull, SKMatrix* matrix, void* context)
		{
			var del = Get<SKGlyphPathDelegate> ((IntPtr)context, out _);
			var path = SKPath.GetObject (pathOrNull, false);
			del.Invoke (path, *matrix);
		}
	}
}
