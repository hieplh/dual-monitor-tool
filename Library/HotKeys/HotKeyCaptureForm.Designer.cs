namespace DMT.Library.HotKeys
{
	partial class HotKeyCaptureForm
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
			this.buttonCancel = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxCombo = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(412, 226);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 0;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.buttonCancel_KeyDown);
			this.buttonCancel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.buttonCancel_KeyPress);
			this.buttonCancel.KeyUp += new System.Windows.Forms.KeyEventHandler(this.buttonCancel_KeyUp);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(28, 26);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(172, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Press required hot key combination";
			// 
			// textBoxCombo
			// 
			this.textBoxCombo.Enabled = false;
			this.textBoxCombo.Location = new System.Drawing.Point(49, 115);
			this.textBoxCombo.Name = "textBoxCombo";
			this.textBoxCombo.Size = new System.Drawing.Size(355, 20);
			this.textBoxCombo.TabIndex = 2;
			this.textBoxCombo.TabStop = false;
			// 
			// HotKeyCaptureForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(499, 261);
			this.Controls.Add(this.textBoxCombo);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonCancel);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "HotKeyCaptureForm";
			this.Text = "HotKeyCaptureForm";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HotKeyCaptureForm_KeyDown);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.HotKeyCaptureForm_KeyUp);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxCombo;
	}
}