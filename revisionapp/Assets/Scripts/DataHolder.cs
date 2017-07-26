using UnityEngine;

using System;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DataHolder : MonoBehaviour {
    public string previousScene;

	public static bool isDataHolderExist;

    //public string errorMessage;
    //private DBFactory db;

    public bool loggedIn = false;

	//key is the originating scene, value stores scenes that user can go to
	public Dictionary <string, List<string>> allowedScenes;
    string errorMessage;

    // Use this for initialization
    void Start() {
        if (!isDataHolderExist) {
            isDataHolderExist = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else {
            Destroy(transform.gameObject);
        }

		//db = transform.gameObject.AddComponent<DBFactory>();
		SetAllowedScenes();
    }

    public IEnumerator CheckInternetConnection(System.Action onComplete) {
        return CheckInternetConnection(onComplete, null);
    }

    //high level function that takes in two actions 
    // onComplete action will be run if there is internet, otherwise run onError
    public IEnumerator CheckInternetConnection(System.Action onComplete, System.Action onError) {
        Debug.Log("Checking for internet");
        string url = Config.domain + "/admin/api/worldofboshicraft/checkinternet";
        WWW www = new WWW(url);
        yield return www;

        //if there is internet connection, then run whatever action (function) is passed to CheckInternetConnection IEnumerator
        if (www.error != null) {
            Debug.LogError("No internet connection detected");
            if (onError != null) {
                onError();
            }
            
        }
        else {
            Debug.Log("Internet connection detected");
            onComplete();
        }
    }


    //used to set what scenes we can access from each scene
    void SetAllowedScenes() {
		allowedScenes = new Dictionary<string, List<string>> ();

		allowedScenes.Add("IndexMenu", new List<string>() {"QuizMenu", "LeaderboardMenu"});
        allowedScenes.Add("QuizMenu", new List< string> () { "IndexMenu"});
		allowedScenes.Add("LeaderboardMenu", new List< string> () { "IndexMenu"});
	}

	//use this function to handle all changing scenens
	//validates whether we can go to goToScene based on current scene
	public bool GoToScene(string goToScene) {

		string currentScene = SceneManager.GetActiveScene().name;

		//if the goToScene is not allowed, return false
		if (!allowedScenes.ContainsKey(currentScene) || !allowedScenes[currentScene].Contains(goToScene)) {
			errorMessage = "Not allowed to go to " +  goToScene + " from " + currentScene;
			return false;
		}

		this.previousScene = currentScene;
		SceneManager.LoadScene(goToScene);
		return true;
	}

	public void GoToPreviousScene() {
		GoToScene(this.previousScene);
	}
}
