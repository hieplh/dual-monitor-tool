﻿#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2015  Gerald Evans
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

namespace DMT.Library.HotKeys
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Data;
	using System.Drawing;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.Windows.Forms;

	using DMT.Resources;
	using GuiUtils;

	/// <summary>
	/// Dialog for editing a hotkey
	/// </summary>
	public partial class HotKeyForm : Form
	{
		// hotkey being edited
		HotKey _hotKey;

		/// <summary>
		/// Initialises a new instance of the <see cref="HotKeyForm" /> class.
		/// </summary>
		/// <param name="hotKey">The hotkey to edit</param>
		/// <param name="decription">A short description of the function of the hotkey.</param>
		/// <param name="note">Further information relating to the hotkey.
		/// If the functionality of this hotkey is provided by Windows 7, 
		/// then currently we use this note to advise the user of this fact.</param>
		public HotKeyForm(HotKey hotKey, string decription, string note)
		{
			InitializeComponent();

			labelDescription.Text = decription;

			if (note != null && note.Length > 0)
			{
				lblNote.Text = note;
			}
			else
			{
				HideNoteBox();
			}

			this._hotKey = hotKey;
			keyComboPanel.KeyCombo = hotKey.HotKeyCombo;
			checkBoxEnable.Checked = hotKey.HotKeyCombo.Enabled;
			UpdateEnableStatus();
		}

		private void HotKeyForm_Load(object sender, EventArgs e)
		{
		}

		// Hides the (yellow) box where we would normally display the note
		private void HideNoteBox()
		{
			// adjust the clent size to hide the note box
			Size clientSize = this.ClientSize;
			clientSize.Height = lblNote.Location.Y;
			this.ClientSize = clientSize;
		}

		private void checkBoxEnable_CheckedChanged(object sender, EventArgs e)
		{
			UpdateEnableStatus();
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			// get the hotkey from the panel
			KeyCombo keyCombo = keyComboPanel.KeyCombo;

			// and disable/enable it depending on the checkbox
			keyCombo.Enabled = checkBoxEnable.Checked;

			if (keyCombo.Enabled)
			{
				if (keyCombo.KeyCode == (Keys)0)
				{
					MsgDlg.UserError(CommonStrings.NoKey);
					return;
				}
			}

			// attempt to register this hotkey
			if (!_hotKey.RegisterHotKey(keyCombo))
			{
				MsgDlg.UserError(CommonStrings.RegisterFail);
				return;
			}

			// hotkey is OK
			DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
		}

		private void UpdateEnableStatus()
		{
			keyComboPanel.Enabled = checkBoxEnable.Checked;
		}
	}
}
