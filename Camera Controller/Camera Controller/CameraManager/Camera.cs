using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CameraController.CameraManager
{
	public class Camera
	{
		private float fov;

		public Camera()
		{

		}

		public Camera(Camera from)
		{
			this.CopyFrom(from);
		}

		public void CopyFrom(Camera from)
		{
			this.Position = from.Position;
			this.Rotation = from.Rotation;
			this.Fov = from.Fov;
			this.Time = from.Time;
		}

		public Vector3 Position;

		public Vector3 Rotation;

		public int Time { get; set; }

		public float Fov
		{
			get
			{
				return this.fov;
			}
			set
			{
				if (value > 0.01f && value < 3f)
				{
					this.fov = value;
				}
			}
		}

		public Vector3 GetTarget(float orbit)
		{
			var target = new Vector3();

			target.X = (float)(Math.Cos(this.Rotation.Y) * Math.Sin(this.Rotation.X)) + this.Position.X;
			target.Y = (float)(Math.Sin(this.Rotation.Y) * Math.Cos(this.Rotation.X)) + this.Position.Y;
			target.Z = (float)Math.Sin(this.Rotation.X) + this.Position.Z;

			return target;
		}

		public Vector3 ToTarget(Vector3 target)
		{
			var dif = this.Position - target;

			var dist = Math.Sqrt(dif.X * dif.X + dif.Y * dif.Y);

			var ret = new Vector3();
			ret.Z = this.Rotation.Z;

			ret.X = (float)-Math.Atan(dif.Z / dist);

			var rotY = (float)Math.Asin(dif.X / dist);
			if (dif.X > 0 && dif.Y < 0)
			{
				ret.Y = (float)Math.PI / 2 - rotY;
			}
			else if (dif.X > 0 && dif.Y > 0)
			{
				ret.Y = rotY;
			}
			else if (dif.X < 0 && dif.Y > 0)
			{
				ret.Y = rotY;
			}
			else if (dif.X < 0 && dif.Y < 0)
			{
				ret.Y = (float)Math.PI / 2 - rotY;
			}

			return ret;
		}

		public bool MovePositionTowards(Vector3 target, float maxDistanceDelta)
		{
			return this.MoveTowards(ref this.Position, target, maxDistanceDelta);
		}

		public bool MoveRotationTowards(Vector3 target, float maxDistanceDelta)
		{
			return this.MoveTowards(ref this.Rotation, target, maxDistanceDelta);
		}

		private bool MoveTowards(ref Vector3 source, Vector3 target, float maxDistanceDelta)
		{
			var toVector_x = (double)(target.X - source.X);
			var toVector_y = (double)(target.Y - source.Y);
			var toVector_z = (double)(target.Z - source.Z);

			var sqdist = toVector_x * toVector_x + toVector_y * toVector_y + toVector_z * toVector_z;

			if (sqdist == 0 || (maxDistanceDelta >= 0 && sqdist <= maxDistanceDelta * maxDistanceDelta))
			{
				source = target;
				return true;
			}
			var dist = (float)Math.Sqrt(sqdist);

			source = new Vector3(
				(float)(source.X + toVector_x / dist * maxDistanceDelta),
				(float)(source.Y + toVector_y / dist * maxDistanceDelta),
				(float)(source.Z + toVector_z / dist * maxDistanceDelta));

			return false;
		}

		public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
		{
			t = Clamp01(t);
			return new Vector3(a.X + (b.X - a.X) * t, a.Y + (b.Y - a.Y) * t, a.Z + (b.Z - a.Z) * t);
		}

		public static float Clamp01(float value)
		{
			if ((double)value < 0.0)
				return 0.0f;
			if ((double)value > 1.0)
				return 1f;
			return value;
		}

		public CameraViewModel ToViewModel()
		{
			var camera = new CameraViewModel();

			camera.X = this.Position.X;
			camera.Y = this.Position.Y;
			camera.Z = this.Position.Z;

			camera.Pitch = this.Rotation.X;
			camera.Yaw = this.Rotation.Y;
			camera.Roll = this.Rotation.Z;

			camera.Fov = this.Fov;

			camera.Time = this.Time;

			return camera;
		}
	}
}
