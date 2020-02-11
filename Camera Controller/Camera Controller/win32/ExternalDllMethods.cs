using System;
using System.Runtime.InteropServices;

namespace Memory
{
	public class ExternalDllMethods
	{
		[Flags]
		protected enum ProcessAccessFlags : uint
		{
			All = 0x001F0FFF,
			Terminate = 0x00000001,
			CreateThread = 0x00000002,
			VirtualMemoryOperation = 0x00000008,
			VirtualMemoryRead = 0x00000010,
			DuplicateHandle = 0x00000040,
			CreateProcess = 0x000000080,
			SetQuota = 0x00000100,
			SetInformation = 0x00000200,
			QueryInformation = 0x00000400,
			QueryLimitedInformation = 0x00001000,
			Synchronize = 0x00100000
		}

		[DllImport("kernel32.dll", SetLastError = true)]
		protected static extern IntPtr OpenProcess(
			ProcessAccessFlags processAccess,
			bool bInheritHandle,
			int processId
		);

		[DllImport("kernel32.dll", SetLastError = true)]
		protected static extern bool WriteProcessMemory(
			IntPtr hProcess,
			IntPtr lpBaseAddress,
			byte[] lpBuffer,
			int nSize,
			out IntPtr lpNumberOfBytesWritten);

		[DllImport("kernel32.dll")]
		protected static extern bool VirtualProtectEx(
			IntPtr hProcess,
			IntPtr lpAddress,
			int nSize,
			uint flNewProtect,
			out uint lpflOldProtect);

		[DllImport("kernel32.dll", SetLastError = true)]
		protected static extern bool ReadProcessMemory(
			IntPtr hProcess,
			IntPtr lpBaseAddress,
			[Out] byte[] lpBuffer,
			int dwSize,
			out IntPtr lpNumberOfBytesRead);

		[DllImport("kernel32.dll")]
		protected static extern bool ReadProcessMemory(
			IntPtr hProcess,
			IntPtr lpBaseAdress,
			out IntPtr lpBuffer,
			uint iSize,
			out uint lpNumberOfBytesRead);

		[Flags]
		public enum AllocationType
		{
			Commit = 0x00001000,
			Reserve = 0x00002000,
			Decommit = 0x00004000,
			Release = 0x00008000,
			Reset = 0x00080000,
			TopDown = 0x00100000,
			WriteWatch = 0x00200000,
			Physical = 0x00400000,
			LargePages = 0x20000000
		}

		[Flags]
		public enum MemoryProtection
		{
			NoAccess = 0x0001,
			ReadOnly = 0x0002,
			ReadWrite = 0x0004,
			WriteCopy = 0x0008,
			Execute = 0x0010,
			ExecuteRead = 0x0020,
			ExecuteReadWrite = 0x0040,
			ExecuteWriteCopy = 0x0080,
			GuardModifierflag = 0x0100,
			NoCacheModifierflag = 0x0200,
			WriteCombineModifierflag = 0x0400
		}

		[DllImport("kernel32.dll")]
		internal static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, int dwSize, AllocationType flAllocationType, MemoryProtection flProtect);

		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		internal static extern short GetKeyState(int keyCode);
	}
}