using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardMenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	public void goToIndexMenu() {
		SceneManager.LoadScene("IndexMenu");
	}
}
