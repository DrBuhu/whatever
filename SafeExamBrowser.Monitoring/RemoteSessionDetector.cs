﻿/*
 * Copyright (c) 2025 ETH Zürich, IT Services
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System.Windows.Forms;
using SafeExamBrowser.Logging.Contracts;
using SafeExamBrowser.Monitoring.Contracts;

namespace SafeExamBrowser.Monitoring
{
	public class RemoteSessionDetector : IRemoteSessionDetector
	{
		private readonly ILogger logger;

		public RemoteSessionDetector(ILogger logger)
		{
			this.logger = logger;
		}

		public bool IsRemoteSession()
		{
			//var isRemoteSession = SystemInformation.TerminalServerSession;

			//logger.Debug($"System appears {(isRemoteSession ? "" : "not ")}to be running in a remote session.");

			//return isRemoteSession;
			return false;
		}
	}
}
