using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace HarfBuzzSharp
{
	public delegate UnicodeCombiningClass CombiningClassDelegate (UnicodeFunctions ufuncs, uint unicode);

	public delegate UnicodeGeneralCategory GeneralCategoryDelegate (UnicodeFunctions ufuncs, uint unicode);

	public delegate uint MirroringDelegate (UnicodeFunctions ufuncs, uint unicode);

	public delegate Script ScriptDelegate (UnicodeFunctions ufuncs, uint unicode);

	public delegate bool ComposeDelegate (UnicodeFunctions ufuncs, uint a, uint b, out uint ab);

	public delegate bool DecomposeDelegate (UnicodeFunctions ufuncs, uint ab, out uint a, out uint b);

	internal static unsafe partial class DelegateProxies
	{
#if !NET6_0
		public static readonly UnicodeCombiningClassProxyDelegate CombiningClassProxy = CombiningClassProxyImplementation;
		public static readonly UnicodeGeneralCategoryProxyDelegate GeneralCategoryProxy = GeneralCategoryProxyImplementation;
		public static readonly UnicodeMirroringProxyDelegate MirroringProxy = MirroringProxyImplementation;
		public static readonly UnicodeScriptProxyDelegate ScriptProxy = ScriptProxyImplementation;
		public static readonly UnicodeComposeProxyDelegate ComposeProxy = ComposeProxyImplementation;
		public static readonly UnicodeDecomposeProxyDelegate DecomposeProxy = DecomposeProxyImplementation;
#else
		public static readonly nint CombiningClassProxy = (nint)(delegate* unmanaged[Cdecl]<nint, uint, void*, int>)&CombiningClassProxyImplementation;
		public static readonly nint GeneralCategoryProxy = (nint)(delegate* unmanaged[Cdecl]<nint, uint, void*, int>)&GeneralCategoryProxyImplementation;
		public static readonly nint MirroringProxy = (nint)(delegate* unmanaged[Cdecl]<nint, uint, void*, uint>)&MirroringProxyImplementation;
		public static readonly nint ScriptProxy = (nint)(delegate* unmanaged[Cdecl]<nint, uint, void*, uint>)&ScriptProxyImplementation;
		public static readonly nint ComposeProxy = (nint)(delegate* unmanaged[Cdecl]<nint, uint, uint, uint*, void*, bool>)&ComposeProxyImplementation;
		public static readonly nint DecomposeProxy = (nint)(delegate* unmanaged[Cdecl]<nint, uint, uint*, uint*, void*, bool>)&DecomposeProxyImplementation;
#endif

#if !NET6_0
		[MonoPInvokeCallback (typeof (UnicodeCombiningClassProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static int CombiningClassProxyImplementation (IntPtr ufuncs, uint unicode, void* context)
		{
			GetMultiUserData<CombiningClassDelegate, UnicodeFunctions> ((IntPtr)context, out var del, out var functions, out _);
			return (int)del.Invoke (functions, unicode);
		}

#if !NET6_0
		[MonoPInvokeCallback (typeof (UnicodeGeneralCategoryProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static int GeneralCategoryProxyImplementation (IntPtr ufuncs, uint unicode, void* context)
		{
			GetMultiUserData<GeneralCategoryDelegate, UnicodeFunctions> ((IntPtr)context, out var del, out var functions, out _);
			return (int)del.Invoke (functions, unicode);
		}

#if !NET6_0
		[MonoPInvokeCallback (typeof (UnicodeMirroringProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static uint MirroringProxyImplementation (IntPtr ufuncs, uint unicode, void* context)
		{
			GetMultiUserData<MirroringDelegate, UnicodeFunctions> ((IntPtr)context, out var del, out var functions, out _);
			return del.Invoke (functions, unicode);
		}

#if !NET6_0
		[MonoPInvokeCallback (typeof (UnicodeScriptProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static uint ScriptProxyImplementation (IntPtr ufuncs, uint unicode, void* context)
		{
			GetMultiUserData<ScriptDelegate, UnicodeFunctions> ((IntPtr)context, out var del, out var functions, out _);
			return del.Invoke (functions, unicode);
		}

#if !NET6_0
		[MonoPInvokeCallback (typeof (UnicodeComposeProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static bool ComposeProxyImplementation (IntPtr ufuncs, uint a, uint b, uint* ab, void* context)
		{
			GetMultiUserData<ComposeDelegate, UnicodeFunctions> ((IntPtr)context, out var del, out var functions, out _);
			var result = del.Invoke (functions, a, b, out var abManaged);
			if (ab != null)
				*ab = abManaged;
			return result;
		}

#if !NET6_0
		[MonoPInvokeCallback (typeof (UnicodeDecomposeProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static bool DecomposeProxyImplementation (IntPtr ufuncs, uint ab, uint* a, uint* b, void* context)
		{
			GetMultiUserData<DecomposeDelegate, UnicodeFunctions> ((IntPtr)context, out var del, out var functions, out _);
			var result = del.Invoke (functions, ab, out var aManaged, out var bManaged);
			if (a != null)
				*a = aManaged;
			if (b != null)
				*b = bManaged;
			return result;
		}
	}
}
