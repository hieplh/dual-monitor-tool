namespace DMT.Modules.General
{
	partial class EditNumericValueForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditNumericValueForm));
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.trackBarBrightness = new System.Windows.Forms.TrackBar();
			this.labelCurBrightness = new System.Windows.Forms.Label();
			this.labelMinBrightness = new System.Windows.Forms.Label();
			this.labelMaxBrightness = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.trackBarBrightness)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonOK
			// 
			resources.ApplyResources(this.buttonOK, "buttonOK");
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			resources.ApplyResources(this.buttonCancel, "buttonCancel");
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// trackBarBrightness
			// 
			resources.ApplyResources(this.trackBarBrightness, "trackBarBrightness");
			this.trackBarBrightness.Name = "trackBarBrightness";
			this.trackBarBrightness.Scroll += new System.EventHandler(this.trackBarBrightness_Scroll);
			// 
			// labelCurBrightness
			// 
			resources.ApplyResources(this.labelCurBrightness, "labelCurBrightness");
			this.labelCurBrightness.Name = "labelCurBrightness";
			// 
			// labelMinBrightness
			// 
			resources.ApplyResources(this.labelMinBrightness, "labelMinBrightness");
			this.labelMinBrightness.Name = "labelMinBrightness";
			// 
			// labelMaxBrightness
			// 
			resources.ApplyResources(this.labelMaxBrightness, "labelMaxBrightness");
			this.labelMaxBrightness.Name = "labelMaxBrightness";
			// 
			// BrightnessForm
			// 
			this.AcceptButton = this.buttonOK;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.Controls.Add(this.labelMaxBrightness);
			this.Controls.Add(this.labelMinBrightness);
			this.Controls.Add(this.labelCurBrightness);
			this.Controls.Add(this.trackBarBrightness);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "BrightnessForm";
			this.ShowInTaskbar = false;
			((System.ComponentModel.ISupportInitialize)(this.trackBarBrightness)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.TrackBar trackBarBrightness;
		private System.Windows.Forms.Label labelCurBrightness;
		private System.Windows.Forms.Label labelMinBrightness;
		private System.Windows.Forms.Label labelMaxBrightness;
	}
}