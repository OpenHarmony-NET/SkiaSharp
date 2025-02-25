using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;


namespace HarfBuzzSharp
{
	public delegate void ReleaseDelegate ();

	public delegate Blob GetTableDelegate (Face face, Tag tag);

	[EditorBrowsable (EditorBrowsableState.Never)]
	[Obsolete ("Use ReleaseDelegate instead.")]
	public delegate void BlobReleaseDelegate (object context);

	internal static unsafe partial class DelegateProxies
	{
		// references to the proxy implementations
#if !NET6_0
		public static readonly DestroyProxyDelegate ReleaseDelegateProxy = ReleaseDelegateProxyImplementation;
		public static readonly DestroyProxyDelegate ReleaseDelegateProxyForMulti = ReleaseDelegateProxyImplementationForMulti;
		public static readonly ReferenceTableProxyDelegate GetTableDelegateProxy = GetTableDelegateProxyImplementation;
#else
		public static readonly nint ReleaseDelegateProxy = (nint)(delegate* unmanaged[Cdecl]<void*, void>)&ReleaseDelegateProxyImplementation;
		public static readonly nint ReleaseDelegateProxyForMulti = (nint)(delegate* unmanaged[Cdecl]<void*, void>)&ReleaseDelegateProxyImplementationForMulti;
		public static readonly nint GetTableDelegateProxy = (nint)(delegate* unmanaged[Cdecl]<nint, uint, void*, nint>)&GetTableDelegateProxyImplementation;
#endif

		// internal proxy implementations
#if !NET6_0
		[MonoPInvokeCallback (typeof (DestroyProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static void ReleaseDelegateProxyImplementation (void* context)
		{
			var del = Get<ReleaseDelegate> ((IntPtr)context, out var gch);
			try {
				del.Invoke ();
			} finally {
				gch.Free ();
			}
		}

#if !NET6_0
		[MonoPInvokeCallback (typeof (ReferenceTableProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static IntPtr GetTableDelegateProxyImplementation (IntPtr face, uint tag, void* context)
		{
			GetMultiUserData<GetTableDelegate, Face> ((IntPtr)context, out var getTable, out var userData, out _);
			var blob = getTable.Invoke (userData, tag);
			return blob?.Handle ?? IntPtr.Zero;
		}

#if !NET6_0
		[MonoPInvokeCallback (typeof (DestroyProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static void ReleaseDelegateProxyImplementationForMulti (void* context)
		{
			var del = GetMulti<ReleaseDelegate> ((IntPtr)context, out var gch);
			try {
				del?.Invoke ();
			} finally {
				gch.Free ();
			}
		}
	}
}
