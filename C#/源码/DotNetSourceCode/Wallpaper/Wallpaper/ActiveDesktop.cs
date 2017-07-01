using System;
using System.Runtime.InteropServices ;
namespace SetWallpaper
{
	
		enum WPSTYLE
		{
			CENTER = 0,
			TILE = 1,
			STRETCH = 2,
			MAX = 3
		}

		/*struct WALLPAPEROPT
		{
			public int dwSize;
			public WPSTYLE dwStyle;
		}*/

		/*struct COMPONENTSOPT
		{
			public int dwSize;
			[MarshalAs(UnmanagedType.Bool)] public bool fEnableComponents;
			[MarshalAs(UnmanagedType.Bool)] public bool fActiveDesktop;
		}*/

		/*struct COMPPOS
		{
			public const int COMPONENT_TOP = 0x3FFFFFFF;
			public const int COMPONENT_DEFAULT_LEFT = 0xFFFF;
			public const int COMPONENT_DEFAULT_TOP = 0xFFFF;

			//public int dwSize;
			//public int iLeft;
			//public int iTop;
			//public int dwWidth;
			//public int dwHeight;
			//public int izIndex;
			[MarshalAs(UnmanagedType.Bool)] public bool fCanResize;
			[MarshalAs(UnmanagedType.Bool)] public bool fCanResizeX;
			[MarshalAs(UnmanagedType.Bool)] public bool fCanResizeY;
			public int iPreferredLeftPercent;
			public int iPreferredTopPercent;
		}*/

		[Flags]
		enum ITEMSTATE
		{
			NORMAL = 0x00000001,
			FULLSCREEN = 00000002,
			SPLIT = 0x00000004,
			VALIDSIZESTATEBITS = NORMAL | SPLIT | FULLSCREEN,
			VALIDSTATEBITS = NORMAL | SPLIT | FULLSCREEN |
				unchecked((int)0x80000000) | 0x40000000
		}

		/*struct COMPSTATEINFO
		{
			public int dwSize;
			public int iLeft;
			public int iTop;
			public int dwWidth;
			public int dwHeight;
			public int dwItemState;
		}*/

