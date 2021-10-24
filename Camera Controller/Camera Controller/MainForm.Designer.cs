namespace CameraController
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.noPartRestrict = new System.Windows.Forms.CheckBox();
			this.vinylFix = new System.Windows.Forms.CheckBox();
			this.allPartsOptional = new System.Windows.Forms.CheckBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.resetBtn = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.RotationSpeed = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.MoveSpeed = new System.Windows.Forms.NumericUpDown();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.rotateLabel = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.mouseEnabled = new System.Windows.Forms.CheckBox();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.clearBtn = new System.Windows.Forms.Button();
			this.StopBtn = new System.Windows.Forms.Button();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.moveDownBtn = new System.Windows.Forms.Button();
			this.moveUpBtn = new System.Windows.Forms.Button();
			this.DeleteRowBtn = new System.Windows.Forms.Button();
			this.dataGridView = new System.Windows.Forms.DataGridView();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.RotationSpeed)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.MoveSpeed)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.tabPage3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
			this.statusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(725, 328);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.noPartRestrict);
			this.tabPage1.Controls.Add(this.vinylFix);
			this.tabPage1.Controls.Add(this.allPartsOptional);
			this.tabPage1.Controls.Add(this.label8);
			this.tabPage1.Controls.Add(this.label7);
			this.tabPage1.Controls.Add(this.resetBtn);
			this.tabPage1.Controls.Add(this.label6);
			this.tabPage1.Controls.Add(this.RotationSpeed);
			this.tabPage1.Controls.Add(this.label5);
			this.tabPage1.Controls.Add(this.MoveSpeed);
			this.tabPage1.Controls.Add(this.groupBox1);
			this.tabPage1.Controls.Add(this.mouseEnabled);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(717, 302);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Main";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// noPartRestrict
			// 
			this.noPartRestrict.AutoSize = true;
			this.noPartRestrict.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.noPartRestrict.Location = new System.Drawing.Point(203, 145);
			this.noPartRestrict.Name = "noPartRestrict";
			this.noPartRestrict.Size = new System.Drawing.Size(114, 17);
			this.noPartRestrict.TabIndex = 14;
			this.noPartRestrict.Text = "No part restrictions";
			this.noPartRestrict.UseVisualStyleBackColor = true;
			this.noPartRestrict.Visible = false;
			this.noPartRestrict.CheckedChanged += new System.EventHandler(this.NoPartRestrict_CheckedChanged);
			// 
			// vinylFix
			// 
			this.vinylFix.AutoSize = true;
			this.vinylFix.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.vinylFix.Location = new System.Drawing.Point(172, 175);
			this.vinylFix.Name = "vinylFix";
			this.vinylFix.Size = new System.Drawing.Size(145, 17);
			this.vinylFix.TabIndex = 13;
			this.vinylFix.Text = "No vinyl scale restrictions";
			this.vinylFix.UseVisualStyleBackColor = true;
			this.vinylFix.Visible = false;
			this.vinylFix.CheckedChanged += new System.EventHandler(this.VinylFix_CheckedChanged);
			// 
			// allPartsOptional
			// 
			this.allPartsOptional.AutoSize = true;
			this.allPartsOptional.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.allPartsOptional.Location = new System.Drawing.Point(214, 119);
			this.allPartsOptional.Name = "allPartsOptional";
			this.allPartsOptional.Size = new System.Drawing.Size(103, 17);
			this.allPartsOptional.TabIndex = 12;
			this.allPartsOptional.Text = "All parts optional";
			this.allPartsOptional.UseVisualStyleBackColor = true;
			this.allPartsOptional.Visible = false;
			this.allPartsOptional.CheckedChanged += new System.EventHandler(this.AllPartsOptional_CheckedChanged);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(509, 281);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(80, 13);
			this.label8.TabIndex = 11;
			this.label8.Text = "made by Archie";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(509, 255);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(191, 13);
			this.label7.TabIndex = 10;
			this.label7.Text = "Need for Speed: Heat - Camera Toolkit";
			// 
			// resetBtn
			// 
			this.resetBtn.Location = new System.Drawing.Point(371, 9);
			this.resetBtn.Name = "resetBtn";
			this.resetBtn.Size = new System.Drawing.Size(91, 28);
			this.resetBtn.TabIndex = 9;
			this.resetBtn.Text = "Reset position";
			this.resetBtn.UseVisualStyleBackColor = true;
			this.resetBtn.Click += new System.EventHandler(this.ResetBtn_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(169, 86);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(79, 13);
			this.label6.TabIndex = 8;
			this.label6.Text = "Rotation speed";
			// 
			// RotationSpeed
			// 
			this.RotationSpeed.DecimalPlaces = 2;
			this.RotationSpeed.Location = new System.Drawing.Point(254, 84);
			this.RotationSpeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
			this.RotationSpeed.Name = "RotationSpeed";
			this.RotationSpeed.Size = new System.Drawing.Size(77, 20);
			this.RotationSpeed.TabIndex = 7;
			this.RotationSpeed.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.RotationSpeed.ValueChanged += new System.EventHandler(this.RotationSpeed_ValueChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(169, 47);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(66, 13);
			this.label5.TabIndex = 6;
			this.label5.Text = "Move speed";
			// 
			// MoveSpeed
			// 
			this.MoveSpeed.DecimalPlaces = 2;
			this.MoveSpeed.Location = new System.Drawing.Point(254, 45);
			this.MoveSpeed.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.MoveSpeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
			this.MoveSpeed.Name = "MoveSpeed";
			this.MoveSpeed.Size = new System.Drawing.Size(77, 20);
			this.MoveSpeed.TabIndex = 5;
			this.MoveSpeed.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.MoveSpeed.ValueChanged += new System.EventHandler(this.MoveSpeed_ValueChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.rotateLabel);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(15, 6);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(148, 247);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Hotkeys";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(6, 85);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(75, 13);
			this.label9.TabIndex = 5;
			this.label9.Text = "Up/Down -  [ ]";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 28);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(61, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "F1 - Enable";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 169);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(55, 13);
			this.label4.TabIndex = 3;
			this.label4.Text = "FOV - B N";
			// 
			// rotateLabel
			// 
			this.rotateLabel.AutoSize = true;
			this.rotateLabel.ForeColor = System.Drawing.SystemColors.AppWorkspace;
			this.rotateLabel.Location = new System.Drawing.Point(6, 140);
			this.rotateLabel.Name = "rotateLabel";
			this.rotateLabel.Size = new System.Drawing.Size(84, 13);
			this.rotateLabel.TabIndex = 2;
			this.rotateLabel.Text = "Rotate -  7 8 9 0";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 113);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(52, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Tilt -  U O";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 59);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(76, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Move -  I J K L";
			// 
			// mouseEnabled
			// 
			this.mouseEnabled.AutoSize = true;
			this.mouseEnabled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.mouseEnabled.Checked = true;
			this.mouseEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
			this.mouseEnabled.Location = new System.Drawing.Point(169, 16);
			this.mouseEnabled.Name = "mouseEnabled";
			this.mouseEnabled.Size = new System.Drawing.Size(121, 17);
			this.mouseEnabled.TabIndex = 3;
			this.mouseEnabled.Text = "Use mouse to rotate";
			this.mouseEnabled.UseVisualStyleBackColor = true;
			this.mouseEnabled.CheckedChanged += new System.EventHandler(this.MouseEnabled_CheckedChanged);
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.clearBtn);
			this.tabPage3.Controls.Add(this.StopBtn);
			this.tabPage3.Controls.Add(this.label11);
			this.tabPage3.Controls.Add(this.label10);
			this.tabPage3.Controls.Add(this.moveDownBtn);
			this.tabPage3.Controls.Add(this.moveUpBtn);
			this.tabPage3.Controls.Add(this.DeleteRowBtn);
			this.tabPage3.Controls.Add(this.dataGridView);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(717, 302);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Montage";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// clearBtn
			// 
			this.clearBtn.Location = new System.Drawing.Point(71, 130);
			this.clearBtn.Name = "clearBtn";
			this.clearBtn.Size = new System.Drawing.Size(75, 23);
			this.clearBtn.TabIndex = 8;
			this.clearBtn.Text = "Clear";
			this.clearBtn.UseVisualStyleBackColor = true;
			this.clearBtn.Click += new System.EventHandler(this.ClearBtn_Click);
			// 
			// StopBtn
			// 
			this.StopBtn.Location = new System.Drawing.Point(71, 85);
			this.StopBtn.Name = "StopBtn";
			this.StopBtn.Size = new System.Drawing.Size(75, 23);
			this.StopBtn.TabIndex = 7;
			this.StopBtn.Text = "Stop";
			this.StopBtn.UseVisualStyleBackColor = true;
			this.StopBtn.Click += new System.EventHandler(this.StopBtn_Click);
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(8, 44);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(48, 13);
			this.label11.TabIndex = 6;
			this.label11.Text = "F3 - Play";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(8, 17);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(47, 13);
			this.label10.TabIndex = 5;
			this.label10.Text = "F2 - Add";
			// 
			// moveDownBtn
			// 
			this.moveDownBtn.Location = new System.Drawing.Point(71, 41);
			this.moveDownBtn.Name = "moveDownBtn";
			this.moveDownBtn.Size = new System.Drawing.Size(75, 23);
			this.moveDownBtn.TabIndex = 3;
			this.moveDownBtn.Text = "Move Down";
			this.moveDownBtn.UseVisualStyleBackColor = true;
			this.moveDownBtn.Click += new System.EventHandler(this.MoveDownBtn_Click);
			// 
			// moveUpBtn
			// 
			this.moveUpBtn.Location = new System.Drawing.Point(71, 12);
			this.moveUpBtn.Name = "moveUpBtn";
			this.moveUpBtn.Size = new System.Drawing.Size(75, 23);
			this.moveUpBtn.TabIndex = 2;
			this.moveUpBtn.Text = "Move Up";
			this.moveUpBtn.UseVisualStyleBackColor = true;
			this.moveUpBtn.Click += new System.EventHandler(this.MoveUpBtn_Click);
			// 
			// DeleteRowBtn
			// 
			this.DeleteRowBtn.Location = new System.Drawing.Point(71, 159);
			this.DeleteRowBtn.Name = "DeleteRowBtn";
			this.DeleteRowBtn.Size = new System.Drawing.Size(75, 23);
			this.DeleteRowBtn.TabIndex = 1;
			this.DeleteRowBtn.Text = "Delete";
			this.DeleteRowBtn.UseVisualStyleBackColor = true;
			this.DeleteRowBtn.Click += new System.EventHandler(this.DeleteRowBtn_Click);
			// 
			// dataGridView
			// 
			this.dataGridView.AllowDrop = true;
			this.dataGridView.AllowUserToAddRows = false;
			this.dataGridView.AllowUserToResizeRows = false;
			this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView.Location = new System.Drawing.Point(168, 3);
			this.dataGridView.MultiSelect = false;
			this.dataGridView.Name = "dataGridView";
			this.dataGridView.RowHeadersVisible = false;
			this.dataGridView.Size = new System.Drawing.Size(541, 293);
			this.dataGridView.TabIndex = 0;
			this.dataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.DataGridView_DataError);
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
			this.statusStrip.Location = new System.Drawing.Point(0, 331);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(725, 22);
			this.statusStrip.TabIndex = 1;
			this.statusStrip.Text = "statusStrip1";
			// 
			// statusLabel
			// 
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new System.Drawing.Size(52, 17);
			this.statusLabel.Text = "Disabled";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(725, 353);
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.tabControl1);
			this.Cursor = System.Windows.Forms.Cursors.Default;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(2);
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "NFS: Heat - Camera Toolkit";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.RotationSpeed)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.MoveSpeed)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.tabPage3.ResumeLayout(false);
			this.tabPage3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel statusLabel;
		private System.Windows.Forms.CheckBox mouseEnabled;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label rotateLabel;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown MoveSpeed;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.NumericUpDown RotationSpeed;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.DataGridView dataGridView;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button StopBtn;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Button moveDownBtn;
		private System.Windows.Forms.Button moveUpBtn;
		private System.Windows.Forms.Button DeleteRowBtn;
		private System.Windows.Forms.Button resetBtn;
		private System.Windows.Forms.Button clearBtn;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.CheckBox allPartsOptional;
		private System.Windows.Forms.CheckBox vinylFix;
		private System.Windows.Forms.CheckBox noPartRestrict;
	}
}

