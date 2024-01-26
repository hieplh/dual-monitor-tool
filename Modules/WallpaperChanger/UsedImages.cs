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

using DMT.Library.WallpaperPlugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMT.Modules.WallpaperChanger
{
	/// <summary>
	/// Keeps track of the images used for the current wallpaper.
	/// This is so we can update parts of the wallpaper for individual monitors
	/// and is also used for the 'Wallpaper Changer'->'Properties' options page.
	/// 
	/// Also supports building up a new set of images with the ability to
	/// commit to using the images, or rolling back if it needs to be aborted
	/// due to an error (typically after an image download fails).
	/// </summary>
	class UsedImages
	{
		List<ProviderImage> _currentImages = new List<ProviderImage>();

		List<Tuple<ProviderImage, int>> _candidateImages = new List<Tuple<ProviderImage, int>>();

		public bool HaveAllCurrentImages(int numMonitors)
		{
			// make sure we have a slot for each active monitor
			if (_currentImages.Count < numMonitors)
			{
				return false;
			}

			// check none of these are empty slots
			for (int n = 0; n < numMonitors; n++)
			{
				if (_currentImages[n] == null)
				{
					return false;
				}
			}

			// have an active slot for each active monitor
			return true;
		}

		public ProviderImage GetRememberedImage(int screenIndex)
		{
			ProviderImage ret = null;

			if (screenIndex < _currentImages.Count)
			{
				ret = _currentImages[screenIndex];
			}

			return ret;
		}

		public void AddCandidateImage(ProviderImage providerImage, int screenIndex)
		{
			_candidateImages.Add(new Tuple<ProviderImage, int>(providerImage, screenIndex));
		}

		public void Commit(bool allNew)
		{
			if (allNew)
			{
				// all existing images are to be removed
				foreach (ProviderImage providerImage in _currentImages)
				{
					if (providerImage != null)
					{
						providerImage.Dispose();
					}
				}
				_currentImages = new List<ProviderImage>();
			}

			foreach (Tuple<ProviderImage, int> tuple in _candidateImages)
			{
				UseImage(tuple.Item1, tuple.Item2);
			}

			_candidateImages = new List<Tuple<ProviderImage, int>>();
		}

		public void Rollback()
		{
			// must dispose of any images that we have decided not to use
			// _currentImages will remain unchanged
			foreach (Tuple<ProviderImage, int> tuple in _candidateImages)
			{
				if (tuple.Item1 != null)
				{
					tuple.Item1.Dispose();
				}
			}

			_candidateImages = new List<Tuple<ProviderImage, int>>();
		}


		void UseImage(ProviderImage providerImage, int screenIndex)
		{
			// grow the current image list if needed
			while (_currentImages.Count <= screenIndex)
			{
				_currentImages.Add(null);
			}

			// dispose of old image if there was one
			if (_currentImages[screenIndex] != null)
			{
				_currentImages[screenIndex].Dispose();
			}

			// and remember the new image
			_currentImages[screenIndex] = providerImage;
		}

	}
}
