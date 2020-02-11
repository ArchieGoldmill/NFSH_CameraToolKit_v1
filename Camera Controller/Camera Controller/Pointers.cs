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

		private static void Init(out int[] ar, int value)
		{
			var main = new List<int>(Main);
			main.Add(value);
			ar = main.ToArray();
		}

		public static int[] Main =
		{
			0x4a87d90,
			0x98,
			0xF8,
			0x50,
		};

		public static int[] FovPtr =
{
			0x0479D3B8,
			0x0,
			0x1168
		};

		public static int[] MainObj;

		public static int[] PosX;
		public static int[] PosY;
		public static int[] PosZ;

		public static int[] Rotation;

		public static long CamCodeCave = 0x14D535817;

		public static long FovCodePtr = 0x1486D6C53;

		public static long LOD = 0x14D17335E;

		public static long AllPartsOptional = 0x141BB4EA5;

		public static long VinylsPtr = 0x141D53A84;

		public static long NoPartRestrictions1 = 0x141E59347;
		public static long NoPartRestrictions2 = 0x141E58FF3;
	}
}