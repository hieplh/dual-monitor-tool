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

namespace DMT.Library.WallpaperPlugin
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Linq;
	using System.Text;
	using Utils;

	/// <summary>
	/// An image returned by a provider
	/// </summary>
	public class ProviderImage : IDisposable
	{
		/// <summary>
		/// Initialises a new instance of the <see cref="ProviderImage" /> class.
		/// </summary>
		/// <param name="image">The image</param>
		public ProviderImage(Image image)
		{
			Image = image;
			AutoRotate(Image);
		}

		~ProviderImage()
		{
			Dispose(false);
		}

		/// <summary>
		/// Gets or sets the image
		/// </summary>
		public Image Image { get; set; }

		/// <summary>
		/// Gets or sets the name of the provider
		/// </summary>
		public string Provider { get; set; }

		/// <summary>
		/// Gets or sets a URL to the provider
		/// </summary>
		public string ProviderUrl { get; set; }

		/// <summary>
		/// Gets or sets a description of the image source
		/// </summary>
		public string Source { get; set; }

		/// <summary>
		/// Gets or sets a URL to the image source
		/// </summary>
		public string SourceUrl { get; set; }

		/// <summary>
		/// Gets or sets the name of the photographer
		/// </summary>
		public string Photographer { get; set; }

		/// <summary>
		/// Gets or sets a URL to the photographer
		/// </summary>
		public string PhotographerUrl { get; set; }

		/// <summary>
		/// Gets or sets more information about the image
		/// </summary>
		public string MoreInfo { get; set; }

		/// <summary>
		/// Gets or sets a URL containing more information about the image
		/// </summary>
		public string MoreInfoUrl { get; set; }

		/// <summary>
		/// Dispose of any resources held
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (Image != null)
				{
					Image.Dispose();
				}
			}
		}

		void AutoRotate(Image image)
		{
			// see http://stackoverflow.com/questions/6222053/problem-reading-jpeg-metadata-orientation/38459903#38459903

			int orientation = image.GetExifOrientation();

			if (orientation > 0)
			{
				switch (orientation)
				{
					case 1:
						// correctly orientated
						break;
					case 2:
						image.RotateFlip(RotateFlipType.RotateNoneFlipX);
						break;
					case 3:
						image.RotateFlip(RotateFlipType.Rotate180FlipNone);
						break;
					case 4:
						image.RotateFlip(RotateFlipType.Rotate180FlipX);
						break;
					case 5:
						image.RotateFlip(RotateFlipType.Rotate90FlipX);
						break;
					case 6:
						image.RotateFlip(RotateFlipType.Rotate90FlipNone);
						break;
					case 7:
						image.RotateFlip(RotateFlipType.Rotate270FlipX);
						break;
					case 8:
						image.RotateFlip(RotateFlipType.Rotate270FlipNone);
						break;
				}

				// we shouldn't need to test this again, but remove it jic
				image.RemoveExifOrientation();
			}
		}
	}
}
