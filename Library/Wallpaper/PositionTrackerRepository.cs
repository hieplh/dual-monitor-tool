using DMT.Library.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DMT.Library.Wallpaper
{
	class PositionTrackerRepository
	{
		#region Singleton support
		// the single instance of the controller object
		static readonly PositionTrackerRepository SingleInstance = new PositionTrackerRepository();

		// Explicit static constructor to tell C# compiler
		// not to mark type as beforefieldinit
		static PositionTrackerRepository()
		{
		}

		PositionTrackerRepository()
		{
			//LoadFileLocations();
		}

		/// <summary>
		/// Gets the singleton instance of the class
		/// </summary>
		public static PositionTrackerRepository Instance
		{
			get
			{
				return SingleInstance;
			}
		}
		#endregion

		public PositionTracker GetPositionTracker(PositionTrackerKey key, int len, bool random, bool persist)
		{
			return new PositionTracker(len, random, persist, GetPositionTrackerFnm(key));
		}

		string GetPositionTrackerFnm(PositionTrackerKey key)
		{
			string dir = FileLocations.Instance.WallpaperPositionTrackerDirectory;
			string fnm = Path.Combine(dir, key.ToString() + ".dmtp");

			return fnm;
		}
	}
}
