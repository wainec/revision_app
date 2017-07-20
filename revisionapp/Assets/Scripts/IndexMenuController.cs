using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndexMenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	public void goToQuizMenu() {
		SceneManager.LoadScene("QuizMenu");
	}

	public void goToLeaderboardMenu() {
		SceneManager.LoadScene("LeaderboardMenu");
	}

	public void goToLoginMenu() {
		SceneManager.LoadScene("LoginMenu");
	}

}