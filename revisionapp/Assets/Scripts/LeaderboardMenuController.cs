using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardMenuController : MonoBehaviour {

    private DataHolder dataHolder;
	public GameObject StudentCurrentPreFab;
	public GameObject StudentOthersPreFab;

    // Use this for initialization
    void Start () {
		dataHolder = FindObjectOfType<DataHolder> ();

		// KIV CODE TO TEST
		List<LeaderboardStudent> leaderboardStudents = new List<LeaderboardStudent> ();
		LeaderboardStudent temp = new LeaderboardStudent ();
		temp.display_name = "Tom";
		temp.coins = 12;
		temp.current_student = true;
		temp.leaderboard_position = "1";
		temp.avatar = "jiejie";
		leaderboardStudents.Add(temp);

		temp = new LeaderboardStudent ();
		temp.display_name = "Tom";
		temp.coins = 12;
		temp.current_student = false;
		temp.leaderboard_position = "2";
		temp.avatar = "jiejie";
		leaderboardStudents.Add(temp);

		temp = new LeaderboardStudent ();
		temp.display_name = "Tom";
		temp.coins = 12;
		temp.current_student = false;
		temp.leaderboard_position = "3";
		temp.avatar = "jiejie";
		leaderboardStudents.Add(temp);

		temp = new LeaderboardStudent ();
		temp.display_name = "Tom";
		temp.coins = 12;
		temp.current_student = false;
		temp.leaderboard_position = "4";
		temp.avatar = "jiejie";
		leaderboardStudents.Add(temp);

		temp = new LeaderboardStudent ();
		temp.display_name = "Tom";
		temp.coins = 12;
		temp.current_student = false;
		temp.leaderboard_position = "5";
		temp.avatar = "jiejie";
		leaderboardStudents.Add(temp);

		DrawLeaderboardStudents(leaderboardStudents);

    }
	
	public void goToIndexMenu() {
        dataHolder.GoToScene("IndexMenu");
	}

	//creates instances of student prefabs using a passed in list of students
	public void DrawLeaderboardStudents (List<LeaderboardStudent> leaderboardStudents) {
		int index = 0;
		float yOffset = 0.0f;

		float StudentOthersPrefabHeight = 1.44f;
		float StudentCurrentPrefabHeight = 2.1f;

		foreach(var leaderboardStudent in leaderboardStudents) {
			GameObject Prefab = leaderboardStudent.current_student ? StudentCurrentPreFab : StudentOthersPreFab;

			Vector3 currPos = new Vector3(transform.position.x,
				transform.position.y - yOffset,
				transform.position.z);

			GameObject leaderboardStudentObject = (GameObject) Instantiate(Prefab, currPos, transform.rotation);

			leaderboardStudentObject.GetComponent<StudentItem>().UpdateLeaderboardStudentUI(leaderboardStudent);
			leaderboardStudentObject.transform.SetParent(transform);

			// offsets the yPos of the next object according
			yOffset += leaderboardStudent.current_student ? StudentCurrentPrefabHeight : StudentOthersPrefabHeight;
			index++;
		}
	}

}
