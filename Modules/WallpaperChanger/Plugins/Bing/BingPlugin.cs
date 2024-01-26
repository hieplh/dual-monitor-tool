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

namespace DMT.Modules.WallpaperChanger.Plugins.Bing
{
	using DMT.Library.WallpaperPlugin;
	using System.Collections.Generic;
	using System.Drawing;
	/// <summary>
	/// Bing plugin
	/// </summary>
	public class BingPlugin : IDWC_Plugin
	{
		const string _pluginName = "Bing";
		public const string PluginVersion = "0.0";

		public static string PluginName { get { return _pluginName; } }
		public static Image PluginImage { get { return Properties.Resources.BingPlugin; } }

		public string Name { get { return PluginName; } }
		public Image Image { get { return PluginImage; } }

		public IImageProvider CreateProvider(Dictionary<string, string> config)
		{
			return new BingProvider(config);
		}
	}
}
