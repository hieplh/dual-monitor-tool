#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2009-2015  Gerald Evans
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

namespace DMT.Library.HotKeys
{
	using System;
	using System.Collections.Generic;
	using System.Runtime.InteropServices;
	using System.Text;
	using System.Windows.Forms;

	using DMT.Library.PInvoke;

	/// <summary>
	/// Provides the glue so that an event can be fired when
	/// a particular key combination is pressed.
	/// <para />
	/// It is best to think of the HotKey as being associated 
	/// with the action to be performed rather than the key
	/// combination, although both can be changed during the
	/// life of the HotKey. 
	/// </summary>
	public class HotKey : IMessageFilter, IDisposable
	{
		KeyCombo _hotKeyCombo;
		Form _form;
		int _id;
		bool _isRegistered;

		/// <summary>
		/// Initialises a new instance of the <see cref="HotKey" /> class.
		/// The hot key will not actually be registered until RegisterHotKey() is called.
		/// </summary>
		/// <param name="form">Window to receive hot key as required by Win32 API</param>
		/// <param name="id">An ID for the hot key as required by the Win32 API</param>
		public HotKey(Form form, int id)
		{
			_form = form;
			_id = id;

			// we need to monitor the messages so we know when the hotkey is pressed
			Application.AddMessageFilter(this);
		}

		~HotKey()
		{
			Dispose(false);
		}

		/// <summary>
		/// Definition required for delegates that want to be notified 
		/// when the hot key has been pressed.
		/// </summary>
		public delegate void HotKeyHandler();

		/// <summary>
		/// Event that will be fired when the hot key has been pressed.
		/// </summary>
		public event HotKeyHandler HotKeyPressed;

		/// <summary>
		/// Gets the KeyCombo that we will be using as the hotkey.
		/// </summary>
		public KeyCombo HotKeyCombo
		{
			get { return _hotKeyCombo; }
		}

		/// <summary>
		/// Disposes of the hotkey
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Registers the hot key with Windows.
		/// If another key combo was registered, then this will be de-registered first.
		/// </summary>
		/// <param name="keyCombo">The new key combo to register with the hotkey</param>
		/// <returns>true if the hot key was accepted.  
		/// false indicates new keyCombo was not accepted, but previous state
		/// of the hot key should have been restored.</returns>
		public bool RegisterHotKey(KeyCombo keyCombo)
		{
			if (_form == null)
			{
				throw new ApplicationException("HotKey must be associated with a form before registering");
			}

			if (_form.Handle == IntPtr.Zero)
			{
				throw new ApplicationException("HotKey must be associated with a window before registering");
			}

			if (_isRegistered)
			{
				UnRegisterHotKey();
			}

			if (keyCombo.Enabled)
			{
				_isRegistered = NativeMethods.RegisterHotKey(_form.Handle, _id, keyCombo.Win32Modifier, keyCombo.Win32KeyCode);
				if (_isRegistered)
				{
					_hotKeyCombo = keyCombo;

					// new key combinaton as been succesfully registered as a hotkey
					return true;
				}
				else
				{
					// failed to register new key combo 
					// - probably because it's already registered as a hotkey
					if (_hotKeyCombo.Enabled)
					{
						// re-register old key combo to return to the state we were in when called
						// this should not fail 
						// but if it does, there is not much we can do about it
						_isRegistered = NativeMethods.RegisterHotKey(_form.Handle, _id, _hotKeyCombo.Win32Modifier, _hotKeyCombo.Win32KeyCode);
					}

					// failed to register new key combination as hot key
					return false;
				}
			}
			else
			{
				// as the key asked to be disabled
				// isRegistered will be false,
				// but we return true as we have done what we were asked to do
				_isRegistered = false;
				_hotKeyCombo = keyCombo;
				return true;
			}
		}

		/// <summary>
		/// Unregisters the hot key with windows.
		/// </summary>
		public void UnRegisterHotKey()
		{
			if (_isRegistered)
			{
				if (NativeMethods.UnregisterHotKey(_form.Handle, _id))
				{
					_isRegistered = false;
				}
			}
		}

		/// <summary>
		/// Our filter to check if we have been told that our hot key has been pressed.
		/// If so, the HotKeyPressed event is fired.
		/// </summary>
		/// <param name="m">The windows message</param>
		/// <returns>false, to allow the message to be dispatched.</returns>
		public bool PreFilterMessage(ref Message m)
		{
			if (m.Msg == NativeMethods.WM_HOTKEY)
			{
				if ((int)m.WParam == _id)
				{
					if (HotKeyPressed != null)
					{
						HotKeyPressed();
					}
				}
			}

			return false; // allow message to be dispatched
		}

		/// <summary>
		/// Cleans up 
		/// </summary>
		/// <param name="disposing">True if called from Dispose</param>
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				// no managed objects to dispose
			}

			// make sure hot key is not registered
			UnRegisterHotKey();

			// and remove us from the message filter
			Application.RemoveMessageFilter(this);
		}
	}
}
