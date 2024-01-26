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

namespace DMT.Modules.WallpaperChanger.Plugins.LocalDisk
{
	using System.Collections.Generic;
	using DMT.Library.WallpaperPlugin;

	/// <summary>
	/// Configuration required for each provider from the LocalDisk plugin
	/// </summary>
	public class LocalDiskConfig
	{
		/// <summary>
		/// Ordering type
		/// Note: Don't change existing numeric values, as these are saved in the config file
		/// </summary>
		public enum OrderType { Random = 0, Alphabetical = 1 };

		/// <summary>
		/// Initialises a new instance of the <see cref="LocalDiskConfig" /> class.
		/// </summary>
		public LocalDiskConfig()
		{
		}

		/// <summary>
		/// Initialises a new instance of the <see cref="LocalDiskConfig" /> class.
		/// </summary>
		/// <param name="configDictionary">Configuration as a dictionary</param>
		public LocalDiskConfig(Dictionary<string, string> configDictionary)
		{
			Enabled = ProviderHelper.ConfigToBool(configDictionary, "enabled", true);
			Weight = ProviderHelper.ConfigToInt(configDictionary, "weight", 10);
			Description = ProviderHelper.ConfigToString(configDictionary, "description", "Windows Wallpaper from local disk");
			Monitor1Directory = ProviderHelper.ConfigToString(configDictionary, "monitor1Directory", string.Empty);
			Monitor2Directory = ProviderHelper.ConfigToString(configDictionary, "monitor2Directory", string.Empty);
			Monitor3Directory = ProviderHelper.ConfigToString(configDictionary, "monitor3Directory", string.Empty);
			Monitor4Directory = ProviderHelper.ConfigToString(configDictionary, "monitor4Directory", string.Empty);
			PortraitDirectory = ProviderHelper.ConfigToString(configDictionary, "portraitDirectory", string.Empty);
			DefaultDirectory = ProviderHelper.ConfigToString(configDictionary, "directory", @"C:\Windows\Web\Wallpaper");

			Type = (OrderType)ProviderHelper.ConfigToInt(configDictionary, "type", (int)OrderType.Random);
			Recursive = ProviderHelper.ConfigToBool(configDictionary, "recursive", true);
			Rescan = ProviderHelper.ConfigToBool(configDictionary, "rescan", false);
			Cycle = ProviderHelper.ConfigToBool(configDictionary, "cycle", false);
			Monitor2K = ProviderHelper.ConfigToBool(configDictionary, "monitor2K", false);
			RandomStart = ProviderHelper.ConfigToBool(configDictionary, "randomStart", false);
		}

		/// <summary>
		/// Gets or sets the wight for this provider
		/// </summary>
		public bool Enabled { get; set; }

		/// <summary>
		/// Gets or sets the wight for this provider
		/// </summary>
		public int Weight { get; set; }

		/// <summary>
		/// Gets or sets the description for this provider
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the directory to use for images for monitor 1
		/// </summary>
		public string Monitor1Directory { get; set; }

		/// <summary>
		/// Gets or sets the directory to use for images for monitor 2
		/// </summary>
		public string Monitor2Directory { get; set; }

		/// <summary>
		/// Gets or sets the directory to use for images for monitor 3
		/// </summary>
		public string Monitor3Directory { get; set; }

		/// <summary>
		/// Gets or sets the directory to use for images for monitor 4
		/// </summary>
		public string Monitor4Directory { get; set; }

		/// <summary>
		/// Gets or sets the directory to use for images for monitors in portrait mode
		/// without an explicit directory  
		/// </summary>
		public string PortraitDirectory { get; set; }

		/// <summary>
		/// Gets or sets the directory to use for any monitors
		/// without a explicit directory
		/// </summary>
		public string DefaultDirectory { get; set; }

		public OrderType Type { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether to scan recursively down through directories
		/// </summary>
		public bool Recursive { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether to re-scan disk contents for every image request
		/// </summary>
		public bool Rescan { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether to cycle through all images in a random order
		/// or if each image should be random (ignoring what was returned before) 
		/// </summary>
		public bool Cycle { get; set; }

		public bool Monitor2K { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether to start at random point when viewing images alphabetically
		/// </summary>
		public bool RandomStart { get; set; }

		/// <summary>
		/// Gets the configuration as a dictionary ready for saving to disk
		/// </summary>
		/// <returns>Dictionary representation of configuration</returns>
		public Dictionary<string, string> ToDictionary()
		{
			Dictionary<string, string> configDictionary = new Dictionary<string, string>();
			configDictionary["enabled"] = Enabled.ToString();
			configDictionary["weight"] = Weight.ToString();
			configDictionary["description"] = Description;
			configDictionary["monitor1Directory"] = Monitor1Directory;
			configDictionary["monitor2Directory"] = Monitor2Directory;
			configDictionary["monitor3Directory"] = Monitor3Directory;
			configDictionary["monitor4Directory"] = Monitor4Directory;
			configDictionary["portraitDirectory"] = PortraitDirectory;
			configDictionary["directory"] = DefaultDirectory;

			configDictionary["type"] = ((int)Type).ToString();
			configDictionary["recursive"] = Recursive.ToString();
			configDictionary["rescan"] = Rescan.ToString();
			configDictionary["cycle"] = Cycle.ToString();
			configDictionary["monitor2K"] = Monitor2K.ToString();
			configDictionary["randomStart"] = RandomStart.ToString();
			return configDictionary;
		}
	}
}
