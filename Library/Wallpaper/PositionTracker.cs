using DMT.Library.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DMT.Library.Wallpaper
{
	public class PositionTracker
	{
		const int FILE_ID = 0x50544D44; // "DMTP"
		const int FILE_VERSION = 1;
		//const int FILE_MAX_ENTRIES = 1000000; // arbitary value 

		public bool UseRandomOrder { get; private set; }

		bool _persist;
		public bool Persist 
		{
			get
			{
				return _persist;
			}
			set
			{
				if (value != _persist)
				{
					if (value)
					{
						// make sure we have a persisted copy 
						SaveFile(true);
					}
					else
					{
						// remove the persisted copy
						DeleteFile();
					}
					_persist = value;
				}
			}
		}
 
		// number of items to iterate through
		int _len;

		//// indicates if we will iterate in a random order or not
		//bool _random;

		// filename used to persist position / random order (if required)
		// null if not being used (user doesn't want to persist this info)
		string _positionFnm;

		// indicates if initial position/order has been loaded
		// allows lazy loading of the above file
		bool _initialised;

		// the next position in the list
		// if past end, means we need to reset the position/list
		int _nextPosition;

		// randomised ordering of the list
		// we try to only build this when needed
		int[] _randOrder;

		//public PositionTracker(int len, bool random, int position, int[] randOrder)
		public PositionTracker(int len, bool random, bool persist, string positionFnm)
		{
			// information regarding 
			_len = len;
			UseRandomOrder = random;
			//_persist = persist;

			_positionFnm = positionFnm;
			_initialised = false;

			// minimum initialisation of position /order 
			_nextPosition = _len + 1; // to force list reset 
			_randOrder = null;

			//if (!Persist)
			//{
			//	// prevent files being left around when user disables persist option
			//	DeleteFile();
			//}

			// this will delete any the existing file if persist is off
			Persist = persist;
		}

		public void Reset(int len, bool random)
		{
			_len = len;
			UseRandomOrder = random;
			_nextPosition = _len + 1; // to force list reset 
			_randOrder = null;
		}

		// this may be more efficient (lazy list gen), but don't like it
		public int GetNextPoistion()
		{
			int ret = -1;
			bool randOrderChanged = false;

			// This will load the persisted position if required
			CheckInitialised();

			int positionIdx = _nextPosition;

			if (_nextPosition >= _len)
			{
				_nextPosition = 0;
				if (UseRandomOrder)
				{
					GenerateNewRandomOrder();
					randOrderChanged = true;
				}
			}

			if (UseRandomOrder)
			{
				if (_len > 0)
				{
					ret = _randOrder[_nextPosition];
				}
			}
			else
			{
				ret = _nextPosition;
			}

			_nextPosition++;

			if (Persist)
			{
				SaveFile(randOrderChanged);
			}

			return ret;
		}

		///// <summary>
		///// Deletes the file being used for persistence, if there is one
		///// </summary>
		//public void Delete()
		//{
		//	DeleteFile();
		//}

		//public int GetNextPoistion()
		//{
		//	int ret = -1;

		//	int positionIdx = _nextPosition;

		//	if (_random)
		//	{
		//		ret = _randOrder[_nextPosition];
		//	}
		//	else
		//	{
		//		ret = _nextPosition;
		//	}

		//	_nextPosition++;
		//	if (_nextPosition >= _len)
		//	{
		//		_nextPosition = 0;
		//		if (_random)
		//		{
		//			GenerateNewRandomOrder();
		//		}
		//	}

		//	return ret;
		//}

		void CheckInitialised()
		{
			if (!_initialised)
			{
				if (!string.IsNullOrEmpty(_positionFnm) && Persist)
				{
					LoadFile();
				}

				_initialised = true;
			}
		}


		void GenerateNewRandomOrder()
		{
			_randOrder = new int[_len];

			// these are the numbers that will be used to generate the new random order
			int[] unusedNums = new int[_len];
			for (int idx = 0; idx < _len; idx++)
			{
				unusedNums[idx] = idx;
			}

			// populate _randOrder with random numbers taken from unusedNums
			int unusedLen = unusedNums.Length;
			for (int idx = 0; idx < _len; idx++)
			{
				// choose random index into unusedNums
				int unusedIdx = RNG.Next(0, unusedLen);
				// add the corresponding value to the random order
				_randOrder[idx] = unusedNums[unusedIdx];

				// removed this value from the unusedNums
				// by replacing it with the last value in the list, and shorten the list
				unusedNums[unusedIdx] = unusedNums[unusedLen - 1];
				unusedLen--;
			}

		}

		#region File I/O
		void LoadFile()
		{
			if (!string.IsNullOrEmpty(_positionFnm))
			{
				//CheckPositionTrackerDirExists();

				if (File.Exists(_positionFnm))
				{
					using (FileStream fs = File.OpenRead(_positionFnm))
					{
						long fileLength = fs.Length;
						using (BinaryReader r = new BinaryReader(fs))
						{
							int id = r.ReadInt32();
							if (id != FILE_ID)
							{
								ReadError("ID of 0x{0:x} unexpected", id);
							}
							int version = r.ReadInt32();
							if (version > FILE_VERSION)
							{
								ReadError("Versions of {0} not supported", version);
							}

							int len = r.ReadInt32();
							//if (len < 0 || len > FILE_MAX_ENTRIES)
							_nextPosition = r.ReadInt32();

							int lenRandOrder = r.ReadInt32();

							// check file size is consistent with what the header says
							// note: it is OK if the actual file length is greater than what we need
							// as we never truncate the file
							long minRequiredLength = (5 + lenRandOrder) * sizeof(Int32);
							if (fileLength < minRequiredLength)
							{
								ReadError("Actual file length ({0}) needs to be at least {1}", fileLength, minRequiredLength);
							}

							// read the random order
							_randOrder = new int[lenRandOrder];
							for (int idx = 0; idx < lenRandOrder; idx++)
							{
								_randOrder[idx] = r.ReadInt32();
							}
						}
					}
				}
			}
		}

		void ReadError(string format, params object[] args)
		{
			string msg = string.Format(format, args) + " in " + _positionFnm;
			throw new ApplicationException(msg);
		}

		void SaveFile(bool includeRandOrder)
		{
			if (_initialised)
			{
				if (!string.IsNullOrEmpty(_positionFnm))
				{
					CheckPositionTrackerDirExists();

					using (FileStream fs = File.OpenWrite(_positionFnm))
					{
						using (BinaryWriter w = new BinaryWriter(fs))
						{
							// write the header
							w.Write(FILE_ID);
							w.Write(FILE_VERSION);

							// write the length of the list
							w.Write(_len);
							// write next position in the list
							w.Write(_nextPosition);

							// to improve speed, especially with big lists,
							// we only write the randdom order when it changes
							if (includeRandOrder)
							{
								// write the length of the random order that follows
								int lenRandOrder = _len;
								if (!UseRandomOrder)
								{
									// the random order list is not required
									lenRandOrder = 0;
								}
								w.Write(lenRandOrder);

								// and now the random order (if needed)
								for (int idx = 0; idx < lenRandOrder; idx++)
								{
									w.Write(_randOrder[idx]);
								}
							}
						}
					}
				}
			}
		}

		void DeleteFile()
		{
			if (!string.IsNullOrEmpty(_positionFnm))
			{
				//CheckPositionTrackerDirExists();

				if (File.Exists(_positionFnm))
				{
					File.Delete(_positionFnm);
				}
			}
		}

		void CheckPositionTrackerDirExists()
		{
			// TODO: we could do this at startup,
			// but this means user can delete the directory while we are running
			// and it will be created automatically if needed
			string dir = Path.GetDirectoryName(_positionFnm);
			Directory.CreateDirectory(dir);
		}
		#endregion File I/O
	}
}
