#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2023  Gerald Evans
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

namespace DMT.Modules.Cursor
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

	using DMT.Library.HotKeys;
	using DMT.Resources;
	using DMT.Library.GuiUtils;

	/// <summary>
	/// General options panel for the cursor module
	/// </summary>
	partial class CursorPrimaryOptionsPanel : UserControl
	{
		CursorModule _cursorModule;

		/// <summary>
		/// Initialises a new instance of the <see cref="CursorGeneralOptionsPanel" /> class.
		/// </summary>
		/// <param name="cursorModule">The cursor module</param>
		public CursorPrimaryOptionsPanel(CursorModule cursorModule)
		{
			_cursorModule = cursorModule;

			InitializeComponent();

			InitOptions();
		}

		public void ShowMonitorPreview()
		{
			MonitorPreviewHelper.ShowLayout(picPreview, _cursorModule.PrimaryGroup);
		}

		void InitOptions()
		{
			ShowMonitorPreview();
		}

		private void radioButtonPrimaryScreen_CheckedChanged(object sender, EventArgs e)
		{
			ShowMonitorPreview();
		}

		private void radioButtonPrimaryArea_CheckedChanged(object sender, EventArgs e)
		{
			ShowMonitorPreview();
		}

		private void picPreview_MouseClick(object sender, MouseEventArgs e)
		{
			// check if the area clicked belongs to one of the screens
			int monitorIndex = MonitorPreviewHelper.PosnToMonitor(picPreview, e.X, e.Y);
			if (monitorIndex >= 0)
			{
				List<int> primaryGroup = _cursorModule.PrimaryGroup;
				if (primaryGroup.Contains(monitorIndex))
				{
					primaryGroup.Remove(monitorIndex);
				}
				else
				{
					primaryGroup.Add(monitorIndex);
				}

				_cursorModule.PrimaryGroup = primaryGroup;

				ShowMonitorPreview();
			}
		}

		//void UpdateArea()
		//{
		//	ShowMonitorPreview();
		//}

	}
}
