using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMT.Library.Wallpaper
{
	public class PositionTrackerKey
	{
		Guid _key;

		public PositionTrackerKey(string keyString = null)
		{
			_key = StringToKey(keyString);
		}

		PositionTrackerKey(Guid key)
		{
			_key = key;
		}

		public bool IsEmpty()
		{
			return _key == Guid.Empty;
		}

		public override string ToString()
		{
			return KeyToString(_key);
		}

		public override bool Equals(object obj)
		{
			PositionTrackerKey other = obj as PositionTrackerKey;
			if (other != null)
			{
				return _key.Equals(other._key);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return _key.GetHashCode();
		}

		public static PositionTrackerKey NewKey()
		{
			Guid key = Guid.NewGuid();
			return new PositionTrackerKey(key);
		}

		static Guid StringToKey(string keyString)
		{
			Guid key;
			if (Guid.TryParse(keyString, out key))
			{
				return key;
			}

			return Guid.Empty;
		}

		static string KeyToString(Guid key)
		{
			if (key == Guid.Empty)
			{
				return "";
			}

			return key.ToString();
		}
	}
}
