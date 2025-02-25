using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace HarfBuzzSharp
{
	public delegate bool FontExtentsDelegate (Font font, object fontData, out FontExtents extents);

	public delegate bool NominalGlyphDelegate (Font font, object fontData, uint unicode, out uint glyph);

	public delegate uint NominalGlyphsDelegate (Font font, object fontData, uint count, ReadOnlySpan<uint> codepoints, Span<uint> glyphs);

	public delegate bool VariationGlyphDelegate (Font font, object fontData, uint unicode, uint variationSelector, out uint glyph);

	public delegate int GlyphAdvanceDelegate (Font font, object fontData, uint glyph);

	public delegate void GlyphAdvancesDelegate (Font font, object fontData, uint count, ReadOnlySpan<uint> glyphs, Span<int> advances);

	public delegate bool GlyphOriginDelegate (Font font, object fontData, uint glyph, out int x, out int y);

	public delegate int GlyphKerningDelegate (Font font, object fontData, uint firstGlyph, uint secondGlyph);

	public delegate bool GlyphExtentsDelegate (Font font, object fontData, uint glyph, out GlyphExtents extents);

	public delegate bool GlyphContourPointDelegate (Font font, object fontData, uint glyph, uint pointIndex, out int x, out int y);

	public delegate bool GlyphNameDelegate (Font font, object fontData, uint glyph, out string name);

	public delegate bool GlyphFromNameDelegate (Font font, object fontData, string name, out uint glyph);

	internal static unsafe partial class DelegateProxies
	{
		// references to the proxy implementations
#if !NET6_0
		public static readonly FontGetFontExtentsProxyDelegate FontExtentsProxy = FontExtentsProxyImplementation;
		public static readonly FontGetNominalGlyphProxyDelegate NominalGlyphProxy = NominalGlyphProxyImplementation;
		public static readonly FontGetVariationGlyphProxyDelegate VariationGlyphProxy = VariationGlyphProxyImplementation;
		public static readonly FontGetNominalGlyphsProxyDelegate NominalGlyphsProxy = NominalGlyphsProxyImplementation;
		public static readonly FontGetGlyphAdvanceProxyDelegate GlyphAdvanceProxy = GlyphAdvanceProxyImplementation;
		public static readonly FontGetGlyphAdvancesProxyDelegate GlyphAdvancesProxy = GlyphAdvancesProxyImplementation;
		public static readonly FontGetGlyphOriginProxyDelegate GlyphOriginProxy = GlyphOriginProxyImplementation;
		public static readonly FontGetGlyphKerningProxyDelegate GlyphKerningProxy = GlyphKerningProxyImplementation;
		public static readonly FontGetGlyphExtentsProxyDelegate GlyphExtentsProxy = GlyphExtentsProxyImplementation;
		public static readonly FontGetGlyphContourPointProxyDelegate GlyphContourPointProxy = GlyphContourPointProxyImplementation;
		public static readonly FontGetGlyphNameProxyDelegate GlyphNameProxy = GlyphNameProxyImplementation;
		public static readonly FontGetGlyphFromNameProxyDelegate GlyphFromNameProxy = GlyphFromNameProxyImplementation;
#else
		public static readonly nint FontExtentsProxy = (nint)(delegate* unmanaged[Cdecl]<IntPtr, void*, FontExtents*, void*, bool>)&FontExtentsProxyImplementation;
		public static readonly nint NominalGlyphProxy = (nint)(delegate* unmanaged[Cdecl]<IntPtr, void *, uint, uint*, void*, bool>)&NominalGlyphProxyImplementation;
		public static readonly nint VariationGlyphProxy = (nint)(delegate* unmanaged[Cdecl]<IntPtr, void*, uint, uint, uint*, void*, bool>)&VariationGlyphProxyImplementation;
		public static readonly nint NominalGlyphsProxy = (nint)(delegate* unmanaged[Cdecl]<IntPtr, void*, uint, uint*, uint, uint*, uint, void*, uint >)&NominalGlyphsProxyImplementation;
		public static readonly nint GlyphAdvanceProxy = (nint)(delegate* unmanaged[Cdecl]<IntPtr, void *, uint, void *, int >)&GlyphAdvanceProxyImplementation;
		public static readonly nint GlyphAdvancesProxy = (nint)(delegate* unmanaged[Cdecl]<IntPtr, void *, uint, uint *, uint, int *, uint, void *, void>)&GlyphAdvancesProxyImplementation;
		public static readonly nint GlyphOriginProxy = (nint)(delegate* unmanaged[Cdecl]<IntPtr, void*, uint, int*, int*, void *, bool >)&GlyphOriginProxyImplementation;
		public static readonly nint GlyphKerningProxy = (nint)(delegate* unmanaged[Cdecl]<IntPtr, void *, uint, uint, void * , int>)&GlyphKerningProxyImplementation;
		public static readonly nint GlyphExtentsProxy = (nint)(delegate* unmanaged[Cdecl]<IntPtr, void *, uint, GlyphExtents *, void * , bool>)&GlyphExtentsProxyImplementation;
		public static readonly nint GlyphContourPointProxy = (nint)(delegate* unmanaged[Cdecl]<IntPtr, void *, uint, uint, int * , int * , void * ,bool >)&GlyphContourPointProxyImplementation;
		public static readonly nint GlyphNameProxy = (nint)(delegate* unmanaged[Cdecl]<IntPtr, void *, uint, void *, uint, void *, bool>)&GlyphNameProxyImplementation;
		public static readonly nint GlyphFromNameProxy = (nint)(delegate* unmanaged[Cdecl]<IntPtr, void * , void * , int , uint * , void * , bool>)&GlyphFromNameProxyImplementation;
#endif

		// internal proxy implementations

#if !NET6_0
		[MonoPInvokeCallback (typeof (FontGetFontExtentsProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static bool FontExtentsProxyImplementation (IntPtr font, void* fontData, FontExtents* extents, void* context)
		{
			var del = GetMulti<FontExtentsDelegate> ((IntPtr)context, out _);
			var userData = GetMultiUserData<FontUserData> ((IntPtr)fontData, out _);
			var result = del.Invoke (userData.Font, userData.FontData, out var extentsManaged);
			if (extents != null)
				*extents = extentsManaged;
			return result;
		}

#if !NET6_0
		[MonoPInvokeCallback (typeof (FontGetNominalGlyphProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static bool NominalGlyphProxyImplementation (IntPtr font, void* fontData, uint unicode, uint* glyph, void* context)
		{
			var del = GetMulti<NominalGlyphDelegate> ((IntPtr)context, out _);
			var userData = GetMultiUserData<FontUserData> ((IntPtr)fontData, out _);
			var result = del.Invoke (userData.Font, userData.FontData, unicode, out var glyphManaged);
			if (glyph != null)
				*glyph = glyphManaged;
			return result;
		}

#if !NET6_0
		[MonoPInvokeCallback (typeof (FontGetNominalGlyphsProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static uint NominalGlyphsProxyImplementation (IntPtr font, void* fontData, uint count, uint* firstUnicode, uint unicodeStride, uint* firstGlyph, uint glyphStride, void* context)
		{
			var del = GetMulti<NominalGlyphsDelegate> ((IntPtr)context, out _);
			var unicodes = new ReadOnlySpan<uint> (firstUnicode, (int)count);
			var glyphs = new Span<uint> (firstGlyph, (int)count);
			var userData = GetMultiUserData<FontUserData> ((IntPtr)fontData, out _);
			return del.Invoke (userData.Font, userData.FontData, count, unicodes, glyphs);
		}

#if !NET6_0
		[MonoPInvokeCallback (typeof (FontGetVariationGlyphProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static bool VariationGlyphProxyImplementation (IntPtr font, void* fontData, uint unicode, uint variationSelector, uint* glyph, void* context)
		{
			var del = GetMulti<VariationGlyphDelegate> ((IntPtr)context, out _);
			var userData = GetMultiUserData<FontUserData> ((IntPtr)fontData, out _);
			var result = del.Invoke (userData.Font, userData.FontData, unicode, variationSelector, out var glyphManaged);
			if (glyph != null)
				*glyph = glyphManaged;
			return result;
		}

#if !NET6_0
		[MonoPInvokeCallback (typeof (FontGetGlyphAdvanceProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static int GlyphAdvanceProxyImplementation (IntPtr font, void* fontData, uint glyph, void* context)
		{
			var del = GetMulti<GlyphAdvanceDelegate> ((IntPtr)context, out _);
			var userData = GetMultiUserData<FontUserData> ((IntPtr)fontData, out _);
			return del.Invoke (userData.Font, userData.FontData, glyph);
		}

#if !NET6_0
		[MonoPInvokeCallback (typeof (FontGetGlyphAdvancesProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static void GlyphAdvancesProxyImplementation (IntPtr font, void* fontData, uint count, uint* firstGlyph, uint glyphStride, int* firstAdvance, uint advanceStride, void* context)
		{
			var del = GetMulti<GlyphAdvancesDelegate> ((IntPtr)context, out _);
			var glyphs = new ReadOnlySpan<uint> (firstGlyph, (int)count);
			var advances = new Span<int> (firstAdvance, (int)count);
			var userData = GetMultiUserData<FontUserData> ((IntPtr)fontData, out _);
			del.Invoke (userData.Font, userData.FontData, count, glyphs, advances);
		}

#if !NET6_0
		[MonoPInvokeCallback (typeof (FontGetGlyphOriginProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static bool GlyphOriginProxyImplementation (IntPtr font, void* fontData, uint glyph, int* x, int* y, void* context)
		{
			var del = GetMulti<GlyphOriginDelegate> ((IntPtr)context, out _);
			var userData = GetMultiUserData<FontUserData> ((IntPtr)fontData, out _);
			var result = del.Invoke (userData.Font, userData.FontData, glyph, out var xManaged, out var yManaged);
			if (x != null)
				*x = xManaged;
			if (y != null)
				*y = yManaged;
			return result;
		}

#if !NET6_0
		[MonoPInvokeCallback (typeof (FontGetGlyphKerningProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static int GlyphKerningProxyImplementation (IntPtr font, void* fontData, uint firstGlyph, uint secondGlyph, void* context)
		{
			var del = GetMulti<GlyphKerningDelegate> ((IntPtr)context, out _);
			var userData = GetMultiUserData<FontUserData> ((IntPtr)fontData, out _);
			return del.Invoke (userData.Font, userData.FontData, firstGlyph, secondGlyph);
		}

#if !NET6_0
		[MonoPInvokeCallback (typeof (FontGetGlyphExtentsProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static bool GlyphExtentsProxyImplementation (IntPtr font, void* fontData, uint glyph, GlyphExtents* extents, void* context)
		{
			var del = GetMulti<GlyphExtentsDelegate> ((IntPtr)context, out _);
			var userData = GetMultiUserData<FontUserData> ((IntPtr)fontData, out _);
			var result = del.Invoke (userData.Font, userData.FontData, glyph, out var extentsManaged);
			if (extents != null)
				*extents = extentsManaged;
			return result;
		}

#if !NET6_0
		[MonoPInvokeCallback (typeof (FontGetGlyphContourPointProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static bool GlyphContourPointProxyImplementation (IntPtr font, void* fontData, uint glyph, uint pointIndex, int* x, int* y, void* context)
		{
			var del = GetMulti<GlyphContourPointDelegate> ((IntPtr)context, out _);
			var userData = GetMultiUserData<FontUserData> ((IntPtr)fontData, out _);
			var result = del.Invoke (userData.Font, userData.FontData, glyph, pointIndex, out var xManaged, out var yManaged);
			if (x != null)
				*x = xManaged;
			if (y != null)
				*y = yManaged;
			return result;
		}

#if !NET6_0
		[MonoPInvokeCallback (typeof (FontGetGlyphNameProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static bool GlyphNameProxyImplementation (IntPtr font, void* fontData, uint glyph, void* nameBuffer, uint size, void* context)
		{
			var del = GetMulti<GlyphNameDelegate> ((IntPtr)context, out _);
			var userData = GetMultiUserData<FontUserData> ((IntPtr)fontData, out _);
			var result = del.Invoke (userData.Font, userData.FontData, glyph, out var realName);

			var nameSpan = realName.AsSpan ();
			var bufferSpan = new Span<char> (nameBuffer, (int)size);
			nameSpan.CopyTo (bufferSpan);

			return result;
		}

#if !NET6_0
		[MonoPInvokeCallback (typeof (FontGetGlyphFromNameProxyDelegate))]
#else
		[UnmanagedCallersOnly (CallConvs = new Type[] { typeof (CallConvCdecl) })]
#endif
		private static bool GlyphFromNameProxyImplementation (IntPtr font, void* fontData, void* name, int len, uint* glyph, void* context)
		{
			var del = GetMulti<GlyphFromNameDelegate> ((IntPtr)context, out _);
			var userData = GetMultiUserData<FontUserData> ((IntPtr)fontData, out _);

			var actualName = len < 0
				? new string ((char*)name)
				: new string ((char*)name, 0, len);

			var result = del.Invoke (userData.Font, userData.FontData, actualName, out var glyphManaged);
			if (glyph != null)
				*glyph = glyphManaged;
			return result;
		}
	}
}
