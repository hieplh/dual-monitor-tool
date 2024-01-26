using DMT.Library.PInvoke;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace DMT.Library.HotKeys
{
	public partial class HotKeyCaptureForm : Form
	{

		// Win32 low level keyboard hook
		NativeMethods.HookProc llKeyboardProc;
		IntPtr llKeyboardHook = IntPtr.Zero;

		public HotKeyCaptureForm()
		{
			InitializeComponent();

			llKeyboardProc = llKeyboardHookCallback;

		}


		// This is the low level Keyboard hook callback
		// Processing in here should be efficient as possible
		// as it can be called very frequently.
		private int llKeyboardHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
		{
			if (nCode >= 0)
			{
				// lParam is a pointer to a KBDLLHOOKSTRUCT, but we only want the virtual key code
				// from this which is the first int, so instead of marshalling the entire structure
				// we just marshal the first int to minimise any performance hit 
				uint vkCode = (uint)Marshal.ReadInt32(lParam);
				Keys key = (Keys)vkCode;

				if (key == FreeMovementKey)
				{
					if (AllowFreeMovementKey)
					{
						int msg = (int)wParam;
						if (msg == NativeMethods.WM_KEYDOWN)
						{
							_freeMovementKeyPressed = true;
						}
						else if (msg == NativeMethods.WM_KEYUP)
						{
							_freeMovementKeyPressed = false;

							// must also rebuild the barriers as the cursor may now be on a different screen
							ReBuildBarriers();
						}
					}
				}
			}

			return NativeMethods.CallNextHookEx(llKeyboardHook, nCode, wParam, lParam);
		}


		private void HotKeyCaptureForm_KeyDown(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			string txt = VirtualKey.CodeToName(keyCode);
			textBoxCombo.Text = txt;
		}

		private void HotKeyCaptureForm_KeyUp(object sender, KeyEventArgs e)
		{

		}

		private void buttonCancel_KeyDown(object sender, KeyEventArgs e)
		{
			KeyCodeDown(sender, e);
		}

		private void buttonCancel_KeyUp(object sender, KeyEventArgs e)
		{

		}

		void KeyCodeDown(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			string txt = VirtualKey.CodeToName(keyCode);
			textBoxCombo.Text = txt;
			e.Handled = true;
		}

		private void buttonCancel_KeyPress(object sender, KeyPressEventArgs e)
		{
			//Keys keyCode = e. .KeyCode;
			//string txt = VirtualKey.CodeToName(keyCode);
			//textBoxCombo.Text = txt;
			e.Handled = true;
		}
	}
}
