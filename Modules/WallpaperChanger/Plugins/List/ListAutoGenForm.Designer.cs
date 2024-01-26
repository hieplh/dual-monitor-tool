namespace DMT.Modules.WallpaperChanger.Plugins.List
{
	partial class ListAutoGenForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListAutoGenForm));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxListFnm = new System.Windows.Forms.TextBox();
			this.buttonBrowseList = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxFolder = new System.Windows.Forms.TextBox();
			this.buttonBrowseFolder = new System.Windows.Forms.Button();
			this.checkBoxRecursive = new System.Windows.Forms.CheckBox();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(604, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Creates a text file (specified by \'List Path\') containing a list of image files f" +
    "ound in the specified folder for use by the List provider.";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 46);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(51, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "List Path:";
			// 
			// textBoxListFnm
			// 
			this.textBoxListFnm.Location = new System.Drawing.Point(103, 43);
			this.textBoxListFnm.Name = "textBoxListFnm";
			this.textBoxListFnm.Size = new System.Drawing.Size(542, 20);
			this.textBoxListFnm.TabIndex = 2;
			// 
			// buttonBrowseList
			// 
			this.buttonBrowseList.Location = new System.Drawing.Point(651, 40);
			this.buttonBrowseList.Name = "buttonBrowseList";
			this.buttonBrowseList.Size = new System.Drawing.Size(75, 23);
			this.buttonBrowseList.TabIndex = 3;
			this.buttonBrowseList.Text = "Browse ...";
			this.buttonBrowseList.UseVisualStyleBackColor = true;
			this.buttonBrowseList.Click += new System.EventHandler(this.buttonBrowseList_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 72);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(39, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Folder:";
			// 
			// textBoxFolder
			// 
			this.textBoxFolder.Location = new System.Drawing.Point(103, 69);
			this.textBoxFolder.Name = "textBoxFolder";
			this.textBoxFolder.Size = new System.Drawing.Size(542, 20);
			this.textBoxFolder.TabIndex = 5;
			// 
			// buttonBrowseFolder
			// 
			this.buttonBrowseFolder.Location = new System.Drawing.Point(651, 67);
			this.buttonBrowseFolder.Name = "buttonBrowseFolder";
			this.buttonBrowseFolder.Size = new System.Drawing.Size(75, 23);
			this.buttonBrowseFolder.TabIndex = 6;
			this.buttonBrowseFolder.Text = "Browse ...";
			this.buttonBrowseFolder.UseVisualStyleBackColor = true;
			this.buttonBrowseFolder.Click += new System.EventHandler(this.buttonBrowseFolder_Click);
			// 
			// checkBoxRecursive
			// 
			this.checkBoxRecursive.AutoSize = true;
			this.checkBoxRecursive.Location = new System.Drawing.Point(103, 95);
			this.checkBoxRecursive.Name = "checkBoxRecursive";
			this.checkBoxRecursive.Size = new System.Drawing.Size(118, 17);
			this.checkBoxRecursive.TabIndex = 7;
			this.checkBoxRecursive.Text = "Look in sub-Folders";
			this.checkBoxRecursive.UseVisualStyleBackColor = true;
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(425, 134);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 13;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(279, 134);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 12;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// ListAutoGenForm
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(781, 169);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.checkBoxRecursive);
			this.Controls.Add(this.buttonBrowseFolder);
			this.Controls.Add(this.textBoxFolder);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.buttonBrowseList);
			this.Controls.Add(this.textBoxListFnm);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ListAutoGenForm";
			this.Text = "Auto Generate List";
			this.Load += new System.EventHandler(this.ListAutoGenForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxListFnm;
		private System.Windows.Forms.Button buttonBrowseList;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxFolder;
		private System.Windows.Forms.Button buttonBrowseFolder;
		private System.Windows.Forms.CheckBox checkBoxRecursive;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOK;
	}
}