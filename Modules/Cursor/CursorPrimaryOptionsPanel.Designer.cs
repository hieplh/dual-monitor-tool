namespace DMT.Modules.Cursor
{
	partial class CursorPrimaryOptionsPanel
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.picPreview = new System.Windows.Forms.PictureBox();
			this.labelClickToAdd = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(335, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "This specifies the area you want to treat as your primary working area.";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 17);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(460, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "This is used by the sticky/locked cursor options to allow free movement into and " +
    "within this area.";
			// 
			// picPreview
			// 
			this.picPreview.Location = new System.Drawing.Point(3, 47);
			this.picPreview.Name = "picPreview";
			this.picPreview.Size = new System.Drawing.Size(494, 161);
			this.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.picPreview.TabIndex = 2;
			this.picPreview.TabStop = false;
			this.picPreview.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picPreview_MouseClick);
			// 
			// labelClickToAdd
			// 
			this.labelClickToAdd.AutoSize = true;
			this.labelClickToAdd.Location = new System.Drawing.Point(6, 232);
			this.labelClickToAdd.Name = "labelClickToAdd";
			this.labelClickToAdd.Size = new System.Drawing.Size(311, 13);
			this.labelClickToAdd.TabIndex = 3;
			this.labelClickToAdd.Text = "Click a monitor to add or remove it from the primary working area.";
			// 
			// label4
			// 
			this.label4.ForeColor = System.Drawing.Color.Red;
			this.label4.Location = new System.Drawing.Point(0, 330);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(497, 18);
			this.label4.TabIndex = 4;
			this.label4.Text = "warning";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.label4.Visible = false;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 245);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(383, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Monitors higlighted in blue are considered to be part of the primary working area" +
    ".";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(6, 258);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(491, 42);
			this.label5.TabIndex = 6;
			this.label5.Text = "If no monitors are selected, then only the primary screen will be considered to b" +
    "e in the primary working area.";
			// 
			// CursorPrimaryOptionsPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.labelClickToAdd);
			this.Controls.Add(this.picPreview);
			this.Name = "CursorPrimaryOptionsPanel";
			this.Size = new System.Drawing.Size(500, 360);
			((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox picPreview;
		private System.Windows.Forms.Label labelClickToAdd;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label5;


	}
}
