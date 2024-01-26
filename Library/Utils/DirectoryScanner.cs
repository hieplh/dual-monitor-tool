#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2023  Gerald Evans
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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DMT.Library.Utils
{
	public class DirectoryScanner
	{
		// delegate to check if we want this file
		public delegate bool WantFile(FileSystemInfo info);

		public static List<string> GetFilenames(string baseDirectory, bool recursive, WantFile wantFile)
		{
			List<string> filenames = new List<string>();

			if (!string.IsNullOrEmpty(baseDirectory))
			{
				// check in case we are passed a single file rather than a directory
				if (File.Exists(baseDirectory))
				{
					FileInfo info = new FileInfo(baseDirectory);
					if (wantFile(info))
					{
						filenames.Add(info.FullName);
					}
				}
				else
				{
					AddFilenames(baseDirectory, recursive, wantFile, filenames);
				}
			}

			return filenames;
		}

		public static bool IsImageFile(FileSystemInfo info)
		{
			string extension = info.Extension.ToLower();
			switch (extension)
			{
				case ".jpg": // drop into ".jpeg"
				case ".jpeg":
					return true;
				case ".png":
					return true;
				case ".bmp":
					return true;
				case ".gif":
					return true;
			}

			return false;
		}

		static void AddFilenames(string baseDirectory, bool recursive, WantFile wantFile, List<string> filenames)
		{
			try
			{
				DirectoryInfo dir = new DirectoryInfo(baseDirectory);

				FileSystemInfo[] infos = dir.GetFileSystemInfos();
				foreach (FileSystemInfo info in infos)
				{
					if (info is FileInfo)
					{
						if (wantFile(info))
						{
							filenames.Add(info.FullName);
						}
					}
					else if (info is DirectoryInfo)
					{
						if (recursive)
						{
							AddFilenames(info.FullName, recursive, wantFile, filenames);
						}
					}
				}
			}
			catch (Exception)
			{
				// ignore any i/o errors
			}
		}
	}
}
