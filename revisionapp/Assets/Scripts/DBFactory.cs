using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//https://www.packtpub.com/books/content/using-rest-api-unity-part-1-what-rest-and-basic-queries

[System.Serializable]
public class Student {
    public string display_name;
	//this is the student id in smartschool (current) database
	public string id;
	//this is student id in legacy boshipanda database
	public string legacy_mongodb_id;
    public int coins;
    public string avatar;
 }

[System.Serializable]
public class AuthenticationKey {
	public string key;
}

//note that the way GET and POST is set up in revisionapp is slightly different from previous apps
//instead of using Results in previous apps, it is accessed as an argument to the onComplete function passed to it
public class DBFactory : MonoBehaviour {

    //seems to be a bug when pulling some chinese words
	public WWW GET(string url, System.Action <string> onComplete) {
        WWW www = new WWW(url);
        StartCoroutine(WaitForRequest(www, onComplete, null));
        return www;
    }

    public WWW POST(string url, Dictionary<string, string> post) {
        return POST(url, post, null, null);
    }

	public WWW POST(string url, Dictionary<string, string> post, System.Action <string> onComplete) {
        return POST(url, post, onComplete, null);
    }

	public WWW POST(string url, Dictionary<string, string> post, System.Action <string> onComplete, System.Action onError) {
        WWWForm form = new WWWForm();

        foreach (KeyValuePair<string, string> post_arg in post) {
            form.AddField(post_arg.Key, post_arg.Value);
        }

        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest(www, onComplete, onError));
        return www;
    }

	//public WWW AUTHENTICATION (string url, string token, System.Action <string> onComplete, System.Action onError) {
		//WWWForm form = new WWWForm();

		//Hashtable headers = new Hashtable();
		//headers["Authorization"] = "Token " + token;

		//WWW www = new WWW(url, form, headers);
		//StartCoroutine(WaitForRequest(www, onComplete, onError));
		//return www;
	//}

    //KIV -> if onError is not passed in and there is an error, nothing will happen
	private IEnumerator WaitForRequest(WWW www, System.Action <string> onComplete, System.Action onError) {
        yield return www;

        if (www.error == null) {
            //some GET/POST requests do not have an onComplete function
            if (onComplete != null) {
				onComplete(www.text);
            }
        }
        else if (onError != null) {
            onError();
        }
    }

}
