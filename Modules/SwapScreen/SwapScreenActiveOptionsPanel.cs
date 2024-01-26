﻿#region copyright
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

namespace DMT.Modules.SwapScreen
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Data;
	using System.Drawing;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.Windows.Forms;

	using DMT.Library.HotKeys;

	/// <summary>
	/// Options panel for swap screen active window
	/// </summary>
	partial class SwapScreenActiveOptionsPanel : UserControl
	{
		SwapScreenModule _swapScreenModule;

		/// <summary>
		/// Initialises a new instance of the <see cref="SwapScreenActiveOptionsPanel" /> class.
		/// </summary>
		/// <param name="swapScreenModule">Swap screen module</param>
		public SwapScreenActiveOptionsPanel(SwapScreenModule swapScreenModule)
		{
			_swapScreenModule = swapScreenModule;

			InitializeComponent();

			SetupHotKeys();
		}

		void SetupHotKeys()
		{
			//SetupHotKey(hotKeyPanelNextScreen, _swapScreenModule.NextScreenHotKeyController);
			//SetupHotKey(hotKeyPanelPrevScreen, _swapScreenModule.PrevScreenHotKeyController);
			//SetupHotKey(hotKeyPanelMinimise, _swapScreenModule.MinimiseHotKeyController);
			//SetupHotKey(hotKeyPanelMaximise, _swapScreenModule.MaximiseHotKeyController);
			//SetupHotKey(hotKeyPanelSupersize, _swapScreenModule.SupersizeHotKeyController);
			//SetupHotKey(hotKeyPanelSwapTop2, _swapScreenModule.SwapTop2HotKeyController);
			//SetupHotKey(hotKeyPanelSnapLeft, _swapScreenModule.SnapLeftHotKeyController);
			//SetupHotKey(hotKeyPanelSnapRight, _swapScreenModule.SnapRightHotKeyController);
			//SetupHotKey(hotKeyPanelSnapUp, _swapScreenModule.SnapUpHotKeyController);
			//SetupHotKey(hotKeyPanelSnapDown, _swapScreenModule.SnapDownHotKeyController);

			List<HotKeyController> hotKeyControllers = new List<HotKeyController>();

			hotKeyControllers.Add(_swapScreenModule.NextScreenHotKeyController);
			hotKeyControllers.Add(_swapScreenModule.PrevScreenHotKeyController);
			hotKeyControllers.Add(_swapScreenModule.MinimiseHotKeyController);
			hotKeyControllers.Add(_swapScreenModule.MaximiseHotKeyController);
			hotKeyControllers.Add(_swapScreenModule.SupersizeHotKeyController);
			hotKeyControllers.Add(_swapScreenModule.SwapTop2HotKeyController);
			hotKeyControllers.Add(_swapScreenModule.SnapLeftHotKeyController);
			hotKeyControllers.Add(_swapScreenModule.SnapRightHotKeyController);
			hotKeyControllers.Add(_swapScreenModule.SnapUpHotKeyController);
			hotKeyControllers.Add(_swapScreenModule.SnapDownHotKeyController);

			scrollableHotKeysPanel.Init(hotKeyControllers);

		}

		//void SetupHotKey(HotKeyPanel hotKeyPanel, HotKeyController hotKeyController)
		//{
		//	hotKeyPanel.SetHotKeyController(hotKeyController);
		//}
	}
}
