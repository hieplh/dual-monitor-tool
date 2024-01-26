#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2009-2015  Gerald Evans
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

namespace DMT.Modules.Snap
{
	using DMT.Library.Environment;
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Text;
	using System.Windows.Forms;

	/// <summary>
	/// Custom menu item for displaying a snap
	/// </summary>
	public class SnapMenuItem : ToolStripMenuItem
	{
		static Size _defaultSize = new Size();

		/// <summary>
		/// Initialises a new instance of the <see cref="SnapMenuItem" /> class.
		/// </summary>
		static SnapMenuItem()
		{
			// static ctor used to perform computations that only need to be performed once.
			//Size primaryScreenSize = Screen.PrimaryScreen.Bounds.Size;
			Size primaryScreenSize = Monitor.AllMonitors.PrimaryMonitor.Bounds.Size;

			_defaultSize.Height = 150; // TODO determine suitable (configurable?) value

			// scale width to keep aspect ratio same as primary screen
			_defaultSize.Width = (_defaultSize.Height * primaryScreenSize.Width) / primaryScreenSize.Height;
		}

		/// <summary>
		/// Constructs the menu item
		/// </summary>
		/// <param name="snap">The Snap to display in the menu item.</param>
		public SnapMenuItem(Snap snap)
		{
			Snap = snap;

			this.Size = _defaultSize;
			this.AutoSize = false;
		}

		/// <summary>
		/// Gets or sets the snap displayed in this menu item
		/// </summary>
		public Snap Snap { get; set; }

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			if (Snap != null)
			{
				Graphics g = e.Graphics;
				Rectangle r = this.ContentRectangle;

				// draw a black border around the image
				// Note: Need -1 on the width as DrawRectangle draws at X + Width
				// which is outside of the rectangle.  Ditto with the height.
				g.DrawRectangle(Pens.Black, r.X, r.Y, r.Width - 1, r.Height - 1	);

				// draw the image within the border we have just drawn
				r.Inflate(-1, -1);
				g.DrawImage(Snap.Image, r);
			}
		}
	}
}
