using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

//https://www.packtpub.com/books/content/using-rest-api-unity-part-1-what-rest-and-basic-queries

[System.Serializable]
public class Student {
    public string displayName;
    public string _id;
    public int coins;
    public bool attending;
    public string avatar;
 }

public class DBFactory : MonoBehaviour {

    private string results;

    public string Results {
        get { return results; }
    }

    //seems to be a bug when pulling some chinese words
    public WWW GET(string url, System.Action onComplete) {
        WWW www = new WWW(url);
        StartCoroutine(WaitForRequest(www, onComplete, null));
        return www;
    }

    public WWW POST(string url, Dictionary<string, string> post) {
        return POST(url, post, null, null);
    }

    public WWW POST(string url, Dictionary<string, string> post, System.Action onComplete) {
        return POST(url, post, onComplete, null);
    }

    public WWW POST(string url, Dictionary<string, string> post, System.Action onComplete, System.Action onError) {
        WWWForm form = new WWWForm();

        foreach (KeyValuePair<string, string> post_arg in post) {
            form.AddField(post_arg.Key, post_arg.Value);
        }

        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest(www, onComplete, onError));
        return www;
    }

    //KIV -> if onError is not passed in and there is an error, nothing will happen
    private IEnumerator WaitForRequest(WWW www, System.Action onComplete, System.Action onError) {
        yield return www;

        if (www.error == null) {
            results = www.text;

            //some GET/POST requests do not have an onComplete function
            if (onComplete != null) {
                onComplete();
            }
        }
        else if (onError != null) {
            onError();
        }
    }
}
