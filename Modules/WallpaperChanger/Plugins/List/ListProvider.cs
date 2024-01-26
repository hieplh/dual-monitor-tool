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
	using System.Diagnostics;
	using System.Drawing;
	using System.IO;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	using DMT.Library.Utils;
	using DMT.Library.WallpaperPlugin;
	using DMT.Library.Wallpaper;
	using DMT.Library.Html;
	using DMT.Library.Logging;

	/// <summary>
	/// An instance of a provider from the Local Disk plugin
	/// </summary>
	public class ListProvider : IImageProvider
	{
		ListConfig _config;
		ILogger _logger;

		ListManager _list;
		PositionTracker _positionTracker;
		// for requesting urls
		HttpConnectionManager _connectionManager;
		HttpRequester _httpRequester;


		/// <summary>
		/// Initialises a new instance of the <see cref="LocalDiskProvider" /> class.
		/// </summary>
		/// <param name="config">Configuration for the provider</param>
		public ListProvider(Dictionary<string, string> config, ILogger logger)
		{
			_config = new ListConfig(config);
			_logger = logger;

			_list = new ListManager(_config.ListFnm);

			//_positionTracker = null;
			//CreatePositionTracker();

			// make sure we have a persist key (even if we never use it)
			if (_config.PersistKey.IsEmpty())
			{
				_config.PersistKey = PositionTrackerKey.NewKey();
			}

			bool random = _config.Type == ListConfig.OrderType.Random;
			_positionTracker = PositionTrackerRepository.Instance.GetPositionTracker(_config.PersistKey, _list.Count, random, _config.Persist);

			_connectionManager = new HttpConnectionManager();
			_httpRequester = new HttpRequester(_connectionManager);
		}

		/// <summary>
		/// Gets the provider name - same for all instances of this class
		/// </summary>
		public string ProviderName
		{
			get
			{
				return ListPlugin.PluginName;
			}
		}

		/// <summary>
		/// Gets the provider image - same for all instances of this class
		/// </summary>
		public Image ProviderImage
		{
			get
			{
				return ListPlugin.PluginImage;
			}
		}

		/// <summary>
		/// Gets the provider version - same for all instances of this class
		/// </summary>
		public string Version
		{
			get
			{
				return ListPlugin.PluginVersion;
			}
		}

		/// <summary>
		/// Gets the description for this instance of the provider
		/// </summary>
		public string Description
		{
			get
			{
				return _config.Description;
			}
		}

		/// <summary>
		/// Gets the enabled state for this instance of the provider
		/// </summary>
		public bool Enabled
		{
			get
			{
				return _config.Enabled;
			}
			set
			{
				_config.Enabled = value;
			}
		}

		/// <summary>
		/// Gets the weight for this instance of the provider
		/// </summary>
		public int Weight
		{
			get
			{
				return _config.Weight;
			}
		}

		/// <summary>
		/// Gets the configuration 
		/// </summary>
		public Dictionary<string, string> Config
		{
			get
			{
				return _config.ToDictionary();
			}
		}

		/// <summary>
		/// Allows the user to update the configuration 
		/// </summary>
		/// <returns>New configuration, or null if no changes</returns>
		public Dictionary<string, string> ShowUserOptions()
		{
			ListForm dlg = new ListForm(_config);
			if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				// keep the same PositionTrackerKey
				PositionTrackerKey persistKey = _config.PersistKey;
				//bool oldPersist = _config.Persist;
				_config = dlg.GetConfig();
				_config.PersistKey = persistKey;
				//if (_config.Persist != oldPersist)
				//{
					_positionTracker.Persist = _config.Persist;
				//}

				return _config.ToDictionary();
			}

			// return null to indicate options have not been updated
			return null;
		}

		/// <summary>
		/// Returns an image.
		/// Despite it's name, this would not be random if OrderType != Random
		/// </summary>
		/// <param name="optimumSize">Optimum image size</param>
		/// <param name="screenIndex">Screen index image is for</param>
		/// <returns>A suitable image, or null if one can't be returned</returns>
		public ProviderImage GetRandomImage(Size optimumSize, int screenIndex)
		{
			ProviderImage providerImage = null;

			PositionTrackerKey persistKey = _config.PersistKey;

			// list fnm may have changed, so check we are using correct list
			_list.SetListName(_config.ListFnm);

			bool listChanged = _list.CheckIfChanged();
			bool random = _config.Type == ListConfig.OrderType.Random;
			if (listChanged 
				|| _positionTracker.UseRandomOrder != random)
				//|| _positionTracker.Persist != _config.Persist)
			{
				//// check we have a PositionTrackerKey
				//if (_config.PersistKey.IsEmpty())
				//{
				//	_config.PersistKey = PositionTrackerKey.NewKey();
				//}

				//_positionTracker = PositionTrackerRepository.Instance.GetPositionTracker(_config.PersistKey, _list.Count, random, _config.Persist);
				//CreatePositionTracker();

				_positionTracker.Reset(_list.Count, random);
			}

			int posn = _positionTracker.GetNextPoistion();
			string path = _list[posn];

			if (path != null)
			{
				try
				{
					Image image = null;
					if (IsPathAFile(path))
					{
						try
						{
							image = Image.FromFile(path);
						}
						catch (OutOfMemoryException)
						{
							// This is what MS uses when the file format is not supported!
							_logger.LogError("ListProvider", "Image format used by {0} is not supported", path);
						}
					}
					else
					{
						// assume it is a URL
						Uri uri = new Uri(path);
						image = _httpRequester.GetImage(uri);
					}

					if (image != null)
					{
						providerImage = new ProviderImage(image);
						providerImage.Provider = ProviderName;
						providerImage.Source = path;
						providerImage.SourceUrl = path;

						providerImage.MoreInfo = providerImage.Image.GetExifDescription();
						providerImage.Photographer = providerImage.Image.GetExifPhotographer();
					}
				}
				catch (Exception ex)
				{
					_logger.LogException("ListProvider", ex);
				}
			}


			return providerImage;
		}

		/// <summary>
		/// Indicates that this provider is being removed from the list of providers
		/// </summary>
		public void Remove()
		{
			//if (_positionTracker == null)
			//{
			//	// we have to explicitly create the position tracker if we haven't already
			//	// (user could be removing a provider that is not actively being used by current session)
			//	CreatePositionTracker();
			//}
			
			// this will remove the PositionTracker file if it exists
			_positionTracker.Persist = false;
		}

		bool IsPathAFile(string path)
		{
			if (File.Exists(path))
			{
				return true;
			}

			// assume anything else is a URL
			return false;
		}

		//void CreatePositionTracker()
		//{
		//	if (_config.PersistKey.IsEmpty())
		//	{
		//		_config.PersistKey = PositionTrackerKey.NewKey();
		//	}

		//	bool random = _config.Type == ListConfig.OrderType.Random;
		//	_positionTracker = PositionTrackerRepository.Instance.GetPositionTracker(_config.PersistKey, _list.Count, random, _config.Persist);
		//}

		//void CheckHaveList()
		//{
		//	if (_list == null)
		//	{
		//		bool checkForUpdates = false; // TODO
		//		_list = new ListManager(_config.ListFnm, checkForUpdates);
		//	}
		//}

		//void ClearAllCaches()
		//{
		//	_monitor1Cache.ClearCache();
		//	_monitor2Cache.ClearCache();
		//	_monitor3Cache.ClearCache();
		//	_monitor4Cache.ClearCache();
		//	_portraitCache.ClearCache();
		//	_defaultCache.ClearCache();
		//}

		//string GetNextImageFilename(Size optimumSize, int screenIndex)
		//{
		//	string ret = null;

		//	// check for image for a particular monitor
		//	// Screen Index is zero based, 
		//	// and will be -1 if want an image to cover multiple monitors 
		//	//
		//	// NOTE: Will also be -1 if only a single monitor,
		//	// but the'Multi Monitor' option is one that could imply that
		//	// the image is needed for multiple monitors
		//	if (screenIndex >= 0)
		//	{
		//		switch (screenIndex)
		//		{
		//			case 0:
		//				ret = _monitor1Cache.GetNextImageFilename();
		//				break;
		//			case 1:
		//				ret = _monitor2Cache.GetNextImageFilename();
		//				break;
		//			case 2:
		//				ret = _monitor3Cache.GetNextImageFilename();
		//				break;
		//			case 3:
		//				ret = _monitor4Cache.GetNextImageFilename();
		//				break;
		//			default:
		//				// leave ret as null
		//				break;
		//		}
		//	}

		//	// check for portrait
		//	if (ret == null)
		//	{
		//		if (optimumSize.Height > optimumSize.Width)
		//		{
		//			// ideally want a portrait image
		//			ret = _portraitCache.GetNextImageFilename();
		//		}
		//	}

		//	// landscape / default
		//	if (ret == null)
		//	{
		//		ret = _defaultCache.GetNextImageFilename();
		//	}

		//	return ret;
		//}
	}
}
