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

namespace DMT.Modules
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.Windows.Forms;

	/// <summary>
	/// Leaf (end node) option node 
	/// </summary>
	class ModuleOptionNodeLeaf : ModuleOptionNode
	{
		/// <summary>
		/// Initialises a new instance of the <see cref="ModuleOptionNodeLeaf" /> class.
		/// </summary>
		/// <param name="name">Display name of node</param>
		/// <param name="image">Image to display for node</param>
		/// <param name="panel">Panel to display when node selected</param>
		public ModuleOptionNodeLeaf(string name, Image image, ContainerControl panel)
			: base(name, image, panel)
		{
		}
	}
}
