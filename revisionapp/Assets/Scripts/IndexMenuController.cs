using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndexMenuController : MonoBehaviour {

    private DataHolder dataHolder;

    // Use this for initialization
    void Start () {
        dataHolder = FindObjectOfType<DataHolder>();
    }
	
	public void goToQuizMenu() {
        dataHolder.GoToScene("QuizMenu");
	}

	public void goToLeaderboardMenu() {
        dataHolder.GoToScene("LeaderboardMenu");
	}

}