#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2010-2015  Gerald Evans
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

//#define USE_DEDICATED_THREAD

namespace DMT.Library.Wallpaper
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Drawing;
	using System.Drawing.Imaging;
	using System.IO;
	using System.Text;
	using System.Threading;
	using System.Windows.Forms;

	using DMT.Library.Environment;
	using DMT.Library.PInvoke;
	using DMT.Library.Settings;
	using Microsoft.Win32;
	//using Resources;
	using GuiUtils;

	/// <summary>
	/// This handles Windows specific aspects of wallpaper.
	/// This includes handling the case where you have a monitor to the left
	/// or above the primary monitor, as Windows requires that (0,0) in
	/// the wallpaper corresponds to (0,0) on your primary monitor.
	/// </summary>
	class WindowsWallpaper
	{
		ILocalEnvironment _localEnvironment;
		private Image srcImage;
		private Rectangle virtualDesktop;

		/// <summary>
		/// Initialises a new instance of the <see cref="WindowsWallpaper" /> class.
		/// Takes the virtual desktop rectangle and
		/// an image which is laid out corresponding to the virtual desktop,
		/// so the TLHC of the image corresponds to the TLHC of the virtual desktop
		/// which may not be the same as the TLHC of the primary monitor.
		/// </summary>
		/// <param name="localEnvironment">The local environment</param>
		/// <param name="srcImage">image for the wallpaper</param>
		/// <param name="virtualDesktop">virtual desktop rectangle</param>
		public WindowsWallpaper(ILocalEnvironment localEnvironment, Image srcImage, Rectangle virtualDesktop)
		{
			_localEnvironment = localEnvironment;
			Debug.Assert(srcImage.Size == virtualDesktop.Size, "Image size is wrong");
			this.srcImage = srcImage;
			this.virtualDesktop = virtualDesktop;
		}

		///// <summary>
		///// Sets the Windows wallpaper.
		///// This will create a new image if the primary monitor
		///// is not both the leftmost and topmost monitor.
		///// </summary>
		///// <param name="useFade">If true, tries to use a smooth fade between wallpapers</param>
		//public void SetWallpaper(bool useFade)
		//{
		//	bool wrapped;
		//	Image image = WrapImage(out wrapped);

		//	SetWallpaper(image, useFade);

		//	if (wrapped)
		//	{
		//		image.Dispose();
		//	}
		//}

		public void SetWallpaper(WallpaperImageFormat.ImageFormat fileType, int quality, bool useFade)
		{
			bool wrapped;
			Image image = WrapImage(out wrapped);

			SetWallpaper(image, fileType, quality, useFade);

			if (wrapped)
			{
				image.Dispose();
			}
		}

		/// <summary>
		/// Saves the wallpaper to a file in a format usable by most? 
		/// automatic screen changers.
		/// This will create a new image if the primary monitor
		/// is not both the leftmost and topmost monitor.
		/// </summary>
		/// <param name="fnm">Filename to save the wallpaper too</param>
		public void SaveWallpaper(string fnm)
		{
			bool wrapped;
			Image image = WrapImage(out wrapped);

			SaveWallpaper(image, fnm);

			if (wrapped)
			{
				image.Dispose();
			}
		}


		//void SetWallpaper(Image wallpaper, bool useFade)
		//{
		//	//string path = FileLocations.Instance.WallpaperFilename;
		//	System.Drawing.Imaging.ImageFormat imageFormat;
		//	string path = GetWallpaperFilename(out imageFormat);

		//	try
		//	{
		//		if (SaveWindowsWallpaperImage(wallpaper, path, imageFormat))
		//		{
		//			// make sure image is tiled (must do this for both normal and ActiveDesktop wallpaper)
		//			SetTiledWallpaper();

		//			// XP doesn't support fade and attempting to use it results in the wallpaper not changing
		//			// so we force fade off for XP
		//			if (useFade && _localEnvironment.IsVistaOrLater())
		//			{
		//				SetActiveDesktopWallpaper(path);
		//			}
		//			else
		//			{
		//				SetDesktopWallpaper(path);
		//			}

		//			// save the location of the wallpaper bitmap so that the screen saver can pick it up
		//			DmtRegistry.SetDmtWallpaperFilename(path);
		//		}
		//		else
		//		{
		//			MsgDlg.SystemError("Faled to save Windows wallpaper to: " + path);
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		MsgDlg.SystemError("Set wallpaper: " + ex.Message);
		//	}
		//}

		void SetWallpaper(Image wallpaper, WallpaperImageFormat.ImageFormat fileType, int quality, bool useFade)
		{
			string basePath = FileLocations.Instance.WallpaperFilename;

			try
			{
				string path;
				if (SaveWindowsWallpaperImage(wallpaper, basePath, fileType, quality, out path))
				{
					// make sure image is tiled (must do this for both normal and ActiveDesktop wallpaper)
					SetTiledWallpaper();

					// XP doesn't support fade and attempting to use it results in the wallpaper not changing
					// so we force fade off for XP
					if (useFade && _localEnvironment.IsVistaOrLater())
					{
						SetActiveDesktopWallpaper(path);
					}
					else
					{
						SetDesktopWallpaper(path);
					}

					// save the location of the wallpaper bitmap so that the screen saver can pick it up
					DmtRegistry.SetDmtWallpaperFilename(path);
				}
				else
				{
					MsgDlg.SystemError("Faled to save Windows wallpaper to: " + path);
				}
			}
			catch (Exception ex)
			{
				MsgDlg.SystemError("Set wallpaper: " + ex.Message);
			}
		}

		//string GetWallpaperFilename(out System.Drawing.Imaging.ImageFormat imageFormat)
		//{
		//	// this is the full path, but assumes the extension is .bmp
		//	string path = FileLocations.Instance.WallpaperFilename;

		//	if (_localEnvironment.IsWin7OrLater())
		//	{
		//		// It seems that on later versions of Windows, 
		//		// if a BMP is used for the wallpaper, 
		//		// then Windows will compress this before display using a lossy format (presumably JPEG?).
		//		// On the other hand, it the wallpaper is a PNG file, then that is used as is,
		//		// so we save image as PNG to prevent any reduction in quality.
		//		// Thanks to Manuel Silva for finding out about this.
		//		//
		//		// See https://www.reddit.com/r/Windows10/comments/j9z73c/psa_use_png_files_as_wallpapers/
		//		// and https://www.reddit.com/r/pcgaming/comments/6kry64/protip_windows_automatically_compresses_wallpaper/
		//		//
		//		// However, Windows imposes a file size limit for PNG files,
		//		// and if the generated wallpaper is larger than this, 
		//		// then the wallpaper will not be displayed and the screens will be filled with the default background colour
		//		//
		//		path = Path.ChangeExtension(path, ".png");
		//		imageFormat = System.Drawing.Imaging.ImageFormat.Png;
		//	}
		//	else
		//	{
		//		// Windows XP only supports BMP for wallpaper
		//		// (don't know about Vista?)
		//		// path should already be a .bmp, but JIC
		//		path = Path.ChangeExtension(path, ".bmp");
		//		imageFormat = System.Drawing.Imaging.ImageFormat.Bmp;
		//	}

		//	return path;
		//}

		//bool SaveWindowsWallpaperImage(Image wallpaperImage, string path, System.Drawing.Imaging.ImageFormat imageFormat)
		//{
		//	// There seems to be an issue, where sometimes the Windows wallpaper can not be saved at startup
		//	// seems to be a permission problem!?
		//	// so we allow multiple attempts at this with a delay between each.
		//	int maxAttempts = 3;

		//	for (int attempt = 1; attempt <= maxAttempts; attempt++)
		//	{
		//		try
		//		{
		//			wallpaperImage.Save(path, imageFormat);

		//			//if (attempt > 1)
		//			//{
		//			//	string msg = string.Format("Windows wallpaper saved to {0} on attempt {1}", path, attempt);
		//			//	MessageBox.Show(msg, CommonStrings.MyTitle);
		//			//}

		//			return true;
		//		}
		//		catch (Exception ex)
		//		{
		//			//MessageBox.Show("wallpaper.Save(" + path + ") : " + ex.Message, CommonStrings.MyTitle);
		//			//throw;
		//		}

		//		// add a short(ish) delay before retrying
		//		Thread.Sleep(1000);
		//	}

		//	return false;
		//}


		bool SaveWindowsWallpaperImage(Image wallpaperImage, string basePath, WallpaperImageFormat.ImageFormat fileType, int quality, out string path)
		{
			ImageFormat imageFormat;
			ImageCodecInfo imageEncoder = null;
			EncoderParameters imageEncoderParameters = null;

			switch (fileType)
			{
				case WallpaperImageFormat.ImageFormat.PNG:
					path = Path.ChangeExtension(basePath, ".png");
					imageFormat = System.Drawing.Imaging.ImageFormat.Png;
					break;

				case WallpaperImageFormat.ImageFormat.JPG:
					path = Path.ChangeExtension(basePath, ".jpg");
					imageFormat = System.Drawing.Imaging.ImageFormat.Jpeg; // not used
					imageEncoder = GetEncoder(ImageFormat.Jpeg);
					imageEncoderParameters = new EncoderParameters(1);
					imageEncoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)quality);
					break;

				default: // use bmp as fallback as it should always work
				case WallpaperImageFormat.ImageFormat.BMP:
					path = Path.ChangeExtension(basePath, ".bmp");
					imageFormat = System.Drawing.Imaging.ImageFormat.Bmp;
					break;
			}


			// There seems to be an issue, where sometimes the Windows wallpaper can not be saved at startup
			// seems to be a permission problem!?
			// so we allow multiple attempts at this with a delay between each.
			int maxAttempts = 3;

			for (int attempt = 1; attempt <= maxAttempts; attempt++)
			{
				try
				{
					if (imageEncoder != null)
					{
						wallpaperImage.Save(path, imageEncoder, imageEncoderParameters);
					}
					else
					{
						wallpaperImage.Save(path, imageFormat);
					}

					//if (attempt > 1)
					//{
					//	string msg = string.Format("Windows wallpaper saved to {0} on attempt {1}", path, attempt);
					//	MessageBox.Show(msg, CommonStrings.MyTitle);
					//}

					return true;
				}
				catch (Exception ex)
				{
					//MessageBox.Show("wallpaper.Save(" + path + ") : " + ex.Message, CommonStrings.MyTitle);
					//throw;
				}

				// add a short(ish) delay before retrying
				Thread.Sleep(1000);
			}

			return false;
		}

		// from https://learn.microsoft.com/en-us/dotnet/api/system.drawing.imaging.encoderparameter?view=windowsdesktop-7.0&redirectedfrom=MSDN
		ImageCodecInfo GetEncoder(ImageFormat format)
		{
			ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

			foreach (ImageCodecInfo codec in codecs)
			{
				if (codec.FormatID == format.Guid)
				{
					return codec;
				}
			}

			return null;
		}

		void SetTiledWallpaper()
		{
			using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true))
			{
				key.SetValue("TileWallpaper", "1");
				key.SetValue("WallpaperStyle", "0");
			}
		}

		void SetDesktopWallpaper(string path)
		{
			// now set the wallpaper
			NativeMethods.SystemParametersInfo(NativeMethods.SPI_SETDESKWALLPAPER, 0, path, NativeMethods.SPIF_UPDATEINIFILE | NativeMethods.SPIF_SENDWININICHANGE);
		}

		/*
		 * This section tries to find a solution to the performance issue in Active Desktop 
		 * after a large number of smooth wallpaper changes have been performed
		 */

