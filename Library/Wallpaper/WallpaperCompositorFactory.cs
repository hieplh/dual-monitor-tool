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

namespace DMT.Library.Wallpaper
{

	using DMT.Library.Environment;

	/// <summary>
	/// Allows a IWallpaperCompositor to be created dynamically
	/// </summary>
	class WallpaperCompositorFactory : IWallpaperCompositorFactory
	{
		/// <summary>
		/// Creates a wallpaper compositor
		/// </summary>
		/// <param name="monitors">Monitors to create the compositor for</param>
		/// <returns>A wallpaper compositor</returns>
		public IWallpaperCompositor Create(Monitors monitors)
		{
			return new WallpaperCompositor(monitors);
		}
	}
}
