using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class Authentication {
    public static void Login(string username, string password, System.Action onComplete, System.Action onError) {
        if (username == "admin" && password == "password") {
            onComplete();
        }
        else {
            onError();
        }
    }

}
