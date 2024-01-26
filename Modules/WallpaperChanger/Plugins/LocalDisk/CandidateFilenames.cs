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

namespace DMT.Modules.WallpaperChanger.Plugins.LocalDisk
{
	using System.Collections.Generic;

	using DMT.Library.Utils;

	/// <summary>
	/// List of candidate image filenames
	/// </summary>
	public class CandidateFilenames
	{
		public class Options
		{
			public LocalDiskConfig.OrderType Type { get; set; }

			// common options
			public bool Recursive { get; set; }

			// options for LocalDiskConfig.OrderType.Random
			public bool Rescan { get; set; }
			public bool Cycle { get; set; }

			public bool Monitor2K { get; set; }

			// options for LocalDiskConfig.OrderType.Alphabetical
			public bool RandomStart { get; set; }
		}

		string _directory = null;
		//bool _recursive = false;
		//bool _cycle = false;
		Options _options = null;
		List<string> _filenames = null;

		// for LocalDiskConfig.OrderType.Random
		NumberCycler _filenameIndexCycler = null;

		// for LocalDiskConfig.OrderType.Alphabetical
		int _nextIndex = 0;

		/// <summary>
		/// Sets the directory to be searched
		/// </summary>
		/// <param name="directory">Directory to search</param>
		/// <param name="recursive">True if to search recursively through sub directories</param>
		//public void SetDirectory(string directory, bool recursive, bool cycle)
		public void SetDirectory(string directory, Options options)
		{
			// if Directory or recursive change, we must clear any cached filenames
			if (directory != _directory)
			{
				NumberCycler.ClearCache(_directory);
				_directory = directory;
				_filenames = null;
			}

			if (_options == null)
			{
				_filenames = null;
			}
			else
			{
				if (options.Recursive != _options.Recursive)
				{
					_filenames = null;
				}

				if (options.Type != _options.Type)
				{
					_filenames = null;
				}

				switch (options.Type)
				{
					case LocalDiskConfig.OrderType.Random:
						if (options.Rescan)
						{
							_filenames = null;
						}
						if (options.Cycle != _options.Cycle)
						{
							_filenameIndexCycler = null;
						}
						break;
				}
			}

			if (_filenames == null)
			{
				_filenameIndexCycler = null;
			}

			_options = options;
		}

		/// <summary>
		/// Clears the cached search results
		/// </summary>
		public void ClearCache()
		{
			_filenames = null;
		}

		/// <summary>
		/// Gets a random file form the search results
		/// </summary>
		/// <returns></returns>
		public string GetNextImageFilename()
		{
			if (_filenames == null)
			{
				_filenames = GetCandidateFilenames();
				_filenameIndexCycler = null;
			}

			if (_filenames.Count > 0)
			{
				switch (_options.Type)
				{
					case LocalDiskConfig.OrderType.Random:
						return GetNextRandomImageFilename();

					case LocalDiskConfig.OrderType.Alphabetical:
						return GetNextAlphabeticalImageFilename();
				}
			}

			return null;
		}

		string GetNextRandomImageFilename()
		{
			if (_options.Cycle)
			{
				// need to cycle through all, but in a random order
				if (_filenameIndexCycler == null || _filenameIndexCycler.Count == 0)
				{
					
					// need to start a new cycle
					_filenameIndexCycler = NumberCycler.Instance(0, _filenames.Count - 1, _directory);
				}

				int index = _filenameIndexCycler.NextRandom();
				if (index >= 0 && index < _filenames.Count)
				{
					return _filenames[index];
				}
			}
			else
			{
				// choose one at random independent of previously returned images
				int index = RNG.Next(_filenames.Count);
				return _filenames[index];
			}

			return null;
		}

		string GetNextAlphabeticalImageFilename()
		{
			// check in case we need to wrap around
			if (_nextIndex >= _filenames.Count)
			{
				_nextIndex = 0;
			}

			if (_nextIndex < _filenames.Count)
			{
				return _filenames[_nextIndex++];
			}

			return null;
		}

		List<string> GetCandidateFilenames()
		{
			//List<string> candidateFilenames = GetCandidateFilenames(_directory, _options.Recursive);
			List<string> candidateFilenames = DirectoryScanner.GetFilenames(_directory, _options.Recursive, DirectoryScanner.IsImageFile);

			if (_options.Type == LocalDiskConfig.OrderType.Alphabetical)
			{
				// case is significant (for now)
				candidateFilenames.Sort();
			}

			return candidateFilenames;
		}

		//#region Directory scanning for images
		//List<string> GetCandidateFilenames(string baseDirectory, bool recursive)
		//{
		//	List<string> candidateFilenames = new List<string>();

		//	if (!string.IsNullOrEmpty(baseDirectory))
		//	{
		//		// check in case we are passed a single file rather than a directory
		//		if (File.Exists(baseDirectory))
		//		{
		//			FileInfo info = new FileInfo(baseDirectory);
		//			if (IsImageFile(info))
		//			{
		//				candidateFilenames.Add(info.FullName);
		//			}
		//		}
		//		else
		//		{
		//			AddCandidateFilenames(baseDirectory, recursive, candidateFilenames);
		//		}
		//	}

		//	return candidateFilenames;
		//}


		//void AddCandidateFilenames(string baseDirectory, bool recursive, List<string> candidateFilenames)
		//{
		//	try
		//	{
		//		DirectoryInfo dir = new DirectoryInfo(baseDirectory);

		//		FileSystemInfo[] infos = dir.GetFileSystemInfos();
		//		foreach (FileSystemInfo info in infos)
		//		{
		//			if (info is FileInfo)
		//			{
		//				if (IsImageFile(info))
		//				{
		//					candidateFilenames.Add(info.FullName);
		//				}
		//			}
		//			else if (info is DirectoryInfo)
		//			{
		//				if (recursive)
		//				{
		//					AddCandidateFilenames(info.FullName, recursive, candidateFilenames);
		//				}
		//			}
		//		}
		//	}
		//	catch (Exception)
		//	{
		//		// ignore any i/o errors
		//	}
		//}

		//bool IsImageFile(FileSystemInfo info)
		//{
		//	string extension = info.Extension.ToLower();
		//	switch (extension)
		//	{
		//		case ".jpg": // drop into ".jpeg"
		//		case ".jpeg":
		//			return true;
		//		case ".png":
		//			return true;
		//		case ".bmp":
		//			return true;
		//		case ".gif":
		//			return true;
		//	}

		//	return false;
		//}
		//#endregion Directory scanning for images
	}
}
