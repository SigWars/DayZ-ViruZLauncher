//
//  LocalVersionService.cs
//
//  Author:
//       Jarl Gullberg <jarl.gullberg@gmail.com>
//
//  Copyright (c) 2017 Jarl Gullberg
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.IO;
using NLog;
using ViruZLauncher.Launcher.Utility;

namespace ViruZLauncher.Launcher.Services
{
	/// <summary>
	/// A service which handles local version discovery.
	/// </summary>
	public class LocalVersionService
	{
		/// <summary>
		/// Logger instance for this class.
		/// </summary>
		private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

		/// <summary>
		/// Gets the local game version.
		/// </summary>
		/// <returns>The local game version.</returns>
		public Version GetLocalGameVersion()
		{
			try
			{
				var rawGameVersion = File.ReadAllText(DirectoryHelpers.GetLocalGameVersionPath());

				if (Version.TryParse(rawGameVersion, out var gameVersion))
				{
					return gameVersion;
				}

				Log.Warn("Could not parse local game version. Contents: " + rawGameVersion);
				return new Version("0.0.0");
			}
			catch (IOException ioex)
			{
				Log.Warn("Could not read local game version (IOException): " + ioex.Message);
				return null;
			}
		}

		/// <summary>
		/// Gets the local launcher version.
		/// </summary>
		/// <returns>The version.</returns>
		public Version GetLocalLauncherVersion()
		{
			try
			{
				var rawLauncherPath = Path.Combine(DirectoryHelpers.GetLocalLauncherDirectory(), "LauncherVersion.txt");
				var locVersion = File.ReadAllText(rawLauncherPath);

				if (Version.TryParse(locVersion, out var launcherVersion))
				{
					return launcherVersion;
				}

				Log.Warn("Could not parse local launcher version. Contents: " + locVersion);
				return new Version("0.0.0");
			}
			catch (IOException ioex)
			{
				Log.Warn("Could not read local game version (IOException): " + ioex.Message);
				return null;
			}

			// return GetType().Assembly.GetName().Version; // Original unique line
			/*
			var localversion = Path.Combine(DirectoryHelpers.GetLocalGameDirectory(), "LauncherVersion.txt");
			if (!File.Exists(localversion))
			{
				throw new FileNotFoundException($"Launcher version at path (\"{localversion}\") not found.");
			}

			var remoteVersion = ReadRemoteFile(localversion).RemoveLineSeparatorsAndNulls();

			if (Version.TryParse(remoteVersion, out var version))
			{
				Log.Warn("Local version is " + version);
				return version;
			}

			Log.Warn("Failed to parse the local game version. Using the default of 0.0.0 instead.");
			// return new Version("0.0.0");
			*/
		}
	}
}
