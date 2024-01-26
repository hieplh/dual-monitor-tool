#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2010  Gerald Evans
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

namespace DMT.Modules.Launcher
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Text;

	/// <summary>
	/// Specifies a starting position for an application
	/// </summary>
	[Serializable]
	public class StartupPosition
	{
		bool _enablePosition;
		Rectangle _position;
		int _showCmd;

		/// <summary>
		/// Gets or sets a value indicating whether the Starting Position is to be used
		/// </summary>
		public bool EnablePosition
		{
			get { return _enablePosition; }
			set { _enablePosition = value; }
		}

		/// <summary>
		/// Gets or sets the position to open the window at.
		/// </summary>
		// don't serialize the rectangle directly as it leads to duplication
		// in the XML due to it having multiple public properties to access
		// the same info.
		// Instead we use out own Location and Size for serialization
		[System.Xml.Serialization.XmlIgnore]
		public Rectangle Position
		{
			get { return _position; }
			set { _position = value; }
		}

		/// <summary>
		/// Gets or sets the position to open the window at.
		/// Added for serialisation of location instead of using Position
		/// </summary>
		public Point Location
		{
			get { return _position.Location; }
			set { _position.Location = value; }
		}

		/// <summary>
		/// Gets or sets the size of the window.
		/// Added for serialisation of size instead of using Position
		/// </summary>
		public Size Size
		{
			get { return _position.Size; }
			set { _position.Size = value; }
		}

		/// <summary>
		/// Gets or sets whether the window should be shown minimised, maximised or normal.
		/// This uses the save values as used by Win32.ShowWindow() and Win32.WINDOWPLACEMENT
		/// </summary>
		public int ShowCmd
		{
			get { return _showCmd; }
			set { _showCmd = value; }
		}

		/// <summary>
		/// Deep clone the StartupPosition.
		/// (No actual need for deep clone with current implementation)
		/// note: this is not ICloneable.Clone() - but could be if needed
		/// </summary>
		/// <returns>Cloned object</returns>
		public StartupPosition Clone()
		{
			return (StartupPosition)this.MemberwiseClone();
		}
	}
}
