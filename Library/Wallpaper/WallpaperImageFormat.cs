using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMT.Library.Wallpaper
{
	/// <summary>
	/// Specifies the file type to use for composited wallpaper
	/// </summary>
	public class WallpaperImageFormat
	{
		// These get displayed to the user, but are pretty universal(?)
		// so done need to be in a resource
		const string BmpText = "BMP";
		const string PngText = "PNG";
		const string JpgText = "JPG";

		public WallpaperImageFormat(ImageFormat imageFormat)
		{
			Format = imageFormat;
		}

		public enum ImageFormat
		{
			BMP = 0,
			PNG = 1,
			JPG = 2
		}

		public ImageFormat Format { get; set; }

		public static List<WallpaperImageFormat> AllImageFormats()
		{
			List<WallpaperImageFormat> allTypes = new List<WallpaperImageFormat>();
			allTypes.Add(new WallpaperImageFormat(ImageFormat.BMP));
			allTypes.Add(new WallpaperImageFormat(ImageFormat.PNG));
			allTypes.Add(new WallpaperImageFormat(ImageFormat.JPG));
			return allTypes;
		}

		public override string ToString()
		{
			switch (Format)
			{
				case ImageFormat.BMP:
					return BmpText;

				case ImageFormat.PNG:
					return PngText;

				case ImageFormat.JPG:
					return JpgText;

				default:
					// safest option
					return BmpText;
			}
		}
	}
}