#if USE_DEDICATED_THREAD
		/*
		 * This uses a single thread to perform all active desktop wallpaper changes.
		 * This means we can grab the Active Desktop COM object once and hold on to it.
		 *
		 * Note: this didn't fix the issue
		 */
		static Thread _activeDesktopWallpaperThread = null;
		static string _wallpaperForActiveDesktop;
		static EventWaitHandle _newWallpaperAvailable;

		void SetActiveDesktopWallpaper(string path)
		{
			if (_activeDesktopWallpaperThread == null)
			{
				_newWallpaperAvailable = new EventWaitHandle(false, EventResetMode.AutoReset);
				_activeDesktopWallpaperThread = new Thread(() => ActiveDesktopWallpaperThread());
				_activeDesktopWallpaperThread.SetApartmentState(ApartmentState.STA);
				_activeDesktopWallpaperThread.Start();
			}

			_wallpaperForActiveDesktop = path;
			_newWallpaperAvailable.Set();
		}

		// TODO: need a way to kill this thread when DMT exits
		static void ActiveDesktopWallpaperThread()
		{
			EnableActiveDesktop();

			ActiveDesktop.IActiveDesktop activeDesktop = ActiveDesktop.GetActiveDesktop();

			// now sleep until we are told new wallpaper is available
			for (; ; )
			{
				_newWallpaperAvailable.WaitOne();

				// we assume it is safe to pick this up
				string path = _wallpaperForActiveDesktop;
				activeDesktop.SetWallpaper(path, 0);
				activeDesktop.ApplyChanges(ActiveDesktop.AD_Apply.ALL);
			}
		}
