﻿using System;

    public Canvas canvas;
    private DataHolder dataHolder;

    private bool GUIEnabled = true;
    private bool loggingIn = false;
    private bool hasFocussed = false;
    private Rect windowRect = new Rect(Screen.width / 2 - 150, Screen.height / 2 - 75, 300, 150);
    private string username = "", password = "";

    //turns off canvas at the start
    void Start() {
        ToggleGUIAndCanvas(dataHolder.loggedIn);        
    }
        dataHolder.loggedIn = false;
        ToggleGUIAndCanvas(dataHolder.loggedIn);
    }
        //if logged in, then we show canvas, otherwise we show Login GUI
        GUIEnabled = !loggedIn;
        canvas.enabled = loggedIn;
    }
            windowRect = GUILayout.Window(0, windowRect, ShowWindow, "Login menu");
        }
    }


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
        if (filledIn && e.isKey && e.keyCode == KeyCode.Return)
        {
            DoLogin();
        }

        GUILayout.EndHorizontal();        
        GUILayout.EndVertical();
            GUI.FocusControl("usernameField");
            hasFocussed = true;
        }
        Debug.Log("Logging in");
        //clear fields
        username = "";
        password = "";

        //updated the loggedIn field and then turn canvas on and GUI off
        dataHolder.loggedIn = true;
        ToggleGUIAndCanvas(dataHolder.loggedIn);
    }
        Debug.LogError("Error logging in");
        //clear password but not username
        password = "";
    }