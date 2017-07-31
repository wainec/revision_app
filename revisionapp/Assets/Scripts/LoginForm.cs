using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class LoginForm : MonoBehaviour {
    private const float LABEL_WIDTH = 110;

    public Canvas canvas;
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
				//dataHolder.Login(token);
			}
		}


        ToggleGUIAndCanvas(dataHolder.loggedIn);        
    }

    public void DoLogout() {
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
        bool filledIn = (username != "" && password != "");

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

	private void SuccessfulLogin(string data) {
        //clear fields
        username = "";
        password = "";

		string token = JsonConvert.DeserializeObject<AuthenticationKey>(data).key;
		LoadSaveFactory.SaveToken(token);

        //updated the loggedIn field and then turn canvas on and GUI off
        dataHolder.loggedIn = true;
        ToggleGUIAndCanvas(dataHolder.loggedIn);

    }

    private void UnsuccessfulLogin() {
        Debug.LogError("Error logging in");
        //clear password but not username
        password = "";
    }

}

