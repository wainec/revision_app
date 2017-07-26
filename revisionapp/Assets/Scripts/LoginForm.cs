using System;using System.Collections;using System.Collections.Generic;using UnityEngine;public class LoginForm : MonoBehaviour {    private const float LABEL_WIDTH = 110;

    public Canvas canvas;
    private DataHolder dataHolder;

    private bool GUIEnabled = true;
    private bool loggingIn = false;
    private bool hasFocussed = false;
    private Rect windowRect = new Rect(Screen.width / 2 - 150, Screen.height / 2 - 75, 300, 150);
    private string username = "", password = "";

    //turns off canvas at the start
    void Start() {        dataHolder = FindObjectOfType<DataHolder> ();
        ToggleGUIAndCanvas(dataHolder.loggedIn);        
    }    private void ToggleGUIAndCanvas (bool loggedIn) {
        //if logged in, then we show canvas, otherwise we show Login GUI
        GUIEnabled = !loggedIn;
        canvas.enabled = loggedIn;
    }    private void OnGUI() {        if (GUIEnabled) {
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
        GUILayout.EndHorizontal();        
        GUILayout.EndVertical();        //set focus to username Field        if (!hasFocussed) {
            GUI.FocusControl("usernameField");
            hasFocussed = true;
        }    }    private void DoLogin() {
        Debug.Log("Logging in");        Authentication.Login(username, password, SuccessfulLogin, UnsuccessfulLogin);    }    private void SuccessfulLogin() {
        //updated the loggedIn field and then turn canvas on and GUI off
        dataHolder.loggedIn = true;
        ToggleGUIAndCanvas(dataHolder.loggedIn);
    }    private void UnsuccessfulLogin() {
        Debug.LogError("Error logging in");
    }}