		enum COMP_TYPE
		{
			HTMLDOC = 0,
			PICTURE = 1,
			WEBSITE = 2,
			CONTROL = 3,
			CFHTML = 4,
			MAX = 4
		}

		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode)]
		struct COMPONENT
		{
			private const int INTERNET_MAX_URL_LENGTH = 2084; // =
			//INTERNET_MAX_SCHEME_LENGTH (32) + "://\0".Length +
			//INTERNET_MAX_PATH_LENGTH (2048)

			public int dwSize;
			public int dwID;
			public COMP_TYPE iComponentType;
			[MarshalAs(UnmanagedType.Bool)] public bool fChecked;
			[MarshalAs(UnmanagedType.Bool)] public bool fDirty;
			[MarshalAs(UnmanagedType.Bool)] public bool fNoScroll;
			//public COMPPOS cpPos;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=260)] public string
				wszFriendlyName;
			[MarshalAs(UnmanagedType.ByValTStr,
			           SizeConst=INTERNET_MAX_URL_LENGTH)] public string wszSource;
			[MarshalAs(UnmanagedType.ByValTStr,
			           SizeConst=INTERNET_MAX_URL_LENGTH)] public string wszSubscribedURL;

			#if AD_IE5
			public int dwCurItemState;
			public COMPSTATEINFO csiOriginal;
			public COMPSTATEINFO csiRestored;
			#endif
		}

		enum DTI_ADTIWUI
		{
			DTI_ADDUI_DEFAULT = 0x00000000,
			DTI_ADDUI_DISPSUBWIZARD = 0x00000001,
			DTI_ADDUI_POSITIONITEM = 0x00000002,
		}

		[Flags]
		enum AD_APPLY
		{
			SAVE = 0x00000001,
			HTMLGEN = 0x00000002,
			REFRESH = 0x00000004,
			ALL = SAVE | HTMLGEN | REFRESH,
			FORCE = 0x00000008,
			BUFFERED_REFRESH = 0x00000010,
			DYNAMICREFRESH = 0x00000020
		}

		[Flags]
		enum COMP_ELEM
		{
			TYPE = 0x00000001,
			CHECKED = 0x00000002,
			DIRTY = 0x00000004,
			NOSCROLL = 0x00000008,
			POS_LEFT = 0x00000010,
			POS_TOP = 0x00000020,
			SIZE_WIDTH = 0x00000040,
			SIZE_HEIGHT = 0x00000080,
			POS_ZINDEX = 0x00000100,
			SOURCE = 0x00000200,
			FRIENDLYNAME = 0x00000400,
			SUBSCRIBEDURL = 0x00000800,
			ORIGINAL_CSI = 0x00001000,
			RESTORED_CSI = 0x00002000,
			CURITEMSTATE = 0x00004000,
			ALL = TYPE | CHECKED | DIRTY | NOSCROLL | POS_LEFT | SIZE_WIDTH |
				SIZE_HEIGHT | POS_ZINDEX | SOURCE |
				FRIENDLYNAME | POS_TOP | SUBSCRIBEDURL | ORIGINAL_CSI |
				RESTORED_CSI | CURITEMSTATE
		}

		[Flags]
		enum ADDURL
		{
			SILENT = 0x0001
		}

		[
			ComImport(),
			Guid("F490EB00-1240-11D1-9888-006097DEACF9"),
			InterfaceType(ComInterfaceType.InterfaceIsIUnknown)
		]
		interface IActiveDesktop
		{
			void ApplyChanges(AD_APPLY dwFlags);
			void GetWallpaper([MarshalAs(UnmanagedType.LPWStr)]
			                  System.Text.StringBuilder pwszWallpaper, int cchWallpaper, int
			                  dwReserved);
			void SetWallpaper([MarshalAs(UnmanagedType.LPWStr)] string
			                  pwszWallpaper, int dwReserved);
			//void GetWallpaperOptions(ref WALLPAPEROPT pwpo, int dwReserved);
			//void SetWallpaperOptions([In] ref WALLPAPEROPT pwpo, int
			 //                        dwReserved);
			void GetPattern([MarshalAs(UnmanagedType.LPWStr)]
			                System.Text.StringBuilder pwszPattern, int cchPattern, int
			                dwReserved);
			void SetPattern([MarshalAs(UnmanagedType.LPWStr)] string
			                pwszPattern, int dwReserved);
			//void GetDesktopItemOptions(ref COMPONENTSOPT pco, int dwReserved);
			//void SetDesktopItemOptions([In] ref COMPONENTSOPT pco, int
			 //                          dwReserved);
			void AddDesktopItem([In] ref COMPONENT pcomp, int dwReserved);
			void AddDesktopItemWithUI(IntPtr hwnd, [In] ref COMPONENT pcomp,
			                          DTI_ADTIWUI dwFlags);
			void ModifyDesktopItem([In] ref COMPONENT pcomp, COMP_ELEM
			                       dwFlags);
			void RemoveDesktopItem([In] ref COMPONENT pcomp, int dwReserved);
			void GetDesktopItemCount(out int lpiCount, int dwReserved);
			void GetDesktopItem(int nComponent, ref COMPONENT pcomp, int
			                    dwReserved);
			void GetDesktopItemByID(IntPtr dwID, ref COMPONENT pcomp, int
			                        dwReserved);
			void GenerateDesktopItemHtml([MarshalAs(UnmanagedType.LPWStr)]
			                             string pwszFileName, [In] ref COMPONENT pcomp, int dwReserved);
			void AddUrl(IntPtr hwnd, [MarshalAs(UnmanagedType.LPWStr)] string
			            pszSource, [In] ref COMPONENT pcomp, ADDURL dwFlags);
			void GetDesktopItemBySource([MarshalAs(UnmanagedType.LPWStr)]
			                            string pwszSource, ref COMPONENT pcomp, int dwReserved);
		}
		[
			ComImport(),
			Guid("75048700-EF1F-11D0-9888-006097DEACF9")
		]
		class ActiveDesktop /* : IActiveDesktop */ {}
	}
