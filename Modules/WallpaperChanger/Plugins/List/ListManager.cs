using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DMT.Modules.WallpaperChanger.Plugins.List
{
	/// <summary>
	/// Manages a list of paths/urls read from a file
	/// </summary>
	class ListManager
	{
		string _listFnm;
		List<string> _imagePaths;
		DateTime _lastUpdateTime;

		public int Count
		{
			get
			{
				//CheckHaveLoaded();
				if (_imagePaths != null)
				{
					return _imagePaths.Count;
				}
				return 0;
			}
		}

		public string this[int idx]
		{
			get
			{
				//CheckHaveLoaded();
				if (_imagePaths != null && idx >= 0 && idx < _imagePaths.Count)
				{
					return _imagePaths[idx];
				}
				return null;
			}
		}



		///// <summary>
		///// Gets or sets a value indicating if we should check if the file has been modified
		///// before returning the next image.
		///// If we detect that it has been modified then we have to reset progress through the file.
		///// </summary>
		//public bool CheckForUpdates { get; set; }

		//public ListManager(string listFnm, bool checkForUpdates = false)
		public ListManager(string listFnm)
		{
			_listFnm = listFnm;
			//CheckForUpdates = checkForUpdates;

			// indicate that we have not read the file yet.
			_imagePaths = null;
			_lastUpdateTime = DateTime.MinValue;
		}

		public void SetListName(string listFnm)
		{
			// any change to fnm means we reload it
			if (listFnm != _listFnm)
			{
				_listFnm = listFnm;
				_imagePaths = null;
				_lastUpdateTime = DateTime.MinValue;
			}
		}

		public bool CheckIfChanged()
		{
			bool changed = false;

			try
			{
				DateTime lastUpdateTime = File.GetLastWriteTime(_listFnm);
				if (lastUpdateTime > _lastUpdateTime)
				{
					LoadListFile();
					_lastUpdateTime = lastUpdateTime;
					changed = true;
				}
			}
			catch (Exception)
			{
				throw;
			}

			return changed;
		}

		//void CheckHaveLoaded()
		//{
		//	try
		//	{
		//		if (CheckForUpdates)
		//		{
		//			// this will load the file initially and whenever it is updated
		//			CheckForUpdate();
		//		}
		//		else
		//		{
		//			if (_imagePaths == null)
		//			{
		//				LoadListFile();
		//			}
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		string msg = ex.Message;

		//		// TODO
		//	}
		//}

		//void CheckForUpdate()
		//{
		//	try
		//	{
		//		DateTime lastUpdateTime = File.GetLastWriteTime(_listFnm);
		//		if (lastUpdateTime > _lastUpdateTime)
		//		{
		//			LoadListFile();
		//			_lastUpdateTime = lastUpdateTime;
		//		}
		//	}
		//	catch (Exception)
		//	{
		//		throw;
		//	}

		//}

		void LoadListFile()
		{
			_imagePaths = new List<string>();

			try
			{
				// TODO: it may be better to do this as a stream rather than reading the whole file in one go?
				string[] allLines = File.ReadAllLines(_listFnm);

				_imagePaths = new List<string>(allLines.Length);

				// we allow blank lines and comments starting with '#', so filter these out
				foreach (string line in allLines)
				{
					string cleanedLine = line.Trim();
					if (!string.IsNullOrEmpty(cleanedLine) && cleanedLine[0] != '#')
					{
						_imagePaths.Add(cleanedLine);
					}
				}
			}
			catch (Exception)
			{
				// probably file doesn't exist or can't be read
				// let caller handle this
				throw;
			}
		}


	}
}
