#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2010-2015  Gerald Evans
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

namespace DMT.Library.GuiUtils
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Text;
	using System.Windows.Forms;

	using DMT.Library.PInvoke;

	/// <summary>
	/// Control to allow a top level window to be selected
	/// </summary>
	public class WindowPicker : PictureBox
	{
		Cursor trackingCursor;
		Cursor oldCursor;

		Bitmap imageNoCursor;
		Bitmap imageWithCursor;

		IntPtr hWndLastEvent;

		/// <summary>
		/// Definition required for delegates that want to be notified 
		/// of the window we are hovering over.
		/// </summary>
		/// <param name="hWnd">Handle of window being hovered over</param>
		public delegate void HoverHandler(IntPtr hWnd);

		/// <summary>
		/// Event that will be fired when we hover over a different window.
		/// </summary>
		public event HoverHandler HoveredWindowChanged;

		/// <summary>
		/// One time initialisation 
		/// </summary>
		/// <param name="cursorBitmap">Bitmap of cursor to use to select window</param>
		/// <param name="imageNoCursor">Bitmap for control without the cursor (for when cursor has been
		/// dragged away)</param>
		/// <param name="imageWithCursor">Bitmap for control with the cursor displayed on it</param>
		public void InitControl(Bitmap cursorBitmap, Bitmap imageNoCursor, Bitmap imageWithCursor)
		{
			// convert bitmap to a cursor
			IntPtr hIcon = cursorBitmap.GetHicon();
			trackingCursor = new Cursor(hIcon);

			// save the bitmaps to use as images for the control
			this.imageNoCursor = imageNoCursor;

			// TODO: this could be dynamically generated from imageNoCursor and cursorBitmap
			this.imageWithCursor = imageWithCursor;

			// initially display the image with the cursor
			this.Image = this.imageWithCursor;
		}

		/// <summary>
		/// Handles the mouse down event
		/// </summary>
		/// <param name="e">Mouse event arguments</param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			hWndLastEvent = IntPtr.Zero;
			this.Capture = true;

			// set tracking cursor, remembering old cursor
			oldCursor = this.Cursor;
			this.Cursor = trackingCursor;

			// update our image to show cursor has been taken
			this.Image = imageNoCursor;
		}

		/// <summary>
		/// Handles the mouse move event
		/// </summary>
		/// <param name="e">Mouse event arguments</param>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			if (this.Capture)
			{
				// convert client co-ords to screen co-ords
				Point point = this.PointToScreen(e.Location);

				NativeMethods.POINT win32pt = new NativeMethods.POINT();
				win32pt.x = point.X;
				win32pt.y = point.Y;
				IntPtr hWnd = NativeMethods.WindowFromPoint(win32pt);

				// find the top level window that this window belongs to
				IntPtr hWndParent = hWnd;
				while (hWndParent != IntPtr.Zero)
				{
					hWnd = hWndParent;
					hWndParent = NativeMethods.GetParent(hWnd);
				}

				// only report if different from last event
				if (hWnd != hWndLastEvent)
				{
					hWndLastEvent = hWnd;

					if (HoveredWindowChanged != null)
					{
						HoveredWindowChanged(hWnd);
					}
				}
			}
		}

		/// <summary>
		/// Handles the mouse up event
		/// </summary>
		/// <param name="e">Mouse event arguments</param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);

			if (this.Capture)
			{
				// turn off capturing
				this.Capture = false;

				// restore image to that with a cursor
				this.Image = imageWithCursor;

				// restore cursor
				if (oldCursor != null)
				{
					this.Cursor = oldCursor;
				}

				// could fire an even to indicate capturing finished
			}
		}
	}
}
