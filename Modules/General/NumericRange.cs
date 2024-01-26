using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMT.Modules.General
{
	public class NumericRange
	{
		public bool Available { get; set; }
		public bool Editable { get; set; }

		//public uint MinValue { get; set; }
		//public uint CurValue { get; set; }
		//public uint MaxValue { get; set; }

		// ints make perfrming deltas simpler
		public int MinValue { get; set; }
		public int CurValue { get; set; }
		public int MaxValue { get; set; }

		public NumericRange()
		{
			Available = false;
			Editable = false;
		}

		//public void SetValues(uint min, uint cur, uint max)
		public void SetValues(int min, int cur, int max)
		{
			// if we are called, then we assume the values are available
			Available = true;

			MinValue = min;
			CurValue = cur;
			MaxValue = max;

			// no point making editable if it can only take the 1 value
			Editable = (min < max);
		}

		public string GetCurDisplayableValue()
		{
			if (Available)
			{
				return CurValue.ToString();
			}
			else
			{
				return "N/A";
			}
		}

		public int GetAbsoluteValue(int value, bool isDelta)
		{
			int newValue;

			if (isDelta)
			{
				// value is a delta value from current value
				newValue = CurValue + value;
			}
			else
			{
				// value is an absolute value
				newValue = value;
			}

			return LimitValue(newValue);
		}

		//public int Delta(int delta)
		//{
		//	return LimitValue(CurValue + delta);
		//}

		int LimitValue(int newValue)
		{
			if (newValue < MinValue)
			{
				newValue = MinValue;
			}
			if (newValue > MaxValue)
			{
				newValue = MaxValue;
			}

			return newValue;
		}
	}
}
