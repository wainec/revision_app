using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizMenuController : MonoBehaviour {

    private DataHolder dataHolder;
	public GameObject StudentAvatar;
    public List<Button> choiceButtons;

    private Color ButtonSelectedColor = Color.green;
    private Color ButtonNotSelectedColor = Color.grey;

    //state variable that determines if the choice button has been selected (and should be reset each time a question is answered)
    private bool isChoiceButtonSelected;
    private Button choiceButtonSelected;

    // Use this for initialization
    void Start () {
        dataHolder = FindObjectOfType<DataHolder> ();
		StudentAvatar.GetComponent<StudentItem> ().UpdateStudentUI(dataHolder.student);
        ResetButtonColors();
    }
	
	public void GoToIndexMenu() {
        dataHolder.GoToScene("IndexMenu");
	}

    //called when user clicks a choice button
    public void ClickChoiceButton(Button button) {
        //changes the appearance of the button
        ToggleButtons(choiceButtons, button);
    }

    private void ResetButtonColors() {
        ToggleButtons(choiceButtons, null);
        isChoiceButtonSelected = false;
        choiceButtonSelected = null;
    }

    //function that goes through a list of buttons and changes the button colors
    //if buttonClicked is null, then all buttons are set to the ButtonNotSelectedColor
    private void ToggleButtons (List<Button> buttons, Button buttonClicked) {
        for (var i = 0; i < buttons.Count; i++) {
            //note that we do not want isChoiceSelected to be reset to false if the next button in the button group is false (while this button is true)
            //while we normally would do a break to exit the loop, we allow the for loop to continue to run because we want to set the colors of the remaining buttons
            //to ButtonNotSelectedColor
            if (buttons[i] == buttonClicked) {
                buttons[i].image.color = ButtonSelectedColor;
                isChoiceButtonSelected = true;
                choiceButtonSelected = buttonClicked;
            }
            else {
                buttons[i].image.color = ButtonNotSelectedColor;
            }

        }
    }

    public void ClickConfirmButton() {
        if (isChoiceButtonSelected) {;
            string choiceString = choiceButtonSelected.transform.Find("Text").GetComponent<Text>().text;
        }
        else {
            Debug.LogError("Error - no choice was selected");
        }

    }

}
