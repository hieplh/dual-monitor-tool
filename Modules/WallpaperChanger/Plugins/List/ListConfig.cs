#region copyright
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

namespace DMT.Modules.WallpaperChanger.Plugins.List
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	using DMT.Library.WallpaperPlugin;
	using DMT.Library.Wallpaper;

	/// <summary>
	/// Configuration required for each provider from the LocalDisk plugin
	/// </summary>
	public class ListConfig
	{
		/// <summary>
		/// Ordering type
		/// Note: Don't change existing numeric values, as these are saved in the config file
		/// </summary>
		public enum OrderType { Random = 0, List = 1 };

		/// <summary>
		/// Initialises a new instance of the <see cref="LocalDiskConfig" /> class.
		/// </summary>
		public ListConfig()
		{
			PersistKey = new PositionTrackerKey();
		}

		/// <summary>
		/// Initialises a new instance of the <see cref="LocalDiskConfig" /> class.
		/// </summary>
		/// <param name="configDictionary">Configuration as a dictionary</param>
		public ListConfig(Dictionary<string, string> configDictionary)
		{
			Enabled = ProviderHelper.ConfigToBool(configDictionary, "enabled", true);
			Weight = ProviderHelper.ConfigToInt(configDictionary, "weight", 10);
			Description = ProviderHelper.ConfigToString(configDictionary, "description", "Windows Wallpaper from list");
			ListFnm = ProviderHelper.ConfigToString(configDictionary, "listFnm", string.Empty);

			Type = (OrderType)ProviderHelper.ConfigToInt(configDictionary, "type", (int)OrderType.Random);
			Persist = ProviderHelper.ConfigToBool(configDictionary, "persist", true);
			PersistKey = new PositionTrackerKey(ProviderHelper.ConfigToString(configDictionary, "persistKey", ""));
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
		/// Gets or sets the path to the file containing the list of image locations
		/// </summary>
		public string ListFnm { get; set; }

		/// <summary>
		/// Gets or sets how we are going to run through the list (in order or random)
		/// </summary>
		public OrderType Type { get; set; }

		/// <summary>
		/// Gets or sets if current position in list should be persisted when we shutdown
		/// </summary>
		public bool Persist { get; set; }

		/// <summary>
		/// Gets or sets the key used track the position we are in the list (when Persist = true)
		/// </summary>
		public PositionTrackerKey PersistKey { get; set; }



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
			configDictionary["listFnm"] = ListFnm;

			configDictionary["type"] = ((int)Type).ToString();
			configDictionary["persist"] = Persist.ToString();
			configDictionary["persistKey"] = PersistKey.ToString();
			return configDictionary;
		}
	}
}
