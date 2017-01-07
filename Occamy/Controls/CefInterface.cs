namespace Occamy.Controls {

	using System.Collections.Generic;
	using Newtonsoft.Json;



	/// <summary>
	/// Fundamental class for all interfaces between Javascript and C#
	/// </summary>
	public abstract class CefInterface {



		/// <summary>
		/// Returns array of arguments, if specified method is exists in current class
		/// </summary>
		/// <param name="methodName">Name of the method</param>
		/// <returns>Return array of arguments and their types, if method exists in current class, otherwise, false will be returned. Another option, when method is not exists, null will be returned.</returns>
		public string Help(string methodName) {

			var methods = this.GetType().GetMethods();

			foreach (var method in methods) {

				if (method.Name == methodName) {
					var args = method.GetParameters();
					var arguments = new Dictionary<string, string>();

					foreach (var parameter in method.GetParameters()) {
						arguments.Add(parameter.Name, parameter.ParameterType.ToString());
					}

					if (arguments.Count == 0) {
						return "false";
					}

					var jsonContent = JsonConvert.SerializeObject(arguments, Formatting.Indented);
					return jsonContent;
				}
			}

			return "null";
		}



	}



}
