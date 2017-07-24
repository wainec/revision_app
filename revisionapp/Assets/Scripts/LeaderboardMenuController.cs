using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardMenuController : MonoBehaviour {

    private DataHolder dataHolder;

    // Use this for initialization
    void Start () {
		dataHolder = FindObjectOfType<DataHolder> ();
    }
	
	public void goToIndexMenu() {
        dataHolder.GoToScene("IndexMenu");
	}
}
