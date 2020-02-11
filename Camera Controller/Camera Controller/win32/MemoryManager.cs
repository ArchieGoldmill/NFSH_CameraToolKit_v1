using System;
using System.Diagnostics;
using System.Linq;
using Memory;

namespace CameraController
{
	public class MemoryManager : ExternalDllMethods
	{
		#region private fields

		private IntPtr pHandle;

		private IntPtr baseAdr;

		#endregion

		#region constructors

		public MemoryManager(string processName)
		{
			pHandle = this.GetProcessHandle(processName);

			baseAdr = this.GetProcessAddress(processName);
		}

		#endregion


		#region public methods

		public void MakeWritable(IntPtr pointer, int count)
		{
			uint lpflOldProtect;
			ExternalDllMethods.VirtualProtectEx(pHandle, pointer, count, 0x08, out lpflOldProtect);
		}


		public void WriteFloat(IntPtr pointer, float data)
		{
			IntPtr outP;
			ExternalDllMethods.WriteProcessMemory(pHandle, pointer, BitConverter.GetBytes(data), 4, out outP);
		}

		public void WriteFloat(int pointer, float data)
		{
			IntPtr outP;
			ExternalDllMethods.WriteProcessMemory(pHandle, new IntPtr(pointer), BitConverter.GetBytes(data), 4, out outP);
		}

		public float ReadFloat(IntPtr pointer)
		{
			byte[] data = this.ReadBytes(pointer, 4);

			return BitConverter.ToSingle(data, 0);
		}

		public float ReadFloat(int pointer)
		{
			byte[] data = this.ReadBytes(new IntPtr(pointer), 4);

			return BitConverter.ToSingle(data, 0);
		}


		public void WriteFloatMultiLevel(int[] offsets, float data)
		{
			this.WriteFloat(this.GetAbsoluteAddress(offsets), data);
		}

		public float ReadFloatMultiLevel(int[] offsets)
		{
			return this.ReadFloat(this.GetAbsoluteAddress(offsets));
		}

		public byte[] ReadBytesMultiLevel(int[] offsets, int count)
		{
			return this.ReadBytes(this.GetAbsoluteAddress(offsets), count);
		}


		public void WriteByte(IntPtr pointer, byte data)
		{
			this.WriteBytes(pointer, new byte[] { data });
		}

		public byte ReadByte(IntPtr pointer)
		{
			return this.ReadBytes(pointer, 1)[0];
		}


		public void WriteBytes(IntPtr pointer, byte[] data)
		{
			IntPtr outP;
			ExternalDllMethods.WriteProcessMemory(pHandle, pointer, data, data.Length, out outP);
		}

		public void WriteBytesMultiLevel(int[] offsets, byte[] data)
		{
			this.WriteBytes(this.GetAbsoluteAddress(offsets), data);
		}

		public byte[] ReadBytes(IntPtr pointer, int count)
		{
			IntPtr outP;
			byte[] data = new byte[count];
			ExternalDllMethods.ReadProcessMemory(pHandle, pointer, data, count, out outP);

			return data;
		}

		public IntPtr AllocateMemory(int len)
		{
			return VirtualAllocEx(this.pHandle, IntPtr.Zero, len, AllocationType.Commit, MemoryProtection.ExecuteReadWrite);
		}

		#endregion


		#region private methods

		private IntPtr ReadPointer(IntPtr adress)
		{
			IntPtr tempPTR;
			uint NumberOfBytesRead;

			ReadProcessMemory(pHandle, adress, out tempPTR, 4, out NumberOfBytesRead);
			return tempPTR;
		}

		private IntPtr ReadPointer64(IntPtr adress)
		{
			IntPtr tempPTR;
			uint NumberOfBytesRead;

			ReadProcessMemory(pHandle, adress, out tempPTR, 8, out NumberOfBytesRead);
			return tempPTR;
		}

		private IntPtr GetProcessHandle(string name)
		{
			Process[] pList = Process.GetProcesses();

			if (pList.Length == 0)
			{
				throw new Exception("No processes found");
			}

			foreach (var process in pList)
			{
				if (process.ProcessName == name)
				{
					return OpenProcess(ProcessAccessFlags.All, false, process.Id);
				}
			}

			throw new Exception("Process \"" + name + "\" not found!");
		}

		private IntPtr GetProcessAddress(string name)
		{
			Process[] pList = Process.GetProcesses();

			if (pList.Length == 0)
			{
				throw new Exception("No processes found");
			}

			foreach (var process in pList)
			{
				if (process.ProcessName == name)
				{
					return process.MainModule.BaseAddress;
				}
			}

			throw new Exception("Process \"" + name + "\" not found!");
		}

		public IntPtr GetAbsoluteAddress(int[] offsets)
		{
			IntPtr cur = baseAdr;

			for (int i = 0; i < offsets.Length - 1; i++)
			{
				cur = ReadPointer64(IntPtr.Add(cur, offsets[i]));
			}

			cur = IntPtr.Add(cur, offsets[offsets.Length - 1]);

			return cur;
		}

		#endregion

	}
}
