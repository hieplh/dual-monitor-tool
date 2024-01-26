using DMT.Library.GuiUtils;
using DMT.Library.Utils;
using DMT.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DMT.Modules.WallpaperChanger.Plugins.List
{
	public partial class ListAutoGenForm : Form
	{
		/// <summary>
		/// Sets the initial list fnm to display
		/// Gets the user entered list fnm
		/// </summary>
		public string ListFnm { get; set; }

		public ListAutoGenForm()
		{
			InitializeComponent();
		}


		private void ListAutoGenForm_Load(object sender, EventArgs e)
		{
			textBoxListFnm.Text = ListFnm;
		}


		private void buttonBrowseList_Click(object sender, EventArgs e)
		{
			string sel = SelectListFnm(textBoxListFnm.Text);
			if (sel != null)
			{
				textBoxListFnm.Text = sel;
			}
		}

		private void buttonBrowseFolder_Click(object sender, EventArgs e)
		{
			string sel = SelectFolder(textBoxFolder.Text);
			if (sel != null)
			{
				textBoxFolder.Text = sel;
			}

		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			string source = textBoxFolder.Text;
			string destination = textBoxListFnm.Text;
			bool recursive = checkBoxRecursive.Checked;

			// validation
			if (!Directory.Exists(source))
			{
				string msg = string.Format(CommonStrings.FolderDoesNotExist, source);
				MessageBox.Show(msg, CommonStrings.MyTitle);
				return;
			}

			if (File.Exists(destination))
			{
				string msg = string.Format(CommonStrings.FileExists, destination);
				if (MessageBox.Show(
					msg,
					CommonStrings.MyTitle,
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question,
					MessageBoxDefaultButton.Button2) == DialogResult.No)
				{
					// user doesn't want to overwrite existing file
					return;
				}

				// could test if the directory is valid here,
				// but we'll wait till we actually create the file for that
			}

			try
			{
				// ALT: use File.WriteAllLines(), but we want to check we can write to destination
				// before scanning the directory
				using (StreamWriter writer = new StreamWriter(destination))
				{
					// TODO: consider putting this in a thread 
					// in case we are scanning large directories, or accessing them remotely

					List<string> filenames = DirectoryScanner.GetFilenames(source, recursive, DirectoryScanner.IsImageFile);
					foreach (string filename in filenames)
					{
						writer.WriteLine(filename);
					}
				}

				// if we get here, all should be good
				// TODO: indicate to the user how many iamges are in the list?
				ListFnm = textBoxListFnm.Text;
				DialogResult = DialogResult.OK;
				Close();
			}
			catch (Exception ex)
			{
				string msg = string.Format(CommonStrings.FileWriteError, destination, ex.Message);
			}
		}


		string SelectListFnm(string initialFnm)
		{
			string ret = null;

			// use SaveFileDialog to select a file
			SaveFileDialog dlg = new SaveFileDialog();
			FileSelectionHelper.SetInitialFileNameInDialog(dlg, initialFnm);
			dlg.CheckPathExists = true;
			dlg.DefaultExt = "txt";
			dlg.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
			dlg.FilterIndex = 1;
			if (dlg.ShowDialog(this) == DialogResult.OK)
			{
				ret = dlg.FileName;
			}

			return ret;
		}

		string SelectFolder(string initialFolder)
		{
			string ret = null;

			// use FolderBrowserDialog to select a folder
			FolderBrowserDialog dlg = new FolderBrowserDialog();
			dlg.RootFolder = Environment.SpecialFolder.Desktop;
			dlg.SelectedPath = initialFolder;
			if (dlg.ShowDialog(this) == DialogResult.OK)
			{
				ret = dlg.SelectedPath;
			}

			return ret;
		}

	}
}
