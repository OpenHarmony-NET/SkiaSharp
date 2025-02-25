using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace SkiaSharp
{
	public unsafe class SKTraceMemoryDump : SKObject, ISKSkipObjectRegistration
	{
		private static readonly SKManagedTraceMemoryDumpDelegates delegates;
		private readonly IntPtr userData;

		static SKTraceMemoryDump ()
		{
			delegates = new SKManagedTraceMemoryDumpDelegates {
#if !NET6_0
				fDumpNumericValue = DumpNumericValueInternal,
				fDumpStringValue = DumpStringValueInternal,
#else
				fDumpNumericValue = (nint)(delegate* unmanaged[Cdecl]<IntPtr, void*, void*, void*, void*, ulong, void>)&DumpNumericValueInternal,
				fDumpStringValue = (nint)(delegate* unmanaged[Cdecl]<IntPtr, void*, void*, void*, void*, void>)&DumpStringValueInternal,
#endif
			};

			SkiaApi.sk_managedtracememorydump_set_procs (delegates);
		}

		protected SKTraceMemoryDump (bool detailedDump, bool dumpWrappedObjects)
			: base (IntPtr.Zero, true)
		{
			userData = DelegateProxies.CreateUserData (this, true);
			Handle = SkiaApi.sk_managedtracememorydump_new (detailedDump, dumpWrappedObjects, (void*)userData);

			if (Handle == IntPtr.Zero)
				throw new InvalidOperationException ("Unable to create a new SKTraceMemoryDump instance.");
		}

		protected override void DisposeNative ()
		{
			DelegateProxies.GetUserData<SKTraceMemoryDump> (userData, out var gch);

			SkiaApi.sk_managedtracememorydump_delete (Handle);

			gch.Free ();
		}

		protected virtual void OnDumpNumericValue (string dumpName, string valueName, string units, ulong value)
		{
		}

		protected virtual void OnDumpStringValue (string dumpName, string valueName, string value)
		{
		}

		// impl

#if !NET6_0
		[MonoPInvokeCallback (typeof (SKManagedTraceMemoryDumpDumpNumericValueProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static void DumpNumericValueInternal (IntPtr d, void* context, void* dumpName, void* valueName, void* units, ulong value)
		{
			var dump = DelegateProxies.GetUserData<SKTraceMemoryDump> ((IntPtr)context, out _);
			dump.OnDumpNumericValue (
				Marshal.PtrToStringAnsi ((IntPtr)dumpName),
				Marshal.PtrToStringAnsi ((IntPtr)valueName),
				Marshal.PtrToStringAnsi ((IntPtr)units),
				value);
		}

#if !NET6_0
		[MonoPInvokeCallback (typeof (SKManagedTraceMemoryDumpDumpStringValueProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static void DumpStringValueInternal (IntPtr d, void* context, void* dumpName, void* valueName, void* value)
		{
			var dump = DelegateProxies.GetUserData<SKTraceMemoryDump> ((IntPtr)context, out _);
			dump.OnDumpStringValue (
				Marshal.PtrToStringAnsi ((IntPtr)dumpName),
				Marshal.PtrToStringAnsi ((IntPtr)valueName),
				Marshal.PtrToStringAnsi ((IntPtr)value));
		}
	}
}
