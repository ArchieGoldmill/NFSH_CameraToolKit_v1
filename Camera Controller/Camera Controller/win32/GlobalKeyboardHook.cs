using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CameraController
{
	/// <summary>
	/// A class that manages a global low level keyboard hook
	/// </summary>
	public class GlobalKeyboardHook
	{

		#region Constant, Structure and Delegate Definitions
		/// <summary>
		/// defines the callback type for the hook
		/// </summary>
		public delegate int keyboardHookProc(int code, int wParam, ref keyboardHookStruct lParam);

		private keyboardHookProc hookProcDelegate;

		public struct keyboardHookStruct
		{
			public int vkCode;
			public int scanCode;
			public int flags;
			public int time;
			public int dwExtraInfo;
		}

		const int WH_KEYBOARD_LL = 13;
		const int WM_KEYDOWN = 0x100;
		const int WM_KEYUP = 0x101;
		const int WM_SYSKEYDOWN = 0x104;
		const int WM_SYSKEYUP = 0x105;
		#endregion

		#region Instance Variables
		/// <summary>
		/// The collections of keys to watch for
		/// </summary>
		public List<Keys> HookedKeys = new List<Keys>();
		/// <summary>
		/// Handle to the hook, need this to unhook and call the next hook
		/// </summary>
		IntPtr hhook = IntPtr.Zero;
		#endregion

		#region Events
		/// <summary>
		/// Occurs when one of the hooked keys is pressed
		/// </summary>
		public event KeyEventHandler KeyDown;
		/// <summary>
		/// Occurs when one of the hooked keys is released
		/// </summary>
		public event KeyEventHandler KeyUp;
		#endregion

		#region Constructors and Destructors
		/// <summary>
		/// Initializes a new instance of the <see cref="globalKeyboardHook"/> class and installs the keyboard hook.
		/// </summary>
		public GlobalKeyboardHook()
		{
			hookProcDelegate = hookProc;
			hook();
		}

		/// <summary>
		/// Releases unmanaged resources and performs other cleanup operations before the
		/// <see cref="globalKeyboardHook"/> is reclaimed by garbage collection and uninstalls the keyboard hook.
		/// </summary>
		~GlobalKeyboardHook()
		{
			unhook();
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Installs the global hook
		/// </summary>
		public void hook()
		{
			IntPtr hInstance = LoadLibrary("User32");
			hhook = SetWindowsHookEx(WH_KEYBOARD_LL, hookProcDelegate, hInstance, 0);
		}

		/// <summary>
		/// Uninstalls the global hook
		/// </summary>
		public void unhook()
		{
			UnhookWindowsHookEx(hhook);
		}

		/// <summary>
		/// The callback for the keyboard hook
		/// </summary>
		/// <param name="code">The hook code, if it isn't >= 0, the function shouldn't do anyting</param>
		/// <param name="wParam">The event type</param>
		/// <param name="lParam">The keyhook event information</param>
		/// <returns></returns>
		public int hookProc(int code, int wParam, ref keyboardHookStruct lParam)
		{
			if (code >= 0)
			{
				Keys key = (Keys)lParam.vkCode;
				if (HookedKeys.Contains(key))
				{
					KeyEventArgs kea = new KeyEventArgs(key);
					if ((wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN) && (KeyDown != null))
					{
						KeyDown(this, kea);
					}
					else if ((wParam == WM_KEYUP || wParam == WM_SYSKEYUP) && (KeyUp != null))
					{
						KeyUp(this, kea);
					}
					if (kea.Handled)
						return 1;
				}
			}

			return CallNextHookEx(hhook, code, wParam, ref lParam);
		}
		#endregion

		#region DLL imports
		/// <summary>
		/// Sets the windows hook, do the desired event, one of hInstance or threadId must be non-null
		/// </summary>
		/// <param name="idHook">The id of the event you want to hook</param>
		/// <param name="callback">The callback.</param>
		/// <param name="hInstance">The handle you want to attach the event to, can be null</param>
		/// <param name="threadId">The thread you want to attach the event to, can be null</param>
		/// <returns>a handle to the desired hook</returns>
		[DllImport("user32.dll")]
		static extern IntPtr SetWindowsHookEx(int idHook, keyboardHookProc callback, IntPtr hInstance, uint threadId);

		/// <summary>
		/// Unhooks the windows hook.
		/// </summary>
		/// <param name="hInstance">The hook handle that was returned from SetWindowsHookEx</param>
		/// <returns>True if successful, false otherwise</returns>
		[DllImport("user32.dll")]
		static extern bool UnhookWindowsHookEx(IntPtr hInstance);

		/// <summary>
		/// Calls the next hook.
		/// </summary>
		/// <param name="idHook">The hook id</param>
		/// <param name="nCode">The hook code</param>
		/// <param name="wParam">The wparam.</param>
		/// <param name="lParam">The lparam.</param>
		/// <returns></returns>
		[DllImport("user32.dll")]
		static extern int CallNextHookEx(IntPtr idHook, int nCode, int wParam, ref keyboardHookStruct lParam);

		/// <summary>
		/// Loads the library.
		/// </summary>
		/// <param name="lpFileName">Name of the library</param>
		/// <returns>A handle to the library</returns>
		[DllImport("kernel32.dll")]
		static extern IntPtr LoadLibrary(string lpFileName);
		#endregion
	}

	public class GlobalMouseHook
	{
		public event MouseEventHandler MouseMove;

		/// <summary>
		/// Loads the library.
		/// </summary>
		/// <param name="lpFileName">Name of the library</param>
		/// <returns>A handle to the library</returns>
		[DllImport("kernel32.dll")]
		static extern IntPtr LoadLibrary(string lpFileName);

		[DllImport("User32.dll", SetLastError = true)]
		internal static extern bool RegisterRawInputDevices(RawInputDevice[] pRawInputDevice, uint numberDevices, uint size);

		[StructLayout(LayoutKind.Sequential)]
		internal struct RawInputDevice
		{
			internal HidUsagePage UsagePage;
			internal HidUsage Usage;
			internal RawInputDeviceFlags Flags;
			internal IntPtr Target;

			public override string ToString()
			{
				return string.Format("{0}/{1}, flags: {2}, target: {3}", UsagePage, Usage, Flags, Target);
			}
		}

		public enum HidUsagePage : ushort
		{
			UNDEFINED = 0x00,   // Unknown usage page
			GENERIC = 0x01,     // Generic desktop controls
			SIMULATION = 0x02,  // Simulation controls
			VR = 0x03,          // Virtual reality controls
			SPORT = 0x04,       // Sports controls
			GAME = 0x05,        // Games controls
			KEYBOARD = 0x07,    // Keyboard controls
		}

		public enum HidUsage : ushort
		{
			Undefined = 0x00,       // Unknown usage
			Pointer = 0x01,         // Pointer
			Mouse = 0x02,           // Mouse
			Joystick = 0x04,        // Joystick
			Gamepad = 0x05,         // Game Pad
			Keyboard = 0x06,        // Keyboard
			Keypad = 0x07,          // Keypad
			SystemControl = 0x80,   // Muilt-axis Controller
			Tablet = 0x80,          // Tablet PC controls
			Consumer = 0x0C,        // Consumer
		}

		[Flags]
		internal enum RawInputDeviceFlags
		{
			NONE = 0,                   // No flags
			REMOVE = 0x00000001,        // Removes the top level collection from the inclusion list. This tells the operating system to stop reading from a device which matches the top level collection. 
			EXCLUDE = 0x00000010,       // Specifies the top level collections to exclude when reading a complete usage page. This flag only affects a TLC whose usage page is already specified with PageOnly.
			PAGEONLY = 0x00000020,      // Specifies all devices whose top level collection is from the specified UsagePage. Note that Usage must be zero. To exclude a particular top level collection, use Exclude.
			NOLEGACY = 0x00000030,      // Prevents any devices specified by UsagePage or Usage from generating legacy messages. This is only for the mouse and keyboard.
			INPUTSINK = 0x00000100,     // Enables the caller to receive the input even when the caller is not in the foreground. Note that WindowHandle must be specified.
			CAPTUREMOUSE = 0x00000200,  // Mouse button click does not activate the other window.
			NOHOTKEYS = 0x00000200,     // Application-defined keyboard device hotkeys are not handled. However, the system hotkeys; for example, ALT+TAB and CTRL+ALT+DEL, are still handled. By default, all keyboard hotkeys are handled. NoHotKeys can be specified even if NoLegacy is not specified and WindowHandle is NULL.
			APPKEYS = 0x00000400,       // Application keys are handled.  NoLegacy must be specified.  Keyboard only.

			// Enables the caller to receive input in the background only if the foreground application does not process it. 
			// In other words, if the foreground application is not registered for raw input, then the background application that is registered will receive the input.
			EXINPUTSINK = 0x00001000,
			DEVNOTIFY = 0x00002000
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct Rawinputheader
		{
			public uint dwType;                     // Type of raw input (RIM_TYPEHID 2, RIM_TYPEKEYBOARD 1, RIM_TYPEMOUSE 0)
			public uint dwSize;                     // Size in bytes of the entire input packet of data. This includes RAWINPUT plus possible extra input reports in the RAWHID variable length array. 
			public IntPtr hDevice;                  // A handle to the device generating the raw input data. 
			public IntPtr wParam;                   // RIM_INPUT 0 if input occurred while application was in the foreground else RIM_INPUTSINK 1 if it was not.

			public override string ToString()
			{
				return string.Format("RawInputHeader\n dwType : {0}\n dwSize : {1}\n hDevice : {2}\n wParam : {3}", dwType, dwSize, hDevice, wParam);
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct InputData
		{
			public Rawinputheader header;           // 64 bit header size: 24  32 bit the header size: 16
			public RawData data;                    // Creating the rest in a struct allows the header size to align correctly for 32/64 bit
		}

		[StructLayout(LayoutKind.Explicit)]
		public struct RawData
		{
			[FieldOffset(0)]
			internal Rawmouse mouse;
			[FieldOffset(0)]
			internal Rawkeyboard keyboard;
			[FieldOffset(0)]
			internal Rawhid hid;
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct Rawhid
		{
			public uint dwSizHid;
			public uint dwCount;
			public byte bRawData;

			public override string ToString()
			{
				return string.Format("Rawhib\n dwSizeHid : {0}\n dwCount : {1}\n bRawData : {2}\n", dwSizHid, dwCount, bRawData);
			}
		}

		[StructLayout(LayoutKind.Explicit)]
		internal struct Rawmouse
		{
			[FieldOffset(0)]
			public ushort usFlags;
			[FieldOffset(4)]
			public uint ulButtons;
			[FieldOffset(4)]
			public ushort usButtonFlags;
			[FieldOffset(6)]
			public ushort usButtonData;
			[FieldOffset(8)]
			public uint ulRawButtons;
			[FieldOffset(12)]
			public int lLastX;
			[FieldOffset(16)]
			public int lLastY;
			[FieldOffset(20)]
			public uint ulExtraInformation;
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct Rawkeyboard
		{
			public ushort Makecode;                 // Scan code from the key depression
			public ushort Flags;                    // One or more of RI_KEY_MAKE, RI_KEY_BREAK, RI_KEY_E0, RI_KEY_E1
			private readonly ushort Reserved;       // Always 0    
			public ushort VKey;                     // Virtual Key Code
			public uint Message;                    // Corresponding Windows message for exmaple (WM_KEYDOWN, WM_SYASKEYDOWN etc)
			public uint ExtraInformation;           // The device-specific addition information for the event (seems to always be zero for keyboards)

			public override string ToString()
			{
				return string.Format("Rawkeyboard\n Makecode: {0}\n Makecode(hex) : {0:X}\n Flags: {1}\n Reserved: {2}\n VKeyName: {3}\n Message: {4}\n ExtraInformation {5}\n",
													Makecode, Flags, Reserved, VKey, Message, ExtraInformation);
			}
		}

		[DllImport("User32.dll", SetLastError = true)]
		internal static extern int GetRawInputData(IntPtr hRawInput, DataCommand command, [Out] out InputData buffer, [In, Out] ref int size, int cbSizeHeader);

		[DllImport("User32.dll", SetLastError = true)]
		internal static extern int GetRawInputData(IntPtr hRawInput, DataCommand command, [Out] IntPtr pData, [In, Out] ref int size, int sizeHeader);

		public enum DataCommand : uint
		{
			RID_HEADER = 0x10000005, // Get the header information from the RAWINPUT structure.
			RID_INPUT = 0x10000003   // Get the raw data from the RAWINPUT structure.
		}

		public GlobalMouseHook(IntPtr hwnd)
		{
			var rid = new RawInputDevice[1];

			rid[0].UsagePage = HidUsagePage.GENERIC;
			rid[0].Usage = HidUsage.Mouse;
			rid[0].Flags = RawInputDeviceFlags.INPUTSINK;
			rid[0].Target = hwnd;

			if (!RegisterRawInputDevices(rid, (uint)rid.Length, (uint)Marshal.SizeOf(rid[0])))
			{
				throw new ApplicationException("Failed to register raw input device(s).");
			}
		}

		public void hookProc(IntPtr lParam)
		{

			var dwSize = 0;
			GetRawInputData(lParam, DataCommand.RID_INPUT, IntPtr.Zero, ref dwSize, Marshal.SizeOf(typeof(Rawinputheader)));

			if (dwSize != GetRawInputData(lParam, DataCommand.RID_INPUT, out InputData _rawBuffer, ref dwSize, Marshal.SizeOf(typeof(Rawinputheader))))
			{
				throw new Exception();
			}

			this.MouseMove(this, new MouseEventArgs(MouseButtons.None, 0, _rawBuffer.data.mouse.lLastX, _rawBuffer.data.mouse.lLastY, 0));
		}
	}
}
