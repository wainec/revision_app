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

	private DBFactory db;
    public Student student;

	public Quiz quiz;

    // Use this for initialization
    void Start() {
        if (!isDataHolderExist) {
            isDataHolderExist = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else {
            Destroy(transform.gameObject);
        }

		db = transform.gameObject.AddComponent<DBFactory>();
		SetAllowedScenes();

		//kiv code -> hardcode data
		quiz = new Quiz();
		quiz.chinese_title = "测试";
		quiz.term_id = "p1";
		quiz.term_index = 0;
		quiz.index = 2;	

		quiz.questions = new List<Question> ();
		var q = new Question();
		q.question_text = "家人在我身边,我什么都不怕。";
		q.choices = new List<string> () {
			"ziā rén", "jiā rén", "jiā lén"
		};
		q.correct_answer = "jiā rén";
		quiz.questions.Add(q);
    }

//    public IEnumerator CheckInternetConnection(System.Action onComplete) {
//        return CheckInternetConnection(onComplete, null);
//    }

	//DOMAIN IS WRONG -> KIV
    //high level function that takes in two actions 
    // onComplete action will be run if there is internet, otherwise run onError
//    public IEnumerator CheckInternetConnection(System.Action onComplete, System.Action onError) {
//        Debug.Log("Checking for internet");
//        string url = Config.domain + "/admin/api/worldofboshicraft/checkinternet";
//        WWW www = new WWW(url);
//        yield return www;
//
//        //if there is internet connection, then run whatever action (function) is passed to CheckInternetConnection IEnumerator
//        if (www.error != null) {
//            Debug.LogError("No internet connection detected");
//            if (onError != null) {
//                onError();
//            }
//            
//        }
//        else {
//            Debug.Log("Internet connection detected");
//            onComplete();
//        }
//    }

    
    //gets a token from database
	public void Login(string username, string password, System.Action <string> onComplete, System.Action onError) {
		string url = Config.domain + "/rest-auth/login/";
		Dictionary <string, string> postData = new Dictionary<string, string> () {
			{"username", username},
			{"password", password}
		};

        // note that we pass the results of the post to onComplete
		db.POST(url, postData, onComplete, onError);
	}

    public void GetStudentInfo(string token, System.Action<string> onComplete, System.Action onError) {
		Debug.Log("Getting student info");
		Debug.Log(token);
		string url = Config.domain + "/api/v1/revision/students/current/";
        // note that we pass the results of the post to onComplete
		db.GET(url, token, onComplete, onError);
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
