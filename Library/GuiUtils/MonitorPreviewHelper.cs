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

using DMT.Library.Environment;
using DMT.Library.Wallpaper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DMT.Library.GuiUtils
{
	public static class MonitorPreviewHelper
	{
		public static int PosnToMonitor(PictureBox picPreview, int x, int y)
		{
			int monitor = -1;	// outside bounds of all monitors

			Monitors monitors = Monitor.AllMonitors;

			// get rectangle within the picture box where the monitoes will be displayed
			Rectangle pictureBoxRect = new Rectangle(new Point(0, 0), picPreview.Size);
			Rectangle previewRect = ImageFitter.UnderStretch(monitors.Bounds.Size, pictureBoxRect);

			for (int monitorIndex = 0; monitorIndex < monitors.Count; monitorIndex++)
			{
				Rectangle screenRect = monitors[monitorIndex].Bounds; // position in desktop
				Rectangle previewScreen = WallpaperCompositor.CalcDestRect(monitors.Bounds, previewRect, screenRect);

				if (previewScreen.Contains(x, y))
				{
					return monitorIndex;
				}
			}

			return monitor;
		}

		public static void ShowLayout(PictureBox picPreview, List<int> selectedMonitors = null)
		{
			Monitors monitors = Monitor.AllMonitors;

			// get rectangle within the picture box where the monitoes will be displayed
			Rectangle pictureBoxRect = new Rectangle(new Point(0, 0), picPreview.Size);
			Rectangle previewRect = ImageFitter.UnderStretch(monitors.Bounds.Size, pictureBoxRect);

			Image preview = new Bitmap(previewRect.Width, previewRect.Height);

			using (Graphics g = Graphics.FromImage(preview))
			{
				for (int monitorIndex = 0; monitorIndex < monitors.Count; monitorIndex++)
				{
					string monitorName = string.Format("{0}", monitorIndex + 1);
					if (monitors[monitorIndex].Primary)
					{
						monitorName += "P";
					}
					bool selected = false;
					if (selectedMonitors != null && selectedMonitors.Contains(monitorIndex))
					{
						selected = true;
					}
					ShowMonitor(monitors, monitorIndex, g, previewRect, monitorName, selected);
				}
			}

			// display preview
			if (picPreview.Image != null)
			{
				picPreview.Image.Dispose();
			}

			picPreview.Image = preview;
		}

		static void ShowMonitor(Monitors monitors, int monitorIndex, Graphics g, Rectangle previewRect, string screenName, bool selected)
		{
			// need to determine position of screen rect in the preview
			Rectangle screenRect = monitors[monitorIndex].Bounds; // position in desktop
			Rectangle picBoxRect = new Rectangle(new Point(0, 0), previewRect.Size); // target rectangle for entire dektop
			Rectangle previewScreen = WallpaperCompositor.CalcDestRect(monitors.Bounds, picBoxRect, screenRect);

			// TODO: look into this!
			previewScreen = new Rectangle(previewScreen.Left, previewScreen.Top, previewScreen.Width - 1, previewScreen.Height - 1);

			// draw border around screen
			Pen borderPen1 = Pens.Black;
			Pen borderPen2 = Pens.Black; // Pens.White;
			Brush textBrush = Brushes.Black; //  Brushes.White;
			if (selected)
			{
				borderPen1 = Pens.Blue; // Pens.Yellow;
				borderPen2 = Pens.Blue; // Pens.Yellow;
				textBrush = Brushes.Black; //  Brushes.Yellow;
			}

			// leave outermost pixels of image visible
			previewScreen.Inflate(-1, -1);
			g.DrawRectangle(borderPen1, previewScreen);
			previewScreen.Inflate(-1, -1);
			g.DrawRectangle(borderPen2, previewScreen);

			// display the screen name centered in the screen
			using (Font font = new Font("Arial", 24, FontStyle.Bold, GraphicsUnit.Point))
			{
				StringFormat stringFormat = new StringFormat();
				stringFormat.Alignment = StringAlignment.Center;
				stringFormat.LineAlignment = StringAlignment.Center;
				g.DrawString(screenName, font, textBrush, previewScreen, stringFormat);
			}
		}
	}
}