#else
		/*
		 * original code - creates a new thread for every wallpaper change
		 */
		void SetActiveDesktopWallpaper(string path)
		{
			ActiveDesktopWallpaperThread(path);
			//Thread thread = new Thread(() => ActiveDesktopWallpaperThread(path));
			//thread.SetApartmentState(ApartmentState.STA);
			//thread.Start();

			// don't see any need to wait for the thread to complete
		}

		static void ActiveDesktopWallpaperThread(string path)
		{
			EnableActiveDesktop();

			ActiveDesktop.IActiveDesktop activeDesktop = ActiveDesktop.GetActiveDesktop();

			//string path = _wallpaperForActiveDesktop;
			activeDesktop.SetWallpaper(path, 0);
			//activeDesktop.ApplyChanges(ActiveDesktop.AD_Apply.ALL | ActiveDesktop.AD_Apply.FORCE);
			// Using FORCE seems to cause some applications/windows to repaint themselves causing flicker
			activeDesktop.ApplyChanges(ActiveDesktop.AD_Apply.ALL);

			// GNE 14/11/20 - following shouldn't be needed and didn't help
			//int x = System.Runtime.InteropServices.Marshal.ReleaseComObject(activeDesktop);
			//System.Console.WriteLine("x: {0}", x);
		}

