using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public class LoadSaveFactory : MonoBehaviour {

	//our Save and Load methods deal directly with strings rather than bytes
	static void Save (string name, string textToSave) {
		string path = System.IO.Path.Combine(Application.persistentDataPath, name);
		var bytes = System.Text.Encoding.UTF8.GetBytes(textToSave);

		System.IO.File.WriteAllBytes(path, bytes);
	}

	static string Load (string name) {
		string path = System.IO.Path.Combine (Application.persistentDataPath, name);
		var loadedBytes = System.IO.File.ReadAllBytes (path);
		return System.Text.Encoding.UTF8.GetString(loadedBytes);
	}

	//returns a dictionary containing the authentication token
	//if file doesn't exist, then create an inital copy
	public static string LoadToken () {
		string path = System.IO.Path.Combine(Application.persistentDataPath, Config.TOKEN_PATH);

		//if file doesn't exist, then return a blank string
		if (!System.IO.File.Exists(path)) {
			return ResetToken();
		}

		//otherwise return saved copy
		return Load(Config.TOKEN_PATH);
	}

	public static string ResetToken() {
		return SaveToken("");
	}

	public static string SaveToken(string token) {
		Save(Config.TOKEN_PATH, token);
		Debug.Log("Saved token");
		return token;
	}
}