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

		public static long CamCodeCave = 0x14C9CE497;

		public static long FovCodePtr = 0x1488453C3;

		public static long LOD = 0x14C53A39E;

		public static long AllPartsOptional = 0x141D4BCC1;

		public static long VinylsPtr = 0x141C0E974;

		public static long NoPartRestrictions1 = 0x141EAE1E7;
		public static long NoPartRestrictions2 = 0x141EADE93;
	}
}