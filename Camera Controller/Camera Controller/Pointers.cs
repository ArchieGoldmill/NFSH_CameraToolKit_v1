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

		// 48 8B 0D ?? ?? ?? ?? 48 8B 01 FF 50 10 0F B7 47 58 66 89
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
		public static long CamCodeCave = 0x14C9CE497;

		// F3 0F 11 87 68 11 00 00 48 85 DB 74 25 48 8B 03 48 89 D9 FF 50 08 0F 2F C6 77 0B 48 8B 5B 08 48 85 DB 75 E9 EB 0C F3 0F
		public static long FovCodePtr = 0x1488453C3;

		// 0B BE 75 3F C7 83 18 01 00 00 CD CC 4C 3D C7 83 1C 01 00 00 00 40 1C 46 C7 83 20 01 00 00 AB AA AA 3F C7 83 24 01 00 00
		public static long LOD = 0x14C53A39E;

		// 74 16 80 BD 60 01 00 00 00 74 0D 80 BD 68 01 00 00 00 74 04 B1 01 EB 02 32 C9 88
		// 74 16 80 BD 60 01 00 00 00 74 0D 80 BD 68 01 00 00 00 74 04 B1 01 EB 02 32 C9 88 4C 24 42 49 8B 56 48 48 85 D2 74 23 8B 42 1C 4C 8B 02 83 E0 60 3C 60 75 05 41 38 08 74
		// 74 16 80 BD 40 01 00 00 00 74 0D 80 BD 48 01 00 00 00 74 04 B1 01 EB 02 32 C9 88 4C 24 42 49 8B 56 48 48 85 D2 74 23 8B 42 1C 4C 8B 02 83
		// 74 16 80 BD 40 01 00 00 00 74 0D 80 BD 48 01 00 00 00 74 04 B1 01 EB 02 32 C9 88 4C 24 42 49
		public static long AllPartsOptional = 0x141D4BCC1;

		// 74 20 48 8D 0C 89 48 8D 1C 8A 0F B6 53 11 48 8B CF E8 A5 EF FF FF 0F B6 53 10 48 8B CF E8 89 EC FF FF 48 8B 5C 24 60 48
		// 74 20 48 8D 0C 89 48 8D 1C 8A 0F B6 53 11 48 8B CF E8 75 ED FF FF 0F B6 53 10 48 8B CF E8 C9 E6 FF FF 48
		// 76 20 48 8D 0C 89 48 8D 1C 8A 0F B6 53 11 48 8B CF E8 36 B6 FF FF 0F B6 53 10 48 8B CF E8 7A B0
		// 76 20 48 8D 0C 89 48 8D 1C 8A 0F B6 53 11 48 8B CF E8 75 ED FF FF 0F B6
		// 76 20 48 8D 0C 89 48 8D 1C 8A 0F B6 53 11 48 8B CF E8 E6 BE FF FF 0F B6 53 10 48 8B CF E8 AA B8 FF FF 48 8B 5C 24 60 48
		public static long VinylsPtr = 0x141C0E974;

		// 74 0F 49 8D 76 04 49 8B C4 48 2B C7 48 03 D8 EB 03 48 8B DF 48 85 DB 7F D0 4C 8B A5 E0 00 00 00 49
		// 74 0C 8B 07 89 03 48 8B DF 48 3B FE 75 DF 8B 44 24 78 89 03 49 83 C7 04 4D 3B FE 75 C0
		public static long NoPartRestrictions1 = 0x141EAE1E7;
		public static long NoPartRestrictions2 = 0x141EADE93;
	}
}