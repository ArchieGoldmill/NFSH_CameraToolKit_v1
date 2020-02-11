using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraController.CameraManager
{
	public class CameraViewModel
	{
		private int time;
		[DisplayName("Time(msec)")]
		public int Time
		{
			get => this.time;
			set
			{
				if (value < 1)
				{
					this.time = 1;
				}
				else
				{
					this.time = value;
				}
			}
		}

		public float X { get; set; }

		public float Y { get; set; }

		public float Z { get; set; }

		public float Pitch { get; set; }

		public float Yaw { get; set; }

		public float Roll { get; set; }

		private float fov;
		public float Fov
		{
			get => this.fov;
			set
			{
				if (value >= 0.00)
				{
					this.fov = value;
				}
			}
		}

		public Camera ToCamera()
		{
			var camera = new Camera();

			camera.Position.X = this.X;
			camera.Position.Y = this.Y;
			camera.Position.Z = this.Z;

			camera.Rotation.X = this.Pitch;
			camera.Rotation.Y = this.Yaw;
			camera.Rotation.Z = this.Roll;

			camera.Fov = this.Fov;

			camera.Time = this.Time;

			return camera;
		}
	}
}
