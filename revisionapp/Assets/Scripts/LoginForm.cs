using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class LoginForm : MonoBehaviour {
    private const float LABEL_WIDTH = 110;

    public Canvas canvas;
    public Text TitleText;
    private DataHolder dataHolder;

    private bool GUIEnabled = true;
    private bool loggingIn = false;
    private bool hasFocussed = false;
    private Rect windowRect = new Rect(Screen.width / 2 - 150, Screen.height / 2 - 75, 300, 150);
    private string username = "", password = "";

    //turns off canvas at the start
    void Start() {
		string token;

        dataHolder = FindObjectOfType<DataHolder> ();

		if (!dataHolder.loggedIn) {
			Debug.Log("checking for token");
			token = LoadSaveFactory.LoadToken();

			//if there is a token saved, then we check using the token
			if (token != "") {
				Debug.Log("Token is " + token);
                dataHolder.GetStudentInfo(token, UpdateUIWithStudentInfo, UnsuccessfulLogin);
            }
		}

        ToggleGUIAndCanvas(dataHolder.loggedIn);        
    }

    public void DoLogout() {
        LoadSaveFactory.ResetToken();
        dataHolder.loggedIn = false;
        ToggleGUIAndCanvas(dataHolder.loggedIn);
    }

    private void ToggleGUIAndCanvas (bool loggedIn) {
        //if logged in, then we show canvas, otherwise we show Login GUI
        GUIEnabled = !loggedIn;
        canvas.enabled = loggedIn;
    }

    private void OnGUI() {
        if (GUIEnabled) {
            windowRect = GUILayout.Window(0, windowRect, ShowWindow, "Login menu");
        }
    }

    private void ShowWindow(int windowID) {
        GUILayout.BeginVertical();
        GUILayout.Label("Please enter your username and password");


        GUILayout.BeginHorizontal();
        GUI.SetNextControlName("usernameField");
        GUILayout.Label("Username", GUILayout.Width(LABEL_WIDTH));
        username = GUILayout.TextField(username, 30);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Password", GUILayout.Width(LABEL_WIDTH));
        password = GUILayout.PasswordField(password, '*', 30);
        GUILayout.EndHorizontal();

        GUILayout.FlexibleSpace();

        //checks to see if both username and passwords are filled in
        bool filledIn = (username != "" && password != "");
        GUI.enabled = filledIn;

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Login")) {
            DoLogin();
        }

        Event e = Event.current;
        if (filledIn && e.isKey && e.keyCode == KeyCode.Return) {
            DoLogin();
        }

        GUILayout.EndHorizontal();        
        GUILayout.EndVertical();

        //set focus to username Field
        if (!hasFocussed) {
            GUI.FocusControl("usernameField");
            hasFocussed = true;
        }
    }

    private void DoLogin() {
        Debug.Log("Logging in");
		dataHolder.Login(username, password, SuccessfulLogin, UnsuccessfulLogin);
    }

    //after successful login, get student info
    //kiv - gui only turned off after we have gotten student info
	private void SuccessfulLogin(string data) {
        //clear fields
        username = "";
        password = "";

        string token = JsonConvert.DeserializeObject<AuthenticationKey>(data).key;
		LoadSaveFactory.SaveToken(token);

        dataHolder.GetStudentInfo(token, UpdateUIWithStudentInfo, UnsuccessfulLogin);
    }

    //once we have successfully gotten student information, then we remove GUI
    private void UpdateUIWithStudentInfo(string data) {
        dataHolder.student = JsonConvert.DeserializeObject<Student> (data);
        dataHolder.loggedIn = true;

        TitleText.text = "Welcome " + dataHolder.student.display_name + "! You have " + dataHolder.student.coins + " coins!";

        ToggleGUIAndCanvas(dataHolder.loggedIn);
    }

    private void UnsuccessfulLogin() {
        Debug.LogError("Error logging in");
        //clear password but not username
        password = "";

        //clears any existing token (since it is wrong)
        LoadSaveFactory.ResetToken();

        //updated the loggedIn field and then turn canvas on and GUI off
        dataHolder.loggedIn = false;
        ToggleGUIAndCanvas(dataHolder.loggedIn);
    }

}

