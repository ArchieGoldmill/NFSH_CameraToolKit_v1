using System.Collections.Generic;

namespace CameraController
{
	public static class Pointers
	{
		static Pointers()
		{
			Init(out MainObj, 0x0);

			Init(out PosX, 0xD0);
			Init(out PosZ, 0xD4);
			Init(out PosY, 0xD8);

			Init(out Rotation, 0xA0);
		}

		private static void Init(out long[] ar, long value)
		{
			var main = new List<long>(Main);
			main.Add(value);
			ar = main.ToArray();
		}

		public static long[] Main =
		{
			0x04AED190,
			0x98,
			0xF8,
			0x50,
		};

		public static long[] FovPtr =
{
			0x047FF898,
			0x0,
			0x1168
		};

		public static long[] MainObj;

		public static long[] PosX;
		public static long[] PosY;
		public static long[] PosZ;

		public static long[] Rotation;

		// 0F 29 81 A0 00 00 00 0F 28 48 10 0F 29 89 B0 00 00 00 0F 28 40 20 0F 29 81 C0 00 00 00 0F 28 48 30 0F 29 89 D0 00 00 00
		public static long CamCodeCave = 0x14DBC2737;

		// F3 0F 11 87 68 11 00 00 48 85 DB 74 25 48 8B 03 48 89 D9 FF 50 08 0F 2F C6 77 0B 48 8B 5B 08 48 85 DB 75 E9 EB 0C F3 0F
		public static long FovCodePtr = 0x149A5DF23;

		// 0B BE 75 3F C7 83 18 01 00 00 CD CC 4C 3D C7 83 1C 01 00 00 00 40 1C 46 C7 83 20 01 00 00 AB AA AA 3F C7 83 24 01 00 00
		public static long LOD = 0x14D88734C;

		public static long AllPartsOptional = 0x141D4BCC1;

		public static long VinylsPtr = 0x141C0E974;

		public static long NoPartRestrictions1 = 0x141EADE93;
		public static long NoPartRestrictions2 = 0x141EAE1E7;
	}
}