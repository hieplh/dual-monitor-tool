using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DMT.Modules.General
{
	/// <summary>
	/// Form to change the numeric value (within a given range) for one of the monitor properties
	/// </summary>
	public partial class EditNumericValueForm : Form
	{
		// this is what we will need to call to reflect the change in the numeric value
		public delegate void ReflectChange(int monitorIndex, int numericValue);

		int _monitorIndex;
		int _originalValue;
		NumericRange _numericRange;
		ReflectChange _reflectChange;

		public EditNumericValueForm(string title, int monitorIndex, NumericRange numericRange, ReflectChange reflectChange)
		{
			_monitorIndex = monitorIndex;
			_numericRange = numericRange;
			_reflectChange = reflectChange;

			// remember original value, so we can restore it if the user cancels
			_originalValue = _numericRange.CurValue;

			InitializeComponent();

			this.Text = title;

			trackBarBrightness.Minimum = _numericRange.MinValue;
			trackBarBrightness.Maximum = _numericRange.MaxValue;
			trackBarBrightness.Value = _numericRange.CurValue;

			labelMinBrightness.Text = _numericRange.MinValue.ToString();
			labelMaxBrightness.Text = _numericRange.MaxValue.ToString();
			ShowCurBrightnessValue();
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			// restore value to original value
			_reflectChange(_monitorIndex, _originalValue);

			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void trackBarBrightness_Scroll(object sender, EventArgs e)
		{
			_reflectChange(_monitorIndex, trackBarBrightness.Value);
			ShowCurBrightnessValue();
		}

		void ShowCurBrightnessValue()
		{
			labelCurBrightness.Text = trackBarBrightness.Value.ToString();
		}



		//public EditNumericValueForm(DisplayDevices displayDevices, int monitorIndex, uint curBrightness, uint minBrightness, uint maxBrightness)
		//{
		//	_displayDevices = displayDevices;
		//	_monitorIndex = monitorIndex;
		//	_originalBrightness = curBrightness;

		//	InitializeComponent();

		//	trackBarBrightness.Minimum = (int)minBrightness;
		//	trackBarBrightness.Maximum = (int)maxBrightness;
		//	trackBarBrightness.Value = (int)curBrightness;

		//	labelMinBrightness.Text = minBrightness.ToString();
		//	labelMaxBrightness.Text = maxBrightness.ToString();
		//	ShowCurBrightnessValue();
		//}

		//private void buttonOK_Click(object sender, EventArgs e)
		//{
		//	DialogResult = DialogResult.OK;
		//	Close();
		//}

		//private void buttonCancel_Click(object sender, EventArgs e)
		//{
		//	// restore brightness to original value
		//	_displayDevices.ChangeMonitorBrightness(_monitorIndex, _originalBrightness);

		//	DialogResult = DialogResult.Cancel;
		//	Close();
		//}

		//private void trackBarBrightness_Scroll(object sender, EventArgs e)
		//{
		//	_displayDevices.ChangeMonitorBrightness(_monitorIndex, (uint)trackBarBrightness.Value);
		//	ShowCurBrightnessValue();
		//}

		//void ShowCurBrightnessValue()
		//{
		//	labelCurBrightness.Text = trackBarBrightness.Value.ToString();
		//}
	}
}
