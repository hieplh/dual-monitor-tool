#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2021  Gerald Evans
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

using DMT.Library.Wallpaper;
using DMT.Library.WallpaperPlugin;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DMT.Modules.WallpaperChanger
{
	/// <summary>
	/// Fetches images from the repository and keeps track of the errors
	/// </summary>
	public class ImageFetcher
	{
		//public int NumRequests { get; set; }
		public int NumRequestsGood { get; set; }
		public int NumRequestsBad { get; set; }

		IImageRepository _imageRepository;

		public ImageFetcher(IImageRepository imageRepository)
		{
			_imageRepository = imageRepository;
			NumRequestsGood = 0;
			NumRequestsBad = 0;
		}

		public ProviderImage GetRandomImageForScreen(IWallpaperCompositor compositor, int screenIndex)
		{
			Size optimumSize = compositor.AllScreens[screenIndex].ScreenRect.Size;
			return GetRandomImage(optimumSize, screenIndex);
		}

		public ProviderImage GetRandomImage(Size optimumSize, int screenIndex = -1)
		{
			ProviderImage providerImage = _imageRepository.GetRandomImage(optimumSize, screenIndex);

			if (providerImage == null)
			{
				NumRequestsBad++;
			}
			else
			{
				NumRequestsGood++;
			}

			return providerImage;
		}

		//public bool 
	}
}