#endif

		static void EnableActiveDesktop()
		{
			IntPtr hWndProgman = NativeMethods.FindWindow("Progman", null);
			uint msg = 0x52C;	// TODO: need a const in Win32
			IntPtr wParam = IntPtr.Zero;
			IntPtr lParam = IntPtr.Zero;
			uint fuFlags = 0; // SMTO_NORMAL // TODO: need a const in Win32
			uint uTimeout = 500;	// in ms
			IntPtr lpdwResult = IntPtr.Zero;
			NativeMethods.SendMessageTimeout(hWndProgman, msg, wParam, lParam, fuFlags, uTimeout, out lpdwResult);
		}


		private void SaveWallpaper(Image wallpaper, string fnm)
		{
			try
			{
				wallpaper.Save(fnm, System.Drawing.Imaging.ImageFormat.Bmp);
			}
			catch (Exception ex)
			{
				MsgDlg.SystemError("Save wallpaper: " + ex.Message);
			}
		}

		private Image WrapImage(out bool wrapped)
		{
			if (NeedsWrapping())
			{
				// must wrap image
				// so that the four quadrants
				// ab
				// cd
				// where d would be the primary monitor to
				// dc
				// ba
				wrapped = true;
				Image image = new Bitmap(srcImage.Width, srcImage.Height);
				int xWrap = -virtualDesktop.Left;
				int xNotWrap = srcImage.Width - xWrap;
				int yWrap = -virtualDesktop.Top;
				int yNotWrap = srcImage.Height - yWrap;

				using (Graphics g = Graphics.FromImage(image))
				{
					// quadrant a
					if (xWrap > 0 && yWrap > 0)
					{
						g.DrawImage(
							srcImage,
							new Rectangle(xNotWrap, yNotWrap, xWrap, yWrap),
							new Rectangle(0, 0, xWrap, yWrap),
							GraphicsUnit.Pixel);
					}

					// quadrant b
					if (yWrap > 0 && xNotWrap > 0)
					{
						g.DrawImage(
							srcImage,
							new Rectangle(0, yNotWrap, xNotWrap, yWrap),
							new Rectangle(xWrap, 0, xNotWrap, yWrap),
							GraphicsUnit.Pixel);
					}

					// quadrant c
					if (xWrap > 0 && yNotWrap > 0)
					{
						g.DrawImage(
							srcImage,
							new Rectangle(xNotWrap, 0, xWrap, yNotWrap),
							new Rectangle(0, yWrap, xWrap, yNotWrap),
							GraphicsUnit.Pixel);
					}

					// quadrant d
					if (xNotWrap > 0 && yNotWrap > 0)
					{
						g.DrawImage(
							srcImage,
							new Rectangle(0, 0, xNotWrap, yNotWrap),
							new Rectangle(xWrap, yWrap, xNotWrap, yNotWrap),
							GraphicsUnit.Pixel);
					}
				}

				wrapped = true;
				return image;
			}
			else
			{
				// can use original src image
				wrapped = false;
				return srcImage;
			}
		}

		bool NeedsWrapping()
		{
			// On Windows versions prior to 8, (0,0) in the wallpaper corresponds to (0,0) on your primary monitor
			// On 8 (0,0) in the wallpaper corresponds to the TLHC of your monitors
			if (virtualDesktop.Left < 0 || virtualDesktop.Top < 0)
			{
				// TLHC is not (0,0)
				if (_localEnvironment.IsWin8OrLater())
				{
					// Win 8 and later want a direct mapping
					return false;
				}
				else
				{
					// earlier versions expect the primary TLHC to be (0,0)
					return true;
				}
			}
			else
			{
				// TLHC is (0,0) so direct mapping
				return false;
			}
		}
	}
}
