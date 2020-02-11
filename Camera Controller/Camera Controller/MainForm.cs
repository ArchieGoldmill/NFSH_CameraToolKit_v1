using CameraController.CameraManager;
using Memory;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace CameraController
{
	public partial class MainForm : Form
	{
		[System.Runtime.InteropServices.DllImport("user32.dll")]
		private static extern IntPtr GetForegroundWindow();

		[System.Runtime.InteropServices.DllImport("user32.dll")]
		private static extern int GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

		private IntPtr GetWindowHanlde()
		{
			IntPtr hWnd = IntPtr.Zero;
			foreach (Process pList in Process.GetProcesses())
			{
				if (pList.ProcessName.Contains(ProcessName))
				{
					hWnd = pList.MainWindowHandle;
				}
			}
			return hWnd;
		}

		private bool IsGameFocussed()
		{
			return GetForegroundWindow() == WndHandle;
		}

		private GlobalKeyboardHook globalKeyboardHook;
		private GlobalMouseHook globalMouseHook;

		private MemoryManager memoryManager;

		private Thread keyThread;

		private bool stopKeyThread = false;

		private const string ProcessName = "NeedForSpeedHeat";
		private IntPtr WndHandle;

		private Camera camera = new Camera { Fov = 0.6f };

		private bool enabled = false;
		private bool wasEnabled = false;

		private float rotationSpeed;
		private float moveSpeed;

		private BindingList<CameraViewModel> camList = new BindingList<CameraViewModel>();

		private bool play = false;
		private bool read = true;

		public static float deltaTime;

		public MainForm()
		{
			InitializeComponent();

			WndHandle = GetWindowHanlde();

			this.globalKeyboardHook = new GlobalKeyboardHook();

			this.memoryManager = new MemoryManager(ProcessName);

			this.globalMouseHook = new GlobalMouseHook(this.Handle);
			this.globalMouseHook.MouseMove += (snd, ev) =>
			{
				if (this.mouseEnabled.Checked && enabled && IsGameFocussed() && !play)
				{
					float speed = this.rotationSpeed / 10000f;
					this.camera.Rotation.Y -= ev.X * speed;
					this.camera.Rotation.X -= ev.Y * speed;
				}
			};

			this.globalKeyboardHook.KeyDown += OneKeyDown;

			this.globalKeyboardHook.HookedKeys.Add(Keys.F1);
			this.globalKeyboardHook.HookedKeys.Add(Keys.F2);
			this.globalKeyboardHook.HookedKeys.Add(Keys.F3);

			this.keyThread = new Thread(this.Loop);
			this.keyThread.Start();

			this.FormClosing += (snd, ea) =>
			{
				this.ToggleInGameCamera(false);
			};

			this.RotationSpeed_ValueChanged(null, null);
			this.MoveSpeed_ValueChanged(null, null);

			this.dataGridView.AutoGenerateColumns = true;
			this.dataGridView.DataSource = camList;

			this.memoryManager.WriteFloat(new IntPtr(Pointers.LOD), 2f);
		}

		protected override void WndProc(ref Message message)
		{
			if (message.Msg == 0x00FF)
			{
				this.globalMouseHook.hookProc(message.LParam);
			}

			base.WndProc(ref message);
		}

		private bool IsKeyPressed(Keys key)
		{
			short retVal = ExternalDllMethods.GetKeyState((int)key);

			return (retVal & 0x8000) == 0x8000;
		}

		private Camera targetCam { get => this.playCams[targetCamNum]; }
		private Camera prevCam { get => this.playCams[targetCamNum - 1]; }
		private int targetCamNum;
		private float timeAcc = 0;

		private void PlayUpdate()
		{
			var time = (float)this.targetCam.Time;

			var t = timeAcc / time;
			this.camera.Position = Camera.Lerp(prevCam.Position, targetCam.Position, t);
			this.camera.Rotation = Camera.Lerp(prevCam.Rotation, targetCam.Rotation, t);

			this.camera.Fov = (targetCam.Fov - prevCam.Fov) * t + prevCam.Fov;

			timeAcc += deltaTime;
			if (timeAcc >= time)
			{
				timeAcc = 0;
				this.targetCamNum++;
				if (targetCamNum == this.playCams.Count)
				{
					this.play = false;
					return;
				}
			}
		}

		private void Render()
		{
			this.memoryManager.WriteFloatMultiLevel(Pointers.FovPtr, this.camera.Fov);

			RenderTransofm();
		}

		private static long nanoTime()
		{
			long nano = 10000L * Stopwatch.GetTimestamp();
			nano /= TimeSpan.TicksPerMillisecond;
			nano *= 100L;
			return nano;
		}

		public void Loop()
		{
			long lastLoopTime = nanoTime();
			int TARGET_FPS = 60 * 2;
			long OPTIMAL_TIME = 1000000000 / TARGET_FPS;

			while (!stopKeyThread)
			{
				long now = nanoTime();
				long updateLength = now - lastLoopTime;
				lastLoopTime = now;
				deltaTime = updateLength / 1000000f;

				this.DoWork();

				try
				{
					var sleep = (int)((lastLoopTime - nanoTime() + OPTIMAL_TIME) / 1000000D);
					if (sleep > 1000)
					{
						throw new Exception();
					}

					if (sleep > 0)
					{
						Thread.Sleep(sleep);
					}
				}
				catch (Exception ex)
				{
				}
			}
		}

		private void DoWork()
		{
			if (this.enabled)
			{
				// Read
				if (!this.play && this.read)
				{
					this.Read();
				}

				// Update
				if (this.play)
				{
					this.PlayUpdate();
				}
				else
				{
					this.UpdateCoordinates();
				}

				// Write
				this.Render();
			}
		}

		private void OneKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F1)
			{
				this.enabled = !this.enabled;
				Log("Enable = " + this.enabled);

				this.ToggleInGameCamera(this.enabled);
				if (this.enabled)
				{
					if (this.wasEnabled)
					{
						this.RenderPosition();
					}
					this.statusLabel.Text = "Enabled";
					this.wasEnabled = true;
				}
				else
				{
					this.statusLabel.Text = "Disabled";
				}
			}

			if (e.KeyCode == Keys.F2)
			{
				if (this.enabled)
				{
					var cam = this.camera.ToViewModel();
					if (this.camList.Any())
					{
						cam.Time = 3000;
					}

					this.camList.Add(cam);
					this.DisableFirstTimeCell();
				}
			}

			if (e.KeyCode == Keys.F3)
			{
				this.Play();
			}
		}

		private void DisableFirstTimeCell()
		{
			try
			{
				var cell = this.dataGridView.Rows[0].Cells[0];

				cell.Value = 0f;
				cell.ReadOnly = true;
				cell.Style.BackColor = SystemColors.AppWorkspace;
			}
			catch (Exception e)
			{
#if DEBUG
				MessageBox.Show(e.Message);
#endif
			}
		}

		private IntPtr? allocatedMem;

		private void ToggleInGameCamera(bool enable)
		{
			if (enable)
			{
				// 
				var bytes = new byte[] { 0x52, 0x48, 0xBA, 0x70, 0x26, 0x17, 0x14, 0x00, 0x00, 0x00, 0x00, 0x48, 0x39, 0xD1, 0x74, 0x28, 0x0F,
				0x29, 0x81, 0xA0, 0x00, 0x00, 0x00, 0x0F, 0x28, 0x48, 0x10, 0x0F, 0x29, 0x89, 0xB0, 0x00, 0x00, 0x00, 0x0F, 0x28, 0x40,
				0x20, 0x0F, 0x29, 0x81, 0xC0, 0x00, 0x00, 0x00, 0x0F, 0x28, 0x48, 0x30, 0x0F, 0x29, 0x89, 0xD0, 0x00, 0x00, 0x00, 0x5A, 0x5B, 0xC3 };

				var jmpBytes = new byte[] { 0x53, 0x48, 0xBB, 0x50, 0x55, 0x14, 0x14, 0x00, 0x00, 0x00, 0x00, 0xFF, 0xE3 };

				if (!allocatedMem.HasValue)
				{
					allocatedMem = this.memoryManager.AllocateMemory(bytes.Length);
				}

				var cptr = this.memoryManager.GetAbsoluteAddress(Pointers.MainObj);
				var camPtr = LongToBytes(cptr.ToInt64());
				for (int i = 0; i < 8; i++)
				{
					bytes[i + 3] = camPtr[i];
				}
				this.memoryManager.WriteBytes(allocatedMem.Value, bytes);

				var jmpPtr = LongToBytes(allocatedMem.Value.ToInt64());
				for (int i = 0; i < 8; i++)
				{
					jmpBytes[i + 3] = jmpPtr[i];
				}
				this.memoryManager.WriteBytes(new IntPtr(Pointers.CamCodeCave), jmpBytes);
			}
			else
			{
				//
				if (allocatedMem.HasValue)
				{
					this.memoryManager.WriteBytes(allocatedMem.Value + 0xE, new byte[] { 0x90, 0x90 });
				}
			}

			ToggleFov(enable);
		}

		private void ToggleFov(bool enable)
		{
			if (enable)
			{
				this.memoryManager.WriteBytes(new IntPtr(Pointers.FovCodePtr), new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 });
			}
			else
			{
				this.memoryManager.WriteBytes(new IntPtr(Pointers.FovCodePtr), new byte[] { 0xF3, 0x0F, 0x11, 0x87, 0x68, 0x11, 0x00, 0x00 });
			}
		}

		private byte[] LongToBytes(long val)
		{
			var intBytes = BitConverter.GetBytes(val);
			var result = intBytes;

			return result;
		}

		private void Read()
		{
			this.camera.Position.X = this.memoryManager.ReadFloatMultiLevel(Pointers.PosX);
			this.camera.Position.Y = this.memoryManager.ReadFloatMultiLevel(Pointers.PosY);
			this.camera.Position.Z = this.memoryManager.ReadFloatMultiLevel(Pointers.PosZ);
		}

		private void UpdateCoordinates()
		{
			// rotation
			float rotStep = this.rotationSpeed * deltaTime / 5000f;

			if (IsKeyPressed(Keys.U))
			{
				this.camera.Rotation.Z += rotStep;
			}

			if (IsKeyPressed(Keys.O))
			{
				this.camera.Rotation.Z -= rotStep;
			}

			if (!this.mouseEnabled.Checked)
			{
				if (IsKeyPressed(Keys.D7))
				{
					this.camera.Rotation.Y += rotStep;
				}

				if (IsKeyPressed(Keys.D8))
				{
					this.camera.Rotation.Y -= rotStep;
				}

				if (IsKeyPressed(Keys.D9))
				{
					this.camera.Rotation.X -= rotStep;
				}
				if (IsKeyPressed(Keys.D0))
				{
					this.camera.Rotation.X += rotStep;
				}
			}

			// position
			float step = this.moveSpeed * deltaTime / 1000f;

			if (IsKeyPressed(Keys.J))
			{
				this.camera.Position.Y += step * (float)(Math.Sin(this.camera.Rotation.Y) * Math.Cos(this.camera.Rotation.X));
				this.camera.Position.X -= step * (float)(Math.Cos(this.camera.Rotation.Y) * Math.Cos(this.camera.Rotation.X));
			}
			if (IsKeyPressed(Keys.L))
			{
				this.camera.Position.Y -= step * (float)(Math.Sin(this.camera.Rotation.Y) * Math.Cos(this.camera.Rotation.X));
				this.camera.Position.X += step * (float)(Math.Cos(this.camera.Rotation.Y) * Math.Cos(this.camera.Rotation.X));
			}

			if (IsKeyPressed(Keys.I))
			{
				this.camera.Position.X -= step * (float)(Math.Sin(this.camera.Rotation.Y) * Math.Cos(this.camera.Rotation.X));
				this.camera.Position.Y -= step * (float)(Math.Cos(this.camera.Rotation.Y) * Math.Cos(this.camera.Rotation.X));
				this.camera.Position.Z += step * (float)(Math.Sin(this.camera.Rotation.X));
			}
			if (IsKeyPressed(Keys.K))
			{
				this.camera.Position.X += step * (float)(Math.Sin(this.camera.Rotation.Y) * Math.Cos(this.camera.Rotation.X));
				this.camera.Position.Y += step * (float)(Math.Cos(this.camera.Rotation.Y) * Math.Cos(this.camera.Rotation.X));
				this.camera.Position.Z -= step * (float)(Math.Sin(this.camera.Rotation.X));
			}

			if (IsKeyPressed(Keys.OemCloseBrackets))
			{
				this.camera.Position.Z += step;
			}
			if (IsKeyPressed(Keys.OemOpenBrackets))
			{
				this.camera.Position.Z -= step;
			}

			// fov
			float fovStep = 0.0005f * deltaTime;
			if (IsKeyPressed(Keys.N))
			{
				this.camera.Fov += fovStep;
			}
			if (IsKeyPressed(Keys.B))
			{
				this.camera.Fov -= fovStep;
			}
		}

		private void RenderPosition()
		{
			this.memoryManager.WriteFloatMultiLevel(Pointers.PosX, this.camera.Position.X);
			this.memoryManager.WriteFloatMultiLevel(Pointers.PosY, this.camera.Position.Y);
			this.memoryManager.WriteFloatMultiLevel(Pointers.PosZ, this.camera.Position.Z);
		}

		byte[] renderBuf = new byte[64];
		float[,] renderMatrix = new float[4, 4];
		private void RenderTransofm()
		{
			var matrix = RotationHelper.ConvertAnglesToMatrix(this.camera.Position, this.camera.Rotation);

			renderMatrix[0, 0] = matrix.M11;
			renderMatrix[0, 1] = matrix.M12;
			renderMatrix[0, 2] = matrix.M13;
			renderMatrix[0, 3] = matrix.M14;

			renderMatrix[1, 0] = matrix.M21;
			renderMatrix[1, 1] = matrix.M22;
			renderMatrix[1, 2] = matrix.M23;
			renderMatrix[1, 3] = matrix.M24;

			renderMatrix[2, 0] = matrix.M31;
			renderMatrix[2, 1] = matrix.M32;
			renderMatrix[2, 2] = matrix.M33;
			renderMatrix[2, 3] = matrix.M34;

			renderMatrix[3, 0] = matrix.M41;
			renderMatrix[3, 1] = matrix.M42;
			renderMatrix[3, 2] = matrix.M43;
			renderMatrix[3, 3] = matrix.M44;

			int buffCount = 0;
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					var bytes = BitConverter.GetBytes(renderMatrix[i, j]);
					renderBuf[buffCount] = bytes[0];
					renderBuf[buffCount + 1] = bytes[1];
					renderBuf[buffCount + 2] = bytes[2];
					renderBuf[buffCount + 3] = bytes[3];

					buffCount += 4;
				}
			}

			this.memoryManager.WriteBytesMultiLevel(Pointers.Rotation, renderBuf);
		}

		private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.stopKeyThread = true;
		}

		private void MouseEnabled_CheckedChanged(object sender, EventArgs e)
		{
			if (this.mouseEnabled.Checked)
			{
				this.rotateLabel.ForeColor = SystemColors.AppWorkspace;
			}
			else
			{
				this.rotateLabel.ForeColor = Color.Black;
			}
		}

		private void MoveSpeed_ValueChanged(object sender, EventArgs e)
		{
			this.moveSpeed = (float)this.MoveSpeed.Value;
		}

		private void RotationSpeed_ValueChanged(object sender, EventArgs e)
		{
			this.rotationSpeed = (float)this.RotationSpeed.Value;
		}

		private void MoveUpBtn_Click(object sender, EventArgs e)
		{
			var cells = this.dataGridView.SelectedCells;
			if (cells.Count == 0)
			{
				return;
			}

			var cell = cells[0];

			var i = cell.RowIndex;
			if (i == 0)
			{
				return;
			}

			var temp = this.camList[i - 1];
			this.camList[i - 1] = this.camList[i];
			this.camList[i] = temp;

			if (i - 1 == 0)
			{
				this.camList[1].Time = this.camList[0].Time;
			}

			this.DisableFirstTimeCell();

			this.SelectCell(i - 1, cell.ColumnIndex);
		}

		private void SelectCell(int row, int col)
		{
			this.dataGridView.ClearSelection();

			this.dataGridView.CurrentCell = this.dataGridView.Rows[row].Cells[col];
		}

		private void MoveDownBtn_Click(object sender, EventArgs e)
		{
			var cells = this.dataGridView.SelectedCells;
			if (cells.Count == 0)
			{
				return;
			}

			var cell = cells[0];

			var i = cell.RowIndex;
			if (i == this.camList.Count - 1)
			{
				return;
			}

			var temp = this.camList[i + 1];
			this.camList[i + 1] = this.camList[i];
			this.camList[i] = temp;

			if (i == 0)
			{
				this.camList[1].Time = this.camList[0].Time;
			}

			this.DisableFirstTimeCell();

			this.SelectCell(i + 1, cell.ColumnIndex);
		}

		private void DeleteRowBtn_Click(object sender, EventArgs e)
		{
			var cells = this.dataGridView.SelectedCells;
			if (cells.Count == 0)
			{
				return;
			}

			var cell = cells[0];

			this.camList.Remove(this.camList[cell.RowIndex]);

			if (this.camList.Count != 0)
			{
				this.DisableFirstTimeCell();
			}
		}

		private void DataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			if (e.Exception.GetType().ToString().Equals("System.FormatException"))
			{
				MessageBox.Show("Cannot parse given value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				e.ThrowException = false;
			}
			else
			{
				e.ThrowException = true;
			}
		}

		private List<Camera> playCams;

		private void Play()
		{
			if (this.camList.Count <= 1 || play)
			{
				return;
			}

			this.read = false;

			playCams = this.camList.Select(x => x.ToCamera()).ToList();

			this.camera.CopyFrom(this.playCams[0]);
			this.Render();

			this.targetCamNum = 1;

			this.timeAcc = 0;

			this.play = true;
			this.read = true;

		}

		public static Vector3 Vector3Lerp(Vector3 a, Vector3 b, float t)
		{
			t = Clamp01(t);
			return new Vector3(a.X + (b.X - a.X) * t, a.Y + (b.Y - a.Y) * t, a.Z + (b.Z - a.Z) * t);
		}

		public static Vector3[] MakeSmoothCurve(Vector3[] arrayToCurve, float smoothness)
		{
			List<Vector3> points;
			List<Vector3> curvedPoints;
			int pointsLength = 0;
			int curvedLength = 0;

			if (smoothness < 1.0f) smoothness = 1.0f;

			pointsLength = arrayToCurve.Length;

			curvedLength = (pointsLength * (int)Math.Round(smoothness)) - 1;
			curvedPoints = new List<Vector3>(curvedLength);

			float t = 0.0f;
			for (int pointInTimeOnCurve = 0; pointInTimeOnCurve < curvedLength + 1; pointInTimeOnCurve++)
			{
				t = InverseLerp(0, curvedLength, pointInTimeOnCurve);

				points = new List<Vector3>(arrayToCurve);

				for (int j = pointsLength - 1; j > 0; j--)
				{
					for (int i = 0; i < j; i++)
					{
						points[i] = (1 - t) * points[i] + t * points[i + 1];
					}
				}

				curvedPoints.Add(points[0]);
			}

			return (curvedPoints.ToArray());
		}

		public static float InverseLerp(float a, float b, float value)
		{
			if ((double)a != (double)b)
				return Clamp01((float)(((double)value - (double)a) / ((double)b - (double)a)));
			return 0.0f;
		}

		public static float Clamp01(float value)
		{
			if ((double)value < 0.0)
				return 0.0f;
			if ((double)value > 1.0)
				return 1f;
			return value;
		}

		private void Log(string msg)
		{
#if DEBUG
			Console.WriteLine(msg);
#endif
		}

		private async void ResetBtn_Click(object sender, EventArgs e)
		{
			if (!enabled)
			{
				return;
			}

			await Task.Run(async () =>
			{
				this.ToggleInGameCamera(false);
				await Task.Delay(100);
				this.Read();
				this.ToggleInGameCamera(true);
			});
		}

		private void ClearBtn_Click(object sender, EventArgs e)
		{
			this.camList.Clear();
		}

		private void StopBtn_Click(object sender, EventArgs e)
		{
			this.play = false;
		}

		private void AllPartsOptional_CheckedChanged(object sender, EventArgs e)
		{
			if (this.allPartsOptional.Checked)
			{
				this.memoryManager.WriteBytes(new IntPtr(Pointers.AllPartsOptional), new byte []{ 0x90, 0x90 });
			}
			else
			{
				this.memoryManager.WriteBytes(new IntPtr(Pointers.AllPartsOptional), new byte[] { 0x74, 0x16 });
			}
		}

		private void VinylFix_CheckedChanged(object sender, EventArgs e)
		{
			if (this.vinylFix.Checked)
			{
				this.memoryManager.WriteByte(new IntPtr(Pointers.VinylsPtr), 0xEB);
			}
			else
			{
				this.memoryManager.WriteByte(new IntPtr(Pointers.VinylsPtr), 0x76);
			}
		}

		private void NoPartRestrict_CheckedChanged(object sender, EventArgs e)
		{
			if (this.noPartRestrict.Checked)
			{
				this.memoryManager.WriteBytes(new IntPtr(Pointers.NoPartRestrictions1), new byte[] { 0x90, 0x90 });
				this.memoryManager.WriteBytes(new IntPtr(Pointers.NoPartRestrictions2), new byte[] { 0x90, 0x90 });
			}
			else
			{
				this.memoryManager.WriteBytes(new IntPtr(Pointers.NoPartRestrictions1), new byte[] { 0x74, 0x0F });
				this.memoryManager.WriteBytes(new IntPtr(Pointers.NoPartRestrictions2), new byte[] { 0x74, 0x0C });
			}
		}
	}
}
