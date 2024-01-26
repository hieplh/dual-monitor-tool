namespace DMT.Modules.WallpaperChanger.Plugins.List
{
	partial class ListForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListForm));
			this.textBoxDescription = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.numericUpDownWeight = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label4 = new System.Windows.Forms.Label();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.checkBoxPersist = new System.Windows.Forms.CheckBox();
			this.checkBoxEnabled = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.radioButtonOrderList = new System.Windows.Forms.RadioButton();
			this.radioButtonOrderRandom = new System.Windows.Forms.RadioButton();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxListFnm = new System.Windows.Forms.TextBox();
			this.buttonBrowse = new System.Windows.Forms.Button();
			this.buttonAutoGenerate = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownWeight)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBoxDescription
			// 
			this.textBoxDescription.Location = new System.Drawing.Point(158, 112);
			this.textBoxDescription.Name = "textBoxDescription";
			this.textBoxDescription.Size = new System.Drawing.Size(422, 20);
			this.textBoxDescription.TabIndex = 5;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 115);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(63, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Description:";
			// 
			// numericUpDownWeight
			// 
			this.numericUpDownWeight.Location = new System.Drawing.Point(158, 86);
			this.numericUpDownWeight.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numericUpDownWeight.Name = "numericUpDownWeight";
			this.numericUpDownWeight.Size = new System.Drawing.Size(120, 20);
			this.numericUpDownWeight.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 89);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(44, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Weight:";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::DMT.Properties.Resources.ListPlugin;
			this.pictureBox1.Location = new System.Drawing.Point(12, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(48, 48);
			this.pictureBox1.TabIndex = 27;
			this.pictureBox1.TabStop = false;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(107, 30);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(366, 13);
			this.label4.TabIndex = 0;
			this.label4.Text = "Image provider that reads a file contaning a list of paths and URLs to images";
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(416, 300);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 11;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(270, 300);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 10;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// checkBoxPersist
			// 
			this.checkBoxPersist.AutoSize = true;
			this.checkBoxPersist.Location = new System.Drawing.Point(6, 65);
			this.checkBoxPersist.Name = "checkBoxPersist";
			this.checkBoxPersist.Size = new System.Drawing.Size(214, 17);
			this.checkBoxPersist.TabIndex = 7;
			this.checkBoxPersist.Text = "Remember position between shutdowns";
			this.checkBoxPersist.UseVisualStyleBackColor = true;
			this.checkBoxPersist.CheckedChanged += new System.EventHandler(this.checkBoxPersist_CheckedChanged);
			// 
			// checkBoxEnabled
			// 
			this.checkBoxEnabled.AutoSize = true;
			this.checkBoxEnabled.Location = new System.Drawing.Point(316, 88);
			this.checkBoxEnabled.Name = "checkBoxEnabled";
			this.checkBoxEnabled.Size = new System.Drawing.Size(65, 17);
			this.checkBoxEnabled.TabIndex = 3;
			this.checkBoxEnabled.Text = "Enabled";
			this.checkBoxEnabled.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.radioButtonOrderList);
			this.groupBox2.Controls.Add(this.checkBoxPersist);
			this.groupBox2.Controls.Add(this.radioButtonOrderRandom);
			this.groupBox2.Location = new System.Drawing.Point(12, 177);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(263, 100);
			this.groupBox2.TabIndex = 28;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Ordering";
			// 
			// radioButtonOrderList
			// 
			this.radioButtonOrderList.AutoSize = true;
			this.radioButtonOrderList.Location = new System.Drawing.Point(6, 42);
			this.radioButtonOrderList.Name = "radioButtonOrderList";
			this.radioButtonOrderList.Size = new System.Drawing.Size(82, 17);
			this.radioButtonOrderList.TabIndex = 1;
			this.radioButtonOrderList.TabStop = true;
			this.radioButtonOrderList.Text = "In List Order";
			this.radioButtonOrderList.UseVisualStyleBackColor = true;
			this.radioButtonOrderList.CheckedChanged += new System.EventHandler(this.OnOrderRadioChanged);
			// 
			// radioButtonOrderRandom
			// 
			this.radioButtonOrderRandom.AutoSize = true;
			this.radioButtonOrderRandom.Location = new System.Drawing.Point(6, 19);
			this.radioButtonOrderRandom.Name = "radioButtonOrderRandom";
			this.radioButtonOrderRandom.Size = new System.Drawing.Size(65, 17);
			this.radioButtonOrderRandom.TabIndex = 0;
			this.radioButtonOrderRandom.TabStop = true;
			this.radioButtonOrderRandom.Text = "Random";
			this.radioButtonOrderRandom.UseVisualStyleBackColor = true;
			this.radioButtonOrderRandom.CheckedChanged += new System.EventHandler(this.OnOrderRadioChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 141);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(51, 13);
			this.label3.TabIndex = 16;
			this.label3.Text = "List Path:";
			// 
			// textBoxListFnm
			// 
			this.textBoxListFnm.Location = new System.Drawing.Point(158, 138);
			this.textBoxListFnm.Name = "textBoxListFnm";
			this.textBoxListFnm.Size = new System.Drawing.Size(422, 20);
			this.textBoxListFnm.TabIndex = 17;
			// 
			// buttonBrowse
			// 
			this.buttonBrowse.Location = new System.Drawing.Point(586, 136);
			this.buttonBrowse.Name = "buttonBrowse";
			this.buttonBrowse.Size = new System.Drawing.Size(75, 23);
			this.buttonBrowse.TabIndex = 18;
			this.buttonBrowse.Text = "Browse...";
			this.buttonBrowse.UseVisualStyleBackColor = true;
			this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
			// 
			// buttonAutoGenerate
			// 
			this.buttonAutoGenerate.Location = new System.Drawing.Point(667, 136);
			this.buttonAutoGenerate.Name = "buttonAutoGenerate";
			this.buttonAutoGenerate.Size = new System.Drawing.Size(124, 23);
			this.buttonAutoGenerate.TabIndex = 29;
			this.buttonAutoGenerate.Text = "Auto Generate...";
			this.buttonAutoGenerate.UseVisualStyleBackColor = true;
			this.buttonAutoGenerate.Click += new System.EventHandler(this.buttonAutoGenerate_Click);
			// 
			// ListForm
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(803, 344);
			this.Controls.Add(this.buttonAutoGenerate);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.checkBoxEnabled);
			this.Controls.Add(this.textBoxDescription);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.numericUpDownWeight);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonBrowse);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textBoxListFnm);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.buttonOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ListForm";
			this.ShowInTaskbar = false;
			this.Text = "List";
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownWeight)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBoxDescription;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown numericUpDownWeight;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.CheckBox checkBoxPersist;
		private System.Windows.Forms.CheckBox checkBoxEnabled;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton radioButtonOrderList;
		private System.Windows.Forms.RadioButton radioButtonOrderRandom;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxListFnm;
		private System.Windows.Forms.Button buttonBrowse;
		private System.Windows.Forms.Button buttonAutoGenerate;
	}
}