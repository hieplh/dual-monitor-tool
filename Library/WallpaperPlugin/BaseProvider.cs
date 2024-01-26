//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using System.Text;

//namespace DMT.Library.WallpaperPlugin
//{
//	public abstract class BaseProvider : IImageProvider
//	{
//		/// <summary>
//		/// Gets the version of the provider
//		/// </summary>
//		public abstract string Version { get; }		// Don't think this is needed?

//		/// <summary>
//		/// Gets the provider name
//		/// </summary>
//		public abstract string ProviderName { get; }

//		/// <summary>
//		/// Gets the image for the provider
//		/// </summary>
//		public abstract Image ProviderImage { get; }

//		/// <summary>
//		/// Gets the description for this provider
//		/// </summary>
//		public abstract string Description { get; }

//		/// <summary>
//		/// Gets the enabled status for this provider
//		/// </summary>
//		public abstract bool Enabled { get; set; }

//		/// <summary>
//		/// Gets the weight for this provider
//		/// </summary>
//		public abstract int Weight { get; }

//		/// <summary>
//		/// Gets the configuration for this provider
//		/// </summary>
//		public abstract Dictionary<string, string> Config { get; }

//		/// <summary>
//		/// Shows the configuration dialog and returns chosen options
//		/// </summary>
//		/// <returns>Chosen options</returns>
//		public abstract Dictionary<string, string> ShowUserOptions();

//		/// <summary>
//		/// Gets a random image from the provider
//		/// </summary>
//		/// <param name="optimumSize">Optimum size of image</param>
//		/// <param name="screenIndex">The screen index the image is for</param>
//		/// <returns>Random image</returns>
//		public abstract ProviderImage GetRandomImage(Size optimumSize, int screenIndex);

//		public virtual void Remove()
//		{
//			// nothing to do in default implementation
//		}
//	}
//}
