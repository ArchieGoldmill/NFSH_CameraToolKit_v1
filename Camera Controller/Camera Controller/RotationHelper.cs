using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;


namespace CameraController
{
	public static class RotationHelper
	{
		public static double[,] ConvertAnglesTomatrix(double x, double y, double z)
		{
			x = DegreeToRadian(x);
			y = DegreeToRadian(y);
			z = DegreeToRadian(z);

			double[,] matrix_x =
			{
				{1, 0, 0},
				{0, Math.Cos(x), -Math.Sin(x)},
				{0, Math.Sin(x), Math.Cos(x)}
			};

			double[,] matrix_y =
			{
				{Math.Cos(y), 0, Math.Sin(y)},
				{0, 1, 0},
				{-Math.Sin(y), 0, Math.Cos(y)}
			};

			double[,] matrix_z =
			{
				{Math.Cos(z), -Math.Sin(z), 0},
				{Math.Sin(z), Math.Cos(z), 0},
				{0, 0,1}
			};

			var res1 = MultiplyMatrix(matrix_x, matrix_y);

			var res2 = MultiplyMatrix(res1, matrix_z);

			return res2;
		}

		public static Matrix4x4 ConvertAnglesToMatrix(Vector3 position, Vector3 rotation)
		{
			var matrix = Matrix4x4.CreateFromYawPitchRoll(rotation.Y, rotation.X, rotation.Z);

			matrix.M41 = position.X;
			matrix.M42 = position.Z;
			matrix.M43 = position.Y;

			return matrix;
		}


		public static double[,] ConvertAnglesToMatrix1(double x, double z, double y)
		{
			x = DegreeToRadian(x);
			y = DegreeToRadian(y);
			z = DegreeToRadian(z);

			var matrix = new double[3, 3]
			{
				{ Math.Cos(y)*Math.Cos(x),Math.Sin(z)*Math.Sin(y)*Math.Cos(x)-Math.Cos(z)*Math.Sin(x), Math.Cos(z)*Math.Sin(y)*Math.Cos(x)+Math.Sin(z)*Math.Sin(x) },
				{ Math.Cos(y)*Math.Sin(x),Math.Sin(z)*Math.Sin(y)*Math.Sin(x)+Math.Cos(z)*Math.Cos(x), Math.Cos(z)*Math.Sin(y)*Math.Sin(x)-Math.Sin(z)*Math.Cos(x) },
				{ -Math.Sin(y), Math.Sin(z)*Math.Cos(y), Math.Cos(z)*Math.Cos(y)}
			};

			return matrix;
		}

		public static double[,] MultiplyMatrix1(double[,] A, double[,] B)
		{
			int rA = A.GetLength(0);
			int cA = A.GetLength(1);

			int cB = B.GetLength(1);

			double[,] kHasil = new double[rA, cB];

			for (int i = 0; i < rA; i++)
			{
				for (int j = 0; j < cB; j++)
				{
					double temp = 0;
					for (int k = 0; k < cA; k++)
					{
						temp += A[i, k] * B[k, j];
					}
					kHasil[i, j] = temp;
				}
			}
			return kHasil;
		}

		public static double[,] MultiplyMatrix(double[,] a, double[,] b)
		{
			var c = new double[a.GetLength(0), b.GetLength(1)];
			for (int i = 0; i < c.GetLength(0); i++)
			{
				for (int j = 0; j < c.GetLength(1); j++)
				{
					c[i, j] = 0;
					for (int k = 0; k < a.GetLength(1); k++) // OR k<b.GetLength(0)
						c[i, j] = c[i, j] + a[i, k] * b[k, j];
				}
			}

			return c;
		}

		public static double DegreeToRadian(double angle)
		{
			return Math.PI * angle / 180.0;
		}

	}
}
