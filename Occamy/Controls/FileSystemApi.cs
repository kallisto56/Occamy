namespace Occamy.Controls {

	using System;
	using System.IO;
	using System.Runtime.InteropServices;
	using System.Collections.Generic;

	using Native;
	using Newtonsoft.Json;



	/// <summary>
	/// Application protocol interface between <see cref="System.IO"/> and <see cref="Chromium"/>.
	/// </summary>
	public sealed class FileSystemApi : CefInterface {

		private Controls.Form _form;



		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="form">Form, to which actions are will be applied</param>
		public FileSystemApi(Controls.Form form) {
			this._form = form;
		}



		/// <summary>
		/// Returns list of directories and files, that stored in specified directory
		/// </summary>
		/// <param name="path">Path</param>
		/// <returns>Return null, if specified path does not exists, otherwise array of file system entries in JSON-format.</returns>
		public string GetFileSystemEntries(string path) {
			if (!Directory.Exists(path)) return null;
			var items = Directory.GetFileSystemEntries(path);
			return JsonConvert.SerializeObject(items);
		}



		/// <summary>
		/// Return content of specified file.
		/// </summary>
		/// <param name="filename">Path to a file, which must be loaded</param>
		/// <returns>Return content of specified file or null if specified file doesn't exists.</returns>
		public string ReadAllText(string filename) {
			return File.Exists(filename)
				? File.ReadAllText(filename)
				: null;
		}



		/// <summary>
		/// Writes content into specified file.
		/// </summary>
		/// <param name="filename">Path to a file, which will contain specified content</param>
		/// <param name="content">Content, which will be placed in to specified file</param>
		public void WriteAllText(string filename, string content) {
			File.WriteAllText(filename, content);
		}



		/// <summary>
		/// Return state of file.
		/// </summary>
		/// <param name="filename">Path to a file, which state must be returned</param>
		/// <returns>Returns true, if file exists, otherwise false</returns>
		public bool FileExists(string filename) {
			return File.Exists(filename);
		}



		/// <summary>
		/// Returns state of directory.
		/// </summary>
		/// <param name="path">Path to a directory, which state must be returned</param>
		/// <returns>Return true, if file exists, otherwise false</returns>
		public bool DirectoryExists(string path) {
			return Directory.Exists(path);
		}



		/// <summary>
		/// Creates directory at specified path
		/// </summary>
		/// <param name="path">Path</param>
		public void CreateDirectory(string path) {
			Directory.CreateDirectory(path);
		}



		/// <summary>
		/// Opens a Windows Explorer window with specified items in a particular folder selected.
		/// </summary>
		/// <param name="json">JSON-format string, containing array of files</param>
		/// <returns>Returns true, if operation is successful, otherwise false.</returns>
		public bool BrowseFiles(string json) {

			// Parsing JSON-content into a list of strings
			var files = JsonConvert.DeserializeObject<List<string>>(json);
			if (files.Count == 0) return false;

			// Retrieving root directory for selected files
			var directory = Path.GetDirectoryName(files[0]);
			if (!Directory.Exists(directory)) return false;

			// Declaring variables
			var failed = false;
			uint psfgaoOut;
			IntPtr hDirectory;
			IntPtr[] hFiles = new IntPtr[files.Count];

			// Retrieving native handle on directory
			NativeMethods.SHParseDisplayName(directory, IntPtr.Zero, out hDirectory, 0, out psfgaoOut);

			// Retrieving native handles for each file
			for (var n = 0; n < files.Count; n++) {
				if (!File.Exists(files[n]) && !Directory.Exists(files[n])) {
					failed = false;
					break;
				}
				uint psfgaoOut0;
				NativeMethods.SHParseDisplayName(Path.GetFullPath(files[n]), IntPtr.Zero, out hFiles[n], 0, out psfgaoOut0);
				if (hFiles[n] == IntPtr.Zero) {
					failed = false;
					break;
				}
			}

			// Executing desired method
			if (!failed) NativeMethods.SHOpenFolderAndSelectItems(hDirectory, (uint)hFiles.Length, hFiles, 0);

			// Releasing handle on each file
			for (var n = 0; n < files.Count; n++) {
				if (hFiles[n] != IntPtr.Zero) Marshal.FreeCoTaskMem(hFiles[n]);
			}

			// Releasing handle on directory
			Marshal.FreeCoTaskMem(hDirectory);

			// ...
			return failed;

		}



	}



}
