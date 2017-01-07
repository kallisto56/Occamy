namespace Occamy.Services {

	using System;
	using System.IO;
	using System.Diagnostics;
	using System.ComponentModel;
	using System.Collections.Generic;
	using System.Runtime.CompilerServices;



	/// <summary>
	/// Provides a set of methods and properties that help debug your code.
	/// </summary>
	public sealed class Debugger : BackgroundWorker {

		private readonly string _location;

		private FileStream _fileStream;
		private BinaryWriter _binaryWriter;

		private Queue<string> _queue;

		private ulong _lineIndex;



		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="rootDirectory">Directory, where log-files will be stored.</param>
		public Debugger(string rootDirectory) {

			// Creating directory, if not exists
			var logsDirectory = $"{rootDirectory}/logs";
			if (!Directory.Exists(logsDirectory)) Directory.CreateDirectory(logsDirectory);

			// Calculating filename with full path
			_location = $"{logsDirectory}/{DateTime.Now.Ticks}.log";

			// ...
			_queue = new Queue<string>();
		}



		/// <summary>
		/// ...
		/// </summary>
		/// <param name="line"></param>
		/// <param name="methodName"></param>
		public void Trace(string line = null, [CallerMemberName] string methodName = null) {

			// ...
			line = (line == null) ? "" : $"'{line}'";
			methodName = methodName == ".ctor" ? "ctor" : methodName;

			// ...
			var method = new StackTrace().GetFrame(1).GetMethod();
			var architecturePath = method.ReflectedType?.FullName;
			var isStatic = method.IsStatic ? ":STATIC" : "";
			var message = $"{++_lineIndex,4}: {architecturePath}->{methodName}({line}){isStatic}";

			// During development, writing each line into console
			Debug.WriteLine(message);

			// Adding message into queue and starting background-worker, if not busy
			_queue.Enqueue(message);
			if (!IsBusy) RunWorkerAsync();

		}



		/// <summary>
		/// ...
		/// </summary>
		/// <param name="line"></param>
		public void WriteLine(string line) {

			// During development, writing each line into console
			Debug.WriteLine($"{++_lineIndex,4}: \t{line}");

			// Adding message into queue and starting background-worker, if not busy
			_queue.Enqueue(line);
			if (!IsBusy) RunWorkerAsync();

		}



		/// <summary>
		/// ...
		/// </summary>
		/// <param name="e"></param>
		protected override void OnDoWork(DoWorkEventArgs e) {

			// Creating instance of FileStream and BinaryWriter inside thread,
			// since writing performs only inside this method. We could open
			// and close FileStream during work, but it's probably will be called,
			// very often, so it will cause 'slowdown' of hard drive.
			if (_fileStream == null) {
				_fileStream = new FileStream(_location, FileMode.OpenOrCreate);
				_binaryWriter = new BinaryWriter(_fileStream);
			}

			// Writing each line
			while (_queue.Count != 0) {
				_binaryWriter.Write(_queue.Dequeue());
			}

			// ...
			_binaryWriter.Flush();

		}


		/// <summary>
		/// ...
		/// </summary>
		/// <param name="disposing"></param>
		protected override void Dispose(bool disposing) {

			// BackgroundWorker first
			base.Dispose(disposing);

			// ...
			if (_fileStream == null) return;

			// FileStream last
			_fileStream.Close();
			_fileStream.Dispose();

		}



	}



}
