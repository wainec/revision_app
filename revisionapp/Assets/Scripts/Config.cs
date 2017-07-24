using UnityEngine;
using System.Collections.Generic;

static class Config {

	//use second line and comment out first line if we want to force app to make calls to live server
	//public static string overWriteDomain;
	public static string overWriteDomain = "http://www.boshipanda.com";

	//sets root to localhost if development
	public static string domain;
	public static bool isInitialised;

	public static void SetValues () {
		//will only run if config has not been initialised yet
		if (isInitialised) {
			return;
		}

        isInitialised = true;
		domain = Debug.isDebugBuild ? "localhost:9000" : "http://www.boshipanda.com";

		if (overWriteDomain != null) {
			domain = overWriteDomain;
		}
	}
}