#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2022  Gerald Evans
// 
// Dual Monitor Tools is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
#endregion

namespace DMT.Modules.WallpaperChanger.Plugins.List
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Data;
	using System.Drawing;
	using System.IO;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.Windows.Forms;

	using DMT.Library.GuiUtils;
	using DMT.Resources;

	/// <summary>
	/// This is used to edit the configuration of a local disk provider.
	/// </summary>
	public partial class ListForm : Form
	{
		/// <summary>
		/// Initialises a new instance of the <see cref="LocalDiskForm" /> class.
		/// </summary>
		/// <param name="config">Configuration to be edited</param>
		public ListForm(ListConfig config)
		{
			InitializeComponent();

			checkBoxEnabled.Checked = config.Enabled;
			numericUpDownWeight.Value = config.Weight;
			textBoxDescription.Text = config.Description;
			textBoxListFnm.Text = config.ListFnm;
			ShowOrderType(config.Type); 
			checkBoxPersist.Checked = config.Persist;
		}

		/// <summary>
		/// Gets the current (possibly edited) configuration
		/// </summary>
		/// <returns>Edited configuration</returns>
		public ListConfig GetConfig()
		{
			// ALT: could save original config and update it directly
			ListConfig config = new ListConfig();
			config.Enabled = checkBoxEnabled.Checked;
			config.Weight = (int)numericUpDownWeight.Value;
			config.Description = textBoxDescription.Text;
			config.ListFnm = textBoxListFnm.Text;
			config.Type = GetOrderType();
			config.Persist = checkBoxPersist.Checked;

			return config;
		}


		private void buttonBrowse_Click(object sender, EventArgs e)
		{
			string sel = SelectListFnm(textBoxListFnm.Text);
			if (sel != null)
			{
				textBoxListFnm.Text = sel;
			}
		}

		private void buttonAutoGenerate_Click(object sender, EventArgs e)
		{
			ListAutoGenForm dlg = new ListAutoGenForm();
			dlg.ListFnm = textBoxListFnm.Text;
			if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				textBoxListFnm.Text = dlg.ListFnm;
			}
		}

		private void buttonAbout_Click(object sender, EventArgs e)
		{
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			// TODO: validation

			DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
		}

		string SelectListFnm(string initialFnm)
		{
			string ret = null;

			// use OpenFileDialog to select a file
			OpenFileDialog dlg = new OpenFileDialog();
			FileSelectionHelper.SetInitialFileNameInDialog(dlg, initialFnm);
			dlg.CheckPathExists = true;
			dlg.CheckFileExists = true;
			if (dlg.ShowDialog(this) == DialogResult.OK)
			{
				ret = dlg.FileName;
			}

			return ret;
		}

		private void checkBoxPersist_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxPersist.Checked)
			{
				//
			}
		}

		void ShowOrderType(ListConfig.OrderType orderType)
		{
			switch (orderType)
			{
				case ListConfig.OrderType.Random:
					radioButtonOrderRandom.Checked = true;
					break;

				case ListConfig.OrderType.List:
					radioButtonOrderList.Checked = true;
					break;
			}
		}

		ListConfig.OrderType GetOrderType()
		{
			if (radioButtonOrderRandom.Checked)
			{
				return ListConfig.OrderType.Random;
			}
			else if (radioButtonOrderList.Checked)
			{
				return ListConfig.OrderType.List;
			}
			else
			{
				// shouldn't get here, but jic
				return ListConfig.OrderType.Random;
			}
		}

		//void ShowCheckBox(CheckBox checkBox, bool show)
		//{
		//	if (show)
		//	{
		//		checkBox.Visible = true;
		//		checkBox.Enabled = true;
		//	}
		//	else
		//	{
		//		checkBox.Enabled = false;
		//		checkBox.Visible = false;
		//	}
		//}

		private void OnOrderRadioChanged(object sender, EventArgs e)
		{
			ListConfig.OrderType orderType = GetOrderType();
			ShowOrderType(orderType);
		}
	}
}
