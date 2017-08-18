using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

public class StudentItem : MonoBehaviour {
	//text because this is a UI text not string
	public Text NameText;
	//the text box that displays the coins
	public Text CoinsText;

	public int coinsInt;
	public string studentId;
	public string legacyMongodbId;

	public GameObject AvatarUI;

	//updates diaply name, coins and avatar based on the student object passed in
	public void UpdateStudentUI (Student student) {
		//updating UI
		this.NameText.text = student.display_name;
		this.CoinsText.text = student.coins.ToString();

		//updating data fields
		this.coinsInt = student.coins;
		this.studentId = student.id;
		this.legacyMongodbId = student.legacy_mongodb_id;

		UpdateStudentAvatar (student);
	}
		
	//helper function that updates the avatar
	void UpdateStudentAvatar (Student student) {
		Sprite found = null;
		Sprite [] avatars = Resources.LoadAll<Sprite>("Portraits");
		Sprite defaultAvatar = avatars.Single(a => a.name == "initial");

		//find the corresponding avatar
		foreach (var avatar in avatars) {
			if (avatar.name == student.avatar) {
				found = avatar;
				break;
			}
		}

		//if avatar is not found, then use default avatar
		if (found == null) {
			found = defaultAvatar;
		}

		AvatarUI.GetComponent<SpriteRenderer>().sprite = found;
	}
}
