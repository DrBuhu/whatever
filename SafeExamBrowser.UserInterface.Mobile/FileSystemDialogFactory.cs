﻿/*
 * Copyright (c) 2025 ETH Zürich, IT Services
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System.Windows;
using SafeExamBrowser.I18n.Contracts;
using SafeExamBrowser.SystemComponents.Contracts;
using SafeExamBrowser.UserInterface.Contracts.FileSystemDialog;
using SafeExamBrowser.UserInterface.Contracts.Windows;
using SafeExamBrowser.UserInterface.Mobile.Windows;

namespace SafeExamBrowser.UserInterface.Mobile
{
	public class FileSystemDialogFactory : IFileSystemDialog
	{
		private readonly ISystemInfo systemInfo;
		private readonly IText text;

		public FileSystemDialogFactory(ISystemInfo systemInfo, IText text)
		{
			this.systemInfo = systemInfo;
			this.text = text;
		}

		public FileSystemDialogResult Show(
			FileSystemElement element,
			FileSystemOperation operation,
			string initialPath = default,
			string message = default,
			string title = default,
			IWindow parent = default,
			bool restrictNavigation = false,
			bool showElementPath = true)
		{
			if (parent is Window window)
			{
				return window.Dispatcher.Invoke(() => new FileSystemDialog(element, operation, systemInfo, text, initialPath, message, title, parent, restrictNavigation, showElementPath).Show());
			}
			else
			{
				return new FileSystemDialog(element, operation, systemInfo, text, initialPath, message, title, restrictNavigation: restrictNavigation, showElementPath: showElementPath).Show();
			}
		}
	}
}
