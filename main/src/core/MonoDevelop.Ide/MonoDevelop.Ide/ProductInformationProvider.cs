//
// ProductInformationProvider.cs
//
// Author:
//       jason <jaimison@microsoft.com>
//
// Copyright (c) 2018 
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System.IO;
using MonoDevelop.Core;

namespace MonoDevelop.Ide
{
	public abstract class ProductInformationProvider : ISystemInformationProvider
	{
		/// <summary>
		/// Application ID used by the updater. Usually a GUID.
		/// </summary>
		public abstract string ApplicationId { get; }

		public abstract string Title { get; }

		public virtual string Description => GettextCatalog.GetString("Version: {0}", Version);

		/// <summary>
		/// Human readable version number
		/// </summary>
		public abstract string Version { get; }

		/// <summary>
		/// Version number used by the updater for comparison
		/// </summary>
		public string VersionID => null;

		/// <summary>
		/// Path to the updateinfo file.
		/// </summary>
		protected abstract string UpdateInfoFile { get; }

		public virtual UpdateInfo GetUpdateInfo ()
		{
			if (UpdateInfoFile != null && File.Exists (UpdateInfoFile))
				return UpdateInfo.FromFile (UpdateInfoFile);
			return null;
		}
	}
}
